using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeTransferAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEETRANSFER ADD"; }
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
            return "H_EmployeeTransferList.aspx";
        }

        private H_EmployeeTransferHistory GetH_EmployeeTransfer()
        {
            H_EmployeeTransferHistory h_EmployeeTransfer = new H_EmployeeTransferHistory();
            if (this.Type == TYPE_EDIT)
            {
                h_EmployeeTransfer = H_EmployeeTransferHistory.GetById(Convert.ToInt32(hdnId.Value));
                if (chkCancel.Checked == true)
                {
                    h_EmployeeTransfer.Status = H_EmployeeTransferHistory.Statuses.INACTIVE;
                }
                else
                {
                    h_EmployeeTransfer.Status = H_EmployeeTransferHistory.Statuses.ACTIVE;
                }
            }
            else
            {
                h_EmployeeTransfer = new H_EmployeeTransferHistory();
                h_EmployeeTransfer.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
                h_EmployeeTransfer.Status = H_EmployeeTransferHistory.Statuses.ACTIVE;
                h_EmployeeTransfer.UserLogin = User.Identity.Name;
            }
        
            h_EmployeeTransfer.Type = (H_EmployeeTransferHistory.Types)DBUtility.ToInt32(ddlType.SelectedValue);
            h_EmployeeTransfer.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeeTransfer.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeeTransfer.SourceBranchId = DBUtility.ToInt32(this.hdnBranch.Value);
            h_EmployeeTransfer.DestinationBranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
            h_EmployeeTransfer.JoiningDate = DBUtility.ToDateTime(txtJoiningDate.Text);
            h_EmployeeTransfer.Remarks = DBUtility.ToNullableString(txtRemarks.Text);
            h_EmployeeTransfer.EntryDateTime = DateTime.Now;
        

            return h_EmployeeTransfer;
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

            if (DBUtility.ToInt32(ddlBranch.SelectedValue) == DBUtility.ToInt32(this.hdnBranch.Value))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Transfered branch should not be current branch";
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

                if (h_Employee.Status != H_Employee.Statuses.Working)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    return msg;
                }
                IList<UserRole> role = UserRole.FindByUserLogin(User.Identity.Name, "");

                foreach (UserRole ur in role)
                {
                    if (ur.RoleName.ToLower() == "abm-cum-co")
                    {
                        H_EmployeeDesignation eDesignation = H_EmployeeDesignation.Find("H_EmployeeId=" + h_Employee.Id, " EndDate DESC")[0];
                        H_Designation h_Designation=H_Designation.GetById(eDesignation.H_DesignationId);
                        if (h_Designation.GroupType != H_Designation.GroupTypes.Peon && h_Designation.GroupType != H_Designation.GroupTypes.LO 
                            && h_Designation.GroupType != H_Designation.GroupTypes.Sr_LO && h_Designation.GroupType != H_Designation.GroupTypes.ABM
                            && h_Designation.GroupType != H_Designation.GroupTypes.BM && h_Designation.GroupType != H_Designation.GroupTypes.SBM)
                        {
                            msg.Type = MessageType.Error;
                            msg.Msg = "You are not permitted to Transfer " + h_Designation.Name;
                            return msg;
                        }
                    }
                }
                //if (this.Type == TYPE_EDIT)
                //{
                //    Asa.Hrms.Data.Entity.H_EmployeeTransferHistory h_history = H_EmployeeTransferHistory.Get("H_EmployeeId="+h_Employee.Id+" AND Status=1");
                //    if (h_history.DestinationBranchId != Convert.ToInt32(ddlBranch.SelectedValue))
                //    {
                //        msg.Type = MessageType.Error;
                //        msg.Msg = "You can not edit Transferred Branch ";
                //        return msg;
                //    }
                //}
            }
        
            return msg;
        }

        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                string desc = null;
                H_EmployeeTransferHistory h_EmployeeTransfer = this.GetH_EmployeeTransfer();
                if (this.Type == TYPE_EDIT)
                {
                    desc = "Update [H_EmployeeTransferHistory]";
                }
                else
                {
                    desc = "Insert [H_EmployeeTransferHistory]";
                }

                this.TransactionManager = new TransactionManager(true, desc);
                if (this.Type == TYPE_EDIT)
                {
                    H_EmployeeTransferHistory.Update(this.TransactionManager, h_EmployeeTransfer);
                }
                else
                {
                    H_EmployeeTransferHistory.Insert(this.TransactionManager, h_EmployeeTransfer);
                }

                hdnId.Value = h_EmployeeTransfer.Id.ToString();
                this.Type = TYPE_EDIT;
                /*
            H_EmployeeBranch eBranch = H_EmployeeBranch.Find(this.TransactionManager, "H_EmployeeId=" + h_EmployeeTransfer.H_EmployeeId, "EndDate DESC")[0];

            eBranch.EndDate = h_EmployeeTransfer.JoiningDate.AddDays(-1);
            H_EmployeeBranch.Update(this.TransactionManager, eBranch);

            eBranch = new H_EmployeeBranch();
            eBranch.H_EmployeeId = h_EmployeeTransfer.H_EmployeeId;
            eBranch.BranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
            eBranch.StartDate = h_EmployeeTransfer.JoiningDate;
            eBranch.EndDate = new DateTime(2099, 12, 31);
            H_EmployeeBranch.Insert(this.TransactionManager, eBranch);
             */

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            rvRemarks.Enabled = false;
            chkCancel.Visible = false;
            UIUtility.LoadEnums(ddlType, typeof(H_EmployeeTransfer.Types), false, false, false);

            this.ddlSubzone.DataSource = Subzone.Find("Status=1", "Name");//, User.Identity.Name);
            this.ddlSubzone.DataBind();
            this.ddlSubzone_SelectedIndexChanged(ddlRegion, new EventArgs());
            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                H_EmployeeTransferHistory h_History = H_EmployeeTransferHistory.GetById(Convert.ToInt32(hdnId.Value));

                if (h_History != null)
                {
                    chkCancel.Visible = true;
                    this.Type = TYPE_EDIT;
                    txtLetterNo.Text = h_History.LetterNo;
                    txtLetterDate.Text = UIUtility.Format(h_History.LetterDate);
                    txtJoiningDate.Text = UIUtility.Format(h_History.JoiningDate);
                    txtRemarks.Text = h_History.Remarks;
                    ddlType.SelectedValue = ((Int32)h_History.Type).ToString();
                    Branch dBranch = Branch.GetById(h_History.DestinationBranchId);
                    Region dRegion = Region.GetById(dBranch.RegionId);
                    Subzone dSubzone = Subzone.GetById(dRegion.SubzoneId);
                    ddlSubzone.SelectedValue = dRegion.SubzoneId.ToString();
                    ddlSubzone_SelectedIndexChanged(null, null);
                    ddlRegion.SelectedValue = dRegion.Id.ToString();
                    ddlRegion_SelectedIndexChanged(null, null);
                    ddlBranch.SelectedValue = dBranch.Id.ToString();
                    H_Employee h_Employee = H_Employee.GetById(h_History.H_EmployeeId);
                    txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                    H_EmployeeDepartment eDepartment = H_EmployeeDepartment.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                    txtDepartment.Text = H_Department.GetById(eDepartment.H_DepartmentId).Name;

                    H_EmployeeGrade eGrade = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                    txtGrade.Text = H_Grade.GetById(eGrade.H_GradeId).Name;

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

                    txtZone.Text = Zone.GetById(Subzone.GetById(region.SubzoneId).ZoneId).Name;
                    txtZoneDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[z]["StartDate"]));

                    txtSubzone.Text = Subzone.GetById(region.SubzoneId).Name;
                    txtSubzoneDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[s]["StartDate"]));

                    txtRegion.Text = region.Name;
                    txtRegionDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[r]["StartDate"]));

                    txtBranch.Text = branch.Name;
                    txtBranchDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[b]["StartDate"]));
                    txtMobile.Text =Convert.ToString(branch.MobileNumber);
                    hdnBranch.Value = branch.Id.ToString();
                }
            }
        }

        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlSubzone.SelectedValue != null && this.ddlSubzone.SelectedValue != "")
            {
                this.ddlRegion.DataSource = Region.Find("SubzoneId = " + this.ddlSubzone.SelectedValue + " And Status=1", "Name");//, User.Identity.Name);
                this.ddlRegion.DataBind();
                this.ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlRegion.SelectedValue != null && this.ddlRegion.SelectedValue != "")
            {
                this.ddlBranch.DataSource = Branch.Find("RegionId = " + this.ddlRegion.SelectedValue + " AND Status=1", "Name");//, User.Identity.Name);
                this.ddlBranch.DataBind();
            }
            else
            {
                this.ddlBranch.Items.Clear();
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));

            if (h_Employee != null)
            {
                hdnId.Value = "0";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";
                txtZone.Text = "";
                txtZoneDate.Text = "";
                txtSubzone.Text = "";
                txtSubzoneDate.Text = "";
                txtRegion.Text = "";
                txtRegionDate.Text = "";
                txtBranch.Text = "";
                txtBranchDate.Text = "";
                if (h_Employee.Status != H_Employee.Statuses.Working)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    this.ShowUIMessage(msg);
                    return;
                }
                IList<H_EmployeeTransferHistory> iTranList = H_EmployeeTransferHistory.Find(" H_EmployeeId=" + h_Employee.Id+" AND Status=1", "JoiningDate DESC");
            
                if (iTranList != null && iTranList.Count > 0)
                {
                    H_EmployeeTransferHistory h_EmployeeTransfer = iTranList[0];
                    if (h_EmployeeTransfer.JoiningDate >= DateTime.Today.Date)
                    {
                        Branch branch1 = Branch.GetById(h_EmployeeTransfer.DestinationBranchId);
                        Message msg = new Message();
                        msg.Type = MessageType.Error;
                        msg.Msg = "Already Transfered to " + branch1.Name + " Joining Date: " + h_EmployeeTransfer.JoiningDate.ToString("dd/MM/yyyy");
                        this.ShowUIMessage(msg);
                        return;
                    }
                }
                this.Type = TYPE_ADD;
                hdnId.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                H_EmployeeDepartment eDepartment = H_EmployeeDepartment.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDepartment.Text = H_Department.GetById(eDepartment.H_DepartmentId).Name;

                H_EmployeeGrade eGrade = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                txtGrade.Text = H_Grade.GetById(eGrade.H_GradeId).Name;

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
                    if (z == i-1 && dt.Rows[i - 1]["ZoneId"].ToString() == dt.Rows[i]["ZoneId"].ToString())
                    {
                        z = i;
                    }

                    if (s == i-1 && dt.Rows[i - 1]["SubzoneId"].ToString() == dt.Rows[i]["SubzoneId"].ToString())
                    {
                        s = i;
                    }

                    if (r ==i- 1 && dt.Rows[i - 1]["RegionId"].ToString() == dt.Rows[i]["RegionId"].ToString())
                    {
                        r = i;
                    }

                    if (b == i-1 && dt.Rows[i - 1]["BranchId"].ToString() == dt.Rows[i]["BranchId"].ToString())
                    {
                        b = i;
                    }
                }

                Branch branch = Branch.GetById(DBUtility.ToInt32(dt.Rows[b]["BranchId"]));
                Region region = Region.GetById(branch.RegionId);

                txtZone.Text = Zone.GetById(Subzone.GetById(region.SubzoneId).ZoneId).Name;
                txtZoneDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[z]["StartDate"]));

                txtSubzone.Text = Subzone.GetById(region.SubzoneId).Name;
                txtSubzoneDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[s]["StartDate"]));

                txtRegion.Text = region.Name;
                txtRegionDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[r]["StartDate"]));

                txtBranch.Text = branch.Name;
                txtBranchDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[b]["StartDate"]));
                txtMobile.Text = Convert.ToString(branch.MobileNumber);
                hdnBranch.Value = branch.Id.ToString();
                   

            }
            else
            {
                hdnId.Value = "0";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";
                txtZone.Text = "";
                txtZoneDate.Text = "";
                txtSubzone.Text = "";
                txtSubzoneDate.Text = "";
                txtRegion.Text = "";
                txtRegionDate.Text = "";
                txtBranch.Text = "";
                txtBranchDate.Text = "";

                if (this.txtEmployee.Text.Trim() != "")
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "No employee found";
                    this.ShowUIMessage(msg);
                }
            }
        }

        protected void chkCancel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCancel.Checked == true)
                rvRemarks.Enabled = true;
            else
                rvRemarks.Enabled = false;
        }
    }
}


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     