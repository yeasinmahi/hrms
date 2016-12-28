using System;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using GITS.Hrms.Library.Data.Procedure;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Reports
{
    public partial class LetterOfIdCard : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void HandleSpecialCommand(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {
            this.Validate();

            if (this.IsValid)
            {
                switch (e.Item.Value)
                {
                    case "PRINT":
                        this.PrintData();
                        break;
                    default:
                        this.HandleSpecialCommand(sender, e);
                        break;
                }
            }
        }

        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        protected override Message Save()
        {
            throw new NotImplementedException();
        }

        protected override void LoadData()
        {

        }

        protected override void PrintData()
        {
            DataTable dt = LetterOfIdCardProcedure.GetDataSet(DBUtility.ToInt32(this.txtStartId.Text), DBUtility.ToInt32(this.txtEndId.Text));

            if (dt.Rows.Count <= 0)
            { 
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";

                ShowUIMessage(msg);
                return;
            }

            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~\\Reports\\LetterOfIdCard_CrystalPage.rpt"));

            rd.SetDataSource(dt);
            Session["note"] = txtNote.Text;
            ReportUtility.ShowReport(this, GetParameters(DBUtility.ToInt32(this.txtStartId.Text), DBUtility.ToInt32(this.txtEndId.Text)), rd, false, false);
        }

        private new Message Validate()
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "";

            base.Validate();

            if (base.IsValid == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid data provided or required data missing";
                return msg;
            }

            return msg;
        }

        private ParameterFields GetParameters(Int32 StartId, Int32 EndId)
        {
            ParameterFields parameters = new ParameterFields();

            parameters.Add(ReportUtility.GetParameter("@StartId", StartId));
            parameters.Add(ReportUtility.GetParameter("@EndId", EndId));

            return parameters;
        }
    }
}