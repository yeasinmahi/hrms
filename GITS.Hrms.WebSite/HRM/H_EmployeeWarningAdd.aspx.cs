using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeWarningAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEWARNING ADD"; }
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
            return "H_EmployeeWarningList.aspx";
        }

        private H_EmployeeWarning GetH_EmployeeWarning()
        {
            H_EmployeeWarning h_EmployeeWarning = new H_EmployeeWarning();
            if (this.Type == TYPE_EDIT)
            {
                h_EmployeeWarning = H_EmployeeWarning.GetById(Convert.ToInt32(hfWarningId.Value));
            }
            else
            {
                h_EmployeeWarning = new H_EmployeeWarning();
                h_EmployeeWarning.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
                h_EmployeeWarning.TotalWarningTime = 1;// DBUtility.ToInt32(txtTotalWarningTime.Text);
                h_EmployeeWarning.UserLogin = User.Identity.Name;
                h_EmployeeWarning.IsExempted = false;
            }

            
            h_EmployeeWarning.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeeWarning.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeeWarning.Duration = DBUtility.ToString(DBUtility.ToDateTime(txtLetterDate.Text).Year);
            
            h_EmployeeWarning.BranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
            h_EmployeeWarning.Cause = DBUtility.ToString(txtCause.Text);
            h_EmployeeWarning.WarningType = DBUtility.ToString(ddlWarningType.SelectedValue);
            return h_EmployeeWarning;
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

            //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (h_Employee.AppointmentLetterDate >= DBUtility.ToDateTime(txtLetterDate.Text))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Letter date should be greater than appointment letter date (" + h_Employee.AppointmentLetterDate + ")";
                    return msg;
                }

                if (h_Employee.Status != H_Employee.Statuses.Working && h_Employee.Status != H_Employee.Statuses.Consultancy && h_Employee.Status != H_Employee.Statuses.In_Leave)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    return msg;
                }
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                H_EmployeeWarning h_EmployeeWarning = this.GetH_EmployeeWarning();
                string desc = "Insert [H_EmployeeWarning]";
                if (this.Type == TYPE_EDIT)
                {
                    desc = "UPDATE [H_EmployeeWarning]";
                }
                else
                {
                    desc = "Insert [H_EmployeeWarning]";
                }
                this.TransactionManager = new TransactionManager(true, desc);
                if (this.Type == TYPE_EDIT)
                {
                    H_EmployeeWarning.Update(this.TransactionManager, h_EmployeeWarning);
                }
                else
                {
                    H_EmployeeWarning.Insert(this.TransactionManager, h_EmployeeWarning);
                }
                

                

                hdnId.Value = h_EmployeeWarning.Id.ToString();
                this.Type = TYPE_EDIT;

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            this.ddlSubzone.DataSource = Subzone.Find("Status=1", "Name");//, User.Identity.Name);
            this.ddlSubzone.DataBind();
            this.ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
        }

        //protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ddlZone.SelectedValue != null && this.ddlZone.SelectedValue != "")
        //    {
        //        this.ddlSubzone.DataSource = Subzone.Find("ZoneId = " + this.ddlZone.SelectedValue + " AND Status=1", "Name");//, User.Identity.Name);
        //        this.ddlSubzone.DataBind();
        //        this.ddlSubzone_SelectedIndexChanged(ddlRegion, new EventArgs());
        //    }
        //}

        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlSubzone.SelectedValue != null && this.ddlSubzone.SelectedValue != "")
            {
                this.ddlRegion.DataSource = Region.Find("SubzoneId = " + this.ddlSubzone.SelectedValue, "Name");//, User.Identity.Name);
                this.ddlRegion.DataBind();
                this.ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlRegion.SelectedValue != null && this.ddlRegion.SelectedValue != "")
            {
                this.ddlBranch.DataSource = Branch.Find("RegionId = " + this.ddlRegion.SelectedValue + " AND Status=1", "Name");//, User.Identity.Name);
                this.ddlBranch.DataBind();
            }
            else
            {
                this.ddlBranch.Items.Clear();
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                hdnId.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                Branch branch = Branch.GetById(eBranch.BranchId);
                Region region = Region.GetById(branch.RegionId);
                Subzone subzone = Subzone.GetById(region.SubzoneId);
                txtBranch.Text = branch.Name;
                txtDistrict.Text = subzone.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;

                //ddlZone.SelectedValue = UIUtility.Format(subzone.ZoneId);
                //ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

                ddlSubzone.SelectedValue = UIUtility.Format(region.SubzoneId);
                ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());

                ddlRegion.SelectedValue = UIUtility.Format(branch.RegionId);
                ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());

                ddlBranch.SelectedValue = UIUtility.Format(eBranch.BranchId);
                TransactionManager tm = new TransactionManager(false);
                LoadGridView(tm, h_Employee.Id);
                

            }
            else
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";

                hdnId.Value = "0";

                this.ShowUIMessage(msg);
            }
        }

        protected void gvWarning_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editrow")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string penaltyId = lnkView.CommandArgument;
                H_EmployeeWarning penalty = H_EmployeeWarning.GetById(Convert.ToInt32(penaltyId));
                txtLetterNo.Text = penalty.LetterNo;
                txtLetterDate.Text = UIUtility.Format(penalty.LetterDate);

                Branch branch = Branch.GetById(penalty.BranchId);
                Region region = Region.GetById(branch.RegionId);
                Subzone subzone = Subzone.GetById(region.SubzoneId);
                //ddlZone.SelectedValue = Zone.GetById(subzone.ZoneId).Id.ToString();
                //ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
                ddlSubzone.SelectedValue = subzone.Id.ToString();
                ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
                ddlRegion.SelectedValue = region.Id.ToString();
                ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
                ddlBranch.SelectedValue = branch.Id.ToString();
                txtCause.Text = penalty.Cause;
                hfWarningId.Value = penalty.Id.ToString();
                ddlWarningType.SelectedValue = penalty.WarningType;
                this.Type = TYPE_EDIT;

            }
            if (e.CommandName == "deleterow")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string warningId = lnkView.CommandArgument;
                string desc = "Delete [H_EmployeeWarning]";

                this.TransactionManager = new TransactionManager(true, desc);
                H_EmployeeWarning.Delete(this.TransactionManager, Convert.ToInt32(warningId));

                TransactionManager.Commit();
                LoadGridView(TransactionManager, Convert.ToInt32(hdnId.Value));
            }
        }

        private void LoadGridView(TransactionManager TransactionManager, int p)
        {
            TransactionManager tm = new TransactionManager(false);
            string query = "select H_EmployeeWarning.Id,LetterNo,LetterDate, Duration,TotalWarningTime,Branch.Name  as Branch,Cause,WarningType" +
                        " from H_EmployeeWarning" +
                        " INNER JOIN Branch ON H_EmployeeWarning.BranchId=Branch.Id" +
                        " where H_EmployeeWarning.H_EmployeeId=" + p+
                        " Order by LetterDate DESC";
            DataSet ds = tm.GetDataSet(query);
            if (ds != null)
            {
                gvWarning.DataSource = ds.Tables[0];
                gvWarning.DataBind();
                IList<UserRole> ur = UserRole.FindByUserLogin(User.Identity.Name, "");
                int roles = ur.Where(n => n.RoleName.ToLower() == "edit").Count();
                if (roles == 0)
                {
                    gvWarning.Columns[7].Visible = false;
                }
            }

        }
    }
}
