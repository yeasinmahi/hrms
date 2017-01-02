using System;
using System.Data;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeIncrementHeldupRemissionAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEINCREMENTHELDUPREMISSION"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            
        }

        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }
        private H_EmployeeIncrementHeldup GetH_EmployeeIncrementHeldup()
        {
            H_EmployeeIncrementHeldup h_EmployeeIncrementHeldup = null;
            h_EmployeeIncrementHeldup = H_EmployeeIncrementHeldup.GetById(Convert.ToInt32(hdnId.Value));

            h_EmployeeIncrementHeldup.ExemptionLetterNo = DBUtility.ToString(txtExLetterNo.Text);
            h_EmployeeIncrementHeldup.ExemptionLetterDate = DBUtility.ToDateTime(txtExLetterDate.Text);
            h_EmployeeIncrementHeldup.ExemptionDate = DBUtility.ToDateTime(txtExemptionDate.Text);
            h_EmployeeIncrementHeldup.IncrementExempted = DBUtility.ToInt32(txtExemptedInc.Text);
            h_EmployeeIncrementHeldup.ExemptionRemarks = DBUtility.ToNullableString(txtRmarks.Text);
            return h_EmployeeIncrementHeldup;
        }
        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                H_EmployeeIncrementHeldup h_EmployeeIncrementHeldup = this.GetH_EmployeeIncrementHeldup();
                string desc = "Update [H_EmployeeIncrementHeldup]";

                this.TransactionManager = new TransactionManager(true, desc);
                H_EmployeeIncrementHeldup.Update(this.TransactionManager, h_EmployeeIncrementHeldup);

                this.Type = TYPE_EDIT;

                this.TransactionManager.Commit();
                LoadGridView(h_EmployeeIncrementHeldup.H_EmployeeId);

                txtLetterNo.Text = "";
                txtLetterDate.Text = "";
                txtIncrementStop.Text = "";
                txtFromDate.Text = "";
                txtExLetterNo.Text = "";
                txtExLetterDate.Text = "";
                txtExemptionDate.Text = "";
                txtExemptedInc.Text = "";
                hdnId.Value = "";
            }

            return msg;
        }

        private void LoadGridView(int h_EmployeeId)
        {
            TransactionManager tm = new TransactionManager(false);
            string query = "SELECT H_EmployeeIncrementHeldup.Id, LetterNo,LetterDate, FromDate,ToDate,Branch.Name  as Branch,ExemptionLetterNo,ExemptionLetterDate,ExemptionDate,IncrementExempted" +
                           " FROM H_EmployeeIncrementHeldup" +
                           " INNER JOIN Branch ON H_EmployeeIncrementHeldup.BranchId=Branch.Id" +
                           " WHERE H_EmployeeIncrementHeldup.H_EmployeeId=" + h_EmployeeId;
            DataSet ds = tm.GetDataSet(query);
            if (ds != null)
            {
                gvIncrement.DataSource = ds.Tables[0];
                gvIncrement.DataBind();
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
            if (String.IsNullOrEmpty(hdnId.Value))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Select Increment Heldup from List";
                return msg;
            }
            return msg;
        }
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                //hdnId.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                Branch branch = Branch.GetById(eBranch.BranchId);
                Region region = Region.GetById(branch.RegionId);

                //ddlZone.SelectedValue = UIUtility.Format(Subzone.GetById(region.SubzoneId).ZoneId);
                //ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

                //ddlSubzone.SelectedValue = UIUtility.Format(region.SubzoneId);
                //ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());

                //ddlRegion.SelectedValue = UIUtility.Format(branch.RegionId);
                //ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());

                //ddlBranch.SelectedValue = UIUtility.Format(eBranch.BranchId);
                LoadGridView(h_Employee.Id);
            }
            else
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";

                hdnId.Value = "";

                this.ShowUIMessage(msg);
            }
        }
        
        protected void gvIncrement_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "preview")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string penaltyId = lnkView.CommandArgument;
                H_EmployeeIncrementHeldup penalty = H_EmployeeIncrementHeldup.GetById(Convert.ToInt32(penaltyId));
                txtLetterNo.Text = penalty.LetterNo;
                txtLetterDate.Text = UIUtility.Format(penalty.LetterDate);
                txtIncrementStop.Text = UIUtility.Format(penalty.IncrementStop);
                txtFromDate.Text = UIUtility.Format(penalty.FromDate);

                Branch branch = Branch.GetById(penalty.BranchId);
                Region region = Region.GetById(branch.RegionId);
                hdnId.Value = penalty.Id.ToString();
                this.Type = TYPE_EDIT;

            }
        }
       
    }
}
