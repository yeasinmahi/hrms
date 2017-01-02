<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_MonthlyReportAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_MonthlyReportAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table >
        <tr>
            <td>
                Year</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlYear" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Month</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlMonth" runat="server">
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
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <b>কর্মি সংক্রান্তঃ</b></td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
               নোট-১:</td>
            <td colspan="4">
                <asp:TextBox ID="txtNote1" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                শিক্ষা কর্মসূচীর কর্মীঃ
            </td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtEmpEduProgram" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtEmpEduProgram" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtEmpEduProgram" ID="rvEduProgram" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                নোট-২:</td>
            <td colspan="4">
                <asp:TextBox ID="txtNote2" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                আশা-স্বাস্থ্য কর্মসূচীর কর্মীঃ
            </td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtEmpAsaHealthProgram" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtEmpAsaHealthProgram" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtEmpAsaHealthProgram" ID="RangeValidator1" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                হবিগঞ্জ স্বাস্থ্য কর্মসূচীর কর্মীঃ 
                
            </td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtEmpHabigongHealthProgram" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtEmpHabigongHealthProgram" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtEmpHabigongHealthProgram" ID="RangeValidator2" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                নোট-৩:</td>
            <td colspan="4">
                <asp:TextBox ID="txtNote3" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <b>উদ্বৃত্ত কর্মী সংক্রান্তঃ</b></td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                জেলার সংখ্যা:</td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtExcessEmpDistrict" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtExcessEmpDistrict" ID="RangeValidator3" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                এলওঃ</td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtExcessEmpLO" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtExcessEmpLO" ID="RangeValidator4" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                এবিএমঃ</td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtExcessEmpABM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtExcessEmpABM" ID="RangeValidator5" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                বিএমঃ</td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtExcessEmpBM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtExcessEmpBM" ID="RangeValidator6" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                আরএমঃ</td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtExcessEmpRM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtExcessEmpRM" ID="RangeValidator7" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                ডিএমঃ</td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtExcessEmpDM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtExcessEmpDM" ID="RangeValidator8" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                জেডএমঃ</td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtExcessEmpZM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtExcessEmpZM" ID="RangeValidator9" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                অন্যান্য</td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtExcessEmpOthers" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtExcessEmpOthers" ID="RangeValidator10" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <b>কর্মী চাহিদা ও পুরণ সংক্রান্তঃ</b>তঃ</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                এলওঃ</td>
            <td align="right">
                চাহিদা</td>
            <td>
                <asp:TextBox ID="txtEmpDemandLO" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandLO" ID="RangeValidator11" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                চাহিদা পুরণ</td>
            <td>
                <asp:TextBox ID="txtEmpDemandFillLO" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandFillLO" ID="RangeValidator12" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                </td>
        </tr>
        <tr>
            <td align="right">
                এবিএমঃ</td>
            <td align="right">
                চাহিদা</td>
            <td>
                <asp:TextBox ID="txtEmpDemandABM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandABM" ID="RangeValidator13" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                চাহিদা পুরণ</td>
            <td>
                <asp:TextBox ID="txtEmpDemandFillABM" runat="server" CausesValidation="True"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandFillABM" ID="RangeValidator14" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                বিএমঃ</td>
            <td align="right">
                চাহিদা</td>
            <td>
                <asp:TextBox ID="txtEmpDemandBM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandBM" ID="RangeValidator15" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                চাহিদা পুরণ</td>
            <td>
                <asp:TextBox ID="txtEmpDemandFillBM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandFillBM" ID="RangeValidator16" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                আরএমঃ</td>
            <td align="right">
                চাহিদা</td>
            <td>
                <asp:TextBox ID="txtEmpDemandRM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandRM" ID="RangeValidator17" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                চাহিদা পুরণ</td>
            <td>
                <asp:TextBox ID="txtEmpDemandFillRM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandFillRM" ID="RangeValidator18" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                ডিএমঃ</td>
            <td align="right">
                চাহিদা</td>
            <td>
                <asp:TextBox ID="txtEmpDemandDM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandDM" ID="RangeValidator19" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                চাহিদা পুরণ</td>
            <td>
                <asp:TextBox ID="txtEmpDemandFillDM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandFillDM" ID="RangeValidator20" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                জেডএমঃ</td>
            <td align="right">
                চাহিদা</td>
            <td>
                <asp:TextBox ID="txtEmpDemandZM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandZM" ID="RangeValidator21" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                চাহিদা পুরণ</td>
            <td>
                <asp:TextBox ID="txtEmpDemandFillZM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtEmpDemandFillZM" ID="RangeValidator22" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <b>বদলী সংক্রান্ত নোট</b></td>
            <td colspan="4">
                <asp:TextBox ID="txtTransferNote" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                কম সময়ে বদলী নোট</td>
            <td colspan="4">
                <asp:TextBox ID="txtLessTimeTransferNote" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                ব<b>বদলীর আবেদন নিষ্পত্তি করণ সংক্রান্ত</b></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                এলওঃ</td>
            <td>
                আবেদন এসেছে</td>
            <td>
                <asp:TextBox ID="txtTransAppLO" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppLO" ID="RangeValidator23" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                নিষ্পত্তি হয়েছে</td>
            <td>
                <asp:TextBox ID="txtTransAppSettleLO" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppSettleLO" ID="RangeValidator24" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                এবিএমঃ</td>
            <td>
                আবেদন এসেছে</td>
            <td>
                <asp:TextBox ID="txtTransAppABM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppABM" ID="RangeValidator25" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                নিষ্পততি হয়েছে</td>
            <td>
                <asp:TextBox ID="txtTransAppSettleABM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppSettleABM" ID="RangeValidator26" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                বিএমঃ</td>
            <td>
                আবেদন এসেছে</td>
            <td>
                <asp:TextBox ID="txtTransAppBM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppBM" ID="RangeValidator27" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                নিষ্পত্তি হয়েছে</td>
            <td>
                <asp:TextBox ID="txtTransAppSettleBM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppSettleBM" ID="RangeValidator28" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                সিও/এএসইঃ</td>
            <td>
                আবেদন এসেছে</td>
            <td>
                <asp:TextBox ID="txtTransAppCO_ASE" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppCO_ASE" ID="RangeValidator29" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                নিষ্পত্তি হয়েছে</td>
            <td>
                <asp:TextBox ID="txtTransAppSettleCO" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppSettleCO" ID="RangeValidator30" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
               আরএম/ফিল্ড অডিটরঃ</td>
            <td>
                আবেদন এসেছে</td>
            <td>
                <asp:TextBox ID="txtTransAppRM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppRM" ID="RangeValidator31" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                নিষ্পত্তি হয়েছে</td>
            <td>
                <asp:TextBox ID="txtTransAppSettleRM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppSettleRM" ID="RangeValidator32" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                ডিএম/সি.ফিল্ড অডিটরঃ</td>
            <td>
                আবেদন এসেছে</td>
            <td>
                <asp:TextBox ID="txtTransAppDM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppDM" ID="RangeValidator33" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                নিষ্পত্তি হয়েছে</td>
            <td>
                <asp:TextBox ID="txtTransAppSettleDM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppSettleDM" ID="RangeValidator34" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                জেডএম/জেডএঃ</td>
            <td>
                আবেদন এসেছে</td>
            <td>
                <asp:TextBox ID="txtTransAppZM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppZM" ID="RangeValidator35" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                নিষ্পত্তি হয়েছে</td>
            <td>
                <asp:TextBox ID="txtTransAppSettleZM" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTransAppSettleZM" ID="RangeValidator36" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 18px">
                </td>
            <td style="height: 18px">
                </td>
            <td style="height: 18px">
                </td>
            <td style="height: 18px">
                </td>
            <td style="height: 18px">
                </td>
            <td style="height: 18px">
                </td>
        </tr>
        <tr>
            <td>
                <b>হেলথ কমপ্লেক্সে নিয়োগ</b></td>
            <td align="right">
                চলতি মাস</td>
            <td>
                <asp:TextBox ID="txtHealthComplexRecruit" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtHealthComplexRecruit" ID="RangeValidator40" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <b>প্রিন্টিং সংক্রান্ত</b></td>
            <td colspan="4">
                <asp:TextBox ID="txtPrintingNote" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <b>টেলিফোন/বিদ্যুৎ/মোবাইল বিল সংক্রান্ত</b></td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                টেলিফোন</td>
            <td colspan="2">
                <asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtTelephone" ID="RangeValidator37" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                বিদ্যুৎ</td>
            <td colspan="2">
                <asp:TextBox ID="txtElectricity" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtElectricity" ID="RangeValidator38" Type="Integer" MaximumValue="99999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                মোবাইল</td>
            <td colspan="2">
                <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtMobile" ID="RangeValidator39" Type="Integer" MaximumValue="999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <b>হাজিরা সংক্রান্ত নোট</b>
                </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                ক)</td>
            <td colspan="4">
                <asp:TextBox ID="txtAttendanceNoteKa" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                খ)</td>
            <td colspan="4">
                <asp:TextBox ID="txtAttendanceNoteKha" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                গ)</td>
            <td colspan="4">
                <asp:TextBox ID="txtAttendanceNoteGa" runat="server" AutoPostBack="True" 
                    Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                এইচআর থেকে গৃহিত প্রশাসনিক ব্যবস্থা সমুহঃ</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <b>বদলী সংক্রান্তঃ</b></td>
            <td colspan="4">
                <asp:TextBox ID="txtAdministrativeStepsTransfer" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="height: 20px" align="right">
                <b>ড্রপ-আউট সংক্রান্তঃ</b></td>
            <td colspan="4" style="height: 20px">
                <asp:TextBox ID="txtAdministrativeStepsDropOut" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td style="height: 20px">
                </td>
        </tr>
        <tr>
            <td align="right">
                বিভিন্ন ধরনের শাস্তি প্রদানঃ</td>
            <td colspan="4">
                <asp:TextBox ID="txtAdministrativeStepsPunishment" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <b>চিকিৎসা সংক্রান্তঃ</b></td>
            <td colspan="4">
                <asp:TextBox ID="txtAdministrativeStepsTreatment" runat="server" Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
               মাঠ ও কেন্দ্রীয় পর্যায়ে বিভিন্ন পদে নিয়োগ পরীক্ষা সংক্রান্তঃ</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="4">
                <asp:TextBox ID="txtRecruitmentNote" runat="server" AutoPostBack="True" 
                    Width="400px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
