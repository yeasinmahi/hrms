using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class ThanaAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "THANA ADD"; }
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
            return "ThanaList.aspx";
        }

        private Thana GetThana()
        {
            Thana thana = null;

            if (this.Type == TYPE_EDIT)
            {
                thana = Thana.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                thana = new Thana();
            }

            thana.DistrictId = DBUtility.ToInt32(ddlDistrictId.SelectedValue);
            thana.Name = DBUtility.ToString(txtName.Text);

            return thana;
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
                Thana thana = this.GetThana();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [Thana]";
                }
                else
                {
                    desc = "Update [Thana]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Thana.Insert(this.TransactionManager, thana);

                    hdnId.Value = thana.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Thana.Update(this.TransactionManager, thana);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            this.ddlDistrictId.DataSource = District.FindAll();
            this.ddlDistrictId.DataBind();
        
            Thana thana = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                thana = Thana.GetById(Convert.ToInt32(hdnId.Value));

                if (thana != null)
                {
                    this.Type = TYPE_EDIT;

                    this.txtName.Text = thana.Name;
                }
            }
        }
    }
}
