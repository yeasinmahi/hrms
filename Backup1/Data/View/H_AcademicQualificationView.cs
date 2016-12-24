using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.Data.View
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
            get { return this._H_EmployeeId; }
            set { this._H_EmployeeId = value; }
        }
		public H_AcademicQualification.Levels Level
		{
			get {return this._Level;}
			set {this._Level = value;}
		}

        public String SubjectName
		{
			get {return this._SubjectName;}
			set {this._SubjectName = value;}
		}

		public String ExamName
		{
			get {return this._ExamName;}
			set {this._ExamName = value;}
		}
        public String Institution
        {
            get { return this._Institution; }
            set { this._Institution = value; }
        }

		public String BoardName
		{
			get {return this._BoardName;}
			set {this._BoardName = value;}
		}

        public Int32 PassingYear
        {
            get { return this._PassingYear; }
            set { this._PassingYear = value; }
        }

        public String Result
        {
            get { return this._Result; }
            set { this._Result = value; }
        }
        public Int32 SortOrder
        {
            get { return this._SortOrder; }
            set { this._SortOrder = value; }
        }
    }
}
