<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_MonthlyReportList.aspx.cs" Inherits="Asa.Hrms.WebSite.HRM.H_MonthlyReportList" %>
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
						<asp:HyperLinkField HeaderText="Report Date" DataTextField="ReportDate" DataTextFormatString="{0:dd/MM/yyyy}" SortExpression="ReportDate"  DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_MonthlyReportAdd.aspx?Id={0}"></asp:HyperLinkField>
						
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>

