using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Asa.Hrms.Web;
using Asa.Hrms.Data.Entity;
using System.Data;
using Asa.Hrms.Data.Procedure;
using CrystalDecisions.CrystalReports.Engine;
using Asa.Hrms.Utility;

namespace Asa.Hrms.WebSite.Payroll
{
    public partial class P_SalaryCardReport : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            int index = 0;
            for (int year = DateTime.Today.Year - 2; year <= DateTime.Today.Year + 2; year++)
            {
                ddlYear.Items.Insert(index++, new ListItem(year.ToString(), year.ToString()));
            }
        }
        protected override void PrintData()
        {
            DateTime salaryDate = (new DateTime(Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlMonth.SelectedValue), 1)).AddMonths(1).AddDays(-1);
            P_Process process = P_Process.GetBySalaryDate(salaryDate);
            if (process != null)
            {
                DataTable dt = P_Salary_Card_Procedure.GetDataSet(process.Id);
                string reportName = "P_Salary_Card.rpt";
                ReportDocument rd = new ReportDocument();
                rd.Load(Server.MapPath("~/Reports/" + reportName));
                rd.SetDataSource(dt);
                Session["ReportType"] = Convert.ToInt32(1);
                ReportUtility.ViewReport(this, rd, false, false);
            }
            else
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "No Data Found for the Month of " + salaryDate.ToString("MMMM, yyyy");
                ShowUIMessage(msg);
            }
        }
        
        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        protected override Asa.Hrms.Utility.Message Save()
        {
            throw new NotImplementedException();
        }
    }
}
