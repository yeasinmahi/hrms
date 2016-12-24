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
using System.IO;

namespace Asa.Hrms.WebSite
{
    public partial class Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UserHostAddress != "127.0.0.1")
            {
                Utility.UIUtility.Transfer(Page, "Permission.aspx");
            }
            else
            {
                FileInfo fileInfo = new FileInfo(Request.QueryString["path"]);

                Response.Clear();
                Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileInfo.Name));
                Response.ContentType = "application/unknown";
                Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                Response.TransmitFile(Request.QueryString["path"]);
                Response.Flush();
                Response.Clear();

                File.Delete(Request.QueryString["path"]);
            }
        }
    }
}
