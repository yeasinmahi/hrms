using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class WF_WelfareFundAssistance : EntityBase<WF_WelfareFundAssistance>
    {
        private Int32 _H_EmployeeId;
        private Int32 _BranchId;
        private Int32 _WF_DiseasesId;
        private String _LetterNo;
        private DateTime _LetterDate;
        private Double _Amount;
        private String _Remarks;
        private Int32 _FundType;
		
        
        public WF_WelfareFundAssistance()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[WF_WelfareFundAssistance]"; }
		}

        protected override WF_WelfareFundAssistance Map(SqlDataReader dataReader)
		{
            WF_WelfareFundAssistance entity = new WF_WelfareFundAssistance();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.BranchId = DBUtility.ToInt32(dataReader["BranchId"]);
            entity.WF_DiseasesId = DBUtility.ToInt32(dataReader["WF_DiseasesId"]);
            entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
            entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);     
            entity.Amount = DBUtility.ToDouble(dataReader["Amount"]);
            entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);
            entity.FundType = DBUtility.ToInt32(dataReader["FundType"]);
            
			entity.EntityState = EntityStates.Clean;

			return entity;
		}
        public static IList<WF_WelfareFundAssistance> FindByEmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<WF_WelfareFundAssistance> FindByEmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
        {
            return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }
        public Int32 H_EmployeeId
        {
            get { return this._H_EmployeeId; }
            set { this._H_EmployeeId = value; }
        }

        public Int32 BranchId
        {
            get { return this._BranchId; }
            set { this._BranchId = value; }
        }
        public Int32 WF_DiseasesId
        {
            get { return this._WF_DiseasesId; }
            set { this._WF_DiseasesId = value; }
        }
        public String LetterNo
        {
            get { return this._LetterNo; }
            set { this._LetterNo = value; }
        }
        public DateTime LetterDate
        {
            get { return this._LetterDate; }
            set { this._LetterDate = value; }
        }
        public Double Amount
        {
            get { return this._Amount; }
            set { this._Amount = value; }
        }
		public String Remarks
		{
			get {return this._Remarks;}
			set {this._Remarks = value;}
		}
        public Int32 FundType
        {
            get { return this._FundType; }
            set { this._FundType = value; }
        }
		
    }
}
