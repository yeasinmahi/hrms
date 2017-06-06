using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Payroll
{
    public partial class P_LoanAccountAdd : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            txtDurationMonth.Visible = false;
            ddlLoan.DataSource = P_Loan.Find("Status=1", "Name");
            ddlLoan.DataBind();
            ddlLoan.Items.Insert(0, new ListItem("Select Loan Type","0"));
            UIUtility.LoadEnums(ddlStatus, typeof(P_LoanAccount.Statuses), false, false,false);
            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                P_LoanAccount p_LoanAccount = P_LoanAccount.GetById(Convert.ToInt32(Request.QueryString["Id"]));
                H_Employee h_Employee = H_Employee.GetById(p_LoanAccount.H_EmployeeId);

                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                H_EmployeeDepartment eDepartment = H_EmployeeDepartment.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDepartment.Text = H_Department.GetById(eDepartment.H_DepartmentId).Name;

                H_EmployeeGrade eGrade = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                txtGrade.Text = H_Grade.GetById(eGrade.H_GradeId).Name;

                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;

                ddlLoan.SelectedValue = p_LoanAccount.P_LoanId.ToString();
                txtLoanAmount.Text = p_LoanAccount.LoanAmount.ToString();
                txtInterestRate.Text = p_LoanAccount.InterestRate.ToString();
                ddlDuration.SelectedValue = (p_LoanAccount.NumberOfInstallment / 12).ToString();
            }
        }

        protected override string GetListPageUrl()
        {
            return "P_LoanAccountList.aspx";
        }
        private P_LoanAccount GetP_LoanAccount()
        {
            P_LoanAccount p_LoanAccount = null;
            if (Type == TYPE_EDIT)
            {
                p_LoanAccount = P_LoanAccount.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                p_LoanAccount = new P_LoanAccount();
            }

            p_LoanAccount.H_EmployeeId = Convert.ToInt32(hdnId.Value);
            p_LoanAccount.P_LoanId = Convert.ToInt32(ddlLoan.SelectedValue);
            p_LoanAccount.DisbursDate = DBUtility.ToDateTime(txtDisburseDate.Text);
            p_LoanAccount.LoanAmount = DBUtility.ToDouble(txtLoanAmount.Text);
            p_LoanAccount.InterestRate = DBUtility.ToDouble(txtInterestRate.Text);
            p_LoanAccount.InterestAmount = DBUtility.ToDouble(txtInterestAmount.Text);
            p_LoanAccount.NumberOfInstallment = DBUtility.ToInt32(ddlDuration.SelectedValue) * 12;
            p_LoanAccount.TotalAmount = DBUtility.ToDouble(txtTotalAmount.Text);
            p_LoanAccount.InstallmentAmount = DBUtility.ToDouble(txtInstAmount.Text);
            p_LoanAccount.Status = (P_LoanAccount.Statuses)Convert.ToInt32(ddlStatus.SelectedValue);

            return p_LoanAccount;

        }
        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                string desc = null;
                P_LoanAccount p_LoanAccount = GetP_LoanAccount();
                if (Type == TYPE_EDIT)
                {
                    desc = "Update [P_LoanAccount]";
                }
                else
                {
                    desc = "Insert [P_LoanAccount]";
                }

                TransactionManager = new TransactionManager(true, desc);
                if (Type == TYPE_EDIT)
                {
                    P_LoanAccount.Update(TransactionManager, p_LoanAccount);
                }
                else
                {
                    P_LoanAccount.Insert(TransactionManager, p_LoanAccount);
                }

                hdnId.Value = p_LoanAccount.Id.ToString();
                Type = TYPE_EDIT;
                

                TransactionManager.Commit();
            }

            return msg;
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
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                hdnId.Value = "0";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";

                if (h_Employee.Status != H_Employee.Statuses.Working)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    ShowUiMessage(msg);
                    return;
                }
                
                Type = TYPE_ADD;
                hdnId.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                H_EmployeeDepartment eDepartment = H_EmployeeDepartment.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDepartment.Text = H_Department.GetById(eDepartment.H_DepartmentId).Name;

                H_EmployeeGrade eGrade = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                txtGrade.Text = H_Grade.GetById(eGrade.H_GradeId).Name;

                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                int year = DateTime.Today.Year - h_Employee.DateOfBirth.Value.Year;
                int month = DateTime.Today.Month - h_Employee.DateOfBirth.Value.Month;
                int day = DateTime.Today.Day - h_Employee.DateOfBirth.Value.Day;
                if (day < 0) {day =day+ 30; month = month - 1;}
                if (month < 0) { month = month + 12; year = year - 1; }
                txtAge.Text = year.ToString() + " year(s), " + month.ToString() + " Month(s) " + day.ToString() + " Days";

                year = DateTime.Today.Year - h_Employee.JoiningDate.Value.Year;
                month = DateTime.Today.Month - h_Employee.JoiningDate.Value.Month;
                day = DateTime.Today.Day - h_Employee.JoiningDate.Value.Day;
                if (day < 0) { day = day + 30; month = month - 1; }
                if (month < 0) { month = month + 12; year = year - 1; }
                txtServiceLength.Text = year.ToString() + "y, " + month.ToString() + "m " + day.ToString() + "d";

                year = h_Employee.DateOfBirth.Value.AddYears(60).Year - DateTime.Today.Year;
                month = h_Employee.DateOfBirth.Value.AddYears(60).Month - DateTime.Today.Month;
                day = h_Employee.DateOfBirth.Value.AddYears(60).Day - DateTime.Today.Day;
                if (day < 0) { day = day + 30; month = month - 1; }
                if (month < 0) { month = month + 12; year = year - 1; }
                txtServiceLeft.Text = year.ToString() + "y, " + month.ToString() + "m " + day.ToString() + "d";
            }
            else
            {
                hdnId.Value = "0";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";


                if (txtEmployee.Text.Trim() != "")
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "No employee found";
                    ShowUiMessage(msg);
                }
            }
        }

        protected void ddlLoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLoan.SelectedValue != "0")
            {
                P_Loan loan = P_Loan.GetById(Convert.ToInt32(ddlLoan.SelectedValue));
                txtInterestRate.Text = loan.InterestRate.ToString();
            }
        }
    }
}
