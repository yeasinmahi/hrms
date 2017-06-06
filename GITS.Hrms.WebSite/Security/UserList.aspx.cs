using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Security
{
    public partial class UserList : GridPage
    {
        protected override string PropertyName
        {
            get { return "USER LIST"; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            EntityType = typeof(User);
            SortColumn = "Login";
            SortOrder = "ASC";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "RESET PASSWORD":
                    ResetPassword();
                    Message msg = new Message();
                    msg.Type = MessageType.Information;
                    msg.Msg = "Selected user(s) password reseted successfully";
                    ShowUiMessage(msg);
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
            TransactionManager = new TransactionManager(true, "Reset [User] Password");

            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                GridViewRow row = gvList.Rows[i];
                bool selected = ((CheckBox)row.FindControl("chkSelect")).Checked;

                if (selected)
                {
                    int id = Convert.ToInt32(gvList.DataKeys[i].Value);

                    User user = Library.Data.Entity.User.GetById(TransactionManager, id);
                    user.Password = "123";// user.Login;
                    user.IsReset = false;
                    Library.Data.Entity.User.Update(TransactionManager, user);
                }
            }

            TransactionManager.Commit();

            LoadData();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                User user = (User)e.Row.DataItem;
                BulletedList blRole = (BulletedList)e.Row.FindControl("blRole");
                blRole.DataSource = UserRole.FindByUserLogin(user.Login, "");
                blRole.DataBind();
            }
        }
    }
}