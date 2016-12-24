using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
	[Serializable]
	[Class(ClassAttribute.Attributes.View)]
	public class UserCommandView : ViewBase<UserCommandView>
	{
		private String _UserLogin;
		private String _RoleName;
		private String _PropertyName;
		private String _CommandName;

		public UserCommandView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[UserCommandView]"; }
		}

		protected override UserCommandView Map(SqlDataReader dataReader)
		{
			UserCommandView view = new UserCommandView();

			view.UserLogin = DBUtility.ToString(dataReader["UserLogin"]);
			view.RoleName = DBUtility.ToString(dataReader["RoleName"]);
			view.PropertyName = DBUtility.ToString(dataReader["PropertyName"]);
			view.CommandName = DBUtility.ToString(dataReader["CommandName"]);

			return view;
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

		public String CommandName
		{
			get {return this._CommandName;}
			set {this._CommandName = value;}
		}

	}
}
