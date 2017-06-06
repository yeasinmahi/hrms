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
            //entity.H_LetterFormatsId = DBUtility.ToNullableInt32(dataReader["H_LetterFormatsId"]);
            //entity.Duplication = DBUtility.ToNullableString(dataReader["Duplication"]);

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
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

        public Types Type
		{
			get {return _Type;}
			set {_Type = value;}
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

		public Int32 SourceBranchId
		{
			get {return _SourceBranchId;}
			set {_SourceBranchId = value;}
		}

		public Int32 DestinationBranchId
		{
			get {return _DestinationBranchId;}
			set {_DestinationBranchId = value;}
		}

		public DateTime JoiningDate
		{
			get {return _JoiningDate;}
			set {_JoiningDate = value;}
		}

		public String Remarks
		{
			get {return _Remarks;}
			set {_Remarks = value;}
		}
        public DateTime EntryDateTime
        {
            get { return _EntryDateTime; }
            set { _EntryDateTime = value; }
        }
        public Statuses Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public String UserLogin
        {
            get { return _UserLogin; }
            set { _UserLogin = value; }
        }
        public Nullable<Int32> H_LetterFormatsId
        {
            get { return _H_LetterFormatsId; }
            set { _H_LetterFormatsId = value; }
        }
        public String Duplication
        {
            get { return _Duplication; }
            set { _Duplication = value; }
        }
    }
}
