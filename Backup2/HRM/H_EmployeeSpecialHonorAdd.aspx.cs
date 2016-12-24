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
    public partial class H_EmployeeSpecialHonorAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEESPECIALHONOR ADD"; }
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
            return "H_EmployeeSpecialHonorList.aspx";
        }

        private Asa.Hrms.Data.Entity.H_EmployeeSpecialHonor GetH_EmployeeSpecialHonor()
        {
            Asa.Hrms.Data.Entity.H_EmployeeSpecialHonor h_EmployeeSpecialHonor = new H_EmployeeSpecialHonor();

            h_EmployeeSpecialHonor.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
            h_EmployeeSpecialHonor.SubjectOfHonor = DBUtility.ToString(txtSubjectofHonor.Text);
            h_EmployeeSpecialHonor.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeeSpecialHonor.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);

            return h_EmployeeSpecialHonor;
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

                if (h_Employee.Status != H_Employee.Statuses.Working && h_Employee.Status != H_Employee.Statuses.Consultancy)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    return msg;
                }
                IList<H_EmployeeSpecialHonor> honorList = H_EmployeeSpecialHonor.Find("H_EmployeeID = " + h_Employee.Id + " AND LetterDate= '" + DBUtility.ToDateTime(this.txtLetterDate.Text).ToString(Configuration.DatabaseDateFormat) + "'", "");// + "' AND '" + DBUtility.ToDateTime(this.txtEndDate.Text).ToString(Configuration.DatabaseDateFormat) + "'", "Id DESC");

                if (honorList.Count > 0)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Special Honor already exists, Letter Date: " + honorList[0].LetterDate.ToShortDateString();
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

                    Asa.Hrms.Data.Entity.H_EmployeeSpecialHonor h_EmployeeSpecialHonor = this.GetH_EmployeeSpecialHonor();
                    string desc = "Insert [H_EmployeeSpecialHonor]";

                    this.TransactionManager = new TransactionManager(true, desc);

                    Asa.Hrms.Data.Entity.H_EmployeeSpecialHonor.Insert(this.TransactionManager, h_EmployeeSpecialHonor);

                    hdnId.Value = h_EmployeeSpecialHonor.Id.ToString();
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
    }
}
