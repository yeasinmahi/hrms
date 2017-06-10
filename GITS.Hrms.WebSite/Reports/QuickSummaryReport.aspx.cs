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
            Validate();

            if (IsValid)
            {
                switch (e.Item.Value)
                {
                    case "EXCEL":
                        ViewReport();
                        break;
                    default:
                        HandleSpecialCommand(sender, e);
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
                AtAGlanceReport(report.Query);
            }

            if (report.Name.Contains("Staff Information"))
            {
                StaffInformation(report.Query);
            }
        }

        private void AtAGlanceReport(string queryString)
        {
            int? zoneId = DBUtility.ToInt32(ddlZone.SelectedValue);
            int? subzoneId = DBUtility.ToInt32(ddlSubzone.SelectedValue);
            int? regionId = DBUtility.ToInt32(ddlRegion.SelectedValue);
            int? branchCode = DBUtility.ToInt32(ddlBranch.SelectedValue);
            int? gradeId = DBUtility.ToInt32(ddlGrade.SelectedValue);
            int? designationId = DBUtility.ToInt32(ddlDesignation.SelectedValue);

            DateTime endDate = Configuration.EndDate = Convert.ToDateTime(txtAsOnDate.Text).Date;

            TransactionManager = new TransactionManager(false);

            DataSet ds = TransactionManager.GetDataSet(String.Format(queryString,
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

            Int32 numberOfMuslim = 0;
            Int32 numberOfHindu = 0;
            Int32 numberOfChristian = 0;
            Int32 numberOfBuddha = 0;
            Int32 numberOfOthers = 0;
            Int32 numberOfTotal = 0;

            Int32 masters = 0;
            Int32 honors = 0;
            Int32 higherSecondary = 0;
            Int32 secondary = 0;
            Int32 others = 0;
            Int32 total = 0;

            Int32 age1 = 0;
            Int32 age2 = 0;
            Int32 age3 = 0;
            Int32 age4 = 0;
            Int32 age5 = 0;
            Int32 age6 = 0;
            Int32 age7 = 0;
            Int32 age8= 0;
            Int32 ageTotal = 0;

            if (minorityInformation != null)
            {
                numberOfMuslim = DBUtility.ToInt32(minorityInformation.Compute("SUM([Muslim])", ""));
                numberOfHindu = DBUtility.ToInt32(minorityInformation.Compute("SUM([Hindu])", ""));
                numberOfChristian = DBUtility.ToInt32(minorityInformation.Compute("SUM([Christian])", ""));
                numberOfBuddha = DBUtility.ToInt32(minorityInformation.Compute("SUM([Buddha])", ""));
                numberOfOthers = DBUtility.ToInt32(minorityInformation.Compute("SUM([Others])", ""));
                numberOfTotal = DBUtility.ToInt32(minorityInformation.Compute("SUM([Total])", ""));
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
                age1 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 0-25])", ""));
                age2 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 26-30])", ""));
                age3 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 31-35])", ""));
                age4 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 36-40])", ""));
                age5 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 41-45])", ""));
                age6 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 46-50])", ""));
                age7 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 51-57])", ""));
                age8 = DBUtility.ToInt32(employeeAge.Compute("SUM([Age 58-plus])", ""));
                ageTotal = DBUtility.ToInt32(employeeAge.Compute("SUM(Total)", ""));
            }

            result.Rows.Add(new object[] { null, "", null });
            result.Rows.Add(new object[] { index++, "Minoroties Information of Employees", null });
            result.Rows.Add(new object[] { null, "\t\t\t\tMuslim", numberOfMuslim });
            result.Rows.Add(new object[] { null, "\t\t\t\tHindu", numberOfHindu });
            result.Rows.Add(new object[] { null, "\t\t\t\tChristian", numberOfChristian });
            result.Rows.Add(new object[] { null, "\t\t\t\tBuddha", numberOfBuddha });
            result.Rows.Add(new object[] { null, "\t\t\t\tOthers", numberOfOthers });
            result.Rows.Add(new object[] { null, "\t\t\t\tTotal", numberOfTotal });

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
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 0-25", age1 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 26-30", age2 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 31-35", age3 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 36-40", age4 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 41-45", age5 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 46-50", age6 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 51-57", age7 });
            result.Rows.Add(new object[] { null, "\t\t\t\tAge 58-plus", age8 });
            result.Rows.Add(new object[] { null, "\t\t\t\tTotal", ageTotal });

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
                ddlReport.DataTextField = "Name";
                ddlReport.DataValueField = "Id";
                ddlReport.DataSource = type;
                ddlReport.DataBind();
            }

            LoadZone();
            LoadGrade();

            txtAsOnDate.Text = UIUtility.Format(Configuration.EndDate);
        }

        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegion.SelectedValue != null)
            {
                ReportConfig report = ReportConfig.GetById(DBUtility.ToInt32(ddlReport.SelectedValue));

                if (report.Location)
                {
                    ddlZone.Enabled = true;
                    ddlSubzone.Enabled = true;
                    ddlRegion.Enabled = true;
                    ddlBranch.Enabled = true;
                }
                else
                {
                    ddlZone.SelectedIndex = 0;
                    ddlSubzone.SelectedIndex = 0;
                    ddlRegion.SelectedIndex = 0;
                    ddlBranch.SelectedIndex = 0;

                    ddlZone.Enabled = false;
                    ddlSubzone.Enabled = false;
                    ddlRegion.Enabled = false;
                    ddlBranch.Enabled = false;
                }

                if (report.Position)
                {
                    ddlGrade.Enabled = true;
                    ddlDesignation.Enabled = true;
                }
                else
                {
                    ddlGrade.SelectedIndex = 0;
                    ddlDesignation.SelectedIndex = 0;

                    ddlGrade.Enabled = false;
                    ddlDesignation.Enabled = false;
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
                ddlZone.DataTextField = "Name";
                ddlZone.DataValueField = "Id";
                ddlZone.DataSource = divisionList;
                ddlZone.DataBind();

                //if (divisionList.Count(z => z.Id == Asa.Hrms.Utility.Configuration.ZoneId) > 0)
                //{
                //    this.ddlZone.SelectedValue = Asa.Hrms.Utility.Configuration.ZoneId.ToString();
                //}

                LoadSubzone();
            }
            else
            {
                ddlZone.DataSource = null;
            }
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Asa.Hrms.Utility.Configuration.ZoneId = DBUtility.ToInt32(this.ddlZone.SelectedValue);

            LoadSubzone();
        }

        private void LoadSubzone()
        {
            if (ddlZone.SelectedValue != null)
            {
                Int32 zoneId = DBUtility.ToInt32(ddlZone.SelectedValue);
                IList<Subzone> subzoneList = Subzone.Find("Id > 0 AND ZoneId = " + zoneId + " And Status=1", "Name");

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
                    ddlSubzone.DataTextField = "Name";
                    ddlSubzone.DataValueField = "Id";
                    ddlSubzone.DataSource = subzoneList;
                    ddlSubzone.DataBind();

                    //if (subzoneList.Count(d => d.Id == Asa.Hrms.Utility.Configuration.SubzoneId) > 0)
                    //{
                    //    this.ddlSubzone.SelectedValue = Asa.Hrms.Utility.Configuration.SubzoneId.ToString();
                    //}

                    LoadRegion();
                }
                else
                {
                    ddlSubzone.DataSource = null;
                }
            }
        }

        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Asa.Hrms.Utility.Configuration.SubzoneId = DBUtility.ToInt32(this.ddlSubzone.SelectedValue);

            LoadRegion();
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
                    ddlRegion.DataTextField = "Name";
                    ddlRegion.DataValueField = "Id";
                    ddlRegion.DataSource = regionList;
                    ddlRegion.DataBind();

                    if (regionList.Count(r => r.Id == Configuration.RegionId) > 0)
                    {
                        ddlRegion.SelectedValue = Configuration.RegionId.ToString();
                    }

                    LoadBranch();
                }
                else
                {
                    ddlRegion.DataSource = null;
                }
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.RegionId = DBUtility.ToInt32(ddlRegion.SelectedValue);

            LoadBranch();
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
                    ddlBranch.DataTextField = "Name";
                    ddlBranch.DataValueField = "Id";
                    ddlBranch.DataSource = branchList;
                    ddlBranch.DataBind();

                    if (branchList.Count(r => r.Id == Configuration.BranchCode) > 0)
                    {
                        ddlBranch.SelectedValue = Configuration.BranchCode.ToString();
                    }
                }
                else
                {
                    ddlBranch.DataSource = null;
                }
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.BranchCode = DBUtility.ToInt32(ddlBranch.SelectedValue);
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
                ddlGrade.DataTextField = "Name";
                ddlGrade.DataValueField = "Id";
                ddlGrade.DataSource = gradeList;
                ddlGrade.DataBind();

                if (gradeList.Count(z => z.Id == Configuration.GradeId) > 0)
                {
                    ddlGrade.SelectedValue = Configuration.GradeId.ToString();
                }

                LoadDesignation();
            }
            else
            {
                ddlGrade.DataSource = null;
            }

        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.GradeId = DBUtility.ToInt32(ddlGrade.SelectedValue);

            LoadDesignation();
        }

        private void LoadDesignation()
        {
            if (ddlGrade.SelectedValue != null)
            {
                TransactionManager tm = new TransactionManager(false);

                DataTable dt = tm.GetDataSet("SELECT H_Designation.Id, Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + ddlGrade.SelectedValue + " ORDER BY SortOrder").Tables[0];
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
                    ddlDesignation.DataTextField = "Name";
                    ddlDesignation.DataValueField = "Id";
                    ddlDesignation.DataSource = designationList;
                    ddlDesignation.DataBind();

                    if (designationList.Count(r => r.Id == Configuration.DesignationId) > 0)
                    {
                        ddlDesignation.SelectedValue = Configuration.DesignationId.ToString();
                    }
                }
            }
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.DesignationId = DBUtility.ToInt32(ddlDesignation.SelectedValue);
        }
    }
}
