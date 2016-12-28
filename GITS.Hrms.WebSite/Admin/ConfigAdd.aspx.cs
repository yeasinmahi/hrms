using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class ConfigAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "CONFIG ADD"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override string GetListPageUrl()
        {
            return "ConfigList.aspx";
        }

        private Config GetConfig()
        {
            Config config = null;

            if (this.Type == TYPE_EDIT)
            {
                config = Config.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                config = new Config();
            }

            config.Value = DBUtility.ToString(txtValue.Text);

            return config;
        }

        private new Message Validate()
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Record saved successfully.";

            base.Validate();

            if (base.IsValid == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid data provided or required data missing";
                return msg;
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                Config p_Config = this.GetConfig();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [Config]";
                }
                else
                {
                    desc = "Update [Config]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Config.Insert(this.TransactionManager, p_Config);

                    hdnId.Value = p_Config.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Config.Update(this.TransactionManager, p_Config);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            Config config = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                config = Config.GetById(Convert.ToInt32(hdnId.Value));

                if (config != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = config.Name;
                    txtReadableDataType.Text = config.ReadableDataType;
                    txtValue.Text = config.Value;
                }
            }
        }

        protected void cvValue_ServerValidate(object source, ServerValidateEventArgs args)
        {
            Config config = this.GetConfig();

            try
            {
                Convert.ChangeType(config.Value, System.Type.GetType(config.DataType));
            }
            catch
            {
                args.IsValid = false;
            }
        }
    }
}
