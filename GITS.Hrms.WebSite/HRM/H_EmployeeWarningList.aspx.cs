﻿using System;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeWarningList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEWARNING LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        { }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            EntityType = typeof(H_EmployeeWarning);
        }

        protected override string GetAddPageUrl()
        {
            return "H_EmployeeWarningAdd.aspx";
        }
    }
}
