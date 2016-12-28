using System;
using System.Web;
using System.Web.Security;

namespace GITS.Hrms.WebSite
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                }

                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
}
