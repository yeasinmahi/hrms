using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
	[Serializable]
	[Class(ClassAttribute.Attributes.View)]
	public class ThanaView : ViewBase<ThanaView>
	{
		private String _Name;
		private Int32 _DistrictId;
		private String _DistrictName;

		public ThanaView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[ThanaView]"; }
		}

		protected override ThanaView Map(SqlDataReader dataReader)
		{
			ThanaView view = new ThanaView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
			view.Name = DBUtility.ToString(dataReader["Name"]);
			view.DistrictId = DBUtility.ToInt32(dataReader["DistrictId"]);
			view.DistrictName = DBUtility.ToString(dataReader["DistrictName"]);

			return view;
		}

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

		public Int32 DistrictId
		{
			get {return _DistrictId;}
			set {_DistrictId = value;}
		}

		public String DistrictName
		{
			get {return _DistrictName;}
			set {_DistrictName = value;}
		}
	}
}
