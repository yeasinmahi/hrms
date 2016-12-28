<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_LateAttendanceList.aspx.cs" Inherits="GITS.Hrms.WebSite.Payroll.P_LateAttendanceList" %>
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
						<asp:HyperLinkField HeaderText="Name" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="P_LateAttendanceAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" FieldType="Int32"></mms:BoundField>
						<mms:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<mms:BoundField DataField="EndDate" HeaderText="End Date" SortExpression="EndDate" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<mms:BoundField DataField="Late96_930" HeaderText="Late 9:06-9:30" SortExpression="Late96_930" FieldType="Int32"></mms:BoundField>
						<mms:BoundField DataField="Late931_days" HeaderText="Late 9:31-day" SortExpression="Late931_days" FieldType="Int32"></mms:BoundField>
						<mms:BoundField DataField="Absent" HeaderText="Absent" SortExpression="Absent" FieldType="Int32"></mms:BoundField>

					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>

