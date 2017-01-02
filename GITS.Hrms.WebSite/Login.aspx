<%@ Page Language="C#" AutoEventWireup="true" Inherits="GITS.Hrms.WebSite.Login" CodeBehind="Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GITS Personnel Management Information System</title>
</head>
<body style="vertical-align: middle; text-align: center;">
    <form id="form1" runat="server">
    <table border="0" align="center" cellpadding="0" cellspacing="0">
        <tr valign="top">
            <td>
                <img alt="" src="Images/LoginLeft.jpg" />
            </td>
            <td style="background-image: url('Images/LoginMiddle.jpg')">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="padding-top: 45px;" align="center" valign="top">
                            <img style="width: 120px; height: 60px" alt="" src="Images/logo.png" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 15px; text-align: center; font-size: x-large;">
                            Global Info-Tech Systems
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-size: large;">
                            Personnel Management Information System
                        </td>
                    </tr>
                    <tr style="height: 10px;">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Login ID="login" LoginButtonImageUrl="~/Images/LoginButton.gif" runat="server"
                                DestinationPageUrl="~/Default.aspx" DisplayRememberMe="False" LoginButtonType="Image"
                                UserNameLabelText="Login ID:" UserNameRequiredErrorMessage="Login ID is required."
                                MembershipProvider="MmsMembershipProvider" TitleText="GITS" OnLoggingIn="login_LoggingIn"
                                EnableTheming="True">
                                <TextBoxStyle CssClass="TextBox" Width="150px" />
                                <LabelStyle HorizontalAlign="Left" />
                                <LayoutTemplate>
                                    <table border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;">
                                        <tr>
                                            <td align="right">
                                                <table border="0" cellpadding="0">
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Login ID:</asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="UserName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                                ErrorMessage="Login ID is required." ToolTip="Login ID is required." ValidationGroup="login">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="Password" runat="server" CssClass="TextBox" TextMode="Password"
                                                                Width="150px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="login">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="2" style="color: Red;">
                                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                    <tr style="height: 20px">
                                                        <td align="left" runat="server" id="label" colspan="2" style="color: Red;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:ImageButton ID="LoginImageButton" runat="server" AlternateText="Log In" CommandName="Login"
                                                                ImageUrl="~/Images/LoginButton.gif" ValidationGroup="login" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                            </asp:Login>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: right;">
                <img alt="" src="Images/LoginRight.jpg" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
