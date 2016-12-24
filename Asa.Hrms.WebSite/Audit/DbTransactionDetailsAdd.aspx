<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="DbTransactionDetailsAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.Audit.DbTransactionDetailsAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Db Transaction Id:</td>
		<td>
			<asp:DropDownList ID="ddlDbTransactionId" runat="server" DataTextField="Id" DataValueField="Id"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Type:</td>
		<td>
			<asp:TextBox runat="server" ID="txtType" Text="" MaxLength="10">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvType" runat="server" Display="Dynamic" ControlToValidate="txtType" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Table Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtTableName" Text="" MaxLength="100">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvTableName" runat="server" Display="Dynamic" ControlToValidate="txtTableName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Identity Column:</td>
		<td>
			<asp:TextBox runat="server" ID="txtIdentityColumn" Text="" MaxLength="100">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvIdentityColumn" runat="server" Display="Dynamic" ControlToValidate="txtIdentityColumn" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Identity Value:</td>
		<td>
			<asp:TextBox runat="server" ID="txtIdentityValue" Text="" MaxLength="100">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvIdentityValue" runat="server" Display="Dynamic" ControlToValidate="txtIdentityValue" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			</td>
	</tr>
	<tr>
		<td align="right">Value:</td>
		<td>
			<asp:TextBox TextMode="MultiLine" runat="server" ID="txtValue" Text="" 
                Height="179px" Width="398px"></asp:TextBox>		</td>
	</tr>
	</table>
</asp:Content>
