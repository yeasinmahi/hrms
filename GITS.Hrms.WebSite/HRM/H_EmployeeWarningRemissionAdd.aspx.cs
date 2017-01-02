using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeWarningRemissionAdd : AddPage
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
                txtLetterNo.Text = "";
                txtLetterDate.Text = "";
                txtLetterNo.Text = "";
                txtLetterDate.Text = "";

            }

        }
        private void LoadGridView(int h_EmployeeId)
        {
            //TransactionManager tm = new TransactionManager(false);
            //string query = "SELECT H_EmployeeWarning.Id,LetterNo,LetterDate, Duration,Cause" +
            //                ",Exempted=Case when IsExempted=1 then 'Yes' else 'No' end,ExemptedLetterNo,ExemptedLetterDate" +
            //                " FROM H_EmployeeWarning" +
            //                " WHERE H_EmployeeWarning.H_EmployeeId=" + h_EmployeeId + " ORDER BY LetterDate";
            //DataSet ds = tm.GetDataSet(query);

            //if (ds != null)
            //{
            //    gvList.DataSource = ds.Tables[0];
            //    gvList.DataBind();
            //}
            IList<H_EmployeeWarning> objList = H_EmployeeWarning.FindByH_EmployeeId(h_EmployeeId, "LetterDate");
            if (objList != null)
            {
                gvList.DataSource = objList;
                gvList.DataBind();
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "preview")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string warningId = lnkView.CommandArgument;
                H_EmployeeWarning warning = H_EmployeeWarning.GetById(Convert.ToInt32(warningId));
                txtLetterNo.Text = warning.LetterNo;
                txtLetterDate.Text = UIUtility.Format(warning.LetterDate);
                hfPenalyId.Value = warning.Id.ToString();
                this.Type = TYPE_EDIT;

            }
        }

        private H_EmployeeWarning GetH_EmployeePenalty()
        {
            H_EmployeeWarning h_Employeewarning = null;
            h_Employeewarning = H_EmployeeWarning.GetById(Convert.ToInt32(hfPenalyId.Value));
            h_Employeewarning.IsExempted = true;
            h_Employeewarning.ExemptedLetterNo = DBUtility.ToString(txtExemtedLetterNo.Text);
            h_Employeewarning.ExemptedLetterDate = DBUtility.ToDateTime(txtExemtedLetterDate.Text);
            h_Employeewarning.ExemptedRemarks = DBUtility.ToNullableString(txtExemtedRemarks.Text);

            return h_Employeewarning;
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
                msg.Msg = "Select Warning to be Exemted from List";
                return msg;
            }
            return msg;
        }
        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                H_EmployeeWarning h_EmployeeWarning = this.GetH_EmployeePenalty();
                string desc = "Update [H_EmployeeWarning]";

                this.TransactionManager = new TransactionManager(true, desc);
                H_EmployeeWarning.Update(this.TransactionManager, h_EmployeeWarning);

                this.Type = TYPE_EDIT;

                this.TransactionManager.Commit();
                LoadGridView(h_EmployeeWarning.H_EmployeeId);

                txtDistrict.Text = "";
                txtDesignation.Text = "";
                txtBranch.Text = "";
                txtStatus.Text = "";
                txtLetterNo.Text = "";
                txtLetterDate.Text = "";
                txtLetterNo.Text = "";
                txtLetterDate.Text = "";
                txtExemtedRemarks.Text = "";
                txtExemtedLetterNo.Text = "";
                txtExemtedLetterDate.Text = "";
                hfPenalyId.Value = "";
            }

            return msg;
        }
    }
}
