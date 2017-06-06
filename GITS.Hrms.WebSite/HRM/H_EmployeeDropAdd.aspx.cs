using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeDropAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEDROP ADD"; }
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
            return "H_EmployeeDropList.aspx";
        }

        private H_EmployeeDropHistory GetH_EmployeeDrop()
        {
            H_EmployeeDropHistory h_EmployeeDrop = null;
            if (Type == TYPE_EDIT)
            {
                h_EmployeeDrop = H_EmployeeDropHistory.GetById(Convert.ToInt32(hdnId.Value));
                h_EmployeeDrop.Status = !chkCancel.Checked;
                h_EmployeeDrop.Cause = chkCancel.Checked ? "Cancel" : null;
            }
            else
            {
                h_EmployeeDrop = new H_EmployeeDropHistory();
                h_EmployeeDrop.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
                h_EmployeeDrop.Status = true;
                h_EmployeeDrop.EntryDate = DateTime.Now;
            }           
            h_EmployeeDrop.Type = (H_EmployeeDropHistory.Types)DBUtility.ToInt32(ddlType.SelectedValue);
            h_EmployeeDrop.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeeDrop.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);           
            h_EmployeeDrop.DropDate = DBUtility.ToDateTime(txtDropDate.Text);
            return h_EmployeeDrop;
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

                if (h_Employee.Status == H_Employee.Statuses.Dropped)// H_Employee.Statuses.Working && h_Employee.Status != H_Employee.Statuses.Consultancy)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    return msg;
                }
                if (Type == TYPE_ADD)
                {
                    H_EmployeeDropHistory drop = H_EmployeeDropHistory.Get("H_EmployeeId=" + h_Employee.Id + " AND Status=1");
                    if (drop != null)
                    {
                        msg.Type = MessageType.Error;
                        msg.Msg = "Invalid operation. Employee Drop Information already posted ";
                        return msg;
                    }
                }
                IList<UserRole> role = UserRole.FindByUserLogin(User.Identity.Name, "");

                foreach (UserRole ur in role)
                {
                    if (ur.RoleName.ToLower() == "rm")
                    {
                        if (ddlType.SelectedValue == ((int)H_EmployeeDrop.Types.Resignation).ToString())
                        {
                            H_EmployeeDesignation eDesignation = H_EmployeeDesignation.Find("H_EmployeeId=" + h_Employee.Id, " EndDate DESC")[0];
                            H_Designation h_Designation = H_Designation.GetById(eDesignation.H_DesignationId);
                            if (h_Designation.GroupType != H_Designation.GroupTypes.Peon && h_Designation.GroupType != H_Designation.GroupTypes.LO && h_Designation.GroupType != H_Designation.GroupTypes.ABM)
                            {
                                msg.Type = MessageType.Error;
                                msg.Msg = "You are not permitted to "+ddlType.SelectedItem.ToString()+" " + h_Designation.Name;
                                return msg;
                            }
                        }
                        else
                        {
                            msg.Type = MessageType.Error;
                            msg.Msg = "You are not permitted to "+ddlType.SelectedItem.ToString();
                            return msg;
                        }
                    }
                    if (ur.RoleName.ToLower() == "abm-cum-co")
                    {
                        if (ddlType.SelectedValue == ((int)H_EmployeeDrop.Types.Resignation).ToString() || ddlType.SelectedValue == ((int)H_EmployeeDrop.Types.Termination).ToString())
                        {
                            if (ddlType.SelectedValue == ((int)H_EmployeeDrop.Types.Termination).ToString())
                            {
                                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.Find("H_EmployeeId=" + h_Employee.Id, " EndDate DESC")[0];
                                H_Designation h_Designation = H_Designation.GetById(eDesignation.H_DesignationId);
                                if (h_Designation.GroupType != H_Designation.GroupTypes.Peon)
                                {
                                    msg.Type = MessageType.Error;
                                    msg.Msg = "You are not permitted to " + ddlType.SelectedItem.ToString() + " " + h_Designation.Name;
                                    return msg;
                                }
                            }
                        }
                        else
                        {
                            msg.Type = MessageType.Error;
                            msg.Msg = "You are not permitted to " + ddlType.SelectedItem.ToString();
                            return msg;
                        }
                    }
                }
            }

            //if (DBUtility.ToDateTime(txtLetterDate.Text.Trim()) > DBUtility.ToDateTime(txtDropDate.Text.Trim()))
            //{
            //    msg.Type = MessageType.Error;
            //    msg.Msg = "Drop date should be greater than or equal to letter date";
            //    return msg;
            //}

            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
                if (h_Employee != null)
                {
                    H_EmployeeDropHistory h_EmployeeDrop = GetH_EmployeeDrop();

                    string desc = string.Empty;
                    if (Type == TYPE_ADD)
                    {
                        desc = "Insert [H_EmployeeDropHistory]";
                    }
                    else
                    {
                        desc = "Update [H_EmployeeDropHistory]";
                    }
                    TransactionManager = new TransactionManager(true, desc);
                    if (Type == TYPE_ADD)
                    {

                        H_EmployeeDropHistory.Insert(TransactionManager, h_EmployeeDrop);
                    }
                    else
                    {
                        H_EmployeeDropHistory.Update(TransactionManager, h_EmployeeDrop);
                    }
                   // GITS.Hrms.Data.Entity.H_Employee.Update(this.TransactionManager, h_Employee);
                    //if (h_Employee.Status == H_Employee.Statuses.Consultancy)
                    //{
                    //    IList<H_EmployeeConsultency> h_ConsultencyList = GITS.Hrms.Data.Entity.H_EmployeeConsultency.Find("H_EmployeeId=" + Convert.ToInt32(hdnId.Value) + " AND Status=1", "");
                    //    if (h_ConsultencyList.Count > 0)
                    //    {
                    //        foreach (H_EmployeeConsultency consultency in h_ConsultencyList)
                    //        {
                    //            consultency.Status = H_EmployeeConsultency.Statuses.INACTIVE;
                    //            consultency.EndDate = DBUtility.ToDateTime(txtDropDate.Text);
                    //           // GITS.Hrms.Data.Entity.H_EmployeeConsultency.Update(this.TransactionManager, consultency);
                    //        }
                    //    }
                    //}
                    //if (h_Employee.Status == H_Employee.Statuses.In_Leave)
                    //{
                    //    IList<H_EmployeeLeave> list = GITS.Hrms.Data.Entity.H_EmployeeLeave.Find("H_EmployeeId=" + h_Employee.Id + " AND Status=1", "");
                    //    if (list.Count > 0)
                    //    {
                    //        foreach (H_EmployeeLeave leave in list)
                    //        {
                    //            leave.EndDate = DBUtility.ToDateTime(txtDropDate.Text).AddDays(-1);
                    //            leave.Status = 2;
                    //          //  H_EmployeeLeave.Update(this.TransactionManager, leave);
                    //        }
                    //    }
                    //}

                    hdnId.Value = h_EmployeeDrop.Id.ToString();
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
            UIUtility.LoadEnums(ddlType, typeof(H_EmployeeDrop.Types), false, false, true);
            chkCancel.Checked = false;
            chkCancel.Visible = false;
            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                H_EmployeeDropHistory h_EmployeeDropHistory = H_EmployeeDropHistory.GetById(Convert.ToInt32(hdnId.Value));

                if (h_EmployeeDropHistory != null)
                {
                    chkCancel.Visible = true;
                    Type = TYPE_EDIT;
                    txtLetterNo.Text = h_EmployeeDropHistory.LetterNo;
                    txtLetterDate.Text = UIUtility.Format(h_EmployeeDropHistory.LetterDate);
                    txtDropDate.Text = UIUtility.Format(h_EmployeeDropHistory.DropDate);
                    ddlType.SelectedValue = ((Int32)h_EmployeeDropHistory.Type).ToString();

                    H_Employee h_Employee = H_Employee.GetById(h_EmployeeDropHistory.H_EmployeeId);
                    if (h_Employee != null)
                    {
                        TransactionManager tm = new TransactionManager(false);
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

                        IList<H_EmployeeDrop> drop = H_EmployeeDrop.FindByH_EmployeeId(h_Employee.Id, "");
                        var result = from d in drop.ToList()
                                     select new
                                     {
                                         Name = h_Employee.Name,
                                         Code = h_Employee.Code,
                                         Letter_No = d.LetterNo,
                                         Letter_Date = d.LetterDate,
                                         Drop_Date = d.DropDate,
                                         Drop_Type = d.Type.ToString()
                                     };
                        gvDrop.DataSource = UIUtility.LINQToDataTable(result);
                        gvDrop.DataBind();

                    }

                }
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            TransactionManager tm = new TransactionManager(false);
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
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
                //txtZone.Text = Zone.GetById(Subzone.GetById(region.SubzoneId).ZoneId).Name;
                //txtZoneDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[z]["StartDate"]));
                txtDistrict.Text = Subzone.GetById(region.SubzoneId).Name;
                //txtSubzoneDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[s]["StartDate"]));
                //txtRegion.Text = region.Name;
                //txtRegionDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[r]["StartDate"]));
                txtBranch.Text = branch.Name;
                // txtBranchDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[b]["StartDate"]));
                // hdnBranch.Value = branch.Id.ToString();
                hdnId.Value = h_Employee.Id.ToString();

                    IList<H_EmployeeDrop> drop = H_EmployeeDrop.FindByH_EmployeeId(h_Employee.Id,"");
                    var result = from d in drop.ToList()
                                 select new
                                 {
                                     Name = h_Employee.Name,
                                     Code = h_Employee.Code,
                                     Letter_No = d.LetterNo,
                                     Letter_Date = d.LetterDate,
                                     Drop_Date = d.DropDate,
                                     Drop_Type = d.Type.ToString()
                                 };
                    gvDrop.DataSource = UIUtility.LINQToDataTable(result);
                    gvDrop.DataBind();
                
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
