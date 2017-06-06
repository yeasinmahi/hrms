using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class WF_Diseases : EntityBase<WF_Diseases>
    {
		private String _Name;
		private Statuses _Status;
		
        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }


        public WF_Diseases()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[WF_Diseases]"; }
		}

        protected override WF_Diseases Map(SqlDataReader dataReader)
		{
            WF_Diseases entity = new WF_Diseases();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);		
			entity.Name = DBUtility.ToString(dataReader["Name"]);       
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

		public Statuses Status
		{
			get {return _Status;}
			set {_Status = value;}
		}

    }
}
