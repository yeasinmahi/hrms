<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_DeductionList.aspx.cs" Inherits="GITS.Hrms.WebSite.Payroll.P_DeductionList" %>
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
						<asp:HyperLinkField HeaderText="Name" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="P_DeductionAdd.aspx?Id={0}"></asp:HyperLinkField>
						<mms:BoundField DataField="IsFixed" HeaderText="IsFixed" SortExpression="IsFixed" FieldType="String"></mms:BoundField>

                        <mms:BoundField DataField="SortOrder" HeaderText="SortOrder" SortExpression="SortOrder" FieldType="Int32"></mms:BoundField>
                        
                        <mms:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" FieldType="String"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
	</table>
</asp:Content>

