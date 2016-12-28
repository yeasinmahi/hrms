<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeActingDesignationList.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeActingDesignationList" MasterPageFile="~/Site.master" Title="" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<table style="width: 100%">
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
<asp:HyperLinkField HeaderText="Employee" DataTextField="H_EmployeeId" SortExpression="H_EmployeeId" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_EmployeeActingDesignationAdd.aspx?Id={0}" />
<mms:BoundField DataField="InchargedGradeId" FieldType="Int32" HeaderText="Incharged Grade Id" SortExpression="InchargedGradeId" ItemStyle-HorizontalAlign="Right"></mms:BoundField>
<mms:BoundField DataField="InchargedDesignationId" FieldType="Int32" HeaderText="Icharged Designation Id" SortExpression="InchargedDesignationId" ItemStyle-HorizontalAlign="Right"></mms:BoundField>
<mms:BoundField DataField="FromDate" HeaderText="From Date" FieldType="DateTime" SortExpression="FromDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
<mms:BoundField DataField="ToDate" HeaderText="To Date" FieldType="DateTime" SortExpression="ToDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
</Columns>
</asp:GridView>
</td>
</tr>
</table>
</asp:Content>