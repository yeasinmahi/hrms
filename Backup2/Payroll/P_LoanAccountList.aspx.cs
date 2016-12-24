using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Security;

namespace Asa.Hrms.WebSite.Payroll
{
    public partial class P_LoanAccountList : GridPage
    {
        protected override string PropertyName
        {
            get { return "P_LOANACCOUNT LIST"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.BaseEntityType = typeof(Asa.Hrms.Data.Entity.P_LoanAccount);
            this.EntityType = typeof(Asa.Hrms.Data.View.P_LoanAccountView);
        }

        protected override string GetAddPageUrl()
        {
            return "P_LoanAccountAdd.aspx";
        }
    }
}
