using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
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

        protected override SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@ProcessId", parameters[0]);
            

            return sqlParameters;
        }

        protected override P_Salary_Card_Procedure Map(SqlDataReader dataReader)
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
