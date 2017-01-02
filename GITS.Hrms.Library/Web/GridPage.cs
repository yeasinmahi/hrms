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

        private GridViewSearchPanel gvspList = null;
        private GridView _GridView;
        private Type _EntityType;
        private Type _BaseEntityType;

        protected virtual GridView GridView
        {
            get { return this._GridView; }
            set { this._GridView = value; }
        }

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
                return (Int32)Math.Ceiling(Convert.ToDouble(this.RecordCount) / this.GridView.PageSize);
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
                if (this.SortColumn != null && this.SortColumn != "")
                {
                    return this.SortColumn + " " + this.SortOrder;
                }

                return "";
            }
        }

        protected string FilterExpression
        {
            get
            {
                if (this.gvspList != null)
                {
                    return this.gvspList.WhereClause;
                }

                return "";
            }
        }

        public GridPage()
        {
        }

        protected virtual void LoadData()
        {
            if (this.GridView != null && this.EntityType != null)
            {
                this.GridView.DataSource = this.GetDataSource();
                this.GridView.DataBind();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.gvspList = (GridViewSearchPanel)Page.Master.FindControl("ContentPlaceHolder1").FindControl("gvspList");
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
            if (this.GridView != null && this.EntityType != null)
            {
                ParameterModifier[] modifiers = new ParameterModifier[1];
                modifiers[0] = new ParameterModifier(5);
                modifiers[0][4] = true;
                Object[] values = new Object[5];

                values[0] = this.FilterExpression;
                values[1] = this.SortExpression;
                values[2] = this.PageIndex * this.GridView.PageSize + 1;
                values[3] = this.GridView.PageSize;

                Object list = this.EntityType.BaseType.BaseType.InvokeMember("Find", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public, null, null, values, modifiers, null, null);

                this.RecordCount = Convert.ToInt32(values[4]);

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
            if (this.SortColumn != e.SortExpression)
            {
                this.SortColumn = e.SortExpression;
                this.SortOrder = "ASC";
            }
            else
            {
                this.SortOrder = (this.SortOrder == "ASC") ? "DESC" : "ASC";
            }

            object ds = this.GetDataSource();

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

                if (this.PageIndex > 0)
                {
                    ImageButton firstPage = new ImageButton();
                    firstPage.ImageUrl = "~/Images/XGridFirstPage.gif";
                    firstPage.ToolTip = "First Page";
                    firstPage.Click += new ImageClickEventHandler(firstPage_Click);
                    cell.Controls.Add(firstPage);
                    cell.Controls.Add(space);

                    ImageButton previousPage = new ImageButton();
                    previousPage.ImageUrl = "~/Images/XGridPreviousPage.gif";
                    previousPage.ToolTip = "Previous Page";
                    previousPage.Click += new ImageClickEventHandler(previousPage_Click);
                    cell.Controls.Add(previousPage);
                    space = new Label();
                    space.Text = "&nbsp;";
                    cell.Controls.Add(space);
                }

                if (this.PageIndex < this.PageCount - 1)
                {
                    ImageButton nextPage = new ImageButton();
                    nextPage.ImageUrl = "~/Images/XGridNextPage.gif";
                    nextPage.ToolTip = "Next Page";
                    nextPage.Click += new ImageClickEventHandler(nextPage_Click);
                    cell.Controls.Add(nextPage);
                    space = new Label();
                    space.Text = "&nbsp;";
                    cell.Controls.Add(space);

                    ImageButton lastPage = new ImageButton();
                    lastPage.ImageUrl = "~/Images/XGridLastPage.gif";
                    lastPage.ToolTip = "Last Page";
                    lastPage.Click += new ImageClickEventHandler(lastPage_Click);
                    cell.Controls.Add(lastPage);
                    space = new Label();
                    space.Text = "&nbsp;";
                    cell.Controls.Add(space);
                }

                HtmlInputText label = new HtmlInputText();
                label.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                label.Style.Add(HtmlTextWriterStyle.Width, this.PageCount == 1 ? "99%" : this.PageIndex == 0 || this.PageIndex == this.PageCount - 1 ? "95%" : "91%");
                label.Style.Add(HtmlTextWriterStyle.BackgroundColor, "transparent");
                label.Style.Add("border", "none");
                label.Attributes.Add("readonly", "true");

                label.Value = "Records: " + (this.PageIndex * this.GridView.PageSize + 1) + " - " + (this.PageIndex == this.PageCount - 1 ? this.RecordCount : ((this.PageIndex + 1) * this.GridView.PageSize)) + " of " + this.RecordCount;
                cell.Controls.Add(label);
                 
            }
        }

        void lastPage_Click(object sender, ImageClickEventArgs e)
        {
            this.PageIndex = this.PageCount - 1;
            this.LoadData();
        }

        void nextPage_Click(object sender, ImageClickEventArgs e)
        {
            this.PageIndex++;
            this.LoadData();
        }

        void previousPage_Click(object sender, ImageClickEventArgs e)
        {
            this.PageIndex--;
            this.LoadData();
        }

        void firstPage_Click(object sender, ImageClickEventArgs e)
        {
            this.PageIndex = 0;
            this.LoadData();
        }

        protected void gvspList_SearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                this.PageIndex = 0;
                LoadData();
            }
            catch (Exception ex)
            {
                ShowUIMessage(ex);
            }
        }

        protected void gvspList_ResetButtonClicked(object sender, EventArgs e)
        {
            try
            {
                this.PageIndex = 0;
                LoadData();
            }
            catch (Exception ex)
            {
                ShowUIMessage(ex);
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
            Message msg = this.ValidateDelating(id);

            if (msg.Type == MessageType.Information)
            {
                if (this.BaseEntityType != null)
                {
                    this.BaseEntityType.BaseType.InvokeMember("Delete", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public, null, null, new Object[] { transactionManager, id });
                }
                else
                {
                    this.EntityType.BaseType.InvokeMember("Delete", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public, null, null, new Object[] { transactionManager, id });
                }
            }

            return msg;
        }

        protected virtual Message DeleteSelected()
        {
            if (this.GridView != null && this.EntityType != null)
            {
                Message msg = new Message();
                this.TransactionManager = new TransactionManager(true, "Delete [" + this.EntityType.Name + "]");
                msg.Type = MessageType.Information;
                msg.Msg = "Selected item(s) deleted successfully";

                for (int i = 0; i < this.GridView.Rows.Count; i++)
                {
                    GridViewRow row = this.GridView.Rows[i];
                    Boolean selected = ((CheckBox)row.FindControl("chkSelect")).Checked;

                    if (selected)
                    {
                        Int32 id = Convert.ToInt32(this.GridView.DataKeys[i].Value);
                        msg = this.Delete(this.TransactionManager, id);

                        if (msg.Type != MessageType.Information)
                        {
                            this.TransactionManager.Rollback();
                            return msg;
                        }
                    }
                }

                this.TransactionManager.Commit();

                this.LoadData();

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

            if (this.GridView != null)
            {
                if (this.GridView.Rows.Count > 65500)
                {
                    msg.Msg = "Too many rows to display in excel";
                    msg.Type = MessageType.Information;
                    return msg;
                }

                if (this.GridView.Rows.Count == 0)
                {
                    msg.Type = MessageType.Information;
                    return msg;
                }

                WorksheetRow[] header = new WorksheetRow[3];
                header[0] = new WorksheetRow();
                header[1] = new WorksheetRow();
                header[2] = new WorksheetRow();

                foreach (DataControlField column in this.GridView.Columns)
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

                ExcelReportUtility.Instance.DataSource = this.GridView;
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
                        this.ShowUIMessage(msg);
                    else
                        UIUtility.Transfer(Page, this.GetAddPageUrl());
                    break;
                case COMMAND_DELETE:
                    msg = this.DeleteSelected();
                    this.ShowUIMessage(msg);
                    break;
                case COMMAND_EXCEL:
                    msg = this.ExportToExcel();
                    this.ShowUIMessage(msg);
                    break;
                default:
                    this.HandleSpecialCommand(sender, e);
                    break;

            }
        }
    }
}