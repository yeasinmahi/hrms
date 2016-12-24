<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeMultiLetterAdd.aspx.cs" Inherits="Asa.Hrms.WebSite.HRM.H_EmployeeMultiLetterAdd" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                Letter No:</td>
            <td>
                <asp:TextBox ID="txtLetterNo" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtLetterNo" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                Letter Date</td>
            <td>
                <asp:TextBox ID="txtLetterDate" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtLetterDate" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtLetterDate" Operator="DataTypeCheck" Type="Date" ErrorMessage="*"></asp:CompareValidator>
            </td>
            <td align="right">
                Effective Date:</td>
            <td>
                <asp:TextBox ID="txtEffectiveDate" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtEffectiveDate" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtEffectiveDate" Operator="DataTypeCheck" Type="Date" ErrorMessage="*"></asp:CompareValidator>
            </td>
            
        </tr>


        <tr>
            <td align="right">
                Letter Types:</td>
            <td colspan="5">
                <asp:CheckBoxList ID="chkLetterTypes" runat="server" 
                    RepeatDirection="Horizontal" AutoPostBack="True" 
                    onselectedindexchanged="chkLetterTypes_SelectedIndexChanged">
                    <asp:ListItem Value="1">Transfer</asp:ListItem>
                    <asp:ListItem Value="2">Promotion</asp:ListItem>
                    <asp:ListItem Value="3">Penalty</asp:ListItem>
                    <asp:ListItem Value="4">Warning</asp:ListItem>
                    <asp:ListItem Value="5">Increment Heldup</asp:ListItem>
                    <asp:ListItem Value="6">Rejoin</asp:ListItem>
                </asp:CheckBoxList>
            </td>
            
        </tr>


        </table>
        <asp:Panel ID="pnlTransfer" GroupingText="Transfer" runat="server">
            <table>
                <tr>
                    <td>
                        Type:</td>
                    <td>
                        <asp:DropDownList ID="ddlTransferType" runat="server">
                        </asp:DropDownList>
                        
                    </td>
                    <td>
                        To District:</td>
                    <td>
                        <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" 
                            DataTextField="Name" DataValueField="Id" 
                            onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlDistrict" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlDistrict" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        To Branch:</td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="Name" 
                            DataValueField="Id">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlBranch" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlpromotion" GroupingText="Promotion" runat="server">
        <table>
                <tr>
                <td>
                        Type:</td>
                    <td>
                        <asp:DropDownList ID="ddlPromoType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        To Grade:</td>
                    <td>
                        <asp:DropDownList ID="ddlGrade" runat="server" DataTextField="Name" 
                            DataValueField="Id" AutoPostBack="True" 
                            onselectedindexchanged="ddlGrade_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlGrade" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlGrade" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        To Designation</td>
                    <td>
                        <asp:DropDownList ID="ddlDesignation" runat="server" DataTextField="Name" 
                            DataValueField="Id">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlDesignation" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlDesignation" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlIncrementHeldup" GroupingText="IncrementHeldup" runat="server">
            <table>
                <tr>
                <td>Number of Increment:</td>
                <td>
                    <asp:TextBox ID="txtIncrementStop" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtIncrementStop" runat="server" ErrorMessage="*" ControlToValidate="txtIncrementStop" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvtxtIncrementStop" runat="server" ErrorMessage="*" ControlToValidate="txtIncrementStop" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlPenalty" GroupingText="Penalty" runat="server">
        <table>
                <tr>
                <td>Type:</td>
                <td>
                    <asp:DropDownList ID="ddlPenaltyType" runat="server">
                                <asp:ListItem Value="F">Fine</asp:ListItem>
                                <asp:ListItem Value="P">Penalty</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        Amount:</td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvtxtAmount" runat="server" ErrorMessage="*" ControlToValidate="txtAmount" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvtxtAmount" runat="server" ErrorMessage="*" ControlToValidate="txtAmount" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                    
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlWarning" GroupingText="Warning" runat="server">
        <table>
                <tr>
                <td>Type:</td>
                <td>
                    <asp:DropDownList ID="ddlWarningType" runat="server">
                    <asp:ListItem Value="0">Select Type</asp:ListItem>
                        <asp:ListItem Value="Normal">Normal</asp:ListItem>
                        <asp:ListItem Value="Punishment">Punishment</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlRejoin" GroupingText="Rejoin" runat="server">
        <table>
                <tr>
                <td>Rejoin District:</td>
                <td>
                    <asp:DropDownList ID="ddlRejoinDistrict" runat="server" DataTextField="Name" 
                        DataValueField="Id" AutoPostBack="True" 
                        onselectedindexchanged="ddlRejoinDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlRejoinDistrict" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlRejoinDistrict" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        Rejoin Branch:</td>
                    <td>
                        <asp:DropDownList ID="ddlRejoinBranch" runat="server" 
                            DataTextField="Name" DataValueField="Id" >
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlRejoinBranch" runat="server" ErrorMessage="*" InitialValue="0" ControlToValidate="ddlRejoinBranch" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
               
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlLetter" GroupingText="Letter" runat="server">
        <table>
                <tr>
                <td>Subject:</td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server" Width="700px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSubject" runat="server" 
                        ControlToValidate="txtSubject" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                <td align="right">Letter Body:</td>
                <td>
                    <asp:TextBox ID="txtBody" runat="server" Rows="4" TextMode="MultiLine" 
                        Width="700px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvBody" runat="server" 
                        ControlToValidate="txtBody" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
               <%-- <tr>
                    <td align="right">
                        Conclution:</td>
                    <td>
                        <asp:TextBox ID="txtConclution" runat="server" Width="700px"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td align="right">
                        Signatory:</td>
                    <td>
                        <asp:TextBox ID="txtSignatory" runat="server" Width="700px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSignatory" runat="server" 
                            ControlToValidate="txtSignatory" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Designation:</td>
                    <td>
                        <asp:TextBox ID="txtDesg" runat="server" Width="700px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDesg" runat="server" 
                            ControlToValidate="txtDesg" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Letter CC:</td>
                    <td>
                        
                        
                        <asp:TextBox ID="txtDuplication" runat="server" Rows="5" TextMode="MultiLine" 
                            Width="350px"></asp:TextBox>
                        
                        
                        <asp:RequiredFieldValidator ID="rfvCC" runat="server" 
                            ControlToValidate="txtDuplication" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        
                        
                     </td>
                </tr>
                <tr>
                    <td align="right">
                        Note:</td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" Rows="3" TextMode="MultiLine" 
                            Width="700px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    <asp:HyperLink ID="hlBack" NavigateUrl="~/HRM/H_EmployeeMultiLetterList.aspx" runat="server">Back</asp:HyperLink>
                        </td>
                    <td>
                        <asp:HiddenField ID="hdnLetterId" runat="server" />
                    </td>
                </tr>
            </table>
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
