<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DbInstall.aspx.cs" Inherits="GITS.Hrms.WebSite.DbManagement.DbInstall" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personnel Management Information System</title>

    <script language="javascript" type="text/javascript" src="/Script/Global.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td colspan="3" valign="top" width="5%" bgcolor="#003366">
                            <img alt="" border="0" height="50" src="../Images/LoginlLogo.jpg" width="60" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Menu ID="mnuPageToolbar" runat="server" SkinID="ToolBar" OnMenuItemClick="mnuPageToolbar_MenuItemClick">
                                <Items>
                                    <asp:MenuItem Text="Upgrade Database" Value="UPGRADE"></asp:MenuItem>
                                    <asp:MenuItem Text="Backup Database" Value="BACKUP"></asp:MenuItem>
                                    <asp:MenuItem Text="Restore Database" Value="RESTORE"></asp:MenuItem>
                                    <asp:MenuItem Text="Execute Script" Value="EXECUTE"></asp:MenuItem>
                                    <asp:MenuItem Text="Shrink Database" Value="SHRINK"></asp:MenuItem>
                                </Items>
                            </asp:Menu>
                        </td>
                        <td align="right" style="font-weight: bold;">
                            [<a href="../Login.aspx">Login</a>]
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="3" cellspacing="1" runat="server" id="tblUpgrade"
                    visible="true">
                    <tr>
                        <td>
                            <h3>
                                Upgrade database</h3>
                            <p>
                                This will upgrade the database to the latest version.</p>
                            <span style="color: #900000; font-stretch: wider;">Note</span>: This process is
                            complex and can take a few minutes. Please be patient.
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:CheckBox ID="cbReInstall" runat="server" Text="Re-Install database" AutoPostBack="true"
                                OnCheckedChanged="cbReInstall_CheckedChanged" />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: #FF0000; font-size: smaller"><strong>WARNING</strong></span>:
                            <span style="font-size: smaller">This will wipe all existing content and system will
                                be in initial state - make sure<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;you backup first!</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnUpgrade" runat="server" Text="Upgrade" OnClick="btnUpgrade_Click" />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="3" cellspacing="1" runat="server" id="tblBackup" visible="false">
                    <tr>
                        <td>
                            <h3>
                                Backup data</h3>
                            <p>
                                This will back up the contents of the database in a portable file.</p>
                            <p>
                                You can use this backup to move data between different databases if required, as<br />
                                well as creating a backup that you can use if something goes wrong.</p>
                            <p>
                                <span style="color: #900000; font-stretch: wider;">Note</span>: The backup process
                                is complex and can take a few minutes. Please be patient.
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnBackup" runat="server" Text="Backup" OnClick="btnBackup_Click" />
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="3" cellspacing="1" runat="server" id="tblRestore"
                    visible="false">
                    <tr>
                        <td colspan="3">
                            <h3>
                                Restore data from backup</h3>
                            <p>
                                To restore from a file, enter the filename below.</p>
                            <p>
                                <span style="color: #FF0000; font-stretch: wider;"><strong>WARNING</strong></span>:
                                This will wipe all existing content - make sure you backup first!
                            </p>
                            <span style="color: #900000; font-stretch: wider;">Note</span>: Make sure you know
                            your login details in the data being restored.
                            <br />
                            <span style="color: #900000; font-stretch: wider;">Note</span>: The restore process
                            is complex and can take a few minutes. Please be patient.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td align="right">
                                        File Name:
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="fuFileName" runat="server" Width="400px"></asp:FileUpload><asp:RequiredFieldValidator
                                            ID="rfvFileName" runat="server" Display="Dynamic" ControlToValidate="fuFileName"
                                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnRestore" runat="server" Text="Restore" OnClick="btnRestore_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="3" cellspacing="1" runat="server" id="tblExecute"
                    visible="false">
                    <tr>
                        <td colspan="3">
                            <h3>
                                Execute database script</h3>
                            <p>
                                To execute a database script, enter the filename below.</p>
                            <p>
                                <span style="color: #FF0000; font-stretch: wider;"><strong>WARNING</strong></span>:
                                This may wipe some existing content - make sure you backup first!
                            </p>
                            <span style="color: #900000; font-stretch: wider;">Note</span>: The execute process
                            is complex and can take a few minutes. Please be patient.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table>
                                <tr>
                                    <td align="right" style="width: 10%">
                                        File Name:
                                    </td>
                                    <td style="width: 90%">
                                        <asp:FileUpload ID="fuScriptFileName" runat="server" Width="400px"></asp:FileUpload><asp:RequiredFieldValidator
                                            ID="rfvScriptFileName" runat="server" Display="Dynamic" ControlToValidate="fuScriptFileName"
                                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnExecute" runat="server" Text="Execute" OnClick="btnExecute_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="3" cellspacing="1" runat="server" id="tblShrink" visible="false">
                    <tr>
                        <td colspan="2">
                            <h3>
                                Shrink database</h3>
                            <p>
                                This will shrink database to minimum possible size..</p>
                            <p>
                                <span style="color: #FF0000; font-stretch: wider;"><strong>WARNING</strong></span>:
                                This may wipe some existing content - make sure you backup first!
                            </p>
                            <span style="color: #900000; font-stretch: wider;">Note</span>: The shrink process
                            is complex and can take a few minutes. Please be patient.
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnShrink" runat="server" Text="Shrink" 
                                onclick="btnShrink_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlMessage" runat="server" CssClass="LabelEmptyMessage">
                    <table style="vertical-align: top;">
                        <tr>
                            <td>
                                <asp:Image ID="imgMessage" runat="server" Height="24px" Width="24px" />
                            </td>
                            <td style="white-space: normal;">
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExecute" />
                <asp:PostBackTrigger ControlID="btnRestore" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
            <ProgressTemplate>
                Update in Progress……..
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div>
        <span>Powered by <a href="http://www.asa.org.bd" target="new">ASA</a> the Professional
            Human Resource Management Software. <span style="color: #666666">(Enterprise Edition,
                Version: 1.0.0)</span></span>
    </div>
    </form>
</body>
</html>
