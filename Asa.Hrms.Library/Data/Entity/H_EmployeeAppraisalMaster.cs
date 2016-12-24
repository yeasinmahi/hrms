using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeeAppraisalMaster : EntityBase<H_EmployeeAppraisalMaster>
    {
        private Int32 _H_AppraisalId;
        private Int32 _H_EmployeeId;
        private Int32 _Appraiser;
        private DateTime _AppraisalDate;
        private String _EntryUser;

        public H_EmployeeAppraisalMaster()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_EmployeeAppraisalMaster]"; }
		}

        protected override H_EmployeeAppraisalMaster Map(SqlDataReader dataReader)
		{
            H_EmployeeAppraisalMaster entity = new H_EmployeeAppraisalMaster();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_AppraisalId = DBUtility.ToInt32(dataReader["H_AppraisalId"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Appraiser = DBUtility.ToInt32(dataReader["Appraiser"]);
            entity.AppraisalDate = DBUtility.ToDateTime(dataReader["AppraisalDate"]);
            entity.EntryUser = DBUtility.ToString(dataReader["EntryUser"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}
        public static H_EmployeeAppraisalMaster GetByEmployeeId(Int32 h_EmployeeId)
        {
            return Get("[H_EmployeeId] = " + h_EmployeeId);
        }
        public Int32 H_AppraisalId
		{
            get { return this._H_AppraisalId; }
            set { this._H_AppraisalId = value; }
		}
        public Int32 H_EmployeeId
        {
            get { return this._H_EmployeeId; }
            set { this._H_EmployeeId = value; }
        }
        public Int32 Appraiser
        {
            get { return this._Appraiser; }
            set { this._Appraiser = value; }
        }
        public DateTime AppraisalDate
        {
            get { return this._AppraisalDate; }
            set { this._AppraisalDate = value; }
        }
        public String EntryUser
        {
            get { return this._EntryUser; }
            set { this._EntryUser = value; }
        }
    }
}
