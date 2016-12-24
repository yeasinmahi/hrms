using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;
using System.Collections.Generic;

namespace Asa.Hrms.WebSite.Payroll
{
    public partial class P_EarningAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "P_EARNING ADD"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected override void LoadData()
        {
            ddlParent.DataSource = P_Earning.Find("Status=1", "Name");
            ddlParent.DataBind();
            ddlParent.Items.Insert(0, new ListItem("No Parent", ""));
            UIUtility.LoadEnums(ddlStatus, typeof(P_Earning.Statuses), false, false, true);
            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                P_Earning h_Designation = Asa.Hrms.Data.Entity.P_Earning.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Designation != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = h_Designation.Name;
                    chkIsBasic.Checked = h_Designation.IsBasic;
                    chkIsFixed.Checked = h_Designation.IsFixed;
                    chkIsPaySlip.Checked = h_Designation.IsPaySlip;
                    ddlParent.SelectedValue = h_Designation.ParentId == null ? "" : h_Designation.ParentId.ToString();
                    txtSortOrder.Text = UIUtility.Format(h_Designation.SortOrder);
                    ddlStatus.SelectedValue = ((Int32)h_Designation.Status).ToString();
                    
                }
            }
        }

        protected override string GetListPageUrl()
        {
            return "P_EarningList.aspx";
        }

        private Asa.Hrms.Data.Entity.P_Earning GetP_Earning()
        {
            Asa.Hrms.Data.Entity.P_Earning h_Designation = null;

            if (this.Type == TYPE_EDIT)
            {
                h_Designation = Asa.Hrms.Data.Entity.P_Earning.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Designation = new Asa.Hrms.Data.Entity.P_Earning();
            }

            h_Designation.Name = DBUtility.ToString(txtName.Text);
            h_Designation.IsBasic =chkIsBasic.Checked;
            h_Designation.IsFixed = chkIsFixed.Checked;
            h_Designation.IsPaySlip = chkIsPaySlip.Checked;
            h_Designation.ParentId = DBUtility.ToNullableInt32(ddlParent.SelectedValue);
            h_Designation.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);
            h_Designation.Status = (P_Earning.Statuses)DBUtility.ToInt32(ddlStatus.SelectedValue);
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
            if (chkIsBasic.Checked)
            {
                IList<P_Earning> basic = P_Earning.Find("IsBasic=1", "");
                if (basic.Count > 0)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Already have an earning item as Basic";
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
                Asa.Hrms.Data.Entity.P_Earning h_Designation = this.GetP_Earning();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [P_Earning]";
                }
                else
                {
                    desc = "Update [P_Earning]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Asa.Hrms.Data.Entity.P_Earning.Insert(this.TransactionManager, h_Designation);

                    hdnId.Value = h_Designation.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Asa.Hrms.Data.Entity.P_Earning.Update(this.TransactionManager, h_Designation);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }
    }
}
