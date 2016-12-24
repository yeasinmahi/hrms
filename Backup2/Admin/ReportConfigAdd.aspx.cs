using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

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

	private Asa.Hrms.Data.Entity.ReportConfig GetReportConfig()
	{
		Asa.Hrms.Data.Entity.ReportConfig reportConfig = null;

		if (this.Type == TYPE_EDIT)
		{
			reportConfig = Asa.Hrms.Data.Entity.ReportConfig.GetById(Convert.ToInt32(hdnId.Value));
		}
		else
		{
			reportConfig = new Asa.Hrms.Data.Entity.ReportConfig();
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
			Asa.Hrms.Data.Entity.ReportConfig reportConfig = this.GetReportConfig();
			string desc = "";

			if (this.Type == TYPE_ADD)
			{
				desc = "Insert [ReportConfig]";
			}
			else
			{
				desc = "Update [ReportConfig]";
			}

			this.TransactionManager = new TransactionManager(true, desc);

			if (this.Type == TYPE_ADD)
			{
				Asa.Hrms.Data.Entity.ReportConfig.Insert(this.TransactionManager, reportConfig);

				hdnId.Value = reportConfig.Id.ToString();
				this.Type = TYPE_EDIT;
			}
			else
			{
				Asa.Hrms.Data.Entity.ReportConfig.Update(this.TransactionManager, reportConfig);
			}

			this.TransactionManager.Commit();
		}

		return msg;
	}

	protected override void LoadData()
	{
		Asa.Hrms.Data.Entity.ReportConfig reportConfig = null;

        UIUtility.LoadEnums(ddlReportType, typeof(ReportConfig.ReportType), false, false, true);

		if (Request.QueryString["Id"] != null)
		{
			hdnId.Value = Request.QueryString["Id"];
			reportConfig = Asa.Hrms.Data.Entity.ReportConfig.GetById(Convert.ToInt32(hdnId.Value));

			if (reportConfig != null)
			{
				this.Type = TYPE_EDIT;

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
