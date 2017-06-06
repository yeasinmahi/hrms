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
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}
        public Int32 Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public H_EmployeeLeave.Types Type
		{
			get {return _Type;}
			set {_Type = value;}
		}

        public H_EmployeeLeave.JoinTypes JoinType
        {
            get { return _JoinType; }
            set { _JoinType = value; }
        }

		public String LetterNo
		{
			get {return _LetterNo;}
			set {_LetterNo = value;}
		}
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
		public DateTime LetterDate
		{
			get {return _LetterDate;}
			set {_LetterDate = value;}
		}

		public DateTime StartDate
		{
			get {return _StartDate;}
			set {_StartDate = value;}
		}

		public Nullable<DateTime> EndDate
		{
			get {return _EndDate;}
			set {_EndDate = value;}
		}

		public String Cause
		{
			get {return _Cause;}
			set {_Cause = value;}
		}
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}
