using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeGradeAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEEGRADE ADD"; }
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
            return "H_EmployeeGradeList.aspx";
        }

        private H_EmployeeGrade GetH_EmployeeGrade()
        {
            H_EmployeeGrade h_EmployeeGrade = null;

            if (this.Type == TYPE_EDIT)
            {
                h_EmployeeGrade = H_EmployeeGrade.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_EmployeeGrade = new H_EmployeeGrade();
            }

            h_EmployeeGrade.H_EmployeeId = DBUtility.ToInt32(ddlH_EmployeeId.SelectedValue);
            h_EmployeeGrade.H_GradeId = DBUtility.ToInt32(ddlH_GradeId.SelectedValue);
            h_EmployeeGrade.StartDate = DBUtility.ToDateTime(txtStartDate.Text);
            h_EmployeeGrade.EndDate = DBUtility.ToDateTime(txtEndDate.Text);

            return h_EmployeeGrade;
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
                H_EmployeeGrade h_EmployeeGrade = this.GetH_EmployeeGrade();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [H_EmployeeGrade]";
                }
                else
                {
                    desc = "Update [H_EmployeeGrade]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    H_EmployeeGrade.Insert(this.TransactionManager, h_EmployeeGrade);

                    hdnId.Value = h_EmployeeGrade.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    H_EmployeeGrade.Update(this.TransactionManager, h_EmployeeGrade);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            H_EmployeeGrade h_EmployeeGrade = null;

            this.ddlH_EmployeeId.DataSource = H_Employee.FindAll();
            this.ddlH_EmployeeId.DataBind();

            this.ddlH_GradeId.DataSource = H_Grade.FindAll();
            this.ddlH_GradeId.DataBind();

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_EmployeeGrade = H_EmployeeGrade.GetById(Convert.ToInt32(hdnId.Value));

                if (h_EmployeeGrade != null)
                {
                    this.Type = TYPE_EDIT;

                    ddlH_EmployeeId.SelectedValue = UIUtility.Format(h_EmployeeGrade.H_EmployeeId);
                    ddlH_GradeId.SelectedValue = UIUtility.Format(h_EmployeeGrade.H_GradeId);
                    txtStartDate.Text = UIUtility.Format(h_EmployeeGrade.StartDate);
                    txtEndDate.Text = UIUtility.Format(h_EmployeeGrade.EndDate);
                }
            }
        }
    }
}
