using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class ExamNameList : GridPage
    {
        protected override String PropertyName
        {
            get { return "EXAMNAME LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(ExamName);
        }

        protected override String GetAddPageUrl()
        {
            return "ExamNameAdd.aspx";
        }

    }
}
