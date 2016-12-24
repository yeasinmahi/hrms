<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeMultiLetterList.aspx.cs" Inherits="Asa.Hrms.WebSite.HRM.H_EmployeeMultiLetterList" %>
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
						<%--<asp:TemplateField>
						<ItemStyle HorizontalAlign="Center" Width="20px" />
						<ItemTemplate>
							<asp:CheckBox ID="chkSelect" runat="server" />
						</ItemTemplate>
						<HeaderTemplate>
							<input id="chkAll" runat="server" type="checkbox" onclick="javascript:GridSelectAll(this);" />
						</HeaderTemplate>
						</asp:TemplateField>--%>
						<asp:HyperLinkField DataTextField="Name" HeaderText="Employee" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_EmployeeMultiLetterAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" FieldType="Int32"></mms:BoundField>
						<mms:BoundField DataField="LetterNo" HeaderText="Letter No" SortExpression="LetterNo" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="LetterDate" HeaderText="Letter Date" SortExpression="LetterDate" FieldType="DateTime" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
						<mms:BoundField DataField="EffectiveDate" HeaderText="Effective Date" SortExpression="EffectiveDate" FieldType="DateTime" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>

