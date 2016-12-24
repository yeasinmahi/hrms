using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_Experience : EntityBase<H_Experience>
	{
		private Int32 _H_EmployeeId;
		private String _CompanyName;
		private String _CompanyBusiness;
		private String _CompanyLocation;
		private String _PositionHeld;
		private String _Department;
		private String _Responsibilities;
		private DateTime _StartDate;
		private Nullable<DateTime> _EndDate;
		private Int32 _SortOrder;

		public H_Experience()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_Experience]"; }
		}

		protected override H_Experience Map(SqlDataReader dataReader)
		{
			H_Experience entity = new H_Experience();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.CompanyName = DBUtility.ToString(dataReader["CompanyName"]);
			entity.CompanyBusiness = DBUtility.ToString(dataReader["CompanyBusiness"]);
			entity.CompanyLocation = DBUtility.ToString(dataReader["CompanyLocation"]);
			entity.PositionHeld = DBUtility.ToString(dataReader["PositionHeld"]);
			entity.Department = DBUtility.ToString(dataReader["Department"]);
			entity.Responsibilities = DBUtility.ToString(dataReader["Responsibilities"]);
			entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
			entity.EndDate = DBUtility.ToNullableDateTime(dataReader["EndDate"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_Experience> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_Experience> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

		public String CompanyName
		{
			get {return this._CompanyName;}
			set {this._CompanyName = value;}
		}

		public String CompanyBusiness
		{
			get {return this._CompanyBusiness;}
			set {this._CompanyBusiness = value;}
		}

		public String CompanyLocation
		{
			get {return this._CompanyLocation;}
			set {this._CompanyLocation = value;}
		}

		public String PositionHeld
		{
			get {return this._PositionHeld;}
			set {this._PositionHeld = value;}
		}

		public String Department
		{
			get {return this._Department;}
			set {this._Department = value;}
		}

		public String Responsibilities
		{
			get {return this._Responsibilities;}
			set {this._Responsibilities = value;}
		}

		public DateTime StartDate
		{
			get {return this._StartDate;}
			set {this._StartDate = value;}
		}

		public Nullable<DateTime> EndDate
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
