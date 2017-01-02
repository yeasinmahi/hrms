using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class H_DepartmentList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_DEPARTMENT LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(H_Department);
        }

        protected override string GetAddPageUrl()
        {
            return "H_DepartmentAdd.aspx";
        }

    }
}
