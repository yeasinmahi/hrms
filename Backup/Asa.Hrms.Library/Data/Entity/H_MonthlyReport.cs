using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_MonthlyReport : EntityBase<H_MonthlyReport>
    {
		private DateTime _ReportDate;
        private DateTime _EntryDateTime;
        private String _EmpNote1;
        private String _EmpNote2;
        private String _EmpNote3;
        private Int32 _EmpEduProgram;
        private Int32 _EmpAsaHealthProgram;
        private Int32 _EmpHabigongHealthprogram;
        private Nullable<Int32> _ExcessEmpDistrict;
        private Nullable<Int32> _ExcessEmpLO;
        private Nullable<Int32> _ExcessEmpABM;
        private Nullable<Int32> _ExcessEmpBM;
        private Nullable<Int32> _ExcessEmpRM;
        private Nullable<Int32> _ExcessEmpDM;
        private Nullable<Int32> _ExcessEmpZM;
        private Nullable<Int32> _ExcessEmpOthers;

        private Nullable<Int32> _EmpDemandLO;
        private Nullable<Int32> _EmpDemandFillLO;
        private Nullable<Int32> _EmpDemandABM;
        private Nullable<Int32> _EmpDemandFillABM;
        private Nullable<Int32> _EmpDemandBM;
        private Nullable<Int32> _EmpDemandFillBM;
        private Nullable<Int32> _EmpDemandRM;
        private Nullable<Int32> _EmpDemandFillRM;
        private Nullable<Int32> _EmpDemandDM;
        private Nullable<Int32> _EmpDemandFillDM;
        private Nullable<Int32> _EmpDemandZM;
        private Nullable<Int32> _EmpDemandFillZM;

        private String _TransferNote;
        private String _LessTimeTransferNote;

        private Nullable<Int32> _TransAppLO;
        private Nullable<Int32> _TransAppSettleLO;
        private Nullable<Int32> _TransAppABM;
        private Nullable<Int32> _TransAppSettleABM;
        private Nullable<Int32> _TransAppBM;
        private Nullable<Int32> _TransAppSettleBM;
        private Nullable<Int32> _TransAppCO_ASE;
        private Nullable<Int32> _TransAppSettleCO_ASE;
        private Nullable<Int32> _TransAppRM;
        private Nullable<Int32> _TransAppSettleRM;
        private Nullable<Int32> _TransAppDM;
        private Nullable<Int32> _TransAppSettleDM;
        private Nullable<Int32> _TransAppZM;
        private Nullable<Int32> _TransAppSettleZM;

        private String _AttendanceNoteKa;
        private String _AttendanceNoteKha;
        private String _AttendanceNoteGa;
        private String _AdministrativeStepsTransfer;
        private String _AdministrativeStepsDropOut;
        private String _AdministrativeStepsPunishment;
        private String _AdministrativeStepsTreatment;
        private String _RecruitmentNote;

        private Nullable<Int32> _HealthCompRecruit;
        private String _PrintingNote;
        private Nullable<Int32> _TelephoneBill;
        private Nullable<Int32> _ElectricityBill;
        private Nullable<Int32> _MobileBill;

        public H_MonthlyReport()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_MonthlyReport]"; }
		}

        protected override H_MonthlyReport Map(SqlDataReader dataReader)
		{
            H_MonthlyReport entity = new H_MonthlyReport();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.ReportDate = DBUtility.ToDateTime(dataReader["ReportDate"]);
            entity.EmpNote1 = DBUtility.ToNullableString(dataReader["EmpNote1"]);
            entity.EmpNote2 = DBUtility.ToNullableString(dataReader["EmpNote2"]);
            entity.EmpNote3 = DBUtility.ToNullableString(dataReader["EmpNote3"]);
            entity.EmpEduProgram = DBUtility.ToInt32(dataReader["EmpEduProgram"]);
            entity.EmpAsaHealthProgram = DBUtility.ToInt32(dataReader["EmpAsaHealthProgram"]);
            entity.EmpHabigongHealthprogram = DBUtility.ToInt32(dataReader["EmpHabigongHealthprogram"]);
            entity.ExcessEmpDistrict = DBUtility.ToNullableInt32(dataReader["ExcessEmpDistrict"]);
            entity.ExcessEmpLO = DBUtility.ToNullableInt32(dataReader["ExcessEmpLO"]);
            entity.ExcessEmpABM = DBUtility.ToNullableInt32(dataReader["ExcessEmpABM"]);
            entity.ExcessEmpBM = DBUtility.ToNullableInt32(dataReader["ExcessEmpBM"]);
            entity.ExcessEmpRM = DBUtility.ToNullableInt32(dataReader["ExcessEmpRM"]);
            entity.ExcessEmpDM = DBUtility.ToNullableInt32(dataReader["ExcessEmpDM"]);
            entity.ExcessEmpZM = DBUtility.ToNullableInt32(dataReader["ExcessEmpZM"]);
            entity.ExcessEmpOthers = DBUtility.ToNullableInt32(dataReader["ExcessEmpOthers"]);

            entity.EmpDemandLO = DBUtility.ToNullableInt32(dataReader["EmpDemandLO"]);
            entity.EmpDemandFillLO = DBUtility.ToNullableInt32(dataReader["EmpDemandFillLO"]);
            entity.EmpDemandABM = DBUtility.ToNullableInt32(dataReader["EmpDemandABM"]);
            entity.EmpDemandFillABM = DBUtility.ToNullableInt32(dataReader["EmpDemandFillABM"]);
            entity.EmpDemandBM = DBUtility.ToNullableInt32(dataReader["EmpDemandBM"]);
            entity.EmpDemandFillBM = DBUtility.ToNullableInt32(dataReader["EmpDemandFillBM"]);
            entity.EmpDemandRM = DBUtility.ToNullableInt32(dataReader["EmpDemandRM"]);
            entity.EmpDemandFillRM = DBUtility.ToNullableInt32(dataReader["EmpDemandFillRM"]);
            entity.EmpDemandDM = DBUtility.ToNullableInt32(dataReader["EmpDemandDM"]);
            entity.EmpDemandFillDM = DBUtility.ToNullableInt32(dataReader["EmpDemandFillDM"]);
            entity.EmpDemandZM = DBUtility.ToNullableInt32(dataReader["EmpDemandZM"]);
            entity.EmpDemandFillZM = DBUtility.ToNullableInt32(dataReader["EmpDemandFillZM"]);

            entity.TransferNote = DBUtility.ToNullableString(dataReader["TransferNote"]);
            entity.LessTimeTransferNote = DBUtility.ToNullableString(dataReader["LessTimeTransferNote"]);

            entity.TransAppLO = DBUtility.ToNullableInt32(dataReader["TransAppLO"]);
            entity.TransAppABM = DBUtility.ToNullableInt32(dataReader["TransAppABM"]);
            entity.TransAppBM = DBUtility.ToNullableInt32(dataReader["TransAppBM"]);
            entity.TransAppRM = DBUtility.ToNullableInt32(dataReader["TransAppRM"]);
            entity.TransAppDM = DBUtility.ToNullableInt32(dataReader["TransAppDM"]);
            entity.TransAppZM = DBUtility.ToNullableInt32(dataReader["TransAppZM"]);
            entity.TransAppCO_ASE = DBUtility.ToNullableInt32(dataReader["TransAppCO_ASE"]);
            entity.TransAppSettleLO = DBUtility.ToNullableInt32(dataReader["TransAppSettleLO"]);
            entity.TransAppSettleABM = DBUtility.ToNullableInt32(dataReader["TransAppSettleABM"]);
            entity.TransAppSettleBM = DBUtility.ToNullableInt32(dataReader["TransAppSettleBM"]);
            entity.TransAppSettleRM = DBUtility.ToNullableInt32(dataReader["TransAppSettleRM"]);
            entity.TransAppSettleDM = DBUtility.ToNullableInt32(dataReader["TransAppSettleDM"]);
            entity.TransAppSettleZM = DBUtility.ToNullableInt32(dataReader["TransAppSettleZM"]);
            entity.TransAppSettleCO_ASE = DBUtility.ToNullableInt32(dataReader["TransAppSettleCO_ASE"]);

            entity.AttendanceNoteKa = DBUtility.ToNullableString(dataReader["AttendanceNoteKa"]);
            entity.AttendanceNoteKha = DBUtility.ToNullableString(dataReader["AttendanceNoteKha"]);
            entity.AttendanceNoteGa = DBUtility.ToNullableString(dataReader["AttendanceNoteGa"]);

            entity.AdministrativeStepsTransfer = DBUtility.ToNullableString(dataReader["AdministrativeStepsTransfer"]);
            entity.AdministrativeStepsDropOut = DBUtility.ToNullableString(dataReader["AdministrativeStepsDropOut"]);
            entity.AdministrativeStepsPunishment = DBUtility.ToNullableString(dataReader["AdministrativeStepsPunishment"]);
            entity.AdministrativeStepsTreatment = DBUtility.ToNullableString(dataReader["AdministrativeStepsTreatment"]);
            entity.RecruitmentNote = DBUtility.ToNullableString(dataReader["RecruitmentNote"]);
            entity.EntryDateTime = DBUtility.ToDateTime(dataReader["EntryDateTime"]);

            entity.HealthCompRecruit = DBUtility.ToNullableInt32(dataReader["HealthCompRecruit"]);
            entity.PrintingNote = DBUtility.ToNullableString(dataReader["PrintingNote"]);
            entity.TelephoneBill = DBUtility.ToNullableInt32(dataReader["TelephoneBill"]);
            entity.ElectricityBill = DBUtility.ToNullableInt32(dataReader["ElectricityBill"]);
            entity.MobileBill = DBUtility.ToNullableInt32(dataReader["MobileBill"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}
		
		public DateTime ReportDate
		{
			get {return this._ReportDate;}
			set {this._ReportDate = value;}
		}
        public String EmpNote1
        {
            get { return this._EmpNote1; }
            set { this._EmpNote1 = value; }
        }
        public String EmpNote2
        {
            get { return this._EmpNote2; }
            set { this._EmpNote2 = value; }
        }
        public String EmpNote3
        {
            get { return this._EmpNote3; }
            set { this._EmpNote3 = value; }
        }
        public Int32 EmpEduProgram
        {
            get { return this._EmpEduProgram; }
            set { this._EmpEduProgram = value; }
        }
        public Int32 EmpAsaHealthProgram
        {
            get { return this._EmpAsaHealthProgram; }
            set { this._EmpAsaHealthProgram = value; }
        }
        public Int32 EmpHabigongHealthprogram
        {
            get { return this._EmpHabigongHealthprogram; }
            set { this._EmpHabigongHealthprogram = value; }
        }
        public Nullable<Int32> ExcessEmpDistrict
        {
            get { return this._ExcessEmpDistrict; }
            set { this._ExcessEmpDistrict = value; }
        }
        public Nullable<Int32> ExcessEmpLO
        {
            get { return this._ExcessEmpLO; }
            set { this._ExcessEmpLO = value; }
        }
        public Nullable<Int32> ExcessEmpABM
        {
            get { return this._ExcessEmpABM; }
            set { this._ExcessEmpABM = value; }
        }
        public Nullable<Int32> ExcessEmpBM
        {
            get { return this._ExcessEmpBM; }
            set { this._ExcessEmpBM = value; }
        }
        public Nullable<Int32> ExcessEmpRM
        {
            get { return this._ExcessEmpRM; }
            set { this._ExcessEmpRM = value; }
        }
        public Nullable<Int32> ExcessEmpDM
        {
            get { return this._ExcessEmpDM; }
            set { this._ExcessEmpDM = value; }
        }
        public Nullable<Int32> ExcessEmpZM
        {
            get { return this._ExcessEmpZM; }
            set { this._ExcessEmpZM = value; }
        }
        public Nullable<Int32> ExcessEmpOthers
        {
            get { return this._ExcessEmpOthers; }
            set { this._ExcessEmpOthers = value; }
        }
        public Nullable<Int32> EmpDemandLO
        {
            get { return this._EmpDemandLO; }
            set { this._EmpDemandLO = value; }
        }
        public Nullable<Int32> EmpDemandABM
        {
            get { return this._EmpDemandABM; }
            set { this._EmpDemandABM = value; }
        }
        public Nullable<Int32> EmpDemandBM
        {
            get { return this._EmpDemandBM; }
            set { this._EmpDemandBM = value; }
        }
        public Nullable<Int32> EmpDemandRM
        {
            get { return this._EmpDemandRM; }
            set { this._EmpDemandRM = value; }
        }
        public Nullable<Int32> EmpDemandDM
        {
            get { return this._EmpDemandDM; }
            set { this._EmpDemandDM = value; }
        }
        public Nullable<Int32> EmpDemandZM
        {
            get { return this._EmpDemandZM; }
            set { this._EmpDemandZM = value; }
        }
        public Nullable<Int32> EmpDemandFillLO
        {
            get { return this._EmpDemandFillLO; }
            set { this._EmpDemandFillLO = value; }
        }
        public Nullable<Int32> EmpDemandFillABM
        {
            get { return this._EmpDemandFillABM; }
            set { this._EmpDemandFillABM = value; }
        }
        public Nullable<Int32> EmpDemandFillBM
        {
            get { return this._EmpDemandFillBM; }
            set { this._EmpDemandFillBM = value; }
        }
        public Nullable<Int32> EmpDemandFillDM
        {
            get { return this._EmpDemandFillDM; }
            set { this._EmpDemandFillDM = value; }
        }
        public Nullable<Int32> EmpDemandFillRM
        {
            get { return this._EmpDemandFillRM; }
            set { this._EmpDemandFillRM = value; }
        }
        public Nullable<Int32> EmpDemandFillZM
        {
            get { return this._EmpDemandFillZM; }
            set { this._EmpDemandFillZM = value; }
        }
        public String TransferNote
        {
            get { return this._TransferNote; }
            set { this._TransferNote = value; }
        }
        public String LessTimeTransferNote
        {
            get { return this._LessTimeTransferNote; }
            set { this._LessTimeTransferNote = value; }
        }

        public Nullable<Int32> TransAppLO
        {
            get { return this._TransAppLO; }
            set { this._TransAppLO = value; }
        }
        public Nullable<Int32> TransAppSettleLO
        {
            get { return this._TransAppSettleLO; }
            set { this._TransAppSettleLO = value; }
        }
        public Nullable<Int32> TransAppABM
        {
            get { return this._TransAppABM; }
            set { this._TransAppABM = value; }
        }
        public Nullable<Int32> TransAppSettleABM
        {
            get { return this._TransAppSettleABM; }
            set { this._TransAppSettleABM = value; }
        }
        public Nullable<Int32> TransAppBM
        {
            get { return this._TransAppBM; }
            set { this._TransAppBM = value; }
        }
        public Nullable<Int32> TransAppSettleBM
        {
            get { return this._TransAppSettleBM; }
            set { this._TransAppSettleBM = value; }
        }
        public Nullable<Int32> TransAppCO_ASE
        {
            get { return this._TransAppCO_ASE; }
            set { this._TransAppCO_ASE = value; }
        }
        public Nullable<Int32> TransAppSettleCO_ASE
        {
            get { return this._TransAppSettleCO_ASE; }
            set { this._TransAppSettleCO_ASE = value; }
        }
        public Nullable<Int32> TransAppRM
        {
            get { return this._TransAppRM; }
            set { this._TransAppRM = value; }
        }
        public Nullable<Int32> TransAppSettleRM
        {
            get { return this._TransAppSettleRM; }
            set { this._TransAppSettleRM = value; }
        }
        public Nullable<Int32> TransAppDM
        {
            get { return this._TransAppDM; }
            set { this._TransAppDM = value; }
        }
        public Nullable<Int32> TransAppSettleDM
        {
            get { return this._TransAppSettleDM; }
            set { this._TransAppSettleDM = value; }
        }
        public Nullable<Int32> TransAppZM
        {
            get { return this._TransAppZM; }
            set { this._TransAppZM = value; }
        }
        public Nullable<Int32> TransAppSettleZM
        {
            get { return this._TransAppSettleZM; }
            set { this._TransAppSettleZM = value; }
        }
        public String AttendanceNoteKa
        {
            get { return this._AttendanceNoteKa; }
            set { this._AttendanceNoteKa = value; }
        }
        public String AttendanceNoteKha
        {
            get { return this._AttendanceNoteKha; }
            set { this._AttendanceNoteKha = value; }
        }
        public String AttendanceNoteGa
        {
            get { return this._AttendanceNoteGa; }
            set { this._AttendanceNoteGa = value; }
        }
        public String AdministrativeStepsTransfer
        {
            get { return this._AdministrativeStepsTransfer; }
            set { this._AdministrativeStepsTransfer = value; }
        }
        public String AdministrativeStepsDropOut
        {
            get { return this._AdministrativeStepsDropOut; }
            set { this._AdministrativeStepsDropOut = value; }
        }
        public String AdministrativeStepsPunishment
        {
            get { return this._AdministrativeStepsPunishment; }
            set { this._AdministrativeStepsPunishment = value; }
        }
        public String AdministrativeStepsTreatment
        {
            get { return this._AdministrativeStepsTreatment; }
            set { this._AdministrativeStepsTreatment = value; }
        }
        public String RecruitmentNote
        {
            get { return this._RecruitmentNote; }
            set { this._RecruitmentNote = value; }
        }
        public DateTime EntryDateTime
        {
            get { return this._EntryDateTime; }
            set { this._EntryDateTime = value; }
        }
        public String PrintingNote
        {
            get { return this._PrintingNote; }
            set { this._PrintingNote = value; }
        }
        public Nullable<Int32> TelephoneBill
        {
            get { return this._TelephoneBill; }
            set { this._TelephoneBill = value; }
        }
        public Nullable<Int32> ElectricityBill
        {
            get { return this._ElectricityBill; }
            set { this._ElectricityBill = value; }
        }
        public Nullable<Int32> MobileBill
        {
            get { return this._MobileBill; }
            set { this._MobileBill = value; }
        }

        public Nullable<Int32> HealthCompRecruit
        {
            get { return this._HealthCompRecruit; }
            set { this._HealthCompRecruit = value; }
        }
        
    }
}
