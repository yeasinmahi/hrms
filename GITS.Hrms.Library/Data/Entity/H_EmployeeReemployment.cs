using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeReemployment : EntityBase<H_EmployeeReemployment>
	{
		private Int32 _H_EmployeeId;
		private H_EmployeeDrop.Types _DropoutType;
		private String _LetterNo;
		private DateTime _LetterDate;
		private DateTime _FromDate;
		private DateTime _ReemploymentDate;
		private Int32 _SourceBranchId;
		private Int32 _DestinationBranchId;
        private H_Employee.EmploymentTypes _ReemploymentType;
		private String _Cause;

		public H_EmployeeReemployment()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeReemployment]"; }
		}

		protected override H_EmployeeReemployment Map(SqlDataReader dataReader)
		{
			H_EmployeeReemployment entity = new H_EmployeeReemployment();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.DropoutType = (H_EmployeeDrop.Types)DBUtility.ToInt32(dataReader["DropoutType"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.FromDate = DBUtility.ToDateTime(dataReader["FromDate"]);
			entity.ReemploymentDate = DBUtility.ToDateTime(dataReader["ReemploymentDate"]);
			entity.SourceBranchId = DBUtility.ToInt32(dataReader["SourceBranchId"]);
			entity.DestinationBranchId = DBUtility.ToInt32(dataReader["DestinationBranchId"]);
            entity.ReemploymentType = (H_Employee.EmploymentTypes)DBUtility.ToInt32(dataReader["ReemploymentType"]);
			entity.Cause = DBUtility.ToNullableString(dataReader["Cause"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeReemployment> FindByDestinationBranchId(Int32 destinationBranchId, String sortColumns)
		{
			return Find("[DestinationBranchId] = '" + destinationBranchId + "'", sortColumns);
		}

		public static IList<H_EmployeeReemployment> FindByDestinationBranchId(TransactionManager transactionManager, Int32 destinationBranchId, String sortColumns)
		{
			return Find(transactionManager, "[DestinationBranchId] = '" + destinationBranchId + "'", sortColumns);
		}

		public static IList<H_EmployeeReemployment> FindBySourceBranchId(Int32 sourceBranchId, String sortColumns)
		{
			return Find("[SourceBranchId] = '" + sourceBranchId + "'", sortColumns);
		}

		public static IList<H_EmployeeReemployment> FindBySourceBranchId(TransactionManager transactionManager, Int32 sourceBranchId, String sortColumns)
		{
			return Find(transactionManager, "[SourceBranchId] = '" + sourceBranchId + "'", sortColumns);
		}

		public static IList<H_EmployeeReemployment> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeReemployment> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

        public H_EmployeeDrop.Types DropoutType
        {
			get {return this._DropoutType;}
			set {this._DropoutType = value;}
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

		public DateTime ReemploymentDate
		{
			get {return this._ReemploymentDate;}
			set {this._ReemploymentDate = value;}
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

        public H_Employee.EmploymentTypes ReemploymentType
        {
            get { return this._ReemploymentType; }
            set { this._ReemploymentType = value; }
        }

		public String Cause
		{
			get {return this._Cause;}
			set {this._Cause = value;}
		}
	}
}
