using System;
using System.Data;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeePenaltyRemissionAdd : AddPage
    {
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

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            TransactionManager tm = new TransactionManager(false);
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
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
                txtDistrict.Text = Subzone.GetById(region.SubzoneId).Name;
                txtBranch.Text = branch.Name;
                hdnId.Value = h_Employee.Id.ToString();
                hfPenalyId.Value = "";
                LoadGridView(h_Employee.Id);
            }
            else
            {
                hdnId.Value = "0";
                txtDistrict.Text = "";
                txtDesignation.Text = "";
                txtBranch.Text = "";
                txtStatus.Text = "";
                txtPenaltyAmount.Text = "";
                txtPenaltyLetterNo.Text = "";
                txtPenalyLetterDate.Text = "";
                txtLetterNo.Text = "";
                txtLetterDate.Text = "";
                txtRemissionAmount.Text = "";

            }

        }
        private void LoadGridView(int h_EmployeeId)
        {
            TransactionManager tm = new TransactionManager(false);
            string query = "SELECT H_EmployeePenalty.Id,LetterNo,LetterDate, FineType,FineTime,FineAmount,Duration,RemissionLetterNo,RemissionLetterDate,RemissionAmount,Branch.Name  as Branch" +
                              " FROM H_EmployeePenalty" +
                              " INNER JOIN Branch ON H_EmployeePenalty.BranchId=Branch.Id" +
                              " WHERE H_EmployeePenalty.H_EmployeeId=" + h_EmployeeId + " ORDER BY LetterDate";
            DataSet ds = tm.GetDataSet(query);
            if (ds != null)
            {
                gvList.DataSource = ds.Tables[0];
                gvList.DataBind();
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "preview")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string penaltyId = lnkView.CommandArgument;
                H_EmployeePenalty penalty = H_EmployeePenalty.GetById(Convert.ToInt32(penaltyId));
                txtPenaltyLetterNo.Text = penalty.LetterNo;
                txtPenalyLetterDate.Text = UIUtility.Format(penalty.LetterDate);
                txtPenaltyAmount.Text = penalty.FineAmount.ToString();
                //ddlFineType.SelectedValue = penalty.FineType.ToString();

                Branch branch = Branch.GetById(penalty.BranchId);
                Region region = Region.GetById(branch.RegionId);
                //ddlSubzone.SelectedValue = region.SubzoneId.ToString();
                //ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
                //ddlBranch.SelectedValue = branch.Id.ToString();
                //txtRemarks.Text = penalty.Remarks;
                hfPenalyId.Value = penalty.Id.ToString();
                this.Type = TYPE_EDIT;

            }
        }

        private H_EmployeePenalty GetH_EmployeePenalty()
        {
            H_EmployeePenalty h_EmployeePenalty = null;
            h_EmployeePenalty = H_EmployeePenalty.GetById(Convert.ToInt32(hfPenalyId.Value));

            h_EmployeePenalty.FineAmount = DBUtility.ToDouble(txtPenaltyAmount.Text) - DBUtility.ToDouble(txtRemissionAmount.Text);
            h_EmployeePenalty.RemissionUser = User.Identity.Name;
            h_EmployeePenalty.RemissionLetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeePenalty.RemissionLetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeePenalty.RemissionAmount = DBUtility.ToDouble(txtRemissionAmount.Text);
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
            if (String.IsNullOrEmpty(hfPenalyId.Value))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Select Penalty from List";
                return msg;
            }
            return msg;
        }
        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                H_EmployeePenalty h_EmployeePenalty = this.GetH_EmployeePenalty();
                string desc = "Update [H_EmployeePenalty]";

                this.TransactionManager = new TransactionManager(true, desc);
                H_EmployeePenalty.Update(this.TransactionManager, h_EmployeePenalty);
                                
                this.Type = TYPE_EDIT;

                this.TransactionManager.Commit();
                LoadGridView(h_EmployeePenalty.H_EmployeeId);

                txtDistrict.Text = "";
                txtDesignation.Text = "";
                txtBranch.Text = "";
                txtStatus.Text = "";
                txtPenaltyAmount.Text = "";
                txtPenaltyLetterNo.Text = "";
                txtPenalyLetterDate.Text = "";
                txtLetterNo.Text = "";
                txtLetterDate.Text = "";
                txtRemissionAmount.Text = "";
                hfPenalyId.Value = "";
            }

            return msg;
        }
    }
}
