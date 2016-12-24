using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Data;
using Asa.Hrms.Data.Entity;
using System.IO;
using System.Web.UI;
using Asa.Hrms.Data.View;
using System.Collections.Generic;
using Asa.Hrms.Utility;

public partial class RoleList : Asa.Hrms.Web.GridPage
{
    protected override string PropertyName
    {
        get { return "ROLE LIST"; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.GridView = this.gvList;
        this.EntityType = typeof(Role);
        this.SortColumn = "Name";
        this.SortOrder = "ASC";
    }

	protected void Page_Load(object sender, EventArgs e)
	{
	}

	protected override string GetAddPageUrl()
	{
		return "RoleAdd.aspx";
	}

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            BulletedList blUser = (BulletedList)e.Row.FindControl("blUser");
            blUser.DataSource = UserRole.FindByRoleName(((Role)e.Row.DataItem).Name, "UserLogin");
            blUser.DataBind();
        }
    }
}
