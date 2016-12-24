using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class GradeQualificationWiseReport : ProcedureBase<GradeQualificationWiseReport>
    {
        public GradeQualificationWiseReport()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[GradeQualificationWiseReport]"; }
        }

        protected override System.Data.SqlClient.SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[10];

            sqlParameters[0] = new SqlParameter("@GradeId", parameters[0]);
            sqlParameters[1] = new SqlParameter("@Desg", parameters[1]);
            sqlParameters[2] = new SqlParameter("@MastersGradeJoin", parameters[2]);
            sqlParameters[3] = new SqlParameter("@MastersDate", parameters[3]);
            sqlParameters[4] = new SqlParameter("@BachelorGradeJoin", parameters[4]);
            sqlParameters[5] = new SqlParameter("@BachelorDate", parameters[5]);
            sqlParameters[6] = new SqlParameter("@HSCGradeJoin", parameters[6]);
            sqlParameters[7] = new SqlParameter("@HSCDate", parameters[7]);
            sqlParameters[8] = new SqlParameter("@BelowHSCGradeJoin", parameters[8]);
            sqlParameters[9] = new SqlParameter("@BelowHSCDate", parameters[9]);

            return sqlParameters;
        }

        protected override GradeQualificationWiseReport Map(System.Data.SqlClient.SqlDataReader dataReader)
        {
            GradeQualificationWiseReport procedure = new GradeQualificationWiseReport();

            return procedure;
        }

        public static DataSet GetDataSet(int GradeId, string Desg, int MastersGradeJoin, DateTime? MastersDate, int BachelorGradeJoin, DateTime? BachelorDate, int HSCGradeJoin, DateTime? HSCDate, int BelowHSCGradeJoin, DateTime? BelowHSCDate)
        {
            return ReadDataSet(GradeId, Desg, MastersGradeJoin, MastersDate, BachelorGradeJoin, BachelorDate, HSCGradeJoin, HSCDate, BelowHSCGradeJoin, BelowHSCDate);
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, int mfFlag, DateTime AsOnDate)
        {
            return ReadDataSet(transactionManager, mfFlag, AsOnDate).Tables[0];
        }
    }
}
