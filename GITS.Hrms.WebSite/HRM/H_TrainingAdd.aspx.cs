using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_TrainingAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_TRAINING ADD"; }
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
            return "H_TrainingList.aspx?H_EmployeeId=" + Request.QueryString["H_EmployeeId"];
        }

        private H_Training GetH_Training()
        {
            H_Training h_Training = null;

            if (this.Type == TYPE_EDIT)
            {
                h_Training = H_Training.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Training = new H_Training();
                h_Training.H_EmployeeId = Convert.ToInt32(Request.QueryString["H_EmployeeId"]);
            }

            h_Training.Title = DBUtility.ToString(txtTitle.Text);
            h_Training.Topics = DBUtility.ToString(txtTopics.Text);
            h_Training.InstituteName = DBUtility.ToString(txtInstituteName.Text);
            h_Training.Country = DBUtility.ToString(txtCountry.Text);
            h_Training.Location = DBUtility.ToString(txtLocation.Text);
            h_Training.TrainingYear = DBUtility.ToString(txtTrainingYear.Text);
            h_Training.Duration = DBUtility.ToNullableString(txtDuration.Text);
            h_Training.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);

            return h_Training;
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
                H_Training h_Training = this.GetH_Training();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [H_Training]";
                }
                else
                {
                    desc = "Update [H_Training]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    H_Training.Insert(this.TransactionManager, h_Training);

                    hdnId.Value = h_Training.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    H_Training.Update(this.TransactionManager, h_Training);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            H_Training h_Training = null;
            H_Employee h_Employee = H_Employee.GetById(Convert.ToInt32(Request.QueryString["H_EmployeeId"]));

            if (h_Employee != null)
            {
                this.txtEmployeeName.Text = h_Employee.Name;
                this.hlBack.NavigateUrl = "~/HRM/H_TrainingList.aspx?H_EmployeeId=" + h_Employee.Id;
            }

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_Training = H_Training.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Training != null)
                {
                    this.Type = TYPE_EDIT;

                    txtTitle.Text = h_Training.Title;
                    txtTopics.Text = h_Training.Topics;
                    txtInstituteName.Text = h_Training.InstituteName;
                    txtCountry.Text = h_Training.Country;
                    txtLocation.Text = h_Training.Location;
                    txtTrainingYear.Text = h_Training.TrainingYear;
                    txtDuration.Text = h_Training.Duration;
                    txtSortOrder.Text = UIUtility.Format(h_Training.SortOrder);
                }
            }
        }

        protected override void HandleCommonCommand(object sender, MenuEventArgs e)
        {
            Message msg = new Message();

            switch (e.Item.Value)
            {
                case COMMAND_SAVE_AND_NEW:
                    msg = this.Save();
                    this.ShowUIMessage(msg);

                    if (msg.Type == MessageType.Information)
                    {
                        UIUtility.Transfer(Page, Request.Path + "?H_EmployeeId=" + Request.QueryString["H_EmployeeId"]);
                    }
                    break;
                default:
                    base.HandleCommonCommand(sender, e);
                    break;
            }
        }
    }
}
