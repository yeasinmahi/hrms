using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public class H_AcademicQualificationView : ViewBase<H_AcademicQualificationView>
    {
        private Int32 _H_EmployeeId;
        private H_AcademicQualification.Levels _Level;
		private String _ExamName;
		private String _SubjectName;
        private String _Institution;
        private String _BoardName;
		private String _Result;
        private Int32 _PassingYear;
        private Int32 _SortOrder;

        public H_AcademicQualificationView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_AcademicQualificationView]"; }
		}

        protected override H_AcademicQualificationView Map(SqlDataReader dataReader)
		{
            H_AcademicQualificationView view = new H_AcademicQualificationView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
            view.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            view.Level = (H_AcademicQualification.Levels)DBUtility.ToInt32(dataReader["Level"]);
            view.ExamName = DBUtility.ToString(dataReader["ExamName"]);
            view.SubjectName = DBUtility.ToString(dataReader["SubjectName"]);
            view.Institution = DBUtility.ToNullableString(dataReader["Institution"]);
            view.BoardName = DBUtility.ToString(dataReader["BoardName"]);
            view.PassingYear = DBUtility.ToInt32(dataReader["PassingYear"]);
            view.Result = DBUtility.ToString(dataReader["Result"]);
            view.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);
			return view;
		}
        public Int32 H_EmployeeId
        {
            get { return _H_EmployeeId; }
            set { _H_EmployeeId = value; }
        }
		public H_AcademicQualification.Levels Level
		{
			get {return _Level;}
			set {_Level = value;}
		}

        public String SubjectName
		{
			get {return _SubjectName;}
			set {_SubjectName = value;}
		}

		public String ExamName
		{
			get {return _ExamName;}
			set {_ExamName = value;}
		}
        public String Institution
        {
            get { return _Institution; }
            set { _Institution = value; }
        }

		public String BoardName
		{
			get {return _BoardName;}
			set {_BoardName = value;}
		}

        public Int32 PassingYear
        {
            get { return _PassingYear; }
            set { _PassingYear = value; }
        }

        public String Result
        {
            get { return _Result; }
            set { _Result = value; }
        }
        public Int32 SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
    }
}
