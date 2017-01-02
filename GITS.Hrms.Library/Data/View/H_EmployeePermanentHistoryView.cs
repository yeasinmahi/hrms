using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public class H_EmployeePermanentHistoryView : ViewBase<H_EmployeePermanentHistoryView>
    {
        private Int32 _H_EmployeeId;
        private String _Name;
        private Int32 _Code;
		private String _LetterNo;
		private DateTime _LetterDate;
		private DateTime _PermanentDate;
		private String _Remarks;

        public H_EmployeePermanentHistoryView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeePermanentHistoryView]"; }
		}

        protected override H_EmployeePermanentHistoryView Map(SqlDataReader dataReader)
		{
            H_EmployeePermanentHistoryView view = new H_EmployeePermanentHistoryView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
			view.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            view.Name = DBUtility.ToString(dataReader["Name"]);
            view.Code = DBUtility.ToInt32(dataReader["Code"]);
			view.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			view.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            view.PermanentDate = DBUtility.ToDateTime(dataReader["PermanentDate"]);
			view.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);

			return view;
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
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


		public DateTime PermanentDate
		{
			get {return this._PermanentDate;}
			set {this._PermanentDate = value;}
		}

		public String Remarks
		{
			get {return this._Remarks;}
			set {this._Remarks = value;}
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
