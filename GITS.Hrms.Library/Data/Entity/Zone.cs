using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class Zone : EntityBase<Zone>
	{
		private String _Name;
        private Statuses _Status;
        private Nullable<DateTime> _OpeningDate;
        private Nullable<DateTime> _ClosingDate;
        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
        public Zone()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[Zone]"; }
		}

        protected override Zone Map(SqlDataReader dataReader)
		{
            Zone entity = new Zone();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.OpeningDate = DBUtility.ToNullableDateTime(dataReader["OpeningDate"]);
            entity.ClosingDate = DBUtility.ToNullableDateTime(dataReader["ClosingDate"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<Zone> FindByLogin(String whereClause, String sortColumns, String login)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "Id IN(SELECT DISTINCT Zone.Id FROM Zone INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.ZoneId IS NULL OR UserLocation.ZoneId = Zone.Id) INNER JOIN Subzone ON Zone.Id = Subzone.ZoneId AND (UserLocation.SubzoneId IS NULL OR UserLocation.SubzoneId = Subzone.Id) INNER JOIN Region ON Subzone.Id = Region.SubzoneId AND (UserLocation.RegionId IS NULL OR UserLocation.RegionId = Region.Id) INNER JOIN Branch ON Region.Id = Branch.RegionId  AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return Find(whereClause, sortColumns);
        }
        public static IList<Zone> FindZoneByLogin(String whereClause, String sortColumns, String login)
        {
            
            string filter = string.Empty;
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            UserLocation uLocation = UserLocation.FindByLogin(login, "")[0];

            if (uLocation.ZoneId != null)
            {
                filter = " Id =" + uLocation.ZoneId;
            }
            if (uLocation.SubzoneId != null)
            {
                filter = " Id In (Select Distinct Zone.Id from Zone INNER JOIN Subzone ON Zone.Id = Subzone.ZoneId AND  Subzone.Id =" + uLocation.SubzoneId + ")";
            }
            else if (uLocation.RegionId != null)
            {
                filter = " Id In (Select Distinct Zone.Id from Zone INNER JOIN Subzone ON Zone.Id = Subzone.ZoneId INNER JOIN Region ON Region.SubzoneId=Subzone.Id AND Region.Id "+uLocation.RegionId+")";
            }
            else if (uLocation.BranchId != null)
            {
                filter = " Id In (Select Distinct Zone.Id from Zone INNER JOIN Subzone ON Zone.Id = Subzone.ZoneId INNER JOIN Region ON Region.SubzoneId=Subzone.Id INNER JOIN Branch ON Branch.RegionId=Region.Id  AND Branch.Id=" + uLocation.BranchId + ")";
            }
            else
            {
                filter = string.Empty;
            }

            whereClause += filter;

            return Find(whereClause, sortColumns);
        }
        public static IList<Zone> FindByLogin(TransactionManager transactionManager, String whereClause, String sortColumns, String login)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "Id IN(SELECT DISTINCT Zone.Id FROM Zone INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.ZoneId IS NULL OR UserLocation.ZoneId = Zone.Id) INNER JOIN Subzone ON Zone.Id = Subzone.ZoneId AND (UserLocation.SubzoneId IS NULL OR UserLocation.SubzoneId = Subzone.Id) INNER JOIN Region ON Subzone.Id = Region.SubzoneId AND (UserLocation.RegionId IS NULL OR UserLocation.RegionId = Region.Id) INNER JOIN Branch ON Region.Id = Branch.RegionId  AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return Find(transactionManager, whereClause, sortColumns);
        }

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}
        public Statuses Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public Nullable<DateTime> OpeningDate
        {
            get { return _OpeningDate; }
            set { _OpeningDate = value; }
        }
        public Nullable<DateTime> ClosingDate
        {
            get { return _ClosingDate; }
            set { _ClosingDate = value; }
        }
	}
}
