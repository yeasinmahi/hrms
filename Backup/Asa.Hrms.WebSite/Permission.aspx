<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="Permission" Title="Personnel Management Information System" Codebehind="Permission.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="background-color: #ADC3EF; width: 100%; height: 100%; font-family: Tahoma;
        font-size: 12pt;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="vertical-align: middle; text-align: center;">
                <table cellpadding="10" cellspacing="0">
                    <tr style="background-color: #6B79A5">
                        <td colspan="2" style="vertical-align: middle; text-align: left; color: #FFFFFF;">
                            <asp:Label ID="lblErrorHeading" Font-Bold="True" runat="server" Text="Permission Error"></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #7A96DF;">
                        <td style="vertical-align: middle; text-align: right;">
                            <asp:Image ID="imgError" runat="server" ImageUrl="~/Images/Error.gif" />
                        </td>
                        <td>
                            <asp:Label ID="lblErrorMsg" runat="server" Font-Bold="True" Text="You do not have permission to perform this operation.<br>Please contact System Administrator for details"></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color: #7A96DF;">
                        <td colspan="2">
                            <input type="button" value="Back" onclick="javascript:history.back();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
