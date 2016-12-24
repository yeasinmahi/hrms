using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
	[Serializable]
	[Class(ClassAttribute.Attributes.View)]
	public class UserPropertyView : ViewBase<UserPropertyView>
	{
		private String _ModuleName;
		private String _UserLogin;
		private String _RoleName;
		private String _PropertyName;

		public UserPropertyView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[UserPropertyView]"; }
		}

		protected override UserPropertyView Map(SqlDataReader dataReader)
		{
			UserPropertyView view = new UserPropertyView();

			view.ModuleName = DBUtility.ToString(dataReader["ModuleName"]);
			view.UserLogin = DBUtility.ToString(dataReader["UserLogin"]);
			view.RoleName = DBUtility.ToString(dataReader["RoleName"]);
			view.PropertyName = DBUtility.ToString(dataReader["PropertyName"]);

			return view;
		}

		public String ModuleName
		{
			get {return this._ModuleName;}
			set {this._ModuleName = value;}
		}

		public String UserLogin
		{
			get {return this._UserLogin;}
			set {this._UserLogin = value;}
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

	}
}
