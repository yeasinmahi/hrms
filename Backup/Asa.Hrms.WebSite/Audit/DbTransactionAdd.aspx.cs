using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

public partial class DbTransactionAdd : AddPage
{
	protected override string PropertyName
	{
		get { return "DBTRANSACTION ADD"; }
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
		return "DbTransactionList.aspx";
	}

	private Asa.Hrms.Data.Entity.DbTransaction GetDbTransaction()
	{
		Asa.Hrms.Data.Entity.DbTransaction dbTransaction = null;

		if (this.Type == TYPE_EDIT)
		{
			dbTransaction = Asa.Hrms.Data.Entity.DbTransaction.GetById(Convert.ToInt32(hdnId.Value));
		}
		else
		{
			dbTransaction = new Asa.Hrms.Data.Entity.DbTransaction();
		}

		dbTransaction.Description = DBUtility.ToNullableString(txtDescription.Text);
		dbTransaction.CreatedBy = DBUtility.ToString(ddlCreatedBy.SelectedValue);
		dbTransaction.CreatedDate = DBUtility.ToDateTime(txtCreatedDate.Text);

		return dbTransaction;
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
			Asa.Hrms.Data.Entity.DbTransaction dbTransaction = this.GetDbTransaction();
			string desc = "";

			if (this.Type == TYPE_ADD)
			{
				desc = "Insert [DbTransaction]";
			}
			else
			{
				desc = "Update [DbTransaction]";
			}

			this.TransactionManager = new TransactionManager(true, desc);

			if (this.Type == TYPE_ADD)
			{
				Asa.Hrms.Data.Entity.DbTransaction.Insert(this.TransactionManager, dbTransaction);

				hdnId.Value = dbTransaction.Id.ToString();
				this.Type = TYPE_EDIT;
			}
			else
			{
				Asa.Hrms.Data.Entity.DbTransaction.Update(this.TransactionManager, dbTransaction);
			}

			this.TransactionManager.Commit();
		}

		return msg;
	}

	protected override void LoadData()
	{
		Asa.Hrms.Data.Entity.DbTransaction dbTransaction = null;

		this.ddlCreatedBy.DataSource = Asa.Hrms.Data.Entity.User.FindAll();
		this.ddlCreatedBy.DataBind();

		if (Request.QueryString["Id"] != null)
		{
			hdnId.Value = Request.QueryString["Id"];
			dbTransaction = Asa.Hrms.Data.Entity.DbTransaction.GetById(Convert.ToInt32(hdnId.Value));

			if (dbTransaction != null)
			{
				this.Type = TYPE_EDIT;

				txtDescription.Text = dbTransaction.Description;
				ddlCreatedBy.SelectedValue = dbTransaction.CreatedBy;
				txtCreatedDate.Text = UIUtility.Format(dbTransaction.CreatedDate);
			}
		}
	}
}
