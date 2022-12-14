using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
	[Serializable]
	[Class(ClassAttribute.Attributes.View)]
	public class SubzoneView : ViewBase<SubzoneView>
	{
		private String _Name;
        private String _NameInBangla;
		private Int32 _ZoneId;
		private String _ZoneName;
        private Subzone.Statuses _Status;

        public SubzoneView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[SubzoneView]"; }
		}

        protected override SubzoneView Map(SqlDataReader dataReader)
		{
            SubzoneView view = new SubzoneView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
			view.Name = DBUtility.ToString(dataReader["Name"]);
            view.NameInBangla = DBUtility.ToNullableString(dataReader["NameInBangla"]);
			view.ZoneId = DBUtility.ToInt32(dataReader["ZoneId"]);
			view.ZoneName = DBUtility.ToString(dataReader["ZoneName"]);
            view.Status = (Subzone.Statuses)DBUtility.ToInt32(dataReader["Status"]);
			return view;
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
        public Subzone.Statuses Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
	}
}
