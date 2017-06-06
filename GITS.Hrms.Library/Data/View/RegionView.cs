using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
	[Serializable]
	[Class(ClassAttribute.Attributes.View)]
	public class RegionView : ViewBase<RegionView>
	{
		private String _Name;
		private Int32 _SubzoneId;
		private String _SubzoneName;
		private Int32 _ZoneId;
		private String _ZoneName;
        private Region.Statuses _Status;

		public RegionView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[RegionView]"; }
		}

		protected override RegionView Map(SqlDataReader dataReader)
		{
			RegionView view = new RegionView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
			view.Name = DBUtility.ToString(dataReader["Name"]);
			view.SubzoneId = DBUtility.ToInt32(dataReader["SubzoneId"]);
			view.SubzoneName = DBUtility.ToString(dataReader["SubzoneName"]);
			view.ZoneId = DBUtility.ToInt32(dataReader["ZoneId"]);
			view.ZoneName = DBUtility.ToString(dataReader["ZoneName"]);
            view.Status = (Region.Statuses)DBUtility.ToInt32(dataReader["Status"]);
			return view;
		}

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

		public Int32 SubzoneId
		{
			get {return _SubzoneId;}
			set {_SubzoneId = value;}
		}

		public String SubzoneName
		{
			get {return _SubzoneName;}
			set {_SubzoneName = value;}
		}

		public Int32 ZoneId
		{
			get {return _ZoneId;}
			set {_ZoneId = value;}
		}

		public String ZoneName
		{
			get {return _ZoneName;}
			set {_ZoneName = value;}
		}
        public Region.Statuses Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
	}
}
