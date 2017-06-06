using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class ReportConfig : EntityBase<ReportConfig>
	{
		private String _Name;
        private ReportType _Type;
		private String _Query;
        private Int32 _DateBetween;
        private Boolean _Location;
        private Boolean _ReligionAndSex;
        private Boolean _Position;

        public enum ReportType
        {
            HRM_Report = 1,
            Transfer_Info = 2,
            Assesment_Info = 4,
            Promotion_Demotion=8,
            Punishment_Report=16,
            Summary = 64
        }

		public ReportConfig()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[ReportConfig]"; }
		}

		protected override ReportConfig Map(SqlDataReader dataReader)
		{
			ReportConfig entity = new ReportConfig();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.Type = (ReportType)DBUtility.ToInt32(dataReader["Type"]);            
			entity.Query = DBUtility.ToString(dataReader["Query"]);
            entity.Location = DBUtility.ToBoolean(dataReader["Location"]);
            entity.Position = DBUtility.ToBoolean(dataReader["Position"]);
            entity.ReligionAndSex = DBUtility.ToBoolean(dataReader["ReligionAndSex"]);
            entity.DateBetween = DBUtility.ToInt32(dataReader["DateBetween"]);

			return entity;
		}

        public static IList<ReportConfig> FindByType(Int32 typeId, string sortColumns) 
        {
            return Find("[Type] = '" + typeId + "'", sortColumns);
        }

        public static IList<ReportConfig> FindByType(TransactionManager transactionManager, Int32 typeId, string sortColumns)
        {
            return Find(transactionManager, "[Type] = '" + typeId + "'", sortColumns);
        }

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

        public ReportType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

		public String Query
		{
			get {return _Query;}
			set {_Query = value;}
		}

        public int DateBetween
        {
            get { return _DateBetween; }
            set { _DateBetween = value; }
        }

        public Boolean Location
        {
            get { return _Location; }
            set { _Location=value; }
        }

        public Boolean ReligionAndSex
        {
            get { return _ReligionAndSex; }
            set { _ReligionAndSex = value; }
        }

        public Boolean Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
	}
}
