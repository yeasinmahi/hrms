<%@ Page Language="C#" AutoEventWireup="true" Inherits="GITS.Hrms.WebSite._Default" CodeBehind="Default.aspx.cs" %>

<script language="javascript" type="text/javascript">
    function Transfer(url)
    {
        document.location = url;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Personnel Management Information System</title>
</head>
<body style="margin: 0px; padding: 0px; height: 100%; width: 100%;">
    <form id="frmLayout" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" align="center" style="height: 97.25%; width: 100%;">
                <tr>
                    <td colspan="2" style="height: 40px; background-image: url('Images/header.jpg');">
                        <table cellpadding="1" cellspacing="1" style="width: 100%;">
                            <tr>
                            <td align="left"><asp:Label ForeColor="#D6E8FF" Font-Bold="true" ID="lblTitle1" runat="server" Text="Global Info-Tech Systems" /></td>
                                <td align="right">
                                    <asp:Label ForeColor="#D6E8FF" Font-Bold="true" ID="lblUserName" runat="server" Text="" />
                                </td>
                            </tr>
                            <tr>
                            <td align="left"> <asp:Label ForeColor="#D6E8FF" Font-Bold="true" ID="lblTitle2" runat="server" Text="Personnel Management Information System" /></td>
                                <td align="right">
                                    <asp:LoginStatus ForeColor="#D6E8FF" ID="LoginStatus1" runat="server" LogoutAction="Redirect"
                                        LogoutPageUrl="~/Login.aspx" /><span style="color:#D6E8FF"> | </span> <a style="color: #D6E8FF;" href="ChangePassword.aspx">Change Password</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; height: 100%; vertical-align: top; background-color: #E7ECF6;">
                        <table cellpadding="0" cellspacing="0" style="width: 100%; height:100%">
                            <tr>
                                <td style="text-align: left; vertical-align: middle; height: 24px; padding-left: 6px;white-space:nowrap; background-image: url('Images/TBBackground.gif'); border-bottom: solid 1px #6893CF;">
                                    <asp:Image ID="Image1" Width="24px" Height="24px" Style="vertical-align: middle;"
                                        runat="server" ImageUrl="~/Images/MenuBullet.gif" />
                                    <asp:Label ID="lblNavigationTitle" runat="server" Style="vertical-align: middle; white-space:nowrap;"
                                        Text="" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%; background-color: White; vertical-align:top;">
                                    <div runat="server" id="divMenu" style="background-color: White; overflow-y: auto;">
                                        <asp:Menu ID="mnuMenubar" SkinID="MenuBar" Width="100%" runat="server" BackColor="White"
                                            Orientation="Vertical" StaticEnableDefaultPopOutImage="False" OnMenuItemClick="mnuMenubar_MenuItemClick">
                                        </asp:Menu>
                                    </div>
                                 </td>
                            <tr>
                                <td style="width: 100%; background-color:White; vertical-align:bottom;">
                                    <div style="border-top: solid 1px #6893CF;">
                                        <asp:Menu ID="mnuTitleMenubar" SkinID="TitleMenuBar" Width="100%" runat="server"
                                            Orientation="Vertical" StaticEnableDefaultPopOutImage="False" OnMenuItemClick="mnuTitleMenubar_MenuItemClick">
                                        </asp:Menu>
                                    </div>
                                 </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <iframe id="ifPage" runat="server" frameborder="0" width="100%" height="100%" style="border-left: solid 1px #6893CF;" src="Home.aspx"></iframe>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
