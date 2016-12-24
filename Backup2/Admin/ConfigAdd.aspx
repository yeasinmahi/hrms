<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="ConfigAdd.aspx.cs" Inherits="ConfigAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="100" ReadOnly="true">
			</asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Data Type:</td>
		<td>
			<asp:TextBox runat="server" ID="txtReadableDataType" Text="" MaxLength="100" ReadOnly="true">
			</asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Value:</td>
		<td>
			<asp:TextBox runat="server" ID="txtValue" Text="" MaxLength="100">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvValue" runat="server" Display="Dynamic" ControlToValidate="txtValue" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			<asp:CustomValidator runat="server" ID="cvValue" ControlToValidate="txtValue" 
                onservervalidate="cvValue_ServerValidate" Display="Dynamic" ErrorMessage="Invalid data"></asp:CustomValidator>
		</td>
	</tr>
	<tr>
	    <td colspan="2">
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/ConfigList.aspx" runat="server">Back</asp:HyperLink>
        </td>
	</tr>
	</table>
</asp:Content>
