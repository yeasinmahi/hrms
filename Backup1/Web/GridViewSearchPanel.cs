using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;

namespace Asa.Hrms.Web
{
    [
        Designer(typeof(System.Web.UI.Design.WebControls.CompositeControlDesigner)),
        ToolboxData("<{0}:GridViewSearchPanel runat=\"server\" GridViewControl=\"GridView1\" Filter=\"\" OnSearchButtonClicked=\"cmdFind_Click\" OnResetButtonClicked=\"cmdFind_Click\" />")
    ]
    public class GridViewSearchPanel : CompositeControl
    {
        #region Fields
        private string _gridViewControlID = string.Empty;
        private string _businessEntityType = string.Empty;
        private string _listItems = string.Empty;
        private bool _showGridColumns = true;
        private GridView GridView1;

        private DropDownList cboFieldName;
        private DropDownList cboOperator;
        private TextBox txtKeyword;
        #endregion

        public event EventHandler SearchButtonClicked;
        public event EventHandler ResetButtonClicked;

        public enum Mode
        {
            Simple,
            Advance
        }

        public GridViewSearchPanel()
        {
        }

        #region Properties
        [
        Browsable(true),
        Description("Set / Gets the GridView ControlID"),
        Category("Misc"),
        DefaultValue(""),
        ]
        public string GridViewControlID
        {
            get { return _gridViewControlID; }
            set { _gridViewControlID = value; }
        }

        [
        Browsable(true),
        Description("Set / Gets the List Items"),
        Category("Misc"),
        DefaultValue(""),
        ]
        public string ListItems
        {
            get { return _listItems; }
            set { _listItems = value; }
        }

        [
        Browsable(true),
        Description("Set / Gets the Show Grid Columns"),
        Category("Misc"),
        DefaultValue(true),
        ]
        public bool ShowGridColumns
        {
            get { return _showGridColumns; }
            set { _showGridColumns = value; }
        }

