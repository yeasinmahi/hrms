using System;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Payroll
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
                P_Loan p_Loan = P_Loan.GetById(Convert.ToInt32(hdnId.Value));

                if (p_Loan != null)
                {
                    Type = TYPE_EDIT;

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

        private P_Loan GetP_Earning()
        {
            P_Loan p_Loan = null;

            if (Type == TYPE_EDIT)
            {
                p_Loan = P_Loan.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                p_Loan = new P_Loan();
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

            if (IsValid == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid data provided or required data missing";
                return msg;
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                P_Loan p_Loan = GetP_Earning();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [P_Loan]";
                }
                else
                {
                    desc = "Update [P_Loan]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    P_Loan.Insert(TransactionManager, p_Loan);

                    hdnId.Value = p_Loan.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    P_Loan.Update(TransactionManager, p_Loan);
                }

                TransactionManager.Commit();
            }

            return msg;
        }
    }
}
