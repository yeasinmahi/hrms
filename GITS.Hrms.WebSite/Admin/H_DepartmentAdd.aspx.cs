using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class H_DepartmentAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_DEPARTMENT ADD"; }
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
            return "H_DepartmentList.aspx";
        }

        private H_Department GetH_Department()
        {
            H_Department h_Department = null;

            if (this.Type == TYPE_EDIT)
            {
                h_Department = H_Department.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Department = new H_Department();
            }

            h_Department.Name = DBUtility.ToString(txtName.Text);
            h_Department.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);

            return h_Department;
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
                H_Department h_Department = this.GetH_Department();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [H_Department]";
                }
                else
                {
                    desc = "Update [H_Department]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    H_Department.Insert(this.TransactionManager, h_Department);

                    hdnId.Value = h_Department.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    H_Department.Update(this.TransactionManager, h_Department);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            H_Department h_Department = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_Department = H_Department.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Department != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = h_Department.Name;
                    txtSortOrder.Text = UIUtility.Format(h_Department.SortOrder);
                }
            }
        }
    }
}
