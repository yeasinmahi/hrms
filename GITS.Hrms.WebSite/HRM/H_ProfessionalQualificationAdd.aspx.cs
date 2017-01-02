using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_ProfessionalQualificationAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_PROFESSIONALQUALIFICATION ADD"; }
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
            return "H_ProfessionalQualificationList.aspx?H_EmployeeId=" + Request.QueryString["H_EmployeeId"];
        }

        private H_ProfessionalQualification GetH_ProfessionalQualification()
        {
            H_ProfessionalQualification h_ProfessionalQualification = null;

            if (this.Type == TYPE_EDIT)
            {
                h_ProfessionalQualification = H_ProfessionalQualification.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_ProfessionalQualification = new H_ProfessionalQualification();
                h_ProfessionalQualification.H_EmployeeId = Convert.ToInt32(Request.QueryString["H_EmployeeId"]);
            }

            h_ProfessionalQualification.Certification = DBUtility.ToString(txtCertification.Text);
            h_ProfessionalQualification.InstituteName = DBUtility.ToString(txtInstituteName.Text);
            h_ProfessionalQualification.Location = DBUtility.ToString(txtLocation.Text);
            h_ProfessionalQualification.StartDate = DBUtility.ToDateTime(txtStartDate.Text);
            h_ProfessionalQualification.EndDate = DBUtility.ToDateTime(txtEndDate.Text);
            h_ProfessionalQualification.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);

            return h_ProfessionalQualification;
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
                H_ProfessionalQualification h_ProfessionalQualification = this.GetH_ProfessionalQualification();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [H_ProfessionalQualification]";
                }
                else
                {
                    desc = "Update [H_ProfessionalQualification]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    H_ProfessionalQualification.Insert(this.TransactionManager, h_ProfessionalQualification);

                    hdnId.Value = h_ProfessionalQualification.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    H_ProfessionalQualification.Update(this.TransactionManager, h_ProfessionalQualification);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            H_ProfessionalQualification h_ProfessionalQualification = null;
            H_Employee h_Employee = H_Employee.GetById(Convert.ToInt32(Request.QueryString["H_EmployeeId"]));

            if (h_Employee != null)
            {
                this.txtEmployeeName.Text = h_Employee.Name;
                this.hlBack.NavigateUrl = "~/HRM/H_ProfessionalQualificationList.aspx?H_EmployeeId=" + h_Employee.Id;
            }

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_ProfessionalQualification = H_ProfessionalQualification.GetById(Convert.ToInt32(hdnId.Value));

                if (h_ProfessionalQualification != null)
                {
                    this.Type = TYPE_EDIT;

                    txtCertification.Text = h_ProfessionalQualification.Certification;
                    txtInstituteName.Text = h_ProfessionalQualification.InstituteName;
                    txtLocation.Text = h_ProfessionalQualification.Location;
                    txtStartDate.Text = UIUtility.Format(h_ProfessionalQualification.StartDate);
                    txtEndDate.Text = UIUtility.Format(h_ProfessionalQualification.EndDate);
                    txtSortOrder.Text = UIUtility.Format(h_ProfessionalQualification.SortOrder);
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
