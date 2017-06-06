using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeePermanent : EntityBase<H_EmployeePermanent>
    {
        private Int32 _H_EmployeeId;
		private String _LetterNo;
		private DateTime _LetterDate;
		private DateTime _PermanentDate;
		private String _Remarks;

        public H_EmployeePermanent()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeePermanent]"; }
		}

        protected override H_EmployeePermanent Map(SqlDataReader dataReader)
		{
            H_EmployeePermanent entity = new H_EmployeePermanent();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            entity.PermanentDate = DBUtility.ToDateTime(dataReader["PermanentDate"]);
			entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);

            entity.EntityState = EntityStates.Clean;

			return entity;
		}

        public static IList<H_EmployeePermanent> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
        {
            return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
        }

        public static IList<H_EmployeePermanent> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
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


		public DateTime PermanentDate
		{
			get {return _PermanentDate;}
			set {_PermanentDate = value;}
		}

		public String Remarks
		{
			get {return _Remarks;}
			set {_Remarks = value;}
		}
    }
}
