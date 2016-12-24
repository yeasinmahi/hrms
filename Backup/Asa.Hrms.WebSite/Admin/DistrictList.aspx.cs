using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using Asa.Hrms.Web;
using Asa.Hrms.Data;

public partial class DistrictList : GridPage
{
	protected override string PropertyName
	{
		get { return "DISTRICT LIST"; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
	}

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);

		this.GridView = this.gvList;
		this.EntityType = typeof(Asa.Hrms.Data.Entity.District);
	}

	protected override string GetAddPageUrl()
	{
		return "DistrictAdd.aspx";
	}

}
