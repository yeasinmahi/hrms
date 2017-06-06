using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_ExperienceAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EXPERIENCE ADD"; }
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
            return "H_ExperienceList.aspx?H_EmployeeId=" + Request.QueryString["H_EmployeeId"];
        }

        private H_Experience GetH_Experience()
        {
            H_Experience h_Experience = null;

            if (Type == TYPE_EDIT)
            {
                h_Experience = H_Experience.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Experience = new H_Experience();
                h_Experience.H_EmployeeId = Convert.ToInt32(Request.QueryString["H_EmployeeId"]);
            }

            h_Experience.CompanyName = DBUtility.ToString(txtCompanyName.Text);
            h_Experience.CompanyBusiness = DBUtility.ToString(txtCompanyBusiness.Text);
            h_Experience.CompanyLocation = DBUtility.ToString(txtCompanyLocation.Text);
            h_Experience.PositionHeld = DBUtility.ToString(txtPositionHeld.Text);
            h_Experience.Department = DBUtility.ToString(txtDepartment.Text);
            h_Experience.Responsibilities = DBUtility.ToString(txtResponsibilities.Text);
            h_Experience.StartDate = DBUtility.ToDateTime(txtStartDate.Text);
            h_Experience.EndDate = DBUtility.ToNullableDateTime(txtEndDate.Text);
            h_Experience.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);

            return h_Experience;
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
                H_Experience h_Experience = GetH_Experience();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [H_Experience]";
                }
                else
                {
                    desc = "Update [H_Experience]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    H_Experience.Insert(TransactionManager, h_Experience);

                    hdnId.Value = h_Experience.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    H_Experience.Update(TransactionManager, h_Experience);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            H_Experience h_Experience = null;
            H_Employee h_Employee = H_Employee.GetById(Convert.ToInt32(Request.QueryString["H_EmployeeId"]));

            if (h_Employee != null)
            {
                txtEmployeeName.Text = h_Employee.Name;
                hlBack.NavigateUrl = "~/HRM/H_ExperienceList.aspx?H_EmployeeId=" + h_Employee.Id;
            }
            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_Experience = H_Experience.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Experience != null)
                {
                    Type = TYPE_EDIT;

                    txtCompanyName.Text = h_Experience.CompanyName;
                    txtCompanyBusiness.Text = h_Experience.CompanyBusiness;
                    txtCompanyLocation.Text = h_Experience.CompanyLocation;
                    txtPositionHeld.Text = h_Experience.PositionHeld;
                    txtDepartment.Text = h_Experience.Department;
                    txtResponsibilities.Text = h_Experience.Responsibilities;
                    txtStartDate.Text = UIUtility.Format(h_Experience.StartDate);
                    txtEndDate.Text = UIUtility.Format(h_Experience.EndDate);
                    txtSortOrder.Text = UIUtility.Format(h_Experience.SortOrder);
                }
            }
        }

        protected override void HandleCommonCommand(object sender, MenuEventArgs e)
        {
            Message msg = new Message();

            switch (e.Item.Value)
            {
                case COMMAND_SAVE_AND_NEW:
                    msg = Save();
                    ShowUiMessage(msg);

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
