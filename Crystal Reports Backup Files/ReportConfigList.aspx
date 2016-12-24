<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ReportConfigList.aspx.cs" Inherits="ReportConfigList" Title="" %>
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
						<asp:HyperLinkField HeaderText="Name" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="ReportConfigAdd.aspx?Id={0}"></asp:HyperLinkField>
						<asp:BoundField DataField="Type" HeaderText="Report Type" SortExpression="Type"></asp:BoundField>
						<asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location"></asp:BoundField>
						<asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position"></asp:BoundField>
						<asp:BoundField DataField="ReligionAndSex" HeaderText="Religion And Sex" SortExpression="ReligionAndSex"></asp:BoundField>
						<asp:BoundField DataField="DateBetween" HeaderText="Date Between" SortExpression="DateBetween"></asp:BoundField>
						<mms:BoundField DataField="Query" HeaderText="Query" SortExpression="Query"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
