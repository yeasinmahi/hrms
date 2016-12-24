using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Asa.Hrms.Web;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;
using System.Collections.Generic;

namespace Asa.Hrms.WebSite.Reports
{
    public partial class H_PunishmentReport : GridPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void LoadData()
        {
            //base.LoadData();
        }
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            if (txtEmpId.Text == "")
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "Enter Employee ID";
                ShowUIMessage(msg);
                return;
            }
            H_Employee employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmpId.Text) + UIUtility.GetAccessLevel(User.Identity.Name));

            if (employee != null)
            {
                txtName.Text = employee.Name;
                txtJoiningDate.Text = UIUtility.Format(employee.JoiningDate);
                txtBirthDate.Text = UIUtility.Format(employee.DateOfBirth);
                H_EmployeePhoto ePhoto = H_EmployeePhoto.GetByH_EmployeeId(employee.Id);
                if (ePhoto.Photo != null)
                {
                    string base64String = Convert.ToBase64String(ePhoto.Photo, 0, ePhoto.Photo.Length);
                    imgPhoto.ImageUrl = "data:image/jpg;base64," + base64String;
                }
                H_EmployeeGrade grade = H_EmployeeGrade.Get("H_EmployeeId=" + employee.Id + " AND DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) Between StartDate AND EndDate");
                txtGrade.Text = H_Grade.GetById(grade.H_GradeId).Name;

                H_EmployeeDesignation desg = H_EmployeeDesignation.Get("H_EmployeeId=" + employee.Id + " AND DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) Between StartDate AND EndDate");
                txtDesignation.Text = H_Designation.GetById(desg.H_DesignationId).Name;

                H_EmployeeBranch eb = H_EmployeeBranch.Get("H_EmployeeId=" + employee.Id + " AND DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) Between StartDate AND EndDate");
                Branch branch = Branch.GetById(eb.BranchId);
                txtBranch.Text = branch.Name;
                txtAsaDistrict.Text = Subzone.GetById(Region.GetById(branch.RegionId).SubzoneId).Name;
                H_Address address = H_Address.GetById(employee.PermanentAddressId);
                Thana thana = Thana.GetById(address.ThanaId);
                txtThana.Text = thana.Name;
                txtOwnDistrict.Text = District.GetById(thana.DistrictId).Name;

                LoadTransferInfo(employee.Id);
                LoadLeaveInfo(employee.Id);
                LoadPenaltyInfo(employee.Id);
                WarningInfo(employee.Id);
                IncreamentHeldup(employee.Id);
                PromotionInfo(employee.Id);
            }
            else
            {
                txtName.Text = "";
                txtJoiningDate.Text = "";
                txtBirthDate.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";
                txtBranch.Text = "";
                txtOwnDistrict.Text = "";
                txtThana.Text = "";
                txtAsaDistrict.Text = "";
                txtDept.Text = "";
                txtEmpId.Text = "";
                gvLeav.DataSource = null;
                gvLeav.DataBind();

                gvTransfer.DataSource = null;
                gvTransfer.DataBind();
                gvPenalty.DataSource = null;
                gvPenalty.DataBind();
                gvWarning.DataSource = null;
                gvWarning.DataBind();
                gvIncreamentHeldup.DataSource = null;
                gvIncreamentHeldup.DataBind();
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "No Employee Found";
                ShowUIMessage(msg);
                txtEmpId.Focus();

            }
        }


        private void LoadTransferInfo(int p)
        {
            this.TransactionManager = new Asa.Hrms.Data.TransactionManager(false);
            string query = "SELECT t.LetterNo,t.LetterDate,t.JoiningDate,sb.Name AS From_Branch,db.Name AS To_Branch "
                        + " FROM H_EmployeeTransfer t"
                        + " INNER JOIN Branch sb ON sb.Id=t.SourceBranchId"
                        + " INNER JOIN Branch db ON db.Id=t.DestinationBranchId"
                        + " where H_EmployeeId=" + p +" AND Type=8"
                        + " ORDER BY t.JoiningDate DESC";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvTransfer.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                Asa.Hrms.Web.BoundField bf = new Asa.Hrms.Web.BoundField();
                bf.HeaderText = dc.ColumnName.Replace("_", " ");
                bf.DataField = dc.ColumnName;
                if (dc.DataType == typeof(DateTime))
                {
                    bf.DataFormatString = "{0:dd/MM/yyyy}";
                }
                gvTransfer.Columns.Add(bf);
            }
            gvTransfer.DataSource = dt;
            gvTransfer.DataBind();
        }
        private void LoadLeaveInfo(int p)
        {
            this.TransactionManager = new Asa.Hrms.Data.TransactionManager(false);
            string query = "SELECT Case When t.[Type]=15 Then 'Suspension' Else 'Force Leave' end AS [Type], t.LetterNo AS Letter_No,t.LetterDate AS Letter_Date,t.StartDate AS Start_Date,t.EndDate AS End_Date,t.Cause "
                        + " FROM H_EmployeeLeave t"
                        + " where t.H_EmployeeId=" + p+" AND [Type] IN (15,16) " 
                        + " ORDER BY t.StartDate DESC";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvLeav.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                Asa.Hrms.Web.BoundField bf = new Asa.Hrms.Web.BoundField();
                bf.HeaderText = dc.ColumnName.Replace("_", " ");
                bf.DataField = dc.ColumnName;
                if (dc.DataType == typeof(DateTime))
                {
                    bf.DataFormatString = "{0:dd/MM/yyyy}";
                }
                gvLeav.Columns.Add(bf);
            }
            gvLeav.DataSource = dt;
            gvLeav.DataBind();
        }
        private void LoadPenaltyInfo(int p)
        {
            this.TransactionManager = new Asa.Hrms.Data.TransactionManager(false);
            string query = "SELECT t.LetterNo AS Letter_No,t.LetterDate AS Letter_Date,Case when t.FineType='F' then 'Fine' else 'Penalty' end AS Fine_Type,t.FineAmount AS Fine_Amount "
                        + " FROM H_EmployeePenalty t"
                        + " where t.H_EmployeeId=" + p
                        + " ORDER BY t.LetterDate DESC";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvPenalty.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                Asa.Hrms.Web.BoundField bf = new Asa.Hrms.Web.BoundField();
                bf.HeaderText = dc.ColumnName.Replace("_", " ");
                bf.DataField = dc.ColumnName;
                if (dc.DataType == typeof(DateTime))
                {
                    bf.DataFormatString = "{0:dd/MM/yyyy}";
                }
                gvPenalty.Columns.Add(bf);
            }
            gvPenalty.DataSource = dt;
            gvPenalty.DataBind();
        }
        private void WarningInfo(int p)
        {
            this.TransactionManager = new Asa.Hrms.Data.TransactionManager(false);
            string query = "SELECT t.LetterNo AS Letter_No,t.LetterDate AS Letter_Date,t.Duration,b.Name AS Branch ,t.Cause "
                        + " FROM H_EmployeeWarning t"
                        + " INNER JOIN Branch b ON b.Id=t.BranchId"
                        + " where t.H_EmployeeId=" + p
                        + " ORDER BY t.LetterDate DESC";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvWarning.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                Asa.Hrms.Web.BoundField bf = new Asa.Hrms.Web.BoundField();
                bf.HeaderText = dc.ColumnName.Replace("_", " ");
                bf.DataField = dc.ColumnName;
                if (dc.DataType == typeof(DateTime))
                {
                    bf.DataFormatString = "{0:dd/MM/yyyy}";
                }
                gvWarning.Columns.Add(bf);
            }
            gvWarning.DataSource = dt;
            gvWarning.DataBind();
        }
        private void IncreamentHeldup(int p)
        {
            this.TransactionManager = new Asa.Hrms.Data.TransactionManager(false);
            string query = "SELECT t.LetterNo AS Letter_No,t.LetterDate AS Letter_Date,t.FromDate AS From_Date,t.ToDate AS To_Date,b.Name AS Branch ,t.Cause "
                        + " FROM H_EmployeeIncrementHeldup t"
                        + " INNER JOIN Branch b ON b.Id=t.BranchId"
                        + " where t.H_EmployeeId=" + p
                        + " ORDER BY t.LetterDate DESC";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvIncreamentHeldup.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                Asa.Hrms.Web.BoundField bf = new Asa.Hrms.Web.BoundField();
                bf.HeaderText = dc.ColumnName.Replace("_", " ");
                bf.DataField = dc.ColumnName;
                if (dc.DataType == typeof(DateTime))
                {
                    bf.DataFormatString = "{0:dd/MM/yyyy}";
                }
                gvIncreamentHeldup.Columns.Add(bf);
            }
            gvIncreamentHeldup.DataSource = dt;
            gvIncreamentHeldup.DataBind();
        }
        private void PromotionInfo(int p)
        {
            this.TransactionManager = new Asa.Hrms.Data.TransactionManager(false);
            string query = "select p.LetterNo AS Letter_No,p.LetterDate AS Letter_Date,p.PromotionDate as Promotion_Date,sg.Name AS From_Grade ,ng.Name as To_Grade " +
                           ",sd.Name AS From_Designation,nd.Name AS To_Designation " +
                           "from H_EmployeePromotion p " +
                           "INNER JOIN H_Grade sg ON p.OldH_GradeId=sg.Id " +
                           "INNER JOIN H_Grade ng ON p.NewH_GradeId=ng.Id " +
                           "INNER JOIN H_Designation sd ON p.OldH_DesignationId=sd.Id " +
                           "INNER JOIN H_Designation nd ON p.NewH_DesignationId=nd.Id " +
                           "where p.H_EmployeeId=" + p + " AND p.Type=4" +
                           " order by p.PromotionDate ";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvPromotion.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                Asa.Hrms.Web.BoundField bf = new Asa.Hrms.Web.BoundField();
                bf.HeaderText = dc.ColumnName.Replace("_", " ");
                bf.DataField = dc.ColumnName;
                if (dc.DataType == typeof(DateTime))
                {
                    bf.DataFormatString = "{0:dd/MM/yyyy}";
                }
                gvPromotion.Columns.Add(bf);
            }
            gvPromotion.DataSource = dt;
            gvPromotion.DataBind();
        }
        protected override string GetAddPageUrl()
        {
            throw new NotImplementedException();
        }
        protected override Message ExportToExcel()
        {
            Message msg = new Message();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "printDiv", "printDiv('Indiv');", true);

            return msg;
        }
    }
}
