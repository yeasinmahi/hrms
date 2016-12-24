using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Asa.Hrms.Web;
using Asa.Hrms.Data;


public partial class P_LateAttendanceList : GridPage
{
    protected override string PropertyName
    {
        get { return "LATEATTENDANCE LIST"; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.GridView = this.gvList;
        this.BaseEntityType = typeof(Asa.Hrms.Data.Entity.P_LateAttendance);
        this.EntityType = typeof(Asa.Hrms.Data.View.P_LateAttendanceView);

    }

    protected override string GetAddPageUrl()
    {
        return "P_LateAttendanceAdd.aspx";
    }
}

