using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
    public class Subzone : EntityBase<Subzone>
	{
		private Int32 _ZoneId;
		private String _Name;
        private String _NameInBangla;
        private Statuses _Status;
        private Nullable<DateTime> _OpeningDate;
        private Nullable<DateTime> _ClosingDate;
        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
        public Subzone()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[Subzone]"; }
		}

        protected override Subzone Map(SqlDataReader dataReader)
		{
			Subzone entity = new Subzone();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.ZoneId = DBUtility.ToInt32(dataReader["ZoneId"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.NameInBangla = DBUtility.ToString(dataReader["NameInBangla"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.OpeningDate = DBUtility.ToNullableDateTime(dataReader["OpeningDate"]);
            entity.ClosingDate = DBUtility.ToNullableDateTime(dataReader["ClosingDate"]);
			return entity;
		}

        public static IList<Subzone> FindByZoneId(Int32 divisionId, String sortColumns)
        {
            return Find("[ZoneId] = '" + divisionId + "'", sortColumns);
        }

        public static IList<Subzone> FindByZoneId(TransactionManager transactionManager, Int32 divisionId, String sortColumns)
        {
            return Find(transactionManager, "[ZoneId] = '" + divisionId + "'", sortColumns);
        }

        public static IList<Subzone> FindByLogin(String whereClause, String sortColumns, String login)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "Id IN(SELECT DISTINCT Subzone.Id FROM Subzone INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.SubzoneId IS NULL OR UserLocation.SubzoneId = Subzone.Id) INNER JOIN Region ON Subzone.Id = Region.SubzoneId AND (UserLocation.RegionId IS NULL OR UserLocation.RegionId = Region.Id) INNER JOIN Branch ON Region.Id = Branch.RegionId  AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return Find(whereClause, sortColumns);
        }
        public static IList<Subzone> FindSubZoneByLogin(String whereClause, String sortColumns, String login)
        {

            string filter = string.Empty;
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            UserLocation uLocation = UserLocation.FindByLogin(login, "")[0];

            if (uLocation.ZoneId != null)
            {
                filter = " ZoneId =" + uLocation.ZoneId;
            }
            if (uLocation.SubzoneId != null)
            {
                filter = " Id =" + uLocation.SubzoneId + ")";
            }
            else if (uLocation.RegionId != null)
            {
                filter = " Id In (Select Distinct Subzone.Id from Subzone INNER JOIN Region ON Region.SubzoneId=Subzone.Id AND Region.Id " + uLocation.RegionId + ")";
            }
            else if (uLocation.BranchId != null)
            {
                filter = " Id In (Select Distinct Subzone.Id from Subzone INNER JOIN Region ON Region.SubzoneId=Subzone.Id INNER JOIN Branch ON Branch.RegionId=Region.Id  AND Branch.Id=" + uLocation.BranchId + ")";
            }
            else
            {
                filter = string.Empty;
            }

            whereClause += filter;

            return Find(whereClause, sortColumns);
        }
        public static IList<Subzone> FindByLogin(TransactionManager transactionManager, String whereClause, String sortColumns, String login)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "Id IN(SELECT DISTINCT Subzone.Id FROM Subzone INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.SubzoneId IS NULL OR UserLocation.SubzoneId = Subzone.Id) INNER JOIN Region ON Subzone.Id = Region.SubzoneId AND (UserLocation.RegionId IS NULL OR UserLocation.RegionId = Region.Id) INNER JOIN Branch ON Region.Id = Branch.RegionId  AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return Find(transactionManager, whereClause, sortColumns);
        }

		public Int32 ZoneId
		{
			get {return _ZoneId;}
			set {_ZoneId = value;}
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
