using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeReemploymentAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEREEMPLOYMENT ADD"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Menu mnuPageToolbar = (Menu)Master.FindControl("mnuPageToolbar");

            if (mnuPageToolbar.Items.Count > 1 && mnuPageToolbar.Items[1].Value == "SAVE")
            {
                mnuPageToolbar.Items.RemoveAt(1);
            }
        }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override string GetListPageUrl()
        {
            return "H_EmployeeReemploymentList.aspx";
        }

        private H_EmployeeReemployment GetH_EmployeeReemployment()
        {
            H_EmployeeReemployment h_EmployeeReemployment = new H_EmployeeReemployment();

            h_EmployeeReemployment.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
            h_EmployeeReemployment.DropoutType = (H_EmployeeDrop.Types)DBUtility.ToInt32(ddlDropoutType.SelectedValue);
            h_EmployeeReemployment.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeeReemployment.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeeReemployment.FromDate = DBUtility.ToDateTime(txtFromDate.Text);
            h_EmployeeReemployment.ReemploymentDate = DBUtility.ToDateTime(txtReemploymentDate.Text);
            h_EmployeeReemployment.SourceBranchId = DBUtility.ToInt32(hdnBranch.Value);
            h_EmployeeReemployment.DestinationBranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
            h_EmployeeReemployment.Cause = DBUtility.ToString(txtCause.Text);

            return h_EmployeeReemployment;
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

            if (txtFromDate.Text == "")
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Employee is not currently dropped";
                return msg;
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
                H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
                if (h_Employee != null)
                {
                    H_EmployeeReemployment h_EmployeeReemployment = GetH_EmployeeReemployment();

                    h_Employee.Status = (H_Employee.Statuses)((Int32)H_Employee.Statuses.Working);
                    h_Employee.EmploymentType = (H_Employee.EmploymentTypes)DBUtility.ToInt32(ddlReemploymentType.SelectedValue);

                    string desc = "Insert [H_EmployeeReemployment]";
                    TransactionManager = new TransactionManager(true, desc);

                    H_EmployeeReemployment.Insert(TransactionManager, h_EmployeeReemployment);
                    H_Employee.Update(TransactionManager, h_Employee);

                    hdnId.Value = h_EmployeeReemployment.Id.ToString();
                    Type = TYPE_EDIT;

                    H_EmployeeBranch eBranch = H_EmployeeBranch.Find(TransactionManager, "H_EmployeeId=" + h_EmployeeReemployment.H_EmployeeId, "EndDate DESC")[0];

                    if (eBranch.BranchId != DBUtility.ToInt32(ddlBranch.SelectedValue))
                    {
                        eBranch.EndDate = h_EmployeeReemployment.FromDate.AddDays(-1);
                        H_EmployeeBranch.Update(TransactionManager, eBranch);

                        eBranch = new H_EmployeeBranch();
                        eBranch.H_EmployeeId = h_EmployeeReemployment.H_EmployeeId;
                        eBranch.BranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
                        eBranch.StartDate = h_EmployeeReemployment.ReemploymentDate;
                        eBranch.EndDate = new DateTime(2099, 12, 31);
                        H_EmployeeBranch.Insert(TransactionManager, eBranch);
                    }

                    TransactionManager.Commit();
                }
                else
                {
                    msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "No employee found";

                    hdnId.Value = "0";
                }
            }

            return msg;
        }

        protected override void LoadData()
        {
            ddlZone.DataSource = Zone.Find("Status=1", "Name");//, User.Identity.Name);
            ddlZone.DataBind();
            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

            UIUtility.LoadEnums(ddlDropoutType, typeof(H_EmployeeDrop.Types), false, false, true);

            UIUtility.LoadEnums(ddlReemploymentType, typeof(H_Employee.EmploymentTypes), false, false, true);
            ddlReemploymentType.Items.RemoveAt(0);
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZone.SelectedValue != null && ddlZone.SelectedValue != "")
            {
                ddlSubzone.DataSource = Subzone.Find("ZoneId = " + ddlZone.SelectedValue + " AND Status=1", "Name");//, User.Identity.Name);
                ddlSubzone.DataBind();
                ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
            }
        }

        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubzone.SelectedValue != null && ddlSubzone.SelectedValue != "")
            {
                ddlRegion.DataSource = Region.Find("SubzoneId = " + ddlSubzone.SelectedValue, "Name");//, User.Identity.Name);
                ddlRegion.DataBind();
                ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegion.SelectedValue != null && ddlRegion.SelectedValue != "")
            {
                ddlBranch.DataSource = Branch.Find("RegionId = " + ddlRegion.SelectedValue + " AND Status=1", "Name");//, User.Identity.Name);
                ddlBranch.DataBind();
            }
            else
            {
                ddlBranch.Items.Clear();
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                hdnId.Value = h_Employee.Id.ToString();

                IList<H_EmployeeDrop> eDrop = null;
                eDrop = H_EmployeeDrop.Find("H_EmployeeId=" + h_Employee.Id, "DropDate DESC");

                if (eDrop != null && eDrop.Count != 0)
                {
                    H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                    Branch branch = Branch.GetById(eBranch.BranchId);
                    Region region = Region.GetById(branch.RegionId);

                    txtFromDate.Text = UIUtility.Format(eDrop[0].DropDate);
                    ddlDropoutType.SelectedValue = ((Int32)eDrop[0].Type).ToString();
                    //txtDropoutType.Text = ((H_EmployeeDrop.Types)eDrop[0].Type).ToString();

                    txtZone.Text = Zone.GetById(Subzone.GetById(region.SubzoneId).ZoneId).Name;
                    txtSubzone.Text = Subzone.GetById(region.SubzoneId).Name;
                    txtRegion.Text = region.Name;
                    txtBranch.Text = branch.Name;
                    txtBranchDate.Text = UIUtility.Format(eBranch.StartDate);
                    hdnBranch.Value = eBranch.BranchId.ToString();
                }
                else
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Employee is not currently dropped";

                    txtFromDate.Text = "";
                    ddlDropoutType.SelectedIndex = -1;
                    txtZone.Text = "";
                    txtRegion.Text = "";
                    txtBranch.Text = "";
                    txtBranchDate.Text = "";
                    hdnBranch.Value = "";

                    ShowUiMessage(msg);
                }
            }
            else
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";

                hdnId.Value = "0";
                txtFromDate.Text = "";
                ddlDropoutType.SelectedIndex = -1;
                txtZone.Text = "";
                txtRegion.Text = "";
                txtBranch.Text = "";
                txtBranchDate.Text = "";
                hdnBranch.Value = "";

                ShowUiMessage(msg);
            }
        }
    }
}
