using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_MonthlyReportAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_MONTHLYREPORT ADD"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override string GetListPageUrl()
        {
            return "H_MonthlyReportList.aspx";
        }
        private new Message Validate()
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Record saved successfully.";

            base.Validate();

            if (base.IsValid == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid data provided or required data missing";
                return msg;
            }

            return msg;
        }
        private H_MonthlyReport GetH_MonthlyReport()
        {
            H_MonthlyReport h_MonthlyReport = null;
            if (this.Type == TYPE_ADD)
            {
                h_MonthlyReport = new H_MonthlyReport();
                h_MonthlyReport.EntryDateTime = DateTime.Now;
            }
            else
            {
                h_MonthlyReport = H_MonthlyReport.GetById(Convert.ToInt32(hdnId.Value));
            }
            h_MonthlyReport.ReportDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1).AddMonths(1).AddDays(-1);
            h_MonthlyReport.EmpNote1 = DBUtility.ToNullableString(txtNote1.Text);
            h_MonthlyReport.EmpNote2 = DBUtility.ToNullableString(txtNote2.Text);
            h_MonthlyReport.EmpNote3 = DBUtility.ToNullableString(txtNote3.Text);
            h_MonthlyReport.EmpEduProgram = DBUtility.ToInt32(txtEmpEduProgram.Text);
            h_MonthlyReport.EmpAsaHealthProgram = DBUtility.ToInt32(txtEmpAsaHealthProgram.Text);
            h_MonthlyReport.EmpHabigongHealthprogram = DBUtility.ToInt32(txtEmpHabigongHealthProgram.Text);

            h_MonthlyReport.ExcessEmpDistrict = DBUtility.ToNullableInt32(txtExcessEmpDistrict.Text);
            h_MonthlyReport.ExcessEmpLO = DBUtility.ToNullableInt32(txtExcessEmpLO.Text);
            h_MonthlyReport.ExcessEmpABM = DBUtility.ToNullableInt32(txtExcessEmpABM.Text);
            h_MonthlyReport.ExcessEmpBM = DBUtility.ToNullableInt32(txtExcessEmpBM.Text);
            h_MonthlyReport.ExcessEmpDM = DBUtility.ToNullableInt32(txtExcessEmpDM.Text);
            h_MonthlyReport.ExcessEmpRM = DBUtility.ToNullableInt32(txtExcessEmpRM.Text);
            h_MonthlyReport.ExcessEmpZM = DBUtility.ToNullableInt32(txtExcessEmpZM.Text);
            h_MonthlyReport.ExcessEmpOthers = DBUtility.ToNullableInt32(txtExcessEmpOthers.Text);

            h_MonthlyReport.EmpDemandLO = DBUtility.ToNullableInt32(txtEmpDemandLO.Text);
            h_MonthlyReport.EmpDemandABM = DBUtility.ToNullableInt32(txtEmpDemandABM.Text);
            h_MonthlyReport.EmpDemandBM = DBUtility.ToNullableInt32(txtEmpDemandBM.Text);
            h_MonthlyReport.EmpDemandRM = DBUtility.ToNullableInt32(txtEmpDemandRM.Text);
            h_MonthlyReport.EmpDemandDM = DBUtility.ToNullableInt32(txtEmpDemandDM.Text);
            h_MonthlyReport.EmpDemandZM = DBUtility.ToNullableInt32(txtEmpDemandZM.Text);

            h_MonthlyReport.EmpDemandFillLO = DBUtility.ToNullableInt32(txtEmpDemandFillLO.Text);
            h_MonthlyReport.EmpDemandFillABM = DBUtility.ToNullableInt32(txtEmpDemandFillABM.Text);
            h_MonthlyReport.EmpDemandFillBM = DBUtility.ToNullableInt32(txtEmpDemandFillBM.Text);
            h_MonthlyReport.EmpDemandFillRM = DBUtility.ToNullableInt32(txtEmpDemandFillRM.Text);
            h_MonthlyReport.EmpDemandFillDM = DBUtility.ToNullableInt32(txtEmpDemandFillDM.Text);
            h_MonthlyReport.EmpDemandFillZM = DBUtility.ToNullableInt32(txtEmpDemandFillZM.Text);

            h_MonthlyReport.TransferNote = DBUtility.ToNullableString(txtTransferNote.Text);
            h_MonthlyReport.LessTimeTransferNote = DBUtility.ToNullableString(txtLessTimeTransferNote.Text);

            h_MonthlyReport.TransAppLO = DBUtility.ToNullableInt32(txtTransAppLO.Text);
            h_MonthlyReport.TransAppABM = DBUtility.ToNullableInt32(txtTransAppABM.Text);
            h_MonthlyReport.TransAppBM = DBUtility.ToNullableInt32(txtTransAppBM.Text);
            h_MonthlyReport.TransAppCO_ASE = DBUtility.ToNullableInt32(txtTransAppCO_ASE.Text);
            h_MonthlyReport.TransAppRM = DBUtility.ToNullableInt32(txtTransAppRM.Text);
            h_MonthlyReport.TransAppDM = DBUtility.ToNullableInt32(txtTransAppDM.Text);
            h_MonthlyReport.TransAppZM = DBUtility.ToNullableInt32(txtTransAppZM.Text);

            h_MonthlyReport.TransAppSettleLO = DBUtility.ToNullableInt32(txtTransAppSettleLO.Text);
            h_MonthlyReport.TransAppSettleABM = DBUtility.ToNullableInt32(txtTransAppSettleABM.Text);
            h_MonthlyReport.TransAppSettleBM = DBUtility.ToNullableInt32(txtTransAppSettleBM.Text);
            h_MonthlyReport.TransAppSettleCO_ASE = DBUtility.ToNullableInt32(txtTransAppSettleCO.Text);
            h_MonthlyReport.TransAppSettleRM = DBUtility.ToNullableInt32(txtTransAppSettleRM.Text);
            h_MonthlyReport.TransAppSettleDM = DBUtility.ToNullableInt32(txtTransAppSettleDM.Text);
            h_MonthlyReport.TransAppSettleZM = DBUtility.ToNullableInt32(txtTransAppSettleZM.Text);

            h_MonthlyReport.HealthCompRecruit = DBUtility.ToNullableInt32(txtHealthComplexRecruit.Text);
            h_MonthlyReport.PrintingNote = DBUtility.ToNullableString(txtPrintingNote.Text);
            h_MonthlyReport.TelephoneBill = DBUtility.ToNullableInt32(txtTelephone.Text);
            h_MonthlyReport.ElectricityBill = DBUtility.ToNullableInt32(txtElectricity.Text);
            h_MonthlyReport.MobileBill = DBUtility.ToNullableInt32(txtMobile.Text);

            h_MonthlyReport.AttendanceNoteKa = DBUtility.ToNullableString(txtAttendanceNoteKa.Text);
            h_MonthlyReport.AttendanceNoteKha = DBUtility.ToNullableString(txtAttendanceNoteKha.Text);
            h_MonthlyReport.AttendanceNoteGa = DBUtility.ToNullableString(txtAttendanceNoteGa.Text);

            h_MonthlyReport.AdministrativeStepsTransfer = DBUtility.ToNullableString(txtAdministrativeStepsTransfer.Text);
            h_MonthlyReport.AdministrativeStepsDropOut = DBUtility.ToNullableString(txtAdministrativeStepsDropOut.Text);
            h_MonthlyReport.AdministrativeStepsPunishment = DBUtility.ToNullableString(txtAdministrativeStepsPunishment.Text);
            h_MonthlyReport.AdministrativeStepsTreatment = DBUtility.ToNullableString(txtAdministrativeStepsTreatment.Text);

            h_MonthlyReport.RecruitmentNote = DBUtility.ToNullableString(txtRecruitmentNote.Text);
            return h_MonthlyReport;
        }
        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                H_MonthlyReport h_MonthlyReport=GetH_MonthlyReport();
                String desc = string.Empty;
                if (this.Type == TYPE_EDIT)
                    desc = "UPDATE H_MonthlyReport";
                else
                    desc = "INSERT H_MonthlyReport";
                this.TransactionManager = new TransactionManager(true,desc);
                if (this.Type == TYPE_EDIT)
                {
                    H_MonthlyReport.Update(this.TransactionManager, h_MonthlyReport);
                }
                else
                {
                    H_MonthlyReport.Insert(this.TransactionManager, h_MonthlyReport);
                }

                TransactionManager.Commit();

            }

            return msg;
        }
        protected override void LoadData()
        {
            for (int year = DateTime.Today.Year; year >= 2010; year--)
            {
                ddlYear.Items.Add(new ListItem(year.ToString(),year.ToString()));
            }
            if (Request.QueryString["Id"] != null)
            {
                H_MonthlyReport h_MonthlyReport = H_MonthlyReport.GetById(Convert.ToInt32(Request.QueryString["Id"]));
                if (h_MonthlyReport != null)
                {
                    this.Type = TYPE_EDIT;
                    hdnId.Value = h_MonthlyReport.Id.ToString();
                    ddlYear.SelectedValue = h_MonthlyReport.ReportDate.Year.ToString();
                    ddlMonth.SelectedValue = h_MonthlyReport.ReportDate.Month.ToString();
                    txtNote1.Text = h_MonthlyReport.EmpNote1;
                    txtNote2.Text = h_MonthlyReport.EmpNote2;
                    txtNote3.Text = h_MonthlyReport.EmpNote3;
                    txtEmpEduProgram.Text =Convert.ToString(h_MonthlyReport.EmpEduProgram);
                    txtEmpAsaHealthProgram.Text = Convert.ToString(h_MonthlyReport.EmpAsaHealthProgram);
                    txtEmpHabigongHealthProgram.Text = Convert.ToString(h_MonthlyReport.EmpHabigongHealthprogram);
                    txtExcessEmpDistrict.Text = Convert.ToString(h_MonthlyReport.ExcessEmpDistrict);
                    txtExcessEmpLO.Text = UIUtility.Format(h_MonthlyReport.ExcessEmpLO);
                    txtExcessEmpABM.Text = UIUtility.Format(h_MonthlyReport.ExcessEmpABM);
                    txtExcessEmpBM.Text = UIUtility.Format(h_MonthlyReport.ExcessEmpBM);
                    txtExcessEmpRM.Text = UIUtility.Format(h_MonthlyReport.ExcessEmpRM);
                    txtExcessEmpDM.Text = UIUtility.Format(h_MonthlyReport.ExcessEmpDM);
                    txtExcessEmpZM.Text = UIUtility.Format(h_MonthlyReport.ExcessEmpZM);
                    txtExcessEmpOthers.Text = UIUtility.Format(h_MonthlyReport.ExcessEmpOthers);

                    txtEmpDemandLO.Text = UIUtility.Format(h_MonthlyReport.EmpDemandLO);
                    txtEmpDemandABM.Text = UIUtility.Format(h_MonthlyReport.EmpDemandABM);
                    txtEmpDemandBM.Text = UIUtility.Format(h_MonthlyReport.EmpDemandBM);
                    txtEmpDemandRM.Text = UIUtility.Format(h_MonthlyReport.EmpDemandRM);
                    txtEmpDemandDM.Text = UIUtility.Format(h_MonthlyReport.EmpDemandDM);
                    txtEmpDemandZM.Text = UIUtility.Format(h_MonthlyReport.EmpDemandZM);

                    txtEmpDemandFillLO.Text = UIUtility.Format(h_MonthlyReport.EmpDemandFillLO);
                    txtEmpDemandFillABM.Text = UIUtility.Format(h_MonthlyReport.EmpDemandFillABM);
                    txtEmpDemandFillBM.Text = UIUtility.Format(h_MonthlyReport.EmpDemandFillBM);
                    txtEmpDemandFillRM.Text = UIUtility.Format(h_MonthlyReport.EmpDemandFillRM);
                    txtEmpDemandFillDM.Text = UIUtility.Format(h_MonthlyReport.EmpDemandFillDM);
                    txtEmpDemandFillZM.Text = UIUtility.Format(h_MonthlyReport.EmpDemandFillZM);

                    txtTransferNote.Text = h_MonthlyReport.TransferNote;
                    txtLessTimeTransferNote.Text = h_MonthlyReport.LessTimeTransferNote;

                    txtTransAppLO.Text = UIUtility.Format(h_MonthlyReport.TransAppLO);
                    txtTransAppABM.Text = UIUtility.Format(h_MonthlyReport.TransAppABM);
                    txtTransAppBM.Text = UIUtility.Format(h_MonthlyReport.TransAppBM);
                    txtTransAppRM.Text = UIUtility.Format(h_MonthlyReport.TransAppRM);
                    txtTransAppDM.Text = UIUtility.Format(h_MonthlyReport.TransAppDM);
                    txtTransAppZM.Text = UIUtility.Format(h_MonthlyReport.TransAppZM);
                    txtTransAppCO_ASE.Text = UIUtility.Format(h_MonthlyReport.TransAppCO_ASE);

                    txtTransAppSettleLO.Text = UIUtility.Format(h_MonthlyReport.TransAppSettleLO);
                    txtTransAppSettleABM.Text = UIUtility.Format(h_MonthlyReport.TransAppSettleABM);
                    txtTransAppSettleBM.Text = UIUtility.Format(h_MonthlyReport.TransAppSettleBM);
                    txtTransAppSettleRM.Text = UIUtility.Format(h_MonthlyReport.TransAppSettleRM);
                    txtTransAppSettleDM.Text = UIUtility.Format(h_MonthlyReport.TransAppSettleDM);
                    txtTransAppSettleZM.Text = UIUtility.Format(h_MonthlyReport.TransAppSettleZM);
                    txtTransAppSettleCO.Text = UIUtility.Format(h_MonthlyReport.TransAppSettleCO_ASE);

                    txtAttendanceNoteKa.Text = h_MonthlyReport.AttendanceNoteKa;
                    txtAttendanceNoteKha.Text = h_MonthlyReport.AttendanceNoteKha;
                    txtAttendanceNoteGa.Text = h_MonthlyReport.AttendanceNoteGa;

                    txtAdministrativeStepsTransfer.Text = h_MonthlyReport.AdministrativeStepsTransfer;
                    txtAdministrativeStepsDropOut.Text = h_MonthlyReport.AdministrativeStepsDropOut;
                    txtAdministrativeStepsPunishment.Text = h_MonthlyReport.AdministrativeStepsPunishment;
                    txtAdministrativeStepsTreatment.Text = h_MonthlyReport.AdministrativeStepsTreatment;
                    txtRecruitmentNote.Text =h_MonthlyReport.RecruitmentNote;

                    txtHealthComplexRecruit.Text = UIUtility.Format(h_MonthlyReport.HealthCompRecruit);
                    txtPrintingNote.Text = h_MonthlyReport.PrintingNote;
                    txtTelephone.Text = UIUtility.Format(h_MonthlyReport.TelephoneBill);
                    txtElectricity.Text = UIUtility.Format(h_MonthlyReport.ElectricityBill);
                    txtMobile.Text = UIUtility.Format(h_MonthlyReport.MobileBill);
                }

            }
        }

        
    }
}
