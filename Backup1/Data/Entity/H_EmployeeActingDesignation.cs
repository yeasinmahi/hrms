using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeActingDesignation : EntityBase<H_EmployeeActingDesignation>
	{
		private Int32 _H_EmployeeId;
		private Int32 _InchargedGradeId;
		private Int32 _InchargedDesignationId;
		private DateTime _FromDate;
		private DateTime _ToDate;

		public H_EmployeeActingDesignation()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeActingDesignation]"; }
		}

		protected override H_EmployeeActingDesignation Map(SqlDataReader dataReader)
		{
			H_EmployeeActingDesignation entity = new H_EmployeeActingDesignation();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.InchargedGradeId = DBUtility.ToInt32(dataReader["InchargedGradeId"]);
			entity.InchargedDesignationId = DBUtility.ToInt32(dataReader["InchargedDesignationId"]);
			entity.FromDate = DBUtility.ToDateTime(dataReader["FromDate"]);
			entity.ToDate = DBUtility.ToDateTime(dataReader["ToDate"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeActingDesignation> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeActingDesignation> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeActingDesignation> FindByInchargedDesignationId(Int32 inchargedDesignationId, String sortColumns)
		{
			return Find("[InchargedDesignationId] = '" + inchargedDesignationId + "'", sortColumns);
		}

		public static IList<H_EmployeeActingDesignation> FindByInchargedDesignationId(TransactionManager transactionManager, Int32 inchargedDesignationId, String sortColumns)
		{
			return Find(transactionManager, "[InchargedDesignationId] = '" + inchargedDesignationId + "'", sortColumns);
		}

		public static IList<H_EmployeeActingDesignation> FindByInchargedGradeId(Int32 inchargedGradeId, String sortColumns)
		{
			return Find("[InchargedGradeId] = '" + inchargedGradeId + "'", sortColumns);
		}

		public static IList<H_EmployeeActingDesignation> FindByInchargedGradeId(TransactionManager transactionManager, Int32 inchargedGradeId, String sortColumns)
		{
			return Find(transactionManager, "[InchargedGradeId] = '" + inchargedGradeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

		public Int32 InchargedGradeId
		{
			get {return this._InchargedGradeId;}
			set {this._InchargedGradeId = value;}
		}

		public Int32 InchargedDesignationId
		{
			get {return this._InchargedDesignationId;}
			set {this._InchargedDesignationId = value;}
		}

		public DateTime FromDate
		{
			get {return this._FromDate;}
			set {this._FromDate = value;}
		}

		public DateTime ToDate
		{
			get {return this._ToDate;}
			set {this._ToDate = value;}
		}

	}
}
