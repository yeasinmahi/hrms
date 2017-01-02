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

            if (this.Type == TYPE_EDIT)
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

            if (msg.Type == MessageType.Information || msg.Type == MessageType.Information)
            {
                User user = this.GetUser();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [User]";
                }
                {
                    desc = "Update [User]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Library.Data.Entity.User.Insert(this.TransactionManager, user);

                    hdnId.Value = user.Id.ToString();

                    foreach (ListItem li in cblRole.Items)
                    {
                        if (li.Selected)
                        {
                            UserRole ur = new UserRole();
                            ur.UserLogin = user.Login;
                            ur.RoleName = li.Value;

                            UserRole.Insert(this.TransactionManager, ur);
                        }
                    }

                    this.Type = TYPE_EDIT;
                    trPassword.Visible = false;
                }
                else
                {
                    Library.Data.Entity.User.Update(this.TransactionManager, user);

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

                                UserRole.Insert(this.TransactionManager, ur);
                            }
                        }
                        else if (ur != null)
                        {
                            UserRole.Delete(this.TransactionManager, ur.Id);
                        }
                    }
                }

                UserLocation ul = UserLocation.GetByLogin(this.TransactionManager, user.Login);

                if (ul == null)
                {
                    ul = new UserLocation();
                }

                ul.Login = user.Login;
                ul.ZoneId =ul.SubzoneId = ul.RegionId = ul.BranchId = null;

                if (rbBranch.Checked)
                {
                    ul.BranchId = DBUtility.ToInt32(this.ddlBranch.SelectedValue);
                }
                else if (rbRegion.Checked)
                {
                    ul.RegionId = DBUtility.ToInt32(this.ddlRegion.SelectedValue);
                }
                else if (rbSubzone.Checked)
                {
                    ul.SubzoneId = DBUtility.ToInt32(this.ddlSubzone.SelectedValue);
                }
                else if (rbZone.Checked)
                {
                    ul.ZoneId = DBUtility.ToInt32(this.ddlZone.SelectedValue);
                }

                if (ul.EntityState == EntityStates.New)
                {
                    UserLocation.Insert(this.TransactionManager, ul);
                }
                else
                {
                    UserLocation.Update(this.TransactionManager, ul);
                }

                this.TransactionManager.Commit();

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

            this.ddlZone.DataSource = Zone.FindAll("Name");
            this.ddlZone.DataBind();
            this.ddlZone_SelectedIndexChanged(this.ddlZone, new EventArgs());

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                user = Library.Data.Entity.User.GetById(Convert.ToInt32(hdnId.Value));

                if (user != null)
                {
                    this.Type = TYPE_EDIT;
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
                            this.rbBranch.Checked = true;
                            this.rbBranch_CheckedChanged(this.rbBranch, new EventArgs());

                            Branch b = Branch.GetById(ul.BranchId.Value);
                            Region r = Region.GetById(b.RegionId);
                            Subzone s = Subzone.GetById(r.SubzoneId);

                            this.ddlZone.SelectedValue = s.ZoneId.ToString();
                            this.ddlZone_SelectedIndexChanged(this.ddlZone, new EventArgs());

                            this.ddlSubzone.SelectedValue = r.SubzoneId.ToString();
                            this.ddlSubzone_SelectedIndexChanged(this.ddlSubzone, new EventArgs());

                            this.ddlRegion.SelectedValue = b.RegionId.ToString();
                            this.ddlRegion_SelectedIndexChanged(this.ddlRegion, new EventArgs());

                            this.ddlBranch.SelectedValue = ul.BranchId.ToString();
                        }
                        else if (ul.RegionId != null)
                        {
                            this.rbRegion.Checked = true;
                            this.rbRegion_CheckedChanged(this.rbRegion, new EventArgs());

                            Region r = Region.GetById(ul.RegionId.Value);
                            Subzone s = Subzone.GetById(r.SubzoneId);

                            this.ddlZone.SelectedValue = s.ZoneId.ToString();
                            this.ddlZone_SelectedIndexChanged(this.ddlZone, new EventArgs());

                            this.ddlSubzone.SelectedValue = r.SubzoneId.ToString();
                            this.ddlSubzone_SelectedIndexChanged(this.ddlSubzone, new EventArgs());

                            this.ddlRegion.SelectedValue = ul.RegionId.ToString();
                            this.ddlRegion_SelectedIndexChanged(this.ddlRegion, new EventArgs());
                        }
                        else if (ul.SubzoneId != null)
                        {
                            this.rbSubzone.Checked = true;
                            this.rbSubzone_CheckedChanged(this.rbSubzone, new EventArgs());

                            this.ddlSubzone.SelectedValue = ul.SubzoneId.ToString();
                            this.ddlSubzone_SelectedIndexChanged(this.ddlSubzone, new EventArgs());

                            this.ddlRegion_SelectedIndexChanged(this.ddlRegion, new EventArgs());
                        }
                        else if (ul.ZoneId != null)
                        {
                            this.rbZone.Checked = true;
                            this.rbZone_CheckedChanged(this.rbZone, new EventArgs());

                            this.ddlZone.SelectedValue = ul.ZoneId.ToString();
                            this.ddlZone_SelectedIndexChanged(this.ddlZone, new EventArgs());

                            this.ddlRegion_SelectedIndexChanged(this.ddlRegion, new EventArgs());
                        }
                        else
                        {
                            this.rbAll.Checked = true;
                            this.rbAll_CheckedChanged(this.rbAll, new EventArgs());
                        }
                    }
                }
            }
        }

        protected void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            this.rbZone.Checked = false;
            this.rbSubzone.Checked = false;
            this.rbRegion.Checked = false;
            this.rbBranch.Checked = false;
        }

        protected void rbZone_CheckedChanged(object sender, EventArgs e)
        {
            this.rbAll.Checked = false;
            this.rbSubzone.Checked = false;
            this.rbRegion.Checked = false;
            this.rbBranch.Checked = false;
        }

        protected void rbSubzone_CheckedChanged(object sender, EventArgs e)
        {
            this.rbAll.Checked = false;
            this.rbZone.Checked = false;
            this.rbRegion.Checked = false;
            this.rbBranch.Checked = false;
        }

        protected void rbRegion_CheckedChanged(object sender, EventArgs e)
        {
            this.rbAll.Checked = false;
            this.rbZone.Checked = false;
            this.rbSubzone.Checked = false;
            this.rbBranch.Checked = false;
        }

        protected void rbBranch_CheckedChanged(object sender, EventArgs e)
        {
            this.rbAll.Checked = false;
            this.rbZone.Checked = false;
            this.rbSubzone.Checked = false;
            this.rbRegion.Checked = false;
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlZone.SelectedValue != null && this.ddlZone.SelectedValue != "")
            {
                this.ddlSubzone.DataSource = Subzone.FindByZoneId(Convert.ToInt32(this.ddlZone.SelectedValue), "Name");
                this.ddlSubzone.DataBind();

                this.ddlSubzone_SelectedIndexChanged(ddlRegion, new EventArgs());
            }
        }

        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlSubzone.SelectedValue != null && this.ddlSubzone.SelectedValue != "")
            {
                this.ddlRegion.DataSource = Region.FindBySubzoneId(Convert.ToInt32(this.ddlSubzone.SelectedValue), "Name");
                this.ddlRegion.DataBind();

                this.ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlRegion.SelectedValue != null && this.ddlRegion.SelectedValue != "")
            {
                this.ddlBranch.DataSource = Branch.FindByRegionId(Convert.ToInt32(this.ddlRegion.SelectedValue), "Name");
                this.ddlBranch.DataBind();
            }
        }
    }
}
