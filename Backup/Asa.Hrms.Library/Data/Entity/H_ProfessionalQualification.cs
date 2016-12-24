using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_ProfessionalQualification : EntityBase<H_ProfessionalQualification>
	{
		private Int32 _H_EmployeeId;
		private String _Certification;
		private String _InstituteName;
		private String _Location;
		private DateTime _StartDate;
		private DateTime _EndDate;
		private Int32 _SortOrder;

		public H_ProfessionalQualification()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_ProfessionalQualification]"; }
		}

		protected override H_ProfessionalQualification Map(SqlDataReader dataReader)
		{
			H_ProfessionalQualification entity = new H_ProfessionalQualification();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.Certification = DBUtility.ToString(dataReader["Certification"]);
			entity.InstituteName = DBUtility.ToString(dataReader["InstituteName"]);
			entity.Location = DBUtility.ToString(dataReader["Location"]);
			entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
			entity.EndDate = DBUtility.ToDateTime(dataReader["EndDate"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_ProfessionalQualification> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_ProfessionalQualification> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

		public String Certification
		{
			get {return this._Certification;}
			set {this._Certification = value;}
		}

		public String InstituteName
		{
			get {return this._InstituteName;}
			set {this._InstituteName = value;}
		}

		public String Location
		{
			get {return this._Location;}
			set {this._Location = value;}
		}

		public DateTime StartDate
		{
			get {return this._StartDate;}
			set {this._StartDate = value;}
		}

		public DateTime EndDate
		{
			get {return this._EndDate;}
			set {this._EndDate = value;}
		}

		public Int32 SortOrder
		{
			get {return this._SortOrder;}
			set {this._SortOrder = value;}
		}

	}
}
