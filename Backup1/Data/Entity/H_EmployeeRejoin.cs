using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeRejoin : EntityBase<H_EmployeeRejoin>
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

		public H_EmployeeRejoin()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeRejoin]"; }
		}

		protected override H_EmployeeRejoin Map(SqlDataReader dataReader)
		{
			H_EmployeeRejoin entity = new H_EmployeeRejoin();

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

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeRejoin> FindByDestinationBranchId(Int32 destinationBranchId, String sortColumns)
		{
			return Find("[DestinationBranchId] = '" + destinationBranchId + "'", sortColumns);
		}

		public static IList<H_EmployeeRejoin> FindByDestinationBranchId(TransactionManager transactionManager, Int32 destinationBranchId, String sortColumns)
		{
			return Find(transactionManager, "[DestinationBranchId] = '" + destinationBranchId + "'", sortColumns);
		}

		public static IList<H_EmployeeRejoin> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeRejoin> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeRejoin> FindBySourceBranchId(Int32 sourceBranchId, String sortColumns)
		{
			return Find("[SourceBranchId] = '" + sourceBranchId + "'", sortColumns);
		}

		public static IList<H_EmployeeRejoin> FindBySourceBranchId(TransactionManager transactionManager, Int32 sourceBranchId, String sortColumns)
		{
			return Find(transactionManager, "[SourceBranchId] = '" + sourceBranchId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

        public H_EmployeeLeave.Types LeaveType
		{
			get {return this._LeaveType;}
			set {this._LeaveType = value;}
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

		public DateTime FromDate
		{
			get {return this._FromDate;}
			set {this._FromDate = value;}
		}

		public DateTime RejoinDate
		{
			get {return this._RejoinDate;}
			set {this._RejoinDate = value;}
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

        public H_Employee.EmploymentTypes RejoinType
        {
            get { return this._RejoinType; }
            set { this._RejoinType = value; }
        }
	}
}
