using System;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeTransferApplicationList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEETRANSFERAPPLICATION LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(H_EmployeeTransferApplicationView);
        }
        protected override string GetAddPageUrl()
        {
            return "H_EmployeeTransferApplicationAdd.aspx";
        }
    }
}
