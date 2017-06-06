using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeRejoinList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEREJOIN LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        { }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            BaseEntityType = typeof(H_EmployeeRejoinHistory);
            EntityType = typeof(H_EmployeeRejoinHistoryView);
        }

        protected override string GetAddPageUrl()
        {
            return "H_EmployeeRejoinAdd.aspx";
        }
    }
}
