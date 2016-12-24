<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_SalaryProcess.aspx.cs" Inherits="Asa.Hrms.WebSite.Payroll.P_SalaryProcess" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../Script/jquery-1.11.2.js" type="text/javascript"></script>
    <link href="../Script/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Script/jquery-ui.js" type="text/javascript"></script>
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
    <tr>
    <td>Process End</td>
    <td>
        <asp:CheckBox ID="chkProcessEnd" runat="server" Text="Yes" />
        </td>
    </tr>
</table>
<div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Button ID="btnStart" runat="server" Text="Execute Process" 
            onclick="btnStart_Click" />
            <br />
        </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div id="progressbar" style="width: 400px; height:20px; border-style:solid; border-color:Blue; border-width:1px; text-align:center;">
        </div>
        <div id="percent">
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <asp:Label ID="percentage" runat="server" Text=""></asp:Label>
        
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        $(document).ready(function() {
            // TODO: revert the line below in your actual code
            $("#progressbar").progressbar();
            setTimeout(updateProgress, 0);
        });

        function updateProgress() {
            $.ajax({
                type: "POST",
                url: "P_SalaryProcess.aspx/GetData",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function(msg) {
                    // TODO: revert the line below in your actual code
                    //$("#progressbar").progressbar("option", "value", msg.d);
                    //                    $("#percentage").text(msg.d + '% Complete');
                if (msg.d <= 100) {
                    

                        var interval = setInterval(function() {
                            var myPer = msg.d;
                            var val = msg.d;
                            if (Number(val) == 0) {
                                $("#progressbar").hide();
                                $("#percentage").value=('');
                            }
                            else {
                                $("#progressbar").show();
                            }
                            $("#progressbar").progressbar({ value: val })
                                             .children('.ui-progressbar-value')
                                             .html(myPer.toPrecision(3) + '%')
                                             .css("display", "block");
                            //$("#percentage").text(val + '% Complete');                           
                            if (val == 100) {
                                clearInterval(interval);
                                $("#percentage").text('Process Completed Successfully');
                            }
                        });
                        setTimeout(updateProgress, 100);
                    }
                }
            });
        }
    </script>
</asp:Content>

