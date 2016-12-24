using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;
using System.Drawing;

namespace Asa.Hrms.Data.Entity
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
            get { return this._Name; }
            set { this._Name = value; }
        }

        public Int32 SortOrder
        {
            get { return this._SortOrder; }
            set { this._SortOrder = value; }
        }

    }
}
