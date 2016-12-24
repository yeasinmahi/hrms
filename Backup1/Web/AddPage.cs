using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Asa.Hrms.Utility;

namespace Asa.Hrms.Web
{
    public abstract class AddPage : BasePage
    {
        protected const string COMMAND_SAVE = "SAVE";
        protected const string COMMAND_SAVE_AND_CLOSE = "SAVE AND CLOSE";
        protected const string COMMAND_SAVE_AND_NEW = "SAVE AND NEW";
        protected const string COMMAND_SEARCH = "SEARCH";
        protected const string COMMAND_PRINT = "PRINT";
        protected const string COMMAND_CANCEL = "CANCEL";

        protected const string TYPE_ADD = "ADD";
        protected const string TYPE_EDIT = "EDIT";

        protected HiddenField hdnId = null;

        protected string Type
        {
            get
            {
                if (ViewState["_Type"] == null)
                {
                    ViewState["_Type"] = TYPE_ADD;
                }

                return ViewState["_Type"].ToString();
            }
            set
            {
                ViewState["_Type"] = value;
            }
        }

        public AddPage()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.hdnId = (HiddenField)Master.FindControl("hdnId");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack == false)
            {
                LoadData();
            }
        }

        protected abstract void LoadData();

        protected virtual void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected abstract string GetListPageUrl();
        protected abstract Message Save();

        protected virtual void Search()
        {
            throw new NotImplementedException();
        }

        protected virtual void PrintData()
        {
            throw new NotImplementedException();
        }

        protected override void HandleCommonCommand(object sender, MenuEventArgs e)
        {
            Message msg = new Message();

            switch (e.Item.Value)
            {
                case COMMAND_SAVE:
                    msg = this.Save();
                    this.ShowUIMessage(msg);
                    break;
                case COMMAND_SAVE_AND_CLOSE:
                    msg = this.Save();
                    this.ShowUIMessage(msg);

                    if (msg.Type == MessageType.Information)
                    {
                        UIUtility.Transfer(Page, this.GetListPageUrl());
                    }
                    break;
                case COMMAND_SAVE_AND_NEW:
                    msg = this.Save();
                    this.ShowUIMessage(msg);

                    if (msg.Type == MessageType.Information)
                    {
                        UIUtility.Transfer(Page, Request.Path);
                    }
                    break;
                case COMMAND_CANCEL:
                    UIUtility.Transfer(Page, this.GetListPageUrl());
                    break;
                case COMMAND_SEARCH:
                    this.Search();
                    break;
                case COMMAND_PRINT:
                    this.PrintData();
                    break;
                default:
                    this.HandleSpecialCommand(sender, e);
                    break;

            }
        }
    }
}
