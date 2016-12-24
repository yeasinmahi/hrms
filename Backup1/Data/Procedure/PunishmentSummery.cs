using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Procedure
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

        protected override System.Data.SqlClient.SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@StartDate", parameters[0]);
            sqlParameters[1] = new SqlParameter("@EndDate", parameters[1]);
            

            return sqlParameters;
        }

        protected override PunishmentSummery Map(System.Data.SqlClient.SqlDataReader dataReader)
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
