using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeSalary : EntityBase<H_EmployeeSalary>
	{
		private Int32 _H_EmployeeId;
		private Double _BasicSalary;
		private DateTime _LastIncrementDate;

		public H_EmployeeSalary()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeSalary]"; }
		}

		protected override H_EmployeeSalary Map(SqlDataReader dataReader)
		{
			H_EmployeeSalary entity = new H_EmployeeSalary();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.BasicSalary = DBUtility.ToDouble(dataReader["BasicSalary"]);
			entity.LastIncrementDate = DBUtility.ToDateTime(dataReader["LastIncrementDate"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeSalary> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeSalary> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

		public Double BasicSalary
		{
			get {return _BasicSalary;}
			set {_BasicSalary = value;}
		}

		public DateTime LastIncrementDate
		{
			get {return _LastIncrementDate;}
			set {_LastIncrementDate = value;}
		}

	}
}
