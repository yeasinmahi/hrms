using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Asa.ExcelXmlWriter;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Web
{
    public abstract class TreePage : BasePage
    {
        protected const string COMMAND_ADD = "ADD";
        protected const string COMMAND_DELETE = "DELETE";
        protected const string COMMAND_EXCEL = "EXCEL";
        protected const string COMMAND_REFRESH = "REFRESH";

        private GridView _GridView;
        private TreeView _TreeView;
        private Type _EntityType;
        private Type _BaseEntityType;

        private String _IdField;
        private String _ParentIdField;

        protected virtual TreeView TreeView
        {
            get { return this._TreeView; }
            set { this._TreeView = value; }
        }

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

        protected virtual String IdField
        {
            get { return this._IdField; }
            set { this._IdField = value; }
        }

        protected virtual String ParentIdField
        {
            get { return this._ParentIdField; }
            set { this._ParentIdField = value; }
        }

        public TreePage()
        {
        }

        protected virtual void LoadData()
        {
            if (this.TreeView != null && this.EntityType != null && this.IdField != null && this.ParentIdField != null)
            {
                this.TreeView.DataSource = this.GetDataSource();
                this.TreeView.DataBind();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IsPostBack == false)
            {
                LoadData();
            }
        }

        protected virtual IHierarchicalDataSource GetDataSource()
        {
            if (this.TreeView != null && this.EntityType != null && this.IdField != null && this.ParentIdField != null)
            {
                this.TransactionManager = new TransactionManager(false);
                DataSet ds = this.TransactionManager.GetDataSet("SELECT * FROM [" + this.EntityType.Name + "]");

                return new HierarchicalDataSet(ds, this.IdField, this.ParentIdField);
            }
            else
            {
                throw new NotImplementedException();
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
            if (this.TreeView != null && this.EntityType != null)
            {
                Message msg = new Message();
                this.TransactionManager = new TransactionManager(true, "Delete [" + this.EntityType.Name + "]");
                msg.Type = MessageType.Information;
                msg.Msg = "Selected item(s) deleted successfully";

                foreach (TreeNode node in this.TreeView.CheckedNodes)
                {
                    Int32 id = Convert.ToInt32(node.Value);
                    msg = this.Delete(this.TransactionManager, id);

                    if (msg.Type != MessageType.Information)
                    {
                        this.TransactionManager.Rollback();
                        return msg;
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

        protected virtual Message Refresh()
        {
            throw new NotImplementedException();
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
                case COMMAND_REFRESH:
                    msg = this.Refresh();
                    this.ShowUIMessage(msg);
                    break;
                default:
                    this.HandleSpecialCommand(sender, e);
                    break;

            }
        }
    }
}