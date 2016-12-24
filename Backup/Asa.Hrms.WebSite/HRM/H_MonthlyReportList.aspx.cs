using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Asa.Hrms.Web;
using Asa.Hrms.Data;

namespace Asa.Hrms.WebSite.HRM
{
    public partial class H_MonthlyReportList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_MONTHLYREPORT LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(Asa.Hrms.Data.Entity.H_MonthlyReport);

        }

        protected override string GetAddPageUrl()
        {
            return "H_MonthlyReportAdd.aspx";
        }
    }
}
