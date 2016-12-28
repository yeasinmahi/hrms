<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="HRM_SummeryReport.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.HRM_SummeryReport" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type = "text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlreport.ClientID %>");
            var reportName = document.getElementById("<%=ddlReportName.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('<center><b>ASA Central Office</b><br/>');
            printWindow.document.write('<b>ASA Tower, Shyamoli Dhaka</b><br/>');
            printWindow.document.write(reportName.options[reportName.selectedIndex].text);
            printWindow.document.write('</center>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <table >
        <tr id="trReportName" runat="server">
            <td>
                Report Name</td>
            <td>
                <asp:DropDownList ID="ddlReportName" runat="server" Width="250px" 
                    AutoPostBack="True" onselectedindexchanged="ddlReportName_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select Report</asp:ListItem>
                    <asp:ListItem Value="1">At a Glance</asp:ListItem>
                    <asp:ListItem Value="2">Punishment Report</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvReportName" ControlToValidate="ddlReportName" Display="Dynamic" InitialValue="0" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td style="width: 4px">
                &nbsp;</td>
            <td>
                </td>
            
        </tr>
        <tr id="trYear" runat="server">
            <td>
                Year</td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" Width="100px">
                </asp:DropDownList>
            </td>
            <td style="width: 4px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr id="trMonth" runat="server">
            <td>
                Month</td>
            <td>
                <asp:DropDownList ID="ddlMonth" runat="server" Width="100px">
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
            <td style="width: 4px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr id="trAsonDate" runat="server">
            <td>
                As On date</td>
            <td>
                <asp:TextBox ID="txtAsOnDate" runat="server"></asp:TextBox>
            </td>
            <td style="width: 4px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:Panel ID="pnlreport" runat="server" GroupingText="Report">
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
