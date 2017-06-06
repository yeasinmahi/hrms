using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class Property : EntityBase<Property>
	{
		private String _ModuleName;
		private String _Name;
		private String _DisplayName;
		private String _PermissionType;
		private String _PermissionProperty;
		private String _ImageUrl;
		private String _Path;
		private Boolean _IsActive;
		private Int32 _SortOrder;

		public Property()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[Property]"; }
		}

		protected override Property Map(SqlDataReader dataReader)
		{
			Property entity = new Property();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.ModuleName = DBUtility.ToString(dataReader["ModuleName"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
			entity.DisplayName = DBUtility.ToString(dataReader["DisplayName"]);
			entity.PermissionType = DBUtility.ToString(dataReader["PermissionType"]);
			entity.PermissionProperty = DBUtility.ToNullableString(dataReader["PermissionProperty"]);
			entity.ImageUrl = DBUtility.ToNullableString(dataReader["ImageUrl"]);
			entity.Path = DBUtility.ToNullableString(dataReader["Path"]);
			entity.IsActive = DBUtility.ToBoolean(dataReader["IsActive"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

			return entity;
		}

		public static Property GetByName(String name)
		{
			return Get("[Name] = '" + name + "'");
		}

		public static Property GetByName(TransactionManager transactionManager, String name)
		{
			return Get(transactionManager, "[Name] = '" + name + "'");
		}

		public static IList<Property> FindByModuleName(String moduleName)
		{
			return Find("[ModuleName] = '" + moduleName + "'", "");
		}

		public static IList<Property> FindByModuleName(TransactionManager transactionManager, String moduleName)
		{
			return Find(transactionManager, "[ModuleName] = '" + moduleName + "'", "");
		}

		public static IList<Property> FindByPermissionProperty(String permissionProperty)
		{
			return Find("[PermissionProperty] = '" + permissionProperty + "'", "");
		}

		public static IList<Property> FindByPermissionProperty(TransactionManager transactionManager, String permissionProperty)
		{
			return Find(transactionManager, "[PermissionProperty] = '" + permissionProperty + "'", "");
		}

		public String ModuleName
		{
			get {return _ModuleName;}
			set {_ModuleName = value;}
		}

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

		public String DisplayName
		{
			get {return _DisplayName;}
			set {_DisplayName = value;}
		}

		public String PermissionType
		{
			get {return _PermissionType;}
			set {_PermissionType = value;}
		}

		public String PermissionProperty
		{
			get {return _PermissionProperty;}
			set {_PermissionProperty = value;}
		}

		public String ImageUrl
		{
			get {return _ImageUrl;}
			set {_ImageUrl = value;}
		}

		public String Path
		{
			get {return _Path;}
			set {_Path = value;}
		}

		public Boolean IsActive
		{
			get {return _IsActive;}
			set {_IsActive = value;}
		}

		public Int32 SortOrder
		{
			get {return _SortOrder;}
			set {_SortOrder = value;}
		}

	}
}
