<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeWarningAdd.aspx.cs"
    Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeWarningAdd" MasterPageFile="~/Site.master"
    Title="" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
    
    <div id="div1" style="float:left;" runat="server">
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
                <td colspan="3">
                <asp:Panel ID="Panel1" runat="server"  DefaultButton="lbSearch">
                    <asp:TextBox ID="txtEmployee" runat="server" MaxLength="250" autocomplete="off" Width="300px"
                        
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
                <td align="left" colspan="4">
                    <strong>Current Information:</strong></td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    Designation</td>
                <td><asp:TextBox ID="txtDesignation" runat="server" ForeColor="ActiveCaptionText" 
                    ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
                <td align="right">Status</td>
                <td>
    <asp:TextBox ID="txtStatus" runat="server" ForeColor="ActiveCaptionText" 
        ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Branch</td>
                <td>
    <asp:TextBox ID="txtBranch" runat="server" AutoCompleteType="Disabled" 
        ForeColor="ActiveCaptionText" ReadOnly="true" Enabled="False" ></asp:TextBox>
                </td>
                <td align="right">
                    District</td>
                <td>
    <asp:TextBox ID="txtDistrict" runat="server" ForeColor="ActiveCaptionText" 
        ReadOnly="True" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4">
                    <strong>Warning Information:</strong></td>
            </tr>
            <tr>
                <td align="right">
                    Warning
                    Type:</td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlWarningType" runat="server">
                        <asp:ListItem Value="0">Select Type</asp:ListItem>
                        <asp:ListItem Value="Normal">Normal</asp:ListItem>
                        <asp:ListItem Value="Punishment">Punishment</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvType" runat="server" Display="Dynamic" ControlToValidate="ddlWarningType" InitialValue="0"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Letter No:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtLetterNo" runat="server" Text="" MaxLength="200" Width="135px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" Display="Dynamic" ControlToValidate="txtLetterNo"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Letter Date:
                </td>
                <td colspan="3">
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
                    District:
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlSubzone" runat="server" DataTextField="Name" DataValueField="Id"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSubzone_SelectedIndexChanged" Width="135px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Region:
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Name" DataValueField="Id"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" Width="135px">
                    </asp:DropDownList>
                    <asp:HiddenField ID="hfWarningId" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Branch:
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="Name" DataValueField="Id"
                        Width="135px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Cause:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtCause" runat="server" Text="" MaxLength="300" Width="300px"></asp:TextBox>
                </td>
            </tr>
        </table>
        
        </div>
        <div><b>Warning List</b></div>
        <div style="width:400px;">
            <asp:GridView ID="gvWarning" SkinID="Special" runat="server" 
                onrowcommand="gvWarning_RowCommand">
                <Columns>
                    <mms:BoundField DataField="LetterNo" HeaderText="Letter No." FieldType="String" SortExpression="LetterNo">
                    </mms:BoundField>
                    <mms:BoundField DataField="LetterDate" HeaderText="Letter Date" FieldType="DateTime"
                        SortExpression="LetterDate" DataFormatString="{0:dd/MM/yyyy}">
                    </mms:BoundField>
                    <mms:BoundField DataField="Duration" HeaderText="Duration" FieldType="String" >
                    </mms:BoundField>
                    <mms:BoundField DataField="TotalWarningTime" HeaderText="Warning Time" FieldType="Int32" SortExpression="TotalWarningTime">
                    </mms:BoundField>
                    <mms:BoundField DataField="Branch" HeaderText="Branch" FieldType="String" SortExpression="Branch">
                    </mms:BoundField>
                    <mms:BoundField DataField="Cause" HeaderText="Cause" FieldType="String" SortExpression="Cause">
                    </mms:BoundField>
                    <mms:BoundField DataField="WarningType" HeaderText="Warning Type" FieldType="String" SortExpression="WarningType">
                    </mms:BoundField>
                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                            <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Id") %>'
                                    CausesValidation="false" CommandName="editrow" Text="Edit"></asp:LinkButton> | 
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Id") %>'
                                    CausesValidation="false" CommandName="deleterow" Text="Delete" OnClientClick="if (!confirm('Are you Sure you want to delete this row ?')) { return false; }"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
