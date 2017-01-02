<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_TrainingAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_TrainingAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Employee Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtEmployeeName" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="300px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Training Title:</td>
		<td>
			<asp:TextBox runat="server" ID="txtTitle" Text="" MaxLength="200" Width="300px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvTitle" runat="server" Display="Dynamic" ControlToValidate="txtTitle" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Topics Covered:</td>
		<td>
			<asp:TextBox runat="server" ID="txtTopics" Text="" MaxLength="200" Width="300px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvTopics" runat="server" Display="Dynamic" ControlToValidate="txtTopics" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
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
		<td align="right">Country:</td>
		<td>
			<asp:TextBox runat="server" ID="txtCountry" Text="" MaxLength="100"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvCountry" runat="server" Display="Dynamic" ControlToValidate="txtCountry" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Location:</td>
		<td>
			<asp:TextBox runat="server" ID="txtLocation" Text="" MaxLength="100"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvLocation" runat="server" Display="Dynamic" ControlToValidate="txtLocation" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Training Year:</td>
		<td>
			<asp:TextBox runat="server" ID="txtTrainingYear" Text="" MaxLength="100"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvTrainingYear" runat="server" Display="Dynamic" ControlToValidate="txtTrainingYear" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			<asp:RangeValidator ControlToValidate="txtTrainingYear" ID="rvTrainingYear" Type="Integer" MaximumValue="2099" MinimumValue="1900" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid year"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Duration:</td>
		<td>
			<asp:TextBox runat="server" ID="txtDuration" Text="" MaxLength="100"></asp:TextBox>		
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
                <asp:HyperLink ID="hlBack" NavigateUrl="~/HRM/H_TrainingList.aspx" runat="server">Back</asp:HyperLink>
            </td>
		</tr>
	</table>
</asp:Content>
