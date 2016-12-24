using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeWarning : EntityBase<H_EmployeeWarning>
	{
		private Int32 _H_EmployeeId;
		private String _LetterNo;
		private DateTime _LetterDate;
		private String _Duration;
		private Int32 _TotalWarningTime;
		private Int32 _BranchId;
		private String _Cause;
        private String _WarningType;
        private String _UserLogin;
        private bool _IsExempted;
        private String _ExemptedLetterNo;
        private Nullable<DateTime> _ExemptedLetterDate;
        private String _ExemptedRemarks;

		public H_EmployeeWarning()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeWarning]"; }
		}

		protected override H_EmployeeWarning Map(SqlDataReader dataReader)
		{
			H_EmployeeWarning entity = new H_EmployeeWarning();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.Duration = DBUtility.ToString(dataReader["Duration"]);
			entity.TotalWarningTime = DBUtility.ToInt32(dataReader["TotalWarningTime"]);
			entity.BranchId = DBUtility.ToInt32(dataReader["BranchId"]);
			entity.Cause = DBUtility.ToNullableString(dataReader["Cause"]);
            entity.WarningType = DBUtility.ToString(dataReader["WarningType"]);
            entity.UserLogin = DBUtility.ToString(dataReader["UserLogin"]);
            entity.IsExempted = DBUtility.ToBoolean(dataReader["IsExempted"]);
            entity.ExemptedLetterNo = DBUtility.ToNullableString(dataReader["ExemptedLetterNo"]);
            entity.ExemptedLetterDate = DBUtility.ToNullableDateTime(dataReader["ExemptedLetterDate"]);
            entity.ExemptedRemarks = DBUtility.ToNullableString(dataReader["ExemptedRemarks"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeWarning> FindByBranchId(Int32 branchId, String sortColumns)
		{
			return Find("[BranchId] = '" + branchId + "'", sortColumns);
		}

		public static IList<H_EmployeeWarning> FindByBranchId(TransactionManager transactionManager, Int32 branchId, String sortColumns)
		{
			return Find(transactionManager, "[BranchId] = '" + branchId + "'", sortColumns);
		}

		public static IList<H_EmployeeWarning> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeWarning> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
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

		public String Duration
		{
			get {return this._Duration;}
			set {this._Duration = value;}
		}

		public Int32 TotalWarningTime
		{
			get {return this._TotalWarningTime;}
			set {this._TotalWarningTime = value;}
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
        public String WarningType
        {
            get { return this._WarningType; }
            set { this._WarningType = value; }
        }

        public String UserLogin
        {
            get { return this._UserLogin; }
            set { this._UserLogin = value; }
        }
        public bool IsExempted
        {
            get { return this._IsExempted; }
            set { this._IsExempted = value; }
        }
        public String ExemptedLetterNo
        {
            get { return this._ExemptedLetterNo; }
            set { this._ExemptedLetterNo = value; }
        }
        public Nullable<DateTime> ExemptedLetterDate
        {
            get { return this._ExemptedLetterDate; }
            set { this._ExemptedLetterDate = value; }
        }
        public String ExemptedRemarks
        {
            get { return this._ExemptedRemarks; }
            set { this._ExemptedRemarks = value; }
        }
        [Property(PropertyAttribute.Attributes.NonTable)]
        public String Exempted
        {
            get
            {
                if (this.IsExempted)
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
        }
	}
}
