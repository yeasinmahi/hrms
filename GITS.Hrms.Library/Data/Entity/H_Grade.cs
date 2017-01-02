using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_Grade : EntityBase<H_Grade>
	{
		private String _Name;
		private Int32 _SortOrder;
        private String _RomanName;

		public H_Grade()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_Grade]"; }
		}

		protected override H_Grade Map(SqlDataReader dataReader)
		{
			H_Grade entity = new H_Grade();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);
            entity.RomanName = DBUtility.ToNullableString(dataReader["RomanName"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static H_Grade GetByName(String name)
		{
			return Get("[Name] = '" + name + "'");
		}

		public static H_Grade GetByName(TransactionManager transactionManager, String name)
		{
			return Get(transactionManager, "[Name] = '" + name + "'");
		}

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}
        public String RomanName
        {
            get { return this._RomanName; }
            set { this._RomanName = value; }
        }
		public Int32 SortOrder
		{
			get {return this._SortOrder;}
			set {this._SortOrder = value;}
		}

	}
}
