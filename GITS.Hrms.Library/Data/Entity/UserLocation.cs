using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class UserLocation : EntityBase<UserLocation>
	{
		private String _Login;
		private Nullable<Int32> _ZoneId;
        private Nullable<Int32> _SubzoneId;
		private Nullable<Int32> _RegionId;
		private Nullable<Int32> _BranchId;

		public UserLocation()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[UserLocation]"; }
		}

		protected override UserLocation Map(SqlDataReader dataReader)
		{
			UserLocation entity = new UserLocation();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Login = DBUtility.ToString(dataReader["Login"]);
			entity.ZoneId = DBUtility.ToNullableInt32(dataReader["ZoneId"]);
            entity.SubzoneId = DBUtility.ToNullableInt32(dataReader["SubzoneId"]);
			entity.RegionId = DBUtility.ToNullableInt32(dataReader["RegionId"]);
			entity.BranchId = DBUtility.ToNullableInt32(dataReader["BranchId"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static UserLocation GetByLogin(String login)
		{
			return Get("[Login] = '" + login + "'");
		}

		public static UserLocation GetByLogin(TransactionManager transactionManager, String login)
		{
			return Get(transactionManager, "[Login] = '" + login + "'");
		}

		public static IList<UserLocation> FindByBranchId(Int32 branchId, String sortColumns)
		{
			return Find("[BranchId] = '" + branchId + "'", sortColumns);
		}

		public static IList<UserLocation> FindByBranchId(TransactionManager transactionManager, Int32 branchId, String sortColumns)
		{
			return Find(transactionManager, "[BranchId] = '" + branchId + "'", sortColumns);
		}

		public static IList<UserLocation> FindByLogin(String login, String sortColumns)
		{
			return Find("[Login] = '" + login + "'", sortColumns);
		}

		public static IList<UserLocation> FindByLogin(TransactionManager transactionManager, String login, String sortColumns)
		{
			return Find(transactionManager, "[Login] = '" + login + "'", sortColumns);
		}

		public static IList<UserLocation> FindByRegionId(Int32 regionId, String sortColumns)
		{
			return Find("[RegionId] = '" + regionId + "'", sortColumns);
		}

        public static IList<UserLocation> FindByRegionId(TransactionManager transactionManager, Int32 regionId, String sortColumns)
		{
			return Find(transactionManager, "[RegionId] = '" + regionId + "'", sortColumns);
		}

        public static IList<UserLocation> FindBySubzoneId(Int32 subzoneId, String sortColumns)
        {
            return Find("[SubzoneId] = '" + subzoneId + "'", sortColumns);
        }

        public static IList<UserLocation> FindBySubzoneId(TransactionManager transactionManager, Int32 subzoneId, String sortColumns)
        {
            return Find(transactionManager, "[SubzoneId] = '" + subzoneId + "'", sortColumns);
        }

		public static IList<UserLocation> FindByZoneId(Int32 zoneId, String sortColumns)
		{
			return Find("[ZoneId] = '" + zoneId + "'", sortColumns);
		}

		public static IList<UserLocation> FindByZoneId(TransactionManager transactionManager, Int32 zoneId, String sortColumns)
		{
			return Find(transactionManager, "[ZoneId] = '" + zoneId + "'", sortColumns);
		}

		public String Login
		{
			get {return _Login;}
			set {_Login = value;}
		}

		public Nullable<Int32> ZoneId
		{
			get {return _ZoneId;}
			set {_ZoneId = value;}
		}

        public Nullable<Int32> SubzoneId
        {
            get { return _SubzoneId; }
            set { _SubzoneId = value; }
        }

		public Nullable<Int32> RegionId
		{
			get {return _RegionId;}
			set {_RegionId = value;}
		}

		public Nullable<Int32> BranchId
		{
			get {return _BranchId;}
			set {_BranchId = value;}
		}

	}
}
