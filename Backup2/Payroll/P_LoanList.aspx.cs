using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.WebSite
{
    public partial class P_LoanList : GridPage
    {
        protected override string PropertyName
        {
            get { return "P_LOAN LIST"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(Asa.Hrms.Data.Entity.P_Loan);
        }

        protected override string GetAddPageUrl()
        {
            return "P_LoanAdd.aspx";
        }
    }
}
