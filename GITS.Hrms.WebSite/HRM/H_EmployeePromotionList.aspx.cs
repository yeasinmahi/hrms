using System;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeePromotionList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEPROMOTION LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            EntityType = typeof(H_EmployeePromotionHistoryView);
        }

        protected override string GetAddPageUrl()
        {
            return "H_EmployeePromotionAdd.aspx";
        }

    }
}
