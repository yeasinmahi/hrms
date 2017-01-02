using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class RegionList : GridPage
    {
        protected override string PropertyName
        {
            get { return "REGION LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(RegionView);
            this.BaseEntityType = typeof(Region);
        }

        protected override string GetAddPageUrl()
        {
            return "RegionAdd.aspx";
        }

    }
}
