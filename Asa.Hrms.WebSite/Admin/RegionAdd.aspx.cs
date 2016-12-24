using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class RegionAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "REGION ADD"; }
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
            return "RegionList.aspx";
        }

        private Region GetRegion()
        {
            Region region = null;

            if (this.Type == TYPE_EDIT)
            {
                region = Region.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                region = new Region();
            }

            region.SubzoneId = DBUtility.ToInt32(ddlSubzoneId.SelectedValue);
            region.Name = DBUtility.ToString(txtName.Text);
            region.Status = (Region.Statuses)DBUtility.ToInt32(ddlStatus.SelectedValue);
            if (ddlStatus.SelectedValue == "1")
            {
                region.OpeningDate = DBUtility.ToNullableDateTime(txtOpeningDate.Text);
            }
            else
            {
                region.ClosingDate = DBUtility.ToNullableDateTime(txtOpeningDate.Text);
            }

            return region;
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
                Region region = this.GetRegion();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [Region]";
                }
                else
                {
                    desc = "Update [Region]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Region.Insert(this.TransactionManager, region);

                    hdnId.Value = region.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Region.Update(this.TransactionManager, region);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlStatus, typeof(Region.Statuses), false, false, false);
            this.ddlZoneId.DataSource = Zone.Find("Status=1","Name");
            this.ddlZoneId.DataBind();
            this.ddlZoneId_OnSelectedIndexChanged(this.ddlZoneId, new EventArgs());

            Region region = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];            

                region = Region.GetById(Convert.ToInt32(hdnId.Value));

                if (region != null)
                {
                    this.Type = TYPE_EDIT;

                    Subzone subzone = Subzone.GetById(Convert.ToInt32(region.SubzoneId));
                    this.ddlZoneId.SelectedValue = UIUtility.Format(subzone.ZoneId);
                    this.ddlZoneId_OnSelectedIndexChanged(this.ddlZoneId, new EventArgs());
                    ddlSubzoneId.SelectedValue = UIUtility.Format(region.SubzoneId);

                    txtName.Text = region.Name;
                    ddlStatus.SelectedValue = ((Int32)region.Status).ToString();
                    if (region.Status == Region.Statuses.ACTIVE)
                    {
                        txtOpeningDate.Text = UIUtility.Format(region.OpeningDate);
                        lblOpenClose.Text = "Opening Date:";
                    }
                    else
                    {
                        txtOpeningDate.Text = UIUtility.Format(region.ClosingDate);
                        lblOpenClose.Text = "Closing Date:";
                    }
                }
            }
        }

        protected void ddlZoneId_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZoneId.SelectedValue != null && ddlZoneId.SelectedValue != "")
            {
                this.ddlSubzoneId.DataSource = Subzone.Find("ZoneId="+Convert.ToInt32(this.ddlZoneId.SelectedValue)+ " AND Status=1", "Name");
                this.ddlSubzoneId.DataBind();
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
