using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeeTransferHistory : EntityBase<H_EmployeeTransferHistory>
    {
        private Int32 _H_EmployeeId;
        private Types _Type;
		private String _LetterNo;
		private DateTime _LetterDate;
		private Int32 _SourceBranchId;
		private Int32 _DestinationBranchId;
		private DateTime _JoiningDate;
		private String _Remarks;
        private DateTime _EntryDateTime;
        private Statuses _Status;
        private String _UserLogin;
        private Nullable<Int32> _H_LetterFormatsId;
        private string _Duplication;

        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
        public enum Types
        {
            Normal = 1,
            Punishment = 2,
            Promotion = 4,
            Demotion = 8
        }

        public H_EmployeeTransferHistory()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[H_EmployeeTransferHistory]"; }
		}

		protected override H_EmployeeTransferHistory Map(SqlDataReader dataReader)
		{
			H_EmployeeTransferHistory entity = new H_EmployeeTransferHistory();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Type = (Types)DBUtility.ToInt32(dataReader["Type"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.SourceBranchId = DBUtility.ToInt32(dataReader["SourceBranchId"]);
			entity.DestinationBranchId = DBUtility.ToInt32(dataReader["DestinationBranchId"]);
			entity.JoiningDate = DBUtility.ToDateTime(dataReader["JoiningDate"]);
			entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);
            entity.EntryDateTime = DBUtility.ToDateTime(dataReader["EntryDateTime"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.UserLogin = DBUtility.ToString(dataReader["UserLogin"]);
            entity.H_LetterFormatsId = DBUtility.ToNullableInt32(dataReader["H_LetterFormatsId"]);
            entity.Duplication = DBUtility.ToNullableString(dataReader["Duplication"]);

            entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_EmployeeTransferHistory> FindByDestinationBranchId(Int32 destinationBranchId, String sortColumns)
        {
            return Find("[DestinationBranchId] = '" + destinationBranchId + "'", sortColumns);
        }

        public static IList<H_EmployeeTransferHistory> FindByDestinationBranchId(TransactionManager transactionManager, Int32 destinationBranchId, String sortColumns)
        {
            return Find(transactionManager, "[DestinationBranchId] = '" + destinationBranchId + "'", sortColumns);
        }

        public static IList<H_EmployeeTransferHistory> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeeTransferHistory> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
        {
            return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeeTransferHistory> FindBySourceBranchId(Int32 sourceBranchId, String sortColumns)
        {
            return Find("[SourceBranchId] = '" + sourceBranchId + "'", sortColumns);
        }

        public static IList<H_EmployeeTransferHistory> FindBySourceBranchId(TransactionManager transactionManager, Int32 sourceBranchId, String sortColumns)
        {
            return Find(transactionManager, "[SourceBranchId] = '" + sourceBranchId + "'", sortColumns);
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

		public Int32 SourceBranchId
		{
			get {return this._SourceBranchId;}
			set {this._SourceBranchId = value;}
		}

		public Int32 DestinationBranchId
		{
			get {return this._DestinationBranchId;}
			set {this._DestinationBranchId = value;}
		}

		public DateTime JoiningDate
		{
			get {return this._JoiningDate;}
			set {this._JoiningDate = value;}
		}

		public String Remarks
		{
			get {return this._Remarks;}
			set {this._Remarks = value;}
		}
        public DateTime EntryDateTime
        {
            get { return this._EntryDateTime; }
            set { this._EntryDateTime = value; }
        }
        public Statuses Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        public String UserLogin
        {
            get { return this._UserLogin; }
            set { this._UserLogin = value; }
        }
        public Nullable<Int32> H_LetterFormatsId
        {
            get { return this._H_LetterFormatsId; }
            set { this._H_LetterFormatsId = value; }
        }
        public String Duplication
        {
            get { return this._Duplication; }
            set { this._Duplication = value; }
        }
    }
}
