using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class BranchList : GridPage
    {
        protected override string PropertyName
        {
            get { return "BRANCH LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(BranchView);
            this.BaseEntityType = typeof(Branch);
        }

        protected override string GetAddPageUrl()
        {
            return "BranchAdd.aspx";
        }

    }
}
