using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class DistrictAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "DISTRICT ADD"; }
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
            return "DistrictList.aspx";
        }

        private District GetDistrict()
        {
            District district = null;

            if (Type == TYPE_EDIT)
            {
                district = District.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                district = new District();
            }
            district.DivisionId = DBUtility.ToInt32(ddlDivision.SelectedValue);
            district.Name = DBUtility.ToString(txtName.Text);

            return district;
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
                District district = GetDistrict();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [District]";
                }
                else
                {
                    desc = "Update [District]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    District.Insert(TransactionManager, district);

                    hdnId.Value = district.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    District.Update(TransactionManager, district);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            ddlDivision.DataSource = Division.FindAll("Name");
            ddlDivision.DataBind();
            District district = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                district = District.GetById(Convert.ToInt32(hdnId.Value));

                if (district != null)
                {
                    Type = TYPE_EDIT;
                    ddlDivision.SelectedValue = district.DivisionId.ToString();
                    txtName.Text = district.Name;
                }
            }
        }
    }
}
