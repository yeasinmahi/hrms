using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Audit
{
    public partial class DbTransactionDetailsAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "DBTRANSACTIONDETAILS ADD"; }
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
            return "DbTransactionDetailsList.aspx";
        }

        private DbTransactionDetails GetDbTransactionDetails()
        {
            DbTransactionDetails dbTransactionDetails = null;

            if (this.Type == TYPE_EDIT)
            {
                dbTransactionDetails = DbTransactionDetails.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                dbTransactionDetails = new DbTransactionDetails();
            }

            dbTransactionDetails.DbTransactionId = DBUtility.ToInt32(ddlDbTransactionId.SelectedValue);
            dbTransactionDetails.Type = DBUtility.ToString(txtType.Text);
            dbTransactionDetails.TableName = DBUtility.ToString(txtTableName.Text);
            dbTransactionDetails.IdentityColumn = DBUtility.ToString(txtIdentityColumn.Text);
            dbTransactionDetails.IdentityValue = DBUtility.ToString(txtIdentityValue.Text);
            dbTransactionDetails.Value = DBUtility.ToNullableString(txtValue.Text);

            return dbTransactionDetails;
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
                DbTransactionDetails dbTransactionDetails = this.GetDbTransactionDetails();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [DbTransactionDetails]";
                }
                else
                {
                    desc = "Update [DbTransactionDetails]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    DbTransactionDetails.Insert(this.TransactionManager, dbTransactionDetails);

                    hdnId.Value = dbTransactionDetails.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    DbTransactionDetails.Update(this.TransactionManager, dbTransactionDetails);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            DbTransactionDetails dbTransactionDetails = null;

            this.ddlDbTransactionId.DataSource = DbTransaction.FindAll();
            this.ddlDbTransactionId.DataBind();

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                dbTransactionDetails = DbTransactionDetails.GetById(Convert.ToInt32(hdnId.Value));

                if (dbTransactionDetails != null)
                {
                    this.Type = TYPE_EDIT;

                    ddlDbTransactionId.SelectedValue = UIUtility.Format(dbTransactionDetails.DbTransactionId);
                    txtType.Text = dbTransactionDetails.Type;
                    txtTableName.Text = dbTransactionDetails.TableName;
                    txtIdentityColumn.Text = dbTransactionDetails.IdentityColumn;
                    txtIdentityValue.Text = dbTransactionDetails.IdentityValue;
                    txtValue.Text = dbTransactionDetails.Value;
                }
            }
        }
    }
}
