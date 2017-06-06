using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
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
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

		public String LetterNo
		{
			get {return _LetterNo;}
			set {_LetterNo = value;}
		}

		public DateTime LetterDate
		{
			get {return _LetterDate;}
			set {_LetterDate = value;}
		}

		public Int32 IncrementStop
		{
			get {return _IncrementStop;}
			set {_IncrementStop = value;}
		}

		public DateTime FromDate
		{
			get {return _FromDate;}
			set {_FromDate = value;}
		}

		public Nullable<DateTime> ToDate
		{
			get {return _ToDate;}
			set {_ToDate = value;}
		}

		public Int32 BranchId
		{
			get {return _BranchId;}
			set {_BranchId = value;}
		}

		public String Cause
		{
			get {return _Cause;}
			set {_Cause = value;}
		}
        public String UserLogin
        {
            get { return _UserLogin; }
            set { _UserLogin = value; }
        }
        public String ExemptionLetterNo
        {
            get { return _ExemptionLetterNo; }
            set { _ExemptionLetterNo = value; }
        }
        public Nullable<DateTime> ExemptionLetterDate
        {
            get { return _ExemptionLetterDate; }
            set { _ExemptionLetterDate = value; }
        }
        public Nullable<DateTime> ExemptionDate
        {
            get { return _ExemptionDate; }
            set { _ExemptionDate = value; }
        }
        public Nullable<Int32> IncrementExempted
        {
            get { return _IncrementExempted; }
            set { _IncrementExempted = value; }
        }
        public String ExemptionRemarks
        {
            get { return _ExemptionRemarks; }
            set { _ExemptionRemarks = value; }
        }
	}
}
