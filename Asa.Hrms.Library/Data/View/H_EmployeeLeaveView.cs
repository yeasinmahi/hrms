using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public class H_EmployeeLeaveView : ViewBase<H_EmployeeLeaveView>
    {
        private Int32 _H_EmployeeId;
        private string _Name;
        private Int32 _Code;
        private H_EmployeeLeave.Types _Type;
        private H_EmployeeLeave.JoinTypes _JoinType;
		private String _LetterNo;
		private DateTime _LetterDate;
		private DateTime _StartDate;
		private Nullable<DateTime> _EndDate;
		private String _Cause;
        private Int32 _Status;

        public H_EmployeeLeaveView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeeLeaveView]"; }
		}

        protected override H_EmployeeLeaveView Map(SqlDataReader dataReader)
		{
            H_EmployeeLeaveView entity = new H_EmployeeLeaveView();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.Code = DBUtility.ToInt32(dataReader["Code"]);
            entity.Type = (H_EmployeeLeave.Types)DBUtility.ToInt32(dataReader["Type"]);
            entity.JoinType = (H_EmployeeLeave.JoinTypes)DBUtility.ToInt32(dataReader["JoinType"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
			entity.EndDate = DBUtility.ToNullableDateTime(dataReader["EndDate"]);
			entity.Cause = DBUtility.ToNullableString(dataReader["Cause"]);
            entity.Status = DBUtility.ToInt32(dataReader["Status"]);

			return entity;
		}

        
		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}
        public Int32 Code
        {
            get { return this._Code; }
            set { this._Code = value; }
        }
        public H_EmployeeLeave.Types Type
		{
			get {return this._Type;}
			set {this._Type = value;}
		}

        public H_EmployeeLeave.JoinTypes JoinType
        {
            get { return this._JoinType; }
            set { this._JoinType = value; }
        }

		public String LetterNo
		{
			get {return this._LetterNo;}
			set {this._LetterNo = value;}
		}
        public String Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }
		public DateTime LetterDate
		{
			get {return this._LetterDate;}
			set {this._LetterDate = value;}
		}

		public DateTime StartDate
		{
			get {return this._StartDate;}
			set {this._StartDate = value;}
		}

		public Nullable<DateTime> EndDate
		{
			get {return this._EndDate;}
			set {this._EndDate = value;}
		}

		public String Cause
		{
			get {return this._Cause;}
			set {this._Cause = value;}
		}
        public Int32 Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
    }
}
