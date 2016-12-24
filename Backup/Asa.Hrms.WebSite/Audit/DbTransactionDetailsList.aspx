<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="DbTransactionDetailsList.aspx.cs" Inherits="DbTransactionDetailsList" Title="" %>
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
						<asp:HyperLinkField HeaderText="Db Transaction Id" DataTextField="DbTransactionId" SortExpression="DbTransactionId" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="DbTransactionDetailsAdd.aspx?Id={0}"></asp:HyperLinkField>
						<asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type"></asp:BoundField>
						<asp:BoundField DataField="TableName" HeaderText="Table Name" SortExpression="TableName"></asp:BoundField>
						<asp:BoundField DataField="IdentityColumn" HeaderText="Identity Column" SortExpression="IdentityColumn"></asp:BoundField>
						<asp:BoundField DataField="IdentityValue" HeaderText="Identity Value" SortExpression="IdentityValue"></asp:BoundField>
						<asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value"></asp:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
