using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class Module : EntityBase<Module>
	{
		private String _Name;
		private String _DisplayName;
		private String _ImageUrl;
		private Boolean _IsActive;
		private Int32 _SortOrder;

		public Module()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[Module]"; }
		}

		protected override Module Map(SqlDataReader dataReader)
		{
			Module entity = new Module();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
			entity.DisplayName = DBUtility.ToString(dataReader["DisplayName"]);
			entity.ImageUrl = DBUtility.ToNullableString(dataReader["ImageUrl"]);
			entity.IsActive = DBUtility.ToBoolean(dataReader["IsActive"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

			return entity;
		}

		public static Module GetByName(String name)
		{
			return Get("[Name] = '" + name + "'");
		}

		public static Module GetByName(TransactionManager transactionManager, String name)
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

		public String ImageUrl
		{
			get {return this._ImageUrl;}
			set {this._ImageUrl = value;}
		}

		public Boolean IsActive
		{
			get {return this._IsActive;}
			set {this._IsActive = value;}
		}

		public Int32 SortOrder
		{
			get {return this._SortOrder;}
			set {this._SortOrder = value;}
		}

	}
}
