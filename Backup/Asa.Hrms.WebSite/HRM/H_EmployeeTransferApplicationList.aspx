<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeTransferApplicationList.aspx.cs" Inherits="Asa.Hrms.WebSite.HRM.H_EmployeeTransferApplicationList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
						<asp:HyperLinkField HeaderText=" Employee" DataTextField="EmployeeName" SortExpression="EmployeeName" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_EmployeeTransferApplicationAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="EmployeeCode" HeaderText="Emp_ID" SortExpression="EmployeeCode" FieldType="Int32"></mms:BoundField>
						<mms:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation"></mms:BoundField>
					
						<mms:BoundField DataField="ApplicationNo" HeaderText="Application No" SortExpression="ApplicationNo"></mms:BoundField>
					
						<mms:BoundField DataField="ApplicationDate" HeaderText="Application Date" SortExpression="ApplicationDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
					
						<mms:BoundField DataField="ReceivingDate" HeaderText="Receiving Date" SortExpression="ReceivingDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<mms:BoundField DataField="DemandedPlace" HeaderText="Demanded Place" SortExpression="DemandedPlace" FieldType="String"></mms:BoundField>
					
						<mms:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" FieldType="String"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
