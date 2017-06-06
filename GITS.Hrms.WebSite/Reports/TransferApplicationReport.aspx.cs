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
    public partial class TransferApplicationReport : GridPage
    {
        private static DataSet dataset = null;
        private static string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            SortOrder = "ASC";
        }

        protected override void gvList_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            if (SortColumn != e.SortExpression)
            {
                SortColumn = e.SortExpression;
                SortOrder = "ASC";
            }
            else
            {
                SortOrder = (SortOrder == "ASC") ? "DESC" : "ASC";
            }
            if (Session["ReportTable"] != null)
            {
                DataTable rt = (DataTable)Session["ReportTable"];
                rt.DefaultView.Sort = SortColumn + " " + SortOrder;
                rt.DefaultView.ToTable();
                //this.ShowReport();
                gvList.DataSource = rt;
                gvList.DataBind();
            }
        }

        protected override void HandleSpecialCommand(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {
            Validate();

            if (IsValid)
            {
                switch (e.Item.Value)
                {
                    case "EXCEL":
                        ViewReport();
                        break;
                    case "SEARCH":
                        Search();
                        break;
                    default:
                        HandleSpecialCommand(sender, e);
                        break;
                }
            }
        }
        private void Search()
        {
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
                        ShowUiMessage(msg);
                        return;
                    }
                }
            }
            ShowReport();
        }
        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlStatus, typeof(H_EmployeeTransferApplication.Statuses), false,false, false);
        }


        protected void lbSearch_Click(object sender, EventArgs e)
        {
            
            ShowReport();
        }

        protected override string GetAddPageUrl()
        {
            throw new NotImplementedException();
        }
        private void ShowReport()
        {
            TransactionManager = new TransactionManager(false);
            query = "Select  e.Name,e.Code,desg.Name as Designation,eta.ApplicationNo,eta.ReceivingDate,b.Name as Branch,eb.StartDate as BranchDate,s.Name as ASADistrict"
                + " ,dbo.fn_SubzoneDate(e.Id) as DistrictDate,th.Name as BranchThana ,wd.Name as OwnDistrict,wt.Name as OwnThana,eta.DemandedPlace"
                + " ,Status=Case when eta.status=1 then 'Approved' when eta.status=2 then 'Processing' else 'Rejected' end"
                + " ,eta.Remarks"
                + " from H_EmployeeTransferApplication eta"
                + " INNER JOIN H_Employee e ON eta.H_EmployeeId=e.Id"
                + " INNER JOIN H_Designation desg ON eta.H_DesignationId=desg.Id"
                + " INNER JOIN H_EmployeeBranch eb ON eta.H_EmployeeId=eb.H_EmployeeId and eb.EndDate='2099-12-31'"
                + " INNER JOIN Branch b ON eb.BranchId=b.Id"
                + " INNER JOIN Region r ON b.RegionId=r.Id"
                + " INNER JOIN Subzone s ON r.SubZoneId=s.Id"
                + " INNER JOIN Thana th ON b.ThanaId=th.Id"
                + " INNER JOIN H_Address addr ON e.PermanentAddressId=addr.Id"
                + " INNER JOIN Thana wt ON addr.ThanaId=wt.Id"
                + " INNER JOIN District wd ON wt.DistrictId=wd.Id"
                + " where desg.Name LIKE '%" + txtDesignation.Text + "%'"
                + " AND s.Name LIKE '%" + txtAsaDistrict.Text + "%'"
                + " AND wt.Name LIKE '%" + txtOwnDistrict.Text + "%'"
                + " AND eta.DemandedPlace LIKE '%" + txtDemandedPlace.Text + "%'"
                + " AND eta.ApplicationNo LIKE '" + txtLetterNo.Text + "%'";

            if (!string.IsNullOrEmpty(txtEmpId.Text))
            {
                query += " AND e.Code IN (" + txtEmpId.Text + ")";
            }
            if (ddlStatus.SelectedValue != "0")
            {
                query += " AND eta.status=" + ddlStatus.SelectedValue;
            }

            query += SortExpression == "" ? "" : " ORDER BY " + SortExpression;
            DataSet ds = TransactionManager.GetDataSet(query);
            dataset = ds;
            Session.Remove("ReportTable");
            Session["ReportTable"] = ds.Tables[0];
            gvList.DataSource = ds.Tables[0];
            gvList.DataBind();
        }

        private void ViewReport()
        {
            if (dataset == null)
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "At First Load Data, then Export ";
                ShowUiMessage(msg);
                return;
            }
            IList<DataTable> tables = new List<DataTable>();
            IList<IList<WorksheetRow>> headers = new List<IList<WorksheetRow>>();
            WorksheetRow[][] header = new WorksheetRow[dataset.Tables.Count][];
            Int32 i = 0;
            DataSet ds = new DataSet();
            if (Session["ReportTable"] != null)
            {
                ds.Tables.Add((DataTable)Session["ReportTable"]);
            }
            foreach (DataTable dt in ds.Tables)
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
    }
}
