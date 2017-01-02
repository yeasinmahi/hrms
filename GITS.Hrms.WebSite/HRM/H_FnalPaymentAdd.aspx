<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_FnalPaymentAdd.aspx.cs"
    Inherits="GITS.Hrms.WebSite.HRM.H_FnalPaymentAdd" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1">
        <tr>
            <br />
        </tr>
        <tr>
            <td align="right">
                Employee:
            </td>
            <td colspan="5">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="lbSearch">
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
                Department:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDepartment" Text="" Enabled="false" Font-Bold="true"
                    Width="226px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                Grade:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtGrade" Text="" Enabled="false" Font-Bold="true"
                    ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                Designation:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDesignation" Text="" Enabled="false" Font-Bold="true"
                    Width="226px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                District:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtSubzone" Text="" Enabled="false" Font-Bold="true"
                    ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
            </td>
            <td align="right">
                Branch:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtBranch" Text="" Enabled="false" Font-Bold="true"
                    ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
            </td>
            <td align="right">
            </td>
            <td>
                <asp:HiddenField ID="hdnEmpId" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <br />
                <strong>Final Payment Information</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                Letter No:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtLetterNo" Text="" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" Display="Dynamic" ControlToValidate="txtLetterNo"
                    ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                Letter Date:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtLetterDate" Text="" MaxLength="10"></asp:TextBox>
                <asp:ImageButton ID="ibLetterDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtLetterDate'));return false;">
                </asp:ImageButton>
                <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                    ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                    MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                    ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
            </td>
            <td align="right">
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                Net pay:
            </td>
            <td>
                <asp:TextBox ID="txtNetPay" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNetPay" runat="server" Display="Dynamic" ControlToValidate="txtNetPay"
                    ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtNetPay" ID="rvNetPay" Type="Double" MaximumValue="9999999"
                    MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid Amount"></asp:RangeValidator>
            </td>
            <td align="right">
                Closing Date:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtClosingDate" Text="" MaxLength="10"></asp:TextBox>
                <asp:ImageButton ID="ibClosingDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtClosingDate'));return false;">
                </asp:ImageButton>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                    ControlToValidate="txtClosingDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtClosingDate" ID="RangeValidator1" Type="Date"
                    MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                    ErrorMessage="*" ToolTip="Invalid Closing date"></asp:RangeValidator>
            </td>
            <td align="right">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td colspan="3">
                &nbsp;
            </td>
            <td align="right">
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlList" runat="server" GroupingText="Final Payment Information">
        <asp:GridView ID="gvFinalPayment" runat="server">
            <Columns>
                <mms:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" FieldType="Int32">
                </mms:BoundField>
                <mms:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation" FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="Letter_No" HeaderText="Letter No" SortExpression="Letter_No"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="Letter_Date" HeaderText="Letter Date" SortExpression="Letter_Date"
                    DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime">
                </mms:BoundField>
                <mms:BoundField DataField="Closing_Date" HeaderText="Closing Date" SortExpression="Drop_Date"
                    DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime">
                </mms:BoundField>
                <mms:BoundField DataField="Net_Pay" HeaderText="Net Pay" SortExpression="Net_Pay"
                    FieldType="String">
                </mms:BoundField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
