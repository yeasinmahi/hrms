using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;
namespace Asa.Hrms.WebSite.Admin
{
    public partial class OrganizationAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "ORGANIZATION ADD"; }
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
            return "OrganizationList.aspx";
        }

        private Asa.Hrms.Data.Entity.Organization GetOrganization()
        {
            Asa.Hrms.Data.Entity.Organization organization = null;

            if (this.Type == TYPE_EDIT)
            {
                organization = Asa.Hrms.Data.Entity.Organization.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                organization = new Asa.Hrms.Data.Entity.Organization();
            }

            organization.Name = DBUtility.ToString(txtName.Text);


            return organization;
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
                Asa.Hrms.Data.Entity.Organization organization = this.GetOrganization();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [Organization]";
                }
                else
                {
                    desc = "Update [Organization]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Asa.Hrms.Data.Entity.Organization.Insert(this.TransactionManager, organization);

                    hdnId.Value = organization.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Asa.Hrms.Data.Entity.Organization.Update(this.TransactionManager, organization);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            Asa.Hrms.Data.Entity.Organization organization = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                organization = Asa.Hrms.Data.Entity.Organization.GetById(Convert.ToInt32(hdnId.Value));

                if (organization != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = organization.Name;

                }
            }
        }
    }
}
