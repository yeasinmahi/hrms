<%@ Page Language="C#" AutoEventWireup="true" Inherits="GITS.Hrms.WebSite._Default" CodeBehind="Default.aspx.cs" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hr Paramatrix</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/mySheet.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/myScript.js"></script>

    <script language="javascript" type="text/javascript">
        function Transfer(url) {
            document.location = url;
        }
        $(document).ready(function () {

        });
    </script>
</head>
<body style="margin: 0px; padding: 0px; height: 100%; width: 100%;">
    <form id="frmLayout" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <header class="container">
                    <div class="pull-left">
                        <img src="images/logo.png" height="80" width="80" />
                    </div>
                    <div class="pull-right">
                        <img src="images/gits.png" height="80" width="160" />
                    </div>
                </header>
                <nav class="navbar navbar-inverse">
                    <div class="container-fluid">
                        <div class="navbar-header">
                            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                                <span class="icon-bar"></span>
                            </button>
                            <a class="navbar-brand" href="#">Dashboard</a>
                        </div>
                        <div class="collapse navbar-collapse" id="myNavbar">
                            <ul class="nav navbar-nav">
                                <li class="active"><a href="#">Hr</a></li>
                                <li><a href="#">Finance</a></li>
                            </ul>
                            <ul class="nav navbar-nav navbar-right">
                                <li class="dropdown">
                                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                        <img src="Images/User.png" height="18" width="18" class="profile-image img-circle">
                                        <label id="lblUserName" style="margin-bottom: 0px" runat="server" text="" />
                                        <b class="caret"></b>
                                    </a>
                                    <ul class="dropdown-menu">
                                        <li>
                                            <a href="ChangePassword.aspx">Change Password</a>
                                        </li>
                                        <li class="divider"></li>
                                        <li>
                                            <asp:LoginStatus ID="LoginStatus1" runat="server" LogoutAction="Redirect" LogoutPageUrl="~/Login.aspx" />
                                        </li>
                                    </ul>
                                </li>

                            </ul>
                        </div>
                    </div>
                </nav>

                <div class="container-fluid text-center">
                    <div class="row content">
                        <div class="sidenav">
                            <div class="collapse navbar-collapse" id="mySidenav">
                                <div class="navController">

                                    <span id="closebtn" class="closebtn" onclick="closeNav()">&times;</span>
                                    <span id="openNav" class="hidden" onclick="openNav()">&#9776;</span>
                                </div>

                                <div id="mainMenu">

                                    <asp:Menu ID="mnuTitleMenubar"
                                        CssClass="navbar"
                                        RenderingMode="List"
                                        IncludeStyleBlock="false"
                                        StaticMenuStyle-CssClass="nav"
                                        StaticSelectedStyle-CssClass="active"
                                        DynamicMenuStyle-CssClass="dropdown-menu"
                                        runat="server"
                                        Orientation="Vertical"
                                        OnMenuItemClick="mnuTitleMenubar_MenuItemClick">
                                    </asp:Menu>
                                </div>
                                <div id="subMenu">
                                    <asp:Menu ID="mnuMenubar"
                                        CssClass="navbar"
                                        RenderingMode="List"
                                        IncludeStyleBlock="false"
                                        StaticMenuStyle-CssClass="nav"
                                        StaticSelectedStyle-CssClass="active"
                                        DynamicMenuStyle-CssClass="dropdown-menu"
                                        runat="server"
                                        Orientation="Vertical"
                                        OnMenuItemClick="mnuMenubar_MenuItemClick">
                                    </asp:Menu>
                                </div>
                            </div>

                        </div>
                        <div id="main" class="text-left mainContent">
                            <div id="mainContentHeader">
                                <asp:Image ID="Image1" Width="24px" Height="24px" Style="vertical-align: middle;"
                                    runat="server" ImageUrl="~/Images/MenuBullet.gif" />
                                <asp:Label ID="lblNavigationTitle" runat="server" Style="vertical-align: middle; white-space: nowrap;"
                                    Text="" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black" Font-Bold="true"></asp:Label>
                                <div id="slideDiv" class="pull-right">
                                    <span id="slideUp" class="glyphicon glyphicon-triangle-top" onclick="slideUp()" aria-hidden="false"></span>
                                    <span id="slideDown" class="glyphicon glyphicon-triangle-bottom" style="display: none" onclick="slideDown()" aria-hidden="true"></span>
                                </div>
                            </div>
                            <iframe id="ifPage" runat="server" frameborder="0" width="100%" height="100%" style="border-left: solid 1px #6893CF;" src="Home.aspx"></iframe>
                        </div>
                    </div>
                    <footer class="container-fluid text-center">
                        <p>
                            <a href="http://gits-bd.com/" target="blank">Global Info-Tech Systems Ltd.</a>
                        </p>
                    </footer>
                    <%-- <table cellpadding="0" cellspacing="0" align="center" style="height: 97.25%; width: 100%;">
                    <tr>
                        $1$<td colspan="2" style="height: 40px; background-image: url('Images/header.png'); background-repeat: no-repeat; background-size: cover;">
                            <table cellpadding="1" cellspacing="1" style="width: 100%;">
                                <tr>
                                    <td align="left" rowspan="3" style="height: 40px; width: 15%; background-image: url('Images/logo.png'); background-repeat: no-repeat; background-size: contain;">
                                        $2$<asp:Label ForeColor="#D6E8FF" Font-Bold="true" ID="Label1" runat="server" Text="Logo" />#2#
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        $2$<asp:Label ForeColor="#D6E8FF" Font-Bold="true" ID="lblTitle1" runat="server" Text="Global Info-Tech Systems" />#2#
                                    </td>
                                    <td align="right">
                                        $2$<asp:Label ForeColor="#D6E8FF" Font-Bold="true" ID="lblUserName" runat="server" Text="" />#2#
                                    </td>
                                </tr>
                                <tr>
                                    $2$<td align="left">
                                        <asp:Label ForeColor="#D6E8FF" Font-Bold="true" ID="lblTitle2" runat="server" Text="Personnel Management Information System" /></td>
                                    <td align="right">
                                        <asp:LoginStatus ForeColor="#D6E8FF" ID="LoginStatus1" runat="server" LogoutAction="Redirect"
                                            LogoutPageUrl="~/Login.aspx" />
                                        <span style="color: #D6E8FF">| </span><a style="color: #D6E8FF;" href="ChangePassword.aspx">Change Password</a>
                                    </td>#2#
                                </tr>
                            </table>
                        </td>#1#
                    </tr>
                    <tr>
                        <td style="width: 15%; height: 100%; vertical-align: top; background-color: #E7ECF6;">
                            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="text-align: left; vertical-align: middle; height: 24px; padding-left: 6px; white-space: nowrap; background-image: url('Images/TBBackground.gif'); border-bottom: solid 1px #6893CF;">
                                        <asp:Image ID="Image1" Width="24px" Height="24px" Style="vertical-align: middle;"
                                            runat="server" ImageUrl="~/Images/MenuBullet.gif" />
                                        <asp:Label ID="lblNavigationTitle" runat="server" Style="vertical-align: middle; white-space: nowrap;"
                                            Text="" Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; background-color: White; vertical-align: top;">
                                        <div runat="server" id="divMenu" style="background-color: White; overflow-y: auto;">
                                            $1$<asp:Menu ID="mnuMenubar" SkinID="MenuBar" Width="100%" runat="server" BackColor="White"
                                                Orientation="Vertical" StaticEnableDefaultPopOutImage="False" OnMenuItemClick="mnuMenubar_MenuItemClick">
                                            </asp:Menu>#1#
                                        </div>
                                    </td>
                                    <tr>
                                        <td style="width: 100%; background-color: White; vertical-align: bottom;">
                                            <div style="border-top: solid 1px #6893CF;">
                                                $1$<asp:Menu ID="mnuTitleMenubar" SkinID="TitleMenuBar" Width="100%" runat="server"
                                                    Orientation="Vertical" StaticEnableDefaultPopOutImage="False" OnMenuItemClick="mnuTitleMenubar_MenuItemClick">
                                                </asp:Menu>#1#
                                            </div>
                                        </td>
                                    </tr>
                            </table>
                        </td>
                        <td style="background-image: url(../Images/AppBackground.jpg); background-size: cover">
                            $1$<iframe id="ifPage" runat="server" frameborder="0" width="100%" height="100%" style="border-left: solid 1px #6893CF;" src="Home.aspx"></iframe>#1#
                        </td>
                    </tr>
                </table>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
