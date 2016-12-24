using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class EmployeePunishmentReportProcedure : ProcedureBase<EmployeePunishmentReportProcedure>
    {
        public EmployeePunishmentReportProcedure()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[EmployeePunishmentReport]"; }
        }

        protected override System.Data.SqlClient.SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@EmpId", parameters[0]);
            sqlParameters[1] = new SqlParameter("@Type", parameters[1] == null ? DBNull.Value : parameters[1]);

            return sqlParameters;
        }

        protected override EmployeePunishmentReportProcedure Map(System.Data.SqlClient.SqlDataReader dataReader)
        {
            EmployeePunishmentReportProcedure procedure = new EmployeePunishmentReportProcedure();

            return procedure;
        }

        public static DataTable GetDataSet(Int32 EmpID, Int32 Type)
        {
            return ReadDataSet(EmpID, Type).Tables[0];
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, Int32 startId, Int32 endId)
        {
            return ReadDataSet(transactionManager, startId, endId).Tables[0];
        }
    }
}
