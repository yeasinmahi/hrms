using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Asa.Hrms.WebSite.Reports
{
    public partial class ViewerUI : System.Web.UI.Page
    {
        private ReportDocument report = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 ReportType = Convert.ToInt32(Session["ReportType"]);
            System.IO.MemoryStream oStream = new System.IO.MemoryStream(); 
            report = (ReportDocument)Session["ReportDocument"];
            if (ReportType == 1)
            {
                report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "ASA_Report_"+DateTime.Today.ToString("yyyyMMdd")+"_"+DateTime.Now.ToString("hhmmss"));
            }
            else if (ReportType == 2)
            {
                report.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, false, "ASA_Report_" + DateTime.Today.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hhmmss"));
            }
            else
            {
                report.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "ASA_Report_" + DateTime.Today.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hhmmss"));
            }
            Session.Remove("ReportType");
            //report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            CrystalReportViewer1.ReportSource = report;
            

        }
    }
}
