using System;
using Asa.Hrms.Web;
using Asa.Hrms.Utility;
using System.Data;
using Asa.ExcelXmlWriter;
using System.Collections.Generic;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Asa.Hrms.Data.Entity;
using Asa.Hrms.Data.Procedure;
using System.Web.UI.WebControls;

namespace Asa.Hrms.WebSite.Reports
{
    public partial class CrystalReportParameterUI : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        protected override Message Save()
        {
            throw new NotImplementedException();
        }

        protected override void LoadData()
        {
            if (this.PropertyName == "H_GRADEDESIGNATIONWISETOTALSTAFF")
            {
                trAsonDate.Visible = true;
                trStartDate.Visible = false;
                trEndDate.Visible = false;
                trPunishmentType.Visible = false;
                trEmID.Visible = false;
                rfvAsOnDate.Enabled = true;
                rvID.Enabled = false;
                rfvID.Enabled = false;
                trBranch.Visible = false;
                trYear.Visible = false;
                trMonth.Visible = false;
                trTransferType.Visible = false;
                trLetterNo.Visible = false;
                trNote.Visible = false;
                trDMNote.Visible = false;
                txtAsOnDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                rfvReportName.Enabled = false;
            }
            else if (this.PropertyName == "H_OWNDISTRICTWISEREPORT")
            {
                trAsonDate.Visible = true;
                trStartDate.Visible = false;
                trEndDate.Visible = false;
                trPunishmentType.Visible = false;
                trEmID.Visible = false;
                rfvAsOnDate.Enabled = true;
                rvID.Enabled = false;
                rfvID.Enabled = false;
                trBranch.Visible = false;
                trYear.Visible = false;
                trMonth.Visible = false;
                trTransferType.Visible = false;
                trLetterNo.Visible = false;
                trNote.Visible = false;
                trDMNote.Visible = false;
                txtAsOnDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                rfvReportName.Enabled = false;
            }
            else if (this.PropertyName == "H_ATAGLANCETOTALSTAFFPOSITION")
            {
                trAsonDate.Visible = true;
                trStartDate.Visible = false;
                trEndDate.Visible = false;
                trPunishmentType.Visible = false;
                rfvAsOnDate.Enabled = true;
                trEmID.Visible = false;
                rvID.Enabled = false;
                rfvID.Enabled = false;
                trBranch.Visible = false;
                trYear.Visible = false;
                trMonth.Visible = false;
                trTransferType.Visible = false;
                trLetterNo.Visible = false;
                trNote.Visible = false;
                trDMNote.Visible = false;
                txtAsOnDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                rfvReportName.Enabled = false;
            }
            else if (this.PropertyName == "H_ASADISTRICTWISETOTALSTAFF")
            {
                trAsonDate.Visible = true;
                trStartDate.Visible = false;
                trEndDate.Visible = false;
                trPunishmentType.Visible = false;
                trEmID.Visible = false;
                rfvAsOnDate.Enabled = true;
                rvID.Enabled = false;
                rfvID.Enabled = false;
                trBranch.Visible = false;
                trYear.Visible = false;
                trMonth.Visible = false;
                trTransferType.Visible = false;
                trLetterNo.Visible = false;
                trNote.Visible = false;
                trDMNote.Visible = false;
                txtAsOnDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                rfvReportName.Enabled = false;
            }
            else if (this.PropertyName == "H_PUNISHMENTREPORT")
            {
                trAsonDate.Visible = false;
                trStartDate.Visible = false;
                trEndDate.Visible = false;
                trPunishmentType.Visible = true;
                trEmID.Visible = true;
                rfvAsOnDate.Enabled = false;
                rvID.Enabled = true;
                rfvID.Enabled = true;
                trBranch.Visible = false;
                trYear.Visible = false;
                trMonth.Visible = false;
                trTransferType.Visible = false;
                trLetterNo.Visible = false;
                trNote.Visible = false;
                trDMNote.Visible = false;
                rfvReportName.Enabled = false;
            }
            else if (this.PropertyName == "H_GOVTDISTRICTWISEBRANCH")
            {
                trAsonDate.Visible = false;
                trStartDate.Visible = false;
                trEndDate.Visible = false;
                trPunishmentType.Visible = false;
                trEmID.Visible = false;
                rfvAsOnDate.Enabled = false;
                rvID.Enabled = false;
                rfvID.Enabled = false;
                trBranch.Visible = true;
                trYear.Visible = false;
                trMonth.Visible = false;
                trTransferType.Visible = false;
                trLetterNo.Visible = false;
                trNote.Visible = false;
                trDMNote.Visible = false;
                lblBranchReport.Text = "Govt. District wise Branch";
                rfvReportName.Enabled = false;
            }
            else if (this.PropertyName == "H_ASADISTRICTWISEBRANCH")
            {
                trAsonDate.Visible = false;
                trStartDate.Visible = false;
                trEndDate.Visible = false;
                trPunishmentType.Visible = false;
                trEmID.Visible = false;
                rfvAsOnDate.Enabled = false;
                rvID.Enabled = false;
                rfvID.Enabled = false;
                trBranch.Visible = true;
                trYear.Visible = false;
                trMonth.Visible = false;
                trTransferType.Visible = false;
                trLetterNo.Visible = false;
                trNote.Visible = false;
                trDMNote.Visible = false;
                lblBranchReport.Text = "ASA District wise Branch";
                rfvReportName.Enabled = false;
            }
            else if (this.PropertyName == "H_EMPLOYEERESUME")
            {
                trAsonDate.Visible = false;
                trStartDate.Visible = false;
                trEndDate.Visible = false;
                trPunishmentType.Visible = false;
                trEmID.Visible = true;
                rfvAsOnDate.Enabled = false;
                rvID.Enabled = true;
                rfvID.Enabled = true;
                trBranch.Visible = false;
                trYear.Visible = false;
                trMonth.Visible = false;
                trTransferType.Visible = false;
                trLetterNo.Visible = false;
                trNote.Visible = false;
                trDMNote.Visible = false;
                rfvReportName.Enabled = false;
            }
            else if (this.PropertyName == "H_MONTHLYREPORT")
            {
                trAsonDate.Visible = false;
                trStartDate.Visible = false;
                trEndDate.Visible = false;
                trPunishmentType.Visible = false;
                trEmID.Visible = false;
                rfvAsOnDate.Enabled = false;
                rvID.Enabled = false;
                rfvID.Enabled = false;
                trBranch.Visible = false;
                trYear.Visible = true;
                trMonth.Visible = true;
                trTransferType.Visible = false;
                trLetterNo.Visible = false;
                trNote.Visible = false;
                trDMNote.Visible = false;
                rfvReportName.Enabled = false;
                for (int year = DateTime.Today.Year; year >= 2010; year--)
                {
                    ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem(year.ToString(), year.ToString()));
                }
            }
            else if (this.PropertyName == "H_TRANSFERLETTER")
            {
                trAsonDate.Visible = false;
                trStartDate.Visible = false;
                trEndDate.Visible = false;
                trPunishmentType.Visible = false;
                trEmID.Visible = false;
                rfvAsOnDate.Enabled = false;
                rvID.Enabled = false;
                rfvID.Enabled = false;
                trBranch.Visible = false;
                trYear.Visible = false;
                trMonth.Visible = false;
                trTransferType.Visible = true;
                trLetterNo.Visible = true;
                trNote.Visible = true;
                trDMNote.Visible = true;
                IList<H_LetterFormats> formatList = H_LetterFormats.FindByLetterType((Int32)H_LetterFormats.LetterTypes.Transfer_Letter, "SortOrder");
                ddlReportName.DataSource = formatList;
                ddlReportName.DataBind();
                ddlReportName.Items.Insert(0, new ListItem { Value="0",Text="Select Report"});
                rfvReportName.Enabled = true;
                IList<H_Duplication> dupList = H_Duplication.FindAll("SortOrder");
                chkDuplication.Items.Clear();
                foreach (H_Duplication dup in dupList)
                {
                    chkDuplication.DataTextField = "Name";
                    chkDuplication.DataValueField = "Id";
                    chkDuplication.DataSource = dupList;
                    chkDuplication.DataBind();
                }
            }
            else
            {
            }
        }
        protected override void HandleSpecialCommand(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {
            this.Validate();

            if (this.IsValid)
            {
                switch (e.Item.Value)
                {
                    case "PRINT":
                        this.PrintData();
                        break;
                    default:
                        this.HandleSpecialCommand(sender, e);
                        break;
                }
            }
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
            if (this.PropertyName == "H_TRANSFERLETTER")
            {
                if (string.IsNullOrEmpty(txtLetterNo.Text))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter Letter No";
                    return msg;
                }
                if (chkDuplication.Items.Cast<ListItem>().Where(li => li.Selected).Count() == 0)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "At Least One Duplication(CC) should be selected";
                    return msg;
                }
            }
            return msg;
        }
        protected override void PrintData()
        {
            Message msg = this.Validate();
            if (msg.Type != MessageType.Information)
            {
                ShowUIMessage(msg);
                return;
            }
            string query = string.Empty;
            string reportName = string.Empty;
            DataTable dt = null;
            DataSet ds = null;
            if (this.PropertyName == "H_GRADEDESIGNATIONWISETOTALSTAFF")
            {

                dt = GradeDesignationWiseStaffProcedure.GetDataSet(DBUtility.ToDateTime(txtAsOnDate.Text));
                reportName = "GradeDesigWiseTotalStaff.rpt";
            }
            if (this.PropertyName == "H_OWNDISTRICTWISEREPORT")
            {
                dt = OwnDistrictWiseTotalStaffProcedure.GetDataSet(DBUtility.ToDateTime(txtAsOnDate.Text));
                reportName = "OwnDistrictWiseTotalStaff.rpt";
            }
            if (this.PropertyName == "H_ATAGLANCETOTALSTAFFPOSITION")
            {
                dt = AtaGlanceTotalStaffPositionProcedure.GetDataSet(DBUtility.ToDateTime(txtAsOnDate.Text));
                reportName = "AtAGlanceTotalStaffPosition.rpt";
            }
            if (this.PropertyName == "H_ASADISTRICTWISETOTALSTAFF")
            {
                dt = AsaDistrictWiseTotalStaffProcedure.GetDataSet(DBUtility.ToDateTime(txtAsOnDate.Text));
                reportName = "AsaDistrictWiseTotalStaff.rpt";
            }
            if (this.PropertyName == "H_PUNISHMENTREPORT" && ddtPunishment.SelectedValue == "1")
            {
                dt = EmployeePunishmentReportProcedure.GetDataSet(Convert.ToInt32(txtEmpIdD.Text), Convert.ToInt32(ddtPunishment.SelectedValue));
                reportName = "PenaltyReport.rpt";
            }
            if (this.PropertyName == "H_PUNISHMENTREPORT" && ddtPunishment.SelectedValue == "2")
            {
                dt = EmployeePunishmentReportProcedure.GetDataSet(Convert.ToInt32(txtEmpIdD.Text), Convert.ToInt32(ddtPunishment.SelectedValue));
                reportName = "IncreamentHeldupReport.rpt";
            }
            if (this.PropertyName == "H_PUNISHMENTREPORT" && ddtPunishment.SelectedValue == "3")
            {
                dt = EmployeePunishmentReportProcedure.GetDataSet(Convert.ToInt32(txtEmpIdD.Text), Convert.ToInt32(ddtPunishment.SelectedValue));
                reportName = "WarningReport.rpt";
            }
            if (this.PropertyName == "H_PUNISHMENTREPORT" && ddtPunishment.SelectedValue == "4")
            {
                dt = EmployeePunishmentReportProcedure.GetDataSet(Convert.ToInt32(txtEmpIdD.Text), Convert.ToInt32(ddtPunishment.SelectedValue));
                reportName = "LeaveReport.rpt";
            }
            if (this.PropertyName == "H_GOVTDISTRICTWISEBRANCH")
            {
                Asa.Hrms.Data.TransactionManager tm = new Asa.Hrms.Data.TransactionManager(false);
                string strquery = "Select div.Name AS Division,dis.Name AS District ,count(b.Id) AS Branch from Division div INNER JOIN District dis ON div.Id=dis.DivisionId INNER JOIN Thana t ON dis.Id=t.DistrictId INNER JOIN Branch b ON t.Id=b.ThanaId and b.Status=1 GROUP BY div.Name,dis.Name ORDER BY div.Name,dis.Name";
                dt = tm.GetDataSet(strquery).Tables[0];
                reportName = "GovtDistrictWiseBranch.rpt";
            }
            if (this.PropertyName == "H_ASADISTRICTWISEBRANCH")
            {
                Asa.Hrms.Data.TransactionManager tm = new Asa.Hrms.Data.TransactionManager(false);
                string strquery = "Select s.Name AS District,count(b.Id) AS Branch from Subzone s INNER JOIN Region r ON s.Id=r.SubzoneId INNER JOIN Branch b ON r.Id=b.RegionId and b.Status=1 GROUP BY s.Name ORDER BY s.Name";
                dt = tm.GetDataSet(strquery).Tables[0];
                reportName = "ASADistrictWiseBranch.rpt";
            }
            if (this.PropertyName == "H_EMPLOYEERESUME")
            {
                dt = Employee_Resume_Procedure.GetDataSet(Convert.ToInt32(txtEmpIdD.Text));
                reportName = "EmployeeResume.rpt";
            }
            if (this.PropertyName == "H_MONTHLYREPORT")
            {
                DateTime startdate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1);
                dt = Monthly_Report_Procedure.GetDataSet(startdate);
                reportName = "H_MonthlyReport.rpt";
            }
            if (this.PropertyName == "H_TRANSFERLETTER")
            {
                IList<H_EmployeeTransferHistory> h_Trans = H_EmployeeTransferHistory.Find("LetterNo='" + txtLetterNo.Text + "'", "");
                dt = new DataTable();
                dt.Columns.Add("Name", typeof(String));
                dt.Columns.Add("Code", typeof(int));
                dt.Columns.Add("PresentBranch", typeof(String));
                dt.Columns.Add("PresentDistrict", typeof(String));
                dt.Columns.Add("DestinationBranch", typeof(String));
                dt.Columns.Add("DestinationDistrict", typeof(String));
                dt.Columns.Add("LetterNo", typeof(String));
                dt.Columns.Add("JoiningDate", typeof(DateTime));
                dt.Columns.Add("LetterDate", typeof(DateTime));
                dt.Columns.Add("Duplication", typeof(String));
                dt.Columns.Add("InsideAddress", typeof(String));

                if (h_Trans.Count > 0)
                {
                    List<string> selectedValues = chkDuplication.Items.Cast<ListItem>()
                                                    .Where(li => li.Selected)
                                                    .Select(li => li.Value)
                                                    .ToList();
                    string duplication = string.Empty;
                    int SL = 1;
                    foreach (string str in selectedValues)
                    {
                        duplication = duplication + "@"+NumberInBangla(SL)+". " + H_Duplication.GetById(Convert.ToInt32(str)).Name;
                        SL = SL + 1;
                    }
                    duplication = duplication.Substring(1);
                    foreach (H_EmployeeTransferHistory th in h_Trans)
                    {
                        H_Employee emp = H_Employee.GetById(th.H_EmployeeId);
                        Branch sourceBranch = Branch.GetById(th.SourceBranchId);
                        Subzone sourceSubzone = Subzone.GetById(Region.GetById(sourceBranch.RegionId).SubzoneId);
                        Branch destinationBranch = Branch.GetById(th.DestinationBranchId);
                        Subzone destinationSubzone = Subzone.GetById(Region.GetById(destinationBranch.RegionId).SubzoneId);
                        dt.Rows.Add(emp.Name, emp.Code, sourceBranch.Name, sourceSubzone.Name, destinationBranch.Name, destinationSubzone.Name, th.LetterNo, th.JoiningDate, th.LetterDate, duplication,"");
                    }
                }
                else
                {

                }
                reportName = "H_TransferLetter.rpt";
            }
            if (dt.Rows.Count <= 0)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";

                ShowUIMessage(msg);
                return;
            }

            ReportDocument rd = new ReportDocument();

            rd.Load(Server.MapPath("~/Reports/" + reportName));
            if (this.PropertyName == "H_TRANSFERLETTER")
            {
                string designation = string.Empty;
                foreach (DataRow dr in dt.Rows)
                {
                    H_Employee emp = H_Employee.GetByCode(dr["Code"].ToString());
                    H_EmployeeDesignation ed=H_EmployeeDesignation.Get("H_EmployeeId="+emp.Id+" AND EndDate='2099-12-31'");
                    H_Designation desg = H_Designation.GetById(ed.H_DesignationId);
                    if (!designation.Split(',').Any(d => d.Equals(desg.BanglaName)))
                    {
                        designation = designation + desg.BanglaName + ",";
                    }
                }
                if (designation.Length > 1)
                {
                    designation = designation.Substring(0, designation.Length - 1);
                }
                H_LetterFormats letter = H_LetterFormats.GetById(Convert.ToInt32(ddlReportName.SelectedValue));
                foreach (DataRow row in dt.Rows)
                {
                    row["InsideAddress"] = String.Format(letter.InsideAddress, designation);
                }
                
                //TextObject txtInsideAddress = (TextObject)rd.ReportDefinition.ReportObjects["txtInsideAddress"];
                TextObject txtSubject = (TextObject)rd.ReportDefinition.ReportObjects["txtSubject"];
                TextObject txtBody = (TextObject)rd.ReportDefinition.ReportObjects["txtBody"];
                TextObject txtClosingText = (TextObject)rd.ReportDefinition.ReportObjects["txtClosingText"];
                TextObject txtSalutation = (TextObject)rd.ReportDefinition.ReportObjects["txtSalutation"];
                TextObject txtNote = (TextObject)rd.ReportDefinition.ReportObjects["txtNote"];
                TextObject txtDMNote = (TextObject)rd.ReportDefinition.ReportObjects["txtDMNote"];
                TextObject txtLetterDate = (TextObject)rd.ReportDefinition.ReportObjects["txtLetterDate"];
                TextObject txtDuplication = (TextObject)rd.ReportDefinition.ReportObjects["txtDuplication"];
                TextObject txtSignatory = (TextObject)rd.ReportDefinition.ReportObjects["txtSignatory"];
                TextObject txtDesignation = (TextObject)rd.ReportDefinition.ReportObjects["txtDesignation"];
                txtSignatory.Text = letter.Signatory;
                txtDesignation.Text = letter.Designation;
                if (dt.Rows.Count > 1)
                {
                    txtSalutation.Text = "প্রিয় সহকর্মীবৃন্দ";
                }
                else
                {
                    txtSalutation.Text = "প্রিয় সহকর্মী";
                }
                txtBody.Text = String.Format(letter.LetterBody, UIUtility.DateTimeInBangla(Convert.ToDateTime(dt.Rows[0]["JoiningDate"]), false));// "সংস্থার সিদ্ধান্ত / আপনাদের আবেদন যথাযথ কর্তৃপক্ষের অনুমোদনক্রমে আপনাদেরকে বদলী করা হলো- যা নিম্ন ছকে উল্লেখ করা হলো । আপনাদের এ বদলী " + UIUtility.DateTimeInBangla(Convert.ToDateTime(dt.Rows[0]["JoiningDate"]), false) + " ইং তারিখ থেকে কার্যকারী হবে । অপনারা যথাসময়ে সংশ্লিষ্ট কর্তৃপক্ষের নিকট সমস্ত দায়িত্ব হস্তান্তর করে নির্ধরিত তারিখে বদলীকৃত কর্মস্থলে যোগদান করে সংশ্লিষ্ট কর্তৃপক্ষের নিকট থেকে দায়িত্ব বুঝে নিবেন ।";
                txtClosingText.Text = letter.Conclusion;
                //txtInsideAddress.Text = letter.InsideAddress;
                txtSubject.Text = letter.Subject;
               
                txtNote.Text = this.txtNote.Text;
                txtDMNote.Text = this.txtDMNote.Text;
                if (!String.IsNullOrEmpty(this.txtDMNote.Text))
                {
                    txtDMNote.Border.BorderColor = System.Drawing.Color.Black;
                    txtDMNote.Border.BottomLineStyle = LineStyle.SingleLine;
                    txtDMNote.Border.RightLineStyle = LineStyle.SingleLine;
                    txtDMNote.Border.TopLineStyle = LineStyle.SingleLine;
                    txtDMNote.Border.LeftLineStyle = LineStyle.SingleLine;
                }
                else
                {
                    //linDM.LineColor = System.Drawing.Color.White;
                }
                if (String.IsNullOrEmpty(this.txtNote.Text))
                {
                    TextObject txtNoteLabel = (TextObject)rd.ReportDefinition.ReportObjects["txtNoteLabel"];
                    txtNoteLabel.Text = "";
                }
                txtLetterDate.Text = UIUtility.DateTimeInBangla(Convert.ToDateTime(dt.Rows[0]["LetterDate"]), true) + " ইং";
                TextObject txtUser = (TextObject)rd.ReportDefinition.ReportObjects["txtUser"];
                txtUser.Text = "Printed by- " + User.Identity.Name +" on "+DateTime.Today.Date.ToString("dd/MM/yyyy");              
                
            }

            rd.SetDataSource(dt);
            Session["ReportType"] = Convert.ToInt32(ddlFormat.SelectedValue);
            ReportUtility.ViewReport(this, rd, false, false);
        }

        protected void rvAsOnDate_Init(object sender, EventArgs e)
        {
            ((System.Web.UI.WebControls.RangeValidator)sender).MaximumValue = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }
        private String NumberInBangla(Int32 num)
        {
            string retDate = num.ToString();
            
            retDate = retDate.Replace("0", "০")
                .Replace("1", "১")
                .Replace("2", "২")
                .Replace("3", "৩")
                .Replace("4", "৪")
                .Replace("5", "৫")
                .Replace("6", "৬")
                .Replace("7", "৭")
                .Replace("8", "৮")
                .Replace("9", "৯");
            
            return retDate;
        }

    }
}
