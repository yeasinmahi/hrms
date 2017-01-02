using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeePromotionAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEPROMOTION ADD"; }
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
            return "H_EmployeePromotionList.aspx";
        }

        private H_EmployeePromotionHistory GetH_EmployeePromotion()
        {
            H_EmployeePromotionHistory h_EmployeePromotion = null;
            if (this.Type == TYPE_EDIT)
            {
                h_EmployeePromotion = H_EmployeePromotionHistory.GetById(Convert.ToInt32(hdnId.Value));
                if (chkCancel.Checked == true)
                {
                    h_EmployeePromotion.Status = 2;
                }
            }
            else
            {
                h_EmployeePromotion = new H_EmployeePromotionHistory();
                h_EmployeePromotion.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
                h_EmployeePromotion.Status = 1;
            }
        
            h_EmployeePromotion.Type = (H_EmployeePromotion.Types)DBUtility.ToInt32(ddlType.SelectedValue);
            h_EmployeePromotion.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeePromotion.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeePromotion.OldH_GradeId = DBUtility.ToInt32(hdnGrade.Value);
            h_EmployeePromotion.NewH_GradeId = DBUtility.ToInt32(ddlNewH_GradeId.SelectedValue);
            h_EmployeePromotion.OldH_DesignationId = DBUtility.ToInt32(hdnDesignation.Value);
            h_EmployeePromotion.NewH_DesignationId = DBUtility.ToInt32(ddlNewH_DesignationId.SelectedValue);
            h_EmployeePromotion.PromotionDate = DBUtility.ToDateTime(txtPromotionDate.Text);
            h_EmployeePromotion.Remarks = DBUtility.ToNullableString(txtRemarks.Text);
            h_EmployeePromotion.UserLogin = User.Identity.Name;
        
            return h_EmployeePromotion;
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
            if (h_Employee == null)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";
                return msg;
            }
            IList<H_EmployeePromotionHistory> h_EmployeePromoPendingList = H_EmployeePromotionHistory.Find("H_EmployeeID = " + h_Employee.Id + " AND Status=1", "Id DESC");

            if (h_EmployeePromoPendingList.Count > 0)
            {
                if (chkCancel.Checked == false)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Already Posted for " + h_Employee.Name + "(" + h_Employee.Code.ToString() + "). Please Check List";
                    return msg;
                }
            }
            if (h_Employee.JoiningDate >= DBUtility.ToDateTime(this.txtLetterDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Letter date should be greater than employee's joining date (" + h_Employee.JoiningDate + ")";
                return msg;
            }

            if (h_Employee.JoiningDate >= DBUtility.ToDateTime(this.txtPromotionDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Promotion date should be greater than employee's joining date (" + h_Employee.JoiningDate + ")";
                return msg;
            }

            if ((H_EmployeePromotion.Types)Enum.Parse(typeof(H_EmployeePromotion.Types), ddlType.Text) == H_EmployeePromotion.Types.Promotion)
            {
                H_Grade oldGrade=H_Grade.GetById(DBUtility.ToInt32(hdnGrade.Value));
                H_Grade newGrade=H_Grade.GetById(DBUtility.ToInt32(ddlNewH_GradeId.SelectedValue));
                if (oldGrade.SortOrder <= newGrade.SortOrder)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "New grade should be greater than old grade for promotion";
                    return msg;
                }
            
            }

            if ((H_EmployeePromotion.Types)Enum.Parse(typeof(H_EmployeePromotion.Types), ddlType.Text) == H_EmployeePromotion.Types.Demotion)
            {
                H_Grade oldGrade = H_Grade.GetById(DBUtility.ToInt32(hdnGrade.Value));
                H_Grade newGrade = H_Grade.GetById(DBUtility.ToInt32(ddlNewH_GradeId.SelectedValue));
                if (oldGrade.SortOrder >= newGrade.SortOrder)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "New grade should be lower than old grade for demotion";
                    return msg;
                }
            }
            if ((H_EmployeePromotion.Types)Enum.Parse(typeof(H_EmployeePromotion.Types), ddlType.Text) == H_EmployeePromotion.Types.Designation_Change)
            {
                if (DBUtility.ToInt32(hdnGrade.Value) != DBUtility.ToInt32(ddlNewH_GradeId.SelectedValue))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "New grade and old grade should be same for Designation Change";
                    return msg;
                }
                if (DBUtility.ToInt32(hdnDesignation.Value) == DBUtility.ToInt32(ddlNewH_DesignationId.SelectedValue))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "New and old designation should not be same for Designation Change";
                    return msg;
                }
            }
            if (h_Employee.Status != H_Employee.Statuses.Working && h_Employee.Status != H_Employee.Statuses.Consultancy)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid operation. Employee presently "+((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                return msg;
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                string desc = "";
                H_EmployeePromotionHistory h_EmployeePromotion = this.GetH_EmployeePromotion();
                if (this.Type == TYPE_EDIT)
                {
                    desc = "Update [H_EmployeePromotionHistory]";
                }
                else
                {
                    desc = "Insert [H_EmployeePromotionHistory]";
                }

                this.TransactionManager = new TransactionManager(true, desc);
                if (this.Type == TYPE_EDIT)
                {
                    H_EmployeePromotionHistory.Update(this.TransactionManager, h_EmployeePromotion);
                }
                else
                {
                    H_EmployeePromotionHistory.Insert(this.TransactionManager, h_EmployeePromotion);
                }
            
                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            chkCancel.Enabled = false;
            UIUtility.LoadEnums(ddlType, typeof(H_EmployeePromotion.Types), false, false, false);

            this.ddlNewH_GradeId.DataSource = H_Grade.FindAll();
            this.ddlNewH_GradeId.DataBind();
            this.ddlNewH_GradeId_SelectedIndexChanged(this.ddlNewH_GradeId, new EventArgs());
            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                H_EmployeePromotionHistory h_History = H_EmployeePromotionHistory.GetById(Convert.ToInt32(hdnId.Value));
                if (h_History != null)
                {
                    chkCancel.Enabled = true;
                    this.Type = TYPE_EDIT;
                    H_Employee h_Employee = H_Employee.GetById(h_History.H_EmployeeId);
                    txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                    H_EmployeeDepartment eDepartment = H_EmployeeDepartment.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                    txtDepartment.Text = H_Department.GetById(eDepartment.H_DepartmentId).Name;

                    IList<H_EmployeeGrade> eGrades = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC");
                    txtGrade.Text = H_Grade.GetById(eGrades[0].H_GradeId).Name;
                    hdnGrade.Value = eGrades[0].H_GradeId.ToString();
                    this.ddlNewH_GradeId.SelectedValue = h_History.NewH_GradeId.ToString();
                    this.ddlNewH_GradeId_SelectedIndexChanged(this.ddlNewH_GradeId, new EventArgs());

                    IList<H_EmployeeDesignation> eDesignations = H_EmployeeDesignation.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC");
                    txtDesignation.Text = H_Designation.GetById(eDesignations[0].H_DesignationId).Name;
                    hdnDesignation.Value = eDesignations[0].H_DesignationId.ToString();
                    this.ddlNewH_DesignationId.SelectedValue = h_History.NewH_DesignationId.ToString();


                    Int32 i = 0;

                    for (i = 1; i < eGrades.Count; i++)
                    {
                        if (eGrades[i - 1].H_GradeId != eGrades[i].H_GradeId)
                        {
                            break;
                        }
                    }

                    txtGradeDate.Text = UIUtility.Format(eGrades[i - 1].StartDate);

                    i = 0;

                    for (i = 1; i < eDesignations.Count; i++)
                    {
                        if (eDesignations[i - 1].H_DesignationId != eDesignations[i].H_DesignationId)
                        {
                            break;
                        }
                    }

                    txtDesignationDate.Text = UIUtility.Format(eDesignations[i - 1].StartDate);

                    H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                    Branch branch = Branch.GetById(eBranch.BranchId);
                    Region region = Region.GetById(branch.RegionId);

                    txtZone.Text = Zone.GetById(Subzone.GetById(region.SubzoneId).ZoneId).Name;
                    txtSubzone.Text = Subzone.GetById(region.SubzoneId).Name;
                    txtRegion.Text = region.Name;
                    txtBranch.Text = branch.Name;
                    ddlType.SelectedValue = ((Int32)h_History.Type).ToString();
                    txtLetterNo.Text = h_History.LetterNo.ToString();
                    txtLetterDate.Text = UIUtility.Format(h_History.LetterDate);
                    txtPromotionDate.Text = UIUtility.Format(h_History.PromotionDate);
                    txtRemarks.Text = h_History.Remarks;
                }
            }
        }

        protected void ddlNewH_GradeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlNewH_GradeId.SelectedValue != null && this.ddlNewH_GradeId.SelectedValue != "")
            {
                TransactionManager tm = new TransactionManager(false);

                ddlNewH_DesignationId.DataSource = tm.GetDataSet("SELECT H_Designation.Id, Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + this.ddlNewH_GradeId.SelectedValue + " ORDER BY Name").Tables[0];
                ddlNewH_DesignationId.DataBind();
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                hdnId.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                H_EmployeeDepartment eDepartment = H_EmployeeDepartment.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                txtDepartment.Text = H_Department.GetById(eDepartment.H_DepartmentId).Name;

                IList<H_EmployeeGrade> eGrades = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC");
                txtGrade.Text = H_Grade.GetById(eGrades[0].H_GradeId).Name;
                hdnGrade.Value = eGrades[0].H_GradeId.ToString();
                this.ddlNewH_GradeId.SelectedValue = eGrades[0].H_GradeId.ToString();
                this.ddlNewH_GradeId_SelectedIndexChanged(this.ddlNewH_GradeId, new EventArgs());

                IList<H_EmployeeDesignation> eDesignations = H_EmployeeDesignation.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC");
                txtDesignation.Text = H_Designation.GetById(eDesignations[0].H_DesignationId).Name;
                hdnDesignation.Value = eDesignations[0].H_DesignationId.ToString();
                this.ddlNewH_DesignationId.SelectedValue = eDesignations[0].H_DesignationId.ToString();
            

                Int32 i = 0;

                for (i = 1; i < eGrades.Count; i++)
                {
                    if (eGrades[i - 1].H_GradeId != eGrades[i].H_GradeId)
                    {
                        break;
                    }
                }

                txtGradeDate.Text = UIUtility.Format(eGrades[i - 1].StartDate);

                i = 0;

                for (i = 1; i < eDesignations.Count; i++)
                {
                    if (eDesignations[i - 1].H_DesignationId != eDesignations[i].H_DesignationId)
                    {
                        break;
                    }
                }

                txtDesignationDate.Text = UIUtility.Format(eDesignations[i - 1].StartDate);

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
                txtGradeDate.Text = "";
                txtDesignation.Text = "";
                txtDesignationDate.Text = "";
                txtZone.Text = "";
                txtRegion.Text = "";
                txtBranch.Text = "";

                this.ShowUIMessage(msg);
            }
        }
    }
}
