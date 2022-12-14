using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeDepartment : EntityBase<H_EmployeeDepartment>
	{
		private Int32 _H_EmployeeId;
		private Int32 _H_DepartmentId;
		private DateTime _StartDate;
		private DateTime _EndDate;

		public H_EmployeeDepartment()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeDepartment]"; }
		}

		protected override H_EmployeeDepartment Map(SqlDataReader dataReader)
		{
			H_EmployeeDepartment entity = new H_EmployeeDepartment();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.H_DepartmentId = DBUtility.ToInt32(dataReader["H_DepartmentId"]);
			entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
			entity.EndDate = DBUtility.ToDateTime(dataReader["EndDate"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeDepartment> FindByH_DepartmentId(Int32 h_DepartmentId, String sortColumns)
		{
			return Find("[H_DepartmentId] = '" + h_DepartmentId + "'", sortColumns);
		}

		public static IList<H_EmployeeDepartment> FindByH_DepartmentId(TransactionManager transactionManager, Int32 h_DepartmentId, String sortColumns)
		{
			return Find(transactionManager, "[H_DepartmentId] = '" + h_DepartmentId + "'", sortColumns);
		}

		public static IList<H_EmployeeDepartment> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeDepartment> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

		public Int32 H_DepartmentId
		{
			get {return _H_DepartmentId;}
			set {_H_DepartmentId = value;}
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

	}
}
