<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_ExperienceAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_ExperienceAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Employee Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtEmployeeName" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="300px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Company Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtCompanyName" Text="" MaxLength="200" Width="300px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" Display="Dynamic" ControlToValidate="txtCompanyName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Company Business:</td>
		<td>
			<asp:TextBox runat="server" ID="txtCompanyBusiness" Text="" MaxLength="100" Width="300px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvCompanyBusiness" runat="server" Display="Dynamic" ControlToValidate="txtCompanyBusiness" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Company Location:</td>
		<td>
			<asp:TextBox runat="server" ID="txtCompanyLocation" Text="" MaxLength="100"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvCompanyLocation" runat="server" Display="Dynamic" ControlToValidate="txtCompanyLocation" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Position Held:</td>
		<td>
			<asp:TextBox runat="server" ID="txtPositionHeld" Text="" MaxLength="100" Width="300px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvPositionHeld" runat="server" Display="Dynamic" ControlToValidate="txtPositionHeld" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Department:</td>
		<td>
			<asp:TextBox runat="server" ID="txtDepartment" Text="" MaxLength="100"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvDepartment" runat="server" Display="Dynamic" ControlToValidate="txtDepartment" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Responsibilities:</td>
		<td>
			<asp:TextBox runat="server" ID="txtResponsibilities" Text="" MaxLength="1000" Width="300px" TextMode="MultiLine" Height="50px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvResponsibilities" runat="server" Display="Dynamic" ControlToValidate="txtResponsibilities" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
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
			<asp:RangeValidator ControlToValidate="txtEndDate" ID="rvEndDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid end date"></asp:RangeValidator>
			<asp:CompareValidator ID="cvEndDate" Type="Date" Operator="GreaterThan" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Display="Dynamic" runat="server" ErrorMessage="*" ToolTip="End date should greater than start date"></asp:CompareValidator>
		</td>
	</tr>
	<tr>
		<td align="right" style="font-size: smaller;">Sort Order <br />in grid view:</td>
		<td>
			<asp:TextBox runat="server" ID="txtSortOrder" Text="" MaxLength="10"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvSortOrder" runat="server" Display="Dynamic" ControlToValidate="txtSortOrder" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			<asp:RangeValidator ControlToValidate="txtSortOrder" ID="rvSortOrder" Type="Integer" MaximumValue="2147483647" MinimumValue="0" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid integer values are allowed"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
		<td colspan="2">
                <asp:HyperLink ID="hlBack" NavigateUrl="~/HRM/H_ExperienceList.aspx" runat="server">Back</asp:HyperLink>
            </td>
		</tr>
	</table>
</asp:Content>
