using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeIncrementHeldup : EntityBase<H_EmployeeIncrementHeldup>
	{
		private Int32 _H_EmployeeId;
		private String _LetterNo;
		private DateTime _LetterDate;
		private Int32 _IncrementStop;
		private DateTime _FromDate;
		private Nullable<DateTime> _ToDate;
		private Int32 _BranchId;
		private String _Cause;
        private String _UserLogin;
        private String _ExemptionLetterNo;
        private Nullable<DateTime> _ExemptionLetterDate;
        private Nullable<DateTime> _ExemptionDate;
        private Nullable<Int32> _IncrementExempted;
        private String _ExemptionRemarks;

		public H_EmployeeIncrementHeldup()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeIncrementHeldup]"; }
		}

		protected override H_EmployeeIncrementHeldup Map(SqlDataReader dataReader)
		{
			H_EmployeeIncrementHeldup entity = new H_EmployeeIncrementHeldup();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.IncrementStop = DBUtility.ToInt32(dataReader["IncrementStop"]);
			entity.FromDate = DBUtility.ToDateTime(dataReader["FromDate"]);
			entity.ToDate = DBUtility.ToNullableDateTime(dataReader["ToDate"]);
			entity.BranchId = DBUtility.ToInt32(dataReader["BranchId"]);
			entity.Cause = DBUtility.ToNullableString(dataReader["Cause"]);
            entity.UserLogin = DBUtility.ToString(dataReader["UserLogin"]);
            entity.ExemptionLetterNo = DBUtility.ToNullableString(dataReader["ExemptionLetterNo"]);
            entity.ExemptionLetterDate = DBUtility.ToNullableDateTime(dataReader["ExemptionLetterDate"]);
            entity.ExemptionDate = DBUtility.ToNullableDateTime(dataReader["ExemptionDate"]);
            entity.IncrementExempted = DBUtility.ToNullableInt32(dataReader["IncrementExempted"]);
            entity.ExemptionRemarks = DBUtility.ToNullableString(dataReader["ExemptionRemarks"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeIncrementHeldup> FindByBranchId(Int32 branchId, String sortColumns)
		{
			return Find("[BranchId] = '" + branchId + "'", sortColumns);
		}

		public static IList<H_EmployeeIncrementHeldup> FindByBranchId(TransactionManager transactionManager, Int32 branchId, String sortColumns)
		{
			return Find(transactionManager, "[BranchId] = '" + branchId + "'", sortColumns);
		}

		public static IList<H_EmployeeIncrementHeldup> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeIncrementHeldup> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
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

		public Int32 IncrementStop
		{
			get {return this._IncrementStop;}
			set {this._IncrementStop = value;}
		}

		public DateTime FromDate
		{
			get {return this._FromDate;}
			set {this._FromDate = value;}
		}

		public Nullable<DateTime> ToDate
		{
			get {return this._ToDate;}
			set {this._ToDate = value;}
		}

		public Int32 BranchId
		{
			get {return this._BranchId;}
			set {this._BranchId = value;}
		}

		public String Cause
		{
			get {return this._Cause;}
			set {this._Cause = value;}
		}
        public String UserLogin
        {
            get { return this._UserLogin; }
            set { this._UserLogin = value; }
        }
        public String ExemptionLetterNo
        {
            get { return this._ExemptionLetterNo; }
            set { this._ExemptionLetterNo = value; }
        }
        public Nullable<DateTime> ExemptionLetterDate
        {
            get { return this._ExemptionLetterDate; }
            set { this._ExemptionLetterDate = value; }
        }
        public Nullable<DateTime> ExemptionDate
        {
            get { return this._ExemptionDate; }
            set { this._ExemptionDate = value; }
        }
        public Nullable<Int32> IncrementExempted
        {
            get { return this._IncrementExempted; }
            set { this._IncrementExempted = value; }
        }
        public String ExemptionRemarks
        {
            get { return this._ExemptionRemarks; }
            set { this._ExemptionRemarks = value; }
        }
	}
}
