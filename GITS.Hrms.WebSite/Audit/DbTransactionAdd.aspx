<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="DbTransactionAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.Audit.DbTransactionAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Description:</td>
		<td>
			<asp:TextBox runat="server" ID="txtDescription" Text="" MaxLength="100">
			</asp:TextBox>		</td>
	</tr>
	<tr>
		<td align="right">Created By:</td>
		<td>
			<asp:DropDownList ID="ddlCreatedBy" runat="server" DataTextField="Login" DataValueField="Login"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Created Date:</td>
		<td>
			<asp:TextBox runat="server" ID="txtCreatedDate" Text="" MaxLength="10">
			</asp:TextBox><asp:ImageButton ID="ibCreatedDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtCreatedDate'));return false;">
			</asp:ImageButton><asp:RequiredFieldValidator ID="rfvCreatedDate" runat="server" Display="Dynamic" ControlToValidate="txtCreatedDate" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator><asp:RangeValidator ControlToValidate="txtCreatedDate" ID="rvCreatedDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
	    <td colspan="2">
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Audit/DbTransactionList.aspx" runat="server">Back</asp:HyperLink>
        </td>
	</tr>
	</table>
</asp:Content>
