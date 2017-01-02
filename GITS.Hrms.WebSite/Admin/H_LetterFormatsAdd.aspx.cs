using System;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class H_LetterFormatsAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "LETTERFORMAT ADD"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlLetterType, typeof(H_LetterFormats.LetterTypes), false, false, true);
            H_LetterFormats config = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                config = H_LetterFormats.GetById(Convert.ToInt32(hdnId.Value));

                if (config != null)
                {
                    this.Type = TYPE_EDIT;
                    ddlLetterType.SelectedValue=((Int32)config.LetterType).ToString();
                    txtName.Text = config.Name;
                    txtInsideAddress.Text = config.InsideAddress;
                    txtSubject.Text = config.Subject;
                    txtLetterBody.Text = config.LetterBody;
                    txtComplimentary.Text = config.Conclusion;
                    txtSignatory.Text = config.Signatory;
                    txtDesignation.Text = config.Designation;
                    txtSortOrder.Text = UIUtility.Format(config.SortOrder);
                }
            }
        }

        protected override string GetListPageUrl()
        {
            return "H_LetterFormatsList.aspx";
        }
        private H_LetterFormats GetLetterFormats()
        {
            H_LetterFormats letterFormats = null;

            if (this.Type == TYPE_EDIT)
            {
                letterFormats = H_LetterFormats.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                letterFormats = new H_LetterFormats();
            }
            letterFormats.LetterType = (H_LetterFormats.LetterTypes)DBUtility.ToInt32(ddlLetterType.SelectedValue);
            letterFormats.Name = DBUtility.ToString(txtName.Text);
            letterFormats.InsideAddress = DBUtility.ToString(txtInsideAddress.Text);
            letterFormats.Subject = DBUtility.ToString(txtSubject.Text);
            letterFormats.LetterBody = DBUtility.ToString(txtLetterBody.Text);
            letterFormats.Conclusion = DBUtility.ToString(txtComplimentary.Text);
            letterFormats.Signatory = DBUtility.ToString(txtSignatory.Text);
            letterFormats.Designation = DBUtility.ToString(txtDesignation.Text);
            letterFormats.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);

            return letterFormats;
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
                H_LetterFormats h_LetterFormats = this.GetLetterFormats();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [H_LetterFormats]";
                }
                else
                {
                    desc = "Update [H_LetterFormats]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    H_LetterFormats.Insert(this.TransactionManager, h_LetterFormats);

                    hdnId.Value = h_LetterFormats.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    H_LetterFormats.Update(this.TransactionManager, h_LetterFormats);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }
    }
}
