using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeeDropHistory : EntityBase<H_EmployeeDropHistory>
    {
        private Int32 _H_EmployeeId;
        private Types _Type;
		private String _LetterNo;
		private DateTime _LetterDate;
		private DateTime _DropDate;
		private String _Cause;
        private Boolean _Status;
        private Nullable<DateTime> _EntryDate;

        public enum Types
        {
            Retirement = 21,
            Force_Retirement = 22,
            Resignation = 23,
            Termination = 24,
            Died = 25,
            Discharge = 26,
            Golden_Handshake = 27
        }

        public H_EmployeeDropHistory()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeeDropHistory]"; }
		}

        protected override H_EmployeeDropHistory Map(SqlDataReader dataReader)
		{
            H_EmployeeDropHistory entity = new H_EmployeeDropHistory();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Type = (Types)DBUtility.ToInt32(dataReader["Type"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.DropDate = DBUtility.ToDateTime(dataReader["DropDate"]);
			entity.Cause = DBUtility.ToNullableString(dataReader["Cause"]);
            entity.Status = DBUtility.ToBoolean(dataReader["Status"]);
            entity.EntryDate = DBUtility.ToNullableDateTime(dataReader["EntryDate"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_EmployeeDropHistory> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeeDropHistory> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
        {
            return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

		public Int32 H_EmployeeId
		{
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

        public Types Type
		{
			get {return _Type;}
			set {_Type = value;}
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


		public DateTime DropDate
		{
			get {return _DropDate;}
			set {_DropDate = value;}
		}

		public String Cause
		{
			get {return _Cause;}
			set {_Cause = value;}
		}
        public Boolean Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public Nullable<DateTime> EntryDate
        {
            get { return _EntryDate; }
            set { _EntryDate = value; }
        }
        
    }
}