        public string WhereClause
        {
            get
            {
                if (ViewState["_whereClause"] != null)
                {
                    return (string)ViewState["_whereClause"];
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["_whereClause"] = value;
            }
        }

        [
        Browsable(true),
        Description("Set / Gets the Filter criteria"),
        Category("Misc"),
        DefaultValue(""),
        ]
        public string Filter
        {
            get
            {
                if (ViewState["_filter"] != null)
                {
                    return (string)ViewState["_filter"];
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                ViewState["_filter"] = value;
            }
        }

        [
        Browsable(true),
        Description("Set / Gets the Search Field Name"),
        Category("Misc"),
        DefaultValue(""),
        ]
        public string SearchFieldName
        {
            get
            {
                EnsureChildControls();
                return cboFieldName.SelectedValue;
            }
            set
            {
                EnsureChildControls();
                ListItem fieldItem = cboFieldName.Items.FindByValue(value);

                if (fieldItem != null)
                {
                    cboFieldName.SelectedItem.Selected = false; // deselect any currently selected item
                    fieldItem.Selected = true;
                }
            }
        }

        [
        Browsable(true),
        Description("Set / Gets the Search keyword"),
        Category("Misc"),
        DefaultValue(""),
        ]
        public string SearchKeyword
        {
            get
            {
                EnsureChildControls();
                return txtKeyword.Text;
            }
            set
            {
                EnsureChildControls();
                txtKeyword.Text = value;
            }
        }

        [
        Browsable(true),
        Description("Whether the control causes validation to fire"),
        Category("Behavior"),
        DefaultValue("true"),
        ]
        public bool CausesValidation
        {
            get
            {
                if (ViewState["_causesValidation"] != null)
                {
                    return (bool)ViewState["_causesValidation"];
                }
                else
                {
                    return true;
                }
            }
            set
            {
                ViewState["_causesValidation"] = value;
            }
        }

        [
        Browsable(true),
        Description("Text to display for the 'Look For' label"),
        Category("Misc"),
        DefaultValue("Look For:"),
        ]
        public string LookForText
        {
            get
            {
                if (ViewState["_lookForText"] != null)
                {
                    return (string)ViewState["_lookForText"];
                }
                else
                {
                    return "Look For:";
                }
            }
            set
            {
                ViewState["_lookForText"] = value;
            }
        }

        [
        Browsable(true),
        Description("Text to display for the 'Which' label"),
        Category("Misc"),
        DefaultValue("Which:"),
        ]
        public string WhichText
        {
            get
            {
                if (ViewState["_whichText"] != null)
                {
                    return (string)ViewState["_whichText"];
                }
                else
                {
                    return "Which:";
                }
            }
            set
            {
                ViewState["_whichText"] = value;
            }
        }

        [
        Browsable(true),
        Description("Display mode of search panel"),
        Category("Misc"),
        DefaultValue(Mode.Simple),
        ]
        public Mode DisplayMode
        {
            get
            {
                if (ViewState["_displayMode"] != null)
                {
                    return (Mode)ViewState["_displayMode"];
                }
                else
                {
                    return Mode.Simple;
                }
            }
            set
            {
                ViewState["_displayMode"] = value;
            }
        }
        #endregion

        public static Control FindControl(Control control, params String[] controlIds)
        {
            if (control != null)
            {
                foreach (String controlId in controlIds)
                {
                    control = control.FindControl(controlId);

                    if (control == null)
                    {
                        break;
                    }
                }
            }

            return control;
        }

        private static string GetUniqueGridViewName(GridView gridView)
        {
            return gridView.Page.Request.Url.AbsoluteUri + gridView.UniqueID;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.EnsureChildControls();

            if (Page.IsPostBack == false)
            {
                if ((string.IsNullOrEmpty(txtKeyword.Text) == false && string.IsNullOrEmpty(cboFieldName.SelectedValue) == false) || string.IsNullOrEmpty(Filter) == false)
                {
                    this.DataBind();
                }
            }
        }

        public override void DataBind()
        {
            base.DataBind();
            BuildSearch();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.EnsureChildControls();
            base.Render(writer);
        }

        private TableRow GetTableRow(int rowIndex)
        {
            TableRow tr = new TableRow();
            CheckBox chkSelect = new CheckBox();
            DropDownList fieldName = new DropDownList();
            DropDownList operatorName = new DropDownList();
            TextBox keyword = new TextBox();
            Button cmdSearch = new Button();
            LinkButton lbMore = new LinkButton();
            TableCell td = null;

            chkSelect.ID = "chkSelect" + rowIndex;
            chkSelect.Text = "";

            fieldName.ID = "cboFieldName" + rowIndex;

            foreach (ListItem li in this.cboFieldName.Items)
            {
                ListItem li2 = new ListItem();
                li2.Text = li.Text;
                li2.Value = li.Value;

                if (li.Attributes["FieldType"] != null)
                {
                    li2.Attributes.Add("FieldType", li.Attributes["FieldType"]);
                }

                fieldName.Items.Add(li2);
            }

            fieldName.Items[rowIndex].Selected = true;

            fieldName.AutoPostBack = true;
            fieldName.SelectedIndexChanged += new EventHandler(cboFieldName_SelectedIndexChanged);

            operatorName.ID = "cboOperator" + rowIndex;
            this.LoadOperator(fieldName, operatorName);

            keyword.ID = "txtKeyword" + rowIndex;

            //cmdSearch.ID = "cmdSearch" + rowIndex;
            //cmdSearch.Text = "Search";
            //cmdSearch.CausesValidation = CausesValidation;
            //cmdSearch.Click += new EventHandler(cmdSearch_Click);

            //lbMore.ID = "lbMore" + rowIndex;
            //lbMore.Text = "More";
            //lbMore.CausesValidation = CausesValidation;
            //lbMore.Click += new EventHandler(lbMore_Click);

            td = new TableCell();
            td.Controls.Add(chkSelect);
            tr.Cells.Add(td);

            td = new TableCell();
            td.Controls.Add(fieldName);
            tr.Cells.Add(td);

            td = new TableCell();
            td.Controls.Add(operatorName);
            tr.Cells.Add(td);

            td = new TableCell();
            td.Controls.Add(keyword);
            tr.Cells.Add(td);

            //td = new TableCell();
            //td.Controls.Add(cmdSearch);
            //tr.Cells.Add(td);

            //td = new TableCell();
            //td.Controls.Add(lbMore);
            //tr.Cells.Add(td);

            return tr;
        }

        protected override void CreateChildControls()
        {
            base.Controls.Clear();

            cboFieldName = new DropDownList();
            cboFieldName.ID = "cboFieldName";

            if (base.DesignMode == false)
            {
                GridView1 = (GridView)this.Parent.FindControl(this.GridViewControlID);

                if (GridView1 != null && ShowGridColumns)
                {
                    foreach (DataControlField dcf in GridView1.Columns)
                    {
                        if (dcf is Asa.Hrms.Web.BoundField)
                        {
                            Asa.Hrms.Web.BoundField df = dcf as Asa.Hrms.Web.BoundField;

                            if (df.DataField != "Id")
                            {
                                ListItem li = new ListItem();
                                li.Attributes.Add("FieldType", Convert.ToInt32(df.FieldType).ToString());
                                li.Text = df.HeaderText;
                                li.Value = df.DataField;
                                cboFieldName.Items.Add(li);
                            }
                        }
                        else if (dcf is System.Web.UI.WebControls.BoundField)
                        {
                            System.Web.UI.WebControls.BoundField df = dcf as System.Web.UI.WebControls.BoundField;

                            if (df.DataField != "Id")
                            {
                                ListItem li = new ListItem();
                                li.Text = df.HeaderText;
                                li.Value = df.DataField;
                                cboFieldName.Items.Add(li);
                            }
                        }
                        else if (dcf is HyperLinkField)
                        {
                            HyperLinkField hlf = dcf as HyperLinkField;

                            if (hlf.DataTextField != "Id")
                            {
                                ListItem li = new ListItem();
                                li.Text = hlf.HeaderText;
                                li.Value = hlf.DataTextField;
                                cboFieldName.Items.Add(li);
                            }
                        }
                        else if (dcf is TemplateField)
                        {
                            TemplateField tf = dcf as TemplateField;

                            if (tf.SortExpression != "")
                            {
                                ListItem li = new ListItem();
                                li.Text = tf.SortExpression;
                                li.Value = tf.SortExpression;
                                cboFieldName.Items.Add(li);
                            }
                        }
                    }
                }

                if (ListItems != "")
                {
                    string[] items = ListItems.Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string item in items)
                    {
                        if (item != "")
                        {
                            string[] subItems = item.Replace("<", "").Split(new char[] { ',' });

                            if (subItems.Length == 2)
                            {
                                ListItem li = new ListItem();
                                li.Text = subItems[0].Trim();
                                li.Value = subItems[1].Trim();
                                cboFieldName.Items.Add(li);
                            }
                        }
                    }
                }
            }
            else
            {
                cboFieldName.Items.Add(new ListItem("FieldName", "FieldValue"));
            }

            this.cboFieldName.AutoPostBack = true;
            this.cboFieldName.SelectedIndexChanged += new EventHandler(cboFieldName_SelectedIndexChanged);

            #region UI implementation
            cboOperator = new DropDownList();
            cboOperator.ID = "cboOperator";
            this.LoadOperator(cboFieldName, cboOperator);

            txtKeyword = new TextBox();
            txtKeyword.ID = "txtKeyword";

            Label lblLookFor = new Label();
            lblLookFor.ID = "lblLookFor";
            lblLookFor.Text = LookForText;

            Label lblWhich = new Label();
            lblWhich.ID = "lblWhich";
            lblWhich.Text = WhichText;

            Button cmdSearch = new Button();
            cmdSearch.ID = "cmdSearch";
            cmdSearch.Text = "Search";
            cmdSearch.CausesValidation = CausesValidation;
            cmdSearch.Click += new EventHandler(cmdSearch_Click);

            Button cmdReset = new Button();
            cmdReset.ID = "cmdReset";
            cmdReset.Text = "Reset";
            cmdReset.CausesValidation = CausesValidation;
            cmdReset.Click += new EventHandler(cmdReset_Click);

            Table tbl = new Table();
            tbl.ID = "tblSearchPanel";
            TableRow tr = new TableRow();
            tr.ID = "trSimple";
            TableCell td;

            td = new TableCell();
            td.Controls.Add(lblLookFor);
            tr.Cells.Add(td);

            td = new TableCell();
            td.Controls.Add(cboFieldName);
            tr.Cells.Add(td);

            td = new TableCell();
            td.Controls.Add(lblWhich);
            tr.Cells.Add(td);

            td = new TableCell();
            td.Controls.Add(cboOperator);
            tr.Cells.Add(td);

            td = new TableCell();
            td.Controls.Add(txtKeyword);
            tr.Cells.Add(td);

            td = new TableCell();
            td.Controls.Add(cmdSearch);
            tr.Cells.Add(td);

            td = new TableCell();
            td.Controls.Add(cmdReset);
            tr.Cells.Add(td);

            LinkButton lbAdvance = new LinkButton();
            lbAdvance.ID = "lbAdvance";
            lbAdvance.Text = "Advance";
            lbAdvance.CausesValidation = CausesValidation;
            lbAdvance.Click += new EventHandler(lbAdvance_Click);

            td = new TableCell();
            td.Controls.Add(lbAdvance);
            tr.Cells.Add(td);

            tbl.Rows.Add(tr);

            tr = new TableRow();
            tr.ID = "trAdvance";
            tr.Visible = false;

            td = new TableCell();
            td.ID = "tdAdvance";
            td.ColumnSpan = 8;
            tr.Cells.Add(td);
            tbl.Rows.Add(tr);

            base.Controls.Add(tbl);

            tbl = new Table();
            tbl.ID = "tblAdvance";
            td.Controls.Add(tbl);

            for (int i = 0; i < cboFieldName.Items.Count - 1; i++)
            {
                tbl.Rows.Add(this.GetTableRow(tbl.Rows.Count));

                CheckBox chkOr = new CheckBox();
                chkOr.ID = "chkOr" + i;
                chkOr.Text = "Or";

                td = new TableCell();
                td.Controls.Add(chkOr);
                tr.Cells.Add(td);

                tbl.Rows[tbl.Rows.Count - 1].Cells.Add(td);
            }

            tbl.Rows.Add(this.GetTableRow(tbl.Rows.Count));
            tr = tbl.Rows[tbl.Rows.Count - 1];

            Button cmdAdvanceSearch = new Button();
            cmdAdvanceSearch.ID = "cmdAdvanceSearch";
            cmdAdvanceSearch.Text = "Search";
            cmdAdvanceSearch.CausesValidation = CausesValidation;
            cmdAdvanceSearch.Click += new EventHandler(cmdSearch_Click);

            //Button cmdAdvanceReset = new Button();
            //cmdAdvanceReset.ID = "cmdAdvanceReset";
            //cmdAdvanceReset.Text = "Reset";
            //cmdAdvanceReset.CausesValidation = CausesValidation;
            //cmdAdvanceReset.Click += new EventHandler(cmdReset_Click);

            LinkButton lbSimple = new LinkButton();
            lbSimple.ID = "lbSimple";
            lbSimple.Text = "Simple";
            lbSimple.CausesValidation = CausesValidation;
            lbSimple.Click += new EventHandler(lbAdvance_Click);

            td = new TableCell();
            td.Controls.Add(cmdAdvanceSearch);
            tr.Cells.Add(td);

            //td = new TableCell();
            //td.Controls.Add(cmdAdvanceReset);
            //tr.Cells.Add(td);

            td = new TableCell();
            td.Controls.Add(lbSimple);
            tr.Cells.Add(td);

            #endregion

            base.ClearChildViewState();
        }

        void cboFieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList fieldName = (DropDownList)sender;
            DropDownList operatorName = cboOperator;
            string fieldType = fieldName.SelectedItem.Attributes["FieldType"];

            if (fieldName.ID != "cboFieldName")
            {
                operatorName = (DropDownList)this.FindControl("tblSearchPanel").FindControl("trAdvance").FindControl("tdAdvance").FindControl("tblAdvance").FindControl("cboOperator" + fieldName.ID.Replace("cboFieldName", ""));
            }

            if (operatorName != null)
            {
                this.LoadOperator(fieldName, operatorName);
            }
        }

