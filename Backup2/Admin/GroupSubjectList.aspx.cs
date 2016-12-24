using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using Asa.Hrms.Web;
using Asa.Hrms.Data;

public partial class GroupSubjectList : GridPage
{
	protected override String PropertyName
	{
		get { return "GROUPSUBJECT LIST"; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
	}

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);

		this.GridView = this.gvList;
		this.EntityType = typeof(Asa.Hrms.Data.Entity.GroupSubject);
	}

	protected override String GetAddPageUrl()
	{
		return "GroupSubjectAdd.aspx";
	}

}
