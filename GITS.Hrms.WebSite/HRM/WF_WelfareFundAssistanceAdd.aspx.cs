using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class WF_WelfareFundAssistanceAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_WELFAREFUNDASST ADD"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected override string GetListPageUrl()
        {
            return "WF_WelfareFundAssistanceList.aspx";
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
            //IList<WF_WelfareFundAssistance> wfList = WF_WelfareFundAssistance.FindByEmployeeId(Convert.ToInt32(hdnId.Value), "");
            //if (wfList.Count > 1)
            //{
            //    int num = (from w in wfList
            //               where w.WF_DiseasesId.ToString() == ddlDiseases.SelectedValue
            //               select w).Count();
            //    if (num > 1)
            //    {
            //        msg.Type = MessageType.Error;
            //        msg.Msg = "One cant get Welfare Fund Assistance more than 2 Times for same Disease";
            //        return msg;
            //    }
            //}
            //if (wfList.Count >= 2)
            //{

            //    msg.Type = MessageType.Error;
            //    msg.Msg = "One cant get Welfare Fund Assistance more than 2 Times";
            //    return msg;
            //}
            return msg;
        }
        private WF_WelfareFundAssistance GetWF_WelfareFundAssistance()
        {
            WF_WelfareFundAssistance wfa = new WF_WelfareFundAssistance();
            if (Type == TYPE_EDIT)
            {
                wfa = WF_WelfareFundAssistance.GetById(Convert.ToInt32(hdnId.Value));

            }
            else
            {
                wfa = new WF_WelfareFundAssistance();
                wfa.H_EmployeeId = DBUtility.ToInt32(hdnId.Value);
            }

            wfa.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            wfa.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            wfa.BranchId = DBUtility.ToInt32(hdnBranch.Value);
            wfa.Amount = DBUtility.ToDouble(txtAmount.Text);
            wfa.WF_DiseasesId = DBUtility.ToInt32(ddlDiseases.SelectedValue);
            wfa.Remarks = DBUtility.ToNullableString(txtRemarks.Text);
            wfa.FundType = DBUtility.ToInt32(ddlFundType.SelectedValue);

            return wfa;
        }
        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                string desc = null;
                WF_WelfareFundAssistance h_EmployeeTransfer = GetWF_WelfareFundAssistance();
                if (Type == TYPE_EDIT)
                {
                    desc = "Update [WF_WelfareFundAssistance]";
                }
                else
                {
                    desc = "Insert [WF_WelfareFundAssistance]";
                }

                TransactionManager = new TransactionManager(true, desc);
                if (Type == TYPE_EDIT)
                {
                    WF_WelfareFundAssistance.Update(TransactionManager, h_EmployeeTransfer);
                }
                else
                {
                    WF_WelfareFundAssistance.Insert(TransactionManager, h_EmployeeTransfer);
                }

                hdnId.Value = h_EmployeeTransfer.Id.ToString();
                Type = TYPE_EDIT;

                TransactionManager.Commit();
                LoadGrid(h_EmployeeTransfer.H_EmployeeId);
            }

            return msg;
        }

        protected override void LoadData()
        {
            IList<WF_Diseases> list = WF_Diseases.FindAll();
            WF_Diseases select = new WF_Diseases();
            select.Id = 0;
            select.Name = "Select Disease";
            select.Status = WF_Diseases.Statuses.ACTIVE;
            list.Insert(0, select);
            ddlDiseases.DataSource = list;
            ddlDiseases.DataBind();

        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));

            if (h_Employee != null)
            {
                hdnId.Value = "0";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";
                txtZone.Text = "";
                txtSubzone.Text = "";
                txtRegion.Text = "";
                txtBranch.Text = "";
                //if (h_Employee.Status != H_Employee.Statuses.Working)
                //{
                //    Message msg = new Message();
                //    msg.Type = MessageType.Error;
                //    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                //    this.ShowUIMessage(msg);
                //    return;
                //}

                //IList<H_EmployeeTransferHistory> iTranList = H_EmployeeTransferHistory.Find(" H_EmployeeId=" + h_Employee.Id + " AND Status=1", "JoiningDate DESC");

                //if (iTranList != null && iTranList.Count > 0)
                //{
                //    H_EmployeeTransferHistory h_EmployeeTransfer = iTranList[0];
                //    if (h_EmployeeTransfer.JoiningDate >= DateTime.Today.Date)
                //    {
                //        Branch branch1 = Branch.GetById(h_EmployeeTransfer.DestinationBranchId);
                //        Message msg = new Message();
                //        msg.Type = MessageType.Error;
                //        msg.Msg = "Already Transfered to " + branch1.Name + " Joining Date: " + h_EmployeeTransfer.JoiningDate.ToString("dd/MM/yyyy");
                //        this.ShowUIMessage(msg);
                //        return;
                //    }
                //}
                Type = TYPE_ADD;
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
                txtSubzone.Text = Subzone.GetById(region.SubzoneId).Name;
                txtRegion.Text = region.Name;
                txtBranch.Text = branch.Name;
                hdnBranch.Value = branch.Id.ToString();

                LoadGrid(h_Employee.Id);
            }
            else
            {
                hdnId.Value = "0";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";
                txtZone.Text = "";
                txtSubzone.Text = "";
                txtRegion.Text = "";
                txtBranch.Text = "";

                if (txtEmployee.Text.Trim() != "")
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "No employee found";
                    ShowUiMessage(msg);
                }
            }
        }
        private void LoadGrid(Int32 h_EmployeeId)
        {
            IList<WF_WelfareFundAssistance> wfList = WF_WelfareFundAssistance.FindByEmployeeId(h_EmployeeId, "");

            var result = from w in wfList
                select new
                {
                    Letter_No = w.LetterNo,
                    Letter_Date = w.LetterDate,
                    Amount = w.Amount,
                    Branch_Name = Branch.GetById(w.BranchId).Name,
                    FundType = w.FundType == 1 ? "পরিবার কল্যান তহবিল" : "কর্মি কল্যান তহবিল",
                    DiseaseName = WF_Diseases.GetById(w.WF_DiseasesId).Name,
                    Remarks = w.Remarks
                };

            var result1 = from t in result
                select t;

            gvList.DataSource = LINQToDataTable(result.AsQueryable());
            gvList.DataBind();
        }
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {

                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                                                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                        (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
}

