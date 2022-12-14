using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class PunishmentSummery : ProcedureBase<PunishmentSummery>
    {
        public PunishmentSummery()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[PunishmentSummery]"; }
        }

        protected override SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@StartDate", parameters[0]);
            sqlParameters[1] = new SqlParameter("@EndDate", parameters[1]);
            

            return sqlParameters;
        }

        protected override PunishmentSummery Map(SqlDataReader dataReader)
        {
            PunishmentSummery procedure = new PunishmentSummery();

            return procedure;
        }

        public static DataTable GetDataSet(DateTime StartDate,DateTime EndDate)
        {
            return ReadDataSet(StartDate, EndDate).Tables[0];
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, DateTime StartDate, DateTime EndDate)
        {
            return ReadDataSet(transactionManager, StartDate, EndDate).Tables[0];
        }
    }
}
