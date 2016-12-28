using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data.Procedure;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Reports
{
    public partial class HRM_SummeryReport : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
         protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }
         protected override void HandleSpecialCommand(object sender, System.Web.UI.WebControls.MenuEventArgs e)
         {
             this.Validate();

             if (this.IsValid)
             {
                 switch (e.Item.Value)
                 {
                     case "SEARCH":
                         this.Search();
                         break;
                     case "REFRESH":
                         this.PunishmentReport();
                         break;
                     case "EXCEL":
                         this.MaleFemaleReport(1); //Male
                         pnlreport.GroupingText = "Male Summery Report";
                         break;
                     case "UPGRADE":
                         this.LoanOfficer(); // 1 for Designation Type LO
                         break;
                     case "RESTORE":
                         this.BranchManager(); 
                         break;
                     case "BACKUP":
                         this.RegionalManager(); 
                         break;
                     case "EXECUTE":
                         this.DistrictManager();
                         break;
                     case "SHRINK":
                         this.ZonalManager(); 
                         break;
                     default:
                         this.HandleSpecialCommand(sender, e);
                         break;
                 }
             }
         }

         private void RegionalManager()
         {
             this.EmployeeSummery(4,0);
             pnlreport.GroupingText = "Regional Manager Summery";
         }

         private void DistrictManager()
         {
             this.EmployeeSummery(5,6);
             pnlreport.GroupingText = "District Manager Summery";
         }

         private void ZonalManager()
         {
             pnlreport.GroupingText = "Zonal Manager Summery";
            
             
         }

         private void BranchManager()
         {
             this.EmployeeSummery(2,0);
             pnlreport.GroupingText = "Branch Manager Summery";
         }

         private void LoanOfficer()
         {
             this.EmployeeSummery(1,0);
             pnlreport.GroupingText = "Loan Officer Summery";
         }

         private void EmployeeSummery(int GroupType1, int GroupType12)
         {
             DataTable dt = null;
             dt =SubzoneWiseEmpoyeeSummery.GetDataSet(GroupType1,GroupType12);
             Table table = new Table();
             table.BorderWidth = 1;
             table.BorderColor = System.Drawing.Color.Black;
             table.CellSpacing = 0;
             TableRow tr = new TableRow();
             tr.BackColor = System.Drawing.Color.Silver;
             tr.Font.Bold = true;
             tr.Style["text-align"] = "center";
             TableCell tc = new TableCell();
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 20; tc.Text = "SL#"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "District"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Branch"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Male"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Female"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Total"; tr.Cells.Add(tc);
             table.Rows.Add(tr);

             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 tr = new TableRow();
                 tr.Style["text-align"] = "right";
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = (i + 1).ToString(); //SL no
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][0].ToString();
                 tc.Style["text-align"] = "left";
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][1].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][2].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][3].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][4].ToString();
                 tr.Cells.Add(tc);

                 table.Rows.Add(tr);
             }

             tr = new TableRow();
             tr.Style["text-align"] = "right";
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = ""; //SL no
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = "Total";
             tc.Style["text-align"] = "left";
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("Branch")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("Male")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("Female")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("Total")).ToString();
             tr.Cells.Add(tc);
             table.Rows.Add(tr);
             pnlreport.Controls.Add(table);
             pnlreport.BackColor = System.Drawing.Color.White;
         }
         protected override void PrintData()
         {
             this.MaleFemaleReport(2); // Female
             pnlreport.GroupingText = "Female Summery Report";
         }
         private void MaleFemaleReport(int mfFlag)
         {
             DateTime AsOndate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
             DataTable dt = null;
             dt = MaleFemaleSummery.GetDataSet(mfFlag,AsOndate);// mfFlag=1 for Male 2 for Female
             Table table = new Table();
             table.BorderWidth = 1;
             table.BorderColor = System.Drawing.Color.Black;
             table.CellSpacing = 0;
             TableRow tr = new TableRow();
             tr.BackColor = System.Drawing.Color.Silver;
             tr.Font.Bold = true;
             tr.Style["text-align"] = "center";
             TableCell tc = new TableCell(); 
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 20; tc.Text = "SL#"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "District"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Branch"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "LO"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "ABM"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "BM"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "RM"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "DM"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Auditor"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Others"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Total Staff"; tr.Cells.Add(tc);
             table.Rows.Add(tr);

             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 tr = new TableRow();
                 tr.Style["text-align"] = "right";
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = (i + 1).ToString(); //SL no
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][0].ToString();
                 tc.Style["text-align"] = "left";
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][1].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][2].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][3].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][4].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][5].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][6].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][7].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][8].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][9].ToString();
                 tr.Cells.Add(tc);
                 table.Rows.Add(tr);
             }
             tr = new TableRow();
             tr.Style["text-align"] = "right";
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = "";
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = "Total";
             tc.Style["text-align"] = "left";
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("Branch")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("LO")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("ABM")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("BM")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("RM")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("DM")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("Auditor")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("Others")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<int>("Total_Staff")).ToString();
             tr.Cells.Add(tc);
             table.Rows.Add(tr);
             pnlreport.Controls.Add(table);
             pnlreport.BackColor = System.Drawing.Color.White;
             
         }

         private void PunishmentReport()
         {
             DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
             DateTime Enddate = startDate.AddMonths(1).AddDays(-1);
             DataTable dt = null;
             dt = PunishmentSummery.GetDataSet(startDate, Enddate);
             Table table = new Table();
             table.BorderWidth = 1;
             table.BorderColor = System.Drawing.Color.Black;
             table.CellSpacing = 0;
             //Header Level 1
             TableRow tr = new TableRow();
             tr.BackColor = System.Drawing.Color.Silver;
             tr.Font.Bold = true;
             tr.Style["text-align"] = "center";
             TableCell tc = new TableCell(); tc.BorderWidth = 1; tc.RowSpan = 2; tc.Text = "SL#"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.RowSpan = 2; tc.Text = "District"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.ColumnSpan = 2; tc.Text = "Penalty"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.ColumnSpan = 2; tc.Text = "Fine"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.ColumnSpan = 2; tc.Text = "Warning"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.ColumnSpan = 2; tc.Text = "Increment Heldup"; tr.Cells.Add(tc);
             table.Rows.Add(tr);
             //Header Level 2
             tr = new TableRow();
             tr.BackColor = System.Drawing.Color.Silver;
             tr.Font.Bold = true;
             tr.Style["text-align"] = "center";
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "No.of Penalty"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Taka"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "No.of Fine"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Taka"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Person"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Times"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Person"; tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Width = 80; tc.Text = "Times"; tr.Cells.Add(tc);
             table.Rows.Add(tr);

             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 tr = new TableRow();
                 tr.Style["text-align"] = "right";
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = (i + 1).ToString(); //SL no
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][0].ToString();
                 tc.Style["text-align"] = "left";
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][1].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][2].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][3].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][4].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][5].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][6].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][7].ToString();
                 tr.Cells.Add(tc);
                 tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[i][8].ToString();
                 tr.Cells.Add(tc);
                 table.Rows.Add(tr);
             }
             tr = new TableRow();
             tr.Style["text-align"] = "right";
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = "";
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = "Total";
             tc.Style["text-align"] = "left";
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<Nullable<int>>("PNo")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<Nullable<Double>>("PTaka")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<Nullable<int>>("FNo")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<Nullable<Double>>("FTaka")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<Nullable<int>>("WPer")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<Nullable<int>>("WNo")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<Nullable<int>>("incPer")).ToString();
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.AsEnumerable().Sum(x => x.Field<Nullable<int>>("incNo")).ToString();
             tr.Cells.Add(tc);
             table.Rows.Add(tr);

             pnlreport.Controls.Add(table);
             pnlreport.BackColor = System.Drawing.Color.White;
             pnlreport.GroupingText = "Punishment Summery Report";
         }
         protected override void Search()
         {
             DateTime AsOnDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
             DataTable dt = null;
             dt = AtaGlanceTotalStaffPositionProcedure.GetDataSet(AsOnDate);
             Table table = new Table();
             table.BorderWidth = 1;
             table.BorderColor = System.Drawing.Color.Black;
             table.CellSpacing = 0;
             TableRow tr = new TableRow();
             tr.BackColor = System.Drawing.Color.Silver;
             Label lbl = new Label(); lbl.Width = 200; lbl.Font.Bold = true; lbl.Text = "Category of Staff"; lbl.ID = "cat";
             TableCell tc = new TableCell(); tc.BorderWidth = 1; tr.Cells.Add(tc); tc.Controls.Add(lbl);
             tr.Cells.Add(tc);
             lbl = new Label(); lbl.Width = 200; lbl.Font.Bold = true; lbl.Text = "No of Staff"; lbl.ID = "No";
             tc = new TableCell(); tc.BorderWidth = 1; tr.Cells.Add(tc); tc.Controls.Add(lbl);
             tr.Cells.Add(tc);
             table.Rows.Add(tr);

             tr = new TableRow();
             lbl = new Label(); lbl.Width = 200; lbl.Text = "Presently Working"; lbl.ID = "lblWorking";
             tc = new TableCell(); tc.BorderWidth = 1; tr.Cells.Add(tc); tc.Controls.Add(lbl);
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[0][0].ToString();
             tr.Cells.Add(tc);
             table.Rows.Add(tr);

             tr = new TableRow();
             lbl = new Label(); lbl.Width = 200; lbl.Text = "Total Saff"; lbl.ID = "T";
             tc = new TableCell(); tc.BorderWidth = 1; tr.Cells.Add(tc); tc.Controls.Add(lbl);
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[0][1].ToString();
             tr.Cells.Add(tc);
             table.Rows.Add(tr);

             tr = new TableRow();
             lbl = new Label(); lbl.Width = 200; lbl.Text = "Total Male Staff"; lbl.ID = "m";
             tc = new TableCell(); tc.BorderWidth = 1; tr.Cells.Add(tc); tc.Controls.Add(lbl);
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[0][2].ToString();
             tr.Cells.Add(tc);
             table.Rows.Add(tr);

             tr = new TableRow();
             lbl = new Label(); lbl.Width = 200; lbl.Text = "Total Female Staff"; lbl.ID = "fm";
             tc = new TableCell(); tc.BorderWidth = 1; tr.Cells.Add(tc); tc.Controls.Add(lbl);
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[0][3].ToString();
             tr.Cells.Add(tc);
             table.Rows.Add(tr);

             tr = new TableRow();
             lbl = new Label(); lbl.Width = 200; lbl.Text = "Ratio of Male & Female Staff"; lbl.ID = "dfr";
             tc = new TableCell(); tc.BorderWidth = 1; tr.Cells.Add(tc); tc.Controls.Add(lbl);
             tr.Cells.Add(tc);
             tc = new TableCell(); tc.BorderWidth = 1; tc.Text = dt.Rows[0][4].ToString() + " : " + dt.Rows[0][5].ToString();
             tr.Cells.Add(tc);
             table.Rows.Add(tr);

             pnlreport.Controls.Add(table);
             pnlreport.BackColor = System.Drawing.Color.White;

             pnlreport.BackColor = System.Drawing.Color.White;
             pnlreport.GroupingText = "At a Glance";
         }
        protected override Message Save()
        {
            throw new NotImplementedException();
        }
        private new Message Validate()
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Record saved successfully.";

            base.Validate();

            //if (base.IsValid == false)
            //{
            //    msg.Type = MessageType.Error;
            //    msg.Msg = "Invalid data provided or required data missing";
            //    return msg;
            //}
            return msg;
        }
        protected override void LoadData()
        {
            trReportName.Visible = false;
            rfvReportName.Enabled = false;
            txtAsOnDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            Int32 j;
            for (j = DateTime.Today.Year; j >= DateTime.Today.Year - 5; j--)
            {
                ddlYear.Items.Add(j.ToString());
            }
            ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
            trAsonDate.Visible = false;
            trMonth.Visible = false;
            trYear.Visible = false;
        }
        protected void ddlReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportName.SelectedValue == "1")
            {
                trYear.Visible = false;
                trMonth.Visible = false;
                trAsonDate.Visible = true;
                txtAsOnDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            else if (ddlReportName.SelectedValue == "2")
            {
                trYear.Visible = true;
                trMonth.Visible = true;
                trAsonDate.Visible = false;
                ddlYear.SelectedValue = DateTime.Today.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
            }
            else
            {
                trYear.Visible = false;
                trMonth.Visible = false;
                trAsonDate.Visible = false;
            }
        }
    }
}
