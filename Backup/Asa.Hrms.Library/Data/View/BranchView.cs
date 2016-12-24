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
	public class BranchView : ViewBase<BranchView>
	{
		private Nullable<Int32> _Code;
		private String _Name;
        private String _NameInBangla;
        private Branch.BranchTypes _BranchType;
		private DateTime _OpeningDate;
		private String _MobileNumber;
        private String _Status;
		private Branch.LocationTypes _LocationType;
		private Int32 _RegionId;
		private String _RegionName;
        private Int32 _SubzoneId;
        private String _SubzoneName;
		private Int32 _ZoneId;
		private String _ZoneName;
        private Int32 _DistrictId;
        private String _DistrictName;
        private Int32 _ThanaId;
		private String _ThanaName;

		public BranchView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[BranchView]"; }
		}

		protected override BranchView Map(SqlDataReader dataReader)
		{
			BranchView view = new BranchView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
			view.Code = DBUtility.ToNullableInt32(dataReader["Code"]);
			view.Name = DBUtility.ToString(dataReader["Name"]);
            view.NameInBangla = DBUtility.ToNullableString(dataReader["NameInBangla"]);
            view.BranchType = (Branch.BranchTypes)DBUtility.ToInt32(dataReader["BranchType"]);
			view.OpeningDate = DBUtility.ToDateTime(dataReader["OpeningDate"]);
			view.MobileNumber = DBUtility.ToNullableString(dataReader["MobileNumber"]);
            view.Status = DBUtility.ToString(dataReader["Status"]);
            view.LocationType = (Branch.LocationTypes)DBUtility.ToInt32(dataReader["LocationType"]);
			view.RegionId = DBUtility.ToInt32(dataReader["RegionId"]);
			view.RegionName = DBUtility.ToString(dataReader["RegionName"]);
            view.SubzoneId = DBUtility.ToInt32(dataReader["SubzoneId"]);
            view.SubzoneName = DBUtility.ToString(dataReader["SubzoneName"]);
			view.ZoneId = DBUtility.ToInt32(dataReader["ZoneId"]);
			view.ZoneName = DBUtility.ToString(dataReader["ZoneName"]);
			view.DistrictId = DBUtility.ToInt32(dataReader["DistrictId"]);
			view.DistrictName = DBUtility.ToString(dataReader["DistrictName"]);
            view.ThanaId = DBUtility.ToInt32(dataReader["ThanaId"]);
            view.ThanaName = DBUtility.ToString(dataReader["ThanaName"]);

			return view;
		}

		public Nullable<Int32> Code
		{
			get {return this._Code;}
			set {this._Code = value;}
		}

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}
        public String NameInBangla
        {
            get { return this._NameInBangla; }
            set { this._NameInBangla = value; }
        }

        public Branch.BranchTypes BranchType
        {
            get { return this._BranchType; }
            set { this._BranchType = value; }
        }

		public DateTime OpeningDate
		{
			get {return this._OpeningDate;}
			set {this._OpeningDate = value;}
		}

		public String MobileNumber
		{
			get {return this._MobileNumber;}
			set {this._MobileNumber = value;}
		}

        public String Status
		{
			get {return this._Status;}
			set {this._Status = value;}
		}

		public Branch.LocationTypes LocationType
		{
			get {return this._LocationType;}
			set {this._LocationType = value;}
		}

		public Int32 RegionId
		{
			get {return this._RegionId;}
			set {this._RegionId = value;}
		}

		public String RegionName
		{
			get {return this._RegionName;}
			set {this._RegionName = value;}
		}

        public Int32 SubzoneId
        {
            get { return this._SubzoneId; }
            set { this._SubzoneId = value; }
        }

        public String SubzoneName
        {
            get { return this._SubzoneName; }
            set { this._SubzoneName = value; }
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

        public Int32 DistrictId
        {
            get { return this._DistrictId; }
            set { this._DistrictId = value; }
        }

        public String DistrictName
        {
            get { return this._DistrictName; }
            set { this._DistrictName = value; }
        }

		public Int32 ThanaId
		{
			get {return this._ThanaId;}
			set {this._ThanaId = value;}
		}

		public String ThanaName
		{
			get {return this._ThanaName;}
			set {this._ThanaName = value;}
		}

		
	}
}
