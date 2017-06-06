using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Security;

namespace GITS.Hrms.WebSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                if (User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                }

                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                User usr = Library.Data.Entity.User.GetByLogin(User.Identity.Name);
                if (!usr.IsReset)
                {
                    Response.Redirect("~/ChangePassword.aspx");
                    return;
                }
                if (IsPostBack == false)
                {
                    lblUserName.InnerText = User.Identity.Name;
                    BuildMenu();
                }
            }
        }

        private void BuildMenu()
        {
            IList<Module> modules = Module.Find("IsActive = 'True'", "SortOrder");
            mnuTitleMenubar.Items.Clear();

            if (modules != null)
            {
                foreach (Module module in modules)
                {
                    if (UserPropertyView.Find("ModuleName = '" + module.Name + "' AND UserLogin = '" + User.Identity.Name + "'", "").Count > 0)
                    {
                        MenuItem item = new MenuItem();
                        item.Value = module.Name;
                        item.Text = @"<span class='hideText'>&nbsp;" + module.DisplayName + @"</span>";
                        item.ImageUrl = module.ImageUrl;
                        mnuTitleMenubar.Items.Add(item);
                    }
                }
            }

            mnuTitleMenubar.DataBind();

            if (mnuTitleMenubar.Items.Count > 0)
            {
                mnuTitleMenubar.Items[0].Selected = true;
                mnuTitleMenubar_MenuItemClick(mnuTitleMenubar, new MenuEventArgs(mnuTitleMenubar.Items[0]));
            }

            //this.divMenu.Style["height"] = 528 - this.mnuTitleMenubar.Items.Count * 30 + "px";
        }

        protected void mnuTitleMenubar_MenuItemClick(object sender, MenuEventArgs e)
        {
            IList<Property> properties = Property.Find("IsActive = 'True' AND ModuleName ='" + e.Item.Value + "'", "SortOrder");

            mnuMenubar.Items.Clear();

            if (properties != null)
            {
                foreach (Property property in properties)
                {
                    if (MmsPermissionProvider.HasPermission(User.Identity.Name, property.Name))
                    {
                        MenuItem item = new MenuItem();
                        item.Value = property.Name;
                        item.Text = property.DisplayName;
                        item.PopOutImageUrl = property.Path;
                        item.ImageUrl = property.ImageUrl;
                        mnuMenubar.Items.Add(item);
                    }
                }
            }

            mnuMenubar.DataBind();
            Image1.ImageUrl = e.Item.ImageUrl;

            if (mnuMenubar.Items.Count > 0)
            {
                mnuMenubar.Items[0].Selected = true;
                mnuMenubar_MenuItemClick(mnuMenubar, new MenuEventArgs(mnuMenubar.Items[0]));
            }
        }

        protected void mnuMenubar_MenuItemClick(object sender, MenuEventArgs e)
        {
            lblNavigationTitle.Text = e.Item.Text;
            if (MmsPermissionProvider.HasPermission(User.Identity.Name, e.Item.Value) == false)
            {
                ifPage.Attributes["src"] = MmsPermissionProvider.PERMISSION_PAGE;
            }
            else
            {
                ifPage.Attributes["src"] = e.Item.PopOutImageUrl + "?propertyname=" + e.Item.Value;
            }
        }
    }
}
