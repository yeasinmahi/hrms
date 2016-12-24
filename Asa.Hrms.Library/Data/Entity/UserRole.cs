using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class UserRole : EntityBase<UserRole>
	{
		private String _UserLogin;
		private String _RoleName;

		public UserRole()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[UserRole]"; }
		}

		protected override UserRole Map(SqlDataReader dataReader)
		{
			UserRole entity = new UserRole();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.UserLogin = DBUtility.ToString(dataReader["UserLogin"]);
			entity.RoleName = DBUtility.ToString(dataReader["RoleName"]);

			return entity;
		}

		public static UserRole GetByUserLoginAndRoleName(String userLogin, String roleName)
		{
			return Get("[UserLogin] = '" + userLogin + "' AND [RoleName] = '" + roleName + "'");
		}

		public static UserRole GetByUserLoginAndRoleName(TransactionManager transactionManager, String userLogin, String roleName)
		{
			return Get(transactionManager, "[UserLogin] = '" + userLogin + "' AND [RoleName] = '" + roleName + "'");
		}

        public static IList<UserRole> FindByRoleName(String roleName, String sortColumns)
		{
            return Find("[RoleName] = '" + roleName + "'", sortColumns);
		}

        public static IList<UserRole> FindByRoleName(TransactionManager transactionManager, String roleName, String sortColumns)
		{
            return Find(transactionManager, "[RoleName] = '" + roleName + "'", sortColumns);
		}

        public static IList<UserRole> FindByUserLogin(String userLogin, String sortColumns)
		{
            return Find("[UserLogin] = '" + userLogin + "'", sortColumns);
		}

        public static IList<UserRole> FindByUserLogin(TransactionManager transactionManager, String userLogin, String sortColumns)
		{
            return Find(transactionManager, "[UserLogin] = '" + userLogin + "'", sortColumns);
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

	}
}
