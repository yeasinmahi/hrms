using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class User : EntityBase<User>
	{
		private String _Login;
		private String _Password;
		private String _Name;
		private Boolean _IsActive;
        private Boolean _IsReset;
        private Nullable<DateTime> _LastResetDate;
        private UserTypes _UserType;

        public enum UserTypes
        {
            HO_User = 1,
            Field_User = 2
        }

		public User()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[User]"; }
		}

		protected override User Map(SqlDataReader dataReader)
		{
			User entity = new User();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Login = DBUtility.ToString(dataReader["Login"]);
			entity.Password = DBUtility.ToString(dataReader["Password"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
			entity.IsActive = DBUtility.ToBoolean(dataReader["IsActive"]);
            entity.IsReset = DBUtility.ToBoolean(dataReader["IsReset"]);
            entity.LastResetDate = DBUtility.ToNullableDateTime(dataReader["LastResetDate"]);
            entity.UserType = (UserTypes)DBUtility.ToInt32(dataReader["UserType"]);
			return entity;
		}

		public static User GetByLogin(String login)
		{
			return Get("[Login] = '" + login + "'");
		}

		public static User GetByLogin(TransactionManager transactionManager, String login)
		{
			return Get(transactionManager, "[Login] = '" + login + "'");
		}

		public String Login
		{
			get {return this._Login;}
			set {this._Login = value;}
		}

		public String Password
		{
			get {return this._Password;}
			set {this._Password = value;}
		}

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}

		public Boolean IsActive
		{
			get {return this._IsActive;}
			set {this._IsActive = value;}
		}
        public Boolean IsReset
        {
            get { return this._IsReset; }
            set { this._IsReset = value; }
        }
        public Nullable<DateTime> LastResetDate
        {
            get { return this._LastResetDate; }
            set { this._LastResetDate = value; }
        }
        public UserTypes UserType
        {
            get { return this._UserType; }
            set { this._UserType = value; }
        }
	}
}
