using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;
using GITS.Hrms.WebSite.Reports;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_TransferAdd : AddPage
    {
        protected override string PropertyName
        {
            get
            {
                return "H_EMPLOYEETRANSFER LETTER";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IList<H_LetterFormats> formatList = H_LetterFormats.FindByLetterType(1, "SortOrder");
                ddlReportName.DataSource = formatList;
                ddlReportName.DataBind();
                ddlReportName.Items.Insert(0, new ListItem { Value = "0", Text = "Select Report Format" });
            }
            RegisterPostBackControl();
        }

        private void RegisterPostBackControl()
        {
            foreach (GridViewRow row in gvDesignation.Rows)
            {
                LinkButton lnkFull = row.FindControl("linkDelete") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);
            }
        }

        protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportName.SelectedValue != "0")
            {
                //if (Session["TransferList"] != null)
                //    Session.Remove("TransferList");
                H_LetterFormats format = H_LetterFormats.GetById(Convert.ToInt32(ddlReportName.SelectedValue));
                txtSubject.Text = format.Subject;
                txtBody.Text = format.LetterBody;
                txtConlution.Text = format.Conclusion;
                txtAddressTo.Text = format.InsideAddress;

                //LoadGridView();
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
            // dt.Columns.Add("Status", typeof(int));

            return dt;
        }
        private void LoadGridView()
        {
            if (Session["TransferList"] == null || ((DataTable)Session["TransferList"]).Rows.Count == 0)
            {
                DataTable dt = GetDataTable();
                DataRow dr = dt.NewRow();
                //dr[0] = 1;
                //dr[1] = "Rofiq";
                //dr[2] = 41581;
                //dr[3] = "SE";
                //dr[4] = "Magura";
                //dr[5] = 1;
                //dr[6] = "Magura";
                //dr[7] = "dfhgfh";
                //dr[8] = 1;
                //dr[9] = "ghjgh";

                dt.Rows.Add(dr);
                gvDesignation.DataSource = dt;
                gvDesignation.DataBind();
                gvDesignation.Rows[0].Visible = false;

            }
            else
            {
                DataTable dt = (DataTable)Session["TransferList"];
                gvDesignation.DataSource = dt;
                gvDesignation.DataBind();
                gvDesignation.Rows[0].Visible = true;
            }
            DropDownList ddlDistrict = (DropDownList)gvDesignation.FooterRow.FindControl("ddlDistrict");
            DropDownList ddlBranch = (DropDownList)gvDesignation.FooterRow.FindControl("ddlBranch");

            if (ddlDistrict != null)
            {
                IList<Subzone> disList = Subzone.Find(" Status=1", "Name");

                ddlDistrict.DataSource = disList;
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem {Text="Select District",Value="0" });

                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlDistrict.SelectedValue));
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem { Text = "Select Branch", Value = "0" });
            }
        }
        private IList<Branch> GetBranchBySubzoneId(int subzoneId)
        {
            return Branch.Find(" Status=1 AND RegionId IN (Select Id from Region Where SubzoneId=" + subzoneId + ")", "Name");
        }

        protected void gvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "addrow")
            {
                TextBox txtCode = (TextBox)gvDesignation.FooterRow.FindControl("txtCode");
                TextBox txtName = (TextBox)gvDesignation.FooterRow.FindControl("txtName");
                TextBox txtDesignation = (TextBox)gvDesignation.FooterRow.FindControl("txtDesignation");
                TextBox txtPresentDistrict = (TextBox)gvDesignation.FooterRow.FindControl("txtPresentDistrict");
                TextBox txtPresentBranch = (TextBox)gvDesignation.FooterRow.FindControl("txtPresentBranch");
                DropDownList ddlBranch = (DropDownList)gvDesignation.FooterRow.FindControl("ddlBranch");
                if (ddlBranch.SelectedValue == "0")
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Select Correct Branch then add";
                    ShowUiMessage(msg);
                    return;
                }
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
                    dr[0] = Convert.ToInt32(emp.Id);
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
                    LoadGridView();
                }
            }
            if (e.CommandName == "deleterow")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                DataTable dt = (DataTable)Session["TransferList"];

                dt.Rows.Cast<DataRow>().Where(r => Convert.ToInt32(r.ItemArray[0]) == id)
                                       .ToList()
                                       .ForEach(r => r.Delete());

                Session["TransferList"] = dt;
                LoadGridView();
            }
        }

        protected void lbEmpSearch_Click(object sender, EventArgs e)
        {
            TextBox txtCode = (TextBox)gvDesignation.FooterRow.FindControl("txtCode");
            TextBox txtName = (TextBox)gvDesignation.FooterRow.FindControl("txtName");
            TextBox txtDesignation = (TextBox)gvDesignation.FooterRow.FindControl("txtDesignation");
            TextBox txtPresentDistrict = (TextBox)gvDesignation.FooterRow.FindControl("txtPresentDistrict");
            TextBox txtPresentBranch = (TextBox)gvDesignation.FooterRow.FindControl("txtPresentBranch");
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
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SubzoneId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlBranch = (DropDownList)gvDesignation.FooterRow.FindControl("ddlBranch");
            if (ddlBranch != null)
            {
                if (SubzoneId != "0")
                {
                    ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(SubzoneId));
                    ddlBranch.DataBind();
                    ddlBranch.Items.Insert(0, new ListItem { Text = "Select Branch", Value = "0" });
                }

            }
        }
        protected override void LoadData()
        {
            Session.Remove("TransferList");
            UIUtility.LoadEnums(ddlType, typeof(H_EmployeeTransfer.Types), false, false, false);
            IList<H_Duplication> dupList = H_Duplication.FindAll("SortOrder");
            chkDuplication.Items.Clear();

            chkDuplication.DataTextField = "Name";
            chkDuplication.DataValueField = "Id";
            chkDuplication.DataSource = dupList;
            chkDuplication.DataBind();
            LoadGridView();

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                H_EmployeeTransferHistory h_History = H_EmployeeTransferHistory.GetById(Convert.ToInt32(hdnId.Value));

                if (h_History != null)
                {
                    Type = TYPE_EDIT;
                    txtLetterNo.Text = h_History.LetterNo;
                    txtLetterDate.Text = UIUtility.Format(h_History.LetterDate);
                    txtTransferDate.Text = UIUtility.Format(h_History.JoiningDate);

                    ddlType.SelectedValue = ((Int32)h_History.Type).ToString();
                    IList<H_EmployeeTransferHistory> list = H_EmployeeTransferHistory.Find("LetterNo='" + h_History.LetterNo + "'", "");
                    var datble = from e in list
                                 select new
                                 {
                                     Id = e.H_EmployeeId,
                                     Name = H_Employee.GetById(e.H_EmployeeId).Name,
                                     Code = H_Employee.GetById(e.H_EmployeeId).Code,
                                     Designation = H_Designation.GetById(H_EmployeeDesignation.FindByH_EmployeeId(e.H_EmployeeId, "EndDate DESC")[0].H_DesignationId).Name,
                                     PresentDistrict = Subzone.GetById(Region.GetById(Branch.GetById(e.SourceBranchId).RegionId).SubzoneId).Name,
                                     PresentBranchId = e.SourceBranchId,
                                     PresentBranch = Branch.GetById(e.SourceBranchId).Name,
                                     DestinationDistrict = Subzone.GetById(Region.GetById(Branch.GetById(e.DestinationBranchId).RegionId).SubzoneId).Name,
                                     DestinationBranchId = e.DestinationBranchId,
                                     DestinationBranch = Branch.GetById(e.DestinationBranchId).Name

                                 };
                    if(list[0].H_LetterFormatsId !=null)
                    {
                        ddlReportName.SelectedValue = list[0].H_LetterFormatsId.ToString();
                        ddlReportName_SelectedIndexChanged(ddlReportName, new EventArgs());
                    }
                    if (list[0].Duplication != null)
                    {
                        string[] dup=list[0].Duplication.Split(',');
                        foreach (string value in dup)
                        {
                            chkDuplication.Items.Cast<ListItem>().Where(c => c.Value == value).FirstOrDefault().Selected = true;
                        }
                    }
                    Session["TransferList"] = UIUtility.LINQToDataTable(datble);
                    LoadGridView();


                }
            }
        }

        protected override string GetListPageUrl()
        {
            return "H_EmployeeTransferList.aspx";
        }

        private new Message Validate()
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Record saved successfully.";

            base.Validate();

            if (IsValid == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid data provided or required data missing";
                return msg;
            }
            if (chkDuplication.Items.Cast<ListItem>().Where(li => li.Selected).Count() == 0)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "At Least One Duplication(CC) should be selected";
                return msg;
            }
            if (Session["TransferList"] == null || ((DataTable)Session["TransferList"]).Rows.Count ==0)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Add Employee to be transfered";
                return msg;
            }
            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();
            if (msg.Type == MessageType.Information)
            {
                string desc = "INSERT H_EmployeeTransferHistory";

                TransactionManager = new TransactionManager(true, desc);
                if (Type == TYPE_EDIT)
                {
                    H_EmployeeTransferHistory emp = H_EmployeeTransferHistory.GetById(Convert.ToInt32(hdnId.Value));
                    IList<H_EmployeeTransferHistory> list = H_EmployeeTransferHistory.Find("LetterNo='" + emp.LetterNo + "'", "");
                    foreach (H_EmployeeTransferHistory entity in list)
                    {
                        H_EmployeeTransferHistory.Delete(TransactionManager, entity);
                    }
                }
                DataTable dt = (DataTable)Session["TransferList"];
                foreach (DataRow dr in dt.Rows)
                {
                    H_EmployeeTransferHistory entity = new H_EmployeeTransferHistory();
                    entity.H_EmployeeId = Convert.ToInt32(dr[0]);
                    entity.LetterNo = txtLetterNo.Text;
                    entity.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
                    entity.JoiningDate = DBUtility.ToDateTime(txtTransferDate.Text);
                    entity.Remarks = "";
                    entity.SourceBranchId = Convert.ToInt32(dr[5]);
                    entity.DestinationBranchId = Convert.ToInt32(dr[8]);
                    entity.Type = (H_EmployeeTransferHistory.Types)DBUtility.ToInt32(ddlType.SelectedValue);
                    entity.UserLogin = User.Identity.Name;
                    entity.EntryDateTime = DateTime.Now;
                    entity.Status = H_EmployeeTransferHistory.Statuses.ACTIVE;
                    entity.H_LetterFormatsId = Convert.ToInt32(ddlReportName.SelectedValue);
                    entity.Duplication = GetDuplication();
                    H_EmployeeTransferHistory.Insert(TransactionManager, entity);
                }

                Type = TYPE_EDIT;
                TransactionManager.Commit();
                ShowReport();
            }

            return msg;
        }

        private void ShowReport()
        {
            DataTable dt = null;
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
                    dt.Rows.Add(emp.Name, emp.Code, sourceBranch.Name, sourceSubzone.Name, destinationBranch.Name, destinationSubzone.Name, th.LetterNo, th.JoiningDate, th.LetterDate, duplication, "");
                }
            }
            else
            {

            }
            string reportName = "H_TransferLetter.rpt";
            ReportDocument rd = new ReportDocument();

            rd.Load(Server.MapPath("~/Reports/" + reportName));

            string designation = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                H_Employee emp = H_Employee.GetByCode(dr["Code"].ToString());
                H_EmployeeDesignation ed = H_EmployeeDesignation.Get("H_EmployeeId=" + emp.Id + " AND EndDate='2099-12-31'");
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
            txtUser.Text = "Printed by- " + User.Identity.Name + " on " + DateTime.Today.Date.ToString("dd/MM/yyyy");



            rd.SetDataSource(dt);
            Session["ReportType"] = Convert.ToInt32(ddlFormat.SelectedValue);
            ReportUtility.ViewReport(this, rd, false, false);
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
        private string GetDuplication()
        {
            List<string> selectedValues = chkDuplication.Items.Cast<ListItem>()
                                                .Where(li => li.Selected)
                                                .Select(li => li.Value)
                                                .ToList();
            string duplication = string.Empty;
            foreach (string str in selectedValues)
            {
                duplication = duplication + "," + str;
            }
            duplication = duplication.Substring(1);
            return duplication;
        }
    }
}
