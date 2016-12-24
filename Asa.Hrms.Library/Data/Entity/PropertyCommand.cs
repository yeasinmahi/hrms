using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class PropertyCommand : EntityBase<PropertyCommand>
	{
		private String _PropertyName;
		private String _CommandName;
		private Int32 _SortOrder;

        private String _DisplayName;

		public PropertyCommand()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[PropertyCommand]"; }
		}

		protected override PropertyCommand Map(SqlDataReader dataReader)
		{
			PropertyCommand entity = new PropertyCommand();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.PropertyName = DBUtility.ToString(dataReader["PropertyName"]);
			entity.CommandName = DBUtility.ToString(dataReader["CommandName"]);
			entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

			return entity;
		}

		public static PropertyCommand GetByPropertyNameAndCommandName(String propertyName, String commandName)
		{
			return Get("[PropertyName] = '" + propertyName + "' AND [CommandName] = '" + commandName + "'");
		}

		public static PropertyCommand GetByPropertyNameAndCommandName(TransactionManager transactionManager, String propertyName, String commandName)
		{
			return Get(transactionManager, "[PropertyName] = '" + propertyName + "' AND [CommandName] = '" + commandName + "'");
		}

		public static IList<PropertyCommand> FindByCommandName(String commandName)
		{
			return Find("[CommandName] = '" + commandName + "'", "");
		}

		public static IList<PropertyCommand> FindByCommandName(TransactionManager transactionManager, String commandName)
		{
			return Find(transactionManager, "[CommandName] = '" + commandName + "'", "");
		}

		public static IList<PropertyCommand> FindByPropertyName(String propertyName)
		{
			return Find("[PropertyName] = '" + propertyName + "'", "");
		}

		public static IList<PropertyCommand> FindByPropertyName(TransactionManager transactionManager, String propertyName)
		{
			return Find(transactionManager, "[PropertyName] = '" + propertyName + "'", "");
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

		public Int32 SortOrder
		{
			get {return this._SortOrder;}
			set {this._SortOrder = value;}
		}

        [Property(PropertyAttribute.Attributes.NonTable)]
        public String DisplayName
        {
            get { return this._DisplayName; }
            set { this._DisplayName = value; }
        }

	}
}
