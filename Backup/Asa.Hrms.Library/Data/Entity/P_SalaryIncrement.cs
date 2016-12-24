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
    public class P_SalaryIncrement : EntityBase<P_SalaryIncrement>
    {
        private Int32 _H_EmployeeId;
        private Nullable<DateTime> _LastIncrementDate;
        private Double _LastBasic;
        private Double _PresentBasic;
        private Boolean _Status;

        public P_SalaryIncrement()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_SalaryIncrement]"; }
		}
        protected override P_SalaryIncrement Map(SqlDataReader dataReader)
        {
            P_SalaryIncrement entity = new P_SalaryIncrement();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.LastIncrementDate = DBUtility.ToNullableDateTime(dataReader["JoiningDate"]);
            entity.LastBasic = DBUtility.ToDouble(dataReader["LastBasic"]);
            entity.PresentBasic = DBUtility.ToDouble(dataReader["PresentBasic"]);
            entity.Status = DBUtility.ToBoolean(dataReader["Status"]);
            entity.EntityState = EntityStates.Clean;

            return entity;
        }
        public static P_PayScale GetByGradeId(Int32 gradeId)
        {
            return P_PayScale.Get("H_GradeId=" + gradeId);
        }
        public Int32 H_EmployeeId
        {
            get { return this._H_EmployeeId; }
            set { this._H_EmployeeId = value; }
        }
        
        public Nullable<DateTime> LastIncrementDate
        {
            get { return this._LastIncrementDate; }
            set { this._LastIncrementDate = value; }
        }
        public Double LastBasic
        {
            get { return this._LastBasic; }
            set { this._LastBasic = value; }
        }

        public Double PresentBasic
        {
            get { return this._PresentBasic; }
            set { this._PresentBasic = value; }
        }

        public Boolean Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        [Property(PropertyAttribute.Attributes.NonTable)]
        public String IsActive
        {
            get { return Status ? "Active" : "Inactive"; }
        }
        [Property(PropertyAttribute.Attributes.NonTable)]
        public String Name
        {
            get { return H_Employee.GetById(H_EmployeeId).Name; }
        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        public Int32 Code
        {
            get { return H_Employee.GetById(H_EmployeeId).Code; }
        }
    }
}
