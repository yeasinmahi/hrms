using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public class H_EmployeePromotionHistoryView : ViewBase<H_EmployeePromotionHistoryView>
    {
        private Int32 _H_EmployeeId;
        private String _Name;
        private Int32 _Code;
        private String _Type;
        private String _LetterNo;
        private DateTime _LetterDate;
        private Int32 _OldH_GradeId;
        private Int32 _NewH_GradeId;
        private Int32 _OldH_DesignationId;
        private Int32 _NewH_DesignationId;
        private DateTime _PromotionDate;
        private String _OldGrade;
        private String _NewGrade;
        private String _OldDesignation;
        private String _NewDesignation;
        private String _Remarks;

        public H_EmployeePromotionHistoryView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_EmployeePromotionHistoryView]"; }
		}

        protected override H_EmployeePromotionHistoryView Map(SqlDataReader dataReader)
		{
            H_EmployeePromotionHistoryView entity = new H_EmployeePromotionHistoryView();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.Code = DBUtility.ToInt32(dataReader["Code"]);
            entity.Type = DBUtility.ToString(dataReader["Type"]);
           
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);
            entity.PromotionDate = DBUtility.ToDateTime(dataReader["PromotionDate"]);
            entity.OldH_GradeId = DBUtility.ToInt32(dataReader["OldH_GradeId"]);
            entity.NewH_GradeId = DBUtility.ToInt32(dataReader["NewH_GradeId"]);
            entity.OldH_DesignationId = DBUtility.ToInt32(dataReader["OldH_DesignationId"]);
            entity.NewH_DesignationId = DBUtility.ToInt32(dataReader["NewH_DesignationId"]);
            entity.OldGrade = DBUtility.ToString(dataReader["OldGrade"]);
            entity.NewGrade = DBUtility.ToString(dataReader["NewGrade"]);
            entity.OldDesignation = DBUtility.ToString(dataReader["OldDesignation"]);
            entity.NewDesignation = DBUtility.ToString(dataReader["NewDesignation"]);
            entity.Remarks = DBUtility.ToNullableString(dataReader["Remarks"]);


			return entity;
		}

        
		public Int32 H_EmployeeId
		{
			get {return this._H_EmployeeId;}
			set {this._H_EmployeeId = value;}
		}
        public String Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }
        public Int32 Code
        {
            get { return this._Code; }
            set { this._Code = value; }
        }
        public String Type
		{
			get {return this._Type;}
			set {this._Type = value;}
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

		public DateTime PromotionDate
		{
			get {return this._PromotionDate;}
			set {this._PromotionDate = value;}
		}

		
		public String Remarks
		{
			get {return this._Remarks;}
			set {this._Remarks = value;}
		}
        public Int32 OldH_GradeId
        {
            get { return this._OldH_GradeId; }
            set { this._OldH_GradeId = value; }
        }
        public Int32 NewH_GradeId
        {
            get { return this._NewH_GradeId; }
            set { this._NewH_GradeId = value; }
        }
        public Int32 OldH_DesignationId
        {
            get { return this._OldH_DesignationId; }
            set { this._OldH_DesignationId = value; }
        }
        public Int32 NewH_DesignationId
        {
            get { return this._NewH_DesignationId; }
            set { this._NewH_DesignationId = value; }
        }
        public String OldGrade
        {
            get { return this._OldGrade; }
            set { this._OldGrade = value; }
        }
        public String NewGrade
        {
            get { return this._NewGrade; }
            set { this._NewGrade = value; }
        }
        public String OldDesignation
        {
            get { return this._OldDesignation; }
            set { this._OldDesignation = value; }
        }
        public String NewDesignation
        {
            get { return this._NewDesignation; }
            set { this._NewDesignation = value; }
        }
    }
}
