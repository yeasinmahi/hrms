using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
   public class P_EmployeeDeduction : EntityBase<P_EmployeeDeduction>
    {
        private Int32 _H_EmployeeId;
        private Int32 _P_DeductionId;
        private Double _Amount;


        public P_EmployeeDeduction()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_EmployeeDeduction]"; }
		}
        protected override P_EmployeeDeduction Map(SqlDataReader dataReader)
        {
            P_EmployeeDeduction entity = new P_EmployeeDeduction();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.P_DeductionId = DBUtility.ToInt32(dataReader["P_DeductionId"]);
            entity.Amount = DBUtility.ToDouble(dataReader["Amount"]);

            entity.EntityState = EntityStates.Clean;

            return entity;
        }
        public static IList<P_EmployeeDeduction> FindByEmployeeId(Int32 employeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = " + employeeId  , sortColumns);
        }
        public static IList<P_EmployeeDeduction> FindByEmployeeIdAndSalaryType(Int32 employeeId, int salaryType, String sortColumns)
        {
            return Find("[H_EmployeeId] = " + employeeId+" AND SalaryType="+salaryType, sortColumns);
        }
        public Int32 H_EmployeeId
        {
            get { return this._H_EmployeeId; }
            set { this._H_EmployeeId = value; }
        }
        public Int32 P_DeductionId
        {
            get { return this._P_DeductionId; }
            set { this._P_DeductionId = value; }
        }

        public Double Amount
        {
            get { return this._Amount; }
            set { this._Amount = value; }
        }

    }
}
