using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.WebSite.Payroll
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
            this.EntityType = typeof(Asa.Hrms.Data.Entity.P_Earning);
        }

        protected override string GetAddPageUrl()
        {
            return "P_EarningAdd.aspx";
        }
    }
}
