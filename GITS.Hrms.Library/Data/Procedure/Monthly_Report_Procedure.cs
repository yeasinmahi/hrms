using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class Monthly_Report_Procedure : ProcedureBase<Monthly_Report_Procedure>
    {
        public Monthly_Report_Procedure()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[Monthly_Report_Procedure]"; }
        }

        protected override SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@StartDate", parameters[0]);

            return sqlParameters;
        }

        protected override Monthly_Report_Procedure Map(SqlDataReader dataReader)
        {
            Monthly_Report_Procedure procedure = new Monthly_Report_Procedure();

            return procedure;
        }

        public static DataTable GetDataSet(DateTime StartDate)
        {
            return ReadDataSet(StartDate).Tables[0];
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, DateTime StartDate)
        {
            return ReadDataSet(transactionManager, StartDate).Tables[0];
        }
    }
}
