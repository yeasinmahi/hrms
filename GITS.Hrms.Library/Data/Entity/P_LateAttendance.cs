using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class P_LateAttendance : EntityBase<P_LateAttendance>
    {
        private Int32 _H_EmployeeId;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private Nullable<Int32> _Late96_930;
        private Nullable<Int32> _Late931_days;
        private Nullable<Int32> _Absent;

        public P_LateAttendance()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_LateAttendance]"; }
		}
        protected override P_LateAttendance Map(SqlDataReader dataReader)
        {
            P_LateAttendance entity = new P_LateAttendance();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
            entity.EndDate = DBUtility.ToDateTime(dataReader["EndDate"]);
            entity.Late96_930 = DBUtility.ToNullableInt32(dataReader["Late96_930"]);
            entity.Late931_days = DBUtility.ToNullableInt32(dataReader["Late931_days"]);
            entity.Absent = DBUtility.ToNullableInt32(dataReader["Absent"]);
            entity.EntityState = EntityStates.Clean;

            return entity;
        }

        public P_LateAttendance GetByH_EmployeeId(Int32 h_EmployeeId)
        {
            return Get("H_EmployeeId=" + h_EmployeeId);
        }

        public Int32 H_EmployeeId
        {
            get { return _H_EmployeeId; }
            set { _H_EmployeeId = value; }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public Nullable<Int32> Late96_930
        {
            get { return _Late96_930; }
            set { _Late96_930 = value; }
        }
        public Nullable<Int32> Late931_days
        {
            get { return _Late931_days; }
            set { _Late931_days = value; }
        }

        public Nullable<Int32> Absent
        {
            get { return _Absent; }
            set { _Absent = value; }
        }
    }
}
