<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeAddNotification.aspx.cs" Inherits="Asa.Hrms.WebSite.HRM.H_EmployeeAddNotification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr><td colspan="2" style="font-size:36pt; font-weight:bold; color:Red;">আপনি কি নুতন নিয়োগ প্রাপ্ত কর্মীর  <br /> <u>নতুন আইডি</u> নম্বর পেতে চান?</td></tr>
    <tr><td></td><td></td></tr>
    <tr><td style="width:120px;">
        <asp:Button ID="btnYes" runat="server" Text="Yes" onclick="btnYes_Click" 
            Width="100px" /></td><td>
            <asp:Button ID="btnNo" runat="server" Text="No" onclick="btnNo_Click" 
                Width="100px" /></td></tr>
</table>
</asp:Content>

