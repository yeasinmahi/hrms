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
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

		public Int32 Level
		{
			get {return _Level;}
			set {_Level = value;}
		}

        public Int32 ExamNameId
		{
			get {return _ExamNameId;}
			set {_ExamNameId = value;}
		}

        public Int32 GroupSubjectId
		{
			get {return _GroupSubjectId;}
			set {_GroupSubjectId = value;}
		}

        public Int32 BoardUniversityId
		{
			get {return _BoardUniversityId;}
			set {_BoardUniversityId = value;}
		}

        public String Institution
        {
            get { return _Institution; }
            set { _Institution = value; }
        }

		public String Result
		{
			get {return _Result;}
			set {_Result = value;}
		}
        public Nullable<Double> GPA
        {
            get { return _GPA; }
            set { _GPA = value; }
        }

		public Int32 PassingYear
		{
			get {return _PassingYear;}
			set {_PassingYear = value;}
		}

		

		public Int32 SortOrder
		{
			get {return _SortOrder;}
			set {_SortOrder = value;}
		}

	}
}
