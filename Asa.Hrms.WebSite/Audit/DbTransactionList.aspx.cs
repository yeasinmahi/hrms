using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Audit
{
    public partial class DbTransactionList : GridPage
    {
        protected override string PropertyName
        {
            get { return "DBTRANSACTION LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(DbTransaction);
        }

        protected override string GetAddPageUrl()
        {
            return "DbTransactionAdd.aspx";
        }

    }
}
