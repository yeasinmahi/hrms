using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
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

        public Branch.BranchTypes BranchType
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

        public String Status
		{
			get {return _Status;}
			set {_Status = value;}
		}

		public Branch.LocationTypes LocationType
		{
			get {return _LocationType;}
			set {_LocationType = value;}
		}

		public Int32 RegionId
		{
			get {return _RegionId;}
			set {_RegionId = value;}
		}

		public String RegionName
		{
			get {return _RegionName;}
			set {_RegionName = value;}
		}

        public Int32 SubzoneId
        {
            get { return _SubzoneId; }
            set { _SubzoneId = value; }
        }

        public String SubzoneName
        {
            get { return _SubzoneName; }
            set { _SubzoneName = value; }
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

        public Int32 DistrictId
        {
            get { return _DistrictId; }
            set { _DistrictId = value; }
        }

        public String DistrictName
        {
            get { return _DistrictName; }
            set { _DistrictName = value; }
        }

		public Int32 ThanaId
		{
			get {return _ThanaId;}
			set {_ThanaId = value;}
		}

		public String ThanaName
		{
			get {return _ThanaName;}
			set {_ThanaName = value;}
		}

		
	}
}
