using System;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Manager.DbManagement;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;
using Configuration = GITS.Hrms.Library.Utility.Configuration;

namespace GITS.Hrms.WebSite.DbManagement
{
    public partial class DbManagement : AddPage
    {
        DatabaseManagementManager databaseManagementManager = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.databaseManagementManager = new DatabaseManagementManager();

            switch (this.PropertyName)
            {
                case "UPGRADE DATABASE":
                    this.tblUpgrade.Visible = true;
                    this.tblBackup.Visible = false;
                    this.tblRestore.Visible = false;
                    this.tblExecute.Visible = false;
                    this.tblShrink.Visible = false;
                    break;
                case "BACKUP DATABASE":
                    this.tblUpgrade.Visible = false;
                    this.tblBackup.Visible = true;
                    this.tblRestore.Visible = false;
                    this.tblExecute.Visible = false;
                    this.tblShrink.Visible = false;
                    break;
                case "RESTORE DATABASE":
                    this.tblUpgrade.Visible = false;
                    this.tblBackup.Visible = false;
                    this.tblRestore.Visible = true;
                    this.tblExecute.Visible = false;
                    this.tblShrink.Visible = false;
                    break;
                case "EXECUTE SCRIPT":
                    this.tblUpgrade.Visible = false;
                    this.tblBackup.Visible = false;
                    this.tblRestore.Visible = false;
                    this.tblExecute.Visible = true;
                    this.tblShrink.Visible = false;
                    break;
                case "SHRINK DATABASE":
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

        protected override void LoadData()
        {
        }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            ShowUIMessage(msg);

            switch (e.Item.Value)
            {
                case "UPGRADE":
                    this.Upgrade();
                    break;
                case "BACKUP":
                    this.Backup();
                    break;
                case "RESTORE":
                    this.Restore();
                    break;
                case "EXECUTE":
                    this.Execute();
                    break;
                case "SHRINK":
                    this.Shrink();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    
        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        protected override Message Save()
        {
            throw new NotImplementedException();
        }

        private void Upgrade()
        {
            Message msg = this.databaseManagementManager.InstallDatabase(false);

            if (msg.Type == MessageType.Information)
            {
                msg.Msg = "Database upgraded successfully";
            }

            ShowUIMessage(msg);
        }

        private void Backup()
        {
            Message msg = new Message();

            String file = Path.GetTempPath() + DateTime.Now.ToString("yyyyMMddHHmmss_") + Configuration.DatabaseName + "_" + Configuration.Version;
        
            msg = this.databaseManagementManager.Backup(file);

            if (msg.Type == MessageType.Information)
            {
                FileInfo fileInfo = new FileInfo(file);
                Response.Clear();
                Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileInfo.Name));
                Response.ContentType = "application/unknown";
                Response.AddHeader("Content-Length", DBUtility.ToString(fileInfo.Length));
                Response.TransmitFile(file);
                Response.Flush();
                Response.Clear();

                msg.Msg = "Backup completed successfully";

                File.Delete(file);
            }

            ShowUIMessage(msg);
        }

        private void Restore()
        {
            Message msg = new Message();

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

            if (msg.Type == MessageType.Information)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                ShowUIMessage(msg);
            }
        }

        private void Execute()
        {
            Message msg = new Message();

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

        private void Shrink()
        {
            Message msg = this.databaseManagementManager.ShrinkDatabase(Configuration.DatabaseName);

            ShowUIMessage(msg);
        }
    }
}