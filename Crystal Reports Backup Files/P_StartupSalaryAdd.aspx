<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_StartupSalaryAdd.aspx.cs" Inherits="Asa.Hrms.WebSite.Payroll.P_StartupSalaryAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />

    <div id="div1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1">
        <tr><br /></tr>
        <tr>
            <td align="right">
                Employee:
            </td>
            <td colspan="5">
                <asp:Panel ID="Panel1" runat="server"  DefaultButton="lbSearch">
                <asp:TextBox ID="txtEmployee" runat="server" MaxLength="250" autocomplete="off" Width="380px" onchange="javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbSearch', '')"></asp:TextBox>
                <ajaxc:AutoCompleteExtender ID="aceEmployee" runat="server" TargetControlID="txtEmployee"
                    ServicePath="~/Services/SearchService.asmx" ServiceMethod="GetSuggestions"
                    MinimumPrefixLength="1" CompletionInterval="0" EnableCaching="true" CompletionSetCount="10"
                    DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="pnlExtenderList"
                    OnClientShown="AdjustWidth">
                </ajaxc:AutoCompleteExtender>
                <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" Display="Dynamic" ControlToValidate="txtEmployee" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:LinkButton ID="lbSearch" runat="server" CausesValidation="False" onclick="lbSearch_Click">Search</asp:LinkButton>
                </asp:Panel>
          </td>
        </tr>
        <tr>
            <td colspan="4"><br />
                <strong>Current Information</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                Department:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDepartment" Text="" Enabled="false" Font-Bold="true" Width="226px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                Grade:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtGrade" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                Designation:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDesignation" Text="" Enabled="false" Font-Bold="true" Width="226px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td align="right">
                </td>
            <td>
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td align="right">
                Joining Date :</td>
            <td>
                <asp:TextBox ID="txtLastIncrementDate" runat="server" 
                    AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToValidate="txtLastIncrementDate" Display="Dynamic" ErrorMessage="*" 
                    Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
            </td>
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
            <td align="right">
                Last Basic/Cosolidate :</td>
            <td>
                <asp:TextBox ID="txtLastBasic" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtLastBasic" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*"></asp:CompareValidator>
            </td>
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
            <td align="right">
                Present Basic/Consolidate :</td>
            <td>
                <asp:TextBox ID="txtPresentBasic" runat="server"></asp:TextBox>
            </td>
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
            <td align="right">
                Status :</td>
            <td>
                <asp:CheckBox ID="chkStatus" runat="server" Text="Active" />
            </td>
            <td align="right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td align="right">
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
                    function isNumber(evt) {
                        evt = (evt) ? evt : window.event;
                        var charCode = (evt.which) ? evt.which : evt.keyCode;
                        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                            return false;
                        }
                        
                        return true;
                    }
                    
                </script>
            </td>
            </tr>
    </table>
    </div>
</asp:Content>

