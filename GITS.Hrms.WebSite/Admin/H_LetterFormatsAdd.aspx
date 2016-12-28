<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_LetterFormatsAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.Admin.H_LetterFormatsAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Letter Type</td>
		<td>
			<asp:DropDownList ID="ddlLetterType" runat="server" Width="250px">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Title:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="250" 
                 Width="500px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Address To:</td>
		<td>
			<asp:TextBox runat="server" ID="txtInsideAddress" Text="" MaxLength="250" 
                 Width="500px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Subject:</td>
		<td>
			<asp:TextBox runat="server" ID="txtSubject" Text="" MaxLength="250" 
                 Width="500px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Letter Body:</td>
		<td>
			<asp:TextBox runat="server" ID="txtLetterBody" Text="" MaxLength="100" 
                TextMode="MultiLine" Width="500px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Complimentary</td>
		<td>
			<asp:TextBox runat="server" ID="txtComplimentary" Text="" MaxLength="250" 
                 Width="500px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Signatory</td>
		<td>
			<asp:TextBox runat="server" ID="txtSignatory" Text="" MaxLength="250" 
                 Width="500px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Designation</td>
		<td>
			<asp:TextBox runat="server" ID="txtDesignation" Text="" MaxLength="250" 
                 Width="500px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Sort Order</td>
		<td>
			<asp:TextBox ID="txtSortOrder" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSortOrder" runat="server" ControlToValidate="txtSortOrder" 
                ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rvSortOrder" runat="server" ControlToValidate ="txtSortOrder" 
                Type="Integer" MinimumValue="1" MaximumValue="999" ErrorMessage="*"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
	    <td colspan="2">
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/H_LetterFormatsList.aspx" runat="server">Back</asp:HyperLink>
        </td>
	</tr>
	</table>
</asp:Content>

