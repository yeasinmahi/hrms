using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class AtaGlanceTotalStaffPositionProcedure : ProcedureBase<AtaGlanceTotalStaffPositionProcedure>
    {
        public AtaGlanceTotalStaffPositionProcedure()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[At_A_Glance_Total_Staff_Position]"; }
        }

        protected override SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@AsOnDate", parameters[0]);
            

            return sqlParameters;
        }

        protected override AtaGlanceTotalStaffPositionProcedure Map(SqlDataReader dataReader)
        {
            AtaGlanceTotalStaffPositionProcedure procedure = new AtaGlanceTotalStaffPositionProcedure();

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