        private void LoadOperator(DropDownList fieldName, DropDownList operatorName)
        {
            string fieldType = fieldName.SelectedItem.Attributes["FieldType"];

            operatorName.Items.Clear();

            if (fieldType == null || fieldType == "" || (BoundField.FieldTypes)Convert.ToInt32(fieldType) != BoundField.FieldTypes.DateTime)
            {
                operatorName.Items.Add(new ListItem("contains", "CONTAINS"));
                operatorName.Items.Add(new ListItem("not contains", "NOT CONTAINS"));
            }

            operatorName.Items.Add(new ListItem("equals", "EQUAL"));
            operatorName.Items.Add(new ListItem("not equals", "NOT EQUAL"));
            operatorName.Items.Add(new ListItem("greater than", "GREATER THAN"));
            operatorName.Items.Add(new ListItem("less than", "LESS THAN"));

            if (fieldType == null || fieldType == "" || (BoundField.FieldTypes)Convert.ToInt32(fieldType) != BoundField.FieldTypes.DateTime)
            {
                operatorName.Items.Add(new ListItem("starts with", "STARTS WITH"));
                operatorName.Items.Add(new ListItem("ends with", "ENDS WITH"));
            }

            operatorName.Items.Add(new ListItem("is null", "IS NULL"));
            operatorName.Items.Add(new ListItem("is not null", "IS NOT NULL"));
        }

