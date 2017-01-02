<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="OrganizationAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.Admin.OrganizationAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <table border="0" cellpadding="3" cellspacing="1">
        <tr>
            <td align="right">
                Name:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtName" Text="" MaxLength="50" Width="250px">
			</asp:TextBox><asp:RequiredFieldValidator
                    ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*"
                    ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/OrganizationList.aspx" runat="server">Back</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
