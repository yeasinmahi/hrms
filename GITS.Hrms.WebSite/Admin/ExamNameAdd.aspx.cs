using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class ExamNameAdd : AddPage
    {
        protected override String PropertyName
        {
            get { return "EXAMNAME ADD"; }
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
            return "ExamNameList.aspx";
        }

        private ExamName GetExamName()
        {
            ExamName examName = null;

            if (this.Type == TYPE_EDIT)
            {
                examName = ExamName.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                examName = new ExamName();
            }

            examName.Name = DBUtility.ToString(txtName.Text);

            return examName;
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
                ExamName examName = this.GetExamName();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [ExamName]";
                }
                else
                {
                    desc = "Update [ExamName]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    ExamName.Insert(this.TransactionManager, examName);

                    hdnId.Value = examName.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    ExamName.Update(this.TransactionManager, examName);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            ExamName examName = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                examName = ExamName.GetById(Convert.ToInt32(hdnId.Value));

                if (examName != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = examName.Name;
                }
            }
        }
    }
}
