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
            get { return this._H_EmployeeId; }
            set { this._H_EmployeeId = value; }
        }
        public Int32 P_LoanId
        {
            get { return this._P_LoanId; }
            set { this._P_LoanId = value; }
        }
        public DateTime DisbursDate
        {
            get { return this._DisbursDate; }
            set { this._DisbursDate = value; }
        }

        public Double LoanAmount
        {
            get { return this._LoanAmount; }
            set { this._LoanAmount = value; }
        }
        public Double InterestRate
        {
            get { return this._InterestRate; }
            set { this._InterestRate = value; }
        }
        public Double InterestAmount
        {
            get { return this._InterestAmount; }
            set { this._InterestAmount = value; }
        }
        public Double TotalAmount
        {
            get { return this._TotalAmount; }
            set { this._TotalAmount = value; }
        }
        public Int32 NumberOfInstallment
        {
            get { return this._NumberOfInstallment; }
            set { this._NumberOfInstallment = value; }
        }
        public Nullable<Int32> PaidInstallment
        {
            get { return this._PaidInstallment; }
            set { this._PaidInstallment = value; }
        }
        public Double InstallmentAmount
        {
            get { return this._InstallmentAmount; }
            set { this._InstallmentAmount = value; }
        }
        
        public Nullable<Double> PaidAmount
        {
            get { return this._PaidAmount; }
            set { this._PaidAmount = value; }
        }
        
        
        public Nullable<DateTime> LastPaidDate
        {
            get { return this._LastPaidDate; }
            set { this._LastPaidDate = value; }
        }
               
        public Statuses Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        
    }
}
