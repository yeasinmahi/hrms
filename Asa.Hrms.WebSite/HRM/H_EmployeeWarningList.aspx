<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeWarningList.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeWarningList" MasterPageFile="~/Site.master" Title="" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<table style="width: 100%;">
<tr>
<td style="width: 90%;">
<mms:GridViewSearchPanel ID="gvspList" runat="server" GridViewControlID="gvList" OnResetButtonClicked="gvspList_ResetButtonClicked" OnSearchButtonClicked="gvspList_SearchButtonClicked" />
</td>
</tr>
<tr>
<td colspan="2">
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
<asp:HyperLinkField HeaderText="Employee" DataTextField="H_EmployeeId" SortExpression="H_EmployeeId" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_EmployeeWarningAdd.aspx?Id={0}" />
<mms:BoundField DataField="LetterNo" HeaderText="Letter No." FieldType="String" SortExpression="LetterNo"></mms:BoundField>
<mms:BoundField DataField="LetterDate" HeaderText="Letter Date" FieldType="DateTime" SortExpression="LetterDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
<mms:BoundField DataField="Duration" HeaderText="Duration" FieldType="String" SortExpression="Duration"></mms:BoundField> 
<mms:BoundField DataField="TotalWarningTime" HeaderText="Total Warning Time" FieldType="Int32" SortExpression="TotalWarningTime" ItemStyle-HorizontalAlign="Right"></mms:BoundField>
<mms:BoundField DataField="BranchId" HeaderText="Branch ID" SortExpression="BranchId" DataFormatString="{0:#,##;(#,##);0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" FieldType="Int32"></mms:BoundField>
<mms:BoundField DataField="Cause" HeaderText="Cause" SortExpression="Cause" FieldType="String"></mms:BoundField>
</Columns>
</asp:GridView>
</td>
</tr>
</table>
</asp:Content>
