using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
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
			get {return _Location;}
			set {_Location = value;}
		}

		public String Type
		{
			get {return _Type;}
			set {_Type = value;}
		}

		public String Version
		{
			get {return _Version;}
			set {_Version = value;}
		}

		public DateTime StartDate
		{
			get {return _StartDate;}
			set {_StartDate = value;}
		}

		public Nullable<DateTime> EndDate
		{
			get {return _EndDate;}
			set {_EndDate = value;}
		}

		public String Status
		{
			get {return _Status;}
			set {_Status = value;}
		}

	}
}
