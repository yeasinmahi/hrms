using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeDrop : EntityBase<H_EmployeeDrop>
	{
		private Int32 _H_EmployeeId;
        private Types _Type;
		private String _LetterNo;
		private DateTime _LetterDate;
        //private String _DropCode;
		private DateTime _DropDate;
		private String _Cause;

        //Changes in this enumeration need changes in H_Employee.Statuses enumeration
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

		public H_EmployeeDrop()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[H_EmployeeDrop]"; }
		}

		protected override H_EmployeeDrop Map(SqlDataReader dataReader)
		{
			H_EmployeeDrop entity = new H_EmployeeDrop();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Type = (Types)DBUtility.ToInt32(dataReader["Type"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            //entity.DropCode = DBUtility.ToString(dataReader["DropCode"]);
			entity.DropDate = DBUtility.ToDateTime(dataReader["DropDate"]);
			entity.Cause = DBUtility.ToNullableString(dataReader["Cause"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_EmployeeDrop> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeeDrop> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
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

        //public String DropCode
        //{
        //    get {return this._DropCode;}
        //    set {this._DropCode = value;}
        //}

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

	}
}
