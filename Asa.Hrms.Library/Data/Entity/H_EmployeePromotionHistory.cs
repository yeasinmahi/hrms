using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeePromotionHistory : EntityBase<H_EmployeePromotionHistory>
    {
        private Int32 _H_EmployeeId;
        private H_EmployeePromotion.Types _Type;
		private String _LetterNo;
		private DateTime _LetterDate;
		private Int32 _OldH_GradeId;
		private Int32 _NewH_GradeId;
		private Int32 _OldH_DesignationId;
		private Int32 _NewH_DesignationId;
		private DateTime _PromotionDate;
		private String _Remarks;
        private Int32 _Status;
        private String _UserLogin;

        public H_EmployeePromotionHistory()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeePromotionHistory]"; }
		}

        protected override H_EmployeePromotionHistory Map(SqlDataReader dataReader)
		{
            H_EmployeePromotionHistory entity = new H_EmployeePromotionHistory();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Type = (H_EmployeePromotion.Types)DBUtility.ToInt32(dataReader["Type"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.OldH_GradeId = DBUtility.ToInt32(dataReader["OldH_GradeId"]);
			entity.NewH_GradeId = DBUtility.ToInt32(dataReader["NewH_GradeId"]);
			entity.OldH_DesignationId = DBUtility.ToInt32(dataReader["OldH_DesignationId"]);
			entity.NewH_DesignationId = DBUtility.ToInt32(dataReader["NewH_DesignationId"]);
			entity.PromotionDate = DBUtility.ToDateTime(dataReader["PromotionDate"]);
			entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);
            entity.Status = DBUtility.ToInt32(dataReader["Status"]);
            entity.UserLogin = DBUtility.ToNullableString(dataReader["UserLogin"]);
            entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_EmployeePromotionHistory> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotionHistory> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
        {
            return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotionHistory> FindByNewH_DesignationId(Int32 newH_DesignationId, String sortColumns)
        {
            return Find("[NewH_DesignationId] = '" + newH_DesignationId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotionHistory> FindByNewH_DesignationId(TransactionManager transactionManager, Int32 newH_DesignationId, String sortColumns)
        {
            return Find(transactionManager, "[NewH_DesignationId] = '" + newH_DesignationId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotionHistory> FindByNewH_GradeId(Int32 newH_GradeId, String sortColumns)
        {
            return Find("[NewH_GradeId] = '" + newH_GradeId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotionHistory> FindByNewH_GradeId(TransactionManager transactionManager, Int32 newH_GradeId, String sortColumns)
        {
            return Find(transactionManager, "[NewH_GradeId] = '" + newH_GradeId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotionHistory> FindByOldH_DesignationId(Int32 oldH_DesignationId, String sortColumns)
        {
            return Find("[OldH_DesignationId] = '" + oldH_DesignationId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotionHistory> FindByOldH_DesignationId(TransactionManager transactionManager, Int32 oldH_DesignationId, String sortColumns)
        {
            return Find(transactionManager, "[OldH_DesignationId] = '" + oldH_DesignationId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotionHistory> FindByOldH_GradeId(Int32 oldH_GradeId, String sortColumns)
        {
            return Find("[OldH_GradeId] = '" + oldH_GradeId + "'", sortColumns);
        }

        public static IList<H_EmployeePromotionHistory> FindByOldH_GradeId(TransactionManager transactionManager, Int32 oldH_GradeId, String sortColumns)
        {
            return Find(transactionManager, "[OldH_GradeId] = '" + oldH_GradeId + "'", sortColumns);
        }

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

        public H_EmployeePromotion.Types Type
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

        public Int32 Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        public String UserLogin
        {
            get { return this._UserLogin; }
            set { this._UserLogin = value; }
        }
    }
}
