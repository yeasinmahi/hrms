<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="SubzoneList.aspx.cs" Inherits="SubzoneList" Title="" %>
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
						<asp:HyperLinkField DataTextField="Name" HeaderText="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="SubzoneAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="NameInBangla" HeaderText="Bangla Name" SortExpression="NameInBangla"></mms:BoundField>
						<mms:BoundField DataField="ZoneName" HeaderText="Zone Name" SortExpression="ZoneName" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" FieldType="String"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
