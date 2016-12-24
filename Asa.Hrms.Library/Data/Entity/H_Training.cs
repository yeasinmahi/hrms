using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_Training : EntityBase<H_Training>
	{
		private Int32 _H_EmployeeId;
		private String _Title;
		private String _Topics;
		private String _InstituteName;
		private String _Country;
		private String _Location;
		private String _TrainingYear;
		private String _Duration;
		private Int32 _SortOrder;

		public H_Training()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_Training]"; }
		}

		protected override H_Training Map(SqlDataReader dataReader)
		{
			H_Training entity = new H_Training();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.Title = DBUtility.ToString(dataReader["Title"]);
			entity.Topics = DBUtility.ToString(dataReader["Topics"]);
			entity.InstituteName = DBUtility.ToString(dataReader["InstituteName"]);
			entity.Country = DBUtility.ToString(dataReader["Country"]);
			entity.Location = DBUtility.ToString(dataReader["Location"]);
			entity.TrainingYear = DBUtility.ToString(dataReader["TrainingYear"]);
			entity.Duration = DBUtility.ToNullableString(dataReader["Duration"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_Training> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_Training> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

		public String Title
		{
			get {return this._Title;}
			set {this._Title = value;}
		}

		public String Topics
		{
			get {return this._Topics;}
			set {this._Topics = value;}
		}

		public String InstituteName
		{
			get {return this._InstituteName;}
			set {this._InstituteName = value;}
		}

		public String Country
		{
			get {return this._Country;}
			set {this._Country = value;}
		}

		public String Location
		{
			get {return this._Location;}
			set {this._Location = value;}
		}

		public String TrainingYear
		{
			get {return this._TrainingYear;}
			set {this._TrainingYear = value;}
		}

		public String Duration
		{
			get {return this._Duration;}
			set {this._Duration = value;}
		}

		public Int32 SortOrder
		{
			get {return this._SortOrder;}
			set {this._SortOrder = value;}
		}

	}
}
