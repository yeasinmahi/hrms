using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class P_Loan : EntityBase<P_Loan>
    {
        private String _Name;
        private Double _InterestRate;
        private Statuses _Status;
        private Int32 _SortOrder;


        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
        public P_Loan()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_Loan]"; }
		}
        protected override P_Loan Map(SqlDataReader dataReader)
        {
            P_Loan entity = new P_Loan();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.InterestRate = DBUtility.ToDouble(dataReader["InterestRate"]);
            entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.EntityState = EntityStates.Clean;

            return entity;
        }

        public String Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }
        public Double InterestRate
        {
            get { return this._InterestRate; }
            set { this._InterestRate = value; }
        }

               
        public Statuses Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        public Int32 SortOrder
        {
            get { return this._SortOrder; }
            set { this._SortOrder = value; }
        }
    }
}
