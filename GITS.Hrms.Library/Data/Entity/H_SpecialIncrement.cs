using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
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
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

		public String LetterNo
		{
			get {return _LetterNo;}
			set {_LetterNo = value;}
		}

		public DateTime LetterDate
		{
			get {return _LetterDate;}
			set {_LetterDate = value;}
		}

        public Int32 NumberOfIncrement
		{
            get { return _NumberOfIncrement; }
            set { _NumberOfIncrement = value; }
		}

        public DateTime EffectiveDate
		{
            get { return _EffectiveDate; }
            set { _EffectiveDate = value; }
		}

		
        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
    }
}
