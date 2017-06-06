using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class P_LoanAccount : EntityBase<P_LoanAccount>
    {
        private Int32 _H_EmployeeId;
        private Int32 _P_LoanId;
        private DateTime _DisbursDate;
        private Double _LoanAmount;
        private Double _InterestRate;
        private Double _InterestAmount;
        private Double _TotalAmount;
        private Int32 _NumberOfInstallment;
        private Double _InstallmentAmount;
        private Nullable<Double> _PaidAmount;
        private Nullable<Int32> _PaidInstallment;
        private Nullable<DateTime> _LastPaidDate;
        private Statuses _Status;


        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
        public P_LoanAccount()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_LoanAccount]"; }
		}
        protected override P_LoanAccount Map(SqlDataReader dataReader)
        {
            P_LoanAccount entity = new P_LoanAccount();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.P_LoanId = DBUtility.ToInt32(dataReader["P_LoanId"]);
            entity.DisbursDate = DBUtility.ToDateTime(dataReader["DisbursDate"]);
            entity.LoanAmount = DBUtility.ToDouble(dataReader["LoanAmount"]);
            entity.InterestRate = DBUtility.ToDouble(dataReader["InterestRate"]);
            entity.InterestAmount = DBUtility.ToDouble(dataReader["InterestAmount"]);
            entity.TotalAmount = DBUtility.ToDouble(dataReader["TotalAmount"]);
            entity.NumberOfInstallment = DBUtility.ToInt32(dataReader["NumberOfInstallment"]);
            entity.InstallmentAmount = DBUtility.ToDouble(dataReader["InstallmentAmount"]);
            entity.PaidAmount = DBUtility.ToNullableDouble(dataReader["PaidAmount"]);
            entity.PaidInstallment = DBUtility.ToNullableInt32(dataReader["PaidInstallment"]);
            entity.LastPaidDate = DBUtility.ToNullableDateTime(dataReader["LastPaidDate"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.EntityState = EntityStates.Clean;

            return entity;
        }

        public Int32 H_EmployeeId
        {
            get { return _H_EmployeeId; }
            set { _H_EmployeeId = value; }
        }
        public Int32 P_LoanId
        {
            get { return _P_LoanId; }
            set { _P_LoanId = value; }
        }
        public DateTime DisbursDate
        {
            get { return _DisbursDate; }
            set { _DisbursDate = value; }
        }

        public Double LoanAmount
        {
            get { return _LoanAmount; }
            set { _LoanAmount = value; }
        }
        public Double InterestRate
        {
            get { return _InterestRate; }
            set { _InterestRate = value; }
        }
        public Double InterestAmount
        {
            get { return _InterestAmount; }
            set { _InterestAmount = value; }
        }
        public Double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        public Int32 NumberOfInstallment
        {
            get { return _NumberOfInstallment; }
            set { _NumberOfInstallment = value; }
        }
        public Nullable<Int32> PaidInstallment
        {
            get { return _PaidInstallment; }
            set { _PaidInstallment = value; }
        }
        public Double InstallmentAmount
        {
            get { return _InstallmentAmount; }
            set { _InstallmentAmount = value; }
        }
        
        public Nullable<Double> PaidAmount
        {
            get { return _PaidAmount; }
            set { _PaidAmount = value; }
        }
        
        
        public Nullable<DateTime> LastPaidDate
        {
            get { return _LastPaidDate; }
            set { _LastPaidDate = value; }
        }
               
        public Statuses Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        
    }
}
