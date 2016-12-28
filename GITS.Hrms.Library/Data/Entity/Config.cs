using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class Config : EntityBase<Config>
	{
		private String _Key;
		private String _Name;
		private String _DataType;
		private String _ReadableDataType;
		private String _Value;

        public const String KEY_DECIMAL_PLACE = "DECIMAL PLACE";
        public const String KEY_ACCOUNTS_DECIMAL_PLACE = "ACCOUNTS DECIMAL PLACE";
        public const String KEY_DAYS_IN_YEAR = "DAYS IN YEAR";
        public const String KEY_CASH_AT_HAND = "CASH AT HAND";
        public const String KEY_CASH_AT_BANK = "CASH AT BANK";
        public const String KEY_RETAINED_EARNINGS = "RETAINED EARNINGS";
        public const String KEY_LOAN_LOSS_PROVISION_CURRENT = "LOAN LOSS PROVISION (CURRENT)";
        public const String KEY_LOAN_LOSS_PROVISION_1 = "LOAN LOSS PROVISION (1-30)";
        public const String KEY_LOAN_LOSS_PROVISION_31 = "LOAN LOSS PROVISION (31-90)";
        public const String KEY_LOAN_LOSS_PROVISION_91 = "LOAN LOSS PROVISION (91-180)";
        public const String KEY_LOAN_LOSS_PROVISION_181 = "LOAN LOSS PROVISION (181-365)";
        public const String KEY_LOAN_LOSS_PROVISION_OVER = "LOAN LOSS PROVISION (OVER)";
        public const String KEY_REPORT_EXTENSION = "REPORT EXTENSION";
        public const String KEY_COUNTRY_EXTENSION = "COUNTRY EXTENSION";
        public const String KEY_ENABLE_AUDIT = "ENABLE AUDIT";
        public const String KEY_AUDIT_DURATION = "AUDIT DURATION";

		public Config()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[Config]"; }
		}

		protected override Config Map(SqlDataReader dataReader)
		{
			Config entity = new Config();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Key = DBUtility.ToString(dataReader["Key"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
			entity.DataType = DBUtility.ToString(dataReader["DataType"]);
			entity.ReadableDataType = DBUtility.ToString(dataReader["ReadableDataType"]);
			entity.Value = DBUtility.ToString(dataReader["Value"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static Config GetByKey(String key)
		{
			return Get("[Key] = '" + key + "'");
		}

		public static Config GetByKey(TransactionManager transactionManager, String key)
		{
			return Get(transactionManager, "[Key] = '" + key + "'");
		}

		public String Key
		{
			get {return this._Key;}
			set {this._Key = value;}
		}

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}

		public String DataType
		{
			get {return this._DataType;}
			set {this._DataType = value;}
		}

		public String ReadableDataType
		{
			get {return this._ReadableDataType;}
			set {this._ReadableDataType = value;}
		}

		public String Value
		{
			get {return this._Value;}
			set {this._Value = value;}
		}

	}
}
