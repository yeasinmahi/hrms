using System;
using System.Collections;
using Asa.ExcelXmlWriter;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Asa.Hrms.Utility
{
    public class ExcelReportUtility
    {
        public delegate Boolean RowEventHandler(DataRow dataRow);
        public delegate Boolean ColumnEventHandler(DataColumn dataColumn);

        private Object _DataSource;
        private WorksheetRow[][] _Header;
        private String _Name;
        public event RowEventHandler BoldRow;
        public event ColumnEventHandler BoldColumn;
        private static ExcelReportUtility _Instance;

        public ExcelReportUtility()
        {
            this.BoldRow += new RowEventHandler(DefaultEventHandler);
            this.BoldColumn += new ColumnEventHandler(DefaultEventHandler);
        }

        public static ExcelReportUtility Instance
        {
            get 
            {
                if (_Instance == null)
                {
                    _Instance = new ExcelReportUtility();
                }

                return _Instance;
            }
        }

        public WorksheetRow[][] Header
        {
            get { return _Header; }
            set { _Header = value; }
        }

        public Object DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        public String Name
        {
            get { if (_Name != null && _Name != "") return _Name; else return "Report" + Configuration.ReportExtension; }
            set { _Name = value.Replace(",","_").Replace(" ", "_"); }
        }

        private Boolean DefaultEventHandler(DataRow dataRow)
        {
            return false;
        }

        private Boolean DefaultEventHandler(DataColumn dataColumn)
        {
            return false;
        }

        public void ViewReport()
        {
            if (this.DataSource != null)
            {
                Type type = this.DataSource.GetType();
                Workbook workbook = this.GetWorkbook();                

                if (type == typeof(DataTable))
                {
                    this.AddWorksheet(workbook, (DataTable)this.DataSource, 0);
                }
                else if (type == typeof(DataTable[]))
                {
                    DataTable[] tables = (DataTable[])this.DataSource;

                    for (Int32 i = 0; i < tables.Length; i++)
                    {
                        this.AddWorksheet(workbook, tables[i], i);
                    }
                }
                else if (type == typeof(GridView))
                {
                    this.AddWorksheet(workbook, (GridView)this.DataSource, 0);
                }
                else if (type == typeof(GridView[]))
                {
                    GridView[] gridViews = (GridView[])this.DataSource;

                    for (Int32 i = 0; i < gridViews.Length; i++)
                    {
                        this.AddWorksheet(workbook, gridViews[i], i);
                    }
                }
                else
                {
                    try 
                    { 
                        this.AddWorksheet(workbook, (DataTable)this.DataSource, 0); 
                    }
                    catch 
                    { 
                        throw new Exception("Unsupported data source found"); 
                    }
                }

                if (workbook != null)
                {
                    String tempFile = Path.GetTempFileName() + Configuration.ReportExtension;
                    workbook.Save(tempFile);
                    FileInfo fileInfo = new FileInfo(tempFile);
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", this.Name));
                    System.Web.HttpContext.Current.Response.Charset = "";
                    System.Web.HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                    System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Length", DBUtility.ToString(fileInfo.Length));
                    System.Web.HttpContext.Current.Response.TransmitFile(tempFile);
                    System.Web.HttpContext.Current.Response.Flush();
                    System.Web.HttpContext.Current.Response.Clear();

                    //System.Web.HttpContext.Current.Response.Clear();
                    //System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Report" + Configuration.ReportExtension);
                    //System.Web.HttpContext.Current.Response.Charset = "";
                    //System.Web.HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                    //System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                    //System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                    //workbook.Save(System.Web.HttpContext.Current.Response.OutputStream);
                    //System.Web.HttpContext.Current.Response.Flush();

                    //String temp = Path.GetTempFileName() + Configuration.ReportExtension;
                    //workbook.Save(temp);
                    //System.Web.UI.ScriptManager.RegisterClientScriptBlock((System.Web.UI.Page)System.Web.HttpContext.Current.Handler, System.Web.HttpContext.Current.Handler.GetType(), "Hello", "<script language=\"javascript\" type=\"text/javascript\">window.open('../Export.aspx?path=" + temp.Replace("\\", "\\\\") + "', 'Export', 0);</script>", false);
                }
            }
            else
            {
                throw new Exception("No data source found");
            }

            _Instance = null;
        }

        private Workbook GetWorkbook()
        {
            Workbook workbook = new Workbook();
            workbook.Styles.Add(this.GetWorksheetStyle("Default", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, false, false, 0, null, null, false));

            workbook.Styles.Add(this.GetWorksheetStyle("DefaultCell", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, false, true, 0, null, null, false));
            workbook.Styles.Add(this.GetWorksheetStyle("DefaultBoldCell", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, true, true, 0, null, null, false));
            workbook.Styles.Add(this.GetWorksheetStyle("DefaultIntegerCell", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, false, true, 0, null, Configuration.ExcelIntegerFormat, false));
            workbook.Styles.Add(this.GetWorksheetStyle("DefaultIntegerBoldCell", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, true, true, 0, null, Configuration.ExcelIntegerFormat, false));
            workbook.Styles.Add(this.GetWorksheetStyle("DefaultDoubleCell", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, false, true, 0, null, Configuration.ExcelDoubleFormat, false));
            workbook.Styles.Add(this.GetWorksheetStyle("DefaultDoubleBoldCell", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, true, true, 0, null, Configuration.ExcelDoubleFormat, false));
            workbook.Styles.Add(this.GetWorksheetStyle("DefaultNumberCell", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, false, true, 0, null, Configuration.ExcelDecimalFormat, false));
            workbook.Styles.Add(this.GetWorksheetStyle("DefaultNumberBoldCell", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, true, true, 0, null, Configuration.ExcelDecimalFormat, false));
            workbook.Styles.Add(this.GetWorksheetStyle("DefaultDateTimeCell", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, false, true, 0, null, Configuration.ExcelDateFormat, false));
            workbook.Styles.Add(this.GetWorksheetStyle("DefaultDateTimeBoldCell", StyleHorizontalAlignment.Automatic, StyleVerticalAlignment.Automatic, true, true, 0, null, Configuration.ExcelDateFormat, false));
            workbook.Styles.Add(this.GetWorksheetStyle("HeaderTop1", StyleHorizontalAlignment.Center, StyleVerticalAlignment.Center, true, false, 20, null, null, false));
            workbook.Styles.Add(this.GetWorksheetStyle("HeaderTop2", StyleHorizontalAlignment.Center, StyleVerticalAlignment.Center, true, false, 15, null, null, false));
            workbook.Styles.Add(this.GetWorksheetStyle("HeaderTop3", StyleHorizontalAlignment.Left, StyleVerticalAlignment.Center, true, false, 12, null, null, false));
            workbook.Styles.Add(this.GetWorksheetStyle("HeaderTop4", StyleHorizontalAlignment.Right, StyleVerticalAlignment.Center, true, false, 12, null, null, false));
            workbook.Styles.Add(this.GetWorksheetStyle("HeaderCenterAlign", StyleHorizontalAlignment.Center, StyleVerticalAlignment.Center, true, true, 0, "#C0C0C0", null, true));
            workbook.Styles.Add(this.GetWorksheetStyle("HeaderLeftAlign", StyleHorizontalAlignment.Left, StyleVerticalAlignment.Center, true, true, 0, "#C0C0C0", null, true));
            workbook.Styles.Add(this.GetWorksheetStyle("HeaderRightAlign", StyleHorizontalAlignment.Right, StyleVerticalAlignment.Center, true, true, 0, "#C0C0C0", null, true));

            return workbook;
        }

        private void AddWorksheet(Workbook workbook, DataTable dataTable, Int32 index)
        {
            Worksheet worksheet;

            if (String.IsNullOrEmpty(dataTable.TableName) == true)
            {
                worksheet = workbook.Worksheets.Add("Sheet" + (index + 1));
            }
            else
            {
                worksheet = workbook.Worksheets.Add(dataTable.TableName);
            }

            WorksheetRow worksheetRow = new WorksheetRow();

            Double totalWidth = 0;

            worksheet.Options.Print.ValidPrinterInfo = true;
            worksheet.Options.PageSetup.Layout.CenterHorizontal = true;
            worksheet.Options.PageSetup.Footer.Data = "Page &P of &N";

            foreach (DataColumn column in dataTable.Columns)
            {
                Int32 width = 0;
                Boolean hidden = false;

                if (column.ExtendedProperties.ContainsKey("Width"))
                {
                    Int32.TryParse(column.ExtendedProperties["Width"].ToString(), out width);
                }
                else
                {
                    switch (column.DataType.ToString())
                    {
                        case "System.String":
                            width = 100;
                            break;
                        case "System.DateTime":
                            width = 80;
                            break;
                        default:
                            width = 50;
                            break;
                    }
                }

                if (column.ExtendedProperties.ContainsKey("Hidden"))
                {
                    Boolean.TryParse(column.ExtendedProperties["Hidden"].ToString(), out hidden);
                }

                worksheet.Table.Columns.Add(new WorksheetColumn(width));
                worksheet.Table.Columns[worksheet.Table.Columns.Count - 1].Hidden = hidden;

                totalWidth += width;
            }

            if (totalWidth > 600)
            {
                worksheet.Options.Print.PaperSizeIndex = 5;
                worksheet.Options.PageSetup.Layout.Orientation = ExcelXmlWriter.Orientation.Landscape;
                worksheet.Options.Print.Scale = (Int32)Math.Round(75000 / totalWidth);
            }
            else if (totalWidth > 0)
            {
                worksheet.Options.Print.PaperSizeIndex = 9;
                worksheet.Options.Print.Scale = (Int32)Math.Round(45000 / totalWidth);
            }
            else
            {
                worksheet.Options.Print.PaperSizeIndex = 9;
                worksheet.Options.Print.Scale = 100;
            }

            if (this.Header != null)
            {
                foreach (WorksheetRow header in this.Header[index])
                {
                    worksheet.Table.Rows.Add(header);
                }
            }

            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; ++rowIndex)
            {
                Boolean boldRow = this.BoldRow(dataTable.Rows[rowIndex]);
                worksheetRow = worksheet.Table.Rows.Add();

                for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; ++columnIndex)
                {
                    Boolean boldColumn = this.BoldColumn(dataTable.Columns[columnIndex]);
                    Object cell = dataTable.Rows[rowIndex][columnIndex];
                    DataType dataType = GetDataType(dataTable.Columns[columnIndex].DataType);
                    String format = "";

                    if (dataTable.Columns[columnIndex].DataType == typeof(Decimal))
                    {
                        format = "Number";
                    }
                    else if (dataTable.Columns[columnIndex].DataType == typeof(Double))
                    {
                        format = "Double";
                    }
                    else if (dataType == DataType.Number)
                    {
                        format = "Integer";
                    }
                    else if (dataTable.Columns[columnIndex].DataType == typeof(DateTime))
                    {
                        format = "DateTime";
                    }

                    String style = boldRow || boldColumn ? "Default" + format + "BoldCell" : "Default" + format + "Cell";

                    if (cell != DBNull.Value)
                    {
                        if (dataType == DataType.DateTime)
                        {
                            worksheetRow.Cells.Add(Convert.ToDateTime(cell).ToString("yyyy-MM-dd"), dataType, style);
                        }
                        else
                        {
                            worksheetRow.Cells.Add(cell.ToString(), dataType, style);
                        }
                    }
                    else
                    {
                        worksheetRow.Cells.Add(null, DataType.String, style);
                    }
                }
            }
        }

        private void AddWorksheet(Workbook workbook, GridView gridView, Int32 index)
        {
            Worksheet worksheet = workbook.Worksheets.Add("Sheet" + (index + 1));
            WorksheetRow worksheetRow = new WorksheetRow();

            Int32 i = 0;
            Double width = 0;

            worksheet.Options.Print.ValidPrinterInfo = true;
            worksheet.Options.PageSetup.Layout.CenterHorizontal = true;
            worksheet.Options.PageSetup.Footer.Data = "Page &P of &N";

            if (this.Header != null && this.Header.Length > 0)
            {
                worksheet.Names.Add(new WorksheetNamedRange("Print_Titles", "=" + "Sheet" + (index + 1) + "!R" + this.Header[0].Length, false));
            }

            foreach (DataControlField column in gridView.Columns)
            {
                if (column.Visible)
                {
                    WorksheetStyle cellStyle = SetWorksheetStyle(workbook, LineStyleOption.Continuous, null, "column" + i + "row-1");
                    worksheet.Table.Columns.Add();
                    worksheetRow.Cells.Add(column.HeaderText, DataType.String, cellStyle.ID);

                    width += (Int32)Math.Round(column.HeaderStyle.Width.Value * 0.7);

                    i++;
                }
            }

            if (width > 600)
            {
                worksheet.Options.Print.PaperSizeIndex = 5;
                worksheet.Options.PageSetup.Layout.Orientation = ExcelXmlWriter.Orientation.Landscape;
                worksheet.Options.Print.Scale = (Int32)Math.Round(75000 / width);
            }
            else if (width > 0)
            {
                worksheet.Options.Print.PaperSizeIndex = 9;
                worksheet.Options.Print.Scale = (Int32)Math.Round(45000 / width);
            }
            else
            {
                worksheet.Options.Print.PaperSizeIndex = 9;
                worksheet.Options.Print.Scale = 100;
            }

            if (this.Header != null)
            {
                foreach (WorksheetRow header in this.Header[index])
                {
                    worksheet.Table.Rows.Add(header);
                }
            }
            else
            {
                worksheet.Table.Rows.Insert(0, worksheetRow);
            }

            for (int rowIndex = 0; rowIndex < gridView.Rows.Count; ++rowIndex)
            {
                if (gridView.Rows[rowIndex].Visible)
                {
                    worksheetRow = worksheet.Table.Rows.Add();

                    for (int columnIndex = 0; columnIndex < gridView.Columns.Count; ++columnIndex)
                    {
                        if (gridView.Columns[columnIndex].Visible)
                        {
                            TableCell cell = gridView.Rows[rowIndex].Cells[columnIndex];
                            WorksheetStyle cellStyle = SetWorksheetStyle(workbook, LineStyleOption.Continuous, null, "column" + columnIndex + "row" + rowIndex);

                            if (cell.Text != null && cell.Text != "&nbsp;")
                            {
                                worksheetRow.Cells.Add(cell.Text, DataType.String, cellStyle.ID);
                            }
                            else
                            {
                                worksheetRow.Cells.Add(null, DataType.String, cellStyle.ID);
                            }
                        }
                    }
                }
            }
        }

        private WorksheetStyle GetWorksheetStyle(String styleId, StyleHorizontalAlignment horizontalAlignment, StyleVerticalAlignment verticalAlignment, Boolean bold, Boolean border, Int32 fontSize, String color, String format, Boolean wrapText)
        {
            WorksheetStyle style = new WorksheetStyle(styleId);
            style.Alignment.Horizontal = horizontalAlignment;
            style.Alignment.Vertical = verticalAlignment;
            style.Alignment.WrapText = wrapText;
            style.Font.Bold = bold;
            style.Font.Size = fontSize;

            if (color != null)
            {
                style.Interior.Color = color;
                style.Interior.Pattern = StyleInteriorPattern.Solid;
            }

            //style.Font.Color = GetColorName(Color.Black);
            //style.Font.FontName = "Tahoma";
            //style.Font.Italic = false;
            //style.Font.Strikethrough = false;
            //style.Font.Underline = UnderlineStyle.None;

            if (border)
            {
                style.Borders.Add(StylePosition.Top, LineStyleOption.Continuous, 1, "Black");
                style.Borders.Add(StylePosition.Right, LineStyleOption.Continuous, 1, "Black");
                style.Borders.Add(StylePosition.Bottom, LineStyleOption.Continuous, 1, "Black");
                style.Borders.Add(StylePosition.Left, LineStyleOption.Continuous, 1, "Black");
            }

            if (format != null && format != "")
            {
                style.NumberFormat = format;
            }

            return style;
        }

        private static WorksheetStyle SetWorksheetStyle(Workbook workbook, LineStyleOption lineStyleOption, String format, String styleId)
        {
            WorksheetStyle worksheetStyle = null;


            worksheetStyle = new WorksheetStyle(styleId);

            worksheetStyle.Borders.Add(StylePosition.Top, lineStyleOption, 1, "Black");
            worksheetStyle.Borders.Add(StylePosition.Right, lineStyleOption, 1, "Black");
            worksheetStyle.Borders.Add(StylePosition.Bottom, lineStyleOption, 1, "Black");
            worksheetStyle.Borders.Add(StylePosition.Left, lineStyleOption, 1, "Black");

            if (format != null && format != "")
            {
                worksheetStyle.NumberFormat = format;
            }

            WorksheetStyle old = Contains(workbook, worksheetStyle);

            if (old != null)
            {
                worksheetStyle = old;
            }
            else
            {
                workbook.Styles.Add(worksheetStyle);
            }

            return worksheetStyle;
        }

        private static WorksheetStyle Contains(Workbook workbook, WorksheetStyle worksheetStyle)
        {
            foreach (WorksheetStyle wss in workbook.Styles)
            {
                if (wss.Interior.Color == worksheetStyle.Interior.Color)
                    if (wss.Interior.Pattern == worksheetStyle.Interior.Pattern)
                        if (wss.Font.Color == worksheetStyle.Font.Color)
                            if (wss.Font.Bold == worksheetStyle.Font.Bold)
                                if (wss.Font.FontName == worksheetStyle.Font.FontName)
                                    if (wss.Font.Italic == worksheetStyle.Font.Italic)
                                        if (wss.Font.Size == worksheetStyle.Font.Size)
                                            if (wss.Font.Strikethrough == worksheetStyle.Font.Strikethrough)
                                                if (wss.Font.Underline == worksheetStyle.Font.Underline)
                                                    if (wss.NumberFormat == worksheetStyle.NumberFormat)
                                                        if (wss.Borders.Count > 3 && wss.Borders[0].LineStyle == worksheetStyle.Borders[0].LineStyle)
                                                            if (wss.Borders[1].LineStyle == worksheetStyle.Borders[1].LineStyle)
                                                                if (wss.Borders[2].LineStyle == worksheetStyle.Borders[2].LineStyle)
                                                                    if (wss.Borders[3].LineStyle == worksheetStyle.Borders[3].LineStyle)
                                                                        return wss;
            }

            return null;
        }

        private static string GetColorName(Color color)
        {
            return "#" + color.ToArgb().ToString("X").Substring(2);
        }

        private static DataType GetDataType(Type valueType)
        {
            if (valueType == typeof(DateTime))
            {
                return DataType.DateTime;
            }
            else if (valueType == typeof(String))
            {
                return DataType.String;
            }
            else if (valueType == typeof(sbyte)
              || valueType == typeof(byte)
              || valueType == typeof(short)
              || valueType == typeof(ushort)
              || valueType == typeof(int)
              || valueType == typeof(uint)
              || valueType == typeof(long)
              || valueType == typeof(ulong)
              || valueType == typeof(float)
              || valueType == typeof(double)
              || valueType == typeof(decimal))
            {
                return DataType.Number;
            }
            else if (valueType.ToString() == "System.Nullable`1[System.DateTime]")
            {
                // if nullable datetime type is regular datetime instead of string
                // this will resolve dateformat mismatch in excel
                return DataType.DateTime;
            }
            else
            {
                return DataType.String;
            }
        }
    }
}
