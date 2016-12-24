<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_EmployeeList.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeList" Title="" %>
<%@ Register TagPrefix="mms" Namespace="GITS.Hrms.Library.Web" Assembly="GITS.Hrms.Library" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table style="width: 100%">
		<tr>
			<td>
				<mms:GridViewSearchPanel ID="gvspList" runat="server" GridViewControlID="gvList" OnResetButtonClicked="gvspList_ResetButtonClicked" OnSearchButtonClicked="gvspList_SearchButtonClicked" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:GridView ID="gvList" runat="server" OnRowCreated="gvList_RowCreated" OnSorting="gvList_Sorting" DataKeyNames="Id">
					<Columns>
						<asp:TemplateField>
						<ItemStyle HorizontalAlign="Center" Width="20px" />
						<ItemTemplate>
							<asp:CheckBox ID="chkSelect" runat="server" />
						</ItemTemplate>
						<HeaderTemplate>
							<input id="chkAll" runat="server" type="checkbox" onclick="javascript:GridSelectAll(this);" />
						</HeaderTemplate>
						</asp:TemplateField>
						<mms:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" FieldType="Int32"></mms:BoundField>
						<asp:HyperLinkField HeaderText="Name" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_EmployeeAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="NameInBangla" HeaderText="Bangla Name" SortExpression="NameInBangla" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="FatherName" HeaderText="Father Name" SortExpression="FatherName" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="MotherName" HeaderText="Mother Name" SortExpression="MotherName" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="DateOfBirth" HeaderText="Date Of Birth" SortExpression="DateOfBirth" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<%--<mms:BoundField DataField="BloodGroup" HeaderText="Blood Group" SortExpression="BloodGroup"></mms:BoundField>--%>
						<mms:BoundField DataField="Sex" HeaderText="Sex" SortExpression="Sex"></mms:BoundField>
						<mms:BoundField DataField="MaritalStatus" HeaderText="Marital Status" SortExpression="MaritalStatus"></mms:BoundField>
						<mms:BoundField DataField="Religion" HeaderText="Religion" SortExpression="Religion"></mms:BoundField>
						<mms:BoundField DataField="AppointmentLetterDate" HeaderText="Appointment Letter Date" SortExpression="AppointmentLetterDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<mms:BoundField DataField="AppointmentLetterNo" HeaderText="Appointment Letter No" SortExpression="AppointmentLetterNo" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="BranchName" HeaderText="Branch" SortExpression="BranchName"></mms:BoundField>
						<mms:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="DepartmentName"></mms:BoundField>
						<mms:BoundField DataField="GradeName" HeaderText="Grade" SortExpression="GradeName"></mms:BoundField>
						<mms:BoundField DataField="DesignationName" HeaderText="Designation" SortExpression="DesignationName"></mms:BoundField>
						<mms:BoundField DataField="EmploymentType" HeaderText="Employment Type" SortExpression="EmploymentType"></mms:BoundField>
						<mms:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
