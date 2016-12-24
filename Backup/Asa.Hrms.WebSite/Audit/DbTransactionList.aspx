<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="DbTransactionList.aspx.cs" Inherits="DbTransactionList" Title="" %>
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
						<asp:HyperLinkField HeaderText="Description" DataTextField="Description" SortExpression="Description" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="DbTransactionAdd.aspx?Id={0}"></asp:HyperLinkField>
						<asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy"></asp:BoundField>
						<asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="CreatedDate" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
