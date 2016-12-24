<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeRejoinList.aspx.cs" Inherits="Asa.Hrms.WebSite.HRM.H_EmployeeRejoinList" MasterPageFile="~/Site.master" Title="" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<table style="width: 100%";>
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
<asp:HyperLinkField HeaderText="Employee" DataTextField="Name" SortExpression="Name" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="H_EmployeeRejoinAdd.aspx?Id={0}" />
<mms:BoundField DataField="Code" HeaderText="Code" SortExpression="Code"></mms:BoundField>
<mms:BoundField DataField="LeaveType" HeaderText="Leave Type" SortExpression="LeaveType"></mms:BoundField>
<mms:BoundField DataField="LetterNo" HeaderText="Letter No." FieldType="String" SortExpression="LetterNo"></mms:BoundField>
<mms:BoundField DataField="LetterDate" HeaderText="Letter Date" FieldType="DateTime" SortExpression="LetterDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
<mms:BoundField DataField="FromDate" HeaderText="From Date" FieldType="DateTime" SortExpression="FromDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
<mms:BoundField DataField="RejoinDate" HeaderText="Re-join Date" FieldType="DateTime" SortExpression="RejoinDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
<mms:BoundField DataField="SourceBranch" HeaderText="Source Branch" SortExpression="SourceBranch" FieldType="String" ItemStyle-HorizontalAlign="Right" ></mms:BoundField>
<mms:BoundField DataField="DestinationBranch" HeaderText="Destination Branch" SortExpression="DestinationBranch" FieldType="String" ></mms:BoundField>
</Columns>
</asp:GridView>
</td>
</tr>
</table>
</asp:Content>
