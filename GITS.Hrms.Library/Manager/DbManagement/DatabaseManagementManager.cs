using System;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Properties;
using GITS.Hrms.Library.Utility;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Configuration = GITS.Hrms.Library.Utility.Configuration;

namespace GITS.Hrms.Library.Manager.DbManagement
{
    public class DatabaseManagementManager
    {
        protected const string ACCESS_MODE_SINGLE_USER = "SINGLE_USER";
        protected const string ACCESS_MODE_MULTI_USER = "MULTI_USER";
        protected const string ACCESS_MODE_RESTRICTED_USER = "RESTRICTED_USER";

        protected TransactionManager dbDefault = null;

        public delegate void CompletePercentEventHandler(Int32 percent);

        private event CompletePercentEventHandler _CompletePercent;

        public event CompletePercentEventHandler CompletePercent
        {
            add { _CompletePercent += value; }
            remove { _CompletePercent -= value; }
        }

        public DatabaseManagementManager()
        {
            this.dbDefault = new TransactionManager(false, null, null);
            this.CompletePercent += new CompletePercentEventHandler(DatabaseManagementManager_CompletePercent);
        }

        protected void PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            _CompletePercent.Invoke(e.Percent);
        }

        void DatabaseManagementManager_CompletePercent(int percent)
        {
        }

