using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
	[Serializable]
	[Class(ClassAttribute.Attributes.View)]
	public class RolePropertyView : ViewBase<RolePropertyView>
	{
		private String _ModuleName;
		private String _RoleName;
		private String _PropertyName;
		private String _PropertyDisplayName;

		public RolePropertyView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[RolePropertyView]"; }
		}

		protected override RolePropertyView Map(SqlDataReader dataReader)
		{
			RolePropertyView view = new RolePropertyView();

			view.ModuleName = DBUtility.ToString(dataReader["ModuleName"]);
			view.RoleName = DBUtility.ToString(dataReader["RoleName"]);
			view.PropertyName = DBUtility.ToString(dataReader["PropertyName"]);
			view.PropertyDisplayName = DBUtility.ToString(dataReader["PropertyDisplayName"]);

			return view;
		}

		public String ModuleName
		{
			get {return _ModuleName;}
			set {_ModuleName = value;}
		}

		public String RoleName
		{
			get {return _RoleName;}
			set {_RoleName = value;}
		}

		public String PropertyName
		{
			get {return _PropertyName;}
			set {_PropertyName = value;}
		}

		public String PropertyDisplayName
		{
			get {return _PropertyDisplayName;}
			set {_PropertyDisplayName = value;}
		}

	}
}
