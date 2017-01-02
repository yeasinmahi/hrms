using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class EmployeeEducationInformation : ProcedureBase<EmployeeEducationInformation>
    {
        public EmployeeEducationInformation()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[EmployeeEducationInformation]"; }
        }

        protected override EmployeeEducationInformation Map(SqlDataReader dataReader)
        {
            EmployeeEducationInformation procedure = new EmployeeEducationInformation();

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
