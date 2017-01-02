using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public class H_EmployeeTransferView : ViewBase<H_EmployeeTransferView>
    {
		private String _EmployeeName;
        private H_EmployeeTransferHistory.Types _Type;
		private DateTime _LetterDate;
		private String _LetterNo;
		private String _SourceBranch;
        private String _DestinationBranch;
        private DateTime _JoiningDate;
        private String _Remarks;
        private Int32 _Emp_ID;
        //private String _PresentMobile;
        //private String _PastMobile;


        public H_EmployeeTransferView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_EmployeeTransferView]"; }
		}

        protected override H_EmployeeTransferView Map(SqlDataReader dataReader)
		{
            H_EmployeeTransferView view = new H_EmployeeTransferView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
            view.EmployeeName = DBUtility.ToString(dataReader["EmployeeName"]);
            view.Emp_ID = DBUtility.ToInt32(dataReader["Emp_ID"]);
            view.Type = (H_EmployeeTransferHistory.Types)DBUtility.ToInt32(dataReader["Type"]);
            view.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            view.JoiningDate = DBUtility.ToDateTime(dataReader["JoiningDate"]);
            view.LetterNo = DBUtility.ToNullableString(dataReader["LetterNo"]);
            view.SourceBranch = DBUtility.ToString(dataReader["SourceBranch"]);
            view.DestinationBranch = DBUtility.ToString(dataReader["DestinationBranch"]);
            view.Remarks = DBUtility.ToString(dataReader["Remarks"]);
            //view.PresentMobile = DBUtility.ToString(dataReader["PresentMobile"]);
            //view.PastMobile = DBUtility.ToString(dataReader["PastMobile"]);

			return view;
		}


		public String EmployeeName
		{
			get {return this._EmployeeName;}
			set {this._EmployeeName = value;}
		}

        public H_EmployeeTransferHistory.Types Type
        {
            get { return this._Type; }
            set { this._Type = value; }
        }

		public DateTime LetterDate
		{
			get {return this._LetterDate;}
			set {this._LetterDate = value;}
		}
        public DateTime JoiningDate
        {
            get { return this._JoiningDate; }
            set { this._JoiningDate = value; }
        }
		public String LetterNo
		{
			get {return this._LetterNo;}
			set {this._LetterNo = value;}
		}

		public String SourceBranch
		{
			get {return this._SourceBranch;}
			set {this._SourceBranch = value;}
		}

        public String DestinationBranch
        {
            get { return this._DestinationBranch; }
            set { this._DestinationBranch = value; }
        }

		public String Remarks
		{
			get {return this._Remarks;}
			set {this._Remarks = value;}
		}
        //public String PresentMobile
        //{
        //    get { return this._PresentMobile; }
        //    set { this._PresentMobile = value; }
        //}
        //public String PastMobile
        //{
        //    get { return this._PastMobile; }
        //    set { this._PastMobile = value; }
        //}
        public Int32 Emp_ID
        {
            get { return this._Emp_ID; }
            set { this._Emp_ID = value; }
        }

       
    }
}
