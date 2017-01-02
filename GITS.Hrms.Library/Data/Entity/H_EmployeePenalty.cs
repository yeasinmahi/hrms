using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeePenalty : EntityBase<H_EmployeePenalty>
	{
		private Int32 _H_EmployeeId;
		private String _LetterNo;
		private DateTime _LetterDate;
		private String _FineType;
		private Int32 _FineTime;
		private Double _FineAmount;
        private Int32 _Duration;
        private Int32 _BranchId;
        private String _Remarks;
        private String _UserLogin;
        private String _RemissionLetterNo;
        private Nullable<DateTime> _RemissionLetterDate;
        private Nullable<Double> _RemissionAmount;
        private String _RemissionUser;

		public H_EmployeePenalty()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeePenalty]"; }
		}

		protected override H_EmployeePenalty Map(SqlDataReader dataReader)
		{
			H_EmployeePenalty entity = new H_EmployeePenalty();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.FineType = DBUtility.ToString(dataReader["FineType"]);
			entity.FineTime = DBUtility.ToInt32(dataReader["FineTime"]);
			entity.FineAmount = DBUtility.ToDouble(dataReader["FineAmount"]);
            entity.Duration = DBUtility.ToInt32(dataReader["Duration"]);
            entity.BranchId = DBUtility.ToInt32(dataReader["BranchId"]);
            entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);
            entity.UserLogin = DBUtility.ToString(dataReader["UserLogin"]);
            entity.RemissionLetterNo = DBUtility.ToNullableString(dataReader["RemissionLetterNo"]);
            entity.RemissionLetterDate = DBUtility.ToNullableDateTime(dataReader["RemissionLetterDate"]);
            entity.RemissionAmount = DBUtility.ToNullableDouble(dataReader["RemissionAmount"]);
            entity.RemissionUser = DBUtility.ToNullableString(dataReader["RemissionUser"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeePenalty> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeePenalty> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
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

		public String FineType
		{
			get {return this._FineType;}
			set {this._FineType = value;}
		}

		public Int32 FineTime
		{
			get {return this._FineTime;}
			set {this._FineTime = value;}
		}

		public Double FineAmount
		{
			get {return this._FineAmount;}
			set {this._FineAmount = value;}
		}
        public Int32 Duration
        {
            get { return this._Duration; }
            set { this._Duration = value; }
        }
        public Int32 BranchId
        {
            get { return this._BranchId; }
            set { this._BranchId = value; }
        }
        public String Remarks
        {
            get { return this._Remarks; }
            set { this._Remarks = value; }
        }
        public String UserLogin
        {
            get { return this._UserLogin; }
            set { this._UserLogin = value; }
        }
        public String RemissionLetterNo
        {
            get { return this._RemissionLetterNo; }
            set { this._RemissionLetterNo = value; }
        }
        public Nullable<DateTime> RemissionLetterDate
        {
            get { return this._RemissionLetterDate; }
            set { this._RemissionLetterDate = value; }
        }
        public Nullable<Double> RemissionAmount
        {
            get { return this._RemissionAmount; }
            set { this._RemissionAmount = value; }
        }
        public String RemissionUser
        {
            get { return this._RemissionUser; }
            set { this._RemissionUser = value; }
        }
	}
}
