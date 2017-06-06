using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class WF_DiseasesList : GridPage
    {
        protected override string PropertyName
        {
            get { return "WF_DISEASES LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            EntityType = typeof(WF_Diseases);
        }

        protected override string GetAddPageUrl()
        {
            return "WF_DiseasesAdd.aspx";
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

    }
}
