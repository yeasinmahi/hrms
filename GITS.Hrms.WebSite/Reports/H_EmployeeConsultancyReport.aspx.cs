using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Asa.ExcelXmlWriter;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Reports
{
    public partial class H_EmployeeConsultancyReport : GridPage
    {
        private static DataSet dataset = null;
        private static string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.GridView = this.gvList;
            this.SortOrder = "ASC";
        }
        protected override void gvList_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            string sortedQuery = string.Empty;
            this.TransactionManager = new TransactionManager(false);
            sortedQuery = query + " ORDER BY " + e.SortExpression + " " + SortOrder;
            DataSet ds = TransactionManager.GetDataSet(sortedQuery);
            dataset = ds;
            DataTable dt = ds.Tables[0];
            DataColumn dcsl = new DataColumn("SL", typeof(Int32));

            dcsl.AutoIncrement = true;
            dcsl.AutoIncrementSeed = 1;
            dcsl.AutoIncrementStep = 1;
            dt.Columns.Add(dcsl);
            dcsl.SetOrdinal(0);
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = dr.Table.Rows.IndexOf(dr) + 1;
            }
            gvList.DataSource = dt;
            gvList.DataBind();
            this.SortOrder = this.SortOrder == "ASC" ? "DESC" : "ASC";
        }
        protected override void HandleSpecialCommand(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {
            this.Validate();

            if (this.IsValid)
            {
                switch (e.Item.Value)
                {
                    case "EXCEL":
                        this.ViewReport();
                        break;
                    case "SEARCH":
                        this.Search();
                        break;
                    default:
                        this.HandleSpecialCommand(sender, e);
                        break;
                }
            }
        }
        private void Search()
        {
            if (IsValid == false)
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid Data provided by the user";
                ShowUIMessage(msg);
                return;
            }
            if (txtEmpId.Text == "" && txtDesignation.Text == "" && txtCountry.Text == "" && txtFund.Text=="")
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "Enter value at least one field";
                ShowUIMessage(msg);
                return;
            }
            ShowReport();
        }
        private void ShowReport()
        {
            this.TransactionManager = new TransactionManager(false);
            if (string.IsNullOrEmpty(txtEmpId.Text))
            {

                query = "SELECT "
                            + "e.Code AS Employee_ID,"
                            + "e.Name AS Employee_Name,"
                            + "dsg.Name AS Designation,"
                            + "hec.LetterNo,"
                            + "hec.LetterDate,"
                            + "hec.NgoName,"
                            + "hec.StartDate,"
                            + " hec.EndDate,"
                            + " s.Name AS District,"
                            + " hec.Through,"
                            + "c.Name as Country "
                            + " FROM "
                            + " H_EmployeeConsultency hec "
                            + "INNER JOIN H_Employee e ON hec.H_EmployeeId =e.id "
                            +
                            "INNER JOIN H_EmployeeDesignation ed ON ed.H_EmployeeId=e.Id AND (DATEADD(dd, 0, DATEDIFF(dd, 0,GETDATE())) Between ed.StartDate AND ed.EndDate) "
                            + "INNER JOIN H_Designation dsg ON dsg.Id=ed.H_DesignationId "
                            +
                            " INNER JOIN H_EmployeeBranch AS heb ON heb.H_EmployeeId=e.id AND (DATEADD(dd, 0, DATEDIFF(dd, 0,GETDATE())) Between heb.StartDate AND heb.EndDate) "
                            + " INNER JOIN Branch AS b ON b.Id=heb.BranchId "
                            + " INNER JOIN Region AS r ON r.Id=b.RegionId "
                            + " INNER JOIN Subzone AS s ON s.id=r.SubZoneId"
                            + " inner join organization o on hec.OrganizationId=o.Id"
                            + " inner join Country c on hec.CountryId=c.Id"
                            + " WHERE hec.[Status]<>0";
               


                if (ddlDesignationFilter.SelectedValue == "1")
                {
                    query += " and dsg.Name Like '%" + txtDesignation.Text + "%'";
                }
                else
                {
                    query += " and dsg.Name ='" + txtDesignation.Text + "'";
                }
                if (ddlCountryFilter.SelectedValue == "1")
                {
                    query += " AND c.Name Like '%" + txtCountry.Text + "%'";
                }
                else
                {
                    query += " AND c.Name = '" + txtCountry.Text + "'";
                }

                if (ddlFund.SelectedValue == "1")
                {
                    query += " AND o.Name Like '%" + txtFund.Text + "%'";
                }
                else
                {
                    query += " AND o.Name = '" + txtFund.Text + "'";
                }
            }
            else
            {
                H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmpId.Text) + UIUtility.GetAccessLevel(User.Identity.Name));

                if (h_Employee != null)
                {
                    query = "SELECT "
                            + "e.Code AS Employee_ID,"
                            + "e.Name AS Employee_Name,"
                            + "dsg.Name AS Designation,"
                            + "hec.LetterNo,"
                            + "hec.LetterDate,"
                            + "hec.NgoName,"
                            + "hec.StartDate,"
                            + " hec.EndDate,"
                            + " s.Name AS District,"
                            + " hec.Through," 
                            +"c.Name as Country "
                            + " FROM "
                            + " H_EmployeeConsultency hec "
                            + "INNER JOIN H_Employee e ON hec.H_EmployeeId =e.id "
                            +
                            "INNER JOIN H_EmployeeDesignation ed ON ed.H_EmployeeId=e.Id AND (DATEADD(dd, 0, DATEDIFF(dd, 0,GETDATE())) Between ed.StartDate AND ed.EndDate) "
                            + "INNER JOIN H_Designation dsg ON dsg.Id=ed.H_DesignationId "
                            +
                            " INNER JOIN H_EmployeeBranch AS heb ON heb.H_EmployeeId=e.id AND (DATEADD(dd, 0, DATEDIFF(dd, 0,GETDATE())) Between heb.StartDate AND heb.EndDate) "
                            + " INNER JOIN Branch AS b ON b.Id=heb.BranchId "
                            + " INNER JOIN Region AS r ON r.Id=b.RegionId "
                            + " INNER JOIN Subzone AS s ON s.id=r.SubZoneId" 
                            +" inner join organization o on hec.OrganizationId=o.Id"
                            +" inner join Country c on hec.CountryId=c.Id"
                            + " WHERE e.code=" + h_Employee.Code;
                }
                //+" ORDER BY t.JoiningDate ";
            }

            DataSet ds = TransactionManager.GetDataSet(query);
            dataset = ds;
            DataTable dt = ds.Tables[0];
            gvList.Columns.Clear();
            DataColumn dcsl = new DataColumn("SL", typeof(Int32));

            //dcsl.AutoIncrement = true;
            //dcsl.AutoIncrementSeed = 1;
            //dcsl.AutoIncrementStep = 1;
            dt.Columns.Add(dcsl);
            dcsl.SetOrdinal(0);
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = dr.Table.Rows.IndexOf(dr) + 1;
            }

            foreach (DataColumn dc in dt.Columns)
            {
                BoundField bf = new BoundField();
                bf.HeaderText = dc.ColumnName.Replace("_", " ");
                bf.DataField = dc.ColumnName;
                bf.SortExpression = dc.ColumnName;
                if (dc.DataType == typeof(DateTime))
                {
                    bf.DataFormatString = "{0:dd/MM/yyyy}";
                }
                gvList.Columns.Add(bf);
            }
            gvList.DataSource = dt;// ds.Tables[0];
            gvList.DataBind();
        }

        private void ViewReport()
        {
            if (dataset == null)
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "At First Load Data, then Export ";
                ShowUIMessage(msg);
                return;
            }
            IList<DataTable> tables = new List<DataTable>();
            IList<IList<WorksheetRow>> headers = new List<IList<WorksheetRow>>();
            WorksheetRow[][] header = new WorksheetRow[dataset.Tables.Count][];
            Int32 i = 0;

            foreach (DataTable dt in dataset.Tables)
            {
                tables.Add(dt);

                header[i] = new WorksheetRow[4];
                header[i][0] = new WorksheetRow();
                header[i][1] = new WorksheetRow();
                header[i][2] = new WorksheetRow();
                header[i][3] = new WorksheetRow();

                header[i][0].Cells.Add("Employee Information", DataType.String, "HeaderTop1").MergeAcross = dt.Columns.Count - 1;
                header[i][1].Cells.Add("ASA, Central Office, 23/3 Shyamoli, Dhaka", DataType.String, "HeaderTop3").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2 - 1;
                header[i][2].Cells.Add("", DataType.String, "HeaderTop3").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2 - 1;
                header[i][2].Cells.Add("As On : " + UIUtility.Format(DateTime.Today.Date), DataType.String, "HeaderTop4").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2;

                foreach (DataColumn dc in dt.Columns)
                {
                    header[i][3].Cells.Add(dc.ColumnName, DataType.String, "HeaderLeftAlign");
                }

                i++;
            }

            for (i = 0; i < headers.Count; i++)
            {
                header[i] = headers[i].ToArray();
            }

            ExcelReportUtility.Instance.DataSource = tables.ToArray();
            ExcelReportUtility.Instance.Header = header;
            ExcelReportUtility.Instance.Name = "Emp_Info_" + "(" + DateTime.Now.Ticks + ")" + Configuration.ReportExtension;
            ExcelReportUtility.Instance.ViewReport();
        }
        protected override string GetAddPageUrl()
        {
            throw new NotImplementedException();
        }
        protected override void LoadData()
        {
           
        }
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            if (txtEmpId.Text == "" && txtDesignation.Text == "" && txtCountry.Text == "" && txtFund.Text=="")
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "Enter value at least one field";
                ShowUIMessage(msg);
                return;
            }
            ShowReport();
        }
    }
}
