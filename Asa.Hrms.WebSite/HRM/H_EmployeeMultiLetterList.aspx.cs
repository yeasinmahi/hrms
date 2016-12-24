using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeMultiLetterList : GridPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string GetAddPageUrl()
        {
            return "H_EmployeeMultiLetterAdd.aspx";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(H_EmployeeMultiLetter);
        }
    }
}
