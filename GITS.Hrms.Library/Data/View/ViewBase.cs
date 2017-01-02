using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    /// <summary>
    /// Summary description for ViewBase
    /// </summary>
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public abstract class ViewBase<T> : AbstractBase<T>
    {
        private static ViewBase<T> _Instance;

        protected new static ViewBase<T> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = (ViewBase<T>)Activator.CreateInstance(typeof(T));
                }

                return _Instance;
            }
        }

        public static DataSet ReadDataSet(TransactionManager transactionManager, string whereClause, string sortColumns)
        {
            string strSql = "SELECT * FROM " + Instance.AbstractName;

            if (whereClause.Trim() != "")
            {
                strSql += " WHERE " + whereClause;
            }

            if (sortColumns.Trim() != "")
            {
                strSql += " ORDER BY " + sortColumns;
            }

            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.SelectCommand = transactionManager.CreateCommand();
            dataAdapter.SelectCommand.CommandText = strSql;

            dataAdapter.Fill(dataSet);
            dataAdapter.Dispose();

            return dataSet;
        }

        public static DataSet ReadDataSet(string whereClause, string sortColumns)
        {
            TransactionManager tm = new TransactionManager(false);

            return ReadDataSet(tm, whereClause, sortColumns);
        }
    }
}
