using System;
using System.Data;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeIncrementHeldupAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEINCREMENTHELDUP ADD"; }
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
            return "H_EmployeeIncrementHeldupList.aspx";
        }

        private H_EmployeeIncrementHeldup GetH_EmployeeIncrementHeldup()
        {
            H_EmployeeIncrementHeldup h_EmployeeIncrementHeldup = new H_EmployeeIncrementHeldup();

            h_EmployeeIncrementHeldup.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
            h_EmployeeIncrementHeldup.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeeIncrementHeldup.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeeIncrementHeldup.IncrementStop = DBUtility.ToInt32(txtIncrementStop.Text);
            h_EmployeeIncrementHeldup.FromDate = DBUtility.ToDateTime(txtFromDate.Text);
            h_EmployeeIncrementHeldup.ToDate = DBUtility.ToNullableDateTime(txtToDate.Text);
            h_EmployeeIncrementHeldup.BranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
            h_EmployeeIncrementHeldup.Cause = DBUtility.ToString(txtCause.Text);
            h_EmployeeIncrementHeldup.UserLogin = User.Identity.Name;

            return h_EmployeeIncrementHeldup;
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

            //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee == null)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";
                return msg;
            }

            if (h_Employee.JoiningDate >= DBUtility.ToDateTime(txtLetterDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Letter date should be greater than employee's joining date (" + h_Employee.JoiningDate + ")";
                return msg;
            }

            if (h_Employee.JoiningDate >= DBUtility.ToDateTime(txtFromDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "From date should be greater than employee's joining date (" + h_Employee.JoiningDate + ")";
                return msg;
            }
            if(!String.IsNullOrEmpty(txtToDate.Text))
            if (DBUtility.ToDateTime(txtFromDate.Text.Trim()) > DBUtility.ToDateTime(txtToDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "To date should be greater than or equal to from date";
                return msg;
            }

            if (h_Employee.Status != H_Employee.Statuses.Working && h_Employee.Status != H_Employee.Statuses.Consultancy && h_Employee.Status != H_Employee.Statuses.In_Leave)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                return msg;
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                H_EmployeeIncrementHeldup h_EmployeeIncrementHeldup = GetH_EmployeeIncrementHeldup();
                string desc = "Insert [H_EmployeeIncrementHeldup]";

                TransactionManager = new TransactionManager(true, desc);

                H_EmployeeIncrementHeldup.Insert(TransactionManager, h_EmployeeIncrementHeldup);

                hdnId.Value = h_EmployeeIncrementHeldup.Id.ToString();
                Type = TYPE_EDIT;

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            ddlZone.DataSource = Zone.Find("Status=1", "Name");//, User.Identity.Name);
            ddlZone.DataBind();
            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZone.SelectedValue != null && ddlZone.SelectedValue != "")
            {
                ddlSubzone.DataSource = Subzone.Find("ZoneId = " + ddlZone.SelectedValue + " AND Status=1", "Name");//, User.Identity.Name);
                ddlSubzone.DataBind();
                ddlSubzone_SelectedIndexChanged(ddlRegion, new EventArgs());
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
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                Branch branch = Branch.GetById(eBranch.BranchId);
                Region region = Region.GetById(branch.RegionId);

                ddlZone.SelectedValue = UIUtility.Format(Subzone.GetById(region.SubzoneId).ZoneId);
                ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

                ddlSubzone.SelectedValue = UIUtility.Format(region.SubzoneId);
                ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());

                ddlRegion.SelectedValue = UIUtility.Format(branch.RegionId);
                ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());

                ddlBranch.SelectedValue = UIUtility.Format(eBranch.BranchId);
                TransactionManager tm = new TransactionManager(false);
                string query = "SELECT LetterNo,LetterDate, FromDate,ToDate,Branch.Name  as Branch" +
                               " FROM H_EmployeeIncrementHeldup" +
                               " INNER JOIN Branch ON H_EmployeeIncrementHeldup.BranchId=Branch.Id" +
                               " WHERE H_EmployeeIncrementHeldup.H_EmployeeId=" + h_Employee.Id;
                DataSet ds = tm.GetDataSet(query);
                if (ds != null)
                {
                    gvIncrement.DataSource = ds.Tables[0];
                    gvIncrement.DataBind();
                }
            }
            else
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";

                hdnId.Value = "0";

                ShowUiMessage(msg);
            }
        }
    }
}
