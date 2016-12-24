using System;
using Asa.Hrms.Web;
using Asa.Hrms.Utility;
using System.Data;
using Asa.ExcelXmlWriter;
using System.Collections.Generic;
using System.Linq;
using Asa.Hrms.Data.Entity;
using Asa.Hrms.Data.Procedure;

public partial class HRM_Report : AddPage
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
    protected override void Search()
    {
        Message msg = new Message();
        Asa.Hrms.Data.Entity.ReportConfig report = Asa.Hrms.Data.Entity.ReportConfig.GetById(DBUtility.ToInt32(ddlReport.SelectedValue));
        Nullable<Int32> ZoneId = DBUtility.ToInt32(ddlZone.SelectedValue);
        Nullable<Int32> subzoneId = DBUtility.ToInt32(ddlSubzone.SelectedValue);
        Nullable<Int32> regionId = DBUtility.ToInt32(ddlRegion.SelectedValue);
        Nullable<Int32> branchCode = DBUtility.ToInt32(ddlBranch.SelectedValue);
        Nullable<Int32> gradeId = DBUtility.ToInt32(ddlGrade.SelectedValue);
        Nullable<Int32> designationId = DBUtility.ToInt32(ddlDesignation.SelectedValue);
        Nullable<Int32> religionId = DBUtility.ToInt32(ddlReligion.SelectedValue);
        Nullable<Int32> sexId = DBUtility.ToInt32(ddlSex.SelectedValue);

        DateTime startDate = Configuration.StartDate = Convert.ToDateTime(txtStartDate.Text).Date;
        DateTime endDate = Configuration.EndDate = Convert.ToDateTime(txtEndDate.Text).Date;

        this.TransactionManager = new Asa.Hrms.Data.TransactionManager(false);

        DataSet ds = this.TransactionManager.GetDataSet(String.Format(report.Query,
            UIUtility.ToInt32(startDate),
            UIUtility.ToInt32(endDate),
            "'" + startDate.ToString(Configuration.DatabaseDateFormat) + "'",
            "'" + endDate.ToString(Configuration.DatabaseDateFormat) + "'",
            ZoneId == 0 ? "NULL" : ZoneId.ToString(),
            subzoneId == 0 ? "NULL" : subzoneId.ToString(),
            regionId == 0 ? "NULL" : regionId.ToString(),
            branchCode == 0 ? "NULL" : branchCode.ToString(),
            gradeId == 0 ? "NULL" : gradeId.ToString(),
            designationId == 0 ? "NULL" : designationId.ToString(),
            religionId == 0 ? "NULL" : religionId.ToString(),
            sexId == 0 ? "NULL" : sexId.ToString()));

        DataTable dt = ds.Tables[0];
        gvList.Columns.Clear();
        foreach (DataColumn dc in dt.Columns)
        {
            BoundField bf = new BoundField();
            bf.HeaderText = dc.ColumnName;
            bf.DataField = dc.ColumnName;
            if (dc.DataType == typeof(DateTime))
            {
                bf.DataFormatString = "{0:dd/MM/yyyy}";
            }
            gvList.Columns.Add(bf);
        }
        gvList.DataSource = dt;
        gvList.DataBind();
        if (dt.Rows.Count > 0)
        {
            lblTotalRecord.Text = "Total Record : " + dt.Rows.Count.ToString();
            lblTotalRecord.Visible = true;
        }
        else
        {
            lblTotalRecord.Visible = false;
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
        Asa.Hrms.Data.Entity.ReportConfig report = Asa.Hrms.Data.Entity.ReportConfig.GetById(DBUtility.ToInt32(ddlReport.SelectedValue));
        Nullable<Int32> ZoneId = DBUtility.ToInt32(ddlZone.SelectedValue);
        Nullable<Int32> subzoneId = DBUtility.ToInt32(ddlSubzone.SelectedValue);
        Nullable<Int32> regionId = DBUtility.ToInt32(ddlRegion.SelectedValue);
        Nullable<Int32> branchCode = DBUtility.ToInt32(ddlBranch.SelectedValue);
        Nullable<Int32> gradeId = DBUtility.ToInt32(ddlGrade.SelectedValue);
        Nullable<Int32> designationId = DBUtility.ToInt32(ddlDesignation.SelectedValue);
        Nullable<Int32> religionId = DBUtility.ToInt32(ddlReligion.SelectedValue);
        Nullable<Int32> sexId = DBUtility.ToInt32(ddlSex.SelectedValue);

        DateTime startDate = Configuration.StartDate = Convert.ToDateTime(txtStartDate.Text).Date;
        DateTime endDate = Configuration.EndDate = Convert.ToDateTime(txtEndDate.Text).Date;

        this.TransactionManager = new Asa.Hrms.Data.TransactionManager(false);

        DataSet ds = this.TransactionManager.GetDataSet(String.Format(report.Query,
            UIUtility.ToInt32(startDate),
            UIUtility.ToInt32(endDate),
            "'" + startDate.ToString(Configuration.DatabaseDateFormat) + "'",
            "'" + endDate.ToString(Configuration.DatabaseDateFormat) + "'",
            ZoneId == 0 ? "NULL" : ZoneId.ToString(),
            subzoneId == 0 ? "NULL" : subzoneId.ToString(),
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

            header[i] = new WorksheetRow[4];
            header[i][0] = new WorksheetRow();
            header[i][1] = new WorksheetRow();
            header[i][2] = new WorksheetRow();
            header[i][3] = new WorksheetRow();

            header[i][0].Cells.Add(report.Name, DataType.String, "HeaderTop1").MergeAcross = dt.Columns.Count - 1;
            header[i][1].Cells.Add(UIUtility.GetHeader(ZoneId, subzoneId, regionId, branchCode), DataType.String, "HeaderTop3").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2 - 1;
            header[i][2].Cells.Add(UIUtility.GetPositionHeader(gradeId, designationId), DataType.String, "HeaderTop3").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2 - 1;
            header[i][2].Cells.Add("As On : " + UIUtility.Format(endDate), DataType.String, "HeaderTop4").MergeAcross = (dt.Columns.Count % 2) == 0 ? dt.Columns.Count / 2 - 1 : (dt.Columns.Count - 1) / 2;

            foreach (DataColumn dc in dt.Columns)
            {
                header[i][3].Cells.Add((dc.ColumnName.Replace("_"," ")), DataType.String, "HeaderLeftAlign");
            }

            i++;
        }

        for (i = 0; i < headers.Count; i++)
        {
            header[i] = headers[i].ToArray();
        }

        ExcelReportUtility.Instance.DataSource = tables.ToArray();
        ExcelReportUtility.Instance.Header = header;
        ExcelReportUtility.Instance.Name = report.Name + "(" + DateTime.Now.Ticks + ")" + Asa.Hrms.Utility.Configuration.ReportExtension;
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
        UIUtility.LoadEnums(this.ddlReportType, typeof(ReportConfig.ReportType), false, false, true);
        this.ddlReportType.Items.RemoveAt(this.ddlReportType.Items.Count-1);

        this.ddlReport.DataSource = Asa.Hrms.Data.Entity.ReportConfig.Find("Type = " + this.ddlReportType.SelectedValue, "");
        this.ddlReport.DataBind();

        this.ddlReport_SelectedIndexChanged(this.ddlReport, new EventArgs());

        this.LoadZone();
        this.LoadGrade();

        UIUtility.LoadEnums(this.ddlReligion, typeof(H_Employee.Religions), false, true, false);
        UIUtility.LoadEnums(this.ddlSex, typeof(H_Employee.Sexes), false, true, false);

        txtStartDate.Text = UIUtility.Format(Configuration.StartDate);
        txtEndDate.Text = UIUtility.Format(Configuration.EndDate);
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
            IList<Subzone> subzoneList = Subzone.Find("Id > 0 AND ZoneId = " + ZoneId+" And Status=1", "Name");

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

                if (regionList.Count(r => r.Id == Asa.Hrms.Utility.Configuration.RegionId) > 0)
                {
                    this.ddlRegion.SelectedValue = Asa.Hrms.Utility.Configuration.RegionId.ToString();
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
        Asa.Hrms.Utility.Configuration.RegionId = DBUtility.ToInt32(this.ddlRegion.SelectedValue);

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

                if (branchList.Count(r => r.Id == Asa.Hrms.Utility.Configuration.BranchCode) > 0)
                {
                    this.ddlBranch.SelectedValue = Asa.Hrms.Utility.Configuration.BranchCode.ToString();
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
        Asa.Hrms.Utility.Configuration.BranchCode = DBUtility.ToInt32(this.ddlBranch.SelectedValue);
    }

    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlReport.DataSource = Asa.Hrms.Data.Entity.ReportConfig.Find("Type = " + this.ddlReportType.SelectedValue, "");
        this.ddlReport.DataBind();

        this.ddlReport_SelectedIndexChanged(this.ddlReport, new EventArgs());
    }

    protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReport.SelectedValue != null)
        {
            Asa.Hrms.Data.Entity.ReportConfig report = Asa.Hrms.Data.Entity.ReportConfig.GetById(DBUtility.ToInt32(ddlReport.SelectedValue));

            if (report != null && report.Location)
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

            if (report != null && report.DateBetween==2)
            {
                trEndDate.Visible = true;
                trStartDate.Visible = true;
                tdEndDate.InnerText = "End Date:";
                cvDate.Enabled = true;
            }
            else if (report != null && report.DateBetween == 1)
            {
                trStartDate.Visible = false;
                trEndDate.Visible = true;
                txtEndDate.Enabled = true;
                tdEndDate.InnerText = "As On:";
                cvDate.Enabled = false;
            }
            else
            {
                trEndDate.Visible = false;
                trStartDate.Visible = false;
                txtEndDate.Enabled = false;
                tdEndDate.InnerText = "As On:";
                cvDate.Enabled = false;
            }

            if (report != null && report.ReligionAndSex)
            {
                ddlReligion.Enabled = true;
                ddlSex.Enabled = true;
            }
            else
            {
                ddlReligion.SelectedIndex = 0;
                ddlSex.SelectedIndex = 0;

                ddlReligion.Enabled = false;
                ddlSex.Enabled = false;
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
            this.ddlGrade.DataTextField = "Name";
            this.ddlGrade.DataValueField = "Id";
            this.ddlGrade.DataSource = gradeList;
            this.ddlGrade.DataBind();

            if (gradeList.Count(z => z.Id == Asa.Hrms.Utility.Configuration.GradeId) > 0)
            {
                this.ddlGrade.SelectedValue = Asa.Hrms.Utility.Configuration.GradeId.ToString();
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
        Asa.Hrms.Utility.Configuration.GradeId = DBUtility.ToInt32(this.ddlGrade.SelectedValue);

        this.LoadDesignation();
    }

    private void LoadDesignation()
    {
        if (ddlGrade.SelectedValue != null)
        {
            Asa.Hrms.Data.TransactionManager tm = new Asa.Hrms.Data.TransactionManager(false);

            DataTable dt = tm.GetDataSet("SELECT H_Designation.Id, Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + this.ddlGrade.SelectedValue + " ORDER BY Name").Tables[0];
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

                if (designationList.Count(r => r.Id == Asa.Hrms.Utility.Configuration.DesignationId) > 0)
                {
                    this.ddlDesignation.SelectedValue = Asa.Hrms.Utility.Configuration.DesignationId.ToString();
                }
            }
        }
    }

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        Asa.Hrms.Utility.Configuration.DesignationId = DBUtility.ToInt32(this.ddlDesignation.SelectedValue);
    }
}
