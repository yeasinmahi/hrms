using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

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

	private Asa.Hrms.Data.Entity.District GetDistrict()
	{
		Asa.Hrms.Data.Entity.District district = null;

		if (this.Type == TYPE_EDIT)
		{
			district = Asa.Hrms.Data.Entity.District.GetById(Convert.ToInt32(hdnId.Value));
		}
		else
		{
			district = new Asa.Hrms.Data.Entity.District();
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
			Asa.Hrms.Data.Entity.District district = this.GetDistrict();
			string desc = "";

			if (this.Type == TYPE_ADD)
			{
				desc = "Insert [District]";
			}
			else
			{
				desc = "Update [District]";
			}

			this.TransactionManager = new TransactionManager(true, desc);

			if (this.Type == TYPE_ADD)
			{
				Asa.Hrms.Data.Entity.District.Insert(this.TransactionManager, district);

				hdnId.Value = district.Id.ToString();
				this.Type = TYPE_EDIT;
			}
			else
			{
				Asa.Hrms.Data.Entity.District.Update(this.TransactionManager, district);
			}

			this.TransactionManager.Commit();
		}

		return msg;
	}

	protected override void LoadData()
	{
        ddlDivision.DataSource = Division.FindAll("Name");
        ddlDivision.DataBind();
		Asa.Hrms.Data.Entity.District district = null;

		if (Request.QueryString["Id"] != null)
		{
			hdnId.Value = Request.QueryString["Id"];
			district = Asa.Hrms.Data.Entity.District.GetById(Convert.ToInt32(hdnId.Value));

			if (district != null)
			{
				this.Type = TYPE_EDIT;
                ddlDivision.SelectedValue = district.DivisionId.ToString();
				txtName.Text = district.Name;
			}
		}
	}
}
