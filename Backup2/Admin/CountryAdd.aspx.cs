using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.WebSite.Admin
{
    public partial class CountryAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "COUNTRY ADD"; }
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
            return "CountryList.aspx";
        }

        private Asa.Hrms.Data.Entity.Country GetCountry()
        {
            Asa.Hrms.Data.Entity.Country country = null;

            if (this.Type == TYPE_EDIT)
            {
                country = Asa.Hrms.Data.Entity.Country.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                country = new Asa.Hrms.Data.Entity.Country();
            }

            country.Name = DBUtility.ToString(txtName.Text);


            return country;
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
                Asa.Hrms.Data.Entity.Country country = this.GetCountry();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [Country]";
                }
                else
                {
                    desc = "Update [Country]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Asa.Hrms.Data.Entity.Country.Insert(this.TransactionManager, country);

                    hdnId.Value = country.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Asa.Hrms.Data.Entity.Country.Update(this.TransactionManager, country);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            Asa.Hrms.Data.Entity.Country country = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                country = Asa.Hrms.Data.Entity.Country.GetById(Convert.ToInt32(hdnId.Value));

                if (country != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = country.Name;
                   
                }
            }
        }
    }
}
