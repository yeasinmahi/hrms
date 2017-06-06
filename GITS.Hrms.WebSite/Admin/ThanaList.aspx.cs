using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class ThanaList : GridPage
    {
        protected override string PropertyName
        {
            get { return "THANA LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            EntityType = typeof(ThanaView);
            BaseEntityType = typeof(Thana);
        }

        protected override string GetAddPageUrl()
        {
            return "ThanaAdd.aspx";
        }

    }
}
