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
                    this.lblUserName.Text = "User: " + User.Identity.Name;
                    this.BuildMenu();
                }
            }
        }

        private void BuildMenu()
        {
            IList<Module> modules = Module.Find("IsActive = 'True'", "SortOrder");
            this.mnuTitleMenubar.Items.Clear();

            if (modules != null)
            {
                foreach (Module module in modules)
                {
                    if (UserPropertyView.Find("ModuleName = '" + module.Name + "' AND UserLogin = '" + User.Identity.Name + "'", "").Count > 0)
                    {
                        MenuItem item = new MenuItem();
                        item.Value = module.Name;
                        item.Text = "&nbsp;" + module.DisplayName;
                        item.ImageUrl = module.ImageUrl;
                        this.mnuTitleMenubar.Items.Add(item);
                    }
                }
            }

            this.mnuTitleMenubar.DataBind();

            if (this.mnuTitleMenubar.Items.Count > 0)
            {
                this.mnuTitleMenubar.Items[0].Selected = true;
                this.mnuTitleMenubar_MenuItemClick(this.mnuTitleMenubar, new MenuEventArgs(this.mnuTitleMenubar.Items[0]));
            }

            //this.divMenu.Style["height"] = 528 - this.mnuTitleMenubar.Items.Count * 30 + "px";
        }

        protected void mnuTitleMenubar_MenuItemClick(object sender, MenuEventArgs e)
        {
            IList<Property> properties = Property.Find("IsActive = 'True' AND ModuleName ='" + e.Item.Value + "'", "SortOrder");

            this.mnuMenubar.Items.Clear();

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
                        this.mnuMenubar.Items.Add(item);
                    }
                }
            }

            this.mnuMenubar.DataBind();

            this.lblNavigationTitle.Text = e.Item.Text;
            this.Image1.ImageUrl = e.Item.ImageUrl;

            if (this.mnuMenubar.Items.Count > 0)
            {
                this.mnuMenubar.Items[0].Selected = true;
                this.mnuMenubar_MenuItemClick(this.mnuMenubar, new MenuEventArgs(this.mnuMenubar.Items[0]));
            }
        }

        protected void mnuMenubar_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (MmsPermissionProvider.HasPermission(User.Identity.Name, e.Item.Value) == false)
            {
                this.ifPage.Attributes["src"] = MmsPermissionProvider.PERMISSION_PAGE;
            }
            else
            {
                this.ifPage.Attributes["src"] = e.Item.PopOutImageUrl + "?propertyname=" + e.Item.Value;
            }
        }
    }
}
