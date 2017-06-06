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

            if (Type == TYPE_EDIT)
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
                Thana thana = GetThana();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [Thana]";
                }
                else
                {
                    desc = "Update [Thana]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    Thana.Insert(TransactionManager, thana);

                    hdnId.Value = thana.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    Thana.Update(TransactionManager, thana);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            ddlDistrictId.DataSource = District.FindAll();
            ddlDistrictId.DataBind();
        
            Thana thana = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                thana = Thana.GetById(Convert.ToInt32(hdnId.Value));

                if (thana != null)
                {
                    Type = TYPE_EDIT;

                    txtName.Text = thana.Name;
                }
            }
        }
    }
}
