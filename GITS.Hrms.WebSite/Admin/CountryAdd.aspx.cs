using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
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

        private Country GetCountry()
        {
            Country country = null;

            if (this.Type == TYPE_EDIT)
            {
                country = Country.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                country = new Country();
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
                Country country = this.GetCountry();
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
                    Country.Insert(this.TransactionManager, country);

                    hdnId.Value = country.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Country.Update(this.TransactionManager, country);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            Country country = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                country = Country.GetById(Convert.ToInt32(hdnId.Value));

                if (country != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = country.Name;
                   
                }
            }
        }
    }
}
