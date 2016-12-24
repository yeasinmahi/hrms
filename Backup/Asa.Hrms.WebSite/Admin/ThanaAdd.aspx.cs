using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

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

	private Asa.Hrms.Data.Entity.Thana GetThana()
	{
		Asa.Hrms.Data.Entity.Thana thana = null;

		if (this.Type == TYPE_EDIT)
		{
			thana = Asa.Hrms.Data.Entity.Thana.GetById(Convert.ToInt32(hdnId.Value));
		}
		else
		{
			thana = new Asa.Hrms.Data.Entity.Thana();
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
			Asa.Hrms.Data.Entity.Thana thana = this.GetThana();
			string desc = "";

			if (this.Type == TYPE_ADD)
			{
				desc = "Insert [Thana]";
			}
			else
			{
				desc = "Update [Thana]";
			}

			this.TransactionManager = new TransactionManager(true, desc);

			if (this.Type == TYPE_ADD)
			{
				Asa.Hrms.Data.Entity.Thana.Insert(this.TransactionManager, thana);

				hdnId.Value = thana.Id.ToString();
				this.Type = TYPE_EDIT;
			}
			else
			{
				Asa.Hrms.Data.Entity.Thana.Update(this.TransactionManager, thana);
			}

			this.TransactionManager.Commit();
		}

		return msg;
	}

	protected override void LoadData()
	{
        this.ddlDistrictId.DataSource = Asa.Hrms.Data.Entity.District.FindAll();
        this.ddlDistrictId.DataBind();
        
		Asa.Hrms.Data.Entity.Thana thana = null;

		if (Request.QueryString["Id"] != null)
		{
			hdnId.Value = Request.QueryString["Id"];
			thana = Asa.Hrms.Data.Entity.Thana.GetById(Convert.ToInt32(hdnId.Value));

			if (thana != null)
			{
				this.Type = TYPE_EDIT;

                this.txtName.Text = thana.Name;
			}
		}
	}
}
