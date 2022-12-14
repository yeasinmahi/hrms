using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_Duplication : EntityBase<H_Duplication>
    {
        private String _Name;
        private Int32 _SortOrder;



        public H_Duplication()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_Duplication]"; }
		}

        protected override H_Duplication Map(SqlDataReader dataReader)
        {
            H_Duplication entity = new H_Duplication();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);

            
            entity.EntityState = EntityStates.Clean;

            return entity;
        }

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public Int32 SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }

    }
}
