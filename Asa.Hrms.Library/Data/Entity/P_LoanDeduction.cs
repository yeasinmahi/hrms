using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class P_LoanDeduction : EntityBase<P_LoanDeduction>
    {
        private Int32 _P_ProcessId;
        private Int32 _P_LoanAccountId;
        private Double _Ammount;
        private DateTime _PaidDate;


        public P_LoanDeduction()
		{

		}
        protected override bool Audit
        {
            get
            {
                return false;
            }
        }
		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_LoanDeduction]"; }
		}
        protected override P_LoanDeduction Map(SqlDataReader dataReader)
        {
            P_LoanDeduction entity = new P_LoanDeduction();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.P_ProcessId = DBUtility.ToInt32(dataReader["P_ProcessId"]);
            entity.P_LoanAccountId = DBUtility.ToInt32(dataReader["P_LoanAccountId"]);
            entity.Ammount = DBUtility.ToDouble(dataReader["Ammount"]);
            entity.PaidDate = DBUtility.ToDateTime(dataReader["PaidDate"]);

            entity.EntityState = EntityStates.Clean;

            return entity;
        }

        public Int32 P_ProcessId
        {
            get { return this._P_ProcessId; }
            set { this._P_ProcessId = value; }
        }
        public Int32 P_LoanAccountId
        {
            get { return this._P_LoanAccountId; }
            set { this._P_LoanAccountId = value; }
        }

        public Double Ammount
        {
            get { return this._Ammount; }
            set { this._Ammount = value; }
        }

        public DateTime PaidDate
        {
            get { return this._PaidDate; }
            set { this._PaidDate = value; }
        }
    }
}
