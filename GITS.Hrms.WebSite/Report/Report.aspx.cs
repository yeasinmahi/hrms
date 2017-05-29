using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Asa.ExcelXmlWriter;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

public partial class Report : AddPage
{
    protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
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

    private string GetLabel(string field)
    {
        if (field.Trim().Contains(' '))
        {
            return field;
        }

        if (field.EndsWith("Id"))
        {
            field = field.Substring(0, field.Length - 2).Trim();
        }

        for (int i = 1; i < field.Length - 1; i++)
        {
            if (field[i] >= 'A' && field[i] <= 'Z')
            {
                field = field.Insert(i, " ");

                i++;
            }
        }

        Int32 index = field.IndexOf('_');

        if (index > 0)
        {
            field = field.Substring(index + 1);
        }

        return field;
    }

    private void ViewReport()
    {
        Message msg = new Message();
        ReportConfig report = ReportConfig.GetById(DBUtility.ToInt32(ddlReport.SelectedValue));
        int? DivisionId = DBUtility.ToInt32(ddlDivision.SelectedValue);
        int? zoneId = DBUtility.ToInt32(ddlDistrict.SelectedValue);
        int? regionId = DBUtility.ToInt32(ddlRegion.SelectedValue);
        int? branchCode = DBUtility.ToInt32(ddlBranch.SelectedValue);
        int? gradeId = DBUtility.ToInt32(ddlGrade.SelectedValue);
        int? designationId = DBUtility.ToInt32(ddlDesignation.SelectedValue);
        int? religionId = DBUtility.ToInt32(ddlReligion.SelectedValue);
        int? sexId = DBUtility.ToInt32(ddlSex.SelectedValue);

        DateTime startDate = Configuration.StartDate = Convert.ToDateTime(txtStartDate.Text).Date;
        DateTime endDate = Configuration.EndDate = Convert.ToDateTime(txtEndDate.Text).Date;

        TransactionManager = new GITS.Hrms.Data.TransactionManager(false);

        DataSet ds = TransactionManager.GetDataSet(String.Format(report.Query,
            UIUtility.ToInt32(startDate),
            UIUtility.ToInt32(endDate),
            "'" + startDate.ToString(Configuration.DatabaseDateFormat) + "'",
            "'" + endDate.ToString(Configuration.DatabaseDateFormat) + "'",
            DivisionId == 0 ? "NULL" : DivisionId.ToString(),
            zoneId == 0 ? "NULL" : zoneId.ToString(),
            regionId == 0 ? "NULL" : regionId.ToString(),
            branchCode == 0 ? "NULL" : branchCode.ToString(),
            gradeId == 0 ? "NULL" : gradeId.ToString(),
            designationId == 0 ? "NULL" : designationId.ToString(),
            religionId == 0 ? "NULL" : religionId.ToString(),
            sexId == 0 ? "NULL" : sexId.ToString()));

        IList<DataTable> tables = new List<DataTable>();
        IList<IList<WorksheetRow>> headers = new List<IList<WorksheetRow>>();
        WorksheetRow[][] header = new WorksheetRow[ds.Tables.Count][];
        Int32 i = 0;

        foreach (DataTable dt in ds.Tables)
        {
            tables.Add(dt);

            header[i] = new WorksheetRow[3];
            header[i][0] = new WorksheetRow();
            header[i][1] = new WorksheetRow();
            header[i][2] = new WorksheetRow();

            header[i][0].Cells.Add(report.Name, DataType.String, "HeaderTop1").MergeAcross = dt.Columns.Count - 1;
            header[i][1].Cells.Add(UIUtility.GetHeader(DivisionId, zoneId, regionId, branchCode), DataType.String, "HeaderTop3").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2 - 1;
            header[i][1].Cells.Add(report.DateBetween ? "From : " + UIUtility.Format(startDate) + " To : " + UIUtility.Format(endDate) : "As On : " + UIUtility.Format(endDate), DataType.String, "HeaderTop4").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2;

            foreach (DataColumn dc in dt.Columns)
            {
                header[i][2].Cells.Add(GetLabel(dc.ColumnName), DataType.String, "HeaderLeftAlign");
            }

            i++;
        }

        for (i = 0; i < headers.Count; i++)
        {
            header[i] = headers[i].ToArray();
        }

        ExcelReportUtility.Instance.DataSource = tables.ToArray();
        ExcelReportUtility.Instance.Header = header;
        ExcelReportUtility.Instance.Name = report.Name + "(" + DateTime.Now.Ticks + ")" + GITS.Hrms.Utility.Configuration.ReportExtension;
        ExcelReportUtility.Instance.ViewReport();
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
        ddlReport.DataSource = GITS.Hrms.Data.Entity.ReportConfig.FindAll("Name");
        ddlReport.DataBind();

        ddlReport_SelectedIndexChanged(ddlReport, new EventArgs());

        LoadDivision();
        LoadGrade();

        UIUtility.LoadEnums(ddlReligion, typeof(H_Employee.Religions), false, true, false);
        UIUtility.LoadEnums(ddlSex, typeof(H_Employee.Sexes), false, true, false);

        txtStartDate.Text = UIUtility.Format(Configuration.StartDate);
        txtEndDate.Text = UIUtility.Format(Configuration.EndDate);
    }

