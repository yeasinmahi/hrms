<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeIncrementHeldupRemissionAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeIncrementHeldupRemissionAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <td>
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
                    Letter No</td>
                <td>
                    <asp:TextBox ID="txtLetterNo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Letter Date</td>
                <td>
                    <asp:TextBox ID="txtLetterDate" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    From Date</td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    No. of Increment Stop</td>
                <td>
                    <asp:TextBox ID="txtIncrementStop" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    Exemption
                    Letter No:
                </td>
                <td>
                    <asp:TextBox ID="txtExLetterNo" runat="server" Text="" MaxLength="200" 
                        Width="135px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" Display="Dynamic" ControlToValidate="txtExLetterNo"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Exemption
                    Letter Date:
                </td>
                <td>
                    <asp:TextBox ID="txtExLetterDate" runat="server" Text="" MaxLength="10" 
                        Width="135px" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:ImageButton ID="ibLetterDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtExLetterDate'));return false;">
                    </asp:ImageButton>
                    <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtExLetterDate"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtExLetterDate" ID="rvLetterDate" Type="Date"
                        MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                        ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Exemption Date:
                </td>
                <td>
                    <asp:TextBox ID="txtExemptionDate" runat="server" Text="" MaxLength="10" 
                        Width="135px"></asp:TextBox>
                    <asp:ImageButton ID="ibFromDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtExemptionDate'));return false;">
                    </asp:ImageButton>
                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" Display="Dynamic" ControlToValidate="txtExemptionDate"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtExemptionDate" ID="rvFromDate" Type="Date" MaximumValue="31/12/9999"
                        MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid from date"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    No. of Increment Exempted:
                </td>
                <td>
                    <asp:TextBox ID="txtExemptedInc" runat="server" Text="" Width="135px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvIncrementStop" runat="server" Display="Dynamic"
                        ControlToValidate="txtExemptedInc" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtExemptedInc" ID="rvIncrementStop" Type="Integer"
                        MaximumValue="9" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*"
                        ToolTip="Only integer values are allowed"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Remarks:
                </td>
                <td>
                    <asp:TextBox ID="txtRmarks" runat="server" Text="" MaxLength="250" 
                        Width="380px"></asp:TextBox>
                </td>
            </tr>
        </table>
        </div>
        <div><b>Increment Heldup List</b></div>
        <div style="width:400px;">
            <asp:GridView ID="gvIncrement" runat="server" 
                onrowcommand="gvIncrement_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="Letter No." ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkLetterNo" runat="server" CommandArgument='<%# Eval("Id") %>'
                                    CausesValidation="false" CommandName="preview" Text='<%# Eval("LetterNo") %>'></asp:LinkButton>
                            </ItemTemplate>
                    </asp:TemplateField>
                   
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
                    <mms:BoundField DataField="ExemptionLetterNo" HeaderText="ExemptionLetterNo" FieldType="String" SortExpression="ExemptionLetterNo">
                    </mms:BoundField>
                    <mms:BoundField DataField="ExemptionLetterDate" HeaderText="Ex.Letter Date" FieldType="DateTime"
                        SortExpression="ExemptionLetterDate" DataFormatString="{0:dd/MM/yyyy}">
                    </mms:BoundField>
                    <mms:BoundField DataField="ExemptionDate" HeaderText="ExemptionDate" FieldType="DateTime"
                        SortExpression="ExemptionDate" DataFormatString="{0:dd/MM/yyyy}">
                    </mms:BoundField>
                    <mms:BoundField DataField="IncrementExempted" HeaderText="Rem.Inc" FieldType="Int32" SortExpression="IncrementExempted">
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

