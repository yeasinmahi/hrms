using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Payroll
{
    public partial class P_DeductionList : GridPage
    {
        protected override string PropertyName
        {
            get { return "P_DEDUCTION LIST"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            EntityType = typeof(P_Deduction);
        }

        protected override string GetAddPageUrl()
        {
            return "P_DeductionAdd.aspx";
        }
    }
}
