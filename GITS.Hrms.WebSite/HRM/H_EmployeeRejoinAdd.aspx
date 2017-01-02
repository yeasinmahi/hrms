<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeRejoinAdd.aspx.cs"
    Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeRejoinAdd" MasterPageFile="~/Site.master"
    Title="" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
    <div id="div1" runat="server">
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <br />
            </tr>
            <tr>
                <td align="right">
                    Employee:
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtEmployee" runat="server" MaxLength="250" autocomplete="off" Width="380px"
                        onchange="javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbSearch', '')"></asp:TextBox>
                    <ajaxc:AutoCompleteExtender ID="aceEmployee" runat="server" TargetControlID="txtEmployee"
                        ServicePath="~/Services/SearchService.asmx" ServiceMethod="GetSuggestions" MinimumPrefixLength="1"
                        CompletionInterval="0" EnableCaching="true" CompletionSetCount="10" DelimiterCharacters=","
                        CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="pnlExtenderList"
                        OnClientShown="AdjustWidth">
                    </ajaxc:AutoCompleteExtender>
                    <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" Display="Dynamic" ControlToValidate="txtEmployee"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:LinkButton ID="lbSearch" runat="server" CausesValidation="False" OnClick="lbSearch_Click">Search</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <strong>Current Information</strong>
                </td>
            </tr>
            <tr>
                <td align="right">
                    District:
                </td>
                <td>
                    &nbsp;<asp:TextBox runat="server" ID="txtSourceSubzone" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>
                </td>
                <td align="right">
                    Region:
                </td>
                <td>
                    &nbsp;<asp:TextBox runat="server" ID="txtSourceRegion" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    Branch:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSourceBranch" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="135px"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txtSourceBranchDate" Text="" Enabled="false" Font-Bold="true"
                        Width="75px" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    &nbsp;</td>
                <td>
                    &nbsp;
                    <input id="hdnBranch" runat="server" type="hidden" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Leave Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddlLeaveType" runat="server" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtLeaveType" runat="server" Enabled="False" 
                        ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Text="" MaxLength="10" Enabled="false"
                        Font-Bold="true" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    To Date:
                </td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" Text="" MaxLength="10" Enabled="false"
                        Font-Bold="true" ForeColor="ActiveCaptionText" Width="135px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <strong>Re-join Information</strong>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Letter No:
                </td>
                <td>
                    <asp:TextBox ID="txtLetterNo" runat="server" Text="" MaxLength="200" Width="135px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" Display="Dynamic" ControlToValidate="txtLetterNo"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                </td>
                <td align="right">
                    Letter Date:
                </td>
                <td>
                    <asp:TextBox ID="txtLetterDate" runat="server" Text="" MaxLength="10" Width="135px"></asp:TextBox>
                    <asp:ImageButton ID="ibLetterDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtLetterDate'));return false;">
                    </asp:ImageButton>
                    <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                        MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                        ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                </td>
                <td align="right" style="font-size: smaller">
                    Re-join<br />
                    Date:
                </td>
                <td>
                    <asp:TextBox ID="txtRejoinDate" runat="server" Text="" MaxLength="10" Width="135px"></asp:TextBox>
                    <asp:ImageButton ID="ibRejoinDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtRejoinDate'));return false;">
                    </asp:ImageButton>
                    <asp:RequiredFieldValidator ID="rfvRejoinDate" runat="server" Display="Dynamic" ControlToValidate="txtRejoinDate"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtRejoinDate" ID="rvRejoinDate" Type="Date"
                        MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                        ErrorMessage="*" ToolTip="Invalid re-join date"></asp:RangeValidator>
                    <asp:CompareValidator ID="cvRejoinDate" Type="Date" Operator="GreaterThan" ControlToCompare="txtFromDate"
                        ControlToValidate="txtRejoinDate" Display="Dynamic" runat="server" ErrorMessage="*"
                        ToolTip="Re-join date should greater than previous leave's from date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    ASA District:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubzone" runat="server" DataTextField="Name" DataValueField="Id"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSubzone_SelectedIndexChanged" Width="135px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Region:
                </td>
                <td>
                    <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Name" DataValueField="Id"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" Width="135px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Branch:
                </td>
                <td>
                    <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="Name" DataValueField="Id"
                        Width="135px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" style="font-size: smaller;">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlExtenderList" ScrollBars="Vertical" Style="overflow: hidden;
                        height: 0px; width: 0px; z-index: 99999">
                    </asp:Panel>

                    <script type="text/javascript">
    function AdjustWidth(source, eventArgs)
    {
      document.getElementById('ctl00_ContentPlaceHolder1_pnlExtenderList').style.height = '200px';
      document.getElementById('ctl00_ContentPlaceHolder1_pnlExtenderList').style.width = '700px';
    }
                    </script>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
