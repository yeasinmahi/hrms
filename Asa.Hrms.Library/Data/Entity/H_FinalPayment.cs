﻿using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_FinalPayment : EntityBase<H_FinalPayment>
    {
        private Int32 _H_EmployeeId;
        private String _LetterNo;
        private DateTime _LetterDate;
        private DateTime _ClosingDate;
        private Double _NetPay;
		

        public H_FinalPayment()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_FinalPayment]"; }
		}

        protected override H_FinalPayment Map(SqlDataReader dataReader)
		{
            H_FinalPayment entity = new H_FinalPayment();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
            entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            entity.ClosingDate = DBUtility.ToDateTime(dataReader["ClosingDate"]);
            entity.NetPay = DBUtility.ToDouble(dataReader["NetPay"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}
        public Int32 H_EmployeeId
        {
            get { return this._H_EmployeeId; }
            set { this._H_EmployeeId = value; }
        }
        
		public String LetterNo
		{
			get {return this._LetterNo;}
			set {this._LetterNo = value;}
		}
        public DateTime LetterDate
        {
            get { return this._LetterDate; }
            set { this._LetterDate = value; }
        }
		
        public Double NetPay
        {
            get { return this._NetPay; }
            set { this._NetPay = value; }
        }
        public DateTime ClosingDate
        {
            get { return this._ClosingDate; }
            set { this._ClosingDate = value; }
        }
    }
}
