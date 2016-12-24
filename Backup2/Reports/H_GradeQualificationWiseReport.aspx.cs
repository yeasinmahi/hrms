using System;
using Asa.Hrms.Web;
using Asa.Hrms.Utility;
using System.Data;
using Asa.ExcelXmlWriter;
using System.Collections.Generic;
using System.Linq;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.WebSite.Reports
{
    public partial class H_GradeQualificationWiseReport : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
                    default:
                        this.HandleSpecialCommand(sender, e);
                        break;
                }
            }
        }
        private void ViewReport()
        {
            string query = string.Empty;
            if (this.IsValid)
            {
                DataSet ds = Asa.Hrms.Data.Procedure.GradeQualificationWiseReport.GetDataSet(Convert.ToInt32(ddlGrade.SelectedValue),txtDesignation.Text,Convert.ToInt32(ddlMastersFilter.SelectedValue),DBUtility.ToNullableDateTime(txtMastersDate.Text),Convert.ToInt32(ddlBachelorFilter.SelectedValue),DBUtility.ToNullableDateTime(txtBachelorDate.Text),Convert.ToInt32(ddlHSCFilter.SelectedValue),DBUtility.ToNullableDateTime(txtHSCDate.Text),Convert.ToInt32(ddlBelowHSCFilter.SelectedValue),DBUtility.ToNullableDateTime(txtBelowHSCDate.Text));

                IList<DataTable> tables = new List<DataTable>();
                IList<IList<WorksheetRow>> headers = new List<IList<WorksheetRow>>();
                WorksheetRow[][] header = new WorksheetRow[ds.Tables.Count][];
                Int32 i = 0;

                foreach (DataTable dt in ds.Tables)
                {
                    tables.Add(dt);

                    header[i] = new WorksheetRow[4];
                    header[i][0] = new WorksheetRow();
                    header[i][1] = new WorksheetRow();
                    header[i][2] = new WorksheetRow();
                    header[i][3] = new WorksheetRow();

                    header[i][0].Cells.Add("Grade Qualification Wise Service length Report", DataType.String, "HeaderTop1").MergeAcross = dt.Columns.Count - 1;
                    header[i][1].Cells.Add("ASA Central office", DataType.String, "HeaderTop3").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2 - 1;
                    header[i][2].Cells.Add("", DataType.String, "HeaderTop3").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2 - 1;
                    header[i][2].Cells.Add("As On : " + DateTime.Today.ToShortDateString(), DataType.String, "HeaderTop4").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2;

                    foreach (DataColumn dc in dt.Columns)
                    {
                        header[i][3].Cells.Add((dc.ColumnName.Replace("_", " ")), DataType.String, "HeaderLeftAlign");
                    }

                    i++;
                }

                for (i = 0; i < headers.Count; i++)
                {
                    header[i] = headers[i].ToArray();
                }

                ExcelReportUtility.Instance.DataSource = tables.ToArray();
                ExcelReportUtility.Instance.Header = header;
                ExcelReportUtility.Instance.Name = "Grade_Qualification_Experience" + "(" + DateTime.Now.Ticks + ")" + Asa.Hrms.Utility.Configuration.ReportExtension;
                ExcelReportUtility.Instance.ViewReport();
            }
        }
        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        protected override Message Save()
        {
            throw new NotImplementedException();
        }
        protected override void LoadData()
        {
            ddlGrade.DataSource = H_Grade.FindAll("Name");
            ddlGrade.DataBind();
            System.Web.UI.WebControls.ListItem all=new  System.Web.UI.WebControls.ListItem("All","0",true);
            ddlGrade.Items.Insert(0,all);
        }

    }
}
