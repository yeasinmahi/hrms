using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeeTransferApplication : EntityBase<H_EmployeeTransferApplication>
    {
        
        private Int32 _H_EmployeeId;
        private Statuses _Status;
        private String _ApplicationNo;
        private DateTime _ApplicationDate;
       
        private DateTime _ReceivingDate;
        private String _DemandedPlace;
        private String _Remarks;
        private Int32 _H_DesignationId;
        private String _UserLogin;

        public enum Statuses
        {
            Select_Status=0,
            Approved = 1,
            Processing = 2,
            Rejected = 3
        }


        public H_EmployeeTransferApplication()
        {

        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override String AbstractName
        {
            get { return "[H_EmployeeTransferApplication]"; }
        }
     

        public int H_EmployeeId
        {
            get { return _H_EmployeeId; }
            set { _H_EmployeeId = value; }
        }

        public Statuses Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public String ApplicationNo
        {
            get { return _ApplicationNo; }
            set { _ApplicationNo = value; }
        }

        public DateTime ApplicationDate
        {
            get { return _ApplicationDate; }
            set { _ApplicationDate = value; }
        }

        public DateTime ReceivingDate
        {
            get { return _ReceivingDate; }
            set { _ReceivingDate = value; }
        }

        public string DemandedPlace
        {
            get { return _DemandedPlace; }
            set { _DemandedPlace = value; }
        }

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        public string UserLogin
        {
            get { return _UserLogin; }
            set { _UserLogin = value; }
        }

        public int H_DesignationId
        {
            get { return _H_DesignationId; }
            set { _H_DesignationId = value; }
        }


        protected override H_EmployeeTransferApplication Map(SqlDataReader dataReader)
        {
            H_EmployeeTransferApplication entity = new H_EmployeeTransferApplication();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.H_DesignationId = DBUtility.ToInt32(dataReader["H_DesignationId"]);
            entity.ApplicationNo = DBUtility.ToString(dataReader["ApplicationNo"]);
            entity.ApplicationDate = DBUtility.ToDateTime(dataReader["ApplicationDate"]);

            entity.ReceivingDate = DBUtility.ToDateTime(dataReader["ReceivingDate"]);
            entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);
            entity.DemandedPlace = DBUtility.ToNullableString(dataReader["DemandedPlace"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.UserLogin = DBUtility.ToString(dataReader["UserLogin"]);


            entity.EntityState = EntityStates.Clean;

            return entity;
        }

       



    }
}
