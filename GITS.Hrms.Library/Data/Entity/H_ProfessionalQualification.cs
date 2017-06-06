using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
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
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

		public String Certification
		{
			get {return _Certification;}
			set {_Certification = value;}
		}

		public String InstituteName
		{
			get {return _InstituteName;}
			set {_InstituteName = value;}
		}

		public String Location
		{
			get {return _Location;}
			set {_Location = value;}
		}

		public DateTime StartDate
		{
			get {return _StartDate;}
			set {_StartDate = value;}
		}

		public DateTime EndDate
		{
			get {return _EndDate;}
			set {_EndDate = value;}
		}

		public Int32 SortOrder
		{
			get {return _SortOrder;}
			set {_SortOrder = value;}
		}

	}
}
