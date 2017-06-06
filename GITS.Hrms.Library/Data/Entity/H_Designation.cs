using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_Designation : EntityBase<H_Designation>
	{
		private String _Name;
        private String _BanglaName;
        private String _ShortName;
		private Int32 _SortOrder;
        private Nullable< GroupTypes> _GroupType;
        private Statuses _Status;
        public enum GroupTypes
        {
            LO = 1,
            BM = 2,
            ABM = 3,
            RM = 4,
            DM = 5,
            ADM = 6,
            Auditor = 7,
            Other = 8,
            Peon=9,
            Sr_LO=10,
            SBM=11,
            ZM=12,
            Jr_Auditor=13,
            Sr_Auditor=14,
            Auditor_M=15,
            Sr_Auditor_M=16,
            ZA=17,
            IRO=18,
            Off_Asst=19,
            Central=20,
            ASE=21,
            CO=22
        }
        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
		public H_Designation()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_Designation]"; }
		}

		protected override H_Designation Map(SqlDataReader dataReader)
		{
			H_Designation entity = new H_Designation();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.ShortName = DBUtility.ToNullableString(dataReader["ShortName"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);
		    var nullableInt32 = DBUtility.ToNullableInt32(dataReader["GroupType"]);
		    if (nullableInt32 != null)
		        entity.GroupType = (GroupTypes)nullableInt32;
		    entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.BanglaName = DBUtility.ToNullableString(dataReader["BanglaName"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static H_Designation GetByName(String name)
		{
			return Get("[Name] = '" + name + "'");
		}

		public static H_Designation GetByName(TransactionManager transactionManager, String name)
		{
			return Get(transactionManager, "[Name] = '" + name + "'");
		}

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}
        public String ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }
		public Int32 SortOrder
		{
			get {return _SortOrder;}
			set {_SortOrder = value;}
		}
        public Nullable<GroupTypes> GroupType
        {
            get { return _GroupType; }
            set { _GroupType = value; }
        }
        public Statuses Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public String BanglaName
        {
            get { return _BanglaName; }
            set { _BanglaName = value; }
        }
	}
}
