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
    public partial class H_SpecialIncrementAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_SPECIALINCREMENT ADD"; }
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

        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                string desc = string.Empty;
                H_SpecialIncrement h_EmployeePenalty = this.GetH_SpecialIncrement();
                if (this.Type == TYPE_EDIT)
                {
                    desc = "Update [H_SpecialIncrement]";
                }
                else
                {
                    desc = "Insert [H_SpecialIncrement]";
                }

                this.TransactionManager = new TransactionManager(true, desc);
                if (this.Type == TYPE_EDIT)
                {
                    H_SpecialIncrement.Update(this.TransactionManager, h_EmployeePenalty);
                }
                else
                {
                    H_SpecialIncrement.Insert(this.TransactionManager, h_EmployeePenalty);
                }

                this.Type = TYPE_EDIT;

                this.TransactionManager.Commit();
                LoadGridView(Convert.ToInt32(hdnId.Value));
                txtLetterNo.Text = "";
                txtLetterDate.Text = "";
                txtEffectiveDate.Text = "";
                txtNumberOfIncrement.Text = "";
            }

            return msg;
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

            
            return msg;
        }
        private H_SpecialIncrement GetH_SpecialIncrement()
        {
            H_SpecialIncrement h_EmployeePenalty = null;
            if (this.Type == TYPE_EDIT)
            {
                h_EmployeePenalty = H_SpecialIncrement.GetById(Convert.ToInt32(hfPenalyId.Value));
            }
            else
            {
                h_EmployeePenalty = new H_SpecialIncrement();
                h_EmployeePenalty.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
            }


            h_EmployeePenalty.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeePenalty.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeePenalty.EffectiveDate = DBUtility.ToDateTime(txtEffectiveDate.Text);
            h_EmployeePenalty.NumberOfIncrement = DBUtility.ToInt32(txtNumberOfIncrement.Text);

            h_EmployeePenalty.Remarks = DBUtility.ToNullableString(txtRemarks.Text);
            return h_EmployeePenalty;
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

                LoadGridView(h_Employee.Id);
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
        private void LoadGridView(int h_EmployeeId)
        {
            IList<H_SpecialIncrement> IncList = H_SpecialIncrement.FindByH_EmployeeId(h_EmployeeId, "");
            if (IncList != null)
            {
                gvList.DataSource = IncList;
                gvList.DataBind();
                //IList<UserRole> ur = UserRole.FindByUserLogin(User.Identity.Name, "");
                //int roles = ur.Where(n => n.RoleName.ToLower() == "edit").Count();
                //if (roles == 0)
                //{
                //    foreach (GridViewRow row in gvList.Rows)
                //    {
                //        LinkButton lbtn = new LinkButton();
                //        lbtn = (LinkButton)row.FindControl("lnkLetterNo");
                //        lbtn.Enabled = false;
                //    }
                //    gvList.Columns[7].Visible = false;
                //}

            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "preview")
            {
                LinkButton lnkView = (LinkButton)e.CommandSource;
                string penaltyId = lnkView.CommandArgument;
                H_SpecialIncrement penalty = H_SpecialIncrement.GetById(Convert.ToInt32(penaltyId));
                if (DateTime.Today.Date > penalty.EffectiveDate)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Edit not Permitted after effective Date";
                    ShowUIMessage(msg);
                    return;
                }
                txtLetterNo.Text = penalty.LetterNo;
                txtLetterDate.Text = UIUtility.Format(penalty.LetterDate);
                txtEffectiveDate.Text = UIUtility.Format(penalty.EffectiveDate);
                txtNumberOfIncrement.Text = UIUtility.Format(penalty.NumberOfIncrement);
                txtRemarks.Text = penalty.Remarks;

                hfPenalyId.Value = penalty.Id.ToString();
                this.Type = TYPE_EDIT;

            }
            
        }


    }
}
