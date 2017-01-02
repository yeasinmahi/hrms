using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class EmployeeAgeInformation : ProcedureBase<EmployeeAgeInformation>
    {
        public EmployeeAgeInformation()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[EmployeeAgeInformation]"; }
        }

        protected override EmployeeAgeInformation Map(SqlDataReader dataReader)
        {
            EmployeeAgeInformation procedure = new EmployeeAgeInformation();

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
