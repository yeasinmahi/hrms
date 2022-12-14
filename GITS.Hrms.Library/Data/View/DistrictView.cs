using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
	[Serializable]
	[Class(ClassAttribute.Attributes.View)]
	public class DistrictView : ViewBase<DistrictView>
	{
		private String _Name;
		private Int32 _DivisionId;
		private String _DivisionName;

		public DistrictView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[DistrictView]"; }
		}

		protected override DistrictView Map(SqlDataReader dataReader)
		{
			DistrictView view = new DistrictView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
			view.Name = DBUtility.ToString(dataReader["Name"]);
			view.DivisionId = DBUtility.ToInt32(dataReader["DivisionId"]);
			view.DivisionName = DBUtility.ToString(dataReader["DivisionName"]);

			return view;
		}

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

		public Int32 DivisionId
		{
			get {return _DivisionId;}
			set {_DivisionId = value;}
		}

		public String DivisionName
		{
			get {return _DivisionName;}
			set {_DivisionName = value;}
		}
	}
}
