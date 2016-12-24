using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class P_Deduction : EntityBase<P_Deduction>
    {
        private String _Name;
        private Boolean _IsFixed;
        private Statuses _Status;
        private Int32 _SortOrder;

        
        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
        public P_Deduction()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_Deduction]"; }
		}
        protected override P_Deduction Map(SqlDataReader dataReader)
        {
            P_Deduction entity = new P_Deduction();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.IsFixed = DBUtility.ToBoolean(dataReader["IsFixed"]);
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

        public Boolean IsFixed
        {
            get { return this._IsFixed; }
            set { this._IsFixed = value; }
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
