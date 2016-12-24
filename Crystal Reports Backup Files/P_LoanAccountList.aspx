<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_LoanAccountList.aspx.cs" Inherits="Asa.Hrms.WebSite.Payroll.P_LoanAccountList" %>
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
						
						<asp:HyperLinkField HeaderText="Employee" DataTextField="Employee" SortExpression="Employee" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="P_LoanAccountAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="Code" HeaderText="Code" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="LoanType" HeaderText="LoanType" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="DisbursDate" HeaderText="Disburs Date" FieldType="DateTime" DataFormatString="{0:dd/MM/yyy}"></mms:BoundField>
						<mms:BoundField DataField="LoanAmount" HeaderText="LoanAmount" FieldType="Double" DataFormatString="{0:n}"></mms:BoundField>
						<mms:BoundField DataField="InterestRate" HeaderText="InterestRate" FieldType="Double" DataFormatString="{0:n}"></mms:BoundField>
						<mms:BoundField DataField="InterestAmount" HeaderText="InterestAmount" FieldType="Double" DataFormatString="{0:n}"></mms:BoundField>
						<mms:BoundField DataField="TotalAmount" HeaderText="TotalAmount" FieldType="Double" DataFormatString="{0:n}"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>

