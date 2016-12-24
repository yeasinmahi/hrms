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
    public partial class H_EmployeeTransferApplicationAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEETRANSFERAPPLICATION ADD"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Menu mnuPageToolbar = (Menu)Master.FindControl("mnuPageToolbar");

            //if (mnuPageToolbar.Items.Count > 1 && mnuPageToolbar.Items[1].Value == "SAVE")
            //{
            //    mnuPageToolbar.Items.RemoveAt(1);
            //}
        }
        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            
            switch (e.Item.Value)
            {
                case "REFRESH":
                    UIUtility.Transfer(Page, Request.Path);
                    break;               
                default:
                    this.HandleSpecialCommand(sender, e);
                    break;
            }
        }

        protected override string GetListPageUrl()
        {
            return "H_EmployeeTransferApplicationList.aspx";
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
                if (h_Employee.AppointmentLetterDate >= DBUtility.ToDateTime(txtApplicationDate.Text))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Application date should be greater than appointment letter date (" + h_Employee.AppointmentLetterDate + ")";
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
                        H_Designation h_Designation = H_Designation.GetById(eDesignation.H_DesignationId);
                        if (h_Designation.GroupType != H_Designation.GroupTypes.Peon && h_Designation.GroupType != H_Designation.GroupTypes.LO && h_Designation.GroupType != H_Designation.GroupTypes.ABM && h_Designation.GroupType != H_Designation.GroupTypes.BM)
                        {
                            msg.Type = MessageType.Error;
                            msg.Msg = "You are not permitted to Transfer " + h_Designation.Name;
                            return msg;
                        }
                    }
                }
                if (this.Type == TYPE_ADD)
                {
                    IList<H_EmployeeTransferApplication> iTranList =
                        H_EmployeeTransferApplication.Find(" H_EmployeeId=" + h_Employee.Id + " AND Status=2", "");

                    if (iTranList != null && iTranList.Count > 0)
                    {

                        msg.Type = MessageType.Error;
                        msg.Msg = "Invalid operation. Employee transfer applicaiton presently " +
                                  ((H_EmployeeTransferApplication.Statuses)(iTranList[0].Status)).ToString()
                                                                                                .Replace("_", " ")
                                                                                                .ToLower();
                        this.ShowUIMessage(msg);
                        return msg;
                    }
                }
            }

            return msg;
        }

        private H_EmployeeTransferApplication GetH_EmployeeTransferApplication()
        {
            H_EmployeeTransferApplication hEmployeeTransferApplication = null;
            if (this.Type == TYPE_EDIT)
            {
                hEmployeeTransferApplication = H_EmployeeTransferApplication.GetById(Convert.ToInt32(hdnId.Value));
               
            }
            else
            {

                hEmployeeTransferApplication = new H_EmployeeTransferApplication();
                hEmployeeTransferApplication.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
                hEmployeeTransferApplication.ApplicationNo = txtApplicationNo.Text;
                hEmployeeTransferApplication.UserLogin = User.Identity.Name;
            }

            hEmployeeTransferApplication.DemandedPlace = txtDemandedPlace.Text;          
            hEmployeeTransferApplication.ApplicationDate = DBUtility.ToDateTime(txtApplicationDate.Text);
            hEmployeeTransferApplication.Status = (H_EmployeeTransferApplication.Statuses)DBUtility.ToInt32(ddlStatus.SelectedValue);
            hEmployeeTransferApplication.ReceivingDate = DBUtility.ToDateTime(txtReceivingDate.Text);
            hEmployeeTransferApplication.Remarks = DBUtility.ToNullableString(txtRemarks.Text);

            H_Employee h_Employee = H_Employee.GetById(hEmployeeTransferApplication.H_EmployeeId);
            H_EmployeeDesignation empDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
            H_Designation eDesignation = H_Designation.GetById(empDesignation.H_DesignationId);
            hEmployeeTransferApplication.H_DesignationId = eDesignation.Id;

            return hEmployeeTransferApplication;
        }
        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                string desc = null;
                H_EmployeeTransferApplication hEmployeeTransferApplication = this.GetH_EmployeeTransferApplication();
                if (this.Type == TYPE_EDIT)
                {
                    desc = "Update [H_EmployeeTransferApplication]";
                }
                else
                {
                    desc = "Insert [H_EmployeeTransferApplication]";
                    
                }

                this.TransactionManager = new TransactionManager(true, desc);
                if (this.Type == TYPE_EDIT)
                {
                    H_EmployeeTransferApplication.Update(this.TransactionManager, hEmployeeTransferApplication);
                }
                else
                {
                    string groupType = H_Designation.GetById(hEmployeeTransferApplication.H_DesignationId).GroupType.ToString();
                    string query = "Select ISNULL(Max(convert(int,SUBSTRING(ApplicationNo," + (groupType.Length + 2) + ",len(ApplicationNo)-" + (groupType.Length + 1) + "))),0)+1 FROM H_EmployeeTransferApplication WHERE LEFT(ApplicationNo," + groupType.Length + ")='" + groupType + "'";
                    DataTable dt = TransactionManager.GetDataSet(query).Tables[0] ;
                    hEmployeeTransferApplication.ApplicationNo = groupType +"-" + dt.Rows[0][0].ToString();
                    H_EmployeeTransferApplication.Insert(this.TransactionManager, hEmployeeTransferApplication);
                    txtApplicationNo.Text = hEmployeeTransferApplication.ApplicationNo;
                }

                hdnId.Value = hEmployeeTransferApplication.Id.ToString();
                this.Type = TYPE_EDIT;

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlStatus, typeof(H_EmployeeTransferApplication.Statuses), false, false, false);

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                H_EmployeeTransferApplication hEmployeeTransferApplication = H_EmployeeTransferApplication.GetById(Convert.ToInt32(hdnId.Value));
                if (hEmployeeTransferApplication != null)
                {
                    H_Employee h_Employee = H_Employee.GetById(hEmployeeTransferApplication.H_EmployeeId);
                    //hdnId.Value = "0";
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
                    txtHomeDistrict.Text = "";
                    txtHomeThana.Text = "";
                    txtMobile.Text = "";
                    txtduration.Text = "";
                    txtApplicationDate.Text = "";
                    txtReceivingDate.Text = "";
                    txtDemandedPlace.Text = "";
                    txtRemarks.Text = "";
                    txtApplicationNo.Text = "";
                    ddlStatus.SelectedValue = "1";
                    if (h_Employee.Status != H_Employee.Statuses.Working)
                    {
                        Message msg = new Message();
                        msg.Type = MessageType.Error;
                        msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                        this.ShowUIMessage(msg);
                        return;
                    }
                  
                    this.Type = TYPE_EDIT;
                    //hdnId.Value = h_Employee.Id.ToString();
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
                    txtMobile.Text = Convert.ToString(branch.MobileNumber);
                    hdnBranch.Value = branch.Id.ToString();

                    txtApplicationNo.Text = hEmployeeTransferApplication.ApplicationNo.ToString();
                    txtApplicationDate.Text = UIUtility.Format(hEmployeeTransferApplication.ApplicationDate);
                    txtReceivingDate.Text = UIUtility.Format(hEmployeeTransferApplication.ReceivingDate);
                    txtDemandedPlace.Text = hEmployeeTransferApplication.DemandedPlace;
                    txtRemarks.Text = hEmployeeTransferApplication.Remarks;
                    var statusId=(Int32)Enum.Parse(typeof(H_EmployeeTransferApplication.Statuses), hEmployeeTransferApplication.Status.ToString());
                    ddlStatus.SelectedValue = statusId.ToString();


                    H_Address address = H_Address.GetById(h_Employee.PermanentAddressId);
                    Thana athana = Thana.GetById(address.ThanaId);
                    District aDistrict = District.GetById(athana.DistrictId);
                    txtHomeDistrict.Text = aDistrict.Name;
                    txtHomeThana.Text = athana.Name;
                    txtduration.Text = UIUtility.GetDifference(DBUtility.ToDateTime(dt.Rows[s]["StartDate"]), DateTime.Now.Date);
                }

            }

        }
      
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));

            if (h_Employee != null)
            {
                //hdnId.Value = "0";
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
                txtHomeDistrict.Text = "";
                txtHomeThana.Text = "";
                txtMobile.Text = "";
                txtduration.Text="";
                txtApplicationDate.Text = "";
                txtReceivingDate.Text = "";
                txtDemandedPlace.Text = "";
                txtRemarks.Text = "";
                //txtApplicationNo.Text = "";
                //ddlStatus.SelectedValue = "1";
                if (h_Employee.Status != H_Employee.Statuses.Working)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    this.ShowUIMessage(msg);
                    return;
                }
                IList<H_EmployeeTransferApplication> iTranList = H_EmployeeTransferApplication.Find(" H_EmployeeId=" + h_Employee.Id + " AND Status=2", "");

                if (iTranList != null && iTranList.Count > 0)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee transfer applicaiton presently " + ((H_EmployeeTransferApplication.Statuses)(iTranList[0].Status)).ToString().Replace("_", " ").ToLower();
                    this.ShowUIMessage(msg);
                    return;
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
                txtMobile.Text = Convert.ToString(branch.MobileNumber);
                hdnBranch.Value = branch.Id.ToString();

                //H_Designation rDesignation = H_Designation.GetById(eDesignation.H_DesignationId);
                //Int32 EmpCode = Convert.ToInt32(tm.GetDataSet("SELECT isnull(MAX(Application_No),0) FROM H_EmployeeTransferApplication where sourcedesignationId=" + rDesignation.Id).Tables[0].Rows[0][0]);
                //EmpCode++;
                

                H_Address address = H_Address.GetById(h_Employee.PermanentAddressId);
                Thana athana = Thana.GetById(address.ThanaId);
                District aDistrict = District.GetById(athana.DistrictId);
                txtHomeDistrict.Text = aDistrict.Name;
                txtHomeThana.Text = athana.Name;
                txtduration.Text = UIUtility.GetDifference(DBUtility.ToDateTime(dt.Rows[s]["StartDate"]),DateTime.Now.Date);
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
                txtHomeDistrict.Text = "";
                txtHomeThana.Text = "";
                txtMobile.Text = "";
                txtduration.Text = "";
                txtApplicationDate.Text = "";
                txtReceivingDate.Text = "";
                txtDemandedPlace.Text = "";
                txtRemarks.Text = "";
                txtApplicationNo.Text = "";
                ddlStatus.SelectedValue = "1";
                if (this.txtEmployee.Text.Trim() != "")
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "No employee found";
                    this.ShowUIMessage(msg);
                }
            }
        }

    }
}
