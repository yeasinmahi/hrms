using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class ProcessExecution : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            
        }

        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        protected override Message Save()
        {
            throw new NotImplementedException();
        }
        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            this.Validate();

            if (this.IsValid)
            {
                switch (e.Item.Value)
                {
                    case "EXECUTE":
                        this.ExecuteProcess();
                        break;
                    default:
                        this.HandleSpecialCommand(sender, e);
                        break;
                }
            }
        }

        private void ExecuteProcess()
        {
            Message ms = new Message();
            ms.Type = MessageType.Information;
            int row = 0;
            TransactionManager tm = new TransactionManager(false);
            row = tm.ExecuteUpdate("Process_Employee_Transfer");//Process_Employee_Transfer");
            ms.Msg = "Process Executed Successfully, " + Convert.ToString(row) + " row(s) affected.";
            //try
            //{
            //    row = tm.ExecuteUpdate("TestProc");//Process_Employee_Transfer");
            //    ms.Msg = "Process Executed Successfully, " + Convert.ToString(row) + " row(s) affected.";
            //    tm.Commit();
            //}
            //catch
            //{
            //    tm.Rollback();
            //    ms.Msg = "Process Execution Failed , " + Convert.ToString(row) + " row(s) affected.";
            //    ms.Type = MessageType.Error;
            //}

            ShowUIMessage(ms);
            
        }
    }
}
