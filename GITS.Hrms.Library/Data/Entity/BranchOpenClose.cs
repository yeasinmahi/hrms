using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class BranchOpenClose : EntityBase<BranchOpenClose>
    {
        private String _LetterNo;
        private DateTime _LetterDate;
        private Int32 _BranchId;
        private Int32 _Types;
		private DateTime _EffectiveDate;
        private Boolean _IsRecent;

        public BranchOpenClose()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[BranchOpenClose]"; }
		}

        protected override BranchOpenClose Map(SqlDataReader dataReader)
		{
            BranchOpenClose entity = new BranchOpenClose();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
            entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            entity.BranchId = DBUtility.ToInt32(dataReader["BranchId"]);
            entity.Types = DBUtility.ToInt32(dataReader["Types"]);
            entity.Effectivedate = DBUtility.ToDateTime(dataReader["Effectivedate"]);
            entity.IsRecent = DBUtility.ToBoolean(dataReader["IsRecent"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

        
		public String LetterNo
		{
			get {return _LetterNo;}
			set {_LetterNo = value;}
		}
        public DateTime LetterDate
        {
            get { return _LetterDate; }
            set { _LetterDate = value; }
        }
		public Int32 BranchId
		{
			get {return _BranchId;}
			set {_BranchId = value;}
		}
        
        public Int32 Types
        {
            get { return _Types; }
            set { _Types = value; }
        }
        public DateTime Effectivedate
        {
            get { return _EffectiveDate; }
            set { _EffectiveDate = value; }
        }
        public Boolean IsRecent
        {
            get { return _IsRecent; }
            set { _IsRecent = value; }
        }
    }
}
