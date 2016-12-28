using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeGrade : EntityBase<H_EmployeeGrade>
	{
		private Int32 _H_EmployeeId;
		private Int32 _H_GradeId;
		private DateTime _StartDate;
		private DateTime _EndDate;

		public H_EmployeeGrade()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeGrade]"; }
		}

		protected override H_EmployeeGrade Map(SqlDataReader dataReader)
		{
			H_EmployeeGrade entity = new H_EmployeeGrade();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.H_GradeId = DBUtility.ToInt32(dataReader["H_GradeId"]);
			entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
			entity.EndDate = DBUtility.ToDateTime(dataReader["EndDate"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeGrade> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeGrade> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeGrade> FindByH_GradeId(Int32 h_GradeId, String sortColumns)
		{
			return Find("[H_GradeId] = '" + h_GradeId + "'", sortColumns);
		}

		public static IList<H_EmployeeGrade> FindByH_GradeId(TransactionManager transactionManager, Int32 h_GradeId, String sortColumns)
		{
			return Find(transactionManager, "[H_GradeId] = '" + h_GradeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

		public Int32 H_GradeId
		{
			get {return this._H_GradeId;}
			set {this._H_GradeId = value;}
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
