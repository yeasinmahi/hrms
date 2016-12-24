using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Security;

public partial class H_EmployeeTransferList : GridPage
{
	protected override string PropertyName
	{
		get { return "H_EMPLOYEETRANSFER LIST"; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
         if (!MmsPermissionProvider.HasPermission(User.Identity.Name, this.PropertyName, "SEARCH")) // Search => Transfer Letter Menu
         {
             gvList.Columns[1].Visible = false;
         }
	}

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);

		this.GridView = this.gvList;
		this.EntityType = typeof(Asa.Hrms.Data.View.H_EmployeeTransferView);
	}

    protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
    {
        switch (e.Item.Value)
        {

            case "SEARCH":
                Asa.Hrms.Utility.UIUtility.Transfer(Page, "H_TransferAdd.aspx");
                break;
            default:
                this.HandleSpecialCommand(sender, e);
                break;

        }

    }
	protected override string GetAddPageUrl()
	{
		return "H_EmployeeTransferAdd.aspx";
	}

}
