<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_EmployeePromotionList.aspx.cs" Inherits="H_EmployeePromotionList" Title="" %>
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
						<asp:HyperLinkField HeaderText="Employee" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_EmployeePromotionAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="Code" HeaderText="Emp.ID" SortExpression="Code"></mms:BoundField>
						<mms:BoundField DataField="Type" HeaderText="Type" SortExpression="Type"></mms:BoundField>
						<mms:BoundField DataField="LetterNo" HeaderText="Letter No" SortExpression="LetterNo" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="LetterDate" HeaderText="Letter Date" SortExpression="LetterDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<mms:BoundField DataField="PromotionDate" HeaderText="Promotion Date" SortExpression="PromotionDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<mms:BoundField DataField="OldGrade" HeaderText="Old Grade" SortExpression="OldGrade"  ></mms:BoundField>
						<mms:BoundField DataField="NewGrade" HeaderText="New Grade" SortExpression="NewGrade"  ></mms:BoundField>
						<mms:BoundField DataField="OldDesignation" HeaderText="Old Designation" SortExpression="OldDesignation" ></mms:BoundField>
						<mms:BoundField DataField="NewDesignation" HeaderText="New Designation" SortExpression="NewDesignation" ></mms:BoundField>
						<mms:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" FieldType="String"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
