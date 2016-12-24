using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

public partial class BoardUniversityAdd : AddPage
{
	protected override String PropertyName
	{
		get { return "BOARDUNIVERSITY ADD"; }
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
		return "BoardUniversityList.aspx";
	}

	private Asa.Hrms.Data.Entity.BoardUniversity GetBoardUniversity()
	{
		Asa.Hrms.Data.Entity.BoardUniversity boardUniversity = null;

		if (this.Type == TYPE_EDIT)
		{
			boardUniversity = Asa.Hrms.Data.Entity.BoardUniversity.GetById(Convert.ToInt32(hdnId.Value));
		}
		else
		{
			boardUniversity = new Asa.Hrms.Data.Entity.BoardUniversity();
		}

		boardUniversity.Name = DBUtility.ToString(txtName.Text);

		return boardUniversity;
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
			Asa.Hrms.Data.Entity.BoardUniversity boardUniversity = this.GetBoardUniversity();
			string desc = "";

			if (this.Type == TYPE_ADD)
			{
				desc = "Insert [BoardUniversity]";
			}
			else
			{
				desc = "Update [BoardUniversity]";
			}

			this.TransactionManager = new TransactionManager(true, desc);

			if (this.Type == TYPE_ADD)
			{
				Asa.Hrms.Data.Entity.BoardUniversity.Insert(this.TransactionManager, boardUniversity);

				hdnId.Value = boardUniversity.Id.ToString();
				this.Type = TYPE_EDIT;
			}
			else
			{
				Asa.Hrms.Data.Entity.BoardUniversity.Update(this.TransactionManager, boardUniversity);
			}

			this.TransactionManager.Commit();
		}

		return msg;
	}

	protected override void LoadData()
	{
		Asa.Hrms.Data.Entity.BoardUniversity boardUniversity = null;

		if (Request.QueryString["Id"] != null)
		{
			hdnId.Value = Request.QueryString["Id"];
			boardUniversity = Asa.Hrms.Data.Entity.BoardUniversity.GetById(Convert.ToInt32(hdnId.Value));

			if (boardUniversity != null)
			{
				this.Type = TYPE_EDIT;

				txtName.Text = boardUniversity.Name;
			}
		}
	}
}
