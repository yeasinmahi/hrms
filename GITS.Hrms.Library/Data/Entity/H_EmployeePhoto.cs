using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeePhoto : EntityBase<H_EmployeePhoto>
    {
        private Int32 _H_EmployeeId;
        private Byte[] _Photo;

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[H_EmployeePhoto]"; }
        }
        protected override H_EmployeePhoto Map(SqlDataReader dataReader)
        {
            H_EmployeePhoto entity = new H_EmployeePhoto();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Photo = dataReader["Photo"] == DBNull.Value ? null : (Byte[])dataReader["Photo"];
            
            entity.EntityState = EntityStates.Clean;

            return entity;
        }
        public static H_EmployeePhoto GetByH_EmployeeId(Int32 h_EmployeeId)
        {
            return Get("[H_EmployeeId] = " + h_EmployeeId);
        }
        public Int32 H_EmployeeId
        {
            get { return _H_EmployeeId; }
            set { _H_EmployeeId = value; }
        }
        public Byte[] Photo
        {
            get { return _Photo; }
            set { _Photo = value; }
        }
    }
}
