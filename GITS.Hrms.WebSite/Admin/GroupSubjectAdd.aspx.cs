using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
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

        private GroupSubject GetGroupSubject()
        {
            GroupSubject groupSubject = null;

            if (this.Type == TYPE_EDIT)
            {
                groupSubject = GroupSubject.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                groupSubject = new GroupSubject();
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
                GroupSubject groupSubject = this.GetGroupSubject();
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
                    GroupSubject.Insert(this.TransactionManager, groupSubject);

                    hdnId.Value = groupSubject.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    GroupSubject.Update(this.TransactionManager, groupSubject);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            GroupSubject groupSubject = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                groupSubject = GroupSubject.GetById(Convert.ToInt32(hdnId.Value));

                if (groupSubject != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = groupSubject.Name;
                }
            }
        }
    }
}
