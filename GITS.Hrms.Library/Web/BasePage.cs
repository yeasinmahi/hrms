using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Properties;
using GITS.Hrms.Library.Security;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Web
{
    public abstract class BasePage : System.Web.UI.Page
    {
        protected Panel PnlMessage;
        protected Image ImgMessage;
        protected Label LblMessage;
        private String _propertyName;

        protected virtual String PropertyName
        {
            get { return _propertyName; }
        }

        protected TransactionManager TransactionManager { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Request.QueryString["propertyname"] != null)
            {
                _propertyName = Request.QueryString["propertyname"];
            }
            else if (string.IsNullOrEmpty(PropertyName))
            {
                Property property = Property.Get("Path = '" + Request.Path.Substring(1) + "'");

                if (property != null)
                {
                    _propertyName = property.Name;
                }
            }

            if (Master != null)
            {
                PnlMessage = (Panel)Master.FindControl("pnlMessage");
                LblMessage = (Label)Master.FindControl("lblMessage");
                ImgMessage = (Image)Master.FindControl("imgMessage");

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
                mnuPageToolbar.MenuItemClick += ToolButtonClick;

                if (MmsPermissionProvider.HasPermission(User.Identity.Name, PropertyName) == false)
                {
                    UIUtility.Transfer(Page, "/" + MmsPermissionProvider.PERMISSION_PAGE);
                    return;
                }

                if (IsPostBack == false)
                {
                    SetToolbar(mnuPageToolbar);
                }
            }
        }

        protected abstract void HandleCommonCommand(object sender, MenuEventArgs e);

        protected void ToolButtonClick(object sender, MenuEventArgs e)
        {
            try
            {
                if (MmsPermissionProvider.HasPermission(User.Identity.Name, PropertyName, e.Item.Value) == false)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "You do not have permission to do this";
                    ShowUiMessage(msg);
                }
                else
                {
                    HandleCommonCommand(sender, e);
                }
            }
            catch (Exception ex)
            {
                if (TransactionManager != null)
                {
                    TransactionManager.Rollback();
                }

                ShowUiMessage(ex);
            }
        }

        private void SetToolbar(Menu mnuPageToolbar)
        {
            IList<PropertyCommandView> propertyCommands = PropertyCommandView.Find("PropertyName = '" + PropertyName + "'", "SortOrder");

            if (mnuPageToolbar.Items.Count > 0)
            {
                mnuPageToolbar.Items.Clear();
            }
//
//            MenuItem tool = new MenuItem("", "", "~/Images/MenuBullet.gif", "");
//            tool.Selectable = false;
//            mnuPageToolbar.Items.Add(tool);
            if (propertyCommands != null)
            {
                foreach (PropertyCommandView propertyCommandView in propertyCommands)
                {
                    if (MmsPermissionProvider.HasPermission(User.Identity.Name, propertyCommandView.PropertyName, propertyCommandView.CommandName))
                    {
                        MenuItem tool = new MenuItem("", propertyCommandView.CommandName, propertyCommandView.ImageUrl, propertyCommandView.NavigateUrl);

                        if (string.IsNullOrEmpty(propertyCommandView.ImageUrl))
                        {
                            tool.Text = propertyCommandView.DisplayName;
                        }
                        else
                        {
                            tool.Text = Resources.Space + propertyCommandView.DisplayName;
                        }

                        //tool.SeparatorImageUrl = propertyCommandView.SeperatorUrl;
                        tool.ToolTip = propertyCommandView.ToolTipText;
                        mnuPageToolbar.Items.Add(tool);
                    }
                }

                mnuPageToolbar.DataBind();
            }

            if (mnuPageToolbar.Items.Count == 0)
            {
                mnuPageToolbar.Visible = false;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ShowUiMessage(new Message());
        }

        public void ShowUiMessage(Message msg)
        {
            switch (msg.Type)
            {
                case MessageType.Error:
                    LblMessage.Text = @"Error: " + msg.Msg;
                    ImgMessage.ImageUrl = "~/Images/error.gif";
                    PnlMessage.CssClass = "LabelErrorMessage";
                    break;
                case MessageType.Warning:
                    LblMessage.Text = @"Warning: " + msg.Msg;
                    ImgMessage.ImageUrl = "~/Images/warning.gif";
                    PnlMessage.CssClass = "LabelWarningMessage";
                    break;
                case MessageType.Information:
                    LblMessage.Text = msg.Msg;

                    if (string.IsNullOrEmpty(msg.Msg))
                    {
                        ImgMessage.ImageUrl = "";
                        PnlMessage.CssClass = "LabelEmptyMessage";
                    }
                    else
                    {
                        ImgMessage.ImageUrl = "~/Images/info.gif";
                        PnlMessage.CssClass = "LabelInformationMessage";
                    }
                    break;
            }
        }

        public void ShowUiMessage(Exception ex)
        {
            Message msg = new Message();
            msg.Type = MessageType.Error;

            if (ex is SqlException || (ex.InnerException is SqlException))
            {
                SqlException sqlEx;

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

            ShowUiMessage(msg);
        }
    }
}
