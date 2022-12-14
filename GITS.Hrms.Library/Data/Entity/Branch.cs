using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class Branch : EntityBase<Branch>
	{
		private Int32 _ThanaId;
		private Int32 _RegionId;
		private Nullable<Int32> _Code;
		private String _Name;
        private String _NameInBangla;
        private BranchTypes _BranchType;
		private DateTime _OpeningDate;
		private String _MobileNumber;
		private Statuses _Status;
		private LocationTypes _LocationType;
		private String _Village;
        private String _PostOffice;
        private String _PostCode;

        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
        public enum LocationTypes
        {
            RURAL = 1,
            URBAN = 2,
            SEMI_URBAN = 3,
            CITY_CORPORATION=4,
            REMOTE_AREA=5
        }

        public enum BranchTypes
        {
            Full = 1,
            Central = 2,
            Partners = 3,
            SEL = 4,
            HP = 5,
            ST = 6,
            SUB = 7,
            SB = 8
        }

		public Branch()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[Branch]"; }
		}

		protected override Branch Map(SqlDataReader dataReader)
		{
			Branch entity = new Branch();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.ThanaId = DBUtility.ToInt32(dataReader["ThanaId"]);
			entity.RegionId = DBUtility.ToInt32(dataReader["RegionId"]);
			entity.Code = DBUtility.ToNullableInt32(dataReader["Code"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.NameInBangla = DBUtility.ToString(dataReader["NameInBangla"]);
            entity.BranchType = (BranchTypes)DBUtility.ToInt32(dataReader["BranchType"]);
			entity.OpeningDate = DBUtility.ToDateTime(dataReader["OpeningDate"]);
			entity.MobileNumber = DBUtility.ToNullableString(dataReader["MobileNumber"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
			entity.LocationType = (LocationTypes)DBUtility.ToInt32(dataReader["LocationType"]);
            entity.Village = DBUtility.ToNullableString(dataReader["Village"]);
            entity.PostOffice = DBUtility.ToNullableString(dataReader["PostOffice"]);
            entity.PostCode = DBUtility.ToNullableString(dataReader["PostCode"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static Branch GetByCode(Int32 code)
		{
			return Get("[Code] = '" + code + "'");
		}

		public static Branch GetByCode(TransactionManager transactionManager, Int32 code)
		{
			return Get(transactionManager, "[Code] = '" + code + "'");
		}

		public static IList<Branch> FindByRegionId(Int32 regionId, String sortColumns)
		{
			return Find("[RegionId] = '" + regionId + "'", sortColumns);
		}

		public static IList<Branch> FindByRegionId(TransactionManager transactionManager, Int32 regionId, String sortColumns)
		{
			return Find(transactionManager, "[RegionId] = '" + regionId + "'", sortColumns);
		}

		public static IList<Branch> FindByThanaId(Int32 thanaId, String sortColumns)
		{
			return Find("[ThanaId] = '" + thanaId + "'", sortColumns);
		}

		public static IList<Branch> FindByThanaId(TransactionManager transactionManager, Int32 thanaId, String sortColumns)
		{
			return Find(transactionManager, "[ThanaId] = '" + thanaId + "'", sortColumns);
		}

        public static IList<Branch> FindByBranchType(Int32 typeId, String sortColumns)
        {
            return Find("[BranchType] = '" + typeId + "'", sortColumns);
        }

        public static IList<Branch> FindByBranchType(TransactionManager transactionManager, Int32 typeId, String sortColumns)
        {
            return Find(transactionManager, "[BranchType] = '" + typeId + "'", sortColumns);
        }

        public static IList<Branch> FindByLogin(String whereClause, String sortColumns, String login)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "Id IN(SELECT DISTINCT Branch.Id FROM Branch INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return Find(whereClause, sortColumns);
        }

        public static IList<Branch> FindByLogin(TransactionManager transactionManager, String whereClause, String sortColumns, String login)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "Id IN(SELECT DISTINCT Branch.Id FROM Branch INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return Find(transactionManager, whereClause, sortColumns);
        }

		public Int32 ThanaId
		{
			get {return _ThanaId;}
			set {_ThanaId = value;}
		}

		public Int32 RegionId
		{
			get {return _RegionId;}
			set {_RegionId = value;}
		}

		public Nullable<Int32> Code
		{
			get {return _Code;}
			set {_Code = value;}
		}

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}
        public String NameInBangla
        {
            get { return _NameInBangla; }
            set { _NameInBangla = value; }
        }

        public BranchTypes BranchType
        {
            get { return _BranchType; }
            set { _BranchType = value; }
        }

		public DateTime OpeningDate
		{
			get {return _OpeningDate;}
			set {_OpeningDate = value;}
		}

		public String MobileNumber
		{
			get {return _MobileNumber;}
			set {_MobileNumber = value;}
		}

		public Statuses Status
		{
			get {return _Status;}
			set {_Status = value;}
		}

		public LocationTypes LocationType
		{
			get {return _LocationType;}
			set {_LocationType = value;}
		}

		public String Village
		{
			get {return _Village;}
			set {_Village = value;}
		}
        public String PostOffice
        {
            get { return _PostOffice; }
            set { _PostOffice = value; }
        }
        public String PostCode
        {
            get { return _PostCode; }
            set { _PostCode = value; }
        }

	}
}
