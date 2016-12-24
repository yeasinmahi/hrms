using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Asa.Hrms.Web;

namespace Asa.Hrms.WebSite.HRM
{
    public partial class H_EmployeeMultiLetterList : GridPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string GetAddPageUrl()
        {
            return "H_EmployeeMultiLetterAdd.aspx";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(Asa.Hrms.Data.Entity.H_EmployeeMultiLetter);
        }
    }
}
