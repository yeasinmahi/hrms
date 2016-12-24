<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_DepartmentAdd.aspx.cs" Inherits="H_DepartmentAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="50" Width="250px">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Sort Order:</td>
		<td>
			<asp:TextBox runat="server" ID="txtSortOrder" Text="" MaxLength="10" Width="250px">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvSortOrder" runat="server" Display="Dynamic" ControlToValidate="txtSortOrder" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator><asp:RangeValidator ControlToValidate="txtSortOrder" ID="rvSortOrder" Type="Integer" MaximumValue="2147483647" MinimumValue="0" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
	    <td colspan="2">
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/H_DepartmentList.aspx" runat="server">Back</asp:HyperLink>
        </td>
	</tr>
	</table>
</asp:Content>
