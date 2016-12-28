<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_SalaryCardReport.aspx.cs" Inherits="GITS.Hrms.WebSite.Payroll.P_SalaryCardReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
    <td>Year:</td>
    <td>
        <asp:DropDownList ID="ddlYear" runat="server">
        </asp:DropDownList>
        </td>
    </tr>
    <tr>
    <td>Month:</td>
    <td>
        <asp:DropDownList ID="ddlMonth" runat="server">
        <asp:ListItem Value="0">Select Month</asp:ListItem>
            <asp:ListItem Value="1">January</asp:ListItem>
            <asp:ListItem Value="2">February</asp:ListItem>
            <asp:ListItem Value="3">March</asp:ListItem>
            <asp:ListItem Value="4">April</asp:ListItem>
            <asp:ListItem Value="5">May</asp:ListItem>
            <asp:ListItem Value="6">June</asp:ListItem>
            <asp:ListItem Value="7">July</asp:ListItem>
            <asp:ListItem Value="8">August</asp:ListItem>
            <asp:ListItem Value="9">September</asp:ListItem>
            <asp:ListItem Value="10">October</asp:ListItem>
            <asp:ListItem Value="11">November</asp:ListItem>
            <asp:ListItem Value="12">December</asp:ListItem>
        </asp:DropDownList>
        </td>
    </tr>
</table>
</asp:Content>

