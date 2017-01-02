using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeRejoinAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEREJOIN ADD"; }
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
            return "H_EmployeeRejoinList.aspx";
        }

        private H_EmployeeRejoinHistory GetH_EmployeeRejoin()
        {
            H_EmployeeRejoinHistory h_EmployeeRejoin = null;
            if (this.Type == TYPE_EDIT)
            {
                h_EmployeeRejoin = H_EmployeeRejoinHistory.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_EmployeeRejoin = new H_EmployeeRejoinHistory();
                h_EmployeeRejoin.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
                h_EmployeeRejoin.LeaveType = (H_EmployeeLeave.Types)DBUtility.ToInt32(ddlLeaveType.SelectedValue);
            }

            
           
            h_EmployeeRejoin.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeeRejoin.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeeRejoin.FromDate = DBUtility.ToDateTime(txtFromDate.Text);
            h_EmployeeRejoin.RejoinDate = DBUtility.ToDateTime(txtRejoinDate.Text);
            h_EmployeeRejoin.SourceBranchId = DBUtility.ToInt32(this.hdnBranch.Value);
            h_EmployeeRejoin.DestinationBranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
            h_EmployeeRejoin.Status = true;
            
            return h_EmployeeRejoin;
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

            if (this.txtFromDate.Text == "")
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Employee is not in leave";
                return msg;
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
                    string desc = "Insert [H_EmployeeRejoinHistory]";
                    this.TransactionManager = new TransactionManager(true, desc);
                    //if (h_Employee.Status == H_Employee.Statuses.Consultancy)
                    //{
                    //    IList<H_EmployeeConsultency> list = Asa.Hrms.Data.Entity.H_EmployeeConsultency.Find("H_EmployeeId=" + h_Employee.Id + " AND Status=1", "");
                    //    if (list.Count > 0)
                    //    {
                    //        foreach (H_EmployeeConsultency h_Consultency in list)
                    //        {
                    //            h_Consultency.EndDate = DBUtility.ToDateTime(txtRejoinDate.Text).AddDays(-1);
                    //            h_Consultency.Status = H_EmployeeConsultency.Statuses.INACTIVE;
                    //            H_EmployeeConsultency.Update(this.TransactionManager, h_Consultency);
                    //        }
                    //    }
                    //}
                    //if (h_Employee.Status == H_Employee.Statuses.In_Leave)
                    //{
                    //    IList<H_EmployeeLeave> list = Asa.Hrms.Data.Entity.H_EmployeeLeave.Find("H_EmployeeId=" + h_Employee.Id + " AND Status=1", "");
                    //    if (list.Count > 0)
                    //    {
                    //        foreach (H_EmployeeLeave leave in list)
                    //        {
                    //            leave.EndDate = DBUtility.ToDateTime(txtRejoinDate.Text).AddDays(-1);
                    //            leave.Status = 2;
                    //            H_EmployeeLeave.Update(this.TransactionManager, leave);
                    //        }
                    //    }
                    //}
                    H_EmployeeRejoinHistory h_EmployeeRejoin = this.GetH_EmployeeRejoin();

                   // h_Employee.Status = (H_Employee.Statuses)((Int32)H_Employee.Statuses.Working);
                   // h_Employee.EmploymentType = (H_Employee.EmploymentTypes)DBUtility.ToInt32(ddlRejoinType.SelectedValue);

                    
                    h_EmployeeRejoin.RejoinType = h_Employee.EmploymentType;
                    if (this.Type == TYPE_EDIT)
                    {
                        H_EmployeeRejoinHistory.Update(this.TransactionManager, h_EmployeeRejoin);
                    }
                    else
                    {
                        H_EmployeeRejoinHistory.Insert(this.TransactionManager, h_EmployeeRejoin);
                    }
                    //Asa.Hrms.Data.Entity.H_EmployeeRejoinHistory.Insert(this.TransactionManager, h_EmployeeRejoin);
                    //Asa.Hrms.Data.Entity.H_Employee.Update(this.TransactionManager, h_Employee);

                    hdnId.Value = h_EmployeeRejoin.Id.ToString();
                    this.Type = TYPE_EDIT;

                   // H_EmployeeBranch eBranch = H_EmployeeBranch.Find(this.TransactionManager, "H_EmployeeId=" + h_EmployeeRejoin.H_EmployeeId, "EndDate DESC")[0];

                    //if (eBranch.BranchId != DBUtility.ToInt32(this.ddlBranch.SelectedValue))
                    //{
                    //    eBranch.EndDate = h_EmployeeRejoin.FromDate.AddDays(-1);
                    //    H_EmployeeBranch.Update(this.TransactionManager, eBranch);

                    //    eBranch = new H_EmployeeBranch();
                    //    eBranch.H_EmployeeId = h_EmployeeRejoin.H_EmployeeId;
                    //    eBranch.BranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
                    //    eBranch.StartDate = h_EmployeeRejoin.RejoinDate;
                    //    eBranch.EndDate = new DateTime(2099, 12, 31);
                    //    H_EmployeeBranch.Insert(this.TransactionManager, eBranch);
                    //    //rejoin with tranfer
                    //    H_EmployeeTransfer h_EmployeeTransfer = new H_EmployeeTransfer();

                    //    h_EmployeeTransfer.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
                    //    h_EmployeeTransfer.Type = H_EmployeeTransfer.Types.Normal;
                    //    h_EmployeeTransfer.LetterNo = DBUtility.ToString(txtLetterNo.Text);
                    //    h_EmployeeTransfer.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
                    //    h_EmployeeTransfer.SourceBranchId = DBUtility.ToInt32(this.hdnBranch.Value);
                    //    h_EmployeeTransfer.DestinationBranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
                    //    h_EmployeeTransfer.JoiningDate = h_EmployeeRejoin.RejoinDate;
                    //    h_EmployeeTransfer.Remarks = "Rejoin with Transfer";
                    //    Asa.Hrms.Data.Entity.H_EmployeeTransfer.Insert(this.TransactionManager, h_EmployeeTransfer);
                    //}

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
            txtLeaveType.Visible = false;
            //this.ddlZone.DataSource = Zone.FindByLogin("", "Name", User.Identity.Name);
            //this.ddlZone.DataBind();
            //this.ddlZone_SelectedIndexChanged(this.ddlZone, new EventArgs());

            this.ddlSubzone.DataSource = Subzone.FindByLogin("Status=1", "Name", User.Identity.Name);
            this.ddlSubzone.DataBind();
            this.ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());

            UIUtility.LoadEnums(ddlLeaveType, typeof(H_EmployeeLeave.Types), false, false, true);
            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];

                H_EmployeeRejoinHistory h_History = H_EmployeeRejoinHistory.GetById(Convert.ToInt32(hdnId.Value));

                if (h_History != null)
                {
                    this.Type = TYPE_EDIT;
                    txtLetterNo.Text = h_History.LetterNo;
                    txtLetterDate.Text = UIUtility.Format(h_History.LetterDate);
                    txtRejoinDate.Text = UIUtility.Format(h_History.RejoinDate);
                    txtFromDate.Text = UIUtility.Format(h_History.FromDate);
                    hdnBranch.Value = h_History.SourceBranchId.ToString();
                    Branch branch = Branch.GetById(h_History.DestinationBranchId);
                    Region region = Region.GetById(branch.RegionId);
                    ddlSubzone.SelectedValue = region.SubzoneId.ToString();
                    this.ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
                    ddlRegion.SelectedValue = region.Id.ToString();
                    ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
                    ddlBranch.SelectedValue = branch.Id.ToString();
                    H_Employee h_Employee = H_Employee.GetById(h_History.H_EmployeeId);
                    
                    if (h_Employee != null)
                    {
                        txtEmployee.Text = h_Employee.Code.ToString() + ":" + h_Employee.Name;
                        if (h_Employee.Status == H_Employee.Statuses.In_Leave)
                        {
                            IList<H_EmployeeLeave> eLeave = null;
                            eLeave = H_EmployeeLeave.Find("H_EmployeeId = " + h_Employee.Id + " AND JoinType = " + (int)H_EmployeeLeave.JoinTypes.Rejoin, "StartDate DESC");
                            H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                            if (eLeave != null && eLeave.Count != 0)
                            {
                                Branch sBranch = Branch.GetById(h_History.SourceBranchId);
                                Region sRegion = Region.GetById(sBranch.RegionId);
                                Subzone sSubzone = Subzone.GetById(sRegion.SubzoneId);
                               
                                txtFromDate.Text = UIUtility.Format(eLeave[0].StartDate);
                                txtEndDate.Text = UIUtility.Format(eLeave[0].EndDate);
                                ddlLeaveType.Visible = true;
                                ddlLeaveType.SelectedValue = ((Int32)eLeave[0].Type).ToString();
                                txtLeaveType.Visible = false;
                                
                                txtSourceSubzone.Text = sSubzone.Name;// Subzone.GetById(region.SubzoneId).Name;
                                txtSourceRegion.Text = sRegion.Name;
                                txtSourceBranch.Text = sBranch.Name;
                                txtSourceBranchDate.Text = UIUtility.Format(eBranch.StartDate);
                                hdnBranch.Value = eBranch.BranchId.ToString();
                            }
                            
                        }
                        if (h_Employee.Status == H_Employee.Statuses.Consultancy)
                        {
                            IList<H_EmployeeConsultency> eCon = null;
                            eCon = H_EmployeeConsultency.Find("H_EmployeeId = " + h_Employee.Id, "StartDate DESC");
                            if (eCon != null && eCon.Count > 0)
                            {
                                H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                                Branch sBranch = Branch.GetById(eBranch.BranchId);
                                Region sRegion = Region.GetById(sBranch.RegionId);
                                Subzone sSubzone = Subzone.GetById(sRegion.SubzoneId);

                                txtFromDate.Text = UIUtility.Format(eCon[0].StartDate);
                                txtEndDate.Text = UIUtility.Format(eCon[0].EndDate);
                                ddlLeaveType.Visible = false;
                                txtLeaveType.Visible = true;
                                txtLeaveType.Text = "Consultancy";
                                txtSourceSubzone.Text = sSubzone.Name;// Subzone.GetById(region.SubzoneId).Name;
                                txtSourceRegion.Text = sRegion.Name;
                                txtSourceBranch.Text = sBranch.Name;
                                txtSourceBranchDate.Text = UIUtility.Format(eBranch.StartDate);
                                hdnBranch.Value = eBranch.BranchId.ToString();

                            }
                            
                        }
                    }
                    
                }
            }


        }

        //protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.ddlZone.SelectedValue != null && this.ddlZone.SelectedValue != "")
        //    {
        //        this.ddlSubzone.DataSource = Subzone.FindByLogin("ZoneId = " + this.ddlZone.SelectedValue + " AND Status=1", "Name", User.Identity.Name);
        //        this.ddlSubzone.DataBind();
        //        this.ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
        //    }
        //}

        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlSubzone.SelectedValue != null && this.ddlSubzone.SelectedValue != "")
            {
                this.ddlRegion.DataSource = Region.FindByLogin("SubzoneId = " + this.ddlSubzone.SelectedValue, "Name", User.Identity.Name);
                this.ddlRegion.DataBind();
                this.ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlRegion.SelectedValue != null && this.ddlRegion.SelectedValue != "")
            {
                this.ddlBranch.DataSource = Branch.FindByLogin("RegionId = " + this.ddlRegion.SelectedValue + " AND Status=1", "Name", User.Identity.Name);
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
            //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));

            if (h_Employee != null)
            {
                if (h_Employee.Status != H_Employee.Statuses.In_Leave && h_Employee.Status != H_Employee.Statuses.Consultancy)
                {
                    txtFromDate.Text = "";
                    txtEndDate.Text = "";

                    ddlLeaveType.SelectedIndex = -1;
                    txtSourceSubzone.Text = "";
                    txtSourceRegion.Text = "";
                    txtSourceBranch.Text = "";
                    txtSourceBranchDate.Text = "";
                    hdnBranch.Value = "";
                    Message msg = new Message();
                    msg.Type = MessageType.Warning;
                    msg.Msg = "Employee is not in leave/Consultancy";
                    this.ShowUIMessage(msg);
                    return;
                }
                hdnId.Value = h_Employee.Id.ToString();
                if (h_Employee.Status == H_Employee.Statuses.In_Leave )
                {
                    IList<H_EmployeeLeave> eLeave = null;
                    eLeave = H_EmployeeLeave.Find("H_EmployeeId = " + h_Employee.Id + " AND JoinType = " + (int)H_EmployeeLeave.JoinTypes.Rejoin, "StartDate DESC");

                    if (eLeave != null && eLeave.Count != 0)
                    {
                        H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                        Branch branch = Branch.GetById(eBranch.BranchId);
                        Region region = Region.GetById(branch.RegionId);
                        Subzone subzone = Subzone.GetById(region.SubzoneId);
                        Zone zone = Zone.GetById(subzone.ZoneId);
                        txtFromDate.Text = UIUtility.Format(eLeave[0].StartDate);
                        txtEndDate.Text = UIUtility.Format(eLeave[0].EndDate);
                        ddlLeaveType.Visible = true;
                        ddlLeaveType.SelectedValue = ((Int32)eLeave[0].Type).ToString();
                        txtLeaveType.Visible = false;
                        //txtSourceZone.Text = zone.Name;// Zone.GetById(Subzone.GetById(region.SubzoneId).ZoneId).Name;
                        txtSourceSubzone.Text = subzone.Name;// Subzone.GetById(region.SubzoneId).Name;
                        txtSourceRegion.Text = region.Name;
                        txtSourceBranch.Text = branch.Name;
                        txtSourceBranchDate.Text = UIUtility.Format(eBranch.StartDate);
                        hdnBranch.Value = eBranch.BranchId.ToString();

                        //ddlZone.SelectedValue = zone.Id.ToString();
                        //ddlZone_SelectedIndexChanged(null, null);
                        ddlSubzone.SelectedValue = subzone.Id.ToString();
                        ddlSubzone_SelectedIndexChanged(null, null);
                        ddlRegion.SelectedValue = region.Id.ToString();
                        ddlRegion_SelectedIndexChanged(null, null);
                        ddlBranch.SelectedValue = branch.Id.ToString();
                    }
                    else
                    {
                        Message msg = new Message();
                        msg.Type = MessageType.Error;
                        msg.Msg = "Employee is not in leave";

                        txtFromDate.Text = "";
                        txtEndDate.Text = "";

                        ddlLeaveType.SelectedIndex = -1;
                        //txtSourceZone.Text = "";
                        txtSourceSubzone.Text = "";
                        txtSourceRegion.Text = "";
                        txtSourceBranch.Text = "";
                        txtSourceBranchDate.Text = "";
                        hdnBranch.Value = "";

                        this.ShowUIMessage(msg);
                    }
                }
                if (h_Employee.Status == H_Employee.Statuses.Consultancy)
                {
                    IList<H_EmployeeConsultency> eCon = null;
                    eCon = H_EmployeeConsultency.Find("H_EmployeeId = " + h_Employee.Id , "StartDate DESC");
                    if (eCon != null && eCon.Count > 0)
                    {
                        H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                        Branch branch = Branch.GetById(eBranch.BranchId);
                        Region region = Region.GetById(branch.RegionId);
                        Subzone subzone = Subzone.GetById(region.SubzoneId);
                        Zone zone = Zone.GetById(subzone.ZoneId);
                        txtFromDate.Text = UIUtility.Format(eCon[0].StartDate);
                        txtEndDate.Text = UIUtility.Format(eCon[0].EndDate);
                        ddlLeaveType.Visible = false;
                        txtLeaveType.Visible = true;
                        txtLeaveType.Text = "Consultancy";
                        //txtSourceZone.Text = zone.Name;// Zone.GetById(Subzone.GetById(region.SubzoneId).ZoneId).Name;
                        txtSourceSubzone.Text = subzone.Name;// Subzone.GetById(region.SubzoneId).Name;
                        txtSourceRegion.Text = region.Name;
                        txtSourceBranch.Text = branch.Name;
                        txtSourceBranchDate.Text = UIUtility.Format(eBranch.StartDate);
                        hdnBranch.Value = eBranch.BranchId.ToString();

                        //ddlZone.SelectedValue = zone.Id.ToString();
                        //ddlZone_SelectedIndexChanged(null, null);
                        ddlSubzone.SelectedValue = subzone.Id.ToString();
                        ddlSubzone_SelectedIndexChanged(null, null);
                        ddlRegion.SelectedValue = region.Id.ToString();
                        ddlRegion_SelectedIndexChanged(null, null);
                        ddlBranch.SelectedValue = branch.Id.ToString();
                    }
                    else
                    {
                        Message msg = new Message();
                        msg.Type = MessageType.Error;
                        msg.Msg = "Employee is not in Consultency";

                        txtFromDate.Text = "";
                        txtEndDate.Text = "";

                        ddlLeaveType.SelectedIndex = -1;
                        //txtSourceZone.Text = "";
                        txtSourceSubzone.Text = "";
                        txtSourceRegion.Text = "";
                        txtSourceBranch.Text = "";
                        txtSourceBranchDate.Text = "";
                        hdnBranch.Value = "";

                        this.ShowUIMessage(msg);
                    }
                }
            }
            else
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";

                hdnId.Value = "0";
                txtFromDate.Text = "";
                txtEndDate.Text = "";
                ddlLeaveType.SelectedIndex = -1;
               // txtSourceZone.Text = "";
                txtSourceSubzone.Text = "";
                txtSourceRegion.Text = "";
                txtSourceBranch.Text = "";
                txtSourceBranchDate.Text = "";
                hdnBranch.Value = "";

                this.ShowUIMessage(msg);
            }
        }
    }
}
