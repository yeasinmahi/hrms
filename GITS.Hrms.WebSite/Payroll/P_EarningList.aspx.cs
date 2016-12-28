using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Payroll
{
    public partial class P_EarningList : GridPage
    {
        protected override string PropertyName
        {
            get { return "P_EARNING LIST"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(P_Earning);
        }

        protected override string GetAddPageUrl()
        {
            return "P_EarningAdd.aspx";
        }
    }
}
