using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class BoardUniversityList : GridPage
    {
        protected override String PropertyName
        {
            get { return "BOARDUNIVERSITY LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            EntityType = typeof(BoardUniversity);
        }

        protected override String GetAddPageUrl()
        {
            return "BoardUniversityAdd.aspx";
        }

    }
}
