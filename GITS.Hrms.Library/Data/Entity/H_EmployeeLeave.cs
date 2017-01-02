using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeLeave : EntityBase<H_EmployeeLeave>
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

		public H_EmployeeLeave()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[H_EmployeeLeave]"; }
		}

		protected override H_EmployeeLeave Map(SqlDataReader dataReader)
		{
			H_EmployeeLeave entity = new H_EmployeeLeave();

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

        public static IList<H_EmployeeLeave> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeeLeave> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
        {
            return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}

        public Types Type
		{
			get {return this._Type;}
			set {this._Type = value;}
		}

        public JoinTypes JoinType
        {
            get { return this._JoinType; }
            set { this._JoinType = value; }
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

		public DateTime StartDate
		{
			get {return this._StartDate;}
			set {this._StartDate = value;}
		}

		public Nullable<DateTime> EndDate
		{
			get {return this._EndDate;}
			set {this._EndDate = value;}
		}

		public String Cause
		{
			get {return this._Cause;}
			set {this._Cause = value;}
		}
        public Int32 Status
        {
            get { return this._Status; }
            set { this._Status = value; }
        }
	}
}
