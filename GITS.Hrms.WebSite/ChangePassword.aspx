<%@ Page Language="C#" AutoEventWireup="true" Inherits="GITS.Hrms.WebSite.ChangePassword" Codebehind="ChangePassword.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Personnel Management Information System</title>
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
                        <td style="padding-top: 45px; text-align: left;" valign="top">
                            <img alt="" src="Images/LoginlLogo.jpg" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top: 45px;">
                            <asp:ChangePassword ID="ChangePassword1" runat="server" 
                                MembershipProvider="MmsMembershipProvider" LabelStyle-Wrap="False" 
                                CancelDestinationPageUrl="~/Default.aspx" 
                                ContinueDestinationPageUrl="~/Default.aspx" ChangePasswordButtonText="Change" 
                                ChangePasswordFailureText="Old Password incorrect or New Password invalid." 
                                PasswordLabelText="Old Password:">
                                <TextBoxStyle CssClass="TextBox" Width="150px" />
                                <LabelStyle CssClass="Label" />
                                <TitleTextStyle CssClass="Label" />
                            </asp:ChangePassword>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: left">
                <img alt="" src="Images/LoginRight.jpg" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
