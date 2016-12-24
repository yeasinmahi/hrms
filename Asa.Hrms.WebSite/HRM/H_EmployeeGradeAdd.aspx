<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_EmployeeGradeAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeGradeAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right"> Employee:</td>
		<td>
			<asp:DropDownList ID="ddlH_EmployeeId" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right"> Grade:</td>
		<td>
			<asp:DropDownList ID="ddlH_GradeId" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
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
			<asp:RangeValidator ControlToValidate="txtEndDate" ID="rvEndDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid end date"></asp:RangeValidator>
		</td>
	</tr>
	</table>
</asp:Content>
