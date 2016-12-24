<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_ProfessionalQualificationAdd.aspx.cs" Inherits="H_ProfessionalQualificationAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Employee Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtEmployeeName" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="300px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Certification:</td>
		<td>
			<asp:TextBox runat="server" ID="txtCertification" Text="" MaxLength="200" Width="300px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvCertification" runat="server" Display="Dynamic" ControlToValidate="txtCertification" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Institute Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtInstituteName" Text="" MaxLength="100" Width="300px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvInstituteName" runat="server" Display="Dynamic" ControlToValidate="txtInstituteName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Location:</td>
		<td>
			<asp:TextBox runat="server" ID="txtLocation" Text="" MaxLength="100" Width="300px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvLocation" runat="server" Display="Dynamic" ControlToValidate="txtLocation" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Start Date:</td>
		<td>
			<asp:TextBox runat="server" ID="txtStartDate" Text="" MaxLength="10"></asp:TextBox>
			<asp:ImageButton ID="ibStartDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate'));return false;"></asp:ImageButton>
			<asp:RequiredFieldValidator ID="rfvStartDate" runat="server" Display="Dynamic" ControlToValidate="txtStartDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			<asp:RangeValidator ControlToValidate="txtStartDate" ID="rvStartDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid start date"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
		<td align="right">End Date:</td>
		<td>
			<asp:TextBox runat="server" ID="txtEndDate" Text="" MaxLength="10"></asp:TextBox>
			<asp:ImageButton ID="ibEndDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate'));return false;"></asp:ImageButton>
			<asp:RequiredFieldValidator ID="rfvEndDate" runat="server" Display="Dynamic" ControlToValidate="txtEndDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			<asp:RangeValidator ControlToValidate="txtEndDate" ID="rvEndDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data"></asp:RangeValidator>
			<asp:CompareValidator ID="cvEndDate" Type="Date" Operator="GreaterThan" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Display="Dynamic" runat="server" ErrorMessage="*" ToolTip="End date should greater than start date"></asp:CompareValidator>
		</td>
	</tr>
	<tr>
		<td align="right" style="font-size: smaller">Sort Order<br />in grid view:</td>
		<td>
			<asp:TextBox runat="server" ID="txtSortOrder" Text="" MaxLength="10"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvSortOrder" runat="server" Display="Dynamic" ControlToValidate="txtSortOrder" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			<asp:RangeValidator ControlToValidate="txtSortOrder" ID="rvSortOrder" Type="Integer" MaximumValue="2147483647" MinimumValue="0" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid integer values are allowed"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
		<td colspan="2">
                <asp:HyperLink ID="hlBack" NavigateUrl="~/HRM/H_ProfessionalQualificationList.aspx" runat="server">Back</asp:HyperLink>
            </td>
		</tr>
	</table>
	</table>
</asp:Content>
