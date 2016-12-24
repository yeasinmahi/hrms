using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeeConsultency : EntityBase<H_EmployeeConsultency>
    {
        private Int32 _H_EmployeeId;
		private String _LetterNo;
		private DateTime _LetterDate;
        private String _NgoName;
        private String _Through;
        private String _Phone;
        private String _Fax;
        private String _Email;
        private DateTime _StartDate;
        private Nullable<DateTime> _EndDate;
		private Statuses _Status;
        private Int32 _CountryId;
        private Int32 _OrganizationId;
        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }

        public H_EmployeeConsultency()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeeConsultency]"; }
		}

        protected override H_EmployeeConsultency Map(SqlDataReader dataReader)
		{
            H_EmployeeConsultency entity = new H_EmployeeConsultency();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            entity.NgoName = DBUtility.ToString(dataReader["NgoName"]);
            entity.Through = DBUtility.ToString(dataReader["Through"]);
            entity.Phone = DBUtility.ToNullableString(dataReader["Phone"]);
            entity.Fax = DBUtility.ToNullableString(dataReader["Fax"]);
            entity.Email = DBUtility.ToNullableString(dataReader["Email"]);
            entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
            entity.EndDate = DBUtility.ToNullableDateTime(dataReader["EndDate"]);
            entity.Status = (Statuses)DBUtility.ToInt32(dataReader["Status"]);
            entity.CountryId = DBUtility.ToInt32(dataReader["CountryId"]);
            entity.OrganizationId = DBUtility.ToInt32(dataReader["OrganizationId"]);
            entity.EntityState = EntityStates.Clean;

			return entity;
		}



        public static IList<H_EmployeeConsultency> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeeConsultency> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
        {
            return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

       

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

        public Statuses Status
		{
			get {return this._Status;}
			set {this._Status = value;}
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

        public String NgoName
		{
			get {return this._NgoName;}
			set {this._NgoName = value;}
		}
        public String Through
        {
            get { return this._Through; }
            set { this._Through = value; }
        }
        public String Phone
        {
            get { return this._Phone; }
            set { this._Phone = value; }
        }
        public String Fax
        {
            get { return this._Fax; }
            set { this._Fax = value; }
        }
        public String Email
        {
            get { return this._Email; }
            set { this._Email = value; }
        }
        

		public DateTime StartDate
		{
			get {return this._StartDate;}
			set {this._StartDate = value;}
		}
        public Nullable<DateTime> EndDate
        {
            get { return this._EndDate; }
            set { this._EndDate = value; }
        }

        public Int32 CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
        }

        public int OrganizationId
        {
            get { return _OrganizationId; }
            set { _OrganizationId = value; }
        }
    }
}
