<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_LetterFormatsList.aspx.cs" Inherits="Asa.Hrms.WebSite.Admin.H_LetterFormatsList" %>
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
						<asp:HyperLinkField HeaderText="Letter Title" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_LetterFormatsAdd.aspx?Id={0}"></asp:HyperLinkField>
						<asp:BoundField DataField="SortOrder" HeaderText="Sort Order" SortExpression="SortOrder"></asp:BoundField>
						<asp:BoundField DataField="LetterType" HeaderText="Letter Type" SortExpression="LetterType"></asp:BoundField>
						<asp:BoundField DataField="Subject" HeaderText="Subject" SortExpression="Subject"></asp:BoundField>
						<asp:BoundField DataField="LetterBody" HeaderText="Letter Body" SortExpression="LetterBody"></asp:BoundField>
						<asp:BoundField DataField="Conclusion" HeaderText="Complimentary" SortExpression="Conclusion"></asp:BoundField>
						
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>

