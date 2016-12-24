using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class Installation : EntityBase<Installation>
	{
		private String _Location;
		private String _Type;
		private String _Version;
		private DateTime _StartDate;
		private Nullable<DateTime> _EndDate;
		private String _Status;

        public const string TYPE_FRESH = "FRESH";
        public const string TYPE_UPGRADE = "UPGRADE";
        public const string TYPE_PATCH = "PATCH";

        public const string STATUS_COMPLETE = "COMPLETE";
        public const string STATUS_INCOMPLETE = "INCOMPLETE";

        public Installation()
        {

        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override bool Audit
        {
            get
            {
                return false;
            }
        }

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[Installation]"; }
		}

		protected override Installation Map(SqlDataReader dataReader)
		{
			Installation entity = new Installation();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Location = DBUtility.ToString(dataReader["Location"]);
			entity.Type = DBUtility.ToString(dataReader["Type"]);
			entity.Version = DBUtility.ToString(dataReader["Version"]);
			entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
			entity.EndDate = DBUtility.ToNullableDateTime(dataReader["EndDate"]);
			entity.Status = DBUtility.ToString(dataReader["Status"]);

			return entity;
		}

		public static Installation GetByTypeAndVersion(String type, String version)
		{
			return Get("[Type] = '" + type + "' AND [Version] = '" + version + "'");
		}

		public static Installation GetByTypeAndVersion(TransactionManager transactionManager, String type, String version)
		{
			return Get(transactionManager, "[Type] = '" + type + "' AND [Version] = '" + version + "'");
		}

		public String Location
		{
			get {return this._Location;}
			set {this._Location = value;}
		}

		public String Type
		{
			get {return this._Type;}
			set {this._Type = value;}
		}

		public String Version
		{
			get {return this._Version;}
			set {this._Version = value;}
		}

		public DateTime StartDate
		{
			get {return this._StartDate;}
			set {this._StartDate = value;}
		}

		public Nullable<DateTime> EndDate
		{
			get {return this._EndDate;}
			set {this._EndDate = value;}
		}

		public String Status
		{
			get {return this._Status;}
			set {this._Status = value;}
		}

	}
}
