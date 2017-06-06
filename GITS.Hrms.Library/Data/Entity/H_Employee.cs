using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_Employee : EntityBase<H_Employee>
	{
        private Int32 _Code;
		private String _Name;
        private String _NameInBangla;
		private String _FatherName;
		private String _MotherName;
		private Nullable<DateTime> _DateOfBirth;
        private BloodGroups _BloodGroup;
        private Sexes _Sex;
        private MaritalStatuses _MaritalStatus;
        private Religions _Religion;
        //private Byte[] _Photo;
		private Int32 _PermanentAddressId;
		private Int32 _PresentAddressId;
        private Nullable<DateTime> _AppointmentLetterDate;
		private String _AppointmentLetterNo;
        private Nullable<DateTime> _JoiningDate;
        private Nullable<DateTime> _PermanentLetterDate;
        private String _PermanentLetterNo;
        private Nullable<DateTime> _PermanentDate;
        private EmploymentTypes _EmploymentType;
        private Statuses _Status;
        private H_Address _PermanentAddress;
        private H_Address _PresentAddress;
        private Nullable<Int64> _NationalId;

        public enum Sexes
        {
            Male = 1,
            Female = 2
        }

        public enum MaritalStatuses
        {
            Married = 1,
            Unmarried = 2,
            Divorced = 4,
            Abandoned = 8,
            Widow = 16
        }

        public enum EmploymentTypes
        {
            None = 0,
            Apprentice = 1,
            Permanent = 2,
            Contractual = 4,
            Provisional = 5
        }

        public enum Statuses
        {
            Working = 1,
            Consultancy = 2,
            In_Leave = 3,
            Dropped = 4,
            Waiting_For_Posting = 5
        }

        public enum Religions
        {
            Islam = 1,
            Hindu = 2,
            Christian = 4,
            Buddhist = 8,
            Other = 16
        }

        public enum BloodGroups
        {
            A_Positive = 1,
            A_Negative = 2,
            B_Positive = 4,
            B_Negative = 8,
            AB_Positive = 16,
            AB_Negative = 32,
            O_Positive = 64,
            O_Negative = 128
        }

		public H_Employee()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[H_Employee]"; }
		}

		protected override H_Employee Map(SqlDataReader dataReader)
		{
			H_Employee entity = new H_Employee();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.Code = DBUtility.ToInt32(dataReader["Code"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.NameInBangla = DBUtility.ToNullableString(dataReader["NameInBangla"]);
			entity.FatherName = DBUtility.ToNullableString(dataReader["FatherName"]);
			entity.MotherName = DBUtility.ToNullableString(dataReader["MotherName"]);
            entity.DateOfBirth = DBUtility.ToNullableDateTime(dataReader["DateOfBirth"]);
            entity.BloodGroup = (BloodGroups)DBUtility.ToNullableInt32(dataReader["BloodGroup"]);
			entity.Sex = (Sexes)DBUtility.ToInt32(dataReader["Sex"]);
			entity.MaritalStatus = (MaritalStatuses)DBUtility.ToInt32(dataReader["MaritalStatus"]);
			entity.Religion = (Religions)DBUtility.ToInt32(dataReader["Religion"]);
            //entity.Photo = dataReader["Photo"] == DBNull.Value ? null : (Byte[])dataReader["Photo"];
            entity.PermanentAddressId = DBUtility.ToInt32(dataReader["PermanentAddressId"]);
            entity.PresentAddressId = DBUtility.ToInt32(dataReader["PresentAddressId"]);
			entity.AppointmentLetterDate = DBUtility.ToNullableDateTime(dataReader["AppointmentLetterDate"]);
			entity.AppointmentLetterNo = DBUtility.ToNullableString(dataReader["AppointmentLetterNo"]);
            entity.JoiningDate = DBUtility.ToNullableDateTime(dataReader["JoiningDate"]);
            entity.PermanentLetterDate = DBUtility.ToNullableDateTime(dataReader["PermanentLetterDate"]);
            entity.PermanentLetterNo = DBUtility.ToNullableString(dataReader["PermanentLetterNo"]);
            entity.PermanentDate = DBUtility.ToNullableDateTime(dataReader["PermanentDate"]);
            entity.EmploymentType = (EmploymentTypes)DBUtility.ToInt32(dataReader["EmploymentType"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.NationalId = DBUtility.ToNullableInt64(dataReader["NationalId"]);
            entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_Employee> FindByPermanentAddressId(Int32 permanentAddressId, String sortColumns)
        {
            return Find("[PermanentAddressId] = '" + permanentAddressId + "'", sortColumns);
        }

        public static H_Employee GetByCode(String code)
        {
            return Get("[Code] = " + code );
        }

        public static IList<H_Employee> FindByPermanentAddressId(TransactionManager transactionManager, Int32 permanentAddressId, String sortColumns)
        {
            return Find(transactionManager, "[PermanentAddressId] = '" + permanentAddressId + "'", sortColumns);
        }

        public static IList<H_Employee> FindByPresentAddressId(Int32 presentAddressId, String sortColumns)
        {
            return Find("[PresentAddressId] = '" + presentAddressId + "'", sortColumns);
        }

        public static IList<H_Employee> FindByPresentAddressId(TransactionManager transactionManager, Int32 presentAddressId, String sortColumns)
        {
            return Find(transactionManager, "[PresentAddressId] = '" + presentAddressId + "'", sortColumns);
        }

        public Int32 Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}
        public String NameInBangla
        {
            get { return _NameInBangla; }
            set { _NameInBangla = value; }
        }

		public String FatherName
		{
			get {return _FatherName;}
			set {_FatherName = value;}
		}

		public String MotherName
		{
			get {return _MotherName;}
			set {_MotherName = value;}
		}

        public Nullable<DateTime> DateOfBirth
		{
			get {return _DateOfBirth;}
			set {_DateOfBirth = value;}
		}

        public BloodGroups BloodGroup
		{
			get {return _BloodGroup;}
			set {_BloodGroup = value;}
		}

        public Sexes Sex
		{
			get {return _Sex;}
			set {_Sex = value;}
		}

        public MaritalStatuses MaritalStatus
		{
			get {return _MaritalStatus;}
			set {_MaritalStatus = value;}
		}

        public Religions Religion
		{
			get {return _Religion;}
			set {_Religion = value;}
		}

        //public Byte[] Photo
        //{
        //    get {return this._Photo;}
        //    set {this._Photo = value;}
        //}

        public Int32 PermanentAddressId
		{
			get {return _PermanentAddressId;}
			set {_PermanentAddressId = value;}
		}

		public Int32 PresentAddressId
		{
			get {return _PresentAddressId;}
			set {_PresentAddressId = value;}
		}

        public Nullable<DateTime> AppointmentLetterDate
		{
            get {return _AppointmentLetterDate;}
			set {_AppointmentLetterDate = value;}
		}

		public String AppointmentLetterNo
		{
			get {return _AppointmentLetterNo;}
			set {_AppointmentLetterNo = value;}
		}

        public Nullable<DateTime> JoiningDate
        {
            get { return _JoiningDate; }
            set { _JoiningDate = value; }
        }

        public Nullable<DateTime> PermanentLetterDate
        {
            get { return _PermanentLetterDate; }
            set { _PermanentLetterDate = value; }
        }

        public String PermanentLetterNo
        {
            get { return _PermanentLetterNo; }
            set { _PermanentLetterNo = value; }
        }

        public Nullable<DateTime> PermanentDate
        {
            get { return _PermanentDate; }
            set { _PermanentDate = value; }
        }

        public EmploymentTypes EmploymentType
        {
            get { return _EmploymentType; }
            set { _EmploymentType = value; }
        }

        public Statuses Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        public String FullName
        {
            get
            {
                if (Name != null)
                {
                    return Code + ": " + Name + "(" + Code.ToString() + ")";
                }
                else
                {
                    return Name;
                }
            }
        }


        [Property(PropertyAttribute.Attributes.NonTable)]
        public String EmployeeName
        {
            get
            {
                if (Name != null)
                {
                    return Code + ": " + Name;
                }
                else
                {
                    return Name;
                }
            }
        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        public H_Address PermanentAddress
        {
            get { return _PermanentAddress; }
            set { _PermanentAddress = value; }
        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        public H_Address PresentAddress
        {
            get { return _PresentAddress; }
            set { _PresentAddress = value; }
        }

        public Nullable<Int64> NationalId
        {
            get { return _NationalId; }
            set { _NationalId = value; }
        }
	}
}
