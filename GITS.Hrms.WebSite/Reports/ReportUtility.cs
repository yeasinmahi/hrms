using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace GITS.Hrms.WebSite.Reports
{
    /// <summary>
    /// Common functions required to generate report by crystal report
    /// </summary>
    public class ReportUtility
    {
        public ReportUtility()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // make a report parameter
        public static ParameterField GetParameter(string name, object value)
        {
            ParameterField param = new ParameterField();
            param.Name = name;

            ParameterDiscreteValue parameterValue = new ParameterDiscreteValue();
            parameterValue.Value = value;
            param.CurrentValues.Add(parameterValue);

            return param;
        }

        // show report in report viewer of crystal
        public static void ShowReport(Page page, ParameterFields parameters, ReportDocument document, bool autoExport, bool showGroupTree)
        {
            // set parameter & report into session
            page.Session["ParameterFields"] = parameters;
            page.Session["ReportDocument"] = document;

            if (showGroupTree)
                page.Session["ShowGroupTree"] = "true";
            else
                page.Session["ShowGroupTree"] = "false";

            string script = "";

            // view report by running client script
            if (autoExport)
            {
                page.Session["PrintMode"] = "Export";
                script = "<script language=javascript>OpenWindow('../Reports/ReportViewer.aspx', 27, 0, 0, 0);</script>";
            }
            else
            {
                page.Session["PrintMode"] = "Print";
                //script = "<script language=javascript>OpenWindow('../Reports/ReportViewer.aspx', 27, 0, 0, 0);</script>";
                script = "<script language=javascript>window.open('/Reports/ReportViewer.aspx');</script>";
            }

            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "LoadPage", script);
            //page.RegisterStartupScript("Report", script);
            //page.Response.Redirect("~/Reports/ReportViewer.aspx");
        }
        public static void ViewReport(Page page, ReportDocument document, bool autoExport, bool showGroupTree)
        {
            page.Session["ReportDocument"] = document;
            string script = "";

            script = "<script language=javascript>window.open('/Reports/ViewerUI.aspx');</script>";
       
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "LoadPage", script);
        }
    }
}