using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeBranch : EntityBase<H_EmployeeBranch>
	{
		private Int32 _H_EmployeeId;
		private Int32 _BranchId;
		private DateTime _StartDate;
		private DateTime _EndDate;

		public H_EmployeeBranch()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeBranch]"; }
		}

		protected override H_EmployeeBranch Map(SqlDataReader dataReader)
		{
			H_EmployeeBranch entity = new H_EmployeeBranch();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.BranchId = DBUtility.ToInt32(dataReader["BranchId"]);
			entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
			entity.EndDate = DBUtility.ToDateTime(dataReader["EndDate"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeBranch> FindByBranchId(Int32 branchId, String sortColumns)
		{
			return Find("[BranchId] = '" + branchId + "'", sortColumns);
		}

		public static IList<H_EmployeeBranch> FindByBranchId(TransactionManager transactionManager, Int32 branchId, String sortColumns)
		{
			return Find(transactionManager, "[BranchId] = '" + branchId + "'", sortColumns);
		}

		public static IList<H_EmployeeBranch> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeBranch> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

		public Int32 BranchId
		{
			get {return this._BranchId;}
			set {this._BranchId = value;}
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
