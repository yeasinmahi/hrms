using System;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Asa.ExcelXmlWriter;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Web
{
    public abstract class GridPage : BasePage
    {
        protected const string COMMAND_ADD = "ADD";
        protected const string COMMAND_DELETE = "DELETE";
        protected const string COMMAND_EXCEL = "EXCEL";

        private GridViewSearchPanel gvspList;
        private Type _EntityType;
        private Type _BaseEntityType;

        protected virtual GridView GridView { get; set; }

        protected virtual Type EntityType
        {
            get { return _EntityType; }
            set
            {
                if (value != null && (value.BaseType == null || (value.BaseType.Name.StartsWith("EntityBase") == false && value.BaseType.Name.StartsWith("ViewBase") == false)))
                {
                    throw new Exception("Unsupported Type");
                }

                _EntityType = value;
            }
        }

        protected virtual Type BaseEntityType
        {
            get { return _BaseEntityType; }
            set
            {
                if (value != null && (value.BaseType == null || value.BaseType.Name.StartsWith("EntityBase") == false))
                {
                    throw new Exception("Unsupported Type");
                }

                _BaseEntityType = value;
            }
        }

        protected string SortColumn
        {
            get
            {
                if (ViewState["SortColumn"] == null)
                {
                    return String.Empty;
                }
                else
                {
                    return ViewState["SortColumn"].ToString();
                }
            }
            set
            {
                ViewState["SortColumn"] = value;
            }
        }

        protected string SortOrder
        {
            get
            {
                if (ViewState["SortOrder"] == null)
                {
                    return String.Empty;
                }
                else
                {
                    return ViewState["SortOrder"].ToString();
                }
            }
            set
            {
                ViewState["SortOrder"] = value;
            }
        }

        protected Int32 PageIndex
        {
            get { return DBUtility.ToInt32(ViewState["PageIndex"]); }
            set { ViewState["PageIndex"] = value; }
        }

        protected Int32 PageCount
        {
            get 
            {
                return (Int32)Math.Ceiling(Convert.ToDouble(RecordCount) / GridView.PageSize);
            }
        }

        protected Int32 RecordCount
        {
            get { return DBUtility.ToInt32(ViewState["RecordCount"]); }
            set { ViewState["RecordCount"] = value; }
        }

        protected string SortExpression
        {
            get
            {
                if (SortColumn != null && SortColumn != "")
                {
                    return SortColumn + " " + SortOrder;
                }

                return "";
            }
        }

        protected string FilterExpression
        {
            get
            {
                if (gvspList != null)
                {
                    return gvspList.WhereClause;
                }

                return "";
            }
        }

        protected virtual void LoadData()
        {
            if (GridView != null && EntityType != null)
            {
                GridView.DataSource = GetDataSource();
                GridView.DataBind();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            gvspList = (GridViewSearchPanel)Page.Master.FindControl("ContentPlaceHolder1").FindControl("gvspList");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack == false)
            {
                LoadData();
            }
        }

        protected virtual Object GetDataSource()
        {
            if (GridView != null && EntityType != null)
            {
                ParameterModifier[] modifiers = new ParameterModifier[1];
                modifiers[0] = new ParameterModifier(5);
                modifiers[0][4] = true;
                Object[] values = new Object[5];

                values[0] = FilterExpression;
                values[1] = SortExpression;
                values[2] = PageIndex * GridView.PageSize + 1;
                values[3] = GridView.PageSize;

                Object list = EntityType.BaseType.BaseType.InvokeMember("Find", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null, null, values, modifiers, null, null);

                RecordCount = Convert.ToInt32(values[4]);

                return list;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected virtual void SetGridEmptyDataText(GridView gv, int beforeFilterCount, int afterFilterCount)
        {
            if (afterFilterCount == 0 && beforeFilterCount > afterFilterCount)
            {
                gv.EmptyDataText = "No matching record(s) found.";
            }
            else if (beforeFilterCount == 0 && afterFilterCount == 0)
            {
                gv.EmptyDataText = "No record(s) available.";
            }
        }

        protected virtual void gvList_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (SortColumn != e.SortExpression)
            {
                SortColumn = e.SortExpression;
                SortOrder = "ASC";
            }
            else
            {
                SortOrder = (SortOrder == "ASC") ? "DESC" : "ASC";
            }

            object ds = GetDataSource();

            if (ds != null)
            {
                GridView gv = (GridView)sender;
                int pIndex = gv.PageIndex;
                gv.DataSource = ds;
                gv.PageIndex = pIndex;
                gv.DataBind();
            }
        }

        protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label space = new Label();
                space.Text = "&nbsp;";
                TableRow tr = (TableRow)e.Row.Controls[0].Parent;
                int columnSpan = tr.Controls.Count;
                tr.Controls.Clear();

                TableCell cell = new TableCell();
                cell.ColumnSpan = columnSpan;
                tr.Controls.Add(cell);

                if (PageIndex > 0)
                {
                    ImageButton firstPage = new ImageButton();
                    firstPage.ImageUrl = "~/Images/XGridFirstPage.gif";
                    firstPage.ToolTip = "First Page";
                    firstPage.Click += firstPage_Click;
                    cell.Controls.Add(firstPage);
                    cell.Controls.Add(space);

                    ImageButton previousPage = new ImageButton();
                    previousPage.ImageUrl = "~/Images/XGridPreviousPage.gif";
                    previousPage.ToolTip = "Previous Page";
                    previousPage.Click += previousPage_Click;
                    cell.Controls.Add(previousPage);
                    space = new Label();
                    space.Text = "&nbsp;";
                    cell.Controls.Add(space);
                }

                if (PageIndex < PageCount - 1)
                {
                    ImageButton nextPage = new ImageButton();
                    nextPage.ImageUrl = "~/Images/XGridNextPage.gif";
                    nextPage.ToolTip = "Next Page";
                    nextPage.Click += nextPage_Click;
                    cell.Controls.Add(nextPage);
                    space = new Label();
                    space.Text = "&nbsp;";
                    cell.Controls.Add(space);

                    ImageButton lastPage = new ImageButton();
                    lastPage.ImageUrl = "~/Images/XGridLastPage.gif";
                    lastPage.ToolTip = "Last Page";
                    lastPage.Click += lastPage_Click;
                    cell.Controls.Add(lastPage);
                    space = new Label();
                    space.Text = "&nbsp;";
                    cell.Controls.Add(space);
                }

                HtmlInputText label = new HtmlInputText();
                label.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                label.Style.Add(HtmlTextWriterStyle.Width, PageCount == 1 ? "99%" : PageIndex == 0 || PageIndex == PageCount - 1 ? "95%" : "91%");
                label.Style.Add(HtmlTextWriterStyle.BackgroundColor, "transparent");
                label.Style.Add("border", "none");
                label.Attributes.Add("readonly", "true");

                label.Value = "Records: " + (PageIndex * GridView.PageSize + 1) + " - " + (PageIndex == PageCount - 1 ? RecordCount : ((PageIndex + 1) * GridView.PageSize)) + " of " + RecordCount;
                cell.Controls.Add(label);
                 
            }
        }

        void lastPage_Click(object sender, ImageClickEventArgs e)
        {
            PageIndex = PageCount - 1;
            LoadData();
        }

        void nextPage_Click(object sender, ImageClickEventArgs e)
        {
            PageIndex++;
            LoadData();
        }

        void previousPage_Click(object sender, ImageClickEventArgs e)
        {
            PageIndex--;
            LoadData();
        }

        void firstPage_Click(object sender, ImageClickEventArgs e)
        {
            PageIndex = 0;
            LoadData();
        }

        protected void gvspList_SearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                PageIndex = 0;
                LoadData();
            }
            catch (Exception ex)
            {
                ShowUiMessage(ex);
            }
        }

        protected void gvspList_ResetButtonClicked(object sender, EventArgs e)
        {
            try
            {
                PageIndex = 0;
                LoadData();
            }
            catch (Exception ex)
            {
                ShowUiMessage(ex);
            }
        }

        protected virtual void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected abstract string GetAddPageUrl();

        protected virtual Message ValidateAdd()
        {
            return new Message();
        }

        protected virtual Message ValidateDelating(Int32 id)
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Selected item(s) deleted successfully";

            return msg;
        }

        protected virtual Message Delete(TransactionManager transactionManager, Int32 id)
        {
            Message msg = ValidateDelating(id);

            if (msg.Type == MessageType.Information)
            {
                if (BaseEntityType != null)
                {
                    BaseEntityType.BaseType.InvokeMember("Delete", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null, null, new Object[] { transactionManager, id });
                }
                else
                {
                    EntityType.BaseType.InvokeMember("Delete", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null, null, new Object[] { transactionManager, id });
                }
            }

            return msg;
        }

        protected virtual Message DeleteSelected()
        {
            if (GridView != null && EntityType != null)
            {
                Message msg = new Message();
                TransactionManager = new TransactionManager(true, "Delete [" + EntityType.Name + "]");
                msg.Type = MessageType.Information;
                msg.Msg = "Selected item(s) deleted successfully";

                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    GridViewRow row = GridView.Rows[i];
                    Boolean selected = ((CheckBox)row.FindControl("chkSelect")).Checked;

                    if (selected)
                    {
                        Int32 id = Convert.ToInt32(GridView.DataKeys[i].Value);
                        msg = Delete(TransactionManager, id);

                        if (msg.Type != MessageType.Information)
                        {
                            TransactionManager.Rollback();
                            return msg;
                        }
                    }
                }

                TransactionManager.Commit();

                LoadData();

                return msg;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected virtual Message ExportToExcel()
        {
            Message msg = new Message();

            if (GridView != null)
            {
                if (GridView.Rows.Count > 65500)
                {
                    msg.Msg = "Too many rows to display in excel";
                    msg.Type = MessageType.Information;
                    return msg;
                }

                if (GridView.Rows.Count == 0)
                {
                    msg.Type = MessageType.Information;
                    return msg;
                }

                WorksheetRow[] header = new WorksheetRow[3];
                header[0] = new WorksheetRow();
                header[1] = new WorksheetRow();
                header[2] = new WorksheetRow();

                foreach (DataControlField column in GridView.Columns)
                {
                    if (column.Visible)
                    {
                        String align = "Left";

                        if (column.HeaderStyle.HorizontalAlign == HorizontalAlign.Right)
                        {
                            align = "Right";
                        }
                        else if (column.HeaderStyle.HorizontalAlign == HorizontalAlign.Center)
                        {
                            align = "Center";
                        }

                        header[2].Cells.Add(column.HeaderText, DataType.String, "Header" + align + "Align");
                    }
                }

                if (header[2].Cells.Count > 1)
                {
                    header[0].Cells.Add(Page.Title, DataType.String, "HeaderTop1").MergeAcross = header[2].Cells.Count - 1;
                    //header[1].Cells.Add("Branch Name: " + Branch.CurrentBranch.FullName, DataType.String, "HeaderTop3").MergeAcross = header[2].Cells.Count - 1;
                }
                else
                {
                    header[0].Cells.Add(Page.Title, DataType.String, "HeaderTop1");
                    //header[1].Cells.Add("Branch Name: " + Branch.CurrentBranch.FullName, DataType.String, "HeaderTop3");
                }

                ExcelReportUtility.Instance.DataSource = GridView;
                ExcelReportUtility.Instance.Header = new WorksheetRow[][] { header };
                ExcelReportUtility.Instance.ViewReport();
            }
            else
            {
                throw new NotImplementedException();
            }

            return msg;
        }

        protected override void HandleCommonCommand(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case COMMAND_ADD:
                    Message msg = ValidateAdd();
                    if (msg.Type != MessageType.Information) 
                        ShowUiMessage(msg);
                    else
                        UIUtility.Transfer(Page, GetAddPageUrl());
                    break;
                case COMMAND_DELETE:
                    msg = DeleteSelected();
                    ShowUiMessage(msg);
                    break;
                case COMMAND_EXCEL:
                    msg = ExportToExcel();
                    ShowUiMessage(msg);
                    break;
                default:
                    HandleSpecialCommand(sender, e);
                    break;

            }
        }
    }
}