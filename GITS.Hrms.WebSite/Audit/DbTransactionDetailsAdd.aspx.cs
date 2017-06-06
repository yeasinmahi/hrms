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

            if (Type == TYPE_EDIT)
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
                DbTransactionDetails dbTransactionDetails = GetDbTransactionDetails();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [DbTransactionDetails]";
                }
                else
                {
                    desc = "Update [DbTransactionDetails]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    DbTransactionDetails.Insert(TransactionManager, dbTransactionDetails);

                    hdnId.Value = dbTransactionDetails.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    DbTransactionDetails.Update(TransactionManager, dbTransactionDetails);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            DbTransactionDetails dbTransactionDetails = null;

            ddlDbTransactionId.DataSource = DbTransaction.FindAll();
            ddlDbTransactionId.DataBind();

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                dbTransactionDetails = DbTransactionDetails.GetById(Convert.ToInt32(hdnId.Value));

                if (dbTransactionDetails != null)
                {
                    Type = TYPE_EDIT;

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
