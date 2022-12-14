using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class RoleProperty : EntityBase<RoleProperty>
	{
		private String _RoleName;
		private String _PropertyName;

		public RoleProperty()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[RoleProperty]"; }
		}

		protected override RoleProperty Map(SqlDataReader dataReader)
		{
			RoleProperty entity = new RoleProperty();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.RoleName = DBUtility.ToString(dataReader["RoleName"]);
			entity.PropertyName = DBUtility.ToString(dataReader["PropertyName"]);

			return entity;
		}

		public static RoleProperty GetByRoleNameAndPropertyName(String roleName, String propertyName)
		{
			return Get("[RoleName] = '" + roleName + "' AND [PropertyName] = '" + propertyName + "'");
		}

		public static RoleProperty GetByRoleNameAndPropertyName(TransactionManager transactionManager, String roleName, String propertyName)
		{
			return Get(transactionManager, "[RoleName] = '" + roleName + "' AND [PropertyName] = '" + propertyName + "'");
		}

		public static IList<RoleProperty> FindByPropertyName(String propertyName)
		{
			return Find("[PropertyName] = '" + propertyName + "'", "");
		}

		public static IList<RoleProperty> FindByPropertyName(TransactionManager transactionManager, String propertyName)
		{
			return Find(transactionManager, "[PropertyName] = '" + propertyName + "'", "");
		}

		public static IList<RoleProperty> FindByRoleName(String roleName)
		{
			return Find("[RoleName] = '" + roleName + "'", "");
		}

		public static IList<RoleProperty> FindByRoleName(TransactionManager transactionManager, String roleName)
		{
			return Find(transactionManager, "[RoleName] = '" + roleName + "'", "");
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

	}
}
