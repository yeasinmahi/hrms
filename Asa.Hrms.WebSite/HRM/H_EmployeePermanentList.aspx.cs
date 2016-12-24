using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeePermanentList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEPERMANENT LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        { }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.BaseEntityType = typeof(H_EmployeePromotionHistory);
            this.EntityType = typeof(H_EmployeePermanentHistoryView);
        }

        protected override string GetAddPageUrl()
        {
            return "H_EmployeePermanentAdd.aspx";
        }
    }
}
