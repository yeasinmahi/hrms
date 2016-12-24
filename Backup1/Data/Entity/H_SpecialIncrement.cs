using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_SpecialIncrement : EntityBase<H_SpecialIncrement>
    {
        private Int32 _H_EmployeeId;
		private String _LetterNo;
		private DateTime _LetterDate;
        private DateTime _EffectiveDate;
		private Int32 _NumberOfIncrement;
        private String _Remarks;

        public H_SpecialIncrement()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_SpecialIncrement]"; }
		}

        protected override H_SpecialIncrement Map(SqlDataReader dataReader)
		{
            H_SpecialIncrement entity = new H_SpecialIncrement();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            entity.NumberOfIncrement = DBUtility.ToInt32(dataReader["NumberOfIncrement"]);
            entity.EffectiveDate = DBUtility.ToDateTime(dataReader["EffectiveDate"]);
			
            entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_SpecialIncrement> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

        public static IList<H_SpecialIncrement> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
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

        public Int32 NumberOfIncrement
		{
            get { return this._NumberOfIncrement; }
            set { this._NumberOfIncrement = value; }
		}

        public DateTime EffectiveDate
		{
            get { return this._EffectiveDate; }
            set { this._EffectiveDate = value; }
		}

		
        public String Remarks
        {
            get { return this._Remarks; }
            set { this._Remarks = value; }
        }
    }
}
