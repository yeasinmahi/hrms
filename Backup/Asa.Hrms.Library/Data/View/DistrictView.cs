using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.View
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
			get {return this._Name;}
			set {this._Name = value;}
		}

		public Int32 DivisionId
		{
			get {return this._DivisionId;}
			set {this._DivisionId = value;}
		}

		public String DivisionName
		{
			get {return this._DivisionName;}
			set {this._DivisionName = value;}
		}
	}
}
