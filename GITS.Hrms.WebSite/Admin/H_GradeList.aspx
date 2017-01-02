<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_GradeList.aspx.cs" Inherits="GITS.Hrms.WebSite.Admin.H_GradeList" Title="" %>
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
						<asp:HyperLinkField HeaderText="Name" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" ItemStyle-VerticalAlign="Bottom" DataNavigateUrlFormatString="H_GradeAdd.aspx?Id={0}"></asp:HyperLinkField>
						<asp:TemplateField HeaderText="Designation">
                            <ItemTemplate>
                                <asp:BulletedList runat="server" ID="blDesignation" DataTextField="Name" DataValueField="Name">
                                </asp:BulletedList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <mms:BoundField DataField="SortOrder" HeaderText="SortOrder" SortExpression="SortOrder" FieldType="Int32"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
