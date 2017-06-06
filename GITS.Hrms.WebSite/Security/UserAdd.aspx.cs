using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Security
{
    public partial class UserAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "USER ADD"; }
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
            return "UserList.aspx";
        }

        private User GetUser()
        {
            User user = null;

            if (Type == TYPE_EDIT)
            {
                user = Library.Data.Entity.User.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                user = new User();
                user.Password = txtPassword.Text;
            }

            user.Login = txtLogin.Text;
            user.Name = txtName.Text;
            user.IsActive = chkIsActive.Checked;
            user.IsReset = false;
            user.UserType = (User.UserTypes)DBUtility.ToInt32(ddlUserType.SelectedValue);

            return user;
        }

        private new Message Validate()
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Record saved successfully.";

            base.Validate();

            if (IsValid == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid data provided or required data missing";
                return msg;
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information || msg.Type == MessageType.Information)
            {
                User user = GetUser();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [User]";
                }
                {
                    desc = "Update [User]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    Library.Data.Entity.User.Insert(TransactionManager, user);

                    hdnId.Value = user.Id.ToString();

                    foreach (ListItem li in cblRole.Items)
                    {
                        if (li.Selected)
                        {
                            UserRole ur = new UserRole();
                            ur.UserLogin = user.Login;
                            ur.RoleName = li.Value;

                            UserRole.Insert(TransactionManager, ur);
                        }
                    }

                    Type = TYPE_EDIT;
                    trPassword.Visible = false;
                }
                else
                {
                    Library.Data.Entity.User.Update(TransactionManager, user);

                    foreach (ListItem li in cblRole.Items)
                    {
                        UserRole ur = UserRole.GetByUserLoginAndRoleName(user.Login, li.Value);

                        if (li.Selected)
                        {
                            if (ur == null)
                            {
                                ur = new UserRole();
                                ur.UserLogin = user.Login;
                                ur.RoleName = li.Value;

                                UserRole.Insert(TransactionManager, ur);
                            }
                        }
                        else if (ur != null)
                        {
                            UserRole.Delete(TransactionManager, ur.Id);
                        }
                    }
                }

                UserLocation ul = UserLocation.GetByLogin(TransactionManager, user.Login);

                if (ul == null)
                {
                    ul = new UserLocation();
                }

                ul.Login = user.Login;
                ul.ZoneId =ul.SubzoneId = ul.RegionId = ul.BranchId = null;

                if (rbBranch.Checked)
                {
                    ul.BranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
                }
                else if (rbRegion.Checked)
                {
                    ul.RegionId = DBUtility.ToInt32(ddlRegion.SelectedValue);
                }
                else if (rbSubzone.Checked)
                {
                    ul.SubzoneId = DBUtility.ToInt32(ddlSubzone.SelectedValue);
                }
                else if (rbZone.Checked)
                {
                    ul.ZoneId = DBUtility.ToInt32(ddlZone.SelectedValue);
                }

                if (ul.EntityState == EntityStates.New)
                {
                    UserLocation.Insert(TransactionManager, ul);
                }
                else
                {
                    UserLocation.Update(TransactionManager, ul);
                }

                TransactionManager.Commit();

                if (User.Identity.Name == user.Login)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }
            }

            return msg;
        }

        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlUserType, typeof(User.UserTypes), false, false, false);
            User user = null;

            cblRole.DataSource = Role.FindAll();
            cblRole.DataBind();

            ddlZone.DataSource = Zone.FindAll("Name");
            ddlZone.DataBind();
            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                user = Library.Data.Entity.User.GetById(Convert.ToInt32(hdnId.Value));

                if (user != null)
                {
                    Type = TYPE_EDIT;
                    trPassword.Visible = false;
                    ddlUserType.SelectedValue = ((Int32)user.UserType).ToString();
                    txtLogin.Text = user.Login;
                    txtName.Text = user.Name;
                    chkIsActive.Checked = user.IsActive;

                    foreach (ListItem li in cblRole.Items)
                    {
                        UserRole ur = UserRole.GetByUserLoginAndRoleName(user.Login, li.Value);

                        li.Selected = (ur != null);
                    }

                    UserLocation ul = UserLocation.GetByLogin(user.Login);

                    if (ul != null)
                    {
                        if (ul.BranchId != null)
                        {
                            rbBranch.Checked = true;
                            rbBranch_CheckedChanged(rbBranch, new EventArgs());

                            Branch b = Branch.GetById(ul.BranchId.Value);
                            Region r = Region.GetById(b.RegionId);
                            Subzone s = Subzone.GetById(r.SubzoneId);

                            ddlZone.SelectedValue = s.ZoneId.ToString();
                            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

                            ddlSubzone.SelectedValue = r.SubzoneId.ToString();
                            ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());

                            ddlRegion.SelectedValue = b.RegionId.ToString();
                            ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());

                            ddlBranch.SelectedValue = ul.BranchId.ToString();
                        }
                        else if (ul.RegionId != null)
                        {
                            rbRegion.Checked = true;
                            rbRegion_CheckedChanged(rbRegion, new EventArgs());

                            Region r = Region.GetById(ul.RegionId.Value);
                            Subzone s = Subzone.GetById(r.SubzoneId);

                            ddlZone.SelectedValue = s.ZoneId.ToString();
                            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

                            ddlSubzone.SelectedValue = r.SubzoneId.ToString();
                            ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());

                            ddlRegion.SelectedValue = ul.RegionId.ToString();
                            ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
                        }
                        else if (ul.SubzoneId != null)
                        {
                            rbSubzone.Checked = true;
                            rbSubzone_CheckedChanged(rbSubzone, new EventArgs());

                            ddlSubzone.SelectedValue = ul.SubzoneId.ToString();
                            ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());

                            ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
                        }
                        else if (ul.ZoneId != null)
                        {
                            rbZone.Checked = true;
                            rbZone_CheckedChanged(rbZone, new EventArgs());

                            ddlZone.SelectedValue = ul.ZoneId.ToString();
                            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

                            ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
                        }
                        else
                        {
                            rbAll.Checked = true;
                            rbAll_CheckedChanged(rbAll, new EventArgs());
                        }
                    }
                }
            }
        }

        protected void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            rbZone.Checked = false;
            rbSubzone.Checked = false;
            rbRegion.Checked = false;
            rbBranch.Checked = false;
        }

        protected void rbZone_CheckedChanged(object sender, EventArgs e)
        {
            rbAll.Checked = false;
            rbSubzone.Checked = false;
            rbRegion.Checked = false;
            rbBranch.Checked = false;
        }

        protected void rbSubzone_CheckedChanged(object sender, EventArgs e)
        {
            rbAll.Checked = false;
            rbZone.Checked = false;
            rbRegion.Checked = false;
            rbBranch.Checked = false;
        }

        protected void rbRegion_CheckedChanged(object sender, EventArgs e)
        {
            rbAll.Checked = false;
            rbZone.Checked = false;
            rbSubzone.Checked = false;
            rbBranch.Checked = false;
        }

        protected void rbBranch_CheckedChanged(object sender, EventArgs e)
        {
            rbAll.Checked = false;
            rbZone.Checked = false;
            rbSubzone.Checked = false;
            rbRegion.Checked = false;
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZone.SelectedValue != null && ddlZone.SelectedValue != "")
            {
                ddlSubzone.DataSource = Subzone.FindByZoneId(Convert.ToInt32(ddlZone.SelectedValue), "Name");
                ddlSubzone.DataBind();

                ddlSubzone_SelectedIndexChanged(ddlRegion, new EventArgs());
            }
        }

        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubzone.SelectedValue != null && ddlSubzone.SelectedValue != "")
            {
                ddlRegion.DataSource = Region.FindBySubzoneId(Convert.ToInt32(ddlSubzone.SelectedValue), "Name");
                ddlRegion.DataBind();

                ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegion.SelectedValue != null && ddlRegion.SelectedValue != "")
            {
                ddlBranch.DataSource = Branch.FindByRegionId(Convert.ToInt32(ddlRegion.SelectedValue), "Name");
                ddlBranch.DataBind();
            }
        }
    }
}
