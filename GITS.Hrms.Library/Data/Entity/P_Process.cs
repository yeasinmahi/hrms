using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class P_Process : EntityBase<P_Process>
    {
        private DateTime _SalaryDate;
        private DateTime _ExecutionDate;
        private String _UserLogin;
        private Boolean _IsProcessEnd;

                
        public P_Process()
		{

		}
        protected override bool Audit
        {
            get
            {
                return false;
            }
        }

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_Process]"; }
		}
        protected override P_Process Map(SqlDataReader dataReader)
        {
            P_Process entity = new P_Process();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.SalaryDate = DBUtility.ToDateTime(dataReader["SalaryDate"]);
            entity.ExecutionDate = DBUtility.ToDateTime(dataReader["ExecutionDate"]);
            entity.UserLogin = DBUtility.ToString(dataReader["UserLogin"]);
            entity.IsProcessEnd = DBUtility.ToBoolean(dataReader["IsProcessEnd"]);

            entity.EntityState = EntityStates.Clean;

            return entity;
        }
        public static P_Process GetBySalaryDate(DateTime salaryDate)
        {
            return Get("SalaryDate='" + salaryDate.ToString("yyyy-MM-dd") + "'");
        }
        public DateTime SalaryDate
        {
            get { return _SalaryDate; }
            set { _SalaryDate = value; }
        }
        public DateTime ExecutionDate
        {
            get { return _ExecutionDate; }
            set { _ExecutionDate = value; }
        }

        public String UserLogin
        {
            get { return _UserLogin; }
            set { _UserLogin = value; }
        }

        public Boolean IsProcessEnd
        {
            get { return _IsProcessEnd; }
            set { _IsProcessEnd = value; }
        }
    }
}
