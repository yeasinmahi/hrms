using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class P_EmployeeEarning : EntityBase<P_EmployeeEarning>
    {
        private Int32 _H_EmployeeId;
        private Int32 _P_EarningId;
        private Double _Amount;

        
        public P_EmployeeEarning()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_EmployeeEarning]"; }
		}
        protected override P_EmployeeEarning Map(SqlDataReader dataReader)
        {
            P_EmployeeEarning entity = new P_EmployeeEarning();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.P_EarningId = DBUtility.ToInt32(dataReader["P_EarningId"]);
            entity.Amount = DBUtility.ToDouble(dataReader["Amount"]);

            entity.EntityState = EntityStates.Clean;

            return entity;
        }
        public static IList<P_EmployeeEarning> FindByEmployeeId(Int32 employeeId , String sortColumns)
        {
            return Find("[H_EmployeeId] = " + employeeId  , sortColumns);
        }
        public static IList<P_EmployeeEarning> FindByEmployeeIdAndSalaryType(Int32 employeeId,int salaryType, String sortColumns)
        {
            return Find("[H_EmployeeId] = " + employeeId+" AND SalaryType="+salaryType, sortColumns);
        }
        public Int32 H_EmployeeId
        {
            get { return _H_EmployeeId; }
            set { _H_EmployeeId = value; }
        }
        public Int32 P_EarningId
        {
            get { return _P_EarningId; }
            set { _P_EarningId = value; }
        }

        public Double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

       
        
    }
}
