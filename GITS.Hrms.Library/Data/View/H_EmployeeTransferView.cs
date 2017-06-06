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
			get {return _EmployeeName;}
			set {_EmployeeName = value;}
		}

        public H_EmployeeTransferHistory.Types Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

		public DateTime LetterDate
		{
			get {return _LetterDate;}
			set {_LetterDate = value;}
		}
        public DateTime JoiningDate
        {
            get { return _JoiningDate; }
            set { _JoiningDate = value; }
        }
		public String LetterNo
		{
			get {return _LetterNo;}
			set {_LetterNo = value;}
		}

		public String SourceBranch
		{
			get {return _SourceBranch;}
			set {_SourceBranch = value;}
		}

        public String DestinationBranch
        {
            get { return _DestinationBranch; }
            set { _DestinationBranch = value; }
        }

		public String Remarks
		{
			get {return _Remarks;}
			set {_Remarks = value;}
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
            get { return _Emp_ID; }
            set { _Emp_ID = value; }
        }

       
    }
}
