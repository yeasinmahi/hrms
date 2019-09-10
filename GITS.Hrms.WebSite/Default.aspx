<%@ Page Language="C#" AutoEventWireup="true" Inherits="GITS.Hrms.WebSite._Default" CodeBehind="Default.aspx.cs" maintainScrollPositionOnPostBack="true"%>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hr Paramatrix</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/mySheet.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    
    <script src="Scripts/myScript.js" type="text/javascript"></script>

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
                            <div class="" id="mySidenav">
                                <div class="navController">

                                    <span id="closebtn" class="closebtn" onclick="closeNav()">&times;</span>
                                    <span id="openNav" class="hidden" onclick="openNav()">&#9776;</span>
                                </div>

                                <div id="mainMenu">

                                    <asp:Menu ID="mnuTitleMenubar"
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
                            <iframe id="ifPage" runat="server" frameborder="0" width="100%" height="95%" style="border-left: solid 1px #6893CF;" src="Home.aspx"></iframe>
                        </div>
                    </div>

                </div>
                <footer class="text-center">
                    <p>
                        <a href="http://gits-bd.com/" target="blank">Global Info-Tech Systems Ltd.</a>
                    </p>
                </footer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
