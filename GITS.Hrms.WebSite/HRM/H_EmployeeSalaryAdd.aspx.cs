using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeSalaryAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEESALARY ADD"; }
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
            return "H_EmployeeSalaryList.aspx";
        }

        private H_EmployeeSalary GetH_EmployeeSalary()
        {
            H_EmployeeSalary h_EmployeeSalary = new H_EmployeeSalary();

            h_EmployeeSalary.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
            h_EmployeeSalary.BasicSalary = DBUtility.ToDouble(txtBasicSalary.Text);
            h_EmployeeSalary.LastIncrementDate = DBUtility.ToDateTime(txtLastIncrementDate.Text);

            return h_EmployeeSalary;
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
                if (h_Employee.AppointmentLetterDate >= DBUtility.ToDateTime(txtLastIncrementDate.Text))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Last increment date should be greater than appointment letter date (" + h_Employee.AppointmentLetterDate + ")";
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
                //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
                H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
                if (h_Employee != null)
                {
                    hdnId.Value = h_Employee.Id.ToString();

                    H_EmployeeSalary h_EmployeeSalary = this.GetH_EmployeeSalary();
                    string desc = "Insert [H_EmployeeSalary]";

                    this.TransactionManager = new TransactionManager(true, desc);

                    H_EmployeeSalary.Insert(this.TransactionManager, h_EmployeeSalary);

                    hdnId.Value = h_EmployeeSalary.Id.ToString();
                    this.Type = TYPE_EDIT;

                    this.TransactionManager.Commit();
                }
                else
                {
                    msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "No employee found";

                    hdnId.Value = "0";
                }
            }

            return msg;
        }

        protected override void LoadData()
        { }

    }
}
