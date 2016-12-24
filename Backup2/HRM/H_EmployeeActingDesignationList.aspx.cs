using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.WebSite.HRM
{
    public partial class H_EmployeeActingDesignationList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEACTINGDESIGNATION LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        { }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(Asa.Hrms.Data.Entity.H_EmployeeActingDesignation);
        }

        protected override string GetAddPageUrl()
        {
            return "H_EmployeeActingDesignationAdd.aspx";
        }
    }
}