        #region Event Methods
        void cmdReset_Click(object sender, EventArgs e)
        {
            cboFieldName.SelectedIndex = 0;
            cboOperator.SelectedIndex = 0;
            txtKeyword.Text = string.Empty;

            WhereClause = string.Empty;

            if (ResetButtonClicked != null)
            {
                ResetButtonClicked(sender, e);
            }
        }

        void cmdSearch_Click(object sender, EventArgs e)
        {
            this.DataBind();

            if (SearchButtonClicked != null)
            {
                SearchButtonClicked(sender, e);
            }
        }

        void lbAdvance_Click(object sender, EventArgs e)
        {
            LinkButton lbAdvance = (LinkButton)sender;
            TableRow trAdvance = (TableRow)lbAdvance.Parent.Parent.Parent.Parent.FindControl("trAdvance");
            TableRow trSimple = (TableRow)lbAdvance.Parent.Parent.Parent.Parent.FindControl("trSimple");

            if (this.DisplayMode == Mode.Simple)
            {
                DisplayMode = Mode.Advance;

                trAdvance.Visible = true;
                trSimple.Visible = false;
            }
            else
            {
                DisplayMode = Mode.Simple;

                trAdvance.Visible = false;
                trSimple.Visible = true;
            }
        }

        private string SimpleSearch(string operatorValue, string fieldName, TextBox txtKeyword, string fieldType)
        {
            string clause = "";
            string searchText = txtKeyword.Text;

            switch (operatorValue)
            {
                case "CONTAINS":
                    clause = "LIKE '%{1}%'";
                    break;
                case "NOT CONTAINS":
                    clause = "NOT LIKE '%{1}%'";
                    break;
                case "EQUAL":
                    clause = "= '{1}'";
                    break;
                case "NOT EQUAL":
                    clause = "<> '{1}'";
                    break;
                case "GREATER THAN":
                    clause = "> '{1}'";
                    break;
                case "LESS THAN":
                    clause = "< '{1}'";
                    break;
                case "STARTS WITH":
                    clause = "LIKE '{1}%'";
                    break;
                case "ENDS WITH":
                    clause = "LIKE '%{1}'";
                    break;
                case "IS NULL":
                case "IS NOT NULL":
                    clause = operatorValue;
                    txtKeyword.Text = "";
                    break;
            }

            if (fieldType != null && fieldType != "" && clause != operatorValue)
            {
                switch ((BoundField.FieldTypes)Convert.ToInt32(fieldType))
                {
                    case BoundField.FieldTypes.String:
                    case BoundField.FieldTypes.Int32:
                    case BoundField.FieldTypes.Int64:
                    case BoundField.FieldTypes.Double:
                    case BoundField.FieldTypes.Boolean:
                        break;
                    case BoundField.FieldTypes.DateTime:
                        DateTime d;
                        if (DateTime.TryParse(searchText, out d))
                        {
                            searchText = d.ToString(Asa.Hrms.Utility.Configuration.DatabaseDateFormat);
                        }
                        else
                        {
                            txtKeyword.Text = "";
                            searchText = "";
                        }
                        break;
                }
            }

            return string.Format("{0} " + clause, fieldName, searchText.Replace("'", "''"));
        }

