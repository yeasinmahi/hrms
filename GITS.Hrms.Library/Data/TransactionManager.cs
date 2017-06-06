using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Configuration = GITS.Hrms.Library.Utility.Configuration;

namespace GITS.Hrms.Library.Data
{
    /// <summary>
    /// Summary description for TransactionManager
    /// </summary>
    public class TransactionManager
    {
        private SqlConnection _Connection = null;
        private SqlTransaction _Transaction = null;
        private Int32 _DbTransactionId;
        private Int32 _RecordAffected = 0;

        public Int32 DbTransactionId
        {
            get { return _DbTransactionId; }
        }

        public Int32 RecordAffected
        {
            get { return _RecordAffected; }
            set { _RecordAffected = value; }
        }

        public TransactionManager(Boolean beginTransaction, String description, String database)
        {
            SqlConnection.ClearAllPools();

            String cs = Configuration.ConnectionString;

            if (database == null)
            {
                cs = cs.Replace("Initial Catalog=" + Configuration.DatabaseName + ";", "");
            }
            else if (database != "")
            {
                cs = cs.Replace("Initial Catalog=" + Configuration.DatabaseName + ";", "Initial Catalog=" + database + ";");
            }

            cs = DBUtility.DecryptConnectionString(cs);

            _Connection = new SqlConnection(cs);
            _Connection.Open();

            if (beginTransaction)
            {
                _Transaction = _Connection.BeginTransaction();

                DbTransaction dbt = new DbTransaction();

                if (HttpContext.Current == null)
                {
                    dbt.CreatedBy = "admin";
                }
                else
                {
                    dbt.CreatedBy = HttpContext.Current.User.Identity.Name;
                }
                
                dbt.CreatedDate = DateTime.Now;
                dbt.Description = description;

                DbTransaction.Insert(this, dbt);

                _DbTransactionId = dbt.Id;
            }
        }

        public TransactionManager(Boolean beginTransaction, String description)
            : this(beginTransaction, description, "")
        {
        }

        //public TransactionManager(String database, Boolean beginTransaction)
        //    : this(beginTransaction, null, database)
        //{
        //}

        public TransactionManager(Boolean beginTransaction)
            : this(beginTransaction, null)
        {
        }

        public void Rollback()
        {
            if (_Transaction != null)
            {
                _Transaction.Rollback();
            }

            if (_Connection != null && _Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }

            if (_Transaction != null)
            {
                _Transaction.Dispose();
            }
        }

        public void Commit()
        {
            if (_Transaction != null)
            {
                if (RecordAffected > 1)
                {
                    _Transaction.Commit();
                }
                else
                {
                    Rollback();
                }
            }

            if (_Connection != null && _Connection.State == ConnectionState.Open)
            {
                _Connection.Close();
            }

            if (_Transaction != null)
            {
                _Transaction.Dispose();
            }
        }

        public SqlCommand CreateCommand()
        {
            SqlCommand cmd = _Connection.CreateCommand();
            cmd.CommandTimeout = _Connection.ConnectionTimeout;
            cmd.Transaction = _Transaction;
            return cmd;
        }

        public int ExecuteUpdate(string sql)
        {
            SqlCommand cmd = CreateCommand();
            cmd.CommandText = sql;

            int effectedRows = cmd.ExecuteNonQuery();
            _RecordAffected += effectedRows;

            return effectedRows;
        }

        public DataSet GetDataSet(string sql)
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            
            dataAdapter.SelectCommand = CreateCommand();
            dataAdapter.SelectCommand.CommandText = sql;
            dataAdapter.Fill(dataSet);
            
            return dataSet;
        }

        public DataSet GetDataSet(string sql, DataSet ds)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.SelectCommand = CreateCommand();
            dataAdapter.SelectCommand.CommandText = sql;
            dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            dataAdapter.Fill(ds);

            return ds;
        }

        public Message SetDataSet(DataSet ds, string dbName)
        {
            Message msg = new Message();

            try
            {
                Server server = new Server(new ServerConnection(_Connection));
                if (server.Databases.Contains(dbName))
                {
                    server.Databases[dbName].Drop();
                }
                Database db = new Database(server, dbName);
                db.Create();

                //set database to the newly created database
                db = server.Databases[dbName];

                foreach (DataTable dt in ds.Tables)
                {
                    //create a new SMO table
                    Table nt = new Table(db, dt.TableName);
                    //Index idx = new Index(nt, "PK_" + dt.TableName + "_Id");
                    //idx.IndexKeyType = IndexKeyType.DriPrimaryKey;  

                    //SMO Column object referring to destination table.
                    Column nc = new Column();

                    //add the column names and types from the datatable into the new table
                    //using the columns name and type property
                    foreach (DataColumn dc in dt.Columns)
                    {
                        //create columns from datatable column schema
                        nc = new Column(nt, dc.ColumnName);
                        nc.DataType = GetDataType(dc.DataType.ToString(), dc.MaxLength);
                        //nc.Nullable = dc.AllowDBNull;

                        // create primary key for Id field
                        //if (dc.ColumnName == "Id")
                        //{
                        //    idx.IndexedColumns.Add(new IndexedColumn(idx, "Id", false));
                        //    nt.Indexes.Add(idx);
                        //}

                        nt.Columns.Add(nc);
                    }
                    // create the destination table                    
                    nt.Create();
                }

                SqlBulkCopy bulkCopy = new SqlBulkCopy(_Connection);

                foreach (DataTable dt in ds.Tables)
                {
                    bulkCopy.DestinationTableName = "[" + dbName + "].[dbo].["  + dt.TableName + "]";
                    bulkCopy.WriteToServer(dt);
                }
            }
            catch (Exception ex)
            {
                msg.Type = MessageType.Error;
                msg.Msg = ex.Message;
            }

            return msg;
        }
        
        public DataType GetDataType(string dataType, int len)
        {
            DataType DTTemp = null;

            switch (dataType)
            {
                case ("System.Decimal"):
                    DTTemp = DataType.Decimal(2, 18);
                    break;
                case ("System.String"):
                    DTTemp = DataType.VarChar(len);
                    break;
                case ("System.Int16"):
                case ("System.Int32"):
                    DTTemp = DataType.Int;
                    break;
                case ("System.Int64"):
                    DTTemp = DataType.BigInt;
                    break;
                case ("System.Boolean"):
                    DTTemp = DataType.Bit;
                    break;
                case ("System.DateTime"):
                    DTTemp = DataType.DateTime;
                    break;
                case ("System.Double"):
                    DTTemp = DataType.Float;
                    break;
            }
            return DTTemp;
        }
    }
}
