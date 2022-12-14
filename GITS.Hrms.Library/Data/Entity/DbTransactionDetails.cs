using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class DbTransactionDetails : EntityBase<DbTransactionDetails>
	{
		private Int32 _DbTransactionId;
		private String _Type;
		private String _TableName;
		private String _IdentityColumn;
		private String _IdentityValue;
		private String _Value;

        public const string TYPE_INSERT = "INSERT";
        public const string TYPE_UPDATE = "UPDATE";
        public const string TYPE_DELETE = "DELETE";

		public DbTransactionDetails()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[DbTransactionDetails]"; }
		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override bool Audit
		{
			get { return false; }
		}

		protected override DbTransactionDetails Map(SqlDataReader dataReader)
		{
			DbTransactionDetails entity = new DbTransactionDetails();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.DbTransactionId = DBUtility.ToInt32(dataReader["DbTransactionId"]);
			entity.Type = DBUtility.ToString(dataReader["Type"]);
			entity.TableName = DBUtility.ToString(dataReader["TableName"]);
			entity.IdentityColumn = DBUtility.ToString(dataReader["IdentityColumn"]);
            entity.IdentityValue = DBUtility.ToString(dataReader["IdentityValue"]);
			entity.Value = DBUtility.ToNullableString(dataReader["Value"]);

			return entity;
		}
		
		public static IList<DbTransactionDetails> FindByDbTransactionId(Int32 dbTransactionId)
		{
			return Find("[DbTransactionId] = '" + dbTransactionId + "'", "");
		}

		public static IList<DbTransactionDetails> FindByDbTransactionId(TransactionManager transactionManager, Int32 dbTransactionId)
		{
			return Find(transactionManager, "[DbTransactionId] = '" + dbTransactionId + "'", "");
		}

		public Int32 DbTransactionId
		{
			get {return _DbTransactionId;}
			set {_DbTransactionId = value;}
		}

		public String Type
		{
			get {return _Type;}
			set {_Type = value;}
		}

		public String TableName
		{
			get {return _TableName;}
			set {_TableName = value;}
		}

		public String IdentityColumn
		{
			get {return _IdentityColumn;}
			set {_IdentityColumn = value;}
		}

		public String IdentityValue
		{
			get {return _IdentityValue;}
			set {_IdentityValue = value;}
		}

		public String Value
		{
			get {return _Value;}
			set {_Value = value;}
		}

	}
}
