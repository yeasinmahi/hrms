using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using Asa.Hrms.Web;
using Asa.Hrms.Data;

public partial class DbTransactionList : GridPage
{
	protected override string PropertyName
	{
		get { return "DBTRANSACTION LIST"; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
	}

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);

		this.GridView = this.gvList;
		this.EntityType = typeof(Asa.Hrms.Data.Entity.DbTransaction);
	}

	protected override string GetAddPageUrl()
	{
		return "DbTransactionAdd.aspx";
	}

}
