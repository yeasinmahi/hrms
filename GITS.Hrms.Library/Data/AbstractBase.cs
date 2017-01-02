using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data
{
    /// <summary>
    /// Summary description for AbstractBase
    /// </summary>
    [Serializable]
    public abstract class AbstractBase<T>
    {
        private Int32 _Id;
        private static AbstractBase<T> _Instance;
        protected abstract string AbstractName { get; }
        protected abstract T Map(SqlDataReader dataReader);

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected static AbstractBase<T> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = (AbstractBase<T>)Activator.CreateInstance(typeof(T));
                }

                return _Instance;
            }
        }

        [Property(PropertyAttribute.Attributes.AutoIncrement)]
        public Int32 Id
        {
            get { return this._Id; }
            set { this._Id = value; }
        }

        public static T GetById(Int32 id)
        {
            return Get("[Id] = '" + id + "'");
        }

        public static T GetById(TransactionManager transactionManager, Int32 id)
        {
            return Get(transactionManager, "[Id] = '" + id + "'");
        }

        public static T Get(string whereClause)
        {
            IList<T> list = Find(whereClause, "");

            if (list != null && list.Count > 0)
            {
                return list[0];
            }

            return default(T);
        }

        public static T Get(TransactionManager transactionManager, string whereClause)
        {
            IList<T> list = Find(transactionManager, whereClause, "");

            if (list != null && list.Count > 0)
            {
                return list[0];
            }

            return default(T);
        }

        public static IList<T> Find(string whereClause, string sortColumns)
        {
            TransactionManager tm = new TransactionManager(false);
            IList<T> list = Find(tm, whereClause, sortColumns);
            tm.Commit();

            return list;
        }
        public static IList<T> FindByLogin(string whereClause, string sortColumns, int start, int count, out Int32 total)
        {
            TransactionManager tm = new TransactionManager(false);
            IList<T> list = FindByLogin(tm, whereClause, sortColumns, start, count, out total);
            tm.Commit();

            return list;
        }

        public static IList<T> Find(TransactionManager transactionManager, string whereClause, string sortColumns)
        {
            string strSql = "SELECT * FROM " + Instance.AbstractName;
            IList<T> list = new List<T>();

            if (whereClause.Trim() != "")
            {
                strSql += " WHERE " + whereClause;
            }

            if (sortColumns.Trim() != "")
            {
                strSql += " ORDER BY " + sortColumns;
            }

            SqlCommand cmd = transactionManager.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(Instance.Map(dr));
            }

            dr.Close();
            dr.Dispose();
            cmd.Cancel();

            return list;
        }
        public static IList<T> FindByLogin(TransactionManager transactionManager, string whereClause, string sortColumns, int start, int count, out int total)
        {
            string strSql = "SELECT ROW_NUMBER() over(Order by Code) as SL, * FROM " + Instance.AbstractName;
            IList<T> list = new List<T>();

            if (whereClause.Trim() != "")
            {
                strSql += " WHERE " + whereClause;
            }

            if (sortColumns.Trim() != "")
            {
                strSql += " ORDER BY " + sortColumns;
            }
            
            SqlCommand cmd = transactionManager.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(*) AS Total FROM " + Instance.AbstractName + " WHERE " + whereClause; ;
            total = Convert.ToInt32(cmd.ExecuteScalar());

            if (total == 0)
            {
                cmd.Dispose();

                return list;
            }
            strSql = "SELECT t.* FROM (" + strSql + ")t Where t.SL >=" + start + " AND t.SL<" + (start + count);
            cmd.CommandText = strSql;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(Instance.Map(dr));
            }

            dr.Close();
            dr.Dispose();
            cmd.Cancel();

            return list;
        }
        public static IList<T> Find(string whereClause, string sortColumns, int start, int count, out Int32 total)
        {
            TransactionManager tm = new TransactionManager(false);
            IList<T> list = Find(tm, whereClause, sortColumns, start, count, out total);
            tm.Commit();

            return list;
        }

        public static IList<T> Find(TransactionManager transactionManager, string whereClause, string sortColumns, int start, int count, out int total)
        {
            string strSql = "SELECT TOP " + count + " * FROM " + Instance.AbstractName;

            IList<T> list = new List<T>();

            SqlCommand cmd = transactionManager.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT COUNT(*) AS Total FROM " + Instance.AbstractName;

            if (whereClause.Trim() != "")
            {
                strSql += " WHERE Id NOT IN(SELECT TOP " + (start - 1) + " Id FROM " + Instance.AbstractName + " WHERE " + whereClause;

                if (sortColumns.Trim() != "")
                {
                    strSql += " ORDER BY " + sortColumns;
                }
                else
                {
                    strSql += " ORDER BY Id";
                }

                strSql += ") AND " + whereClause;

                cmd.CommandText += " WHERE " + whereClause;
            }
            else
            {
                strSql += " WHERE Id NOT IN(SELECT TOP " + (start - 1) + " Id FROM " + Instance.AbstractName;

                if (sortColumns.Trim() != "")
                {
                    strSql += " ORDER BY " + sortColumns;
                }
                else
                {
                    strSql += " ORDER BY Id";
                }

                strSql += ")";
            }

            if (sortColumns.Trim() != "")
            {
                strSql += " ORDER BY " + sortColumns;
            }
            else
            {
                strSql += " ORDER BY Id";
            }

            total = Convert.ToInt32(cmd.ExecuteScalar());

            if (total == 0)
            {
                cmd.Dispose();

                return list;
            }

            cmd.CommandText = strSql;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(Instance.Map(dr));
            }

            dr.Close();
            dr.Dispose();
            cmd.Dispose();

            return list;
        }

        public static IList<T> FindAll()
        {
            return Find("", "");
        }

        public static IList<T> FindAll(string sortColumns)
        {
            return Find("", sortColumns);
        }

        public static IList<T> FindAll(TransactionManager transactionManager)
        {
            return Find(transactionManager, "", "");
        }

        public static IList<T> FindAll(TransactionManager transactionManager, string sortColumns)
        {
            return Find(transactionManager, "", sortColumns);
        }
    }
}