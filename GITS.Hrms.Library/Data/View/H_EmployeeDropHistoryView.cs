using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public class H_EmployeeDropHistoryView : ViewBase<H_EmployeeDropHistoryView>
    {
        private String _Name;
        private Int32 _Code;
        private H_EmployeeDropHistory.Types _Type;
		private String _LetterNo;
		private DateTime _LetterDate;
		private DateTime _DropDate;

        public H_EmployeeDropHistoryView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeeDropHistoryView]"; }
		}

        protected override H_EmployeeDropHistoryView Map(SqlDataReader dataReader)
		{
            H_EmployeeDropHistoryView entity = new H_EmployeeDropHistoryView();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.Code = DBUtility.ToInt32(dataReader["Code"]);
            entity.Type = (H_EmployeeDropHistory.Types)DBUtility.ToInt32(dataReader["Type"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.DropDate = DBUtility.ToDateTime(dataReader["DropDate"]);

			return entity;
		}


        public H_EmployeeDropHistory.Types Type
		{
			get {return _Type;}
			set {_Type = value;}
		}

		public String LetterNo
		{
			get {return _LetterNo;}
			set {_LetterNo = value;}
		}

		public DateTime LetterDate
		{
			get {return _LetterDate;}
			set {_LetterDate = value;}
		}


		public DateTime DropDate
		{
			get {return _DropDate;}
			set {_DropDate = value;}
		}



        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public Int32 Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
    }
}
