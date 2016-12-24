using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class MinoritiesInformationOfEmployees : ProcedureBase<MinoritiesInformationOfEmployees>
    {
        public MinoritiesInformationOfEmployees()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[MinoritiesInformationOfEmployees]"; }
        }

        protected override MinoritiesInformationOfEmployees Map(SqlDataReader dataReader)
        {
            MinoritiesInformationOfEmployees procedure = new MinoritiesInformationOfEmployees();

            return procedure;
        }

        protected override SqlParameter[] GetParameters(params Object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@AsOnDate", parameters[0]);

            return sqlParameters;
        }

        public static DataTable GetDataSet(DateTime asOnDate)
        {
            return ReadDataSet(asOnDate).Tables[0];
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, DateTime asOnDate)
        {
            return ReadDataSet(transactionManager, asOnDate).Tables[0];
        }
    }
}
