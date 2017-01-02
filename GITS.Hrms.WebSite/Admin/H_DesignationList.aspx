<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_DesignationList.aspx.cs" Inherits="GITS.Hrms.WebSite.Admin.H_DesignationList" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table style="width: 100%">
		<tr>
			<td>
				<mms:GridViewSearchPanel ID="gvspList" runat="server" GridViewControlID="gvList" OnResetButtonClicked="gvspList_ResetButtonClicked" OnSearchButtonClicked="gvspList_SearchButtonClicked" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:GridView ID="gvList" runat="server" OnRowCreated="gvList_RowCreated" OnSorting="gvList_Sorting" OnRowDataBound="gvList_RowDataBound" DataKeyNames="Id">
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
						<asp:HyperLinkField HeaderText="Name" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_DesignationAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="BanglaName" HeaderText="Bangla Name" SortExpression="BanglaName" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="ShortName" HeaderText="Short Name" SortExpression="ShortName" FieldType="String"></mms:BoundField>
						<asp:TemplateField HeaderText="Grade">
                            <ItemTemplate>
                                <asp:BulletedList runat="server" ID="blGrade" DataTextField="Name" DataValueField="Name">
                                </asp:BulletedList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <mms:BoundField DataField="SortOrder" HeaderText="SortOrder" SortExpression="SortOrder" FieldType="Int32"></mms:BoundField>
                        <mms:BoundField DataField="GroupType" HeaderText="Group Type" SortExpression="GroupType" FieldType="String"></mms:BoundField>
                        <mms:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" FieldType="String"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
