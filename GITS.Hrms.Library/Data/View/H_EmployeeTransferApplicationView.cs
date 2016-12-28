using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public class H_EmployeeTransferApplicationView : ViewBase<H_EmployeeTransferApplicationView>
    {
        private Int32 _H_EmployeeId;
        private String _EmployeeName;
        private Int32 _EmployeeCode;
        private Statuses _Status;
        private String _ApplicationNo;
        private DateTime _ApplicationDate;

        private DateTime _ReceivingDate;
        private String _DemandedPlace;
        private String _Remarks;

        private String _UserLogin;
        private String _Designation;
        public enum Statuses
        {
            Approved = 1,
            Processing = 2,
            Rejected = 3
        }


        public H_EmployeeTransferApplicationView()
        {

        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override String AbstractName
        {
            get { return "[H_EmployeeTransferApplicationView]"; }
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

        public string EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName = value; }
        }

        public Int32 EmployeeCode
        {
            get { return _EmployeeCode; }
            set { _EmployeeCode = value; }
        }

        public string Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }


        protected override H_EmployeeTransferApplicationView Map(SqlDataReader dataReader)
        {
            H_EmployeeTransferApplicationView entity = new H_EmployeeTransferApplicationView();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.EmployeeName = DBUtility.ToString(dataReader["EmployeeName"]);
            entity.EmployeeCode = DBUtility.ToInt32(dataReader["EmployeeCode"]);
            entity.ApplicationNo = DBUtility.ToString(dataReader["ApplicationNo"]);
            entity.ApplicationDate = DBUtility.ToDateTime(dataReader["ApplicationDate"]);

            entity.ReceivingDate = DBUtility.ToDateTime(dataReader["ReceivingDate"]);
            entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);
            entity.DemandedPlace = DBUtility.ToNullableString(dataReader["DemandedPlace"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.UserLogin = DBUtility.ToString(dataReader["UserLogin"]);
            entity.Designation = DBUtility.ToString(dataReader["Designation"]);

         

            return entity;
        }

       



    }
}
