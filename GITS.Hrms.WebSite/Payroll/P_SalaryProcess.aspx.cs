using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Services;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Payroll
{
    public partial class P_SalaryProcess : AddPage
    {
        
        static BackgroundWorker _bw;
        public static int Percent { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            btnStart.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnStart, null) + ";");
        }

        protected override void LoadData()
        {
            Percent = 0;
            int index = 0;
            for (int year = DateTime.Today.Year - 2; year <= DateTime.Today.Year + 2; year++)
            {
                ddlYear.Items.Insert(index++, new ListItem(year.ToString(), year.ToString()));
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
        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            Validate();

            if (IsValid)
            {
                switch (e.Item.Value)
                {
                    case "EXECUTE":
                        //this.ExecuteSalaryProcess();
                        break;
                    default:
                        HandleSpecialCommand(sender, e);
                        break;
                }
            }
        }

        private Message ExecuteSalaryProcess()
        {
            Session["P"] = 1;
            ViewState["PP"] = 1;
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Process Executed Successfully";

            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            P_Process executedProcess = P_Process.Get("SalaryDate='" + endDate.ToString("yyyy-MM-dd") + "'");
            if (executedProcess !=null && executedProcess.IsProcessEnd)
            {
                msg.Type = MessageType.Warning;
                msg.Msg = "Process Already Ended for the month of " + executedProcess.SalaryDate.ToString("MMMM") + "," + executedProcess.SalaryDate.Year.ToString();
                //ShowUIMessage(msg);
                return msg;
            }
            IList<P_LoanAccount> accountList = P_LoanAccount.Find("Status=1", "");
            TransactionManager = new TransactionManager(true);
            //Start Process
            try
            {
                //Delete Previous Data
                if (executedProcess != null)
                {
                    P_LoanDeduction.Delete(TransactionManager, "P_ProcessId=" + executedProcess.Id);
                    P_Process.Delete(TransactionManager, "Id=" + executedProcess.Id);
                }


                P_Process process = new P_Process();
                process.SalaryDate = endDate;
                process.ExecutionDate = DateTime.Today.Date;
                process.UserLogin = User.Identity.Name;
                process.IsProcessEnd = chkProcessEnd.Checked;
                P_Process.Insert(TransactionManager, process);

                P_LoanDeduction deduction;
                Double paidAmt = 0;
                Double deductionAmt = 0;
                int paidInstallment=0;
                Boolean status = true;
                foreach (P_LoanAccount acc in accountList)
                {
                    paidInstallment = acc.PaidInstallment == null ? 0 : acc.PaidInstallment.Value;
                    paidAmt = acc.PaidAmount == null ? 0 : acc.PaidAmount.Value;
                    deductionAmt = (acc.TotalAmount - paidAmt) >= acc.InstallmentAmount ? acc.InstallmentAmount : (acc.TotalAmount - paidAmt);
                    if ((acc.TotalAmount - (paidAmt + deductionAmt) < deductionAmt) && acc.NumberOfInstallment == (paidInstallment+1))
                    {
                        deductionAmt = acc.TotalAmount - paidAmt;
                        status = false;
                    }
                    
                    deduction = new P_LoanDeduction();
                    deduction.P_ProcessId = process.Id;
                    deduction.P_LoanAccountId = acc.Id;
                    deduction.Ammount = deductionAmt;
                    deduction.PaidDate = endDate;
                    P_LoanDeduction.Insert(TransactionManager, deduction);
                    if (process.IsProcessEnd)
                    {
                        acc.PaidAmount = paidAmt + deductionAmt;
                        acc.PaidInstallment = paidInstallment + 1;
                        acc.LastPaidDate = endDate;
                        acc.Status = status ? P_LoanAccount.Statuses.ACTIVE : P_LoanAccount.Statuses.INACTIVE;
                        P_LoanAccount.Update(acc);
                    }

                }

                TransactionManager.Commit();
                
            }
            catch(Exception ex)
            {
                
                TransactionManager.Rollback();
                msg.Type = MessageType.Error;
                msg.Msg = "Execution Failed. " + ex.Message;
                return msg;
                
            }
           // ShowUIMessage(msg);
            return msg;
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            Percent = 0;
            _bw = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _bw.DoWork += bw_DoWork;
            _bw.ProgressChanged += bw_ProgressChanged;
            _bw.RunWorkerCompleted += bw_RunWorkerCompleted;

            _bw.RunWorkerAsync("dfgdfh");
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            btnStart.Enabled = false;
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Process Executed Successfully";
            int max = 0;
            TransactionManager tm = new TransactionManager(false);
            string query="select Distinct H_EmployeeId "
                        +"from P_EmployeeEarning ee "
                        +"INNER JOIN H_Employee e on ee.H_EmployeeId=e.Id "
                        +"where e.Status=1";
            DataTable empList = tm.GetDataSet(query).Tables[0];

            max = empList.Rows.Count;

            int year = Convert.ToInt32(ddlYear.SelectedValue);
            int month = Convert.ToInt32(ddlMonth.SelectedValue);
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            P_Process executedProcess = P_Process.Get("SalaryDate='" + endDate.ToString("yyyy-MM-dd") + "'");
            if (executedProcess != null && executedProcess.IsProcessEnd)
            {
                msg.Type = MessageType.Warning;
                msg.Msg = "Process Already Ended for the month of " + executedProcess.SalaryDate.ToString("MMMM") + "," + executedProcess.SalaryDate.Year.ToString();
                ShowUiMessage(msg);
                return ;
            }

                //query = "INSERT INTO P_Process (SalaryDate,ExecutionDate,UserLogin,IsProcessEnd) Values('"+endDate+"','"+DateTime.Today.Date+"','"+User.Identity.Name+"',"+(chkProcessEnd.Checked ?1 : 0)+") ; select SCOPE_IDENTITY()";
                string cnnString = Configuration.ConnectionString;
                SqlConnection cnn = new SqlConnection(DBUtility.DecryptConnectionString(cnnString));
                SqlTransaction transaction;
                
                cnn.Open();

                transaction = cnn.BeginTransaction();
                SqlCommand cmd;
            
            //Delete Previous Data if exists
                //if (executedProcess != null)
                //{
                //    cmd = cnn.CreateCommand();
                //    string deleteQuery = "DELETE From P_LoanDeduction where P_ProcessId=" + executedProcess.Id + "; "
                //                        + "DELETE FROM P_SalaryDeduction where P_ProcessId=" + executedProcess.Id + "; "
                //                        + "DELETE FROM P_SalaryEarning where P_ProcessId=" + executedProcess.Id + "; "
                //                        + "DELETE from P_Process where Id=" + executedProcess.Id;
                //    cmd.Transaction = transaction;
                //    cmd.CommandText = deleteQuery;
                //    cmd.CommandType = CommandType.Text;
                //    cmd.ExecuteNonQuery();
                //}
            //end delete

                //cmd = cnn.CreateCommand();
                //cmd.Transaction = transaction;
                //cmd.CommandText = query;

                try
                {
                    //SqlDataReader dr = cmd.ExecuteReader();
                    //int ProcessId=0;
                    //while(dr.Read())
                    //{
                    //    ProcessId = Convert.ToInt32(dr[0]);
                    //}
                    //dr.Close();
                    for (int emp = 1; emp <= max; emp++)
                    {
                        cmd = cnn.CreateCommand();
                        cmd.Transaction = transaction;
                        cmd.CommandText = "P_Salary_Process";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int));
                        cmd.Parameters["@Count"].Value = emp;
                        cmd.Parameters.Add(new SqlParameter("@H_EmployeeId", SqlDbType.Int));
                        cmd.Parameters["@H_EmployeeId"].Value = Convert.ToInt32(empList.Rows[emp-1][0]);
                        cmd.Parameters.Add(new SqlParameter("@ProcessEnd", SqlDbType.Bit));
                        cmd.Parameters["@ProcessEnd"].Value = chkProcessEnd.Checked;
                        cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime));
                        cmd.Parameters["@StartDate"].Value = startDate;
                        cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime));
                        cmd.Parameters["@EndDate"].Value = endDate;
                        cmd.Parameters.Add(new SqlParameter("@User", SqlDbType.Text));
                        cmd.Parameters["@User"].Value = User.Identity.Name;
                        cmd.ExecuteNonQuery();

                        if (_bw.CancellationPending) { e.Cancel = true; return; }
                        _bw.ReportProgress((emp * 100) / max);
                        Thread.Sleep(100);

                    }
                    transaction.Commit();
                }
                catch (Exception sqlEx)
                {
                    transaction.Rollback();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Process Execution Failed, Contact Administrator:" + sqlEx.Message;
                    ShowUiMessage(msg);
                    e.Cancel = true;
                   // return;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }

            ShowUiMessage(msg);
            e.Result = "Process Completed Successfully.";
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //percentage.Text = "Complete: " + e.Result;      // from DoWork
            btnStart.Enabled = true;
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Percent = e.ProgressPercentage;
           // procressList.Items.Add(e.ProgressPercentage.ToString());
        }

        [WebMethod]
        public static int GetData()
        {
            return Percent;
        }
    }
}
