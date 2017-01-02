using System;
using System.Configuration;
using System.Web;
using GITS.Hrms.Library.Data.Entity;

namespace GITS.Hrms.WebSite.HRM
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
   //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DownloadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String filePath = ConfigurationManager.AppSettings["FileUploadPath"].ToString();
            System.Web.HttpRequest request = System.Web.HttpContext.Current.Request;
            string id = request.QueryString["Id"];
            H_FileUpload file = H_FileUpload.GetById(Convert.ToInt32(id));
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = GetMimeType(file.FileName);// "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=" + file.Title.ToString().Replace(" ","_") + ";");
            //response.TransmitFile(System.Web.HttpContext.Current.Server.MapPath("~/Images/Temp/"+file.FileName));
            response.TransmitFile(filePath + file.FileName);
            response.Flush();
            response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}
