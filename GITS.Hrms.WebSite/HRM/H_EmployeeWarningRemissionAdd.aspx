<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeWarningRemissionAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeWarningRemissionAdd" %>
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
                        <td align="right">
                            Letter No</td>
                        <td>
                            <asp:TextBox ID="txtLetterNo" runat="server" AutoCompleteType="Disabled" ForeColor="ActiveCaptionText"
                                ReadOnly="true" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            Letter Date</td>
                        <td>
                            <asp:TextBox ID="txtLetterDate" runat="server" 
                                AutoCompleteType="Disabled" ForeColor="ActiveCaptionText"
                                ReadOnly="true" Enabled="False"></asp:TextBox>
                        </td>
                        <td align="right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td align="right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <strong>Warning Remission</strong>
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
                            Letter No:
                        </td>
                        <td>
                            <asp:TextBox ID="txtExemtedLetterNo" runat="server" Text="" MaxLength="200" 
                                Width="135px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" Display="Dynamic" ControlToValidate="txtExemtedLetterNo"
                                ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
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
                            Letter Date:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtExemtedLetterDate" Text="" MaxLength="10" 
                                Width="135px"></asp:TextBox>
                            <asp:ImageButton ID="ibLetterDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtExemtedLetterDate'));return false;">
                            </asp:ImageButton>
                            <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtExemtedLetterDate"
                                ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ControlToValidate="txtExemtedLetterDate" ID="rvLetterDate" Type="Date"
                                MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                                ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
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
                            Remarks:
                        </td>
                        <td>
                            <asp:TextBox ID="txtExemtedRemarks" runat="server" Width="135px"></asp:TextBox>
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
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                            <asp:HiddenField ID="hfPenalyId" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </div>
            <div style="width: 500px;">
                <asp:GridView ID="gvList" SkinID="Special" runat="server" 
                    onrowcommand="gvList_RowCommand">
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
                        <mms:BoundField DataField="Duration" HeaderText="Duration" FieldType="Int32" SortExpression="Duration">
                        </mms:BoundField>                   
                        <mms:BoundField DataField="Cause" HeaderText="Cause" FieldType="String" SortExpression="Cause">
                        </mms:BoundField>
                        <mms:BoundField DataField="Exempted" HeaderText="Exempted" FieldType="String" SortExpression="Exempted">
                        </mms:BoundField>
                        <mms:BoundField DataField="ExemptedLetterNo" HeaderText="Ex.Letter No" FieldType="String" SortExpression="ExemptedLetterNo">
                        </mms:BoundField>
                        <mms:BoundField DataField="ExemptedLetterDate" HeaderText="Ex.Letter Date" FieldType="DateTime"
                            SortExpression="ExemptedLetterDate" DataFormatString="{0:dd/MM/yyyy}">
                        </mms:BoundField> 
                        <mms:BoundField DataField="ExemptedRemarks" HeaderText="Remarks" FieldType="String" SortExpression="ExemptedRemarks">
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

