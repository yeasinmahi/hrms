<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="SubzoneAdd.aspx.cs" Inherits="SubzoneAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Zone:</td>
		<td>
			<asp:DropDownList ID="ddlZoneId" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="50">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Name in Bangla:</td>
		<td>
			<asp:TextBox ID="txtNameInBangla" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNameinBangla" runat="server" 
                ControlToValidate="txtNameInBangla" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Status</td>
		<td>
			<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlStatus_SelectedIndexChanged">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">
            <asp:Label ID="lblOpenClose" runat="server" Text="Opening Date:"></asp:Label>
        </td>
		<td>
			<asp:TextBox ID="txtOpeningDate" runat="server"></asp:TextBox>
            <asp:RangeValidator ID="rvOpenDate" runat="server" 
                ControlToValidate="txtOpeningDate" Display="Dynamic" ErrorMessage="*" 
                MaximumValue="31/12/2099" MinimumValue="01/01/1900" Type="Date"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
	    <td colspan="2">
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/SubzoneList.aspx" runat="server">Back</asp:HyperLink>
        </td>
	</tr>
	</table>
</asp:Content>
