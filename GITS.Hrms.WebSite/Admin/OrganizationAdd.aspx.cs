using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
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

        private Organization GetOrganization()
        {
            Organization organization = null;

            if (Type == TYPE_EDIT)
            {
                organization = Organization.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                organization = new Organization();
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

            if (IsValid == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid data provided or required data missing";
                return msg;
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                Organization organization = GetOrganization();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [Organization]";
                }
                else
                {
                    desc = "Update [Organization]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    Organization.Insert(TransactionManager, organization);

                    hdnId.Value = organization.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    Organization.Update(TransactionManager, organization);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            Organization organization = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                organization = Organization.GetById(Convert.ToInt32(hdnId.Value));

                if (organization != null)
                {
                    Type = TYPE_EDIT;

                    txtName.Text = organization.Name;

                }
            }
        }
    }
}
