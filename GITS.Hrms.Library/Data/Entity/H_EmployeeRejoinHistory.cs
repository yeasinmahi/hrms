using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeeRejoinHistory : EntityBase<H_EmployeeRejoinHistory>
    {
        private Int32 _H_EmployeeId;
		private H_EmployeeLeave.Types _LeaveType;
		private String _LetterNo;
		private DateTime _LetterDate;
		private DateTime _FromDate;
		private DateTime _RejoinDate;
		private Int32 _SourceBranchId;
		private Int32 _DestinationBranchId;
        private H_Employee.EmploymentTypes _RejoinType;
        private Boolean _Status;

        public H_EmployeeRejoinHistory()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_EmployeeRejoinHistory]"; }
		}

        protected override H_EmployeeRejoinHistory Map(SqlDataReader dataReader)
		{
            H_EmployeeRejoinHistory entity = new H_EmployeeRejoinHistory();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.LeaveType = (H_EmployeeLeave.Types)DBUtility.ToInt32(dataReader["LeaveType"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.FromDate = DBUtility.ToDateTime(dataReader["FromDate"]);
			entity.RejoinDate = DBUtility.ToDateTime(dataReader["RejoinDate"]);
			entity.SourceBranchId = DBUtility.ToInt32(dataReader["SourceBranchId"]);
			entity.DestinationBranchId = DBUtility.ToInt32(dataReader["DestinationBranchId"]);
            entity.RejoinType = (H_Employee.EmploymentTypes)DBUtility.ToInt32(dataReader["RejoinType"]);
            entity.Status = DBUtility.ToBoolean(dataReader["Status"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_EmployeeRejoinHistory> FindByDestinationBranchId(Int32 destinationBranchId, String sortColumns)
		{
			return Find("[DestinationBranchId] = '" + destinationBranchId + "'", sortColumns);
		}

        public static IList<H_EmployeeRejoinHistory> FindByDestinationBranchId(TransactionManager transactionManager, Int32 destinationBranchId, String sortColumns)
		{
			return Find(transactionManager, "[DestinationBranchId] = '" + destinationBranchId + "'", sortColumns);
		}

        public static IList<H_EmployeeRejoinHistory> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

        public static IList<H_EmployeeRejoinHistory> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

        public static IList<H_EmployeeRejoinHistory> FindBySourceBranchId(Int32 sourceBranchId, String sortColumns)
		{
			return Find("[SourceBranchId] = '" + sourceBranchId + "'", sortColumns);
		}

        public static IList<H_EmployeeRejoinHistory> FindBySourceBranchId(TransactionManager transactionManager, Int32 sourceBranchId, String sortColumns)
		{
			return Find(transactionManager, "[SourceBranchId] = '" + sourceBranchId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

        public H_EmployeeLeave.Types LeaveType
		{
			get {return _LeaveType;}
			set {_LeaveType = value;}
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

		public DateTime FromDate
		{
			get {return _FromDate;}
			set {_FromDate = value;}
		}

		public DateTime RejoinDate
		{
			get {return _RejoinDate;}
			set {_RejoinDate = value;}
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

        public H_Employee.EmploymentTypes RejoinType
        {
            get { return _RejoinType; }
            set { _RejoinType = value; }
        }

        public Boolean Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        [Property(PropertyAttribute.Attributes.NonTable)]
        public String Name
        {
            get { return H_Employee.GetById(H_EmployeeId).Name; }
        }
        [Property(PropertyAttribute.Attributes.NonTable)]
        public Int32 Code
        {
            get { return H_Employee.GetById(H_EmployeeId).Code; }
        }
    }
}
