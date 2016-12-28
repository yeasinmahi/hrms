using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class Country : EntityBase<Country>
    {
        private String _Name;
      

        public Country()
        {

        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override String AbstractName
        {
            get { return "[Country]"; }
        }

        protected override Country Map(SqlDataReader dataReader)
        {
            Country entity = new Country();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
          

            entity.EntityState = EntityStates.Clean;

            return entity;
        }

        public static Country GetByName(String name)
        {
            return Get("[Name] = '" + name + "'");
        }

        public static Country GetByName(TransactionManager transactionManager, String name)
        {
            return Get(transactionManager, "[Name] = '" + name + "'");
        }

        public String Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

      

    }
}
