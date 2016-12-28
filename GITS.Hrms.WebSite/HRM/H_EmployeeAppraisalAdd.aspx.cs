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
    public partial class H_EmployeeAppraisalAdd : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string PropertyName
        {
            get
            {
                return "H_EMPLOYEEAPPRAISAL ADD";
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
                UserLocation ul = UserLocation.FindByLogin(User.Identity.Name, "")[0];
                if (ul.BranchId != null)
                {
                    TransactionManager manager = new TransactionManager(false);
                    String query = "select  e.Id,e.Code,e.Name,d.Name,d.GroupType "
                                + "from H_Employee e "
                                + "INNER JOIN H_EmployeeBranch eb ON e.Id=eb.H_EmployeeId and eb.EndDate='2099-12-31' "
                                + "INNER JOIN H_EmployeeDesignation ed ON e.Id=ed.H_EmployeeId and ed.EndDate='2099-12-31' "
                                + "INNER JOIN H_Designation d ON ed.H_DesignationId=d.Id and d.GroupType=2 "
                                + "where e.Status IN (1,3) "
                                + "and eb.BranchId=" + ul.BranchId;
                    DataTable apps = manager.GetDataSet(query).Tables[0];
                    lblAppraiser.Text = apps.Rows[0][2].ToString() + "(" + apps.Rows[0][1].ToString() + ")";
                    lblDesg.Text = apps.Rows[0][3].ToString();
                    hfApp.Value = apps.Rows[0][0].ToString();
                }
                else if (ul.RegionId != null)
                {
                    TransactionManager manager = new TransactionManager(false);
                    String query = "select  e.Id,e.Code,e.Name,d.Name,d.GroupType "
                                + "from H_Employee e "
                                + "INNER JOIN H_EmployeeBranch eb ON e.Id=eb.H_EmployeeId and eb.EndDate='2099-12-31' "
                                + "INNER JOIN Branch b ON eb.BranchId=b.Id "
                                + "INNER JOIN Region r ON b.RegionId=r.Id "
                                + "INNER JOIN H_EmployeeDesignation ed ON e.Id=ed.H_EmployeeId and ed.EndDate='2099-12-31' "
                                + "INNER JOIN H_Designation d ON ed.H_DesignationId=d.Id and d.GroupType=4 "
                                + "where e.Status IN (1,3) "
                                + "and r.Id=" + ul.RegionId;
                    DataTable apps = manager.GetDataSet(query).Tables[0];
                }
                else if (ul.SubzoneId != null)
                {
                    TransactionManager manager = new TransactionManager(false);
                    String query = "select  e.Id,e.Code,e.Name,d.Name,d.GroupType "
                                + "from H_Employee e "
                                + "INNER JOIN H_EmployeeBranch eb ON e.Id=eb.H_EmployeeId and eb.EndDate='2099-12-31' "
                                +"INNER JOIN Branch b ON eb.BranchId=b.Id "
                                +"INNER JOIN Region r ON b.RegionId=r.Id "
                                +"INNER JOIN Subzone s ON r.SubZoneId=s.Id "
                                + "INNER JOIN H_EmployeeDesignation ed ON e.Id=ed.H_EmployeeId and ed.EndDate='2099-12-31' "
                                + "INNER JOIN H_Designation d ON ed.H_DesignationId=d.Id and d.GroupType=5 "
                                + "where e.Status IN (1,3) "
                                + "and s.Id=" + ul.SubzoneId;
                    DataTable apps = manager.GetDataSet(query).Tables[0];
                }
                else
                {
                }
            }
        }

        protected override void LoadData()
        {
            H_Appraisal app = H_Appraisal.Get("Status=1");
            lblEvaluation.Text = app.Name;
            hfAppraisalId.Value = app.Id.ToString();

            IList<H_AppraisalQuestion> quest = H_AppraisalQuestion.Find("H_AppraisalId=" + app.Id, "");
            gvAppraisal.DataSource = quest;
            gvAppraisal.DataBind();
            
        }
        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            if(e.Item.Value=="REFRESH")
               UIUtility.Transfer(Page, Request.Path);

        }
        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                this.TransactionManager = new TransactionManager( true,"INSERT [H_EmployeeAppraisalMaster]");
                H_EmployeeAppraisalMaster master = new H_EmployeeAppraisalMaster();
                master.H_AppraisalId = Convert.ToInt32(hfAppraisalId.Value);
                master.H_EmployeeId = Convert.ToInt32(hdnId.Value);
                master.Appraiser = Convert.ToInt32(hfApp.Value);
                master.AppraisalDate = DateTime.Today.Date;
                master.EntryUser = User.Identity.Name;
                H_EmployeeAppraisalMaster.Insert(this.TransactionManager, master);
                foreach (GridViewRow dr in gvAppraisal.Rows)
                {

                    Int32 QuestionId = Convert.ToInt32(gvAppraisal.DataKeys[dr.RowIndex]["Id"].ToString());
                    RadioButtonList rdo = (RadioButtonList)dr.FindControl("rdoOption");
                    Int32 AnswerId = Convert.ToInt32(rdo.SelectedValue);
                    H_EmployeeAppraisalDetails detail = new H_EmployeeAppraisalDetails();
                    detail.H_EmployeeAppraisalMasterId = master.Id;
                    detail.H_AppraisalQuestionId = QuestionId;
                    detail.Marks = AnswerId;
                    H_EmployeeAppraisalDetails.Insert(this.TransactionManager, detail);
                    
                }

                TransactionManager.Commit();
            }

            return msg;
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
            foreach (GridViewRow dr in gvAppraisal.Rows)
            {

                //Int32 QuestionId = Convert.ToInt32(gvAppraisal.DataKeys[dr.RowIndex]["Id"].ToString());
                RadioButtonList rdo = (RadioButtonList)dr.FindControl("rdoOption");
                Int32 AnswerId;// = Convert.ToInt32(rdo.SelectedValue);
                if (!Int32.TryParse(rdo.SelectedValue,out AnswerId))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = (dr.RowIndex+1).ToString()+" no. Point fillup correctly";
                    return msg;
                }
            }
            return msg;
        }

    }
}
