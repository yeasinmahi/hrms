using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Asa.ExcelXmlWriter;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Reports
{
    public partial class H_EmployeeReport : GridPage
    {
        private static DataSet dataset=null;
        private static string query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.SortOrder = "ASC";
        }

        protected override void gvList_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            if (this.SortColumn != e.SortExpression)
            {
                this.SortColumn = e.SortExpression;
                this.SortOrder = "ASC";
            }
            else
            {
                this.SortOrder = (this.SortOrder == "ASC") ? "DESC" : "ASC";
            }
            if (Session["ReportTable"] != null)
            {
                DataTable rt = (DataTable)Session["ReportTable"];
                rt.DefaultView.Sort = this.SortColumn + " " + this.SortOrder;
                rt.DefaultView.ToTable();
                //this.ShowReport();
                gvList.DataSource = rt;
                gvList.DataBind();
            }
            //string sortedQuery = string.Empty;
            //this.TransactionManager = new Asa.Hrms.Data.TransactionManager(false);
            //sortedQuery = query + " ORDER BY " + e.SortExpression+" "+SortOrder;
            //DataSet ds = TransactionManager.GetDataSet(sortedQuery);
            //dataset = ds;
            //gvList.DataSource = ds.Tables[0];
            //gvList.DataBind();
            ////base.gvList_Sorting(sender, e);
            //this.SortOrder = this.SortOrder == "ASC" ? "DESC" : "ASC";
        }

        protected override void HandleSpecialCommand(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {
            this.Validate();

            if (this.IsValid)
            {
                switch (e.Item.Value)
                {
                    case "EXCEL":
                        this.ViewReport();
                        break;
                    case "SEARCH":
                        this.Search();
                        break;
                    default:
                        this.HandleSpecialCommand(sender, e);
                        break;
                }
            }
        }
        private void Search()
        {
            //if (txtEmpId.Text == "" && txtEmpName.Text == "" && txtDesignation.Text == "" && txtBranch.Text == "" && txtAsaDistrict.Text == "" && txtOwnDistrict.Text == "" && txtOwnThana.Text == "" && ddlGrade.SelectedValue == "0")
            //{
            //    Message msg = new Message();
            //    msg.Type = MessageType.Error;
            //    msg.Msg = "Enter value at least one field";
            //    ShowUIMessage(msg);
            //    return;
            //}
            if (!String.IsNullOrEmpty(txtEmpId.Text))
            {
                String[] str = txtEmpId.Text.Split(',');
                foreach (String s in str)
                {
                    int value;
                    if (!int.TryParse(s,out value))
                    {
                        Message msg = new Message();
                        msg.Type = MessageType.Error;
                        msg.Msg = "Invalid Employee ID";
                        ShowUIMessage(msg);
                        return;
                    }
                }
            }
            ShowReport();
        }
        private void ShowReport()
        {
            this.TransactionManager = new TransactionManager(false);
            query = "SELECT e.Code, e.Name AS 'Name',e.DateOfBirth, e.JoiningDate ,H_Grade.Name as Grade,H_EmployeeGrade.StartDate as GradeDate, dbo.H_Designation.Name AS 'Designation'"
                + ", dbo.Branch.Name AS 'BranchName',dbo.H_EmployeeBranch.StartDate as 'BranchDate' "
                + ", dbo.SubZone.Name AS 'ASADistrict' ,dbo.fn_SubzoneDate(e.Id) as 'DistrictDate',wt.Name as [OwnThana],wd.Name AS [OwnDistrict] "
                + ", Status= case when e.Status=1 then 'Working' when e.Status=2 then 'Consultancy' when e.Status=3 then 'In_Leave' when e.Status=4 then 'Dropped' else 'Waiting_For_Posting' end,  dbo.Branch.MobileNumber AS 'BranchMobile'"
                + ", en.Name AS Qualification, pa.Phone AS Personal_Contact"
                + " FROM dbo.H_Employee e"
                + " INNER JOIN dbo.H_Address pa ON e.PresentAddressId=pa.Id"
                + " LEFT JOIN H_AcademicQualification aq ON e.Id=aq.H_EmployeeId and aq.[Id]=(select top(1) Id  from H_AcademicQualification where H_EmployeeId=e.Id Order By SortOrder DESC,Id DESC) " //aq.[Level]=(select MAX(Level) from H_AcademicQualification where H_EmployeeId=e.Id) "
                + " LEFT JOIN ExamName en ON en.Id=aq.ExamNameId "
                + " INNER JOIN dbo.H_EmployeeBranch ON e.Id = dbo.H_EmployeeBranch.H_EmployeeId "
                + " AND (dbo.H_EmployeeBranch.EndDate = '12/31/2099')";
            if (ddlBranchFilter.SelectedValue == "1")
            {
                query += " INNER JOIN dbo.Branch ON dbo.H_EmployeeBranch.BranchId = dbo.Branch.Id AND ( dbo.Branch.Name LIKE '%" + txtBranch.Text + "%')";
            }
            else
            {
                query += " INNER JOIN dbo.Branch ON dbo.H_EmployeeBranch.BranchId = dbo.Branch.Id AND ( dbo.Branch.Name='" + txtBranch.Text + "')";
            }
            query += " INNER JOIN dbo.Region ON dbo.Branch.RegionId = dbo.Region.Id  ";
            if (ddlAsaDistrictFilter.SelectedValue == "1")
            {
                query += " INNER JOIN dbo.SubZone ON dbo.Region.SubZoneId = dbo.SubZone.Id AND (dbo.SubZone.Name LIKE '%"+txtAsaDistrict.Text+"%')"; 
            }
            else
            {
                query += " INNER JOIN dbo.SubZone ON dbo.Region.SubZoneId = dbo.SubZone.Id AND (dbo.SubZone.Name='"+txtAsaDistrict.Text+"')"; 
            }
            query += " INNER JOIN dbo.Thana bt ON dbo.Branch.ThanaId=bt.Id "
                       + " INNER JOIN dbo.H_Address ON e.PermanentAddressId=dbo.H_Address.Id";
            if (ddlOwnThanaFilter.SelectedValue == "1")
            {
                query += " INNER JOIN dbo.Thana wt ON dbo.H_Address.ThanaId=wt.Id AND wt.Name LIKE '%" + txtOwnThana.Text + "%'";
            }
            else
            {
                query += " INNER JOIN dbo.Thana wt ON dbo.H_Address.ThanaId=wt.Id AND wt.Name ='" + txtOwnThana.Text + "'";
            }
            if (ddlOwnDistrictFilter.SelectedValue == "1")
            {
                query += " INNER JOIN dbo.District wd ON wt.DistrictId=wd.Id  AND wd.Name LIKE '%" + txtOwnDistrict.Text + "%'";
            }
            else
            {
                query += " INNER JOIN dbo.District wd ON wt.DistrictId=wd.Id AND wd.Name='" + txtOwnDistrict.Text + "'";
            }
                       
             query += " INNER JOIN dbo.H_EmployeeGrade ON e.Id = dbo.H_EmployeeGrade.H_EmployeeId AND (dbo.H_EmployeeGrade.EndDate = '12/31/2099')";
           
            if (ddlGradeFilter.SelectedValue == "1")
            {
                if (ddlGrade.SelectedValue == "0")
                {
                    query += " INNER JOIN dbo.H_Grade ON dbo.H_EmployeeGrade.H_GradeId = dbo.H_Grade.Id ";
                }
                else
                {
                    query += " INNER JOIN dbo.H_Grade ON dbo.H_EmployeeGrade.H_GradeId = dbo.H_Grade.Id AND (dbo.H_Grade.Id =" + ddlGrade.SelectedValue + ")";
                }
            }
            else
            {

                if (txtGrade.Text != string.Empty)
                {


                    List<string> grades = txtGrade.Text.Split(',').ToList();
                    string values = "";
                    foreach (var value in grades)
                    {
                        values += "'" + value + "',";
                    }
                    values = values.Remove(values.Length - 1, 1);

                    query +=
                        " INNER JOIN dbo.H_Grade ON dbo.H_EmployeeGrade.H_GradeId = dbo.H_Grade.Id AND (dbo.H_Grade.Name in(" +
                        values + "))";
                }
            }
            
            query += " INNER JOIN dbo.H_EmployeeDesignation ON e.Id = dbo.H_EmployeeDesignation.H_EmployeeId AND (dbo.H_EmployeeDesignation.EndDate = '12/31/2099')";
            if (ddlDesigFilter.SelectedValue == "1")
            {
                query += " INNER JOIN dbo.H_Designation ON dbo.H_EmployeeDesignation.H_DesignationId = dbo.H_Designation.Id AND (dbo.H_Designation.Name LIKE '%" +txtDesignation.Text +"%')";        

            }
            else if (ddlDesigFilter.SelectedValue == "2")
            {
                query += " INNER JOIN dbo.H_Designation ON dbo.H_EmployeeDesignation.H_DesignationId = dbo.H_Designation.Id AND (dbo.H_Designation.Name ='" + txtDesignation.Text + "')";        
            }
            else
            {
                if (txtDesignation.Text != string.Empty)
                {


                    List<string> desg = txtDesignation.Text.Split(',').ToList();
                    string values = "";
                    foreach (var value in desg)
                    {
                        values += "'" + value + "',";
                    }
                    values = values.Remove(values.Length - 1, 1);

                    query +=
                        " INNER JOIN dbo.H_Designation ON dbo.H_EmployeeDesignation.H_DesignationId = dbo.H_Designation.Id AND (dbo.H_Designation.Name in(" +
                        values + "))";
                }
            }
            if (ddlStatus.SelectedValue != "0")
            {
                query += " WHERE e.Status=" + ddlStatus.SelectedValue + " and e.Name LIKE '%" + txtEmpName.Text + "%'";
            }
            else
            {
                query += " WHERE e.Name LIKE '%" + txtEmpName.Text + "%'";
            }
            if (ddlEmploymentType.SelectedValue != "-1")
            {
                query += " AND e.EmploymentType=" + ddlEmploymentType.SelectedValue;
            }
            
            if (!string.IsNullOrEmpty(txtEmpId.Text))
            {
                query += " and e.Code IN (" +txtEmpId.Text+")";
            }

            query += this.SortExpression == "" ? "" : " ORDER BY " + this.SortExpression;
            DataSet ds = TransactionManager.GetDataSet(query);
            dataset = ds;
            Session.Remove("ReportTable");
            Session["ReportTable"] = ds.Tables[0];
            gvList.DataSource = ds.Tables[0];
            gvList.DataBind();
        }

        private void ViewReport()
        {
            if (dataset == null)
            {
                Message msg = new Message();
                msg.Type = MessageType.Error;
                msg.Msg = "At First Load Data, then Export ";
                ShowUIMessage(msg);
                    return;
            }
            IList<DataTable> tables = new List<DataTable>();
            IList<IList<WorksheetRow>> headers = new List<IList<WorksheetRow>>();
            WorksheetRow[][] header = new WorksheetRow[dataset.Tables.Count][];
            Int32 i = 0;
            DataSet ds = new DataSet();
            if (Session["ReportTable"] != null)
            {
                ds.Tables.Add((DataTable)Session["ReportTable"]);
            }
            foreach (DataTable dt in ds.Tables)
            {
                tables.Add(dt);

                header[i] = new WorksheetRow[4];
                header[i][0] = new WorksheetRow();
                header[i][1] = new WorksheetRow();
                header[i][2] = new WorksheetRow();
                header[i][3] = new WorksheetRow();

                header[i][0].Cells.Add("Employee Information", DataType.String, "HeaderTop1").MergeAcross = dt.Columns.Count - 1;
                header[i][1].Cells.Add("ASA, Central Office, 23/3 Shyamoli, Dhaka", DataType.String, "HeaderTop3").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2 - 1;
                header[i][2].Cells.Add("", DataType.String, "HeaderTop3").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2 - 1;
                header[i][2].Cells.Add("As On : " + UIUtility.Format(DateTime.Today.Date), DataType.String, "HeaderTop4").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2;

                foreach (DataColumn dc in dt.Columns)
                {
                    header[i][3].Cells.Add(dc.ColumnName, DataType.String, "HeaderLeftAlign");
                }

                i++;
            }

            for (i = 0; i < headers.Count; i++)
            {
                header[i] = headers[i].ToArray();
            }

            ExcelReportUtility.Instance.DataSource = tables.ToArray();
            ExcelReportUtility.Instance.Header = header;
            ExcelReportUtility.Instance.Name = "Emp_Info_" + "(" + DateTime.Now.Ticks + ")" + Configuration.ReportExtension;
            ExcelReportUtility.Instance.ViewReport();
        }
        protected override string  GetAddPageUrl()
        {
 	        throw new NotImplementedException();
        }
        

        //protected override Message Save()
        //{
        //    throw new NotImplementedException();
        //}
        protected override void LoadData()
        {
            this.LoadGrade();
            UIUtility.LoadEnums(ddlStatus,typeof( H_Employee.Statuses), false, false, false);
            ddlStatus.Items.Add(new System.Web.UI.WebControls.ListItem("All", "0", true));
            UIUtility.LoadEnums(ddlEmploymentType, typeof(H_Employee.EmploymentTypes), false, false, false);
            ddlEmploymentType.Items.Insert(0,new System.Web.UI.WebControls.ListItem("All", "-1", true));
            ddlEmploymentType.SelectedValue = "-1";
        }

        private void LoadGrade()
        {
            IList<H_Grade> gradeList = H_Grade.FindAll("Name");
            H_Grade all = new H_Grade();
            all.Id = 0;
            all.Name = "All";
            gradeList.Insert(0, all);
            ddlGrade.DataSource = gradeList;
            ddlGrade.DataBind(); 
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            //if (txtEmpId.Text == "" && txtEmpName.Text == "" && txtDesignation.Text == "" && txtBranch.Text == "" && txtAsaDistrict.Text == "" && txtOwnDistrict.Text == "" && txtOwnThana.Text=="" && ddlGrade.SelectedValue == "0")
            //{
            //    Message msg = new Message();
            //    msg.Type = MessageType.Error;
            //    msg.Msg = "Enter value at least one field";
            //    ShowUIMessage(msg);
            //    return;
            //}
            if(!String.IsNullOrEmpty(txtEmpId.Text))
            {
                String[] str = txtEmpId.Text.Split(',');
                foreach (String s in str)
                {
                    int value;
                    if (!int.TryParse(s, out value))
                    {
                        Message msg = new Message();
                        msg.Type = MessageType.Error;
                        msg.Msg = "Invalid Employee ID";
                        ShowUIMessage(msg);
                        return;
                    }
                }
            }
            ShowReport();
        }

        protected void ddlGradeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGradeFilter.SelectedValue == "1")
            {
                ddlGrade.Visible = true;
                txtGrade.Visible = false;
            }
            else
            {
                ddlGrade.Visible = false;
                txtGrade.Visible = true;
            }
        }
    }
}
