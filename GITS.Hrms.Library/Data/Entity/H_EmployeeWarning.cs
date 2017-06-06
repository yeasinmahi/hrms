using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
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

		public String Duration
		{
			get {return _Duration;}
			set {_Duration = value;}
		}

		public Int32 TotalWarningTime
		{
			get {return _TotalWarningTime;}
			set {_TotalWarningTime = value;}
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
        public String WarningType
        {
            get { return _WarningType; }
            set { _WarningType = value; }
        }

        public String UserLogin
        {
            get { return _UserLogin; }
            set { _UserLogin = value; }
        }
        public bool IsExempted
        {
            get { return _IsExempted; }
            set { _IsExempted = value; }
        }
        public String ExemptedLetterNo
        {
            get { return _ExemptedLetterNo; }
            set { _ExemptedLetterNo = value; }
        }
        public Nullable<DateTime> ExemptedLetterDate
        {
            get { return _ExemptedLetterDate; }
            set { _ExemptedLetterDate = value; }
        }
        public String ExemptedRemarks
        {
            get { return _ExemptedRemarks; }
            set { _ExemptedRemarks = value; }
        }
        [Property(PropertyAttribute.Attributes.NonTable)]
        public String Exempted
        {
            get
            {
                if (IsExempted)
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
