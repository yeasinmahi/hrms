using System;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class H_DuplicationAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "LETTERDUPLICATION ADD"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            H_Duplication h_Duplication = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_Duplication = H_Duplication.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Duplication != null)
                {
                    Type = TYPE_EDIT;
                    txtName.Text = h_Duplication.Name;
                    txtSortOrder.Text = UIUtility.Format(h_Duplication.SortOrder);
                }
            }
        }

        protected override string GetListPageUrl()
        {
            return "H_DuplicationList.aspx";
        }
        private H_Duplication GetH_Duplication()
        {
            H_Duplication h_Duplication = null;

            if (Type == TYPE_EDIT)
            {
                h_Duplication = H_Duplication.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Duplication = new H_Duplication();
            }
            h_Duplication.Name = DBUtility.ToString(txtName.Text);
            h_Duplication.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);


            return h_Duplication;
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
                H_Duplication h_Duplication = GetH_Duplication();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [H_Duplication]";
                }
                else
                {
                    desc = "Update [H_Duplication]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    H_Duplication.Insert(TransactionManager, h_Duplication);

                    hdnId.Value = h_Duplication.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    H_Duplication.Update(TransactionManager, h_Duplication);
                }

                TransactionManager.Commit();
            }

            return msg;
        }
    }
}