    private void LoadDivision()
    {
        IList<Division> divisionList = Division.Find("Id > 0", "Name");

        if (divisionList == null)
        {
            divisionList = new List<Division>();
        }

        Division all = new Division();
        all.Id = 0;
        all.Name = "All";
        divisionList.Insert(0, all);

        if (divisionList != null && divisionList.Count > 0)
        {
            ddlDivision.DataTextField = "Name";
            ddlDivision.DataValueField = "Id";
            ddlDivision.DataSource = divisionList;
            ddlDivision.DataBind();

            if (divisionList.Count(z => z.Id == GITS.Hrms.Utility.Configuration.DivisionId) > 0)
            {
                ddlDivision.SelectedValue = GITS.Hrms.Utility.Configuration.DivisionId.ToString();
            }

            LoadDistrict();
        }
        else
        {
            ddlDivision.DataSource = null;
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        GITS.Hrms.Utility.Configuration.DivisionId = DBUtility.ToInt32(ddlDivision.SelectedValue);

        LoadDistrict();
    }

    private void LoadDistrict()
    {
        if (ddlRegion.SelectedValue != null)
        {
            Int32 DivisionId = DBUtility.ToInt32(ddlDivision.SelectedValue);
            IList<District> districtList = District.Find("Id > 0 AND DivisionId = " + DivisionId, "Name");

            if (districtList == null)
            {
                districtList = new List<District>();
            }

            District all = new District();
            all.DivisionId = 0;
            all.Id = 0;
            all.Name = "All";
            districtList.Insert(0, all);

            if (districtList != null && districtList.Count > 0)
            {
                ddlDistrict.DataTextField = "Name";
                ddlDistrict.DataValueField = "Id";
                ddlDistrict.DataSource = districtList;
                ddlDistrict.DataBind();

                if (districtList.Count(d => d.Id == GITS.Hrms.Utility.Configuration.DistrictId) > 0)
                {
                    ddlDistrict.SelectedValue = GITS.Hrms.Utility.Configuration.DistrictId.ToString();
                }

                LoadRegion();
            }
            else
            {
                ddlDistrict.DataSource = null;
            }
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        GITS.Hrms.Utility.Configuration.DistrictId = DBUtility.ToInt32(ddlDistrict.SelectedValue);

        LoadRegion();
    }

    private void LoadRegion()
    {
        if (ddlDivision.SelectedValue != null)
        {
            Int32 districtId = DBUtility.ToInt32(ddlDistrict.SelectedValue);
            IList<Region> regionList = Region.Find("Id > 0 AND DistrictId = " + districtId, "Name");

            if (regionList == null)
            {
                regionList = new List<Region>();
            }

            Region all = new Region();
            all.DistrictId = 0;
            all.Id = 0;
            all.Name = "All";
            regionList.Insert(0, all);

            if (regionList != null && regionList.Count > 0)
            {
                ddlRegion.DataTextField = "Name";
                ddlRegion.DataValueField = "Id";
                ddlRegion.DataSource = regionList;
                ddlRegion.DataBind();

                if (regionList.Count(r => r.Id == GITS.Hrms.Utility.Configuration.RegionId) > 0)
                {
                    ddlRegion.SelectedValue = GITS.Hrms.Utility.Configuration.RegionId.ToString();
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
        GITS.Hrms.Utility.Configuration.RegionId = DBUtility.ToInt32(ddlRegion.SelectedValue);

        LoadBranch();
    }

    private void LoadBranch()
    {
        if (ddlRegion.SelectedValue != null)
        {
            Int32 regionId = DBUtility.ToInt32(ddlRegion.SelectedValue);
            IList<Branch> branchList = Branch.Find("Id > 0 AND RegionId = " + regionId, "Name");

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

                if (branchList.Count(r => r.Id == GITS.Hrms.Utility.Configuration.BranchCode) > 0)
                {
                    ddlBranch.SelectedValue = GITS.Hrms.Utility.Configuration.BranchCode.ToString();
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
        GITS.Hrms.Utility.Configuration.BranchCode = DBUtility.ToInt32(ddlBranch.SelectedValue);
    }

    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReport.SelectedValue != null)
        {
            GITS.Hrms.Data.Entity.ReportConfig report = GITS.Hrms.Data.Entity.ReportConfig.GetById(DBUtility.ToInt32(ddlReport.SelectedValue));

            if (report != null && report.Location)
            {
                ddlDivision.Enabled = true;
                ddlDistrict.Enabled = true;
                ddlRegion.Enabled = true;
                ddlBranch.Enabled = true;
            }
            else
            {
                ddlDivision.SelectedIndex = 0;
                ddlDistrict.SelectedIndex = 0;
                ddlRegion.SelectedIndex = 0;
                ddlBranch.SelectedIndex = 0;

                ddlDivision.Enabled = false;
                ddlDistrict.Enabled = false;
                ddlRegion.Enabled = false;
                ddlBranch.Enabled = false;
            }

            if (report != null && report.Position)
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

            if (report != null && report.DateBetween)
            {
                trStartDate.Visible = true;
                tdEndDate.InnerText = "End Date";
                cvDate.Enabled = true;
            }
            else
            {
                trStartDate.Visible = false;
                tdEndDate.InnerText = "As On";
                cvDate.Enabled = false;
            }
        }
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

            if (gradeList.Count(z => z.Id == GITS.Hrms.Utility.Configuration.GradeId) > 0)
            {
                ddlGrade.SelectedValue = GITS.Hrms.Utility.Configuration.GradeId.ToString();
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
        GITS.Hrms.Utility.Configuration.GradeId = DBUtility.ToInt32(ddlGrade.SelectedValue);

        LoadDesignation();
    }

    private void LoadDesignation()
    {
        if (ddlGrade.SelectedValue != null)
        {
            GITS.Hrms.Data.TransactionManager tm = new GITS.Hrms.Data.TransactionManager(false);

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

                if (designationList.Count(r => r.Id == GITS.Hrms.Utility.Configuration.DesignationId) > 0)
                {
                    ddlDesignation.SelectedValue = GITS.Hrms.Utility.Configuration.DesignationId.ToString();
                }
            }
        }
    }

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GITS.Hrms.Utility.Configuration.DesignationId = DBUtility.ToInt32(ddlDesignation.SelectedValue);
    }
}
