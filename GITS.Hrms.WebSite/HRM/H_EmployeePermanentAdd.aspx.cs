using System;
using System.Data;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeePermanentAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEPERMANENT"; }
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
            return "H_EmployeePermanentList.aspx";
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
            if (hdnId.Value !="0" || hdnId.Value !="")
            {
                H_Employee emp = H_Employee.GetById(Convert.ToInt32(hdnId.Value));
                if (emp.EmploymentType == H_Employee.EmploymentTypes.Permanent)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid Operation. Employee already permanent";
                    return msg;
                }
            }

            return msg;
        }
        private H_EmployeePermanentHistory GetH_EmployeePermanent()
        {
            H_EmployeePermanentHistory h_EmployeePenalty = new H_EmployeePermanentHistory();

            if (Type == TYPE_EDIT)
            {
                h_EmployeePenalty = H_EmployeePermanentHistory.GetById(Convert.ToInt32(hdnPer.Value));
            }
            else
            {
                h_EmployeePenalty = new H_EmployeePermanentHistory();
                h_EmployeePenalty.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
            }
            
            h_EmployeePenalty.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeePenalty.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeePenalty.Remarks = DBUtility.ToString(txtRemarks.Text);
            h_EmployeePenalty.PermanentDate = DBUtility.ToDateTime(txtPermanentDate.Text);
            h_EmployeePenalty.Status = true;

            return h_EmployeePenalty;
        }
        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                H_EmployeePermanentHistory h_Permanent = GetH_EmployeePermanent();
                string desc = string.Empty;
                if (Type == TYPE_EDIT)
                {
                    desc = "UPDATE [H_EmployeePermanentHistory]";
                }
                else
                {
                    desc = "INSERT [H_EmployeePermanentHistory]";
                }

                TransactionManager = new TransactionManager(true, desc);
                if (Type == TYPE_EDIT)
                {
                    H_EmployeePermanentHistory.Update(TransactionManager, h_Permanent);
                }
                else
                {
                    H_EmployeePermanentHistory.Insert(TransactionManager, h_Permanent);
                }

                //H_Employee h_Employee = H_Employee.GetById(h_Permanent.H_EmployeeId);
                //h_Employee.EmploymentType = H_Employee.EmploymentTypes.Permanent;
                //h_Employee.PermanentLetterNo = DBUtility.ToString(txtLetterNo.Text);
                //h_Employee.PermanentLetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
                //h_Employee.PermanentDate = DBUtility.ToDateTime(txtPermanentDate.Text);
                //GITS.Hrms.Data.Entity.H_Employee.Update(h_Employee);
                Type = TYPE_EDIT;

                TransactionManager.Commit();
            }

            return msg;
        }
        protected override void LoadData()
        {
            if (Request.QueryString["Id"] != null)
            {
                hdnPer.Value = Request.QueryString["Id"];
                
                H_EmployeePermanentHistory h_History = H_EmployeePermanentHistory.GetById(Convert.ToInt32(hdnPer.Value));

                if (h_History != null)
                {
                    hdnId.Value = h_History.H_EmployeeId.ToString();
                    Type = TYPE_EDIT;
                    txtLetterNo.Text = h_History.LetterNo;
                    txtLetterDate.Text = UIUtility.Format(h_History.LetterDate);
                    txtPermanentDate.Text = UIUtility.Format(h_History.PermanentDate);
                    txtRemarks.Text = h_History.Remarks;

                    TransactionManager tm = new TransactionManager(false);
                    H_Employee h_Employee = H_Employee.GetById(h_History.H_EmployeeId);
                    if (h_Employee != null)
                    {
                        txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                        txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                        txtEmployementType.Text = ((H_Employee.EmploymentTypes)h_Employee.EmploymentType).ToString();
                        txtJoiningDate.Text = UIUtility.Format(h_Employee.JoiningDate);
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

                       

                    }
                    else
                    {
                        hdnPer.Value = "0";
                        hdnId.Value = "0";
                        txtDistrict.Text = "";
                        txtDesignation.Text = "";
                        txtBranch.Text = "";
                        txtStatus.Text = "";

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
                txtEmployementType.Text = ((H_Employee.EmploymentTypes)h_Employee.EmploymentType).ToString();
                txtJoiningDate.Text = UIUtility.Format(h_Employee.JoiningDate);
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
                Type = TYPE_ADD;
                if (h_Employee.EmploymentType == H_Employee.EmploymentTypes.Permanent)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "The Employee is Permanent ( Letter No:" + h_Employee.PermanentLetterNo + "  Letter Date:" + (h_Employee.PermanentLetterDate != null ? UIUtility.Format(h_Employee.PermanentLetterDate) : "") + "  Permanent Date:" + (h_Employee.PermanentDate != null ? UIUtility.Format(h_Employee.PermanentDate) : "") + ")";
                    ShowUiMessage(msg);
                    
                }
                H_EmployeePermanentHistory h_per = H_EmployeePermanentHistory.Get("H_EmployeeId=" + h_Employee.Id);
                if (h_per != null)
                {
                    txtLetterNo.Text = h_per.LetterNo;
                    txtLetterDate.Text = UIUtility.Format(h_per.LetterDate);
                    txtPermanentDate.Text = UIUtility.Format(h_per.PermanentDate);
                    txtRemarks.Text = h_per.Remarks;
                    hdnPer.Value = h_per.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    txtLetterNo.Text = "";
                    txtLetterDate.Text = "";
                    txtPermanentDate.Text = "";
                    txtRemarks.Text = "";
                    hdnPer.Value = "0";
                    Type = TYPE_ADD;
                }


            }
            else
            {
                hdnPer.Value = "0";
                hdnId.Value = "0";
                txtDistrict.Text = "";
                txtDesignation.Text = "";
                txtBranch.Text = "";
                txtStatus.Text = "";

            }


        }

    }
}
