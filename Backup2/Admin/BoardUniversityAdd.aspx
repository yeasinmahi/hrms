<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="BoardUniversityAdd.aspx.cs" Inherits="BoardUniversityAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="100">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	</table>
</asp:Content>
