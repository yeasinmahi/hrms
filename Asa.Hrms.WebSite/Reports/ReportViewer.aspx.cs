using System;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace GITS.Hrms.WebSite.Reports
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        private ReportDocument report = null;
        private ParameterFields parameters = null;
        private string printMode = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            report = (ReportDocument)Session["ReportDocument"];
            TextObject txtnote = (TextObject)report.ReportDefinition.ReportObjects["txtnote"];
            txtnote.Text = (string)Session["note"];
            if (string.IsNullOrEmpty((string)Session["note"]))
            {
                txtnote.Border.BorderColor = System.Drawing.Color.White;
            }
            //Session.Remove("note");

            report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "HRM_Report");
            parameters = (ParameterFields)Session["ParameterFields"];
            printMode = (string)Session["PrintMode"];
            bool showGroupTree = Session["ShowGroupTree"] == null ? false : Convert.ToBoolean(Session["ShowGroupTree"].ToString());

            if (report != null)
            {
                // show report
                crViewer.ReportSource = report;
                crViewer.ParameterFieldInfo = parameters;

                if (showGroupTree)
                {
                    crViewer.HasToggleGroupTreeButton = true;
                    crViewer.DisplayGroupTree = true;
                }
                else
                {
                    crViewer.HasToggleGroupTreeButton = false;
                    crViewer.DisplayGroupTree = false;
                }

                crViewer.DataBind();
                crViewer.BackColor = System.Drawing.Color.White;
            }

            // reset print mode....
            Session["PrintMode"] = null;
        }

        protected void crViewer_AfterRender(object source, CrystalDecisions.Web.HtmlReportRender.AfterRenderEvent e)
        {
            if (report != null)
            {
                if (printMode == "Export")
                {
                    // export report

                    // Stop buffering the response
                    Response.Buffer = false;

                    // Clear the response content and headers
                    Response.ClearContent();
                    Response.ClearHeaders();

                    try
                    {
                        // Export the Report to Response stream in PDF format and file name Customers
                        report.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Report");
                    }
                    catch (Exception ex)
                    {
                        //ShowUIMessage(ex.Message, 1);
                        ex = null;
                    }
                }
            }
        }
    }
}
