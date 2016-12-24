<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="P_LateAttendanceAdd.aspx.cs" Inherits="P_LateAttendanceAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
    <div id="div1" runat="server">
        <div>
            <div style="float: left; width: auto;">
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <br />
                    </tr>
                    <tr>
                        <td align="right">
                            Employee:
                        </td>
                        <td colspan="3">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="lbSearch">
                                <asp:TextBox ID="txtEmployee" runat="server" MaxLength="250" autocomplete="off" Width="300px"
                                    onchange="javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbSearch', '')"></asp:TextBox>
                                <ajaxc:AutoCompleteExtender ID="aceEmployee" runat="server" TargetControlID="txtEmployee"
                                    ServicePath="~/Services/SearchService.asmx" ServiceMethod="GetSuggestions" MinimumPrefixLength="1"
                                    CompletionInterval="0" EnableCaching="true" CompletionSetCount="10" DelimiterCharacters=","
                                    CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="pnlExtenderList"
                                    OnClientShown="AdjustWidth">
                                </ajaxc:AutoCompleteExtender>
                                <asp:RequiredFieldValidator ID="rfvEmployeeName" runat="server" Display="Dynamic"
                                    ControlToValidate="txtEmployee" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                                <asp:LinkButton ID="lbSearch" runat="server" CausesValidation="False" OnClick="lbSearch_Click">Search</asp:LinkButton>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <strong>Current Information</strong>
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Designation
                        </td>
                        <td>
                            <asp:TextBox ID="txtDesignation" runat="server" ForeColor="ActiveCaptionText" ReadOnly="True"
                                Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right">
                            Status
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtStatus" runat="server" ForeColor="ActiveCaptionText" ReadOnly="True"
                                Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Branch
                        </td>
                        <td>
                            <asp:TextBox ID="txtBranch" runat="server" AutoCompleteType="Disabled" ForeColor="ActiveCaptionText"
                                ReadOnly="true" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right">
                            District
                        </td>
                        <td>
                            <asp:TextBox ID="txtDistrict" runat="server" ForeColor="ActiveCaptionText" ReadOnly="True"
                                Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <strong>Late Attendance Information</strong>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Year:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlYear" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvYear" runat="server" 
                                ControlToValidate="ddlYear" Display="Dynamic" ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Month:
                        </td>
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
                            <asp:RequiredFieldValidator ID="rfvMonth" runat="server" 
                                ControlToValidate="ddlMonth" Display="Dynamic" ErrorMessage="*" 
                                InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Late 9:06-9:30:
                        </td>
                        <td>
                            <asp:TextBox ID="txtLate96_930" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="cvLate96_930" runat="server" 
                                ControlToValidate="txtLate96_930" Display="Dynamic" ErrorMessage="*" 
                                Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Late 9:31-Days:
                        </td>
                        <td>
                            <asp:TextBox ID="txtLate931_days" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="cvLate931_Days" runat="server" 
                                ControlToValidate="txtLate931_days" Display="Dynamic" ErrorMessage="*" 
                                Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Absent:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAbsent" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="cvAbsent" runat="server" 
                                ControlToValidate="txtAbsent" Display="Dynamic" ErrorMessage="*" 
                                Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <table>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlExtenderList" ScrollBars="Vertical" Style="overflow: hidden;
                        height: 0px; width: 0px; z-index: 99999">
                    </asp:Panel>

                    <script type="text/javascript">
                        function AdjustWidth(source, eventArgs) {
                            document.getElementById('ctl00_ContentPlaceHolder1_pnlExtenderList').style.height = '200px';
                            document.getElementById('ctl00_ContentPlaceHolder1_pnlExtenderList').style.width = '700px';
                        }
                    </script>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
