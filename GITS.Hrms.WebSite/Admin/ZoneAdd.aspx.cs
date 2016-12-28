using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class ZoneAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "ZONE ADD"; }
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
            return "ZoneList.aspx";
        }

        private Zone GetZone()
        {
            Zone division = null;

            if (this.Type == TYPE_EDIT)
            {
                division = Zone.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                division = new Zone();
            }

            division.Name = DBUtility.ToString(txtName.Text);
            division.Status =(Zone.Statuses) DBUtility.ToInt32(ddlStatus.SelectedValue);
            if (ddlStatus.SelectedValue == "1")
            {
                division.OpeningDate = DBUtility.ToNullableDateTime(txtOpeningDate.Text);
            }
            else
            {
                division.ClosingDate = DBUtility.ToNullableDateTime(txtOpeningDate.Text);
            }
            return division;
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
                Zone division = this.GetZone();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [Zone]";
                }
                else
                {
                    desc = "Update [Zone]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Zone.Insert(this.TransactionManager, division);

                    hdnId.Value = division.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Zone.Update(this.TransactionManager, division);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlStatus, typeof(Zone.Statuses), false, false, false);
            Zone division = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                division = Zone.GetById(Convert.ToInt32(hdnId.Value));

                if (division != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = division.Name;
                    ddlStatus.SelectedValue = ((Int32)division.Status).ToString();
                    if (division.Status == Zone.Statuses.ACTIVE)
                    {
                        txtOpeningDate.Text = UIUtility.Format(division.OpeningDate);
                        lblOpenClose.Text = "Opening Date:";
                    }
                    else
                    {
                        txtOpeningDate.Text = UIUtility.Format(division.ClosingDate);
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
