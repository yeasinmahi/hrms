<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_EmployeeGradeList.aspx.cs" Inherits="H_EmployeeGradeList" Title="" %>
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
						<asp:HyperLinkField HeaderText=" Employee" DataTextField="H_EmployeeId" SortExpression="H_EmployeeId" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_EmployeeGradeAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="H_GradeId" HeaderText=" Grade" SortExpression="H_GradeId" DataFormatString="{0:#,##;(#,##);0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FieldType="Int32"></mms:BoundField>
						<mms:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<mms:BoundField DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
