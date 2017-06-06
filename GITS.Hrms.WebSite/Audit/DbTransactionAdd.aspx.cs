using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Audit
{
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

        private DbTransaction GetDbTransaction()
        {
            DbTransaction dbTransaction = null;

            if (Type == TYPE_EDIT)
            {
                dbTransaction = DbTransaction.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                dbTransaction = new DbTransaction();
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

            if (msg.Type == MessageType.Information)
            {
                DbTransaction dbTransaction = GetDbTransaction();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [DbTransaction]";
                }
                else
                {
                    desc = "Update [DbTransaction]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    DbTransaction.Insert(TransactionManager, dbTransaction);

                    hdnId.Value = dbTransaction.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    DbTransaction.Update(TransactionManager, dbTransaction);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            DbTransaction dbTransaction = null;

            ddlCreatedBy.DataSource = Library.Data.Entity.User.FindAll();
            ddlCreatedBy.DataBind();

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                dbTransaction = DbTransaction.GetById(Convert.ToInt32(hdnId.Value));

                if (dbTransaction != null)
                {
                    Type = TYPE_EDIT;

                    txtDescription.Text = dbTransaction.Description;
                    ddlCreatedBy.SelectedValue = dbTransaction.CreatedBy;
                    txtCreatedDate.Text = UIUtility.Format(dbTransaction.CreatedDate);
                }
            }
        }
    }
}
