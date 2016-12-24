using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;
using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.WebSite.HRM
{
    public partial class H_EmployeePenaltyAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEPENALTY ADD"; }
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
            return "H_EmployeePenaltyList.aspx";
        }

        private Asa.Hrms.Data.Entity.H_EmployeePenalty GetH_EmployeePenalty()
        {
            Asa.Hrms.Data.Entity.H_EmployeePenalty h_EmployeePenalty = null;
            if (this.Type == TYPE_EDIT)
            {
                h_EmployeePenalty = H_EmployeePenalty.GetById(Convert.ToInt32(hfPenalyId.Value));
            }
            else
            {
                h_EmployeePenalty = new H_EmployeePenalty();
                h_EmployeePenalty.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
                h_EmployeePenalty.UserLogin = User.Identity.Name;
            }

            
            h_EmployeePenalty.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeePenalty.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeePenalty.FineType = DBUtility.ToString(ddlFineType.SelectedValue);
            h_EmployeePenalty.FineTime = 1;// DBUtility.ToInt32(txtFineTime.Text);
            h_EmployeePenalty.FineAmount = DBUtility.ToDouble(txtFineAmount.Text);
            h_EmployeePenalty.Duration = DBUtility.ToDateTime(txtLetterDate.Text).Year;
            h_EmployeePenalty.BranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
            h_EmployeePenalty.Remarks = DBUtility.ToNullableString(txtRemarks.Text);
            return h_EmployeePenalty;
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
                if (h_Employee.JoiningDate >= DBUtility.ToDateTime(this.txtLetterDate.Text.Trim()))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Letter date should be greater than employee's joining date (" + h_Employee.JoiningDate + ")";
                    return msg;
                }

                if (h_Employee.Status != H_Employee.Statuses.Working && h_Employee.Status != H_Employee.Statuses.Consultancy && h_Employee.Status != H_Employee.Statuses.In_Leave)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    return msg;
                }
                if (this.Type == TYPE_ADD)
                {
                    H_EmployeePenalty penalty = H_EmployeePenalty.Get("H_EmployeeId=" + h_Employee.Id + " AND LetterDate='" + DBUtility.ToDateTime(txtLetterDate.Text).ToString(Configuration.DatabaseDateFormat) + "'");
                    if (penalty != null)
                    {
                        msg.Type = MessageType.Error;
                        msg.Msg = penalty.FineType == "P" ? "Penalty" : "Fine" + " already posted, Letter Date:" + penalty.LetterDate.ToString("dd/MM/yyyy") + " Amount:" + penalty.FineAmount.ToString();
                        return msg;
                    }
                }
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
                //H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
                //if (h_Employee != null)
                //{
                   // hdnId.Value = h_Employee.Id.ToString();

                    Asa.Hrms.Data.Entity.H_EmployeePenalty h_EmployeePenalty = this.GetH_EmployeePenalty();
                    string desc = "Insert [H_EmployeePenalty]";

                    this.TransactionManager = new TransactionManager(true, desc);
                    if (this.Type == TYPE_EDIT)
                    {
                        Asa.Hrms.Data.Entity.H_EmployeePenalty.Update(this.TransactionManager, h_EmployeePenalty);
                    }
                    else
                    {
                        Asa.Hrms.Data.Entity.H_EmployeePenalty.Insert(this.TransactionManager, h_EmployeePenalty);
                    }

                    //hdnId.Value = h_EmployeePenalty.Id.ToString();
                    this.Type = TYPE_EDIT;

                    this.TransactionManager.Commit();
                //}
                //else
                //{
                //    msg = new Message();
                //    msg.Type = MessageType.Error;
                //    msg.Msg = "No employee found";

                //    hdnId.Value = "0";
                //}
            }

            return msg;
        }

        protected override void LoadData()
        {
            this.ddlSubzone.DataSource = Subzone.Find("Status=1", "Name");//, User.Identity.Name);
            this.ddlSubzone.DataBind();
            this.ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
        }
        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlSubzone.SelectedValue != null && this.ddlSubzone.SelectedValue != "")
            {
                this.ddlBranch.DataSource =Branch.Find("Status=1 AND RegionId IN ( SELECT Id FROM Region where SubzoneId="+ddlSubzone.SelectedValue+")","Name");
                this.ddlBranch.DataBind();
                
            }
        }
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            TransactionManager tm = new TransactionManager(false);
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text =((H_Employee.Statuses) h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;

                DataTable dt = tm.GetDataSet("SELECT ZoneId, SubzoneId, RegionId, BranchId, StartDate FROM H_EmployeeBranch INNER JOIN Branch ON BranchId = Branch.Id INNER JOIN Region ON RegionId = Region.Id INNER JOIN Subzone ON SubzoneId = Subzone.Id WHERE H_EmployeeId = " + h_Employee.Id + " ORDER BY EndDate DESC").Tables[0];
                Int32 z = 0;
                Int32 s = 0;
                Int32 r = 0;
                Int32 b = 0;

                for (Int32 i = 1; i < dt.Rows.Count; i++)
                {
                    if (z == i - 1 && dt.Rows[i - 1]["ZoneId"].ToString() == dt.Rows[i]["ZoneId"].ToString())
                    {
                        z = i;
                    }

                    if (s == i - 1 && dt.Rows[i - 1]["SubzoneId"].ToString() == dt.Rows[i]["SubzoneId"].ToString())
                    {
                        s = i;
                    }

                    if (r == i - 1 && dt.Rows[i - 1]["RegionId"].ToString() == dt.Rows[i]["RegionId"].ToString())
                    {
                        r = i;
                    }

                    if (b == i - 1 && dt.Rows[i - 1]["BranchId"].ToString() == dt.Rows[i]["BranchId"].ToString())
                    {
                        b = i;
                    }
                }

                Branch branch = Branch.GetById(DBUtility.ToInt32(dt.Rows[b]["BranchId"]));
                Region region = Region.GetById(branch.RegionId);
                //txtZone.Text = Zone.GetById(Subzone.GetById(region.SubzoneId).ZoneId).Name;
                //txtZoneDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[z]["StartDate"]));
                txtDistrict.Text = Subzone.GetById(region.SubzoneId).Name;
                ddlSubzone.SelectedValue = region.SubzoneId.ToString();
                ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
                //txtSubzoneDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[s]["StartDate"]));
                //txtRegion.Text = region.Name;
                //txtRegionDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[r]["StartDate"]));
                txtBranch.Text = branch.Name;
                ddlBranch.SelectedValue = branch.Id.ToString();
                // txtBranchDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[b]["StartDate"]));
                // hdnBranch.Value = branch.Id.ToString();
                hdnId.Value = h_Employee.Id.ToString();
                //IList<H_EmployeePenalty> penaltyList = H_EmployeePenalty.Find("H_EmployeeId=" + h_Employee.Id, "LetterDate");
                //gvList.DataSource = penaltyList;
                //gvList.DataBind();
                LoadGridView(tm,h_Employee.Id);
            }
            else
            {
                hdnId.Value = "0";
                txtDistrict.Text = "";
                txtDesignation.Text = "";
                txtBranch.Text = "";
                txtStatus.Text = "";

            }
       

        }
        private void LoadGridView(TransactionManager tm,int h_EmployeeId)
        {
            string query = "SELECT H_EmployeePenalty.Id,LetterNo,LetterDate, FineType,FineTime,FineAmount,Duration,Branch.Name  as Branch" +
                              " FROM H_EmployeePenalty" +
                              " INNER JOIN Branch ON H_EmployeePenalty.BranchId=Branch.Id" +
                              " WHERE H_EmployeePenalty.H_EmployeeId=" + h_EmployeeId + " ORDER BY LetterDate DESC";
                DataSet ds = tm.GetDataSet(query);
                if (ds != null)
                {
                    gvList.DataSource = ds.Tables[0];
                    gvList.DataBind();
                    IList<UserRole> ur = UserRole.FindByUserLogin(User.Identity.Name, "");
                    int roles = ur.Where(n => n.RoleName.ToLower() == "edit").Count();
                    if (roles == 0)
                    {
                        foreach (GridViewRow row in gvList.Rows)
                        {
                            LinkButton lbtn = new LinkButton();
                            lbtn = (LinkButton)row.FindControl("lnkLetterNo");
                            lbtn.Enabled = false;
                        }
                        gvList.Columns[7].Visible = false;
                    }

                }
        }
        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "preview")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string penaltyId = lnkView.CommandArgument;
                H_EmployeePenalty penalty = H_EmployeePenalty.GetById(Convert.ToInt32(penaltyId));
                txtLetterNo.Text = penalty.LetterNo;
                txtLetterDate.Text = UIUtility.Format(penalty.LetterDate);
                txtFineAmount.Text = penalty.FineAmount.ToString();
                ddlFineType.SelectedValue = penalty.FineType.ToString();

                Branch branch = Branch.GetById(penalty.BranchId);
                Region region = Region.GetById(branch.RegionId);
                ddlSubzone.SelectedValue = region.SubzoneId.ToString();
                ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
                ddlBranch.SelectedValue = branch.Id.ToString();
                txtRemarks.Text = penalty.Remarks;
                hfPenalyId.Value = penalty.Id.ToString();
                this.Type = TYPE_EDIT;

            }
            if (e.CommandName == "deleterow")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string penaltyId = lnkView.CommandArgument;
                string desc = "Delete [H_EmployeePenalty]";

                this.TransactionManager = new TransactionManager(true, desc);
                H_EmployeePenalty.Delete(this.TransactionManager, Convert.ToInt32(penaltyId));

                TransactionManager.Commit();
                LoadGridView(TransactionManager, Convert.ToInt32(hdnId.Value));
                
            }
        }


    }
}