        protected Message SetAccessMode(string databaseName, string accessMode)
        {
            Message msg = new Message();

            try
            {
                string sql = "DECLARE @SPId nvarchar(50) " + Environment.NewLine +
                            "DECLARE SPCursor CURSOR FAST_FORWARD FOR " + Environment.NewLine +
                            "SELECT SPId FROM MASTER..SysProcesses WHERE DBId = DB_ID('" + databaseName + "') AND SPId <> @@SPId " + Environment.NewLine +
                            "OPEN SPCursor " + Environment.NewLine +
                            "FETCH NEXT FROM SPCursor INTO @SPId " + Environment.NewLine +
                            "WHILE @@FETCH_STATUS = 0 " + Environment.NewLine +
                            "BEGIN " + Environment.NewLine +
                            "EXEC ('KILL ' + @SPId) " + Environment.NewLine +
                            "FETCH NEXT FROM SPCursor INTO @SPId " + Environment.NewLine +
                            "END " + Environment.NewLine +
                            "CLOSE SPCursor " + Environment.NewLine +
                            "DEALLOCATE SPCursor" + Environment.NewLine +
                            "ALTER DATABASE [" + databaseName + "] SET " + accessMode + " WITH ROLLBACK IMMEDIATE";

                this.dbDefault.ExecuteUpdate(sql);

                if (accessMode == ACCESS_MODE_MULTI_USER)
                {
                    try
                    {
                        //do something
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                msg.Type = MessageType.Error;
                msg.Msg = ex.Message;
                return msg;
            }

            return msg;
        }

        public bool CheckDatabase()
        {
            return this.dbDefault.GetDataSet("SELECT name FROM sys.databases WHERE name = N'" + Configuration.DatabaseName + "'").Tables[0].Rows.Count > 0;
        }

        public Message ShrinkDatabase(String databaseName)
        {
            Message msg = new Message();
            TransactionManager tm = new TransactionManager(false);
            String shrinkScript = "ALTER DATABASE " + databaseName + " SET RECOVERY SIMPLE;" +
                                "DBCC SHRINKFILE (" + databaseName + ", TRUNCATEONLY);" +
                                "DBCC SHRINKFILE (" + databaseName + "_Log, TRUNCATEONLY);" +
                                "DBCC SHRINKDATABASE (" + databaseName + ", TRUNCATEONLY);" +
                                "ALTER DATABASE " + databaseName + " SET RECOVERY FULL;" +
                                "DBCC SHRINKDATABASE (" + databaseName + ");";

            try
            {
                tm.ExecuteUpdate(shrinkScript);
            }
            catch
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Unable to shrink database.";
                return msg;
            }

            msg.Msg = "Database has been shrunk successfully.";
            return msg;
        }

        public Message Backup(String file)
        {
            Message msg = new Message();

            try
            {
                msg = this.SetAccessMode(Configuration.DatabaseName, ACCESS_MODE_SINGLE_USER);

                if (msg.Type != MessageType.Information)
                {
                    return msg;
                }

                SqlConnection cnn = (SqlConnection)dbDefault.GetType().GetField("_Connection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(dbDefault);
                Server server = new Server(new ServerConnection(cnn));
                String backupPath = server.Settings.BackupDirectory;

                BackupDeviceItem bdi = new BackupDeviceItem(backupPath + "\\" + Path.GetFileName(file), DeviceType.File);
                Backup bu = new Backup();
                bu.Database = Configuration.DatabaseName;
                bu.Devices.Add(bdi);
                bu.Checksum = true;
                bu.Initialize = true;

                bu.PercentComplete += new PercentCompleteEventHandler(PercentComplete);
                bu.SqlBackup(server);

                msg = this.SetAccessMode(Configuration.DatabaseName, ACCESS_MODE_MULTI_USER);

                if (msg.Type != MessageType.Information)
                {
                    return msg;
                }

                File.Move(backupPath + "\\" + Path.GetFileName(file), file);
                File.Delete(backupPath + "\\" + Path.GetFileName(file));
            }
            catch (Exception ex)
            {
                msg = this.SetAccessMode(Configuration.DatabaseName, ACCESS_MODE_MULTI_USER);

                if (msg.Type != MessageType.Information)
                {
                    return msg;
                }

                msg.Type = MessageType.Error;
                msg.Msg = ex.Message;
                return msg;
            }

            return msg;
        }

        public Message Restore(string backupFileName)
        {
            Message msg = new Message();

            try
            {
                msg = this.SetAccessMode(Configuration.DatabaseName, ACCESS_MODE_SINGLE_USER);

                if (msg.Type != MessageType.Information)
                {
                    return msg;
                }

                SqlConnection cnn = (SqlConnection)dbDefault.GetType().GetField("_Connection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(dbDefault);
                Server server = new Server(new ServerConnection(cnn));
                string backupPath = server.Settings.BackupDirectory;
                string temp = backupPath + "\\" + Path.GetFileName(backupFileName);

                if (File.Exists(temp))
                {
                    File.Delete(temp);
                }

                File.Copy(backupFileName, temp);

                BackupDeviceItem bdi = new BackupDeviceItem(temp, DeviceType.File);
                Restore res = new Restore();
                res.Database = Configuration.DatabaseName;
                res.ReplaceDatabase = true;
                res.Checksum = true;
                res.Devices.Add(bdi);
                res.Partial = false;

                res.PercentComplete += new PercentCompleteEventHandler(PercentComplete);
                res.SqlRestore(server);

                msg = this.SetAccessMode(Configuration.DatabaseName, ACCESS_MODE_MULTI_USER);

                if (msg.Type != MessageType.Information)
                {
                    return msg;
                }

                File.Delete(temp);
            }
            catch (Exception ex)
            {
                msg = this.SetAccessMode(Configuration.DatabaseName, ACCESS_MODE_MULTI_USER);

                if (msg.Type != MessageType.Information)
                {
                    return msg;
                }

                msg.Type = MessageType.Error;
                msg.Msg = ex.Message;
                return msg;
            }

            return msg;
        }

        private Message CreateDatabase(bool drop, out bool fresh)
        {
            Message msg = new Message();
            fresh = false;

            if (Configuration.Provider == "SQL Server")
            {
                try
                {
                    //try
                    //{
                    //    this.dbDefault.ExecuteUpdate("EXEC master..sp_dropsrvrolemember @loginame = N'NT AUTHORITY\\SYSTEM', @rolename = N'sysadmin'" + Environment.NewLine +
                    //    "EXEC master..sp_dropsrvrolemember @loginame = N'BUILTIN\\Administrators', @rolename = N'sysadmin'");
                    //}
                    //catch (Exception ex)
                    //{
                    //    msg.Type = MessageType.Error;
                    //    msg.Msg = "Unable to execute script(" + ex.Message + ")";
                    //    return msg;
                    //}

                    bool create = false;

                    if (this.dbDefault.GetDataSet("SELECT name FROM sys.databases WHERE name = N'" + Configuration.DatabaseName + "'").Tables[0].Rows.Count > 0)
                    {
                        if (drop)
                        {
                            msg = this.SetAccessMode(Configuration.DatabaseName, ACCESS_MODE_SINGLE_USER);

                            if (msg.Type != MessageType.Information)
                            {
                                return msg;
                            }

                            if (this.dbDefault.ExecuteUpdate("DROP DATABASE [" + Configuration.DatabaseName + "]") == 0)
                            {
                                msg.Type = MessageType.Error;
                                msg.Msg = "Unable to drop " + Configuration.DatabaseName + " database";
                                return msg;
                            }
                            else
                            {
                                create = true;
                            }
                        }
                    }
                    else
                    {
                        create = true;
                    }

                    if (create)
                    {
                        fresh = true;

                        if (this.dbDefault.ExecuteUpdate("CREATE DATABASE [" + Configuration.DatabaseName + "]") == 0)
                        {
                            msg.Type = MessageType.Error;
                            msg.Msg = "Unable to create" + Configuration.DatabaseName + " database";
                            return msg;
                        }

                        msg = this.SetAccessMode(Configuration.DatabaseName, ACCESS_MODE_MULTI_USER);

                        if (msg.Type != MessageType.Information)
                        {
                            return msg;
                        }

                        if (this.dbDefault.ExecuteUpdate("ALTER DATABASE [" + Configuration.DatabaseName + "] SET RECOVERY FULL") == 0)
                        {
                            msg.Type = MessageType.Error;
                            msg.Msg = "Unable to set recovery mode for " + Configuration.DatabaseName + " database";
                            return msg;
                        }

                        if (this.dbDefault.ExecuteUpdate("ALTER DATABASE [" + Configuration.DatabaseName + "] SET AUTO_SHRINK ON") == 0)
                        {
                            msg.Type = MessageType.Error;
                            msg.Msg = "Unable to set auto shrink on for " + Configuration.DatabaseName + " database";
                            return msg;
                        }
                    }
                }
                catch (Exception ex)
                {
                    msg = this.SetAccessMode(Configuration.DatabaseName, ACCESS_MODE_MULTI_USER);

                    if (msg.Type != MessageType.Information)
                    {
                        return msg;
                    }

                    msg.Type = MessageType.Error;
                    msg.Msg = ex.Message;
                    return msg;
                }
            }
            else
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Not implemented yet";
                return msg;
            }

            return msg;
        }

        public DatabaseConfig LoadDatabaseConfig(String xml, string runat)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            for (int i = 0; i < xmlDoc.ChildNodes[1].ChildNodes.Count;)
            {
                XmlNode n = xmlDoc.ChildNodes[1].ChildNodes[i];
                if (n.Attributes != null && !runat.Contains(n.Attributes["runat"].Value))
                {
                    xmlDoc.ChildNodes[1].RemoveChild(n);
                }
                else
                {
                    i++;
                }
            }

            return LoadDatabaseConfig(xmlDoc.InnerXml);
        }

        public DatabaseConfig LoadDatabaseConfig(String xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            XmlReader schema = null;

            try
            {
                sw.Write(Resources.DatabaseConfigSchema);
                sw.Flush();
                ms.Position = 0;

                schema = XmlReader.Create(ms);

                xmlDoc.InnerXml = xml;
                xmlDoc.Schemas.Add(null, schema);
                xmlDoc.Validate(Schemas_ValidationEventHandler);

                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            DatabaseConfig databaseConfig = new DatabaseConfig();
            XmlNodeList xmlNode = xmlDoc.GetElementsByTagName("config");
            databaseConfig.Location = xmlDoc.DocumentElement.Attributes["location"].Value;
            databaseConfig.Version = xmlDoc.DocumentElement.Attributes["version"].Value;
            int ins = 0;
            int dcs = 0;

            if (xmlDoc.DocumentElement.Attributes["installationSequence"] != null)
            {
                ins = Convert.ToInt32(xmlDoc.DocumentElement.Attributes["installationSequence"].Value);
            }

            if (xmlDoc.DocumentElement.Attributes["databaseConfigurationSequence"] != null)
            {
                dcs = Convert.ToInt32(xmlDoc.DocumentElement.Attributes["databaseConfigurationSequence"].Value);
            }

            for (int i = 0; i < xmlNode.Count; i++)
            {
                if (xmlNode[i].Attributes != null)
                {
                    DatabaseConfiguration dc = new DatabaseConfiguration();
                    dc.Sequence = Convert.ToInt64(xmlNode[i].Attributes["sequence"].Value);
                    dc.Type = xmlNode[i].Attributes["type"].Value;
                    dc.Version = xmlNode[i].Attributes["version"].Value;
                    dc.Script = "";

                    if (xmlNode[i].Attributes["description"] != null)
                    {
                        dc.Description = xmlNode[i].Attributes["description"].Value;
                    }

                    if (xmlNode[i].Attributes["runat"] != null)
                    {
                        dc.Runat = xmlNode[i].Attributes["runat"].Value;
                    }

                    if (xmlNode[i].ChildNodes.Count > 0)
                    {
                        if (xmlNode[i].SelectSingleNode("script[(@provider='" + Configuration.StandardProvider + "' or @provider='" + Configuration.Provider + "')]") != null)
                        {
                            dc.Script = xmlNode[i].SelectSingleNode("script[(@provider='" + Configuration.StandardProvider + "' or @provider='" + Configuration.Provider + "')]").InnerText.Trim();
                        }
                    }

                    if (dc.Sequence == ins)
                    {
                        databaseConfig.Installation = dc;
                    }
                    else if (dc.Sequence == dcs)
                    {
                        databaseConfig.DatabaseConfiguration = dc;
                    }
                    else
                    {
                        databaseConfig.Add(dc);
                    }
                }
            }

            SortingUtility<DatabaseConfiguration>.Sort(databaseConfig, "Sequence");

            return databaseConfig;
        }

        static void Schemas_ValidationEventHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            throw e.Exception;
        }

        protected Message ExecuteConfiguration(DatabaseConfig databaseConfig, int installationId, bool fresh, String database)
        {
            Message msg = new Message();
            double progress = 10;
            double increment = 90.0 / databaseConfig.Count / 2;

            this.dbDefault = new TransactionManager(false, null, database);

            foreach (DatabaseConfiguration dc in databaseConfig)
            {
                DatabaseConfiguration old = DatabaseConfiguration.Get(dbDefault, "Sequence = " + dc.Sequence);

                if (dc.Script.Trim() != "" && ((old == null && (fresh == false || (fresh && dc.Type != DatabaseConfiguration.TYPE_UPGRADE))) || (old != null && dc.Type == DatabaseConfiguration.TYPE_ALWAYS && old.Script.Trim().Equals(dc.Script.Trim()) == false)))
                {
                    try
                    {
                        if (old == null)
                        {
                            dc.InstallationId = installationId;
                            this.dbDefault.ExecuteUpdate(dc.Script);
                            DatabaseConfiguration.Insert(this.dbDefault, dc);
                        }
                        else
                        {
                            old.Script = dc.Script;
                            old.InstallationId = installationId;
                            this.dbDefault.ExecuteUpdate("ALTER " + dc.Script.Trim().Substring(6));
                            DatabaseConfiguration.Update(this.dbDefault, old);
                        }
                    }
                    catch (Exception ex)
                    {
                        msg.Type = MessageType.Error;
                        msg.Msg = "Unable to execute " + dc.Sequence + "(" + ex.Message + ")";

                        this.dbDefault.Commit();

                        return msg;
                    }
                }

                progress += increment;

                _CompletePercent.Invoke(Convert.ToInt32(Math.Round(progress)));
            }

            this.dbDefault.Commit();

            return msg;
        }

        public Message InstallDatabase(bool drop)
        {
            Message msg = new Message();
            bool fresh = false;
            bool ins = false;
            bool dcs = false;
            DatabaseConfig databaseConfig = this.LoadDatabaseConfig(Resources.DatabaseConfig);

            msg = this.CreateDatabase(drop, out fresh);

            if (msg.Type != MessageType.Information)
            {
                return msg;
            }

            _CompletePercent.Invoke(10);

            this.dbDefault = new TransactionManager(false, null, "");

            if (databaseConfig.Installation != null && dbDefault.GetDataSet("SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'Installation') AND OBJECTPROPERTY(id, N'IsUserTable') = 1").Tables[0].Rows.Count == 0)
            {
                dbDefault.ExecuteUpdate(databaseConfig.Installation.Script);
                ins = true;
            }

            if (databaseConfig.DatabaseConfiguration != null && dbDefault.GetDataSet("SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'DatabaseConfiguration') AND OBJECTPROPERTY(id, N'IsUserTable') = 1").Tables[0].Rows.Count == 0)
            {
                dbDefault.ExecuteUpdate(databaseConfig.DatabaseConfiguration.Script);
                dcs = true;
            }

            Installation installation = Installation.GetByTypeAndVersion(fresh ? Installation.TYPE_FRESH : Installation.TYPE_UPGRADE, databaseConfig.Version);

            if (installation == null)
            {
                installation = new Installation();
                installation.Location = databaseConfig.Location;
                installation.Type = fresh ? Installation.TYPE_FRESH : Installation.TYPE_UPGRADE;
                installation.Version = databaseConfig.Version;
                installation.StartDate = DateTime.Now;
                installation.Status = Installation.STATUS_INCOMPLETE;
                Installation.Insert(dbDefault, installation);
            }

            if (ins)
            {
                databaseConfig.Installation.InstallationId = installation.Id;
                DatabaseConfiguration.Insert(dbDefault, databaseConfig.Installation);
            }

            if (dcs)
            {
                databaseConfig.DatabaseConfiguration.InstallationId = installation.Id;
                DatabaseConfiguration.Insert(dbDefault, databaseConfig.DatabaseConfiguration);
            }

            msg = this.ExecuteConfiguration(databaseConfig, installation.Id, fresh, "");

            if (msg.Type != MessageType.Information)
            {
                return msg;
            }

            this.dbDefault = new TransactionManager(false);
            
            installation.EndDate = DateTime.Now;
            installation.Status = Installation.STATUS_COMPLETE;
            Installation.Update(dbDefault, installation);

            _CompletePercent.Invoke(100);
            return msg;
        }

        public Message ExecuteScript(string script)
        {
            Message msg = new Message();
            TransactionManager tm = new TransactionManager(false);
            DatabaseConfig databaseConfig = null;

            try
            {
                 databaseConfig = this.LoadDatabaseConfig(script);
            }
            catch (Exception ex)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Unable to execute script(" + ex.Message + ")";
                return msg;
            }

            Installation installation = Installation.GetByTypeAndVersion(Installation.TYPE_PATCH, databaseConfig.Version);

            if (installation == null)
            {
                installation = new Installation();
                installation.Location = databaseConfig.Location;
                installation.Type = Installation.TYPE_PATCH;
                installation.Version = databaseConfig.Version;
                installation.StartDate = DateTime.Now;
                installation.Status = Installation.STATUS_INCOMPLETE;
                Installation.Insert(tm, installation);
            }

            msg = this.ExecuteConfiguration(databaseConfig, installation.Id, false, "");

            if (msg.Type != MessageType.Information)
            {
                return msg;
            }

            installation.EndDate = DateTime.Now;
            installation.Status = Installation.STATUS_COMPLETE;
            Installation.Update(tm, installation);

            _CompletePercent.Invoke(100);

            return msg;
        }
    }
}
