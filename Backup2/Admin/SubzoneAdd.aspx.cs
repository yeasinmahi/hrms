using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

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

	private Asa.Hrms.Data.Entity.Subzone GetSubzone()
	{
		Asa.Hrms.Data.Entity.Subzone subzone = null;

		if (this.Type == TYPE_EDIT)
		{
            subzone = Asa.Hrms.Data.Entity.Subzone.GetById(Convert.ToInt32(hdnId.Value));
		}
		else
		{
            subzone = new Asa.Hrms.Data.Entity.Subzone();
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
			Asa.Hrms.Data.Entity.Subzone zone = this.GetSubzone();
			string desc = "";

			if (this.Type == TYPE_ADD)
			{
				desc = "Insert [Subzone]";
			}
			else
			{
				desc = "Update [Subzone]";
			}

			this.TransactionManager = new TransactionManager(true, desc);

			if (this.Type == TYPE_ADD)
			{
				Asa.Hrms.Data.Entity.Subzone.Insert(this.TransactionManager, zone);

				hdnId.Value = zone.Id.ToString();
				this.Type = TYPE_EDIT;
			}
			else
			{
				Asa.Hrms.Data.Entity.Subzone.Update(this.TransactionManager, zone);
			}

			this.TransactionManager.Commit();
		}

		return msg;
	}

	protected override void LoadData()
	{
        UIUtility.LoadEnums(ddlStatus, typeof(Subzone.Statuses), false, false, false);
		Asa.Hrms.Data.Entity.Subzone zone = null;

		this.ddlZoneId.DataSource = Asa.Hrms.Data.Entity.Zone.Find("Status=1","Name");
		this.ddlZoneId.DataBind();

		if (Request.QueryString["Id"] != null)
		{
			hdnId.Value = Request.QueryString["Id"];
			zone = Asa.Hrms.Data.Entity.Subzone.GetById(Convert.ToInt32(hdnId.Value));

			if (zone != null)
			{
				this.Type = TYPE_EDIT;

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
