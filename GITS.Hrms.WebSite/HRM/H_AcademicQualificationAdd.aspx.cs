using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_AcademicQualificationAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_ACADEMICQUALIFICATION ADD"; }
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
            return "H_AcademicQualificationList.aspx?H_EmployeeId=" + Request.QueryString["H_EmployeeId"];
        }

        private H_AcademicQualification GetH_AcademicQualification()
        {
            H_AcademicQualification h_AcademicQualification = null;

            if (Type == TYPE_EDIT)
            {
                h_AcademicQualification = H_AcademicQualification.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_AcademicQualification = new H_AcademicQualification();
                h_AcademicQualification.H_EmployeeId = Convert.ToInt32(Request.QueryString["H_EmployeeId"]);
            }

            h_AcademicQualification.Level = DBUtility.ToInt32(ddlLevel.SelectedValue);
            h_AcademicQualification.GroupSubjectId = DBUtility.ToInt32(ddlGroupSubject.SelectedValue);
            h_AcademicQualification.ExamNameId = DBUtility.ToInt32(ddlExam.SelectedValue);
            h_AcademicQualification.Institution = DBUtility.ToNullableString(txtInstitution.Text);
            h_AcademicQualification.BoardUniversityId = DBUtility.ToInt32(ddlBoardUniversity.SelectedValue);
            h_AcademicQualification.Result = DBUtility.ToString(ddlResult.SelectedValue);
            h_AcademicQualification.PassingYear = DBUtility.ToInt32(txtPassingYear.Text);
            if (ddlResult.SelectedValue == "Grade")
            {
                h_AcademicQualification.GPA = DBUtility.ToNullableDouble(txtGrade.Text);
            }
            else
            {
                h_AcademicQualification.GPA = null;
            }
            h_AcademicQualification.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);

            return h_AcademicQualification;
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
            if (Type == TYPE_ADD)
            {
                IList<UserRole> role = UserRole.FindByUserLogin(User.Identity.Name, "");
                bool Permitted = false;
                foreach (UserRole ur in role)
                {
                    if (ur.RoleName.ToLower() == "employee")
                    {
                        Permitted = true;
                    }
                }
                if (!Permitted)
                {
                    H_Employee emp = H_Employee.GetById(Convert.ToInt32(Request.QueryString["H_EmployeeId"]));
                    if (DateTime.Today.Date > emp.JoiningDate.Value.AddMonths(1))
                    {
                        msg.Type = MessageType.Error;
                        msg.Msg = "You are not permitted to add new Education of this Employee, Contact HR";
                        return msg;
                    }
                }
            }
            if (Type == TYPE_EDIT)
            {
                IList<UserRole> role = UserRole.FindByUserLogin(User.Identity.Name, "");
                bool Permitted = false;
                foreach (UserRole ur in role)
                {
                    if (ur.RoleName.ToLower() == "employee")
                    {
                        Permitted = true;
                    }
                }
                if (!Permitted)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "You Have no Update Permission";
                    return msg;
                }
            }

            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                H_AcademicQualification h_AcademicQualification = GetH_AcademicQualification();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [H_AcademicQualification]";
                }
                else
                {
                    desc = "Update [H_AcademicQualification]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    H_AcademicQualification.Insert(TransactionManager, h_AcademicQualification);

                    hdnId.Value = h_AcademicQualification.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    H_AcademicQualification.Update(TransactionManager, h_AcademicQualification);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            rfvGrade.Enabled = false;
            txtGrade.Visible = false;
            UIUtility.LoadEnums(ddlLevel, typeof(H_AcademicQualification.Levels), false, false, false);
            ddlExam.DataSource = ExamName.FindAll("Name");
            ddlExam.DataBind();
            ddlBoardUniversity.DataSource = BoardUniversity.FindAll("Name");
            ddlBoardUniversity.DataBind();
            ddlGroupSubject.DataSource = GroupSubject.FindAll("Name");
            ddlGroupSubject.DataBind();
            H_AcademicQualification h_AcademicQualification = null;
            H_Employee h_Employee = H_Employee.GetById(Convert.ToInt32(Request.QueryString["H_EmployeeId"]));

            if (h_Employee != null)
            {
                txtEmployeeName.Text = h_Employee.Name;
                hlBack.NavigateUrl = "~/HRM/H_AcademicQualificationList.aspx?H_EmployeeId=" + h_Employee.Id;
            }

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_AcademicQualification = H_AcademicQualification.GetById(Convert.ToInt32(hdnId.Value));

                if (h_AcademicQualification != null)
                {
                    Type = TYPE_EDIT;

                    ddlLevel.SelectedValue = h_AcademicQualification.Level.ToString();
                    ddlExam.SelectedValue = h_AcademicQualification.ExamNameId.ToString();
                    ddlGroupSubject.SelectedValue = h_AcademicQualification.GroupSubjectId.ToString();
                    txtInstitution.Text = h_AcademicQualification.Institution;
                    ddlBoardUniversity.SelectedValue = h_AcademicQualification.BoardUniversityId.ToString();
                    ddlResult.SelectedValue = h_AcademicQualification.Result;
                    if (h_AcademicQualification.Result == "Grade")
                    {
                        txtGrade.Visible = true;
                        rfvGrade.Enabled = true;
                        txtGrade.Text = h_AcademicQualification.GPA.ToString();
                    }
                    else
                    {
                        rfvGrade.Enabled = false;
                        txtGrade.Visible = false;
                    }
                    txtPassingYear.Text = h_AcademicQualification.PassingYear.ToString();
                    txtSortOrder.Text = h_AcademicQualification.SortOrder.ToString();
                    //txtTitle.Text = h_AcademicQualification.Title;
                    //txtMajor.Text = h_AcademicQualification.Major;
                    //txtInstituteName.Text = h_AcademicQualification.InstituteName;
                    //txtResult.Text = h_AcademicQualification.Result;
                    //txtPassingYear.Text = h_AcademicQualification.PassingYear;
                    //txtDuration.Text = h_AcademicQualification.Duration;
                    //txtAchievement.Text = h_AcademicQualification.Achievement;
                    //txtSortOrder.Text = UIUtility.Format(h_AcademicQualification.SortOrder);
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

        protected void ddlResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlResult.SelectedValue == "Grade")
            {
                rfvGrade.Enabled = true;
                txtGrade.Visible = true;
                txtGrade.Focus();
            }
            else
            {
                rfvGrade.Enabled = false;
                txtGrade.Visible = false;
            }
        }
    }
}
