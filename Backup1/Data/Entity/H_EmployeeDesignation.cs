using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeDesignation : EntityBase<H_EmployeeDesignation>
	{
		private Int32 _H_EmployeeId;
		private Int32 _H_DesignationId;
		private DateTime _StartDate;
		private DateTime _EndDate;

		public H_EmployeeDesignation()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeDesignation]"; }
		}

		protected override H_EmployeeDesignation Map(SqlDataReader dataReader)
		{
			H_EmployeeDesignation entity = new H_EmployeeDesignation();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.H_DesignationId = DBUtility.ToInt32(dataReader["H_DesignationId"]);
			entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
			entity.EndDate = DBUtility.ToDateTime(dataReader["EndDate"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeDesignation> FindByH_DesignationId(Int32 h_DesignationId, String sortColumns)
		{
			return Find("[H_DesignationId] = '" + h_DesignationId + "'", sortColumns);
		}

		public static IList<H_EmployeeDesignation> FindByH_DesignationId(TransactionManager transactionManager, Int32 h_DesignationId, String sortColumns)
		{
			return Find(transactionManager, "[H_DesignationId] = '" + h_DesignationId + "'", sortColumns);
		}

		public static IList<H_EmployeeDesignation> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeDesignation> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

		public Int32 H_DesignationId
		{
			get {return this._H_DesignationId;}
			set {this._H_DesignationId = value;}
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

	}
}
