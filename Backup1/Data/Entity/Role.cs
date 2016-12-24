using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class Role : EntityBase<Role>
	{
		private String _Name;

		public Role()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[Role]"; }
		}

		protected override Role Map(SqlDataReader dataReader)
		{
			Role entity = new Role();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);

			return entity;
		}

		public static Role GetByName(String name)
		{
			return Get("[Name] = '" + name + "'");
		}

		public static Role GetByName(TransactionManager transactionManager, String name)
		{
			return Get(transactionManager, "[Name] = '" + name + "'");
		}

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}

	}
}
