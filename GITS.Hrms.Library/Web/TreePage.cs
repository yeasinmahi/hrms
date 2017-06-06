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

        private Type _EntityType;
        private Type _BaseEntityType;

        protected virtual TreeView TreeView { get; set; }

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

        protected virtual String IdField { get; set; }

        protected virtual String ParentIdField { get; set; }

        protected virtual void LoadData()
        {
            if (TreeView != null && EntityType != null && IdField != null && ParentIdField != null)
            {
                TreeView.DataSource = GetDataSource();
                TreeView.DataBind();
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
            if (TreeView != null && EntityType != null && IdField != null && ParentIdField != null)
            {
                TransactionManager = new TransactionManager(false);
                DataSet ds = TransactionManager.GetDataSet("SELECT * FROM [" + EntityType.Name + "]");

                return new HierarchicalDataSet(ds, IdField, ParentIdField);
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
            Message msg = ValidateDelating(id);

            if (msg.Type == MessageType.Information)
            {
                if (BaseEntityType != null)
                {
                    BaseEntityType.BaseType.InvokeMember("Delete", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public, null, null, new Object[] { transactionManager, id });
                }
                else
                {
                    EntityType.BaseType.InvokeMember("Delete", System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public, null, null, new Object[] { transactionManager, id });
                }
            }

            return msg;
        }

        protected virtual Message DeleteSelected()
        {
            if (TreeView != null && EntityType != null)
            {
                Message msg = new Message();
                TransactionManager = new TransactionManager(true, "Delete [" + EntityType.Name + "]");
                msg.Type = MessageType.Information;
                msg.Msg = "Selected item(s) deleted successfully";

                foreach (TreeNode node in TreeView.CheckedNodes)
                {
                    Int32 id = Convert.ToInt32(node.Value);
                    msg = Delete(TransactionManager, id);

                    if (msg.Type != MessageType.Information)
                    {
                        TransactionManager.Rollback();
                        return msg;
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
                case COMMAND_REFRESH:
                    msg = Refresh();
                    ShowUiMessage(msg);
                    break;
                default:
                    HandleSpecialCommand(sender, e);
                    break;

            }
        }
    }
}