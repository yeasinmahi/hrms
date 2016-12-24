using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.Data.View
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
			get {return this._Name;}
			set {this._Name = value;}
		}

		public Int32 SubzoneId
		{
			get {return this._SubzoneId;}
			set {this._SubzoneId = value;}
		}

		public String SubzoneName
		{
			get {return this._SubzoneName;}
			set {this._SubzoneName = value;}
		}

		public Int32 ZoneId
		{
			get {return this._ZoneId;}
			set {this._ZoneId = value;}
		}

		public String ZoneName
		{
			get {return this._ZoneName;}
			set {this._ZoneName = value;}
		}
        public Region.Statuses Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
	}
}
