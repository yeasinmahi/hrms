using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeActingDesignationAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEACTINGDESIGNATION ADD"; }
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
            return "H_EmployeeActingDesignationList.aspx";
        }

        private H_EmployeeActingDesignation GetH_EmployeeActingDesignation()
        {
            H_EmployeeActingDesignation h_EmployeeActingDesignation = new H_EmployeeActingDesignation();

            h_EmployeeActingDesignation.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
            h_EmployeeActingDesignation.InchargedGradeId = DBUtility.ToInt32(ddlGradeId.SelectedValue);
            h_EmployeeActingDesignation.InchargedDesignationId = DBUtility.ToInt32(ddlDesignationId.SelectedValue);
            h_EmployeeActingDesignation.FromDate = DBUtility.ToDateTime(txtFromDate.Text);
            h_EmployeeActingDesignation.ToDate = DBUtility.ToDateTime(txtToDate.Text);

            return h_EmployeeActingDesignation;
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

            if ((txtGrade.Text == ddlGradeId.SelectedItem.ToString()) && (txtDesignation.Text == ddlDesignationId.SelectedItem.ToString()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid data (same grade & designation can not be assigned)";
                return msg;
            }

            //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (h_Employee.AppointmentLetterDate >= DBUtility.ToDateTime(txtFromDate.Text))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "From date should be greater than appointment letter date (" + h_Employee.AppointmentLetterDate + ")";
                    return msg;
                }
            }

            if (DBUtility.ToDateTime(txtFromDate.Text.Trim()) > DBUtility.ToDateTime(txtToDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "To date should be greater than or equal to from date";
                return msg;
            }

            if (h_Employee.Status != H_Employee.Statuses.Working && h_Employee.Status != H_Employee.Statuses.Consultancy)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                return msg;
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                H_EmployeeActingDesignation h_EmployeeActingDesignation = GetH_EmployeeActingDesignation();
                string desc = "Insert [H_EmployeeActingDesignation]";

                TransactionManager = new TransactionManager(true, desc);

                H_EmployeeActingDesignation.Insert(TransactionManager, h_EmployeeActingDesignation);

                hdnId.Value = h_EmployeeActingDesignation.Id.ToString();
                Type = TYPE_EDIT;


                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            ddlGradeId.DataSource = H_Grade.FindAll();
            ddlGradeId.DataBind();
            ddlGradeId_SelectedIndexChanged(ddlGradeId, new EventArgs());
        }

        protected void ddlGradeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGradeId.SelectedValue != null && ddlGradeId.SelectedValue != "")
            {
                TransactionManager tm = new TransactionManager(false);

                ddlDesignationId.DataSource = tm.GetDataSet("SELECT H_Designation.Id, Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + ddlGradeId.SelectedValue + " ORDER BY SortOrder").Tables[0];
                ddlDesignationId.DataBind();
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                hdnId.Value = h_Employee.Id.ToString();

                H_EmployeeDepartment eDepartment = H_EmployeeDepartment.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                txtDepartment.Text = H_Department.GetById(eDepartment.H_DepartmentId).Name;

                IList<H_EmployeeGrade> eGrades = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC");
                txtGrade.Text = H_Grade.GetById(eGrades[0].H_GradeId).Name;
                //this.ddlGradeId.SelectedValue = eGrades[0].H_GradeId.ToString();
                //this.ddlGradeId_SelectedIndexChanged(this.ddlGradeId, new EventArgs());

                IList<H_EmployeeDesignation> eDesignations = H_EmployeeDesignation.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC");
                txtDesignation.Text = H_Designation.GetById(eDesignations[0].H_DesignationId).Name;
                //this.ddlDesignationId.SelectedValue = eDesignations[0].H_DesignationId.ToString();

                H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                Branch branch = Branch.GetById(eBranch.BranchId);
                Region region = Region.GetById(branch.RegionId);

                txtZone.Text = Zone.GetById(Subzone.GetById(region.SubzoneId).ZoneId).Name;
                txtSubzone.Text = Subzone.GetById(region.SubzoneId).Name;
                txtRegion.Text = region.Name;
                txtBranch.Text = branch.Name;
            }
            else
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";

                hdnId.Value = "0";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";
                txtZone.Text = "";
                txtSubzone.Text = "";
                txtRegion.Text = "";
                txtBranch.Text = "";

                ShowUiMessage(msg);
            }
        }
    }
}
