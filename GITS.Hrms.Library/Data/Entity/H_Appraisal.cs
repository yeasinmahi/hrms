using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_Appraisal : EntityBase<H_Appraisal>
    {
		private String _Name;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private Boolean _Status;

        public H_Appraisal()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_Appraisal]"; }
		}

        protected override H_Appraisal Map(SqlDataReader dataReader)
		{
            H_Appraisal entity = new H_Appraisal();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
            entity.EndDate = DBUtility.ToDateTime(dataReader["EndDate"]);
            entity.Status = DBUtility.ToBoolean(dataReader["Status"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public String Name
		{
            get { return _Name; }
            set { _Name = value; }
		}
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        public Boolean Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

    }
}
