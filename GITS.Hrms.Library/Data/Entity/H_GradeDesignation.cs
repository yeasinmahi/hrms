using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_GradeDesignation : EntityBase<H_GradeDesignation>
	{
		private Int32 _H_GradeId;
		private Int32 _H_DesignationId;

		public H_GradeDesignation()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_GradeDesignation]"; }
		}

		protected override H_GradeDesignation Map(SqlDataReader dataReader)
		{
			H_GradeDesignation entity = new H_GradeDesignation();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_GradeId = DBUtility.ToInt32(dataReader["H_GradeId"]);
			entity.H_DesignationId = DBUtility.ToInt32(dataReader["H_DesignationId"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static H_GradeDesignation GetByH_GradeIdAndH_DesignationId(Int32 h_GradeId, Int32 h_DesignationId)
		{
			return Get("[H_GradeId] = '" + h_GradeId + "' AND [H_DesignationId] = '" + h_DesignationId + "'");
		}

		public static H_GradeDesignation GetByH_GradeIdAndH_DesignationId(TransactionManager transactionManager, Int32 h_GradeId, Int32 h_DesignationId)
		{
			return Get(transactionManager, "[H_GradeId] = '" + h_GradeId + "' AND [H_DesignationId] = '" + h_DesignationId + "'");
		}

		public static IList<H_GradeDesignation> FindByH_DesignationId(Int32 h_DesignationId, String sortColumns)
		{
			return Find("[H_DesignationId] = '" + h_DesignationId + "'", sortColumns);
		}

		public static IList<H_GradeDesignation> FindByH_DesignationId(TransactionManager transactionManager, Int32 h_DesignationId, String sortColumns)
		{
			return Find(transactionManager, "[H_DesignationId] = '" + h_DesignationId + "'", sortColumns);
		}

		public static IList<H_GradeDesignation> FindByH_GradeId(Int32 h_GradeId, String sortColumns)
		{
			return Find("[H_GradeId] = '" + h_GradeId + "'", sortColumns);
		}

		public static IList<H_GradeDesignation> FindByH_GradeId(TransactionManager transactionManager, Int32 h_GradeId, String sortColumns)
		{
			return Find(transactionManager, "[H_GradeId] = '" + h_GradeId + "'", sortColumns);
		}

		public Int32 H_GradeId
		{
			get {return _H_GradeId;}
			set {_H_GradeId = value;}
		}

		public Int32 H_DesignationId
		{
			get {return _H_DesignationId;}
			set {_H_DesignationId = value;}
		}

	}
}
