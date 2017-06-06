using System;
using System.Collections.Generic;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_FileUpload : EntityBase<H_FileUpload>
    {
        private Int32 _H_EmployeeId;
        private String _Title;
        private String _FileName;
        private DateTime _UploadDate;

        public H_FileUpload()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "H_FileUpload"; }
        }

        protected override H_FileUpload Map(System.Data.SqlClient.SqlDataReader dataReader)
        {
            H_FileUpload entity = new H_FileUpload();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Title = DBUtility.ToString(dataReader["Title"]);
            entity.FileName = DBUtility.ToString(dataReader["FileName"]);
            entity.UploadDate = DBUtility.ToDateTime(dataReader["UploadDate"]);
            
            entity.EntityState = EntityStates.Clean;

            return entity;
        }
        public static IList<H_FileUpload> GetByH_EmployeeId(Int32 h_EmployeeId)
        {
            return Find("[H_EmployeeId] = " + h_EmployeeId,"");
        }
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public String FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        public Int32 H_EmployeeId
        {
            get { return _H_EmployeeId; }
            set { _H_EmployeeId = value; }
        }
        public DateTime UploadDate
        {
            get { return _UploadDate; }
            set { _UploadDate = value; }
        }
    }
}
