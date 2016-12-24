<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="BranchList.aspx.cs" Inherits="GITS.Hrms.WebSite.Admin.BranchList" Title="" %>
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
						<asp:HyperLinkField HeaderText="Branch Name" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="BranchAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="NameInBangla" HeaderText="Bangla Name" SortExpression="NameInBangla"></mms:BoundField>
						<mms:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" DataFormatString="{0:#,##;(#,##);0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FieldType="Int32"></mms:BoundField>
						<mms:BoundField DataField="ZoneName" HeaderText="Zone" SortExpression="ZoneName"></mms:BoundField>
						<mms:BoundField DataField="SubzoneName" HeaderText="Subzone" SortExpression="SubzoneName"></mms:BoundField>
						<mms:BoundField DataField="RegionName" HeaderText="Region" SortExpression="RegionName"></mms:BoundField>
						<mms:BoundField DataField="DistrictName" HeaderText="District" SortExpression="DistrictName"></mms:BoundField>
						<mms:BoundField DataField="ThanaName" HeaderText="Thana" SortExpression="ThanaName"></mms:BoundField>
						<mms:BoundField DataField="BranchType" HeaderText="Branch Type" SortExpression="BranchType"></mms:BoundField>
						<mms:BoundField DataField="OpeningDate" HeaderText="Opening Date" SortExpression="OpeningDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<mms:BoundField DataField="MobileNumber" HeaderText="Mobile Number" SortExpression="MobileNumber" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="LocationType" HeaderText="Location Type" SortExpression="LocationType"></mms:BoundField>
						
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>
