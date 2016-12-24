using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Data;
using Asa.Hrms.Data.Entity;
using System.Collections.Generic;
using Asa.Hrms.Utility;

public partial class UserList : Asa.Hrms.Web.GridPage
{
    protected override string PropertyName
    {
        get { return "USER LIST"; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.GridView = this.gvList;
        this.EntityType = typeof(User);
        this.SortColumn = "Login";
        this.SortOrder = "ASC";
    }

	protected void Page_Load(object sender, EventArgs e)
	{
	}

	protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
	{
        switch (e.Item.Value)
        {
            case "RESET PASSWORD":
                this.ResetPassword();
                Message msg = new Message();
                msg.Type = MessageType.Information;
                msg.Msg = "Selected user(s) password reseted successfully";
                ShowUIMessage(msg);
                break;
            default:
                throw new NotImplementedException();
        }
	}

	protected override string GetAddPageUrl()
	{
		return "UserAdd.aspx";
	}

    private void ResetPassword()
    {
        this.TransactionManager = new TransactionManager(true, "Reset [User] Password");

        for (int i = 0; i < gvList.Rows.Count; i++)
        {
            GridViewRow row = gvList.Rows[i];
            bool selected = ((CheckBox)row.FindControl("chkSelect")).Checked;

            if (selected)
            {
                int id = Convert.ToInt32(gvList.DataKeys[i].Value);

                Asa.Hrms.Data.Entity.User user = Asa.Hrms.Data.Entity.User.GetById(this.TransactionManager, id);
                user.Password = "123";// user.Login;
                user.IsReset = false;
                Asa.Hrms.Data.Entity.User.Update(this.TransactionManager, user);
            }
        }

        this.TransactionManager.Commit();

        this.LoadData();
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            Asa.Hrms.Data.Entity.User user = (Asa.Hrms.Data.Entity.User)e.Row.DataItem;
            BulletedList blRole = (BulletedList)e.Row.FindControl("blRole");
            blRole.DataSource = UserRole.FindByUserLogin(user.Login, "");
            blRole.DataBind();
        }
    }
}