using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class RoleCommand : EntityBase<RoleCommand>
	{
		private String _RoleName;
		private String _PropertyName;
		private String _CommandName;

		public RoleCommand()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[RoleCommand]"; }
		}

		protected override RoleCommand Map(SqlDataReader dataReader)
		{
			RoleCommand entity = new RoleCommand();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.RoleName = DBUtility.ToString(dataReader["RoleName"]);
			entity.PropertyName = DBUtility.ToString(dataReader["PropertyName"]);
			entity.CommandName = DBUtility.ToString(dataReader["CommandName"]);

			return entity;
		}

		public static RoleCommand GetByRoleNameAndPropertyNameAndCommandName(String roleName, String propertyName, String commandName)
		{
			return Get("[RoleName] = '" + roleName + "' AND [PropertyName] = '" + propertyName + "' AND [CommandName] = '" + commandName + "'");
		}

		public static RoleCommand GetByRoleNameAndPropertyNameAndCommandName(TransactionManager transactionManager, String roleName, String propertyName, String commandName)
		{
			return Get(transactionManager, "[RoleName] = '" + roleName + "' AND [PropertyName] = '" + propertyName + "' AND [CommandName] = '" + commandName + "'");
		}

		public static IList<RoleCommand> FindByCommandName(String commandName)
		{
			return Find("[CommandName] = '" + commandName + "'", "");
		}

		public static IList<RoleCommand> FindByCommandName(TransactionManager transactionManager, String commandName)
		{
			return Find(transactionManager, "[CommandName] = '" + commandName + "'", "");
		}

		public static IList<RoleCommand> FindByPropertyName(String propertyName)
		{
			return Find("[PropertyName] = '" + propertyName + "'", "");
		}

		public static IList<RoleCommand> FindByPropertyName(TransactionManager transactionManager, String propertyName)
		{
			return Find(transactionManager, "[PropertyName] = '" + propertyName + "'", "");
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
