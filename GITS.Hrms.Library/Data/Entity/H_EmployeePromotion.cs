using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeePromotion : EntityBase<H_EmployeePromotion>
	{
		private Int32 _H_EmployeeId;
        private Types _Type;
		private String _LetterNo;
		private DateTime _LetterDate;
		private Int32 _OldH_GradeId;
		private Int32 _NewH_GradeId;
		private Int32 _OldH_DesignationId;
		private Int32 _NewH_DesignationId;
		private DateTime _PromotionDate;
		private String _Remarks;

        public enum Types
        {
            Select_Type = 1,
            Promotion = 2,
            Demotion = 4,
            Designation_Change=8
        }

		public H_EmployeePromotion()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[H_EmployeePromotion]"; }
		}

		protected override H_EmployeePromotion Map(SqlDataReader dataReader)
		{
			H_EmployeePromotion entity = new H_EmployeePromotion();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Type = (Types)DBUtility.ToInt32(dataReader["Type"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.OldH_GradeId = DBUtility.ToInt32(dataReader["OldH_GradeId"]);
			entity.NewH_GradeId = DBUtility.ToInt32(dataReader["NewH_GradeId"]);
			entity.OldH_DesignationId = DBUtility.ToInt32(dataReader["OldH_DesignationId"]);
			entity.NewH_DesignationId = DBUtility.ToInt32(dataReader["NewH_DesignationId"]);
			entity.PromotionDate = DBUtility.ToDateTime(dataReader["PromotionDate"]);
			entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);

            entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_EmployeePromotion> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotion> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
        {
            return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotion> FindByNewH_DesignationId(Int32 newH_DesignationId, String sortColumns)
        {
            return Find("[NewH_DesignationId] = '" + newH_DesignationId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotion> FindByNewH_DesignationId(TransactionManager transactionManager, Int32 newH_DesignationId, String sortColumns)
        {
            return Find(transactionManager, "[NewH_DesignationId] = '" + newH_DesignationId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotion> FindByNewH_GradeId(Int32 newH_GradeId, String sortColumns)
        {
            return Find("[NewH_GradeId] = '" + newH_GradeId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotion> FindByNewH_GradeId(TransactionManager transactionManager, Int32 newH_GradeId, String sortColumns)
        {
            return Find(transactionManager, "[NewH_GradeId] = '" + newH_GradeId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotion> FindByOldH_DesignationId(Int32 oldH_DesignationId, String sortColumns)
        {
            return Find("[OldH_DesignationId] = '" + oldH_DesignationId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotion> FindByOldH_DesignationId(TransactionManager transactionManager, Int32 oldH_DesignationId, String sortColumns)
        {
            return Find(transactionManager, "[OldH_DesignationId] = '" + oldH_DesignationId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotion> FindByOldH_GradeId(Int32 oldH_GradeId, String sortColumns)
        {
            return Find("[OldH_GradeId] = '" + oldH_GradeId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotion> FindByOldH_GradeId(TransactionManager transactionManager, Int32 oldH_GradeId, String sortColumns)
        {
            return Find(transactionManager, "[OldH_GradeId] = '" + oldH_GradeId + "'", sortColumns);
        }

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

        public Types Type
		{
			get {return this._Type;}
			set {this._Type = value;}
		}

		public String LetterNo
		{
			get {return this._LetterNo;}
			set {this._LetterNo = value;}
		}

		public DateTime LetterDate
		{
			get {return this._LetterDate;}
			set {this._LetterDate = value;}
		}

		public Int32 OldH_GradeId
		{
			get {return this._OldH_GradeId;}
			set {this._OldH_GradeId = value;}
		}

		public Int32 NewH_GradeId
		{
			get {return this._NewH_GradeId;}
			set {this._NewH_GradeId = value;}
		}

		public Int32 OldH_DesignationId
		{
			get {return this._OldH_DesignationId;}
			set {this._OldH_DesignationId = value;}
		}

		public Int32 NewH_DesignationId
		{
			get {return this._NewH_DesignationId;}
			set {this._NewH_DesignationId = value;}
		}

		public DateTime PromotionDate
		{
			get {return this._PromotionDate;}
			set {this._PromotionDate = value;}
		}

		public String Remarks
		{
			get {return this._Remarks;}
			set {this._Remarks = value;}
		}

	}
}
