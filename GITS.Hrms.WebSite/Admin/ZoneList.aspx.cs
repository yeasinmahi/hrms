using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class ZoneList : GridPage
    {
        protected override string PropertyName
        {
            get { return "ZONE LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            EntityType = typeof(Zone);
        }

        protected override string GetAddPageUrl()
        {
            return "ZoneAdd.aspx";
        }

    }
}
