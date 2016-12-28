using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_AcademicQualification : EntityBase<H_AcademicQualification>
	{
		private Int32 _H_EmployeeId;
		private Int32 _Level;
        private Int32 _ExamNameId;
        private Int32 _GroupSubjectId;
        private String _Institution;
        private Int32 _BoardUniversityId;
		private String _Result;
		private Int32 _PassingYear;
		private Nullable<Double> _GPA;
		private Int32 _SortOrder;
        public enum Levels
        {
            Below_Secondary = 1,
            Secondary = 2,
            Higher_Secondary = 3,
            Diploma_Secondary_Level=4,
            Bachelor = 5,
            Diploma_Higher_Secondary_Level=6,
            Masters = 7,
            Post_Graduate_Diploma=8,
            M_Phil=9,
            Doctoral=10
        }
		public H_AcademicQualification()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_AcademicQualification]"; }
		}

		protected override H_AcademicQualification Map(SqlDataReader dataReader)
		{
			H_AcademicQualification entity = new H_AcademicQualification();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.Level = DBUtility.ToInt32(dataReader["Level"]);
            entity.ExamNameId = DBUtility.ToInt32(dataReader["ExamNameId"]);
            entity.GroupSubjectId = DBUtility.ToInt32(dataReader["GroupSubjectId"]);
            entity.BoardUniversityId = DBUtility.ToInt32(dataReader["BoardUniversityId"]);
            entity.Institution = DBUtility.ToNullableString(dataReader["Institution"]);
			entity.Result = DBUtility.ToString(dataReader["Result"]);
			entity.PassingYear = DBUtility.ToInt32(dataReader["PassingYear"]);
			entity.GPA = DBUtility.ToNullableDouble(dataReader["GPA"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_AcademicQualification> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_AcademicQualification> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

		public Int32 Level
		{
			get {return this._Level;}
			set {this._Level = value;}
		}

        public Int32 ExamNameId
		{
			get {return this._ExamNameId;}
			set {this._ExamNameId = value;}
		}

        public Int32 GroupSubjectId
		{
			get {return this._GroupSubjectId;}
			set {this._GroupSubjectId = value;}
		}

        public Int32 BoardUniversityId
		{
			get {return this._BoardUniversityId;}
			set {this._BoardUniversityId = value;}
		}

        public String Institution
        {
            get { return this._Institution; }
            set { this._Institution = value; }
        }

		public String Result
		{
			get {return this._Result;}
			set {this._Result = value;}
		}
        public Nullable<Double> GPA
        {
            get { return this._GPA; }
            set { this._GPA = value; }
        }

		public Int32 PassingYear
		{
			get {return this._PassingYear;}
			set {this._PassingYear = value;}
		}

		

		public Int32 SortOrder
		{
			get {return this._SortOrder;}
			set {this._SortOrder = value;}
		}

	}
}
