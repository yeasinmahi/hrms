using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class P_Earning : EntityBase<P_Earning>
    {
        private String _Name;
        private Boolean _IsBasic;
        private Boolean _IsFixed;
        private Nullable<Int32> _ParentId;
        private Statuses _Status;
        private Int32 _SortOrder;
        private Boolean _IsPaySlip;

        
        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
        public P_Earning()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_Earning]"; }
		}
        protected override P_Earning Map(SqlDataReader dataReader)
        {
            P_Earning entity = new P_Earning();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.IsBasic = DBUtility.ToBoolean(dataReader["IsBasic"]);
            entity.IsFixed = DBUtility.ToBoolean(dataReader["IsFixed"]);
            entity.ParentId = DBUtility.ToNullableInt32(dataReader["ParentId"]);
            entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.IsPaySlip = DBUtility.ToBoolean(dataReader["IsPaySlip"]);
            entity.EntityState = EntityStates.Clean;

            return entity;
        }

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public Boolean IsBasic
        {
            get { return _IsBasic; }
            set { _IsBasic = value; }
        }

        public Boolean IsFixed
        {
            get { return _IsFixed; }
            set { _IsFixed = value; }
        }
        public Boolean IsPaySlip
        {
            get { return _IsPaySlip; }
            set { _IsPaySlip = value; }
        }
        public Nullable<Int32> ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
       
        public Statuses Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public Int32 SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        public string IsFixedValue
        {
            get { return IsFixed == true ? "Yes" : "No"; }
        }
        [Property(PropertyAttribute.Attributes.NonTable)]
        public string Parent
        {
            get { return ParentId==null? "" : GetById(ParentId.Value).Name; }
        }
    }
}
