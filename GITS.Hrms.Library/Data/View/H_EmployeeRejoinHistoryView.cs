using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public class H_EmployeeRejoinHistoryView : ViewBase<H_EmployeeRejoinHistoryView>
    {
        private string _Name;
        private Int32 _Code;
		private H_EmployeeLeave.Types _LeaveType;
		private String _LetterNo;
		private DateTime _LetterDate;
		private DateTime _FromDate;
		private DateTime _RejoinDate;
		private string _SourceBranch;
		private string _DestinationBranch;
        private H_Employee.EmploymentTypes _RejoinType;


        public H_EmployeeRejoinHistoryView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_EmployeeRejoinHistoryView]"; }
		}

        protected override H_EmployeeRejoinHistoryView Map(SqlDataReader dataReader)
		{
            H_EmployeeRejoinHistoryView view = new H_EmployeeRejoinHistoryView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
            view.Name = DBUtility.ToString(dataReader["Name"]);
            view.Code = DBUtility.ToInt32(dataReader["Code"]);
            view.LeaveType = (H_EmployeeLeave.Types)DBUtility.ToInt32(dataReader["LeaveType"]);
			view.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			view.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			view.FromDate = DBUtility.ToDateTime(dataReader["FromDate"]);
			view.RejoinDate = DBUtility.ToDateTime(dataReader["RejoinDate"]);
            view.SourceBranch = DBUtility.ToString(dataReader["SourceBranch"]);
            view.DestinationBranch = DBUtility.ToString(dataReader["DestinationBranch"]);
            view.RejoinType = (H_Employee.EmploymentTypes)DBUtility.ToInt32(dataReader["RejoinType"]);

			return view;
		}

        public H_EmployeeLeave.Types LeaveType
		{
			get {return this._LeaveType;}
			set {this._LeaveType = value;}
		}

		public String LetterNo
		{
			get {return this._LetterNo;}
			set {this._LetterNo = value;}
		}

		public DateTime LetterDate
		{
			get {return this._LetterDate;}
			set {this._LetterDate = value;}
		}

		public DateTime FromDate
		{
			get {return this._FromDate;}
			set {this._FromDate = value;}
		}

		public DateTime RejoinDate
		{
			get {return this._RejoinDate;}
			set {this._RejoinDate = value;}
		}

		public String  SourceBranch
		{
			get {return this._SourceBranch;}
			set {this._SourceBranch = value;}
		}

		public String  DestinationBranch
		{
			get {return this._DestinationBranch;}
			set {this._DestinationBranch = value;}
		}

        public H_Employee.EmploymentTypes RejoinType
        {
            get { return this._RejoinType; }
            set { this._RejoinType = value; }
        }


        public String Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public Int32 Code
        {
            get { return this._Code; }
            set { this._Code = value; }
        }
    }
}
