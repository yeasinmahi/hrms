using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeePermanentHistory : EntityBase<H_EmployeePermanentHistory>
    {
        private Int32 _H_EmployeeId;
		private String _LetterNo;
		private DateTime _LetterDate;
		private DateTime _PermanentDate;
		private String _Remarks;
        private Boolean _Status;

        public H_EmployeePermanentHistory()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeePermanentHistory]"; }
		}

        protected override H_EmployeePermanentHistory Map(SqlDataReader dataReader)
		{
            H_EmployeePermanentHistory entity = new H_EmployeePermanentHistory();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            entity.PermanentDate = DBUtility.ToDateTime(dataReader["PermanentDate"]);
			entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);
            entity.Status = DBUtility.ToBoolean(dataReader["Status"]);
            entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_EmployeePermanentHistory> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeePermanentHistory> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
        {
            return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }
           

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

		public String LetterNo
		{
			get {return this._LetterNo;}
			set {this._LetterNo = value;}
		}

		public DateTime LetterDate
		{
			get {return this._LetterDate;}
			set {this._LetterDate = value;}
		}


		public DateTime PermanentDate
		{
			get {return this._PermanentDate;}
			set {this._PermanentDate = value;}
		}

		public String Remarks
		{
			get {return this._Remarks;}
			set {this._Remarks = value;}
		}

        public Boolean Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
        
    }
}
