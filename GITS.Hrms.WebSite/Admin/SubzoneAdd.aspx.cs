using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class SubzoneAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "SUBZONE ADD"; }
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
            return "SubzoneList.aspx";
        }

        private Subzone GetSubzone()
        {
            Subzone subzone = null;

            if (Type == TYPE_EDIT)
            {
                subzone = Subzone.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                subzone = new Subzone();
            }

            subzone.ZoneId = DBUtility.ToInt32(ddlZoneId.SelectedValue);
            subzone.Name = DBUtility.ToString(txtName.Text);
            subzone.NameInBangla = DBUtility.ToNullableString(txtNameInBangla.Text);
            subzone.Status = (Subzone.Statuses)DBUtility.ToInt32(ddlStatus.SelectedValue);
            if (ddlStatus.SelectedValue == "1")
            {
                subzone.OpeningDate = DBUtility.ToNullableDateTime(txtOpeningDate.Text);
            }
            else
            {
                subzone.ClosingDate = DBUtility.ToNullableDateTime(txtOpeningDate.Text);
            }
            return subzone;
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
                Subzone zone = GetSubzone();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [Subzone]";
                }
                else
                {
                    desc = "Update [Subzone]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    Subzone.Insert(TransactionManager, zone);

                    hdnId.Value = zone.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    Subzone.Update(TransactionManager, zone);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlStatus, typeof(Subzone.Statuses), false, false, false);
            Subzone zone = null;

            ddlZoneId.DataSource = Zone.Find("Status=1","Name");
            ddlZoneId.DataBind();

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                zone = Subzone.GetById(Convert.ToInt32(hdnId.Value));

                if (zone != null)
                {
                    Type = TYPE_EDIT;

                    ddlZoneId.SelectedValue = UIUtility.Format(zone.ZoneId);
                    txtName.Text = zone.Name;
                    txtNameInBangla.Text = zone.NameInBangla;
                    ddlStatus.SelectedValue = ((Int32)zone.Status).ToString();
                    if (zone.Status == Subzone.Statuses.ACTIVE)
                    {
                        txtOpeningDate.Text = UIUtility.Format(zone.OpeningDate);
                        lblOpenClose.Text = "Opening Date:";
                    }
                    else
                    {
                        txtOpeningDate.Text = UIUtility.Format(zone.ClosingDate);
                        lblOpenClose.Text = "Closing Date:";
                    }
                }
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedValue == "1")
            {
                lblOpenClose.Text = "Opening Date:";
            }
            else
            {
                lblOpenClose.Text = "Closing Date:";
            }
        }
    }
}
