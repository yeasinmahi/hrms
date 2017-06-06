using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Security;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeTransferList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEETRANSFER LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!MmsPermissionProvider.HasPermission(User.Identity.Name, PropertyName, "SEARCH")) // Search => Transfer Letter Menu
            {
                gvList.Columns[1].Visible = false;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            EntityType = typeof(H_EmployeeTransferView);
        }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {

                case "SEARCH":
                    UIUtility.Transfer(Page, "H_TransferAdd.aspx");
                    break;
                default:
                    HandleSpecialCommand(sender, e);
                    break;

            }

        }
        protected override string GetAddPageUrl()
        {
            return "H_EmployeeTransferAdd.aspx";
        }

    }
}
