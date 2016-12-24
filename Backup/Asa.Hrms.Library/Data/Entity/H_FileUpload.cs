using System;
using Asa.Hrms.Utility;
using System.Collections.Generic;

namespace Asa.Hrms.Data.Entity
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
            get { return this._Title; }
            set { this._Title = value; }
        }
        public String FileName
        {
            get { return this._FileName; }
            set { this._FileName = value; }
        }

        public Int32 H_EmployeeId
        {
            get { return this._H_EmployeeId; }
            set { this._H_EmployeeId = value; }
        }
        public DateTime UploadDate
        {
            get { return this._UploadDate; }
            set { this._UploadDate = value; }
        }
    }
}
