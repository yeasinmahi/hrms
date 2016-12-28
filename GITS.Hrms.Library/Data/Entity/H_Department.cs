using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_Department : EntityBase<H_Department>
	{
		private String _Name;
		private Int32 _SortOrder;

		public H_Department()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_Department]"; }
		}

		protected override H_Department Map(SqlDataReader dataReader)
		{
			H_Department entity = new H_Department();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static H_Department GetByName(String name)
		{
			return Get("[Name] = '" + name + "'");
		}

		public static H_Department GetByName(TransactionManager transactionManager, String name)
		{
			return Get(transactionManager, "[Name] = '" + name + "'");
		}

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}

		public Int32 SortOrder
		{
			get {return this._SortOrder;}
			set {this._SortOrder = value;}
		}

	}
}