        private void BuildSearch()
        {
            if (DisplayMode == Mode.Simple)
            {
                WhereClause = this.SimpleSearch(cboOperator.SelectedValue, cboFieldName.SelectedValue, txtKeyword, cboFieldName.SelectedItem.Attributes["FieldType"]);
            }
            else
            {
                Table tblAdvance = (Table)this.FindControl("tblSearchPanel").FindControl("trAdvance").FindControl("tdAdvance").FindControl("tblAdvance");
                string search = "";

                for (int i = 0; i < tblAdvance.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)tblAdvance.Rows[i].FindControl("chkSelect" + i);

                    if (chkSelect.Checked)
                    {
                        DropDownList cboFieldName = (DropDownList)tblAdvance.Rows[i].FindControl("cboFieldName" + i);
                        DropDownList cboOperator = (DropDownList)tblAdvance.Rows[i].FindControl("cboOperator" + i);
                        TextBox txtKeyword = (TextBox)tblAdvance.Rows[i].FindControl("txtKeyword" + i);
                        CheckBox chkOr = (CheckBox)tblAdvance.Rows[i].FindControl("chkOr" + i);

                        search += this.SimpleSearch(cboOperator.SelectedValue, cboFieldName.SelectedValue, txtKeyword, cboFieldName.SelectedItem.Attributes["FieldType"]);

                        if (chkOr != null)
                        {
                            search += chkOr.Checked ? " OR  " : " AND ";
                        }
                        else
                        {
                            search += "     ";
                        }
                    }
                }

                if (search != "")
                {
                    WhereClause = search.Substring(0, search.Length - 4);
                }
                else
                {
                    WhereClause = search;
                }
            }
        }
        #endregion
    }
}