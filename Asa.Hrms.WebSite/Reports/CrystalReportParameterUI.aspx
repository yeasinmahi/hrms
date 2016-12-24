<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="CrystalReportParameterUI.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.CrystalReportParameterUI" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table >
        <tr id="trAsonDate" runat="server">
            <td>
                As On Date</td>
            <td>
                <asp:TextBox ID="txtAsOnDate" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAsOnDate" runat="server" Display="Dynamic" ControlToValidate="txtAsOnDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtAsOnDate" ID="rvAsOnDate" Type="Date" 
                    MaximumValue="31/12/9999" MinimumValue="1/1/1990" runat="server" 
                    Display="Dynamic" ErrorMessage="*" ToolTip="Invalid As On date" 
                    oninit="rvAsOnDate_Init"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trStartDate" runat="server">
            <td>
                Start date</td>
            <td>
                <asp:TextBox ID="StartDate" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trEndDate" runat="server">
            <td>
                End date</td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trPunishmentType" runat="server">
            <td>
                Report Type</td>
            <td>
                <asp:DropDownList ID="ddtPunishment" runat="server">
                    <asp:ListItem Value="1">Penalty</asp:ListItem>
                    <asp:ListItem Value="2">Increament Heldup</asp:ListItem>
                    <asp:ListItem Value="3">Warning</asp:ListItem>
                    <asp:ListItem Value="4">Leave</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trEmID" runat="server">
            <td>
                Emp ID</td>
            <td>
                <asp:TextBox ID="txtEmpIdD" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvID" ControlToValidate="txtEmpIdD" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvID" ControlToValidate="txtEmpIdD" Type="Integer" MinimumValue="1" MaximumValue="99999" runat="server" ErrorMessage="*"></asp:RangeValidator>
            </td>
            <td>
                </td>
        </tr>
        <tr id="trBranch" runat="server">
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblBranchReport" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
         <tr id="trYear" runat="server">
            <td>
                Year</td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" Width="100px">
                </asp:DropDownList>
            </td>
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
            <td>
                &nbsp;</td>
        </tr>
        <tr id="trTransferType" runat="server">
            <td>
                Report Type</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlReportName" runat="server" DataTextField="Name" 
                    DataValueField="Id" Width="405px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvReportName" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlReportName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr id="trLetterNo" runat="server">
            <td>
                Letter No</td>
            <td>
                <asp:TextBox ID="txtLetterNo" runat="server"></asp:TextBox>
            </td>
            <td rowspan="5">অনুলিপিঃ <br />
                <asp:CheckBoxList ID="chkDuplication" runat="server" RepeatLayout="Flow">
                    <asp:ListItem Value="1">১। সংশ্লিষ্ট ইভিপি/কেন্দ্রীয় বাস্তবায়নকারী (অপারেশন)।</asp:ListItem>
                    <asp:ListItem Value="2">২। সংশ্লিষ্ট জেডএম, আশা।</asp:ListItem>
                    <asp:ListItem Value="3">৩। সংশ্লিষ্ট ডিএম, আশা।</asp:ListItem>
                    <asp:ListItem Value="4">৪। সংশ্লিষ্ট ব্রাঞ্চ, আশা।</asp:ListItem>
                    <asp:ListItem Value="5">৫। পিএমআইএস।</asp:ListItem>
                    <asp:ListItem Value="6">৬। ব্যক্তিগত নথি।</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr id="trNote" runat="server">
            <td>
                Note</td>
            <td>
                <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr id="trDMNote" runat="server">
            <td>
                Special Note</td>
            <td>
                <asp:TextBox ID="txtDMNote" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Report Type</td>
            <td>
                <asp:DropDownList ID="ddlFormat" runat="server" Width="100px">
                    <asp:ListItem Value="1">PDF</asp:ListItem>
                    <asp:ListItem Value="2">MS Word</asp:ListItem>
                    <asp:ListItem Value="3">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
       
        <tr style="height:400px;">
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
       
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
