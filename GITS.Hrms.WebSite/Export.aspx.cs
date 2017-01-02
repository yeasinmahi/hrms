using System;
using System.IO;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.WebSite
{
    public partial class Export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UserHostAddress != "127.0.0.1")
            {
                UIUtility.Transfer(Page, "Permission.aspx");
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
