using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
	[Serializable]
	[Class(ClassAttribute.Attributes.View)]
	public class H_EmployeeView : ViewBase<H_EmployeeView>
	{
		private String _Name;
        private String _NameInBangla;
        private Int32 _Code;
		private String _FatherName;
		private String _MotherName;
		private DateTime _DateOfBirth;
        private H_Employee.BloodGroups _BloodGroup;
        private String _Sex;
        private String _MaritalStatus;
        private String _Religion;
        private Int32 _PermanentAddressId;
        private Int32 _PresentAddressId;
        private DateTime _AppointmentLetterDate;
        private String _AppointmentLetterNo;
        private DateTime _JoiningDate;
        private String _EmploymentType;
        private String _Status;
		private Int32 _BranchId;
		private String _BranchName;
        private Int32 _RegionId;
        private String _RegionName;
        private Int32 _SubzoneId;
        private String _SubzoneName;
        private Int32 _ZoneId;
        private String _ZoneName;
		private Int32 _H_DepartmentId;
		private String _DepartmentName;
		private Int32 _H_DesignationId;
		private String _DesignationName;
		private Int32 _H_GradeId;
		private String _GradeName;
        private Int32 _SL;

		public H_EmployeeView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeView]"; }
		}

		protected override H_EmployeeView Map(SqlDataReader dataReader)
		{
			H_EmployeeView view = new H_EmployeeView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
			view.Name = DBUtility.ToString(dataReader["Name"]);
            view.NameInBangla = DBUtility.ToNullableString(dataReader["NameInBangla"]);
            view.Code = DBUtility.ToInt32(dataReader["Code"]);
			view.FatherName = DBUtility.ToString(dataReader["FatherName"]);
			view.MotherName = DBUtility.ToString(dataReader["MotherName"]);
			view.DateOfBirth = DBUtility.ToDateTime(dataReader["DateOfBirth"]);
			view.BloodGroup = (H_Employee.BloodGroups)DBUtility.ToInt32(dataReader["BloodGroup"]);
            view.Sex = UIUtility.ConvertEnamValueToString(typeof(H_Employee.Sexes), DBUtility.ToInt32(dataReader["Sex"]));
            view.MaritalStatus = UIUtility.ConvertEnamValueToString(typeof(H_Employee.MaritalStatuses),DBUtility.ToInt32(dataReader["MaritalStatus"]));
            view.Religion = UIUtility.ConvertEnamValueToString(typeof(H_Employee.Religions),DBUtility.ToInt32(dataReader["Religion"]));
			view.PermanentAddressId = DBUtility.ToInt32(dataReader["PermanentAddressId"]);
			view.PresentAddressId = DBUtility.ToInt32(dataReader["PresentAddressId"]);
			view.AppointmentLetterDate = DBUtility.ToDateTime(dataReader["AppointmentLetterDate"]);
			view.AppointmentLetterNo = DBUtility.ToString(dataReader["AppointmentLetterNo"]);
			view.JoiningDate = DBUtility.ToDateTime(dataReader["JoiningDate"]);
            view.EmploymentType = UIUtility.ConvertEnamValueToString(typeof(H_Employee.EmploymentTypes),DBUtility.ToInt32(dataReader["EmploymentType"]));
            view.Status = UIUtility.ConvertEnamValueToString(typeof(H_Employee.Statuses),DBUtility.ToInt32(dataReader["Status"]));
			view.BranchId = DBUtility.ToInt32(dataReader["BranchId"]);
			view.BranchName = DBUtility.ToString(dataReader["BranchName"]);
            view.RegionId = DBUtility.ToInt32(dataReader["RegionId"]);
            view.RegionName = DBUtility.ToString(dataReader["RegionName"]);
            view.SubzoneId = DBUtility.ToInt32(dataReader["SubzoneId"]);
            view.SubzoneName = DBUtility.ToString(dataReader["SubzoneName"]);
            view.ZoneId = DBUtility.ToInt32(dataReader["ZoneId"]);
            view.ZoneName = DBUtility.ToString(dataReader["ZoneName"]);
			view.H_DepartmentId = DBUtility.ToInt32(dataReader["H_DepartmentId"]);
			view.DepartmentName = DBUtility.ToString(dataReader["DepartmentName"]);
			view.H_DesignationId = DBUtility.ToInt32(dataReader["H_DesignationId"]);
            view.DesignationName = DBUtility.ToString(dataReader["DesignationName"]);
			view.H_GradeId = DBUtility.ToInt32(dataReader["H_GradeId"]);
			view.GradeName = DBUtility.ToString(dataReader["GradeName"]);
            view.SL = DBUtility.ToInt32(dataReader["SL"]);

			return view;
		}

        public static IList<H_EmployeeView> FindByLogin(String whereClause, String sortColumns, String login)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "BranchId IN(SELECT DISTINCT Branch.Id FROM Zone INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.ZoneId IS NULL OR UserLocation.ZoneId = Zone.Id) INNER JOIN Subzone ON Zone.Id = Subzone.ZoneId AND (UserLocation.SubzoneId IS NULL OR UserLocation.SubzoneId = Subzone.Id) INNER JOIN Region ON Subzone.Id = Region.SubzoneId AND (UserLocation.RegionId IS NULL OR UserLocation.RegionId = Region.Id) INNER JOIN Branch ON Region.Id = Branch.RegionId  AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return Find(whereClause, sortColumns);
        }
        public static IList<H_EmployeeView> FindByLogin(String whereClause, String sortColumns, String login, int start, int count, out Int32 total)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "BranchId IN(SELECT DISTINCT Branch.Id FROM Zone INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.ZoneId IS NULL OR UserLocation.ZoneId = Zone.Id) INNER JOIN Subzone ON Zone.Id = Subzone.ZoneId AND (UserLocation.SubzoneId IS NULL OR UserLocation.SubzoneId = Subzone.Id) INNER JOIN Region ON Subzone.Id = Region.SubzoneId AND (UserLocation.RegionId IS NULL OR UserLocation.RegionId = Region.Id) INNER JOIN Branch ON Region.Id = Branch.RegionId  AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return FindByLogin(whereClause, sortColumns, start, count, out total);
        }
        public static IList<H_EmployeeView> FindByLogin(TransactionManager transactionManager, String whereClause, String sortColumns, String login)
        {
            if (whereClause.Trim() != "")
            {
                whereClause += " AND ";
            }

            whereClause += "BranchId IN(SELECT DISTINCT Branch.Id FROM Zone INNER JOIN UserLocation ON [Login] = '" + login + "' AND (UserLocation.ZoneId IS NULL OR UserLocation.ZoneId = Zone.Id) INNER JOIN Subzone ON Zone.Id = Subzone.ZoneId AND (UserLocation.SubzoneId IS NULL OR UserLocation.SubzoneId = Subzone.Id) INNER JOIN Region ON Subzone.Id = Region.SubzoneId AND (UserLocation.RegionId IS NULL OR UserLocation.RegionId = Region.Id) INNER JOIN Branch ON Region.Id = Branch.RegionId  AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";

            return Find(transactionManager, whereClause, sortColumns);
        }
        public Int32 SL
        {
            get { return this._SL; }
            set { this._SL = value; }
        }

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}
        public String NameInBangla
        {
            get { return this._NameInBangla; }
            set { this._NameInBangla = value; }
        }
        public Int32 Code
        {
            get { return this._Code; }
            set { this._Code = value; }
        }

		public String FatherName
		{
			get {return this._FatherName;}
			set {this._FatherName = value;}
		}

		public String MotherName
		{
			get {return this._MotherName;}
			set {this._MotherName = value;}
		}

		public DateTime DateOfBirth
		{
			get {return this._DateOfBirth;}
			set {this._DateOfBirth = value;}
		}

        public H_Employee.BloodGroups BloodGroup
		{
			get {return this._BloodGroup;}
			set {this._BloodGroup = value;}
		}

        public String Sex
		{
			get {return this._Sex;}
			set {this._Sex = value;}
		}

        public String MaritalStatus
		{
			get {return this._MaritalStatus;}
			set {this._MaritalStatus = value;}
		}

        public String Religion
		{
			get {return this._Religion;}
			set {this._Religion = value;}
		}

		public Int32 PermanentAddressId
		{
			get {return this._PermanentAddressId;}
			set {this._PermanentAddressId = value;}
		}

		public Int32 PresentAddressId
		{
			get {return this._PresentAddressId;}
			set {this._PresentAddressId = value;}
		}

		public DateTime AppointmentLetterDate
		{
			get {return this._AppointmentLetterDate;}
			set {this._AppointmentLetterDate = value;}
		}

		public String AppointmentLetterNo
		{
			get {return this._AppointmentLetterNo;}
			set {this._AppointmentLetterNo = value;}
		}

		public DateTime JoiningDate
		{
			get {return this._JoiningDate;}
			set {this._JoiningDate = value;}
		}

        public String EmploymentType
        {
            get { return this._EmploymentType; }
            set { this._EmploymentType = value; }
        }

        public String Status
		{
			get {return this._Status;}
			set {this._Status = value;}
		}

		public Int32 BranchId
		{
			get {return this._BranchId;}
			set {this._BranchId = value;}
		}

		public String BranchName
		{
			get {return this._BranchName;}
			set {this._BranchName = value;}
		}

        public Int32 RegionId
        {
            get { return this._RegionId; }
            set { this._RegionId = value; }
        }

        public String RegionName
        {
            get { return this._RegionName; }
            set { this._RegionName = value; }
        }

        public Int32 SubzoneId
        {
            get { return this._SubzoneId; }
            set { this._SubzoneId = value; }
        }

        public String SubzoneName
        {
            get { return this._SubzoneName; }
            set { this._SubzoneName = value; }
        }

        public Int32 ZoneId
        {
            get { return this._ZoneId; }
            set { this._ZoneId = value; }
        }

        public String ZoneName
        {
            get { return this._ZoneName; }
            set { this._ZoneName = value; }
        }

		public Int32 H_DepartmentId
		{
			get {return this._H_DepartmentId;}
			set {this._H_DepartmentId = value;}
		}

		public String DepartmentName
		{
			get {return this._DepartmentName;}
			set {this._DepartmentName = value;}
		}

		public Int32 H_DesignationId
		{
			get {return this._H_DesignationId;}
			set {this._H_DesignationId = value;}
		}

		public String DesignationName
		{
			get {return this._DesignationName;}
			set {this._DesignationName = value;}
		}

		public Int32 H_GradeId
		{
			get {return this._H_GradeId;}
			set {this._H_GradeId = value;}
		}

		public String GradeName
		{
			get {return this._GradeName;}
			set {this._GradeName = value;}
		}

	}
}
