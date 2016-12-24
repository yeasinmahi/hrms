using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.View
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
			get {return this._ModuleName;}
			set {this._ModuleName = value;}
		}

		public String RoleName
		{
			get {return this._RoleName;}
			set {this._RoleName = value;}
		}

		public String PropertyName
		{
			get {return this._PropertyName;}
			set {this._PropertyName = value;}
		}

		public String PropertyDisplayName
		{
			get {return this._PropertyDisplayName;}
			set {this._PropertyDisplayName = value;}
		}

	}
}
