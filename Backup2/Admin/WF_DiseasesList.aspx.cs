using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.WebSite.Admin
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

            this.GridView = this.gvList;
            this.EntityType = typeof(Asa.Hrms.Data.Entity.WF_Diseases);
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
