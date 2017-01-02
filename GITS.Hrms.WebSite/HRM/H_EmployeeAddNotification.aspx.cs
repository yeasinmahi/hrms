using System;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeAddNotification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HRM/H_EmployeeAdd.aspx");
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HRM/H_EmployeeList.aspx");
        }
    }
}
