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
			get {return _PropertyName;}
			set {_PropertyName = value;}
		}

		public String CommandName
		{
			get {return _CommandName;}
			set {_CommandName = value;}
		}

		public String PermissionType
		{
			get {return _PermissionType;}
			set {_PermissionType = value;}
		}

		public String PermissionCommand
		{
			get {return _PermissionCommand;}
			set {_PermissionCommand = value;}
		}

		public String DisplayName
		{
			get {return _DisplayName;}
			set {_DisplayName = value;}
		}

		public String ToolTipText
		{
			get {return _ToolTipText;}
			set {_ToolTipText = value;}
		}

		public String ImageUrl
		{
			get {return _ImageUrl;}
			set {_ImageUrl = value;}
		}

		public String SeperatorUrl
		{
			get {return _SeperatorUrl;}
			set {_SeperatorUrl = value;}
		}

		public String NavigateUrl
		{
			get {return _NavigateUrl;}
			set {_NavigateUrl = value;}
		}

		public Int32 SortOrder
		{
			get {return _SortOrder;}
			set {_SortOrder = value;}
		}

	}
}
