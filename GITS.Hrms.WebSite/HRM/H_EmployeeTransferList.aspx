<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_EmployeeTransferList.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeTransferList" Title="" %>
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
						<asp:HyperLinkField Text="Letter" HeaderText="Action" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_TransferAdd.aspx?Id={0}"></asp:HyperLinkField>
						<asp:HyperLinkField HeaderText=" Employee" DataTextField="EmployeeName" SortExpression="EmployeeName" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_EmployeeTransferAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="Emp_ID" HeaderText="Emp_ID" SortExpression="Emp_ID" FieldType="Int32"></mms:BoundField>
						<mms:BoundField DataField="Type" HeaderText="Type" SortExpression="Type"></mms:BoundField>
						<mms:BoundField DataField="LetterNo" HeaderText="Letter No" SortExpression="LetterNo" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="LetterDate" HeaderText="Letter Date" SortExpression="LetterDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<mms:BoundField DataField="SourceBranch" HeaderText="Source Branch" SortExpression="SourceBranch"   HeaderStyle-HorizontalAlign="Right" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="DestinationBranch" HeaderText="Destination Branch" SortExpression="DestinationBranch"  HeaderStyle-HorizontalAlign="Right" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="JoiningDate" HeaderText="Joining Date" SortExpression="JoiningDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<%--<mms:BoundField DataField="PresentMobile" HeaderText="Present Mobile" SortExpression="PresentMobile" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="PastMobile" HeaderText="Past Mobile" SortExpression="PastMobile" FieldType="String"></mms:BoundField>--%>
						<mms:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" FieldType="String"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
