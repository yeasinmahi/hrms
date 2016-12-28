using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class OwnDistrictWiseTotalStaffProcedure : ProcedureBase<OwnDistrictWiseTotalStaffProcedure>
    {
        public OwnDistrictWiseTotalStaffProcedure()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[Own_District_Designation_Wise_Total_Staff]"; }
        }

        protected override System.Data.SqlClient.SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@AsOnDate", parameters[0]);
            

            return sqlParameters;
        }

        protected override OwnDistrictWiseTotalStaffProcedure Map(System.Data.SqlClient.SqlDataReader dataReader)
        {
            OwnDistrictWiseTotalStaffProcedure procedure = new OwnDistrictWiseTotalStaffProcedure();

            return procedure;
        }

        public static DataTable GetDataSet(DateTime AsOnDate)
        {
            return ReadDataSet(AsOnDate).Tables[0];
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, DateTime AsOnDate)
        {
            return ReadDataSet(transactionManager, AsOnDate).Tables[0];
        }
    }
}
