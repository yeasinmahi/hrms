using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

public partial class GroupSubjectAdd : AddPage
{
	protected override String PropertyName
	{
		get { return "GROUPSUBJECT ADD"; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
	}

	protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
	{
		throw new NotImplementedException();
	}

	protected override String GetListPageUrl()
	{
		return "GroupSubjectList.aspx";
	}

	private Asa.Hrms.Data.Entity.GroupSubject GetGroupSubject()
	{
		Asa.Hrms.Data.Entity.GroupSubject groupSubject = null;

		if (this.Type == TYPE_EDIT)
		{
			groupSubject = Asa.Hrms.Data.Entity.GroupSubject.GetById(Convert.ToInt32(hdnId.Value));
		}
		else
		{
			groupSubject = new Asa.Hrms.Data.Entity.GroupSubject();
		}

		groupSubject.Name = DBUtility.ToString(txtName.Text);

		return groupSubject;
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
			Asa.Hrms.Data.Entity.GroupSubject groupSubject = this.GetGroupSubject();
			string desc = "";

			if (this.Type == TYPE_ADD)
			{
				desc = "Insert [GroupSubject]";
			}
			else
			{
				desc = "Update [GroupSubject]";
			}

			this.TransactionManager = new TransactionManager(true, desc);

			if (this.Type == TYPE_ADD)
			{
				Asa.Hrms.Data.Entity.GroupSubject.Insert(this.TransactionManager, groupSubject);

				hdnId.Value = groupSubject.Id.ToString();
				this.Type = TYPE_EDIT;
			}
			else
			{
				Asa.Hrms.Data.Entity.GroupSubject.Update(this.TransactionManager, groupSubject);
			}

			this.TransactionManager.Commit();
		}

		return msg;
	}

	protected override void LoadData()
	{
		Asa.Hrms.Data.Entity.GroupSubject groupSubject = null;

		if (Request.QueryString["Id"] != null)
		{
			hdnId.Value = Request.QueryString["Id"];
			groupSubject = Asa.Hrms.Data.Entity.GroupSubject.GetById(Convert.ToInt32(hdnId.Value));

			if (groupSubject != null)
			{
				this.Type = TYPE_EDIT;

				txtName.Text = groupSubject.Name;
			}
		}
	}
}
