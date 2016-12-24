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
    public partial class H_LetterReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                UIUtility.LoadEnums(ddlReportType, typeof(H_LetterFormats.LetterTypes), false, false, true);
                ddlReportType.Items.Insert(0, new ListItem { Value = "0", Text = "Select Report Type" });
                ddlReportType.SelectedValue = "0";
                IList<H_Duplication> dupList = H_Duplication.FindAll("SortOrder");
                chkDuplication.Items.Clear();
                chkDuplication.DataTextField = "Name";
                chkDuplication.DataValueField = "Id";
                chkDuplication.DataSource = dupList;
                chkDuplication.DataBind();

                
            }
        }

        private IList<Branch> GetBranchBySubzoneId(int subzoneId)
        {
            return Branch.Find(" Status=1 AND RegionId IN (Select Id from Region Where SubzoneId="+subzoneId+")","Name");
        }
        //protected override string GetListPageUrl()
        //{
        //    throw new NotImplementedException();
        //}
        
        //protected override Message Save()
        //{
        //    throw new NotImplementedException();
        //}
        //protected override void LoadData()
        //{
        //    UIUtility.LoadEnums(ddlReportType, typeof(H_LetterFormats.LetterTypes), false, false, true);
        //    ddlReportType.Items.Insert(0, new ListItem { Value = "0", Text = "Select Report Type" });
        //    ddlReportType.SelectedValue = "0";
        //    IList<H_Duplication> dupList = H_Duplication.FindAll("SortOrder");
        //    chkDuplication.Items.Clear();
        //    chkDuplication.DataTextField = "Name";
        //    chkDuplication.DataValueField = "Id";
        //    chkDuplication.DataSource = dupList;
        //    chkDuplication.DataBind();
        //}

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue != "0")
            {
                IList<H_LetterFormats> formatList = H_LetterFormats.FindByLetterType(Convert.ToInt32(ddlReportType.SelectedValue), "");
                ddlReportName.DataSource = formatList;
                ddlReportName.DataBind();
                ddlReportName.Items.Insert(0, new ListItem { Value = "0", Text = "Select Report Format" });
               
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

            return msg;
        }

        /*protected override void PrintData()
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

            if (ddlReportType.SelectedValue == ((int)H_LetterFormats.LetterTypes.Transfer_Letter).ToString())
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
                        duplication = duplication + "@" + NumberInBangla(SL) + ". " + H_Duplication.GetById(Convert.ToInt32(str)).Name;
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
                        dt.Rows.Add(emp.Name, emp.Code, sourceBranch.Name, sourceSubzone.Name, destinationBranch.Name, destinationSubzone.Name, th.LetterNo, th.JoiningDate, th.LetterDate, duplication);
                    }
                }

                reportName = "H_TransferLetter.rpt";
            }
            else if (ddlReportType.SelectedValue == ((int)H_LetterFormats.LetterTypes.Warning_Letter).ToString())
            {
                H_EmployeeWarning warning = H_EmployeeWarning.Get("LetterNo='" + txtLetterNo.Text + "'");
                H_Employee employee = H_Employee.GetById(warning.H_EmployeeId);
                reportName = "H_WarningLetter.rpt";
            }
            else if (ddlReportType.SelectedValue == ((int)H_LetterFormats.LetterTypes.Increment_Heldup_Letter).ToString())
            {
                reportName = "H_IncrementHeldupLetter.rpt";
            }
            else if (ddlReportType.SelectedValue == ((int)H_LetterFormats.LetterTypes.Penalty_Letter).ToString())
            {
                reportName = "H_PenaltyLetter.rpt";
            }
            else
            {
            }
            if (dt.Rows.Count <= 0)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "No Data Found, Incorrect Letter No";

                ShowUIMessage(msg);
                return;
            }

            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~/Reports/" + reportName));

            H_LetterFormats letter = H_LetterFormats.GetById(Convert.ToInt32(ddlReportName.SelectedValue));
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
            if (ddlReportType.SelectedValue == ((int)H_LetterFormats.LetterTypes.Transfer_Letter).ToString())
            {
                txtBody.Text = String.Format(letter.LetterBody, UIUtility.DateTimeInBangla(Convert.ToDateTime(dt.Rows[0]["JoiningDate"]), false));
            }
            else
            {
                txtBody.Text = letter.LetterBody;
            }
            txtClosingText.Text = letter.Conclusion;
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
            txtUser.Text = "Printed by- " + User.Identity.Name + " on " + DateTime.Today.Date.ToString("dd/MM/yyyy");

            rd.SetDataSource(dt);
            Session["ReportType"] = Convert.ToInt32(ddlFormat.SelectedValue);
            ReportUtility.ViewReport(this, rd, false, false);
        }
         */

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

        protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFormat.SelectedValue != "0")
            {
                H_LetterFormats format = H_LetterFormats.GetById(Convert.ToInt32(ddlFormat.SelectedValue));
                txtSubject.Text = format.Subject;
                txtBody.Text = format.LetterBody;
                txtConlution.Text = format.Conclusion;
                txtAddressTo.Text = format.InsideAddress;
                LoadEmptyRow();
            }
            

        }

        protected void lbEmpSearch_Click(object sender, EventArgs e)
        {
            TextBox txtCode = (TextBox)gvTransfer.FooterRow.FindControl("txtCode");
            TextBox txtName = (TextBox)gvTransfer.FooterRow.FindControl("txtName");
            TextBox txtDesignation = (TextBox)gvTransfer.FooterRow.FindControl("txtDesignation");
            TextBox txtPresentDistrict = (TextBox)gvTransfer.FooterRow.FindControl("txtPresentDistrict");
            TextBox txtPresentBranch = (TextBox)gvTransfer.FooterRow.FindControl("txtPresentBranch");
            if (!String.IsNullOrEmpty(txtCode.Text))
            {
                H_Employee emp = H_Employee.GetByCode(txtCode.Text);
                H_EmployeeDesignation ed = H_EmployeeDesignation.Find("H_EmployeeId=" + emp.Id, "EndDate Desc")[0];
                H_Designation desg = H_Designation.GetById(ed.H_DesignationId);
                H_EmployeeBranch eb = H_EmployeeBranch.Find("H_EmployeeId=" + emp.Id, "EndDate Desc")[0];
                Branch branch = Branch.GetById(eb.BranchId);
                Subzone subzone = Subzone.GetById(Region.GetById(branch.RegionId).SubzoneId);

                txtName.Text = emp.Name;
                txtDesignation.Text = desg.Name;
                txtPresentDistrict.Text = subzone.Name;
                txtPresentBranch.Text = branch.Name;
            }
        }
        private void LoadEmptyRow()
        {
            if (Session["TransferList"] == null || ((DataTable)Session["TransferList"]).Rows.Count == 0)
            {
                DataTable dt = GetDataTable();
                DataRow dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = "";
                dr[2] = 1;
                dr[3] = "";
                dr[4] = "";
                dr[5] = 1;
                dr[6] = "";
                dr[7] = "";
                dr[8] = 1;
                dr[9] = "";

                dt.Rows.Add(dr);
                gvTransfer.DataSource = dt;
                gvTransfer.DataBind();
               // gvTransfer.Rows[0].Visible = false;

            }
            else
            {
                DataTable dt = (DataTable)Session["TransferList"];
                gvTransfer.DataSource = dt;
                gvTransfer.DataBind();
                gvTransfer.Rows[0].Visible = true;
            }
            DropDownList ddlDistrict = (DropDownList)gvTransfer.FooterRow.FindControl("ddlDistrict");
            DropDownList ddlBranch = (DropDownList)gvTransfer.FooterRow.FindControl("ddlBranch");

            if (ddlDistrict != null)
            {
                IList<Subzone> disList = Subzone.Find(" Status=1", "Name");
                ddlDistrict.DataSource = disList;
                ddlDistrict.DataBind();

                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlDistrict.SelectedValue));
                ddlBranch.DataBind();
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SubzoneId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlBranch = (DropDownList)gvTransfer.FooterRow.FindControl("ddlBranch");
            if (ddlBranch != null)
            {
                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(SubzoneId));
                ddlBranch.DataBind();

            }
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(Int32));
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("Code", typeof(Int32));
            dt.Columns.Add("Designation", typeof(String));
            dt.Columns.Add("PresentDistrict", typeof(String));
            dt.Columns.Add("PresentBranchId", typeof(Int32));
            dt.Columns.Add("PresentBranch", typeof(String));
            dt.Columns.Add("DestinationDistrict", typeof(String));
            dt.Columns.Add("DestinationBranchId", typeof(Int32));
            dt.Columns.Add("DestinationBranch", typeof(String));

            return dt;
        }

        protected void gvTransfer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "addrow")
            {
                TextBox txtCode = (TextBox)gvTransfer.FooterRow.FindControl("txtCode");
                TextBox txtName = (TextBox)gvTransfer.FooterRow.FindControl("txtName");
                TextBox txtDesignation = (TextBox)gvTransfer.FooterRow.FindControl("txtDesignation");
                TextBox txtPresentDistrict = (TextBox)gvTransfer.FooterRow.FindControl("txtPresentDistrict");
                TextBox txtPresentBranch = (TextBox)gvTransfer.FooterRow.FindControl("txtPresentBranch");
                DropDownList ddlBranch = (DropDownList)gvTransfer.FooterRow.FindControl("ddlBranch");
                if (!String.IsNullOrEmpty(txtCode.Text))
                {
                    H_Employee emp = H_Employee.GetByCode(txtCode.Text);
                    H_EmployeeDesignation ed = H_EmployeeDesignation.Find("H_EmployeeId=" + emp.Id, "EndDate Desc")[0];
                    H_Designation desg = H_Designation.GetById(ed.H_DesignationId);
                    H_EmployeeBranch eb = H_EmployeeBranch.Find("H_EmployeeId=" + emp.Id, "EndDate Desc")[0];
                    Branch branch = Branch.GetById(eb.BranchId);
                    Subzone subzone = Subzone.GetById(Region.GetById(branch.RegionId).SubzoneId);
                    Branch destinationBranch = Branch.GetById(Convert.ToInt32(ddlBranch.SelectedValue));
                    Subzone destinationSubzone = Subzone.GetById(Region.GetById(destinationBranch.RegionId).SubzoneId);


                    DataTable dt;
                    if (Session["TransferList"] == null || ((DataTable)Session["TransferList"]).Rows.Count == 0)
                    {
                        dt = GetDataTable();
                    }
                    else
                    {
                        dt = (DataTable)Session["TransferList"];
                        Session.Remove("TransferList");
                    }
                    
                    DataRow dr = dt.NewRow();
                    dr[0] =Convert.ToInt32( emp.Id);
                    dr[1] = emp.Name;
                    dr[2] = emp.Code;
                    dr[3] = desg.Name;
                    dr[4] = subzone.Name;
                    dr[5] = branch.Id;
                    dr[6] = branch.Name;
                    dr[7] = destinationSubzone.Name;
                    dr[8] = destinationBranch.Id;
                    dr[9] = destinationBranch.Name;
                    dt.Rows.Add(dr);

                    Session["TransferList"] = dt;
                    LoadEmptyRow();
                }
            }
            if (e.CommandName == "deleterow")
            {
                int code=Convert.ToInt32(e.CommandArgument);
                DataTable dt = (DataTable)Session["TransferList"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Code"].ToString() == code.ToString())
                        dr.Delete();
                }
                dt.AcceptChanges();
                Session["TransferList"] = dt;
                LoadEmptyRow();
            }
        }

       
       

       
    }
}
