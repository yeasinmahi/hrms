using System;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Payroll
{
    public partial class P_DeductionAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "P_DEDUCTION ADD"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlStatus, typeof(P_Earning.Statuses), false, false, true);
            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                P_Deduction h_Designation = P_Deduction.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Designation != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = h_Designation.Name;
                    chkIsFixed.Checked = h_Designation.IsFixed;
                    txtSortOrder.Text = UIUtility.Format(h_Designation.SortOrder);
                    ddlStatus.SelectedValue = ((Int32)h_Designation.Status).ToString();

                }
            }
        }

        protected override string GetListPageUrl()
        {
            return "P_DeductionList.aspx";
        }

        private P_Deduction GetP_Earning()
        {
            P_Deduction h_Designation = null;

            if (this.Type == TYPE_EDIT)
            {
                h_Designation = P_Deduction.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Designation = new P_Deduction();
            }

            h_Designation.Name = DBUtility.ToString(txtName.Text);
            h_Designation.IsFixed = chkIsFixed.Checked;
            h_Designation.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);
            h_Designation.Status = (P_Deduction.Statuses)DBUtility.ToInt32(ddlStatus.SelectedValue);
            return h_Designation;
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

        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                P_Deduction h_Designation = this.GetP_Earning();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [P_Deduction]";
                }
                else
                {
                    desc = "Update [P_Deduction]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    P_Deduction.Insert(this.TransactionManager, h_Designation);

                    hdnId.Value = h_Designation.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    P_Deduction.Update(this.TransactionManager, h_Designation);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }
    }
}
