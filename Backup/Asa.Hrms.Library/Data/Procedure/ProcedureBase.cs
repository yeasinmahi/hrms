using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    abstract public class ProcedureBase<T>
    {
        private static ProcedureBase<T> _Instance;
        protected abstract string AbstractName { get; }
        protected abstract SqlParameter[] GetParameters(params Object[] parameters);
        protected abstract T Map(SqlDataReader dataReader);

        protected static ProcedureBase<T> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = (ProcedureBase<T>)Activator.CreateInstance(typeof(T));
                }

                return _Instance;
            }
        }

        protected static DataSet ReadDataSet(TransactionManager transactionManager, params Object[] parameters)
        {
            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.SelectCommand = transactionManager.CreateCommand();
            dataAdapter.SelectCommand.CommandText = Instance.AbstractName;
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            if (parameters != null && parameters.Length > 0)
            {
                dataAdapter.SelectCommand.Parameters.AddRange(Instance.GetParameters(parameters));
            }

            dataAdapter.Fill(dataSet);
            dataAdapter.Dispose();

            return dataSet;
        }

        protected static DataSet ReadDataSet(params Object[] parameters)
        {
            TransactionManager tm = new TransactionManager(false);
            return ReadDataSet(tm, parameters);
        }

        protected static IList<T> Read(TransactionManager transactionManager, params Object[] parameters)
        {
            IList<T> list = new List<T>();
            SqlCommand cmd = transactionManager.CreateCommand();
            cmd.CommandText = Instance.AbstractName;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter[] sqlParams = Instance.GetParameters(parameters);
            if(sqlParams != null)
                cmd.Parameters.AddRange(sqlParams);

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

        protected static IList<T> Read(params Object[] parameters)
        {
            TransactionManager tm = new TransactionManager(false);
            return Read(tm, parameters);
        }
    }
}
