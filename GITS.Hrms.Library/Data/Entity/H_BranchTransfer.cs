using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_BranchTransfer : EntityBase<H_BranchTransfer>
    {
        private String _LetterNo;
        private DateTime _LetterDate;
        private Int32 _BranchId;
        private Int32 _SourceRegionId;
        private Int32 _DestinationRegionId;
		private DateTime _TransferDate;

        public H_BranchTransfer()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_BranchTransfer]"; }
		}

        protected override H_BranchTransfer Map(SqlDataReader dataReader)
		{
            H_BranchTransfer entity = new H_BranchTransfer();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
            entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            entity.BranchId = DBUtility.ToInt32(dataReader["BranchId"]);
            entity.SourceRegionId = DBUtility.ToInt32(dataReader["SourceRegionId"]);
            entity.DestinationRegionId = DBUtility.ToInt32(dataReader["DestinationRegionId"]);
            entity.TransferDate = DBUtility.ToDateTime(dataReader["TransferDate"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
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
		public Int32 BranchId
		{
			get {return this._BranchId;}
			set {this._BranchId = value;}
		}
        public Int32 SourceRegionId
        {
            get { return this._SourceRegionId; }
            set { this._SourceRegionId = value; }
        }
        public Int32 DestinationRegionId
        {
            get { return this._DestinationRegionId; }
            set { this._DestinationRegionId = value; }
        }
        public DateTime TransferDate
        {
            get { return this._TransferDate; }
            set { this._TransferDate = value; }
        }
    }
}
