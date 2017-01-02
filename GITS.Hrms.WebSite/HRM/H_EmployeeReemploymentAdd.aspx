<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeReemploymentAdd.aspx.cs"
    Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeReemploymentAdd" MasterPageFile="~/Site.master"
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
                <td colspan="5"><asp:Panel ID="Panel1" runat="server" DefaultButton="lbSearch">
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
                    
                    </asp:Panel>
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
                    Zone:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtZone" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                    Subzone:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSubzone" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                    Region:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtRegion" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    Branch:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBranch" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="135px"></asp:TextBox>&nbsp;
                    <asp:TextBox runat="server" ID="txtBranchDate" Text="" Enabled="false" Font-Bold="true"
                        Width="70px" ForeColor="ActiveCaptionText"></asp:TextBox>
                    <input id="hdnBranch" runat="server" type="hidden" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Drop-out Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDropoutType" runat="server" Enabled="false" Width="200px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Text="" MaxLength="10" Enabled="false"
                        Font-Bold="true" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <strong>Re-employment Information</strong>
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
                    Re-employment<br />
                    Date:
                </td>
                <td>
                    <asp:TextBox ID="txtReemploymentDate" runat="server" Text="" MaxLength="10" Width="135px"></asp:TextBox>
                    <asp:ImageButton ID="ibReemploymentDate" runat="server" SkinID="CalendarImageButton"
                        OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtReemploymentDate'));return false;">
                    </asp:ImageButton>
                    <asp:RequiredFieldValidator ID="rfvReemploymentDate" runat="server" Display="Dynamic"
                        ControlToValidate="txtReemploymentDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtReemploymentDate" ID="rvReemploymentDate"
                        Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server"
                        Display="Dynamic" ErrorMessage="*" ToolTip="Invalid re-employment date"></asp:RangeValidator>
                    <asp:CompareValidator ID="cvReemploymentDate" Type="Date" Operator="GreaterThan"
                        ControlToCompare="txtFromDate" ControlToValidate="txtReemploymentDate" Display="Dynamic"
                        runat="server" ErrorMessage="*" ToolTip="Re-employment datae should greater than previous drop-out's from date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Zone:
                </td>
                <td>
                    <asp:DropDownList ID="ddlZone" runat="server" DataTextField="Name" DataValueField="Id"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" Width="135px">
                    </asp:DropDownList>
                </td>
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
            </tr>
            <tr>
                <td align="right" style="font-size: smaller;">
                    Re-employment<br />
                    Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddlReemploymentType" runat="server" Width="135px">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Cause:
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtCause" runat="server" Text="" MaxLength="300" Width="400px"></asp:TextBox>
                </td>
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
