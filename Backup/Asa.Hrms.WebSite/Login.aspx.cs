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
using Asa.Hrms.WebSite.Code.Manager.DbManagement;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DatabaseManagementManager dmm = new DatabaseManagementManager();

        if (dmm.CheckDatabase() == false)
        {
            UIUtility.Transfer(Page, "DbManagement/DbInstall.aspx");
        }

        if (IsPostBack == false && Request.Cookies["ASA_HRMS"] != null)
        {
            ((TextBox)login.Controls[0].FindControl("UserName")).Text = Server.HtmlEncode(Request.Cookies["ASA_HRMS"].Values["UserName"]);
        }

        //#if (DEBUG)
        //        FormsAuthentication.RedirectFromLoginPage("admin", true);
        //#endif
    }

    protected void login_LoggingIn(object sender, LoginCancelEventArgs e)
    {
        DatabaseManagementManager dmm = new DatabaseManagementManager();

        if (dmm.CheckDatabase() == false)
        {
            UIUtility.Transfer(Page, "DbManagement/DbInstall.aspx");
            e.Cancel = true;
        }
        else
        {
            Installation incomplete = Installation.Get("Status='" + Installation.STATUS_INCOMPLETE + "'");
            Installation installation = Installation.Get("Version='" + Asa.Hrms.Utility.Configuration.Version + "'");

            HtmlTableCell label = (HtmlTableCell)login.FindControl("label");

            if (incomplete != null || installation == null)
            {
                label.InnerText = installation == null ? "Version mismatch!" : "Incomplete installation!";
                e.Cancel = true;
            }
            else
            {
                label.InnerText = "";

                HttpCookie cookie = new HttpCookie("ASA_HRMS");
                cookie.Values.Add("UserName", ((TextBox)login.Controls[0].FindControl("UserName")).Text);
                Response.Cookies.Remove("ASA_HRMS");
                Response.Cookies.Add(cookie);
            }
        }
    }
}