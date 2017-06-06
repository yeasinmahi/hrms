using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
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
			get {return _Login;}
			set {_Login = value;}
		}

		public String Password
		{
			get {return _Password;}
			set {_Password = value;}
		}

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

		public Boolean IsActive
		{
			get {return _IsActive;}
			set {_IsActive = value;}
		}
        public Boolean IsReset
        {
            get { return _IsReset; }
            set { _IsReset = value; }
        }
        public Nullable<DateTime> LastResetDate
        {
            get { return _LastResetDate; }
            set { _LastResetDate = value; }
        }
        public UserTypes UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }
	}
}
