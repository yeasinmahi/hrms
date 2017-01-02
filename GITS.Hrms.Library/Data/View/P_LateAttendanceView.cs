using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public class P_LateAttendanceView : ViewBase<P_LateAttendanceView>
    {
        private String _Name;
        private Int32 _Code;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private Nullable<Int32> _Late96_930;
        private Nullable<Int32> _Late931_days;
        private Nullable<Int32> _Absent;


        public P_LateAttendanceView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[P_LateAttendanceView]"; }
		}

        protected override P_LateAttendanceView Map(SqlDataReader dataReader)
		{
            P_LateAttendanceView view = new P_LateAttendanceView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
            view.Name = DBUtility.ToString(dataReader["Name"]);
            view.Code = DBUtility.ToInt32(dataReader["Code"]);
            view.StartDate = DBUtility.ToDateTime(dataReader["StartDate"]);
            view.EndDate = DBUtility.ToDateTime(dataReader["EndDate"]);
            view.Late96_930 = DBUtility.ToNullableInt32(dataReader["Late96_930"]);
            view.Late931_days = DBUtility.ToNullableInt32(dataReader["Late931_days"]);
            view.Absent = DBUtility.ToNullableInt32(dataReader["Absent"]);

			return view;
		}
        public String  Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public Int32 Code
        {
            get { return this._Code; }
            set { this._Code = value; }
        }
        public DateTime StartDate
        {
            get { return this._StartDate; }
            set { this._StartDate = value; }
        }

        public DateTime EndDate
        {
            get { return this._EndDate; }
            set { this._EndDate = value; }
        }

        public Nullable<Int32> Late96_930
        {
            get { return this._Late96_930; }
            set { this._Late96_930 = value; }
        }
        public Nullable<Int32> Late931_days
        {
            get { return this._Late931_days; }
            set { this._Late931_days = value; }
        }

        public Nullable<Int32> Absent
        {
            get { return this._Absent; }
            set { this._Absent = value; }
        }
    }
}
