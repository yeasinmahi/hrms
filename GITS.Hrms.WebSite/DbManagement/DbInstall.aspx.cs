using System;
using System.IO;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Manager.DbManagement;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.WebSite.DbManagement
{
    public partial class DbInstall : System.Web.UI.Page
    {
        DatabaseManagementManager databaseManagementManager = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            databaseManagementManager = new DatabaseManagementManager();
            //if (Request.UserHostAddress != "127.0.0.1")
            //{
            //    Utility.UIUtility.Transfer(Page, "../Permission.aspx");
            //}
            //else
            //{
            //    this.databaseManagementManager = new DatabaseManagementManager();
            //}
        }

        public void ShowUIMessage(Message msg)
        {
            switch (msg.Type)
            {
                case MessageType.Error:
                    lblMessage.Text = "Error: " + msg.Msg;
                    imgMessage.ImageUrl = "~/Images/error.gif";
                    pnlMessage.CssClass = "LabelErrorMessage";
                    break;
                case MessageType.Warning:
                    lblMessage.Text = "Warning: " + msg.Msg;
                    imgMessage.ImageUrl = "~/Images/warning.gif";
                    pnlMessage.CssClass = "LabelWarningMessage";
                    break;
                case MessageType.Information:
                    lblMessage.Text = msg.Msg;

                    if (msg.Msg == null || msg.Msg == "")
                    {
                        imgMessage.ImageUrl = "";
                        pnlMessage.CssClass = "LabelEmptyMessage";
                    }
                    else
                    {
                        imgMessage.ImageUrl = "~/Images/info.gif";
                        pnlMessage.CssClass = "LabelInformationMessage";
                    }
                    break;
            }
        }

        protected void mnuPageToolbar_MenuItemClick(object sender, MenuEventArgs e)
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            this.ShowUIMessage(msg);

            switch (e.Item.Value)
            {
                case "UPGRADE":
                    this.tblUpgrade.Visible = true;
                    this.tblBackup.Visible = false;
                    this.tblRestore.Visible = false;
                    this.tblExecute.Visible = false;
                    this.tblShrink.Visible = false;
                    break;
                case "BACKUP":
                    this.tblUpgrade.Visible = false;
                    this.tblBackup.Visible = true;
                    this.tblRestore.Visible = false;
                    this.tblExecute.Visible = false;
                    this.tblShrink.Visible = false;
                    break;
                case "RESTORE":
                    this.tblUpgrade.Visible = false;
                    this.tblBackup.Visible = false;
                    this.tblRestore.Visible = true;
                    this.tblExecute.Visible = false;
                    this.tblShrink.Visible = false;
                    break;
                case "EXECUTE":
                    this.tblUpgrade.Visible = false;
                    this.tblBackup.Visible = false;
                    this.tblRestore.Visible = false;
                    this.tblExecute.Visible = true;
                    this.tblShrink.Visible = false;
                    break;
                case "SHRINK":
                    this.tblUpgrade.Visible = false;
                    this.tblBackup.Visible = false;
                    this.tblRestore.Visible = false;
                    this.tblExecute.Visible = false;
                    this.tblShrink.Visible = true;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected void btnUpgrade_Click(object sender, EventArgs e)
        {
            Message msg = new Message();
            this.ShowUIMessage(msg);

            msg = this.databaseManagementManager.InstallDatabase(this.cbReInstall.Checked);

            if (msg.Type == MessageType.Information)
            {
                msg.Type = MessageType.Information;

                if (this.cbReInstall.Checked)
                {
                    msg.Msg = "Database re-installed successfully";
                }
                else
                {
                    msg.Msg = "Database upgraded successfully";
                }
            }

            ShowUIMessage(msg);
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            Message msg = new Message();
            ShowUIMessage(msg);

            String file = Path.GetTempPath() + DateTime.Now.ToString("yyyyMMddHHmmss_") + Configuration.DatabaseName + "_" + Configuration.Version;

            msg = this.databaseManagementManager.Backup(file);

            if (msg.Type == MessageType.Information)
            {
                //FileInfo fileInfo = new FileInfo(file);
                //Response.Clear();
                //Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileInfo.Name));
                //Response.ContentType = "application/unknown";
                //Response.AddHeader("Content-Length", DBUtility.ToString(fileInfo.Length));
                //Response.TransmitFile(file);
                //Response.Flush();
                //Response.Clear();

                UIUtility.Transfer(Page, "../Export.aspx?path=" + file.Replace("\\", "\\\\"));

                //msg.Msg = "Backup completed successfully";
                //File.Delete(file);
            }
            else
            {
                ShowUIMessage(msg);
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            Message msg = new Message();
            this.ShowUIMessage(msg);

            if (this.fuFileName.HasFile == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid file name";
            }
            else
            {
                String file = Path.GetTempFileName();

                this.fuFileName.SaveAs(file);

                msg = this.databaseManagementManager.Restore(file);

                File.Delete(file);
            }

            ShowUIMessage(msg);
        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            Message msg = new Message();
            this.ShowUIMessage(msg);

            if (this.fuScriptFileName.HasFile == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid file name";
            }
            else
            {
                String script = "";
                String file = Path.GetTempFileName();

                this.fuScriptFileName.SaveAs(file);

                try
                {
                    script = Cryptography.Decrypt(File.Open(file, FileMode.Open));

                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    msg.Msg = ex.Message;
                    msg.Type = MessageType.Error;
                    ShowUIMessage(msg);
                    return;
                }

                msg = this.databaseManagementManager.ExecuteScript(script);
            }

            if (msg.Type == MessageType.Information)
            {
                msg.Msg = "Database script executed successfully";
            }

            ShowUIMessage(msg);
        }

        protected void cbReInstall_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbReInstall.Checked)
            {
                this.btnUpgrade.Text = "Re-Install";
            }
            else
            {
                this.btnUpgrade.Text = "Upgrade";
            }
        }

        protected void btnShrink_Click(object sender, EventArgs e)
        {
            Message msg = this.databaseManagementManager.ShrinkDatabase(Configuration.DatabaseName);

            ShowUIMessage(msg);
        }
    }
}
