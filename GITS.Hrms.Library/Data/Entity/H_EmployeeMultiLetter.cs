using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeeMultiLetter : EntityBase<H_EmployeeMultiLetter>
    {
        private Int32 _H_EmployeeId;
        private String _Name;
        private Int32 _Code;
        private String  _LetterNo;
        private DateTime _LetterDate;
        private DateTime _EffectiveDate;
		private Nullable<Int32> _H_EmployeeTransferHistoryId;
		private Nullable<Int32> _H_EmployeePromotionHistoryId;
		private Nullable<Int32> _H_EmployeePenatyId;
		private Nullable<Int32> _H_EmployeeWarningId;
		private Nullable<Int32> _H_EmployeeIncrementHeldupId;
        private Nullable<Int32> _H_EmployeeRejoinId;
        private String _Subject;
        private String _Body;
        private String _Conclution;
        private String _Signatory;
        private String _Designation;
        private String _Duplication;
        private String _Note;

        

        public H_EmployeeMultiLetter()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeeMultiLetter]"; }
		}

        protected override H_EmployeeMultiLetter Map(SqlDataReader dataReader)
		{
            H_EmployeeMultiLetter entity = new H_EmployeeMultiLetter();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.Code = DBUtility.ToInt32(dataReader["Code"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            entity.EffectiveDate = DBUtility.ToDateTime(dataReader["EffectiveDate"]);
            entity.H_EmployeeTransferHistoryId = DBUtility.ToNullableInt32(dataReader["H_EmployeeTransferHistoryId"]);
            entity.H_EmployeePromotionHistoryId = DBUtility.ToNullableInt32(dataReader["H_EmployeePromotionHistoryId"]);
            entity.H_EmployeeIncrementHeldupId = DBUtility.ToNullableInt32(dataReader["H_EmployeeIncrementHeldupId"]);
            entity.H_EmployeePenatyId = DBUtility.ToNullableInt32(dataReader["H_EmployeePenatyId"]);
            entity.H_EmployeeWarningId = DBUtility.ToNullableInt32(dataReader["H_EmployeeWarningId"]);
            entity.H_EmployeeRejoinId = DBUtility.ToNullableInt32(dataReader["H_EmployeeRejoinId"]);
            entity.Subject = DBUtility.ToString(dataReader["Subject"]);
            entity.Body = DBUtility.ToString(dataReader["Body"]);
            entity.Conclution = DBUtility.ToString(dataReader["Conclution"]);
            entity.Signatory = DBUtility.ToString(dataReader["Signatory"]);
            entity.Designation = DBUtility.ToString(dataReader["Designation"]);
            entity.Duplication = DBUtility.ToString(dataReader["Duplication"]);
            entity.Note = DBUtility.ToString(dataReader["Note"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

        

		public Int32 H_EmployeeId
		{
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public Int32 Code
        {
            get { return _Code; }
            set { _Code = value; }
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
        public DateTime EffectiveDate
        {
            get { return _EffectiveDate; }
            set { _EffectiveDate = value; }
        }

        public Nullable<Int32> H_EmployeeTransferHistoryId
        {
            get { return _H_EmployeeTransferHistoryId; }
            set { _H_EmployeeTransferHistoryId = value; }
        }
        public Nullable<Int32> H_EmployeePromotionHistoryId
        {
            get { return _H_EmployeePromotionHistoryId; }
            set { _H_EmployeePromotionHistoryId = value; }
        }
        public Nullable<Int32> H_EmployeePenatyId
        {
            get { return _H_EmployeePenatyId; }
            set { _H_EmployeePenatyId = value; }
        }
        public Nullable<Int32> H_EmployeeWarningId
        {
            get { return _H_EmployeeWarningId; }
            set { _H_EmployeeWarningId = value; }
        }
        public Nullable<Int32> H_EmployeeIncrementHeldupId
        {
            get { return _H_EmployeeIncrementHeldupId; }
            set { _H_EmployeeIncrementHeldupId = value; }
        }
        public Nullable<Int32> H_EmployeeRejoinId
        {
            get { return _H_EmployeeRejoinId; }
            set { _H_EmployeeRejoinId = value; }
        }


        public String Subject
		{
            get { return _Subject; }
            set { _Subject = value; }
		}
        public String Body
        {
            get { return _Body; }
            set { _Body = value; }
        }
        public String Conclution
        {
            get { return _Conclution; }
            set { _Conclution = value; }
        }
        public String Signatory
        {
            get { return _Signatory; }
            set { _Signatory = value; }
        }
        public String Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }
        public String Duplication
        {
            get { return _Duplication; }
            set { _Duplication = value; }
        }
        public String Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
    }
}
