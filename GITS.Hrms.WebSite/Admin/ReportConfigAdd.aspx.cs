using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class ReportConfigAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "REPORTCONFIG ADD"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        { }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override string GetListPageUrl()
        {
            return "ReportConfigList.aspx";
        }

        private ReportConfig GetReportConfig()
        {
            ReportConfig reportConfig = null;

            if (Type == TYPE_EDIT)
            {
                reportConfig = ReportConfig.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                reportConfig = new ReportConfig();
            }

            reportConfig.Name = DBUtility.ToString(txtName.Text);
            reportConfig.Type = (ReportConfig.ReportType)DBUtility.ToInt32(ddlReportType.SelectedValue);
            reportConfig.Location = chkLocation.Checked;
            reportConfig.Position = chkPosition.Checked;
            reportConfig.ReligionAndSex = chkReligionAndSex.Checked;
            reportConfig.DateBetween = DBUtility.ToInt32(rdoDate.SelectedValue); // chkDateBetween.Checked;
            reportConfig.Query = DBUtility.ToString(txtQuery.Text);

            return reportConfig;
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
                ReportConfig reportConfig = GetReportConfig();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [ReportConfig]";
                }
                else
                {
                    desc = "Update [ReportConfig]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    ReportConfig.Insert(TransactionManager, reportConfig);

                    hdnId.Value = reportConfig.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    ReportConfig.Update(TransactionManager, reportConfig);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            ReportConfig reportConfig = null;

            UIUtility.LoadEnums(ddlReportType, typeof(ReportConfig.ReportType), false, false, true);

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                reportConfig = ReportConfig.GetById(Convert.ToInt32(hdnId.Value));

                if (reportConfig != null)
                {
                    Type = TYPE_EDIT;

                    txtName.Text = reportConfig.Name;
                    if (reportConfig.Type == 0)
                    {
                        lblReportType.Visible = false;
                        ddlReportType.Visible = false;
                    }
                    else
                    {
                        lblReportType.Visible = true;
                        ddlReportType.Visible = true;
                        ddlReportType.SelectedValue = ((Int32)reportConfig.Type).ToString();
                    }
                    chkLocation.Checked = reportConfig.Location;
                    chkPosition.Checked = reportConfig.Position;
                    chkReligionAndSex.Checked = reportConfig.ReligionAndSex;
                    //chkDateBetween.Checked = reportConfig.DateBetween;
                    rdoDate.SelectedValue = reportConfig.DateBetween.ToString();
                    txtQuery.Text = reportConfig.Query;
                }
            }
        }
    }
}
