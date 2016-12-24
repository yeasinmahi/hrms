using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class Region : EntityBase<Region>
	{
		private Int32 _SubzoneId;
		private String _Name;
        private Statuses _Status;
        private Nullable<DateTime> _OpeningDate;
        private Nullable<DateTime> _ClosingDate;
        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
		public Region()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[Region]"; }
		}

		protected override Region Map(SqlDataReader dataReader)
		{
			Region entity = new Region();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.SubzoneId = DBUtility.ToInt32(dataReader["SubzoneId"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.OpeningDate=DBUtility.ToNullableDateTime(dataReader["OpeningDate"]);
            entity.ClosingDate = DBUtility.ToNullableDateTime(dataReader["ClosingDate"]);
			return entity;
		}

        public static IList<Region> FindBySubzoneId(Int32 subzoneId, String sortColumns)
        {
            return Find("[SubzoneId] = '" + subzoneId + "'", sortColumns);
        }

        public static IList<Region> FindBySubzoneId(TransactionManager transactionManager, Int32 subzoneId, String sortColumns)
        {
            return Find(transactionManager, "[SubzoneId] = '" + subzoneId + "'", sortColumns);
        }

        public static IList<Region> FindByLogin(String whereClause, String sortColumns, String login)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "Id IN(SELECT DISTINCT Region.Id FROM Region INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.RegionId IS NULL OR UserLocation.RegionId = Region.Id) INNER JOIN Branch ON Region.Id = Branch.RegionId  AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return Find(whereClause, sortColumns);
        }

        public static IList<Region> FindByLogin(TransactionManager transactionManager, String whereClause, String sortColumns, String login)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "Id IN(SELECT DISTINCT Region.Id FROM Region INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.RegionId IS NULL OR UserLocation.RegionId = Region.Id) INNER JOIN Branch ON Region.Id = Branch.RegionId  AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return Find(transactionManager, whereClause, sortColumns);
        }

		public Int32 SubzoneId
		{
			get {return this._SubzoneId;}
			set {this._SubzoneId = value;}
		}

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}
        public Statuses Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        public Nullable<DateTime> OpeningDate
        {
            get { return this._OpeningDate; }
            set { this._OpeningDate = value; }
        }
        public Nullable<DateTime> ClosingDate
        {
            get { return this._ClosingDate; }
            set { this._ClosingDate = value; }
        }

	}
}
