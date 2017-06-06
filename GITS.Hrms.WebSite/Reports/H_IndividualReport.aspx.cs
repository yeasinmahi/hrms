using System;
using System.Data;
using System.Web.UI;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;
using BoundField = GITS.Hrms.Library.Web.BoundField;

namespace GITS.Hrms.WebSite.Reports
{
    public partial class H_IndividualReport : GridPage
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
            if (txtEmpId.Text == "" )
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "Enter Employee ID";
                ShowUiMessage(msg);
                return;
            }
            H_Employee employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(txtEmpId.Text) + UIUtility.GetAccessLevel(User.Identity.Name));

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
                Region region = Region.GetById(branch.RegionId);
                txtRegion.Text = region.Name;
                Subzone subzone=Subzone.GetById(region.SubzoneId);
                txtAsaDistrict.Text = subzone.Name;// Subzone.GetById(region.SubzoneId).Name;
                txtZone.Text = Zone.GetById(subzone.ZoneId).Name;
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
                ShowUiMessage(msg);
                txtEmpId.Focus();

            }
        }

        
        private void LoadTransferInfo(int p)
        {
            TransactionManager = new TransactionManager(false);
            string query = "SELECT t.LetterNo,t.LetterDate,d.Name as Designation,sb.Name AS From_Branch,ss.Name AS From_District,db.Name AS To_Branch,t.JoiningDate as To_Branch_Date,ds.Name as To_District "
                          +" FROM H_EmployeeTransfer t"
                          +" INNER JOIN Branch sb ON sb.Id=t.SourceBranchId"
                          +" INNER JOIN Region sr ON sb.RegionId=sr.Id"
                          +" INNER JOIN Subzone ss ON sr.SubZoneId=ss.Id"
                          +" INNER JOIN Branch db ON db.Id=t.DestinationBranchId"
                          +" INNER JOIN Region dr ON db.RegionId=dr.Id"
                          +" INNER JOIN Subzone ds ON dr.SubZoneId=ds.Id"
                          +" INNER JOIN H_EmployeeDesignation ed ON ed.H_EmployeeId=t.H_EmployeeId and t.JoiningDate Between ed.StartDate and ed.EndDate"
                          +" INNER JOIN H_Designation d ON ed.H_DesignationId=d.Id"
                          +" where t.H_EmployeeId="+p
                          + " ORDER BY t.LetterDate";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvTransfer.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                BoundField bf = new BoundField();
                bf.HeaderText = dc.ColumnName.Replace("_"," ");
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
            TransactionManager = new TransactionManager(false);
            string query = "SELECT t.LetterNo AS Letter_No,t.LetterDate AS Letter_Date,t.StartDate AS Start_Date,t.EndDate AS End_Date,Leave_Type=Case t.[Type] when 11 then 'Leave With Pay' when 12 then 'Leave Without Pay' when 13 then 'Medical Leave' when 14 then 'Maternity Leave' when 15 then 'Suspension' when 16 then 'Force Leave' when 17 then 'Lien' else '' end "
                        + " FROM H_EmployeeLeave t"
                        + " where t.H_EmployeeId=" + p
                        + " ORDER BY t.StartDate";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvLeav.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                BoundField bf = new BoundField();
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
            TransactionManager = new TransactionManager(false);
            string query = "SELECT t.LetterNo AS Letter_No,t.LetterDate AS Letter_Date,Case when t.FineType='F' then 'Fine' else 'Penalty' end AS Fine_Type,t.FineTime,t.FineAmount AS Fine_Amount "
                        + " FROM H_EmployeePenalty t"
                        + " where t.H_EmployeeId=" + p
                        + " ORDER BY t.LetterDate";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvPenalty.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                BoundField bf = new BoundField();
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
            TransactionManager = new TransactionManager(false);
            string query = "SELECT t.LetterNo AS Letter_No,t.LetterDate AS Letter_Date,t.TotalWarningTime as Times,b.Name AS Branch ,t.Cause "
                        + " FROM H_EmployeeWarning t"
                        + " INNER JOIN Branch b ON b.Id=t.BranchId"
                        + " where t.H_EmployeeId=" + p
                        + " ORDER BY t.LetterDate";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvWarning.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                BoundField bf = new BoundField();
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
            TransactionManager = new TransactionManager(false);
            string query = "SELECT t.LetterNo AS Letter_No,t.LetterDate AS Letter_Date,t.FromDate AS From_Date,t.ToDate AS To_Date,b.Name AS Branch ,t.Cause "
                        + " FROM H_EmployeeIncrementHeldup t"
                        + " INNER JOIN Branch b ON b.Id=t.BranchId"
                        + " where t.H_EmployeeId=" + p
                        + " ORDER BY t.LetterDate";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvIncreamentHeldup.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                BoundField bf = new BoundField();
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
            TransactionManager = new TransactionManager(false);
            string query = "select Case WHEN p.Type=2 then 'Promotion' WHEN p.Type=4 then 'Demotion' WHEN p.Type=8 then 'Designation Change' else '' END AS [Type] "+
                           ",p.LetterNo AS Letter_No,p.LetterDate AS Letter_Date,p.PromotionDate as 'Promo/Demo_Date',sg.Name AS From_Grade ,ng.Name as To_Grade "+
                           ",sd.Name AS From_Designation,nd.Name AS To_Designation "+
                           "from H_EmployeePromotion p "+
                           "INNER JOIN H_Grade sg ON p.OldH_GradeId=sg.Id "+
                           "INNER JOIN H_Grade ng ON p.NewH_GradeId=ng.Id "+
                           "INNER JOIN H_Designation sd ON p.OldH_DesignationId=sd.Id "+
                           "INNER JOIN H_Designation nd ON p.NewH_DesignationId=nd.Id "+
                           "where p.H_EmployeeId="+p +
                           " order by p.LetterDate ";
            DataSet ds = TransactionManager.GetDataSet(query);
            DataTable dt = ds.Tables[0];
            gvPromotion.Columns.Clear();
            foreach (DataColumn dc in dt.Columns)
            {
                BoundField bf = new BoundField();
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

            ScriptManager.RegisterStartupScript(Page, GetType(), "printDiv", "printDiv('Indiv');", true);

            return msg;
        }
    }
}
