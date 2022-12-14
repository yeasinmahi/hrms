using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeWaitingForPostingAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEWAITINGFORPOSTING ADD"; }
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
            return "H_EmployeeWaitingForPostingList.aspx";
        }

        private H_EmployeeWaitingForPosting GetH_EmployeeWaitingForPosting()
        {
            H_EmployeeWaitingForPosting h_EmployeeWaitingForPosting = new H_EmployeeWaitingForPosting();

            h_EmployeeWaitingForPosting.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
            h_EmployeeWaitingForPosting.Type = (H_EmployeeWaitingForPosting.Types)DBUtility.ToInt32(ddlType.SelectedValue);
            h_EmployeeWaitingForPosting.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeeWaitingForPosting.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeeWaitingForPosting.StartDate = DBUtility.ToDateTime(txtStartDate.Text);

            return h_EmployeeWaitingForPosting;
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

            //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (h_Employee.AppointmentLetterDate >= DBUtility.ToDateTime(txtLetterDate.Text))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Letter date should be greater than appointment letter date (" + h_Employee.AppointmentLetterDate + ")";
                    return msg;
                }

                if (h_Employee.AppointmentLetterDate >= DBUtility.ToDateTime(txtStartDate.Text))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Start date should be greater than appointment letter date (" + h_Employee.AppointmentLetterDate + ")";
                    return msg;
                }

                if (h_Employee.Status != H_Employee.Statuses.Working && h_Employee.Status != H_Employee.Statuses.Consultancy)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    return msg;
                }
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
                H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
                if (h_Employee != null)
                {
                    hdnId.Value = h_Employee.Id.ToString();

                    H_EmployeeWaitingForPosting h_EmployeeWaitingForPosting = GetH_EmployeeWaitingForPosting();

                    h_Employee.Status = H_Employee.Statuses.Waiting_For_Posting;
                    h_Employee.EmploymentType = H_Employee.EmploymentTypes.None;
                    
                    string desc = "Insert [H_EmployeeWaitingForPosting]";

                    TransactionManager = new TransactionManager(true, desc);

                    H_EmployeeWaitingForPosting.Insert(TransactionManager, h_EmployeeWaitingForPosting);

                    hdnId.Value = h_EmployeeWaitingForPosting.Id.ToString();
                    Type = TYPE_EDIT;

                    TransactionManager.Commit();
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
        {
            UIUtility.LoadEnums(ddlType, typeof(H_EmployeeWaitingForPosting.Types), false, false, false);
        }
    }
}
