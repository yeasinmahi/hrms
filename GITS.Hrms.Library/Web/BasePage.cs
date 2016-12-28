using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Security;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Web
{
    public abstract class BasePage : System.Web.UI.Page
    {
        protected Panel pnlMessage = null;
        protected Image imgMessage = null;
        protected Label lblMessage = null;
        private String _PropertyName;

        private TransactionManager _TransactionManager;

        protected virtual String PropertyName
        {
            get { return _PropertyName; }
        }

        protected TransactionManager TransactionManager
        {
            get { return _TransactionManager; }
            set { _TransactionManager = value; }
        }

        public BasePage()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Request.QueryString["propertyname"] != null)
            {
                this._PropertyName = Request.QueryString["propertyname"];
            }
            else if (this.PropertyName == null || this.PropertyName == "")
            {
                Property property = Property.Get("Path = '" + Request.Path.Substring(1) + "'");

                if (property != null)
                {
                    this._PropertyName = property.Name;
                }
            }

            pnlMessage = (Panel)Master.FindControl("pnlMessage");
            lblMessage = (Label)Master.FindControl("lblMessage");
            imgMessage = (Image)Master.FindControl("imgMessage");

            if (User.Identity.IsAuthenticated == false)
            {
                if (User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.SignOut();
                }

                FormsAuthentication.RedirectToLoginPage();
                Response.End();
            }

            Menu mnuPageToolbar = (Menu)Master.FindControl("mnuPageToolbar");
            mnuPageToolbar.MenuItemClick += new MenuEventHandler(ToolButtonClick);

            if (MmsPermissionProvider.HasPermission(User.Identity.Name, this.PropertyName) == false)
            {
                UIUtility.Transfer(Page, "/" + MmsPermissionProvider.PERMISSION_PAGE);
                return;
            }

            if (IsPostBack == false)
            {
                this.SetToolbar(mnuPageToolbar);
            }
        }

        protected abstract void HandleCommonCommand(object sender, MenuEventArgs e);

        protected void ToolButtonClick(object sender, MenuEventArgs e)
        {
            try
            {
                if (MmsPermissionProvider.HasPermission(User.Identity.Name, this.PropertyName, e.Item.Value) == false)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "You do not have permission to do this";
                    this.ShowUIMessage(msg);
                }
                else
                {
                    this.HandleCommonCommand(sender, e);
                }
            }
            catch (Exception ex)
            {
                if (this.TransactionManager != null)
                {
                    this.TransactionManager.Rollback();
                }

                this.ShowUIMessage(ex);
            }
        }

        private void SetToolbar(Menu mnuPageToolbar)
        {
            IList<PropertyCommandView> propertyCommands = PropertyCommandView.Find("PropertyName = '" + this.PropertyName + "'", "SortOrder");

            if (mnuPageToolbar.Items.Count > 0)
            {
                mnuPageToolbar.Items.Clear();
            }

            MenuItem tool = new MenuItem("", "", "~/Images/MenuBullet.gif", "");
            tool.Selectable = false;
            mnuPageToolbar.Items.Add(tool);

            if (propertyCommands != null)
            {
                foreach (PropertyCommandView propertyCommandView in propertyCommands)
                {
                    if (MmsPermissionProvider.HasPermission(User.Identity.Name, propertyCommandView.PropertyName, propertyCommandView.CommandName))
                    {
                        tool = new MenuItem("", propertyCommandView.CommandName, propertyCommandView.ImageUrl, propertyCommandView.NavigateUrl);

                        if (propertyCommandView.ImageUrl == null || propertyCommandView.ImageUrl == "")
                        {
                            tool.Text = propertyCommandView.DisplayName;
                        }
                        else
                        {
                            tool.Text = "&nbsp;" + propertyCommandView.DisplayName;
                        }

                        tool.SeparatorImageUrl = propertyCommandView.SeperatorUrl;
                        tool.ToolTip = propertyCommandView.ToolTipText;
                        mnuPageToolbar.Items.Add(tool);
                    }
                }

                mnuPageToolbar.DataBind();
            }

            if (mnuPageToolbar.Items.Count == 1)
            {
                mnuPageToolbar.Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.ShowUIMessage(new Message());
        }

        public void ShowUIMessage(Message msg)
        {
            switch (msg.Type)
            {
                case MessageType.Error:
                    lblMessage.Text = "Error: " + msg.Msg;
                    imgMessage.ImageUrl = "~/Images/error.gif";
                    pnlMessage.CssClass = "LabelErrorMessage";
                    break;
                case MessageType.Warning:
                    lblMessage.Text = "Warning: " + msg.Msg;
                    imgMessage.ImageUrl = "~/Images/warning.gif";
                    pnlMessage.CssClass = "LabelWarningMessage";
                    break;
                case MessageType.Information:
                    lblMessage.Text = msg.Msg;

                    if (msg.Msg == null || msg.Msg == "")
                    {
                        imgMessage.ImageUrl = "";
                        pnlMessage.CssClass = "LabelEmptyMessage";
                    }
                    else
                    {
                        imgMessage.ImageUrl = "~/Images/info.gif";
                        pnlMessage.CssClass = "LabelInformationMessage";
                    }
                    break;
            }
        }

        public void ShowUIMessage(Exception ex)
        {
            Message msg = new Message();
            msg.Type = MessageType.Error;

            if (ex is SqlException || (ex.InnerException != null && ex.InnerException is SqlException))
            {
                SqlException sqlEx = null;

                if (ex is SqlException)
                {
                    sqlEx = (SqlException)ex;
                }
                else
                {
                    sqlEx = (SqlException)ex.InnerException;
                }

                switch (sqlEx.Number)
                {
                    case 547:
                        if (sqlEx.Message.Contains("DELETE"))
                        {
                            msg.Msg = "You can not delete this record since there exits one or more dependent record(s).";
                        }
                        else if (sqlEx.Message.Contains("INSERT"))
                        {
                            msg.Msg = "You can not insert this record since one or more dependent record(s) do not exist.";
                        }
                        else if (sqlEx.Message.Contains("UPDATE"))
                        {
                            msg.Msg = "You can not update this record since there exits one or more dependent record(s).";
                        }
                        break;
                    case 2601:
                    case 2627:
                        msg.Msg = "A record with the same key already exists.";
                        break;
                    default:
                        msg.Msg = ex.Message;
                        break;
                }
            }
            else
            {
                if (ex is System.Reflection.TargetInvocationException && ex.InnerException != null)
                {
                    msg.Msg = ex.InnerException.Message;
                }
                else
                {
                    msg.Msg = ex.Message;
                }
            }

            this.ShowUIMessage(msg);
        }
    }
}
