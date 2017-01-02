using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class Employee_Resume_Procedure : ProcedureBase<Employee_Resume_Procedure>
    {
        public Employee_Resume_Procedure()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[Employee_Resume_Procedure]"; }
        }

        protected override Employee_Resume_Procedure Map(SqlDataReader dataReader)
        {
            Employee_Resume_Procedure procedure = new Employee_Resume_Procedure();

            return procedure;
        }

        protected override SqlParameter[] GetParameters(params Object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@Code", parameters[0]);

            return sqlParameters;
        }

        public static DataTable GetDataSet(Int32 Code)
        {
            return ReadDataSet(Code).Tables[0];
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, Int32 Code)
        {
            return ReadDataSet(transactionManager, Code).Tables[0];
        }
    }
}
