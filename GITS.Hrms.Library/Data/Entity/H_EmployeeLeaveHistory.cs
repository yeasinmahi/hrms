using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeeLeaveHistory : EntityBase<H_EmployeeLeaveHistory>
    {
        private Int32 _H_EmployeeId;
        private Types _Type;
        private JoinTypes _JoinType;
		private String _LetterNo;
		private DateTime _LetterDate;
		private DateTime _StartDate;
		private Nullable<DateTime> _EndDate;
		private String _Cause;
        private Int32 _Status;

        public enum Types
        {
            Leave_With_Pay = 11,
            Leave_Without_Pay = 12,
            Medical_Leave = 13,
            Maternity_Leave = 14,
            Suspension = 15,
            Force_Leave = 16,
            Lien = 17
        }

        public enum JoinTypes
        {
            Normal = 1,
            Rejoin = 2
        }

        public H_EmployeeLeaveHistory()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeeLeaveHistory]"; }
		}

        protected override H_EmployeeLeaveHistory Map(SqlDataReader dataReader)
		{
            H_EmployeeLeaveHistory entity = new H_EmployeeLeaveHistory();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Type = (Types)DBUtility.ToInt32(dataReader["Type"]);
            entity.JoinType = (JoinTypes)DBUtility.ToInt32(dataReader["JoinType"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
			entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
			entity.EndDate = DBUtility.ToNullableDateTime(dataReader["EndDate"]);
			entity.Cause = DBUtility.ToNullableString(dataReader["Cause"]);
            entity.Status = DBUtility.ToInt32(dataReader["Status"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_EmployeeLeaveHistory> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeeLeaveHistory> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
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

        public JoinTypes JoinType
        {
            get { return _JoinType; }
            set { _JoinType = value; }
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

		public DateTime StartDate
		{
			get {return _StartDate;}
			set {_StartDate = value;}
		}

		public Nullable<DateTime> EndDate
		{
			get {return _EndDate;}
			set {_EndDate = value;}
		}

		public String Cause
		{
			get {return _Cause;}
			set {_Cause = value;}
		}
        public Int32 Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}
