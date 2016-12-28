using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Asa.ExcelXmlWriter;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.Procedure;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Reports
{
    public partial class QuickSummaryReport : AddPage
    {
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
                    default:
                        this.HandleSpecialCommand(sender, e);
                        break;
                }
            }
        }

        private void ViewReport()
        {
            Message msg = new Message();
            ReportConfig report = ReportConfig.GetById(DBUtility.ToInt32(ddlReport.SelectedValue));

            if (report.Name.Contains("At A Glance Report"))
            {
                this.AtAGlanceReport(report.Query);
            }

            if (report.Name.Contains("Staff Information"))
            {
                this.StaffInformation(report.Query);
            }
        }

        private void AtAGlanceReport(string queryString)
        {
            Nullable<Int32> zoneId = DBUtility.ToInt32(ddlZone.SelectedValue);
            Nullable<Int32> subzoneId = DBUtility.ToInt32(ddlSubzone.SelectedValue);
            Nullable<Int32> regionId = DBUtility.ToInt32(ddlRegion.SelectedValue);
            Nullable<Int32> branchCode = DBUtility.ToInt32(ddlBranch.SelectedValue);
            Nullable<Int32> gradeId = DBUtility.ToInt32(ddlGrade.SelectedValue);
            Nullable<Int32> designationId = DBUtility.ToInt32(ddlDesignation.SelectedValue);

            DateTime endDate = Configuration.EndDate = Convert.ToDateTime(txtAsOnDate.Text).Date;

            this.TransactionManager = new TransactionManager(false);

            DataSet ds = this.TransactionManager.GetDataSet(String.Format(queryString,
                "'" + endDate.ToString(Configuration.DatabaseDateFormat) + "'",
                zoneId == 0 ? "NULL" : zoneId.ToString(),
                subzoneId == 0 ? "NULL" : subzoneId.ToString(),
                regionId == 0 ? "NULL" : regionId.ToString(),
                branchCode == 0 ? "NULL" : branchCode.ToString(),
                gradeId == 0 ? "NULL" : gradeId.ToString(),
                designationId == 0 ? "NULL" : designationId.ToString()));

            IList<WorksheetRow> header = new List<WorksheetRow>();
            header.Add(new WorksheetRow());
            header.Add(new WorksheetRow());
            header.Add(new WorksheetRow());
            header.Add(new WorksheetRow());

            Int32 l1 = 2;
            Int32 l3 = 3;
            Int32 l4 = 1;

            header[0].Cells.Add("At A Glance Report", DataType.String, "HeaderTop1").MergeAcross = 2;
            header[l4].Cells.Add("As on: " + UIUtility.Format(endDate.Date), DataType.String, "HeaderTop4").MergeAcross = 1;
            header[l1].Cells.Add(UIUtility.GetHeader(zoneId, subzoneId, regionId, branchCode), DataType.String, "HeaderTop3").MergeAcross = 1;
            header[l3].Cells.Add(UIUtility.GetPositionHeader(gradeId, designationId), DataType.String, "HeaderTop3").MergeAcross = 1;

            WorksheetRow[][] displayheader = new WorksheetRow[1][];
            displayheader[0] = header.ToArray();

            DataTable result = new DataTable();

            DataColumn dc = result.Columns.Add("Description", typeof(String));
            dc.Caption = "Description";
            dc = result.Columns.Add("Column1", typeof(String));
            dc.Caption = "";

            Int32 maleStaff = DBUtility.ToInt32(ds.Tables[0].Rows[0][1].ToString());
            Int32 femaleStaff = DBUtility.ToInt32(ds.Tables[0].Rows[0][2].ToString());

            result.Rows.Add(new object[] { "", "" });
            result.Rows.Add(new object[] { "Total Staff", ds.Tables[0].Rows[0][0].ToString() });
            result.Rows.Add(new object[] { "", "" });
            result.Rows.Add(new object[] { "Total Male Staff", maleStaff });
            result.Rows.Add(new object[] { "Total Female Staff", femaleStaff });
            result.Rows.Add(new object[] { "", "" });
            result.Rows.Add(new object[] { "Ratio of Male & Female Staff", (femaleStaff == 0 ? maleStaff : maleStaff / femaleStaff) + (femaleStaff == 0 ? ": 0" : " : 1") });

            result.Columns[0].ExtendedProperties.Add("Width", 300);
            result.Columns[1].ExtendedProperties.Add("Width", 100);

            result.TableName = "At A Glance";
            ExcelReportUtility.Instance.DataSource = result;
            ExcelReportUtility.Instance.Header = displayheader;
            ExcelReportUtility.Instance.Name = result.TableName + "(" + DateTime.Now.Ticks + ")" + Configuration.ReportExtension;
            ExcelReportUtility.Instance.BoldRow += new ExcelReportUtility.RowEventHandler(QuickReport_BoldRow);
            ExcelReportUtility.Instance.ViewReport();
        }

        private void StaffInformation(string queryString)
        {
            DateTime endDate = Configuration.EndDate = Convert.ToDateTime(txtAsOnDate.Text).Date;

            DataTable result = new DataTable();
            int index = 1;

            DataTable minorityInformation = MinoritiesInformationOfEmployees.GetDataSet(endDate.Date);
            DataTable employeeeducation = EmployeeEducationInformation.GetDataSet(endDate.Date);
            DataTable employeeAge = EmployeeAgeInformation.GetDataSet(endDate.Date);
                
            DataColumn dc = result.Columns.Add("SL", typeof(Int32));
            dc.Caption = "S#";
            dc = result.Columns.Add("Description", typeof(String));
            dc.Caption = "Description";
            dc = result.Columns.Add("Column1", typeof(Int32));
            dc.Caption = "";

            Int32 NumberOfMuslim = 0;
            Int32 NumberOfHindu = 0;
            Int32 NumberOfChristian = 0;
            Int32 NumberOfBuddha = 0;
            Int32 NumberOfOthers = 0;
            Int32 NumberOfTotal = 0;

            Int32 masters = 0;
            Int32 honors = 0;
            Int32 higherSecondary = 0;
            Int32 secondary = 0;
            Int32 others = 0;
            Int32 total = 0;

            Int32 Age1 = 0;
            Int32 Age2 = 0;
            Int32 Age3 = 0;
            Int32 Age4 = 0;
            Int32 Age5 = 0;
            Int32 Age6 = 0;
            Int32 Age7 = 0;
            Int32 Age8= 0;
            Int32 AgeTotal = 0;

            if (minorityInformation != null)
            {
                NumberOfMuslim = DBUtility.ToInt32(minorityInformation.Compute("SUM([Muslim])", ""));
                NumberOfHindu = DBUtility.ToInt32(minorityInformation.Compute("SUM([Hindu])", ""));
                NumberOfChristian = DBUtility.ToInt32(minorityInformation.Compute("SUM([Christian])", ""));
                NumberOfBuddha = DBUtility.ToInt32(minorityInformation.Compute("SUM([Buddha])", ""));
                NumberOfOthers = DBUtility.ToInt32(minorityInformation.Compute("SUM([Others])", ""));
                NumberOfTotal = DBUtility.ToInt32(minorityInformation.Compute("SUM([Total])", ""));
            }

            if (employeeeducation != null)
            {
                masters = DBUtility.ToInt32(employeeeducation.Compute("SUM([Masters])", ""));
                honors = DBUtility.ToInt32(employeeeducation.Compute("SUM([Honors])", ""));
                higherSecondary = DBUtility.ToInt32(employeeeducation.Compute("SUM([Higher Secondary])", ""));
                secondary = DBUtility.ToInt32(employeeeducation.Compute("SUM([Secondary])", ""));
                others = DBUtility.ToInt32(employeeeducation.Compute("SUM([Others])", ""));
                total = masters + honors + higherSecondary + secondary + others;
            }

            if (employeeAge != null)
            {
                Age1 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 0-25])", ""));
                Age2 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 26-30])", ""));
                Age3 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 31-35])", ""));
                Age4 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 36-40])", ""));
                Age5 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 41-45])", ""));
                Age6 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 46-50])", ""));
                Age7 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 51-57])", ""));
                Age8 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 58-plus])", ""));
                AgeTotal = DBUtility.ToInt32(employeeAge.Compute("SUM(Total)", ""));
            }

            result.Rows.Add(new object[] { null, "", null });
            result.Rows.Add(new object[] { index++, "Minoroties Information of Employees", null });
            result.Rows.Add(new object[] { null, "\t\t\t\tMuslim", NumberOfMuslim });
            result.Rows.Add(new object[] { null, "\t\t\t\tHindu", NumberOfHindu });
            result.Rows.Add(new object[] { null, "\t\t\t\tChristian", NumberOfChristian });
            result.Rows.Add(new object[] { null, "\t\t\t\tBuddha", NumberOfBuddha });
            result.Rows.Add(new object[] { null, "\t\t\t\tOthers", NumberOfOthers });
            result.Rows.Add(new object[] { null, "\t\t\t\tTotal", NumberOfTotal });

            result.Rows.Add(new object[] { null, "", null });
            result.Rows.Add(new object[] { index++, "Employee Education Information", null });
            result.Rows.Add(new object[] { null, "\t\t\t\tMasters", masters });
            result.Rows.Add(new object[] { null, "\t\t\t\tHonors", honors });
            result.Rows.Add(new object[] { null, "\t\t\t\tHigher Secondary", higherSecondary });
            result.Rows.Add(new object[] { null, "\t\t\t\tSecondary", secondary });
            result.Rows.Add(new object[] { null, "\t\t\t\tOthers", others });
            result.Rows.Add(new object[] { null, "\t\t\t\tTotal", total });

            result.Rows.Add(new object[] { null, "", null });
            result.Rows.Add(new object[] { index++, "Employee Age Information", null });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 0-25", Age1 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 26-30", Age2 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 31-35", Age3 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 36-40", Age4 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 41-45", Age5 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 46-50", Age6 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 51-57", Age7 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 58-plus", Age8 });
            result.Rows.Add(new object[] { null, "\t\t\t\tTotal", AgeTotal });

            result.Columns[0].ExtendedProperties.Add("Width", 25);
            result.Columns[1].ExtendedProperties.Add("Width", 250);
            result.Columns[2].ExtendedProperties.Add("Width", 50);

            IList<WorksheetRow> header = new List<WorksheetRow>();
            header.Add(new WorksheetRow());
            header.Add(new WorksheetRow());

            header[0].Cells.Add("Staff Information", DataType.String, "HeaderTop1").MergeAcross = 1;
            header[1].Cells.Add("As on: " + UIUtility.Format(endDate.Date), DataType.String, "HeaderTop4").MergeAcross = 2;

            WorksheetRow[][] displayheader = new WorksheetRow[1][];
            displayheader[0] = header.ToArray();


            result.TableName = "Staff Information";
            ExcelReportUtility.Instance.DataSource = result;
            ExcelReportUtility.Instance.Header = displayheader;
            ExcelReportUtility.Instance.Name = result.TableName + "(" + DateTime.Now.Ticks + ")" + Configuration.ReportExtension;
            ExcelReportUtility.Instance.BoldRow += new ExcelReportUtility.RowEventHandler(QuickReport_BoldRow);
            ExcelReportUtility.Instance.ViewReport();
        }

        private bool QuickReport_BoldRow(DataRow dataRow)
        {
            return (dataRow[0] != DBNull.Value);
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
            IList<ReportConfig> type = ReportConfig.FindByType((Int32)ReportConfig.ReportType.Summary, "Name");

            if (type != null)
            {
                this.ddlReport.DataTextField = "Name";
                this.ddlReport.DataValueField = "Id";
                this.ddlReport.DataSource = type;
                this.ddlReport.DataBind();
            }

            this.LoadZone();
            this.LoadGrade();

            txtAsOnDate.Text = UIUtility.Format(Configuration.EndDate);
        }

        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlRegion.SelectedValue != null)
            {
                ReportConfig report = ReportConfig.GetById(DBUtility.ToInt32(ddlReport.SelectedValue));

                if (report.Location)
                {
                    this.ddlZone.Enabled = true;
                    this.ddlSubzone.Enabled = true;
                    this.ddlRegion.Enabled = true;
                    this.ddlBranch.Enabled = true;
                }
                else
                {
                    this.ddlZone.SelectedIndex = 0;
                    this.ddlSubzone.SelectedIndex = 0;
                    this.ddlRegion.SelectedIndex = 0;
                    this.ddlBranch.SelectedIndex = 0;

                    this.ddlZone.Enabled = false;
                    this.ddlSubzone.Enabled = false;
                    this.ddlRegion.Enabled = false;
                    this.ddlBranch.Enabled = false;
                }

                if (report.Position)
                {
                    this.ddlGrade.Enabled = true;
                    this.ddlDesignation.Enabled = true;
                }
                else
                {
                    this.ddlGrade.SelectedIndex = 0;
                    this.ddlDesignation.SelectedIndex = 0;

                    this.ddlGrade.Enabled = false;
                    this.ddlDesignation.Enabled = false;
                }
            }
        }

        private void LoadZone()
        {
            IList<Zone> divisionList = Zone.Find("Id > 0", "Name");

            if (divisionList == null)
            {
                divisionList = new List<Zone>();
            }

            Zone all = new Zone();
            all.Id = 0;
            all.Name = "All";
            divisionList.Insert(0, all);

            if (divisionList != null && divisionList.Count > 0)
            {
                this.ddlZone.DataTextField = "Name";
                this.ddlZone.DataValueField = "Id";
                this.ddlZone.DataSource = divisionList;
                this.ddlZone.DataBind();

                //if (divisionList.Count(z => z.Id == Asa.Hrms.Utility.Configuration.ZoneId) > 0)
                //{
                //    this.ddlZone.SelectedValue = Asa.Hrms.Utility.Configuration.ZoneId.ToString();
                //}

                this.LoadSubzone();
            }
            else
            {
                this.ddlZone.DataSource = null;
            }
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Asa.Hrms.Utility.Configuration.ZoneId = DBUtility.ToInt32(this.ddlZone.SelectedValue);

            this.LoadSubzone();
        }

        private void LoadSubzone()
        {
            if (ddlZone.SelectedValue != null)
            {
                Int32 ZoneId = DBUtility.ToInt32(ddlZone.SelectedValue);
                IList<Subzone> subzoneList = Subzone.Find("Id > 0 AND ZoneId = " + ZoneId + " And Status=1", "Name");

                if (subzoneList == null)
                {
                    subzoneList = new List<Subzone>();
                }

                Subzone all = new Subzone();
                all.ZoneId = 0;
                all.Id = 0;
                all.Name = "All";
                subzoneList.Insert(0, all);

                if (subzoneList != null && subzoneList.Count > 0)
                {
                    this.ddlSubzone.DataTextField = "Name";
                    this.ddlSubzone.DataValueField = "Id";
                    this.ddlSubzone.DataSource = subzoneList;
                    this.ddlSubzone.DataBind();

                    //if (subzoneList.Count(d => d.Id == Asa.Hrms.Utility.Configuration.SubzoneId) > 0)
                    //{
                    //    this.ddlSubzone.SelectedValue = Asa.Hrms.Utility.Configuration.SubzoneId.ToString();
                    //}

                    this.LoadRegion();
                }
                else
                {
                    this.ddlSubzone.DataSource = null;
                }
            }
        }

        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Asa.Hrms.Utility.Configuration.SubzoneId = DBUtility.ToInt32(this.ddlSubzone.SelectedValue);

            this.LoadRegion();
        }

        private void LoadRegion()
        {
            if (ddlSubzone.SelectedValue != null)
            {
                IList<Region> regionList = Region.Find("Id > 0 AND SubzoneId = " + ddlSubzone.SelectedValue + " And Status=1", "Name");

                if (regionList == null)
                {
                    regionList = new List<Region>();
                }

                Region all = new Region();
                all.SubzoneId = 0;
                all.Id = 0;
                all.Name = "All";
                regionList.Insert(0, all);

                if (regionList != null && regionList.Count > 0)
                {
                    this.ddlRegion.DataTextField = "Name";
                    this.ddlRegion.DataValueField = "Id";
                    this.ddlRegion.DataSource = regionList;
                    this.ddlRegion.DataBind();

                    if (regionList.Count(r => r.Id == Configuration.RegionId) > 0)
                    {
                        this.ddlRegion.SelectedValue = Configuration.RegionId.ToString();
                    }

                    this.LoadBranch();
                }
                else
                {
                    this.ddlRegion.DataSource = null;
                }
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.RegionId = DBUtility.ToInt32(this.ddlRegion.SelectedValue);

            this.LoadBranch();
        }

        private void LoadBranch()
        {
            if (ddlRegion.SelectedValue != null)
            {
                Int32 regionId = DBUtility.ToInt32(ddlRegion.SelectedValue);
                IList<Branch> branchList = Branch.Find("Id > 0 AND RegionId = " + regionId + " And Status=1", "Name");

                if (branchList == null)
                {
                    branchList = new List<Branch>();
                }

                Branch all = new Branch();
                all.RegionId = 0;
                all.Id = 0;
                all.Name = "All";
                branchList.Insert(0, all);

                if (branchList != null && branchList.Count > 0)
                {
                    this.ddlBranch.DataTextField = "Name";
                    this.ddlBranch.DataValueField = "Id";
                    this.ddlBranch.DataSource = branchList;
                    this.ddlBranch.DataBind();

                    if (branchList.Count(r => r.Id == Configuration.BranchCode) > 0)
                    {
                        this.ddlBranch.SelectedValue = Configuration.BranchCode.ToString();
                    }
                }
                else
                {
                    this.ddlBranch.DataSource = null;
                }
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.BranchCode = DBUtility.ToInt32(this.ddlBranch.SelectedValue);
        }

        private void LoadGrade()
        {
            IList<H_Grade> gradeList = H_Grade.Find("Id > 0", "Name");

            if (gradeList == null)
            {
                gradeList = new List<H_Grade>();
            }

            H_Grade all = new H_Grade();
            all.Id = 0;
            all.Name = "All";
            gradeList.Insert(0, all);

            if (gradeList != null && gradeList.Count > 0)
            {
                this.ddlGrade.DataTextField = "Name";
                this.ddlGrade.DataValueField = "Id";
                this.ddlGrade.DataSource = gradeList;
                this.ddlGrade.DataBind();

                if (gradeList.Count(z => z.Id == Configuration.GradeId) > 0)
                {
                    this.ddlGrade.SelectedValue = Configuration.GradeId.ToString();
                }

                this.LoadDesignation();
            }
            else
            {
                this.ddlGrade.DataSource = null;
            }

        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.GradeId = DBUtility.ToInt32(this.ddlGrade.SelectedValue);

            this.LoadDesignation();
        }

        private void LoadDesignation()
        {
            if (ddlGrade.SelectedValue != null)
            {
                TransactionManager tm = new TransactionManager(false);

                DataTable dt = tm.GetDataSet("SELECT H_Designation.Id, Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + this.ddlGrade.SelectedValue + " ORDER BY SortOrder").Tables[0];
                IList<H_Designation> designationList = new List<H_Designation>();

                H_Designation all = new H_Designation();
                all.Id = 0;
                all.Name = "All";
                designationList.Insert(0, all);

                if (dt.Rows.Count > 0)
                {
                    int index = 1;

                    foreach (DataRow row in dt.Rows)
                    {
                        H_Designation des = new H_Designation();
                        des.Id = DBUtility.ToInt32(row["Id"].ToString());
                        des.Name = row["Name"].ToString();

                        designationList.Insert(index++, des);
                    }
                }

                if (designationList != null && designationList.Count > 0)
                {
                    this.ddlDesignation.DataTextField = "Name";
                    this.ddlDesignation.DataValueField = "Id";
                    this.ddlDesignation.DataSource = designationList;
                    this.ddlDesignation.DataBind();

                    if (designationList.Count(r => r.Id == Configuration.DesignationId) > 0)
                    {
                        this.ddlDesignation.SelectedValue = Configuration.DesignationId.ToString();
                    }
                }
            }
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.DesignationId = DBUtility.ToInt32(this.ddlDesignation.SelectedValue);
        }
    }
}
