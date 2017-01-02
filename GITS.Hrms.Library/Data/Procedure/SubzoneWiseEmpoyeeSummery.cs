using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class SubzoneWiseEmpoyeeSummery : ProcedureBase<SubzoneWiseEmpoyeeSummery>
    {
        public SubzoneWiseEmpoyeeSummery()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[SubzoneWiseEmpoyeeSummery]"; }
        }

        protected override System.Data.SqlClient.SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@GroupType1", parameters[0]);
            sqlParameters[1] = new SqlParameter("@GroupType2", parameters[1]);
            return sqlParameters;
        }

        protected override SubzoneWiseEmpoyeeSummery Map(System.Data.SqlClient.SqlDataReader dataReader)
        {
            SubzoneWiseEmpoyeeSummery procedure = new SubzoneWiseEmpoyeeSummery();

            return procedure;
        }

        public static DataTable GetDataSet(int GroupType1, int GroupType2)
        {
            return ReadDataSet(GroupType1,GroupType2).Tables[0];
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, int GroupType1, int GroupType2)
        {
            return ReadDataSet(transactionManager, GroupType1,GroupType2).Tables[0];
        }
    }
}
