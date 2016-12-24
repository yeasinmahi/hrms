using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Asa.Hrms.Web;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using Asa.Hrms.Data;

namespace Asa.Hrms.WebSite.HRM
{
    public partial class H_EmployeeMultiLetterAdd : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
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

                if (h_Employee.Status != H_Employee.Statuses.Working)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    this.ShowUIMessage(msg);
                    return;
                }
                IList<H_EmployeeTransferHistory> iTranList = H_EmployeeTransferHistory.Find(" H_EmployeeId=" + h_Employee.Id + " AND Status=1", "JoiningDate DESC");

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
                foreach (ListItem li in chkLetterTypes.Items)
                {
                    li.Selected = false;
                }
                chkLetterTypes_SelectedIndexChanged(chkLetterTypes, new EventArgs());
            }
            else
            {
                hdnId.Value = "0";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";

                if (this.txtEmployee.Text.Trim() != "")
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "No employee found";
                    this.ShowUIMessage(msg);
                }
            }

        }



        protected override void LoadData()
        {
            rfvddlDistrict.Enabled = false;
            rfvddlBranch.Enabled = false;
            rfvddlGrade.Enabled = false;
            rfvddlDesignation.Enabled = false;
            rfvtxtIncrementStop.Enabled = false;
            cvtxtIncrementStop.Enabled = false;
            rfvtxtAmount.Enabled = false;
            cvtxtAmount.Enabled = false;
            rfvddlRejoinDistrict.Enabled = false;
            rfvddlRejoinBranch.Enabled = false;

            Asa.Hrms.Data.Entity.H_EmployeeMultiLetter letter = null;
            Asa.Hrms.Data.Entity.H_Employee h_Employee = null;
            pnlIncrementHeldup.Visible = false;
            pnlTransfer.Visible = false;
            pnlpromotion.Visible = false;
            pnlWarning.Visible = false;
            pnlRejoin.Visible = false;
            pnlPenalty.Visible = false;

            UIUtility.LoadEnums(ddlTransferType, typeof(H_EmployeeTransferHistory.Types), false, false, false);
            ddlDistrict.DataSource = Subzone.Find("Status=1", "Name");
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("Select District", "0"));
            ddlBranch.Items.Insert(0, new ListItem("Select Branch", "0"));

            UIUtility.LoadEnums(ddlPromoType, typeof(H_EmployeePromotion.Types), false, false, false);
            ddlGrade.DataSource = H_Grade.FindAll();
            ddlGrade.DataBind();
            ddlGrade.Items.Insert(0, new ListItem("Select Grade", "0"));
            ddlDesignation.Items.Insert(0, new ListItem("Select Designatiom", "0"));

            ddlRejoinDistrict.DataSource = Subzone.Find("Status=1", "Name");
            ddlRejoinDistrict.DataBind();
            ddlRejoinDistrict.Items.Insert(0, new ListItem("Select District", "0"));
            ddlRejoinBranch.Items.Insert(0, new ListItem("Select Branch", "0"));

            if (Request.QueryString["Id"] != null)
            {
                hdnLetterId.Value = Request.QueryString["Id"];

                letter = Asa.Hrms.Data.Entity.H_EmployeeMultiLetter.GetById(Convert.ToInt32(Request.QueryString["Id"]));
                h_Employee = H_Employee.GetById(letter.H_EmployeeId);

                if (h_Employee != null)
                {
                    hdnId.Value = h_Employee.Id.ToString();
                    this.Type = TYPE_EDIT;
                    txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                    H_EmployeeDepartment eDepartment = H_EmployeeDepartment.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                    txtDepartment.Text = H_Department.GetById(eDepartment.H_DepartmentId).Name;

                    H_EmployeeGrade eGrade = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                    txtGrade.Text = H_Grade.GetById(eGrade.H_GradeId).Name;

                    H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                    txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;

                    txtLetterNo.Text = letter.LetterNo;
                    txtLetterDate.Text = UIUtility.Format(letter.LetterDate);
                    txtEffectiveDate.Text = UIUtility.Format(letter.EffectiveDate);
                    txtSubject.Text = letter.Subject;
                    txtBody.Text = letter.Body;
                    //txtConclution.Text = letter.Conclution;
                    txtSignatory.Text = letter.Signatory;
                    txtDesg.Text = letter.Designation;
                    txtDuplication.Text = letter.Duplication;
                    txtNote.Text = letter.Note;

                    if (letter.H_EmployeeTransferHistoryId != null)
                        chkLetterTypes.Items.Cast<ListItem>().Where(c => c.Value == "1").FirstOrDefault().Selected = true;
                    if (letter.H_EmployeePromotionHistoryId != null)
                        chkLetterTypes.Items.Cast<ListItem>().Where(c => c.Value == "2").FirstOrDefault().Selected = true;
                    if (letter.H_EmployeePenatyId != null)
                        chkLetterTypes.Items.Cast<ListItem>().Where(c => c.Value == "3").FirstOrDefault().Selected = true;
                    if (letter.H_EmployeeWarningId != null)
                        chkLetterTypes.Items.Cast<ListItem>().Where(c => c.Value == "4").FirstOrDefault().Selected = true;
                    if (letter.H_EmployeeIncrementHeldupId != null)
                        chkLetterTypes.Items.Cast<ListItem>().Where(c => c.Value == "5").FirstOrDefault().Selected = true;
                    if (letter.H_EmployeeRejoinId != null)
                        chkLetterTypes.Items.Cast<ListItem>().Where(c => c.Value == "6").FirstOrDefault().Selected = true;
                    chkLetterTypes_SelectedIndexChanged(chkLetterTypes, new EventArgs());
                    if (letter.H_EmployeeTransferHistoryId != null)
                    {
                        H_EmployeeTransferHistory transfer = H_EmployeeTransferHistory.GetById(letter.H_EmployeeTransferHistoryId.Value);
                        ddlTransferType.SelectedValue = ((int)transfer.Type).ToString();
                        Branch branch = Branch.GetById(transfer.DestinationBranchId);
                        ddlDistrict.SelectedValue = Region.GetById(branch.RegionId).SubzoneId.ToString();
                        ddlDistrict_SelectedIndexChanged(ddlDistrict, new EventArgs());
                        ddlBranch.SelectedValue = branch.Id.ToString();
                    }
                    if (letter.H_EmployeePromotionHistoryId != null)
                    {
                        H_EmployeePromotionHistory promotion = H_EmployeePromotionHistory.GetById(letter.H_EmployeePromotionHistoryId.Value);
                        ddlPromoType.SelectedValue = ((int)promotion.Type).ToString();
                        ddlGrade.SelectedValue = promotion.NewH_GradeId.ToString();
                        ddlGrade_SelectedIndexChanged(ddlGrade, new EventArgs());
                        ddlDesignation.SelectedValue = promotion.NewH_DesignationId.ToString();
                    }
                    if (letter.H_EmployeePenatyId != null)
                    {
                        H_EmployeePenalty promotion = H_EmployeePenalty.GetById(letter.H_EmployeePenatyId.Value);
                        ddlPenaltyType.SelectedValue = promotion.FineType;
                        txtAmount.Text =UIUtility.Format( promotion.FineAmount);
                    }
                    if (letter.H_EmployeeWarningId != null)
                    {
                        H_EmployeeWarning promotion = H_EmployeeWarning.GetById(letter.H_EmployeeWarningId.Value);
                        ddlWarningType.SelectedValue = promotion.WarningType;
                    }
                    if (letter.H_EmployeeIncrementHeldupId != null)
                    {
                        H_EmployeeIncrementHeldup inc = H_EmployeeIncrementHeldup.GetById(letter.H_EmployeeIncrementHeldupId.Value);
                        txtIncrementStop.Text = inc.IncrementStop.ToString();
                    }
                    if (letter.H_EmployeeRejoinId != null)
                    {
                        H_EmployeeRejoin inc = H_EmployeeRejoin.GetById(letter.H_EmployeeRejoinId.Value);
                        ddlRejoinDistrict.SelectedValue = Region.GetById(inc.DestinationBranchId).SubzoneId.ToString();
                        ddlRejoinDistrict_SelectedIndexChanged(ddlRejoinDistrict, new EventArgs());
                        ddlRejoinBranch.SelectedValue = inc.DestinationBranchId.ToString();
                    }
                }
            }
        }

        protected override string GetListPageUrl()
        {
            return "H_EmployeeMultiLetterList.aspx";
        }

        private H_EmployeeTransferHistory GetH_EmployeeTransfer()
        {
            H_EmployeeBranch eb = H_EmployeeBranch.Find("H_EmployeeId=" + hdnId.Value, "EndDate DESC")[0];
            H_EmployeeTransferHistory entity = new H_EmployeeTransferHistory();
            entity.H_EmployeeId = Convert.ToInt32(hdnId.Value);
            entity.Type = (H_EmployeeTransferHistory.Types)Convert.ToInt32(ddlTransferType.SelectedValue);
            entity.LetterNo = txtLetterNo.Text;
            entity.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            entity.JoiningDate = DBUtility.ToDateTime(txtEffectiveDate.Text);
            entity.Status = H_EmployeeTransferHistory.Statuses.ACTIVE;
            entity.H_LetterFormatsId = null;
            entity.SourceBranchId = eb.BranchId;
            entity.DestinationBranchId = Convert.ToInt32(ddlBranch.SelectedValue);
            entity.EntryDateTime = DateTime.Now;
            entity.UserLogin = User.Identity.Name;


            return entity;
        }
        private H_EmployeePromotionHistory GetH_EmployeePromotion()
        {
            H_EmployeeGrade eGrade = H_EmployeeGrade.Find("H_EmployeeId=" + hdnId.Value, "EndDate DESC")[0];
            H_EmployeeDesignation eDesignation = H_EmployeeDesignation.Find("H_EmployeeId=" + hdnId.Value, "EndDate DESC")[0];
            H_EmployeePromotionHistory entity = new H_EmployeePromotionHistory();
            entity.H_EmployeeId = Convert.ToInt32(hdnId.Value);
            entity.Type = (H_EmployeePromotion.Types)Convert.ToInt32(ddlPromoType.SelectedValue);
            entity.LetterNo = txtLetterNo.Text;
            entity.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            entity.PromotionDate = DBUtility.ToDateTime(txtEffectiveDate.Text);
            entity.Status = 1;
            entity.OldH_GradeId = eGrade.H_GradeId;
            entity.OldH_DesignationId = eDesignation.H_DesignationId;
            entity.NewH_GradeId = Convert.ToInt32(ddlGrade.SelectedValue);
            entity.NewH_DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
            entity.UserLogin = User.Identity.Name;

            return entity;
        }
        private H_EmployeeWarning GetH_EmployeeWarning()
        {
            H_EmployeeBranch eb = H_EmployeeBranch.Find("H_EmployeeId=" + hdnId.Value, "EndDate DESC")[0];
            H_EmployeeWarning entity = new H_EmployeeWarning();
            entity.H_EmployeeId = Convert.ToInt32(hdnId.Value);
            entity.BranchId = eb.BranchId;
            entity.LetterNo = txtLetterNo.Text;
            entity.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            entity.Duration = DBUtility.ToDateTime(txtLetterDate.Text).Year.ToString();
            entity.TotalWarningTime = 1;
            entity.WarningType = ddlWarningType.SelectedValue;
            entity.UserLogin = User.Identity.Name;

            return entity;
        }
        private H_EmployeePenalty GetH_EmployeePenalty()
        {
            H_EmployeeBranch eb = H_EmployeeBranch.Find("H_EmployeeId=" + hdnId.Value, "EndDate DESC")[0];
            H_EmployeePenalty entity = new H_EmployeePenalty();
            entity.H_EmployeeId = Convert.ToInt32(hdnId.Value);
            entity.BranchId = eb.BranchId;
            entity.LetterNo = txtLetterNo.Text;
            entity.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            entity.Duration = DBUtility.ToDateTime(txtLetterDate.Text).Year;
            entity.FineTime = 1;
            entity.FineType = ddlPenaltyType.SelectedValue;
            entity.FineAmount = DBUtility.ToDouble(txtAmount.Text);
            entity.UserLogin = User.Identity.Name;

            return entity;
        }
        private H_EmployeeIncrementHeldup GetH_EmployeeIncrementHeldup()
        {
            H_EmployeeBranch eb = H_EmployeeBranch.Find("H_EmployeeId=" + hdnId.Value, "EndDate DESC")[0];
            H_EmployeeIncrementHeldup entity = new H_EmployeeIncrementHeldup();
            entity.H_EmployeeId = Convert.ToInt32(hdnId.Value);
            entity.BranchId = eb.BranchId;
            entity.LetterNo = txtLetterNo.Text;
            entity.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            entity.FromDate = DBUtility.ToDateTime(txtEffectiveDate.Text);
            entity.IncrementStop = DBUtility.ToInt32(txtIncrementStop.Text);
            entity.UserLogin = User.Identity.Name;

            return entity;
        }
        private H_EmployeeRejoin GetH_EmployeeRejoin()
        {
            H_Employee emp = H_Employee.GetById(Convert.ToInt32(hdnId.Value));
            H_EmployeeBranch eb = H_EmployeeBranch.Find("H_EmployeeId=" + hdnId.Value, "EndDate DESC")[0];
            H_EmployeeRejoin entity = new H_EmployeeRejoin();
            entity.H_EmployeeId = Convert.ToInt32(hdnId.Value);
            entity.SourceBranchId = eb.BranchId;
            entity.DestinationBranchId = Convert.ToInt32(ddlRejoinBranch.SelectedValue);
            entity.LetterNo = txtLetterNo.Text;
            entity.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            entity.FromDate = DBUtility.ToDateTime(txtEffectiveDate.Text);
            entity.RejoinDate = DBUtility.ToDateTime(txtEffectiveDate.Text);
            entity.RejoinType = emp.EmploymentType;

            return entity;
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
            if (Type == TYPE_ADD)
            {
                if (String.IsNullOrEmpty(hdnId.Value))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "No Employee Found";
                    return msg;
                }
            }
            if (Type==TYPE_EDIT)
            {
                if (String.IsNullOrEmpty(hdnLetterId.Value))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "No Letter Found for Update";
                    return msg;
                }
            }
            List<ListItem> selectedValues = chkLetterTypes.Items.Cast<ListItem>()
                                                .Where(li => li.Selected)
                                                .ToList();
            if (selectedValues.Count ==0)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Letter Type not selected";
                return msg;
            }

            return msg;
        }
        protected override Asa.Hrms.Utility.Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                string desc = "INSER [H_EmployeeMultiLetter]";
                this.TransactionManager = new TransactionManager(true, desc);
                H_EmployeeMultiLetter letter=null;
                if (this.Type == TYPE_EDIT)
                {
                    letter=H_EmployeeMultiLetter.GetById(Convert.ToInt32(hdnLetterId.Value));
                    if (letter.H_EmployeeTransferHistoryId != null) H_EmployeeTransferHistory.Delete(this.TransactionManager, letter.H_EmployeeTransferHistoryId.Value);
                    if (letter.H_EmployeePromotionHistoryId != null) H_EmployeePromotionHistory.Delete(this.TransactionManager, letter.H_EmployeePromotionHistoryId.Value);
                    if (letter.H_EmployeePenatyId != null) H_EmployeePenalty.Delete(this.TransactionManager, letter.H_EmployeePenatyId.Value);
                    if (letter.H_EmployeeWarningId != null) H_EmployeeWarning.Delete(this.TransactionManager, letter.H_EmployeeWarningId.Value);
                    if (letter.H_EmployeeIncrementHeldupId != null) H_EmployeeIncrementHeldup.Delete(this.TransactionManager, letter.H_EmployeeIncrementHeldupId.Value);
                    if (letter.H_EmployeeRejoinId != null) H_EmployeeRejoin.Delete(this.TransactionManager, letter.H_EmployeeRejoinId.Value);
                    
                }
                else
                {
                    letter=new H_EmployeeMultiLetter();
                    letter.H_EmployeeId = Convert.ToInt32(hdnId.Value); 
                }
                List<string> selectedValues = chkLetterTypes.Items.Cast<ListItem>()
                                                .Where(li => li.Selected)
                                                .Select(li => li.Value)
                                                .ToList();
                //Transfer
                if (selectedValues.Contains("1"))
                {
                    H_EmployeeTransferHistory transfer = GetH_EmployeeTransfer();
                    H_EmployeeTransferHistory.Insert(this.TransactionManager, transfer);
                    letter.H_EmployeeTransferHistoryId = transfer.Id;
                }
                else
                {
                    letter.H_EmployeeTransferHistoryId = null;
                }
                //Promotion
                if (selectedValues.Contains("2"))
                {
                    H_EmployeePromotionHistory promotion = GetH_EmployeePromotion();
                    H_EmployeePromotionHistory.Insert(this.TransactionManager, promotion);
                    letter.H_EmployeePromotionHistoryId = promotion.Id;
                }
                else
                {
                    letter.H_EmployeePromotionHistoryId = null;
                }
                //Penalty
                if (selectedValues.Contains("3"))
                {
                    H_EmployeePenalty penalty = GetH_EmployeePenalty();
                    H_EmployeePenalty.Insert(this.TransactionManager, penalty);
                    letter.H_EmployeePenatyId = penalty.Id;
                }
                else
                {
                    letter.H_EmployeePenatyId = null;
                }
                //Warning
                if (selectedValues.Contains("4"))
                {
                    H_EmployeeWarning warning = GetH_EmployeeWarning();
                    H_EmployeeWarning.Insert(this.TransactionManager, warning);
                    letter.H_EmployeeWarningId = warning.Id;
                }
                else
                {
                    letter.H_EmployeeWarningId = null;
                }
                //Increment heldup
                if (selectedValues.Contains("5"))
                {
                    H_EmployeeIncrementHeldup increment = GetH_EmployeeIncrementHeldup();
                    H_EmployeeIncrementHeldup.Insert(this.TransactionManager, increment);
                    letter.H_EmployeeIncrementHeldupId = increment.Id;
                }
                else
                {
                    letter.H_EmployeeIncrementHeldupId = null;
                }
                //Rejoin
                if (selectedValues.Contains("6"))
                {
                    H_EmployeeRejoin rejoin = GetH_EmployeeRejoin();
                    H_Employee h_Employee = H_Employee.GetById(Convert.ToInt32(hdnId.Value));
                    if (h_Employee.Status == H_Employee.Statuses.Consultancy)
                    {
                        IList<H_EmployeeConsultency> list = Asa.Hrms.Data.Entity.H_EmployeeConsultency.Find("H_EmployeeId=" + h_Employee.Id + " AND Status=1", "");
                        if (list.Count > 0)
                        {
                            foreach (H_EmployeeConsultency h_Consultency in list)
                            {
                                h_Consultency.EndDate = DBUtility.ToDateTime(txtEffectiveDate.Text).AddDays(-1);
                                h_Consultency.Status = H_EmployeeConsultency.Statuses.INACTIVE;
                                H_EmployeeConsultency.Update(this.TransactionManager, h_Consultency);
                                rejoin.FromDate = h_Consultency.StartDate;
                            }
                        }
                    }
                    if (h_Employee.Status == H_Employee.Statuses.In_Leave)
                    {
                        IList<H_EmployeeLeave> list = Asa.Hrms.Data.Entity.H_EmployeeLeave.Find("H_EmployeeId=" + h_Employee.Id + " AND Status=1", "");
                        if (list.Count > 0)
                        {
                            foreach (H_EmployeeLeave leave in list)
                            {
                                leave.EndDate = DBUtility.ToDateTime(txtEffectiveDate.Text).AddDays(-1);
                                leave.Status = 2;
                                H_EmployeeLeave.Update(this.TransactionManager, leave);
                                rejoin.FromDate = leave.StartDate;
                            }
                        }
                    }
                    h_Employee.Status = (H_Employee.Statuses)((Int32)H_Employee.Statuses.Working);
                    Asa.Hrms.Data.Entity.H_Employee.Update(this.TransactionManager, h_Employee);
                    
                    H_EmployeeRejoin.Insert(this.TransactionManager, rejoin);
                    letter.H_EmployeeRejoinId = rejoin.Id;
                }
                else
                {
                    letter.H_EmployeeRejoinId = null;
                }

                H_Employee emp = H_Employee.GetById(TransactionManager, letter.H_EmployeeId);
                letter.Name = emp.Name;
                letter.Code = emp.Code;
                letter.LetterNo = txtLetterNo.Text;
                letter.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
                letter.EffectiveDate = DBUtility.ToDateTime(txtEffectiveDate.Text);
                letter.Subject = txtSubject.Text;
                letter.Body = txtBody.Text;
                letter.Conclution = "";// txtConclution.Text;
                letter.Signatory = txtSignatory.Text;
                letter.Designation = txtDesg.Text;
                letter.Duplication = txtDuplication.Text;
                letter.Note = DBUtility.ToNullableString(txtNote.Text);
                if (this.Type == TYPE_EDIT)
                    H_EmployeeMultiLetter.Update(TransactionManager, letter);
                else
                    H_EmployeeMultiLetter.Insert(TransactionManager, letter);

                TransactionManager.Commit();
                hdnLetterId.Value = letter.Id.ToString();
                this.Type = TYPE_EDIT;
                
            }

            return msg;
        }

        protected override void PrintData()
        {
            Save();
            ShowReport();
        }

        private void ShowReport()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Subject", typeof(String));
            dt.Columns.Add("LetterNo", typeof(String));
            dt.Columns.Add("LetterDate", typeof(String));
            dt.Columns.Add("AddressTo", typeof(String));
            dt.Columns.Add("Body", typeof(String));
            dt.Columns.Add("Conclution", typeof(String));
            dt.Columns.Add("Signatory", typeof(String));
            dt.Columns.Add("Designation", typeof(String));
            dt.Columns.Add("Duplication", typeof(String));
            dt.Columns.Add("Note", typeof(String));

            H_Employee emp = H_Employee.GetById(Convert.ToInt32(hdnId.Value));
            H_EmployeeDesignation ed = H_EmployeeDesignation.Find("H_EmployeeId=" + emp.Id, "EndDate DESC")[0];
            H_Designation desg = H_Designation.GetById(ed.H_DesignationId);

            H_EmployeeBranch eb = H_EmployeeBranch.Find("H_EmployeeId=" + emp.Id, "EndDate DESC")[0];
            Branch branch = Branch.GetById(eb.BranchId);
            Subzone district = Subzone.GetById(Region.GetById(branch.RegionId).SubzoneId);

            string AddressTo = string.Empty;
            if (emp.NameInBangla != null)
                AddressTo = emp.NameInBangla + "(" + UIUtility.NumberInBangla(emp.Code) + ")\r\n" + (desg.BanglaName != null ? desg.BanglaName : desg.Name) + "\r\n" + branch.Name + ", " + district.Name;
            else
                AddressTo = emp.Name + "(" + emp.Code + ")\r\n" + desg.Name + "\r\n" + branch.Name + ", " + district.Name;

            string letterDate = UIUtility.DateTimeInBangla(DBUtility.ToDateTime(txtLetterDate.Text), true);
            dt.Rows.Add(txtSubject.Text, txtLetterNo.Text, letterDate, AddressTo, txtBody.Text, "", txtSignatory.Text, txtDesg.Text, txtDuplication.Text,txtNote.Text);
            string reportName = "MultiLetter.rpt";
            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~/Reports/" + reportName));
            rd.SetDataSource(dt);
            Session["ReportType"] = Convert.ToInt32(1);
            ReportUtility.ViewReport(this, rd, false, false);
        }

        protected void chkLetterTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> selectedValues = chkLetterTypes.Items.Cast<ListItem>()
                                                .Where(li => li.Selected)
                                                .Select(li => li.Value)
                                                .ToList();
            if (selectedValues.Contains("1"))
            {
                pnlTransfer.Visible = true;
                rfvddlBranch.Enabled = true;
                rfvddlDistrict.Enabled = true;
            }
            else
            {
                pnlTransfer.Visible = false;
                rfvddlBranch.Enabled = false;
                rfvddlDistrict.Enabled = false;
            }
            if (selectedValues.Contains("2"))
            {
                pnlpromotion.Visible = true;
                rfvddlGrade.Enabled = true;
                rfvddlDesignation.Enabled = true;
            }
            else
            {
                pnlpromotion.Visible = false;
                rfvddlGrade.Enabled = false;
                rfvddlDesignation.Enabled = false;
            }
            if (selectedValues.Contains("3"))
            {
                pnlPenalty.Visible = true;
                rfvtxtAmount.Enabled = true;
                cvtxtAmount.Enabled = true;
                
            }
            else
            {
                pnlPenalty.Visible = false;
                rfvtxtAmount.Enabled = false;
                cvtxtAmount.Enabled = false;
            }
            if (selectedValues.Contains("4"))
            {
                pnlWarning.Visible = true;
            }
            else
            {
                pnlWarning.Visible = false;
            }

            if (selectedValues.Contains("5"))
            {
                pnlIncrementHeldup.Visible = true;
                rfvtxtIncrementStop.Enabled = true;
                cvtxtIncrementStop.Enabled = true;
            }
            else
            {
                pnlIncrementHeldup.Visible = false;
                rfvtxtIncrementStop.Enabled = false;
                cvtxtIncrementStop.Enabled = false;
            }
            if (selectedValues.Contains("6"))
            {
                H_Employee h_Employee = H_Employee.GetById(Convert.ToInt32(hdnId.Value));
                if (h_Employee.Status != H_Employee.Statuses.Consultancy && h_Employee.Status !=H_Employee.Statuses.In_Leave)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Warning;
                    msg.Msg = "Employee is not in leave/Consultancy";
                    this.ShowUIMessage(msg);
                    chkLetterTypes.Items.Cast<ListItem>().Where(li => li.Value == "6").Single().Selected = false;
                    return;
                }
                pnlRejoin.Visible = true;
                rfvddlRejoinDistrict.Enabled = true;
                rfvddlRejoinBranch.Enabled = true;
                
            }
            else
            {
                pnlRejoin.Visible = false;
                rfvddlRejoinDistrict.Enabled = false;
                rfvddlRejoinBranch.Enabled = false;
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistrict.SelectedValue != "0")
            {
                ddlBranch.DataSource = Branch.Find("RegionId IN(Select Id FROM Region Where SubzoneId=" + ddlDistrict.SelectedValue+")","Name");
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("Select Branch", "0"));
            }
        }
        protected void ddlRejoinDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRejoinDistrict.SelectedValue != "0")
            {
                ddlRejoinBranch.DataSource = Branch.Find("RegionId IN(Select Id FROM Region Where SubzoneId=" + ddlRejoinDistrict.SelectedValue + ")", "Name");
                ddlRejoinBranch.DataBind();
                ddlRejoinBranch.Items.Insert(0, new ListItem("Select Branch", "0"));
            }
        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGrade.SelectedValue != "0")
            {
                ddlDesignation.Items.Clear();
                TransactionManager tm = new TransactionManager(false);

                ddlDesignation.DataSource = tm.GetDataSet("SELECT H_Designation.Id, Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + this.ddlGrade.SelectedValue + " ORDER BY Name").Tables[0];
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, new ListItem("Select Designation", "0"));
            }
        }

       
        
    }
}
