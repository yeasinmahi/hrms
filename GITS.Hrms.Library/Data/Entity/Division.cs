using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class Division : EntityBase<Division>
    {
		private String _Name;

        public Division()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[Division]"; }
		}

        protected override Division Map(SqlDataReader dataReader)
		{
            Division entity = new Division();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}
		
		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}
        
    }
}
