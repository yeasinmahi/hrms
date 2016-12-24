using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Procedure
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Procedure)]
    public class MaleFemaleSummery : ProcedureBase<MaleFemaleSummery>
    {
        public MaleFemaleSummery()
        { }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[MaleFemaleSummery]"; }
        }

        protected override System.Data.SqlClient.SqlParameter[] GetParameters(params object[] parameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@mfFlag", parameters[0]);
            sqlParameters[1] = new SqlParameter("@AsOnDate", parameters[1]);

            return sqlParameters;
        }

        protected override MaleFemaleSummery Map(System.Data.SqlClient.SqlDataReader dataReader)
        {
            MaleFemaleSummery procedure = new MaleFemaleSummery();

            return procedure;
        }

        public static DataTable GetDataSet(int mfFlag,DateTime AsOndate)
        {
            return ReadDataSet(mfFlag, AsOndate).Tables[0];
        }

        public static DataTable GetDataSet(TransactionManager transactionManager, int mfFlag,DateTime AsOnDate)
        {
            return ReadDataSet(transactionManager, mfFlag, AsOnDate).Tables[0];
        }
    }
}
