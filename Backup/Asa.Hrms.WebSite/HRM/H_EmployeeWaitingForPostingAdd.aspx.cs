using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.WebSite.HRM
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

        private Asa.Hrms.Data.Entity.H_EmployeeWaitingForPosting GetH_EmployeeWaitingForPosting()
        {
            Asa.Hrms.Data.Entity.H_EmployeeWaitingForPosting h_EmployeeWaitingForPosting = new H_EmployeeWaitingForPosting();

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
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
                H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
                if (h_Employee != null)
                {
                    hdnId.Value = h_Employee.Id.ToString();

                    Asa.Hrms.Data.Entity.H_EmployeeWaitingForPosting h_EmployeeWaitingForPosting = this.GetH_EmployeeWaitingForPosting();

                    h_Employee.Status = H_Employee.Statuses.Waiting_For_Posting;
                    h_Employee.EmploymentType = H_Employee.EmploymentTypes.None;
                    
                    string desc = "Insert [H_EmployeeWaitingForPosting]";

                    this.TransactionManager = new TransactionManager(true, desc);

                    Asa.Hrms.Data.Entity.H_EmployeeWaitingForPosting.Insert(this.TransactionManager, h_EmployeeWaitingForPosting);

                    hdnId.Value = h_EmployeeWaitingForPosting.Id.ToString();
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
        {
            UIUtility.LoadEnums(ddlType, typeof(H_EmployeeWaitingForPosting.Types), false, false, false);
        }
    }
}
