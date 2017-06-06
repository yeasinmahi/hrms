using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
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
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

		public Int32 H_DesignationId
		{
			get {return _H_DesignationId;}
			set {_H_DesignationId = value;}
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
