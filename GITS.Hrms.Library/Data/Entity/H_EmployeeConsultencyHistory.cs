using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeeConsultencyHistory : EntityBase<H_EmployeeConsultencyHistory>
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
        private Boolean _IsProcessed;

        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }

        public H_EmployeeConsultencyHistory()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeeConsultencyHistory]"; }
		}

        protected override H_EmployeeConsultencyHistory Map(SqlDataReader dataReader)
		{
            H_EmployeeConsultencyHistory entity = new H_EmployeeConsultencyHistory();

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
            entity.IsProcessed = DBUtility.ToBoolean(dataReader["IsProcessed"]);

            entity.EntityState = EntityStates.Clean;

			return entity;
		}



        public static IList<H_EmployeeConsultencyHistory> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeeConsultencyHistory> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
        {
            return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

       

		public Int32 H_EmployeeId
		{
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

        public Statuses Status
		{
			get {return _Status;}
			set {_Status = value;}
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

        public String NgoName
		{
			get {return _NgoName;}
			set {_NgoName = value;}
		}
        public String Through
        {
            get { return _Through; }
            set { _Through = value; }
        }
        public String Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }
        public String Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }
        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        

		public DateTime StartDate
		{
			get {return _StartDate;}
			set {_StartDate = value;}
		}
        public Nullable<DateTime> EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
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

        public Boolean IsProcessed
        {
            get { return _IsProcessed; }
            set { _IsProcessed = value; }
        }
        
    }
}
