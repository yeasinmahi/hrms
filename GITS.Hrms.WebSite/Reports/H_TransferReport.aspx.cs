using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Asa.ExcelXmlWriter;
using CrystalDecisions.CrystalReports.Engine;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Reports
{
    public partial class H_TransferReport : GridPage
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
            sortedQuery = query + " ORDER BY " + e.SortExpression +" "+SortOrder;
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
                    case "PRINT":
                        this.Print();
                        break;
                    default:
                        this.HandleSpecialCommand(sender, e);
                        break;
                }
            }
        }

        private void Print()
        {
            if (dataset == null)
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "At First Load Data, then Print ";
                ShowUIMessage(msg);
                return;
            }
            string reportName = "H_EmployeeTransferReport.rpt";
            ReportDocument rd = new ReportDocument();

            rd.Load(Server.MapPath("~/Reports/" + reportName));
            rd.SetDataSource(dataset.Tables[0]);
            Session["ReportType"] = 1;// Convert.ToInt32(ddlFormat.SelectedValue);
            ReportUtility.ViewReport(this, rd, false, false);
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
            if (txtEmpId.Text == "" && txtBranch.Text == "" && txtAsaDistrict.Text == "")
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "Enter value at least one field";
                ShowUIMessage(msg);
                return;
            }
            if (!String.IsNullOrEmpty(txtEmpId.Text))
            {
                String[] str = txtEmpId.Text.Split(',');
                foreach (String s in str)
                {
                    int value;
                    if (!int.TryParse(s, out value))
                    {
                        Message msg = new Message();
                        msg.Type = MessageType.Error;
                        msg.Msg = "Invalid Employee ID";
                        ShowUIMessage(msg);
                        return;
                    }
                }
            }
            ShowReport();
        }
        private void ShowReport()
        {
            this.TransactionManager = new TransactionManager(false);
            if (string.IsNullOrEmpty(txtEmpId.Text))
            {

                query = "select e.Code AS Employee_ID , e.Name,dsg.Name AS Designation,br.Name as Branch,sz.Name as ASA_District,eb.StartDate AS Start_Date,Case when eb.EndDate='2099-12-31' then null else eb.EndDate end AS End_Date"
                        + " from Branch br"
                        + " INNER JOIN H_EmployeeBranch eb ON eb.BranchId=br.Id"
                        + " INNER JOIN H_Employee e ON eb.H_EmployeeId=e.Id"
                        + " INNER JOIN H_EmployeeDesignation ed ON ed.H_EmployeeId=e.Id AND (DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) Between ed.StartDate AND ed.EndDate)"
                        + " INNER JOIN H_Designation dsg ON dsg.Id=ed.H_DesignationId"
                        + " INNER JOIN Region r ON r.Id=br.RegionId";
                if (ddlAsaDistrictFilter.SelectedValue == "1")
                {
                    query += " INNER JOIN Subzone sz ON sz.Id=r.SubzoneId AND sz.Name Like '%" + txtAsaDistrict.Text + "%'";
                }
                else
                {
                    query += " INNER JOIN Subzone sz ON sz.Id=r.SubzoneId AND sz.Name = '" + txtAsaDistrict.Text + "'";
                }
                        

                if (ddlBranchFilter.SelectedValue == "1")
                {
                    query += " where br.Name Like '%" + txtBranch.Text + "%'";
                }
                else
                {
                    query += " where br.Name ='" + txtBranch.Text + "'";
                }
                if (ddlStatus.SelectedValue != "0")
                {
                    query += " AND e.Status =" + ddlStatus.SelectedValue;
                }
            }
            else
            {
                query = "SELECT e.Code AS [Employee_ID],e.Name,dsg.Name AS Designation,"
                    + " t.LetterNo,t.LetterDate,sb.Name as [From_Branch],seb.StartDate as F_Br_Dt,sds.Name AS [From_District],[dbo].[fn_DistrictRegionBranchDate](e.Id,seb.StartDate,1,0) AS F_Dist_dt,db.Name as [To_Branch],t.JoiningDate as [To_Br_Date],dds.Name AS [To_District] ,[dbo].[fn_DistrictRegionBranchDate](e.Id,t.JoiningDate,1,0) AS To_Dist_dt, t.Remarks,"
                    + " Type= Case when t.Type=1 then 'Normal' when t.Type=2 then 'Punisment' when t.Type=4 then 'Promotion' else 'Demotion' end "
                    + " FROM H_EmployeeTransfer t"
                    + " INNER JOIN H_Employee e ON t.H_EmployeeId=e.Id"
                    + " INNER JOIN H_EmployeeDesignation ed ON ed.H_EmployeeId=e.Id AND (DATEADD(dd, 0, DATEDIFF(dd, 0, t.JoiningDate)) Between ed.StartDate AND ed.EndDate)"
                    + " INNER JOIN H_Designation dsg ON dsg.Id=ed.H_DesignationId"
                    + " INNER JOIN Branch sb ON sb.Id=t.SourceBranchId"
                    + " INNER JOIN H_EmployeeBranch seb ON seb.H_EmployeeId=e.Id AND (DATEADD(dd, 0, DATEDIFF(dd, 0, DateADD(day,-1,t.JoiningDate))) Between seb.StartDate AND seb.EndDate) "
                    + " INNER JOIN Branch db ON db.Id=t.DestinationBranchId"
                    +"  INNER JOIN Region AS sth ON sth.Id=sb.RegionId" 
                    +"  INNER JOIN Subzone AS sds ON sth.SubZoneId=sds.Id" 
                    +"  INNER JOIN Region AS dth ON dth.Id=db.RegionId" 
                    +"  INNER JOIN Subzone AS dds ON dth.SubZoneId=dds.Id"
                    + " where e.Code in(" + txtEmpId.Text+")"
                    + " ORDER BY e.Code, t.LetterDate ";
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
                dr[0] = dr.Table.Rows.IndexOf(dr)+1;
            }
           
            foreach (DataColumn dc in dt.Columns)
            {
                BoundField bf = new BoundField();
                bf.HeaderText = dc.ColumnName.Replace("_"," ");
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
                header[i][2].Cells.Add("Transfer Report", DataType.String, "HeaderTop3").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2 - 1;
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
            UIUtility.LoadEnums(ddlStatus, typeof(H_Employee.Statuses), false, false, false);
            ddlStatus.Items.Add(new System.Web.UI.WebControls.ListItem("All", "0", true));
        }
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            if (txtEmpId.Text == "" && txtBranch.Text == "" && txtAsaDistrict.Text == "")
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
