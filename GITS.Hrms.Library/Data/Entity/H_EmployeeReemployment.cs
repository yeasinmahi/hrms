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
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

        public H_EmployeeDrop.Types DropoutType
        {
			get {return _DropoutType;}
			set {_DropoutType = value;}
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

		public DateTime ReemploymentDate
		{
			get {return _ReemploymentDate;}
			set {_ReemploymentDate = value;}
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

        public H_Employee.EmploymentTypes ReemploymentType
        {
            get { return _ReemploymentType; }
            set { _ReemploymentType = value; }
        }

		public String Cause
		{
			get {return _Cause;}
			set {_Cause = value;}
		}
	}
}
