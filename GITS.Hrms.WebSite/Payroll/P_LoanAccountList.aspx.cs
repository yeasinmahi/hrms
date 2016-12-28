using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Payroll
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
            this.BaseEntityType = typeof(P_LoanAccount);
            this.EntityType = typeof(P_LoanAccountView);
        }

        protected override string GetAddPageUrl()
        {
            return "P_LoanAccountAdd.aspx";
        }
    }
}
