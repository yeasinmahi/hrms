<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="WF_WelfareFundAssistanceAdd.aspx.cs" Inherits="WF_WelfareFundAssistanceAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                    <strong>Employee Information</strong>
                </td>
                <td align="right">
                    &nbsp;
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    Department:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDepartment" Text="" Enabled="false" Font-Bold="true"
                        Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
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
                    Zone:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtZone" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>
                    &nbsp;
                </td>
                <td align="right">
                    District:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSubzone" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText"></asp:TextBox>
                    &nbsp;
                </td>
                <td align="right">
                    Region:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtRegion" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Branch
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBranch" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>
                    <input id="hdnBranch" runat="server" type="hidden" />
                </td>
                <td align="right">
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="right">
                    &nbsp;
                </td>
                <td align="right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <strong>&nbsp;Welfare Fund Assistance Information</strong>
                </td>
                <td align="right">
                    &nbsp;
                </td>
                <td align="right">
                    &nbsp;
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
                    Fund Type</td>
                <td>
                    <asp:DropDownList ID="ddlFundType" runat="server" Width="150px">
                        <asp:ListItem Value="0">Select Fund Type</asp:ListItem>
                        <asp:ListItem Value="1">পরিবার কল্যান তহবিল</asp:ListItem>
                        <asp:ListItem Value="2"> কর্মি কল্যান তহবিল</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvFundType" runat="server" Display="Dynamic" InitialValue="0" ControlToValidate="ddlFundType"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Amount:
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAmount" runat="server" Display="Dynamic" ControlToValidate="txtAmount"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtAmount" ID="rvAmount" Type="Integer" MaximumValue="9999999"
                        MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                </td>
                <td align="right">
                    Diseases:
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlDiseases" runat="server" Width="450px" DataTextField="Name"
                        DataValueField="Id">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDiseases" runat="server" Display="Dynamic" ControlToValidate="ddlDiseases"
                        InitialValue="0" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Remarks:
                </td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txtRemarks" Text="" MaxLength="300" Width="400px"></asp:TextBox>
                </td>
                <td align="right">
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                </td>
                <td colspan="3">
                    &nbsp;
                </td>
                <td align="right">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlWelfare" runat="server">
            <asp:GridView ID="gvList" runat="server">
                <Columns>
                    <mms:BoundField DataField="Letter_No" HeaderText="Letter_No" FieldType="String">
                    </mms:BoundField>
                    <mms:BoundField DataField="Letter_Date" HeaderText="Letter_Date" FieldType="String">
                    </mms:BoundField>
                    <mms:BoundField DataField="Amount" HeaderText="Amount" FieldType="String">
                    </mms:BoundField>
                    <mms:BoundField DataField="Branch_Name" HeaderText="Branch_Name" FieldType="String">
                    </mms:BoundField>
                    <mms:BoundField DataField="FundType" HeaderText="Fund Type" FieldType="String">
                    </mms:BoundField>
                    <mms:BoundField DataField="DiseaseName" HeaderText="Disease Name" FieldType="String">
                    </mms:BoundField>
                    <mms:BoundField DataField="Remarks" HeaderText="Remarks" FieldType="String">
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
    </div>
</asp:Content>
