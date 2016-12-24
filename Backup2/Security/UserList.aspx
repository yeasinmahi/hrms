<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="UserList" Title="" Codebehind="UserList.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table style="width: 100%">
	    <tr>
			<td>
				<mms:GridViewSearchPanel ID="gvspList" runat="server" GridViewControlID="gvList" OnResetButtonClicked="gvspList_ResetButtonClicked" OnSearchButtonClicked="gvspList_SearchButtonClicked" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:GridView ID="gvList" runat="server" 
                    OnRowCreated="gvList_RowCreated" OnSorting="gvList_Sorting" 
                    DataKeyNames="Id" onrowdatabound="gvList_RowDataBound">
					<Columns>
						<asp:TemplateField>
						<ItemStyle HorizontalAlign="Center" Width="20px" />
						<ItemTemplate>
							<asp:CheckBox ID="chkSelect" runat="server"/>
						</ItemTemplate>
						<HeaderTemplate>
							<input id="chkAll" runat="server" type="checkbox" onclick="javascript:GridSelectAll(this);" />
						</HeaderTemplate>
						</asp:TemplateField>
						<asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" ReadOnly="True" Visible="False"></asp:BoundField>
						<asp:HyperLinkField HeaderText="Login" DataTextField="Login" SortExpression="Login" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="UserAdd.aspx?Id={0}"></asp:HyperLinkField>
						<asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"></asp:BoundField>
						<asp:BoundField DataField="IsActive" HeaderText="Is Active" SortExpression="IsActive"></asp:BoundField>
						<asp:BoundField DataField="UserType" HeaderText="User Type" SortExpression="UserType"></asp:BoundField>
						<asp:TemplateField HeaderText="Roles">
						    <ItemTemplate>
						        <asp:BulletedList runat="server" ID="blRole" DataTextField="RoleName" DataValueField="RoleName">
						        </asp:BulletedList>
						    </ItemTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
