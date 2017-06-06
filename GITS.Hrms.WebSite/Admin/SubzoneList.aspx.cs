using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class SubzoneList : GridPage
    {
        protected override string PropertyName
        {
            get { return "SUBZONE LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            EntityType = typeof(SubzoneView);
            BaseEntityType = typeof(Subzone);
        }

        protected override string GetAddPageUrl()
        {
            return "SubzoneAdd.aspx";
        }

    }
}
