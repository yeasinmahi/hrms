using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeDropList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEDROP LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        { }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
           // this.gvspList.WhereClause = this.gvspList.WhereClause + " Status=1";
            this.GridView = this.gvList;
            this.BaseEntityType = typeof(H_EmployeeDropHistory);
            this.EntityType = typeof(H_EmployeeDropHistoryView);
        }

        protected override string GetAddPageUrl()
        {
            return "H_EmployeeDropAdd.aspx";
        }
    }
}
