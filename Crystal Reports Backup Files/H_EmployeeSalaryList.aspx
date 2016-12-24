<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeSalaryList.aspx.cs" Inherits="Asa.Hrms.WebSite.HRM.H_EmployeeSalaryList" MasterPageFile="~/Site.master" Title="" %>

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
<asp:HyperLinkField HeaderText="Employee" DataTextField="H_EmployeeId" SortExpression="H_EmployeeId" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_EmployeeSalaryAdd.aspx?Id={0}" />
<mms:BoundField DataField="BasicSalary" HeaderText="Basic Salary" SortExpression="BasicSalary" FieldType="Double"></mms:BoundField>
<mms:BoundField DataField="LastIncrementDate" HeaderText="Last Increment Date" FieldType="DateTime" SortExpression="LastIncrementDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
</Columns>
</asp:GridView>
</td>
</tr>
</table>
</asp:Content>