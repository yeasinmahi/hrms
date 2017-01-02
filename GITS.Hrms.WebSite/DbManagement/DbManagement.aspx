<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="DbManagement.aspx.cs"
    Inherits="GITS.Hrms.WebSite.DbManagement.DbManagement" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <table border="0" cellpadding="3" cellspacing="1" runat="server" id="tblUpgrade" visible="true">
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
                </table>
            </td>
        </tr>
        <tr>
            <td>
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
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="3" cellspacing="1" runat="server" id="tblRestore"
                    visible="false">
                    <tr>
                        <td colspan="2">
                            <h3>
                                Restore data from backup</h3>
                            <p>
                                To restore from a file, enter the filename below.</p>
                            <p>
                                <span style="color: #FF0000; font-stretch: wider;"><strong>WARNING</strong></span>:
                                This will wipe all existing content - make sure you backup first!
                            </p>
                            <span style="color: #900000; font-stretch: wider;">Note</span>: You will be logged
                            out after the restore process. Make sure you know your login details in the data
                            being restored.
                            <br />
                            <span style="color: #900000; font-stretch: wider;">Note</span>: The restore process
                            is complex and can take a few minutes. Please be patient.
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 10%">
                            File Name:
                        </td>
                        <td style="width: 90%">
                            <asp:FileUpload ID="fuFileName" runat="server" Width="400px">
                            </asp:FileUpload><asp:RequiredFieldValidator
                                ID="rfvFileName" runat="server" Display="Dynamic" ControlToValidate="fuFileName"
                                ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="3" cellspacing="1" runat="server" id="tblExecute"
                    visible="false">
                    <tr>
                        <td colspan="2">
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
                        <td align="right" style="width: 10%">
                            File Name:
                        </td>
                        <td style="width: 90%">
                            <asp:FileUpload ID="fuScriptFileName" runat="server" Width="400px"></asp:FileUpload><asp:RequiredFieldValidator
                                ID="rfvScriptFileName" runat="server" Display="Dynamic" ControlToValidate="fuScriptFileName"
                                ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="3" cellspacing="1" runat="server" id="tblShrink"
                    visible="false">
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
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            Update in Progress……..
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
