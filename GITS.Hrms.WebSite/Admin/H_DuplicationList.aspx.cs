using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class H_DuplicationList : GridPage
    {
        protected override string PropertyName
        {
            get { return "LETTERDUPLICATION LIST"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            SortColumn = "SortOrder";
            this.GridView = this.gvList;
            this.EntityType = typeof(H_Duplication);
        }
        protected override string GetAddPageUrl()
        {
            return "H_DuplicationAdd.aspx";
        }
    }
}
