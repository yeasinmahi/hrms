<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_StartupSalaryList.aspx.cs" Inherits="GITS.Hrms.WebSite.Payroll.P_StartupSalaryList" %>
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
						</asp:TemplateField>
						<asp:HyperLinkField HeaderText="Name" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="P_StartupSalaryAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" FieldType="Int32"></mms:BoundField>
                        <mms:BoundField DataField="LastIncrementDate" HeaderText="Last IncrementDate" SortExpression="LastIncrementDate" DataFormatString="{0:d}" FieldType="DateTime"></mms:BoundField>
                        <mms:BoundField DataField="LastBasic" HeaderText="Last Basic" SortExpression="LastBasic" DataFormatString="{0:n}" FieldType="Double"></mms:BoundField>
                        <mms:BoundField DataField="PresentBasic" HeaderText="Present Basic" SortExpression="PresentBasic" DataFormatString="{0:n}" FieldType="Double"></mms:BoundField>
                        <mms:BoundField DataField="IsActive" HeaderText="Status" SortExpression="IsActive" FieldType="String"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>

