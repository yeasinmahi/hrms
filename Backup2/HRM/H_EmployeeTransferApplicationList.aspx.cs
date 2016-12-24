using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Asa.Hrms.Web;

namespace Asa.Hrms.WebSite.HRM
{
    public partial class H_EmployeeTransferApplicationList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEETRANSFERAPPLICATION LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(Asa.Hrms.Data.View.H_EmployeeTransferApplicationView);
        }
        protected override string GetAddPageUrl()
        {
            return "H_EmployeeTransferApplicationAdd.aspx";
        }
    }
}
