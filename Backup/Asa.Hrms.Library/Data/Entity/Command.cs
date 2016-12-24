using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class Command : EntityBase<Command>
	{
		private String _Name;
		private String _DisplayName;
		private String _PermissionType;
		private String _PermissionCommand;
		private String _ToolTipText;
		private String _ImageUrl;
		private String _SeperatorUrl;
		private String _NavigateUrl;
		private Int32 _SortOrder;

		public Command()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[Command]"; }
		}

		protected override Command Map(SqlDataReader dataReader)
		{
			Command entity = new Command();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
			entity.DisplayName = DBUtility.ToString(dataReader["DisplayName"]);
			entity.PermissionType = DBUtility.ToString(dataReader["PermissionType"]);
			entity.PermissionCommand = DBUtility.ToNullableString(dataReader["PermissionCommand"]);
			entity.ToolTipText = DBUtility.ToNullableString(dataReader["ToolTipText"]);
			entity.ImageUrl = DBUtility.ToNullableString(dataReader["ImageUrl"]);
			entity.SeperatorUrl = DBUtility.ToNullableString(dataReader["SeperatorUrl"]);
			entity.NavigateUrl = DBUtility.ToNullableString(dataReader["NavigateUrl"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static Command GetByName(String name)
		{
			return Get("[Name] = '" + name + "'");
		}

		public static Command GetByName(TransactionManager transactionManager, String name)
		{
			return Get(transactionManager, "[Name] = '" + name + "'");
		}

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}

		public String DisplayName
		{
			get {return this._DisplayName;}
			set {this._DisplayName = value;}
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
