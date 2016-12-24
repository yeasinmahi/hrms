using System;
using System.Web.UI.WebControls;
using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;
using System.Collections.Generic;

namespace Asa.Hrms.WebSite
{
    public partial class P_LoanAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "P_LOAN ADD"; }
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
                P_Loan p_Loan = Asa.Hrms.Data.Entity.P_Loan.GetById(Convert.ToInt32(hdnId.Value));

                if (p_Loan != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = p_Loan.Name;
                    txtInterestRate.Text = p_Loan.InterestRate.ToString();
                    txtSortOrder.Text = UIUtility.Format(p_Loan.SortOrder);

                    ddlStatus.SelectedValue = ((Int32)p_Loan.Status).ToString();

                }
            }
        }

        protected override string GetListPageUrl()
        {
            return "P_LoanList.aspx";
        }

        private Asa.Hrms.Data.Entity.P_Loan GetP_Earning()
        {
            Asa.Hrms.Data.Entity.P_Loan p_Loan = null;

            if (this.Type == TYPE_EDIT)
            {
                p_Loan = Asa.Hrms.Data.Entity.P_Loan.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                p_Loan = new Asa.Hrms.Data.Entity.P_Loan();
            }

            p_Loan.Name = DBUtility.ToString(txtName.Text);
            p_Loan.InterestRate = DBUtility.ToDouble(txtInterestRate.Text);
            p_Loan.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);
            p_Loan.Status = (P_Loan.Statuses)DBUtility.ToInt32(ddlStatus.SelectedValue);
            return p_Loan;
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
                Asa.Hrms.Data.Entity.P_Loan p_Loan = this.GetP_Earning();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [P_Loan]";
                }
                else
                {
                    desc = "Update [P_Loan]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Asa.Hrms.Data.Entity.P_Loan.Insert(this.TransactionManager, p_Loan);

                    hdnId.Value = p_Loan.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Asa.Hrms.Data.Entity.P_Loan.Update(this.TransactionManager, p_Loan);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }
    }
}
