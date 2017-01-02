using System;
using System.Data;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class LetterOfIdCardProcedure : ProcedureBase<LetterOfIdCardProcedure>
    {
        public LetterOfIdCardProcedure()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[LetterOfIdCardProcedure]"; }
        }

        protected override System.Data.SqlClient.SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@StartId", parameters[0]);
            sqlParameters[1] = new SqlParameter("@EndId", parameters[1] == null ? DBNull.Value : parameters[1]);

            return sqlParameters;
        }

        protected override LetterOfIdCardProcedure Map(System.Data.SqlClient.SqlDataReader dataReader)
        {
            LetterOfIdCardProcedure procedure = new LetterOfIdCardProcedure();

            return procedure;
        }

        public static DataTable GetDataSet(Int32 startId, Int32 endId)
        {
            return ReadDataSet(startId, endId).Tables[0];
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, Int32 startId, Int32 endId)
        {
            return ReadDataSet(transactionManager, startId, endId).Tables[0];
        }
    }
}
