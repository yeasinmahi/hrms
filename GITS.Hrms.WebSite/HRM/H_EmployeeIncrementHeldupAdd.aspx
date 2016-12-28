<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeIncrementHeldupAdd.aspx.cs"
    Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeIncrementHeldupAdd" MasterPageFile="~/Site.master"
    Title="" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
    <div id="div1" runat="server">
    <div>
        <div style=" float:left; width:auto;">
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <br />
            </tr>
            <tr>
                <td align="right">
                    Employee:
                </td>
                <td colspan="5">
                <asp:Panel ID="Panel1" runat="server"  DefaultButton="lbSearch">
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
                <td align="right">
                    Letter No:
                </td>
                <td>
                    <asp:TextBox ID="txtLetterNo" runat="server" Text="" MaxLength="200" Width="135px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" Display="Dynamic" ControlToValidate="txtLetterNo"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
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
            </tr>
            <tr>
                <td align="right">
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Text="" MaxLength="10" Width="135px"></asp:TextBox>
                    <asp:ImageButton ID="ibFromDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtFromDate'));return false;">
                    </asp:ImageButton>
                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtFromDate" ID="rvFromDate" Type="Date" MaximumValue="31/12/9999"
                        MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid from date"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    To Date:
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Text="" MaxLength="10" Width="135px"></asp:TextBox>
                    <asp:ImageButton ID="ibToDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtToDate'));return false;">
                    </asp:ImageButton>
                    <asp:RangeValidator ControlToValidate="txtToDate" ID="rvToDate" Type="Date" MaximumValue="31/12/9999"
                        MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid to date"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Increment Stop:
                </td>
                <td>
                    <asp:TextBox ID="txtIncrementStop" runat="server" Text="" Width="135px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvIncrementStop" runat="server" Display="Dynamic"
                        ControlToValidate="txtIncrementStop" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtIncrementStop" ID="rvIncrementStop" Type="Integer"
                        MaximumValue="9999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*"
                        ToolTip="Only integer values are allowed"></asp:RangeValidator>
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
            </tr>
            <tr>
                <td align="right">
                    District:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubzone" runat="server" DataTextField="Name" DataValueField="Id"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSubzone_SelectedIndexChanged" Width="135px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Region:
                </td>
                <td>
                    <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Name" DataValueField="Id"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" Width="135px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
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
                <td align="right">
                    Cause:
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtCause" runat="server" Text="" MaxLength="300" Width="380px"></asp:TextBox>
                </td>
            </tr>
        </table>
        </div>
        <div><b>Increment Heldup List</b></div>
        <div style="width:400px;">
            <asp:GridView ID="gvIncrement" runat="server">
                <Columns>
                    <mms:BoundField DataField="LetterNo" HeaderText="Letter No." FieldType="String" SortExpression="LetterNo">
                    </mms:BoundField>
                    <mms:BoundField DataField="LetterDate" HeaderText="Letter Date" FieldType="DateTime"
                        SortExpression="LetterDate" DataFormatString="{0:dd/MM/yyyy}">
                    </mms:BoundField>
                    <mms:BoundField DataField="FromDate" HeaderText="From Date" FieldType="DateTime"
                        SortExpression="FromDate" DataFormatString="{0:dd/MM/yyyy}">
                    </mms:BoundField>
                    <mms:BoundField DataField="ToDate" HeaderText="To Date" FieldType="DateTime"
                        SortExpression="ToDate" DataFormatString="{0:dd/MM/yyyy}">
                    </mms:BoundField>
                    <mms:BoundField DataField="Branch" HeaderText="Branch" FieldType="String" SortExpression="Branch">
                    </mms:BoundField>
                    
                </Columns>
            </asp:GridView>
        </div>
     </div>
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
