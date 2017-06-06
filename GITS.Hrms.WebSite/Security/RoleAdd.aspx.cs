using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Security
{
    public partial class RoleAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "ROLE ADD"; }
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
            return "RoleList.aspx";
        }

        private Role GetRole()
        {
            Role role = null;

            if (Type == TYPE_EDIT)
            {
                role = Role.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                role = new Role();
            }

            role.Name = txtName.Text;

            return role;
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
                Role role = GetRole();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [Role]";
                }
                else
                {
                    desc = "Update [Role]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    Role.Insert(TransactionManager, role);

                    hdnId.Value = role.Id.ToString();

                    foreach (GridViewRow gvr in gvList.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkProperty")).Checked)
                        {
                            RoleProperty rp = new RoleProperty();
                            rp.PropertyName = gvList.DataKeys[gvr.RowIndex].Value.ToString();
                            rp.RoleName = role.Name;

                            RoleProperty.Insert(TransactionManager, rp);
                        }

                        CheckBoxList cblCommand = (CheckBoxList)gvr.FindControl("cblCommand");

                        foreach (ListItem li in cblCommand.Items)
                        {
                            if (li.Selected)
                            {
                                PropertyCommand pc = PropertyCommand.GetById(TransactionManager, Convert.ToInt32(li.Value));

                                if (pc != null)
                                {
                                    RoleCommand rc = new RoleCommand();
                                    rc.RoleName = role.Name;
                                    rc.PropertyName = pc.PropertyName;
                                    rc.CommandName = pc.CommandName;

                                    RoleCommand.Insert(TransactionManager, rc);
                                }
                            }
                        }
                    }

                    Type = TYPE_EDIT;
                }
                else
                {
                    //update role
                    Role.Update(TransactionManager, role);
                
                    foreach (GridViewRow gvr in gvList.Rows)
                    {
                        IList<RoleProperty> delete = new List<RoleProperty>();

                        //update property
                        RoleProperty rp = RoleProperty.GetByRoleNameAndPropertyName(TransactionManager, role.Name, gvList.DataKeys[gvr.RowIndex].Value.ToString());

                        if (((CheckBox)gvr.FindControl("chkProperty")).Checked)
                        {
                            if (rp == null)
                            {
                                rp = new RoleProperty();
                                rp.PropertyName = gvList.DataKeys[gvr.RowIndex].Value.ToString();
                                rp.RoleName = role.Name;

                                RoleProperty.Insert(TransactionManager, rp);
                            }
                        }
                        else if (rp != null)
                        {
                            delete.Add(rp);
                        }

                        //update commands
                        CheckBoxList cblCommand = (CheckBoxList)gvr.FindControl("cblCommand");

                        foreach (ListItem li in cblCommand.Items)
                        {
                            PropertyCommand pc = PropertyCommand.GetById(TransactionManager, Convert.ToInt32(li.Value));

                            if (pc != null)
                            {
                                RoleCommand rc = RoleCommand.GetByRoleNameAndPropertyNameAndCommandName(TransactionManager, role.Name, pc.PropertyName, pc.CommandName);

                                if (li.Selected)
                                {
                                    if (rc == null)
                                    {
                                        rc = new RoleCommand();
                                        rc.RoleName = role.Name;
                                        rc.PropertyName = pc.PropertyName;
                                        rc.CommandName = pc.CommandName;

                                        RoleCommand.Insert(TransactionManager, rc);
                                    }
                                }
                                else if (rc != null)
                                {
                                    RoleCommand.Delete(TransactionManager, rc.Id);
                                }
                            }
                        }

                        foreach (RoleProperty del in delete)
                        {
                            RoleProperty.Delete(TransactionManager, del.Id);
                        }
                    }
                }

                TransactionManager.Commit();

                if (UserRole.GetByUserLoginAndRoleName(User.Identity.Name, role.Name) != null)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                }
            }

            return msg;
        }

        protected override void LoadData()
        {
            Role role = null;
            string filter = gvspList.WhereClause;

            if (filter != "")
            {
                filter += " AND ";
            }

            filter += "IsActive = 1 AND PermissionType = 'OWN'";

            gvList.DataSource = Property.Find(filter, "ModuleName, DisplayName");
            gvList.DataBind();

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                role = Role.GetById(Convert.ToInt32(hdnId.Value));

                if (role != null)
                {
                    Type = TYPE_EDIT;

                    txtName.Text = role.Name;

                    foreach (GridViewRow gvr in gvList.Rows)
                    {
                        RoleProperty rp = RoleProperty.GetByRoleNameAndPropertyName(role.Name, gvList.DataKeys[gvr.RowIndex].Value.ToString());

                        ((CheckBox)gvr.FindControl("chkProperty")).Checked = (rp != null);
                    }
                }
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                Property property = (Property)e.Row.DataItem;
                Role role = null;
                CheckBoxList cblCommand = (CheckBoxList)e.Row.FindControl("cblCommand");
                IList<Property> properties = Property.FindByPermissionProperty(property.Name);
                IList<PropertyCommandView> propertyCommands = new List<PropertyCommandView>();

                properties.Insert(0, property);

                foreach (Property pro in properties)
                {
                    IList<PropertyCommandView> list = PropertyCommandView.Find("PropertyName = '" + pro.Name + "'", "");

                    foreach (PropertyCommandView pc in list)
                    {
                        if (pc.PermissionType == "OWN")
                        {
                            propertyCommands.Add(pc);
                        }
                    }
                }

                cblCommand.DataSource = propertyCommands;
                cblCommand.DataBind();

                if (Request.QueryString["Id"] != null)
                {
                    role = Role.GetById(Convert.ToInt32(Request.QueryString["Id"]));
                }

                foreach (PropertyCommandView pc in propertyCommands)
                {
                    ListItem li = cblCommand.Items.FindByValue(pc.Id.ToString());

                    if (role != null)
                    {
                        RoleCommand rc = RoleCommand.GetByRoleNameAndPropertyNameAndCommandName(role.Name, pc.PropertyName, pc.CommandName);

                        if (li != null && rc != null)
                        {
                            li.Selected = true;
                        }
                    }
                }
            }
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                gvList.PageIndex = e.NewPageIndex;
                LoadData();
            }
        }

        protected void gvspList_ResetButtonClicked(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void gvspList_SearchButtonClicked(object sender, EventArgs e)
        {
            LoadData();   
        }
    }
}
