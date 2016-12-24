using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Asa.Hrms.Web;

namespace Asa.Hrms.WebSite.Payroll
{
    public partial class P_StartupSalaryList : GridPage
    {
        protected override string PropertyName
        {
            get { return "P_STARTUPSALARY LIST"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(Asa.Hrms.Data.Entity.P_SalaryIncrement);
        }

        protected override string GetAddPageUrl()
        {
            return "P_StartupSalaryAdd.aspx";
        }
    }
}
