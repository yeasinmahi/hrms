using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
	[Serializable]
	[Class(ClassAttribute.Attributes.View)]
	public class PropertyCommandView : ViewBase<PropertyCommandView>
	{
		private String _PropertyName;
		private String _CommandName;
		private String _PermissionType;
		private String _PermissionCommand;
		private String _DisplayName;
		private String _ToolTipText;
		private String _ImageUrl;
		private String _SeperatorUrl;
		private String _NavigateUrl;
		private Int32 _SortOrder;

		public PropertyCommandView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[PropertyCommandView]"; }
		}

		protected override PropertyCommandView Map(SqlDataReader dataReader)
		{
			PropertyCommandView view = new PropertyCommandView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
			view.PropertyName = DBUtility.ToString(dataReader["PropertyName"]);
			view.CommandName = DBUtility.ToString(dataReader["CommandName"]);
			view.PermissionType = DBUtility.ToString(dataReader["PermissionType"]);
			view.PermissionCommand = DBUtility.ToNullableString(dataReader["PermissionCommand"]);
			view.DisplayName = DBUtility.ToString(dataReader["DisplayName"]);
			view.ToolTipText = DBUtility.ToNullableString(dataReader["ToolTipText"]);
			view.ImageUrl = DBUtility.ToNullableString(dataReader["ImageUrl"]);
			view.SeperatorUrl = DBUtility.ToNullableString(dataReader["SeperatorUrl"]);
			view.NavigateUrl = DBUtility.ToNullableString(dataReader["NavigateUrl"]);
			view.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

			return view;
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

		public String PermissionType
		{
			get {return this._PermissionType;}
			set {this._PermissionType = value;}
		}

		public String PermissionCommand
		{
			get {return this._PermissionCommand;}
			set {this._PermissionCommand = value;}
		}

		public String DisplayName
		{
			get {return this._DisplayName;}
			set {this._DisplayName = value;}
		}

		public String ToolTipText
		{
			get {return this._ToolTipText;}
			set {this._ToolTipText = value;}
		}

		public String ImageUrl
		{
			get {return this._ImageUrl;}
			set {this._ImageUrl = value;}
		}

		public String SeperatorUrl
		{
			get {return this._SeperatorUrl;}
			set {this._SeperatorUrl = value;}
		}

		public String NavigateUrl
		{
			get {return this._NavigateUrl;}
			set {this._NavigateUrl = value;}
		}

		public Int32 SortOrder
		{
			get {return this._SortOrder;}
			set {this._SortOrder = value;}
		}

	}
}
