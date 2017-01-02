using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class Organization : EntityBase<Organization>
    {
        private String _Name;


        public Organization()
        {

        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override String AbstractName
        {
            get { return "[Organization]"; }
        }

        protected override Organization Map(SqlDataReader dataReader)
        {
            Organization entity = new Organization();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);


            entity.EntityState = EntityStates.Clean;

            return entity;
        }

        public static Organization GetByName(String name)
        {
            return Get("[Name] = '" + name + "'");
        }

        public static Organization GetByName(TransactionManager transactionManager, String name)
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