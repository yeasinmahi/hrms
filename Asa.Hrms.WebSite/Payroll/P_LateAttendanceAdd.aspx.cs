using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Payroll
{
    public partial class P_LateAttendanceAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "LATEATTENDANCE ADD"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override string GetListPageUrl()
        {
            return "P_LateAttendanceList.aspx";
        }

        private P_LateAttendance GetAttendance()
        {
            P_LateAttendance attendance = null;

            if (this.Type == TYPE_EDIT)
            {
                attendance = P_LateAttendance.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                attendance = new P_LateAttendance();
                attendance.H_EmployeeId = Convert.ToInt32(hdnId.Value);
            }
            attendance.StartDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1);
            attendance.EndDate = (new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1)).AddMonths(1).AddDays(-1);
            attendance.Late96_930 = DBUtility.ToNullableInt32(txtLate96_930.Text);
            attendance.Late931_days = DBUtility.ToNullableInt32(txtLate931_days.Text);
            attendance.Absent = DBUtility.ToNullableInt32(txtAbsent.Text);
      
            return attendance;
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
            if(Type==TYPE_ADD)
            {
                DateTime startDate=new DateTime(Convert.ToInt32(ddlYear.SelectedValue),Convert.ToInt32(ddlMonth.SelectedValue),1);
                IList<P_LateAttendance> late = P_LateAttendance.Find("H_EmployeeId="+hdnId.Value+" AND StartDate='"+startDate+"'","");
                if (late != null && late.Count > 0)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Already Saved for yhis Employee";
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
                P_LateAttendance division = this.GetAttendance();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [P_LateAttendance]";
                }
                else
                {
                    desc = "Update [P_LateAttendance]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    P_LateAttendance.Insert(this.TransactionManager, division);

                    hdnId.Value = division.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    P_LateAttendance.Update(this.TransactionManager, division);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            int index=0;
            ddlYear.Items.Insert(index++, new ListItem("Select Year", "0"));
            for (int year = DateTime.Today.Year - 1; year <= DateTime.Today.Year + 1; year++)
            {
                ddlYear.Items.Insert(index++, new ListItem(year.ToString(), year.ToString()));
            }
            P_LateAttendance division = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                division = P_LateAttendance.GetById(Convert.ToInt32(hdnId.Value));
                H_Employee h_Employee = H_Employee.GetById(division.H_EmployeeId);
                txtEmployee.Text = h_Employee.Code.ToString() + ":" + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                TransactionManager tm = new TransactionManager(false);
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
                if (division != null)
                {
                    this.Type = TYPE_EDIT;
                    ddlYear.SelectedValue = division.StartDate.Year.ToString();
                    ddlMonth.SelectedValue = division.StartDate.Month.ToString();
                    txtLate96_930.Text = DBUtility.ToNullableString( division.Late96_930);
                    txtLate931_days.Text = DBUtility.ToNullableString(division.Late931_days);
                    txtAbsent.Text = DBUtility.ToNullableString(division.Absent);

                
                }
            }
        }

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

