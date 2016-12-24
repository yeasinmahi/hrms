﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class P_Salary_Card_Procedure : ProcedureBase<P_Salary_Card_Procedure>
    {
        public P_Salary_Card_Procedure()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[P_Salary_Card_Procedure]"; }
        }

        protected override System.Data.SqlClient.SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@ProcessId", parameters[0]);
            

            return sqlParameters;
        }

        protected override P_Salary_Card_Procedure Map(System.Data.SqlClient.SqlDataReader dataReader)
        {
            P_Salary_Card_Procedure procedure = new P_Salary_Card_Procedure();

            return procedure;
        }

        public static DataTable GetDataSet(Int32 ProcessId)
        {
            return ReadDataSet(ProcessId).Tables[0];
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, DateTime StartDate, DateTime EndDate)
        {
            return ReadDataSet(transactionManager, StartDate, EndDate).Tables[0];
        }
    }
}
