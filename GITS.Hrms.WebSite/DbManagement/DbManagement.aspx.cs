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
            databaseManagementManager = new DatabaseManagementManager();

            switch (PropertyName)
            {
                case "UPGRADE DATABASE":
                    tblUpgrade.Visible = true;
                    tblBackup.Visible = false;
                    tblRestore.Visible = false;
                    tblExecute.Visible = false;
                    tblShrink.Visible = false;
                    break;
                case "BACKUP DATABASE":
                    tblUpgrade.Visible = false;
                    tblBackup.Visible = true;
                    tblRestore.Visible = false;
                    tblExecute.Visible = false;
                    tblShrink.Visible = false;
                    break;
                case "RESTORE DATABASE":
                    tblUpgrade.Visible = false;
                    tblBackup.Visible = false;
                    tblRestore.Visible = true;
                    tblExecute.Visible = false;
                    tblShrink.Visible = false;
                    break;
                case "EXECUTE SCRIPT":
                    tblUpgrade.Visible = false;
                    tblBackup.Visible = false;
                    tblRestore.Visible = false;
                    tblExecute.Visible = true;
                    tblShrink.Visible = false;
                    break;
                case "SHRINK DATABASE":
                    tblUpgrade.Visible = false;
                    tblBackup.Visible = false;
                    tblRestore.Visible = false;
                    tblExecute.Visible = false;
                    tblShrink.Visible = true;
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
            ShowUiMessage(msg);

            switch (e.Item.Value)
            {
                case "UPGRADE":
                    Upgrade();
                    break;
                case "BACKUP":
                    Backup();
                    break;
                case "RESTORE":
                    Restore();
                    break;
                case "EXECUTE":
                    Execute();
                    break;
                case "SHRINK":
                    Shrink();
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
            Message msg = databaseManagementManager.InstallDatabase(false);

            if (msg.Type == MessageType.Information)
            {
                msg.Msg = "Database upgraded successfully";
            }

            ShowUiMessage(msg);
        }

        private void Backup()
        {
            Message msg = new Message();

            String file = Path.GetTempPath() + DateTime.Now.ToString("yyyyMMddHHmmss_") + Configuration.DatabaseName + "_" + Configuration.Version;
        
            msg = databaseManagementManager.Backup(file);

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

            ShowUiMessage(msg);
        }

        private void Restore()
        {
            Message msg = new Message();

            if (fuFileName.HasFile == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid file name";
            }
            else
            {
                String file = Path.GetTempFileName();

                fuFileName.SaveAs(file);

                msg = databaseManagementManager.Restore(file);

                File.Delete(file);
            }

            if (msg.Type == MessageType.Information)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                ShowUiMessage(msg);
            }
        }

        private void Execute()
        {
            Message msg = new Message();

            if (fuScriptFileName.HasFile == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid file name";
            }
            else
            {
                String script = "";
                String file = Path.GetTempFileName();

                fuScriptFileName.SaveAs(file);

                try
                {
                    script = Cryptography.Decrypt(File.Open(file, FileMode.Open));

                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    msg.Msg = ex.Message;
                    msg.Type = MessageType.Error;
                    ShowUiMessage(msg);
                    return;
                }

                msg = databaseManagementManager.ExecuteScript(script);
            }

            if (msg.Type == MessageType.Information)
            {
                msg.Msg = "Database script executed successfully";
            }

            ShowUiMessage(msg);
        }

        private void Shrink()
        {
            Message msg = databaseManagementManager.ShrinkDatabase(Configuration.DatabaseName);

            ShowUiMessage(msg);
        }
    }
}