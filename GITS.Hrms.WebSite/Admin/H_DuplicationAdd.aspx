<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_DuplicationAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.Admin.H_DuplicationAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">CC Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="250" 
                 Width="500px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Sort Order:</td>
		<td>
			<asp:TextBox runat="server" ID="txtSortOrder" Text="" MaxLength="250" 
                 Width="500px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSortOrder" runat="server" Display="Dynamic" ControlToValidate="txtSortOrder" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rvSortOrder" runat="server" ControlToValidate="txtSortOrder" Type="Integer" MinimumValue="1" MaximumValue="999" Display="Dynamic" ErrorMessage="*" ToolTip="Number Only"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
	    <td colspan="2">
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/H_DuplicationList.aspx" runat="server">Back</asp:HyperLink>
        </td>
	</tr>
	</table>
</asp:Content>

