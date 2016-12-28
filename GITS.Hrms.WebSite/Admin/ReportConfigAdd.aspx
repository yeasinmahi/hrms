<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReportConfigAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.Admin.ReportConfigAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="200" Width="650px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
	    <td align="right"><asp:label runat="server" ID="lblReportType">Report Type:</asp:label></td>
	    <td>
	        <asp:DropDownList runat="server" ID="ddlReportType" Width="250"></asp:DropDownList>
	    </td>
	</tr>
	<tr>
		<td align="right">Location:</td>
		<td>
			<asp:CheckBox runat="server" ID="chkLocation" Checked="true" Text=""></asp:CheckBox>
		</td>
	</tr>
	<tr>
		<td align="right">Position:</td>
		<td>
			<asp:CheckBox runat="server" ID="chkPosition" Checked="true" Text=""></asp:CheckBox>
		</td>
	</tr>
	<tr>
		<td align="right">Religion and Sex:</td>
		<td>
			<asp:CheckBox runat="server" ID="chkReligionAndSex" Checked="true" Text=""></asp:CheckBox>
		</td>
	</tr>
	<tr>
		<td align="right">Date:</td>
		<td>
			<asp:RadioButtonList ID="rdoDate" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="2">Date Between</asp:ListItem>
                <asp:ListItem Value="1">As On Date</asp:ListItem>
                <asp:ListItem Value="0">None</asp:ListItem>
            </asp:RadioButtonList>
		</td>
	</tr>
	<tr>
		<td align="right">Query:</td>
		<td>
		<asp:TextBox runat="server" ID="txtQuery" Text="" TextMode="MultiLine" 
                MaxLength="200" Width="650px" Height="600px"></asp:TextBox><asp:RequiredFieldValidator ID="rvfQuery" runat="server" Display="Dynamic" ControlToValidate="txtQuery" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
	    <td colspan="2">
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/ReportConfigList.aspx" runat="server">Back</asp:HyperLink>
        </td>
	</tr>
	</table>
</asp:Content>
