<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="H_EmployeeTransferApplicationAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeTransferApplicationAdd" %>

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
                    <strong>Current Information</strong>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    Department:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDepartment" Text="" Enabled="false" Font-Bold="true"
                        Width="235px" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    Grade:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtGrade" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
                </td>
                <td align="right">
                    Designation:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDesignation" Text="" Enabled="false" Font-Bold="true"
                        Width="245px" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Zone:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtZone" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
                    &nbsp;From
                    <asp:TextBox runat="server" ID="txtZoneDate" Text="" Enabled="false" Font-Bold="true"
                        Width="80px" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    District:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSubzone" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
                    &nbsp;From
                    <asp:TextBox runat="server" ID="txtSubzoneDate" Text="" Enabled="false" Font-Bold="true"
                        Width="80px" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    Region:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtRegion" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText"></asp:TextBox>
                    &nbsp;From
                    <asp:TextBox runat="server" ID="txtRegionDate" Text="" Enabled="false" Font-Bold="true"
                        Width="80px" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Branch:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBranch" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
                    <input id="hdnBranch" runat="server" type="hidden" />
                    &nbsp;From
                    <asp:TextBox runat="server" ID="txtBranchDate" Text="" Enabled="false" Font-Bold="true"
                        Width="80px" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    Mobile:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtMobile" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
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
                    Home District:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtHomeDistrict" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
                </td>
                <td align="right">
                    Home Thana:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtHomeThana" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
                </td>
                <td align="right">
                    duration of dist.:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtduration" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="245px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                    <strong>Application Information</strong>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    Applicaiton No:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtApplicationNo" Text="[Auto]" MaxLength="200" ReadOnly="True"
                        Enabled="False" Font-Bold="true" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    Application Date:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtApplicationDate" Text="" MaxLength="10"></asp:TextBox>
                    <asp:ImageButton ID="ibLetterDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtApplicationDate'));return false;">
                    </asp:ImageButton>
                    <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtApplicationDate"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtApplicationDate" ID="rvLetterDate" Type="Date"
                        MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                        ErrorMessage="*" ToolTip="Invalid Application date"></asp:RangeValidator>
                </td>
                <td align="right">
                    Receiving Date:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtReceivingDate" Text="" MaxLength="10"></asp:TextBox>
                    <asp:ImageButton ID="ibJoiningDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtReceivingDate'));return false;">
                    </asp:ImageButton>
                    <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic"
                        ControlToValidate="txtReceivingDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtReceivingDate" ID="rvJoiningDate" Type="Date"
                        MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                        ErrorMessage="*" ToolTip="Invalid Receiving date"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Demanded Place:
                </td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txtDemandedPlace" Text="" MaxLength="300" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvDemandedPlace" runat="server" Display="Dynamic"
                        ControlToValidate="txtDemandedPlace" ErrorMessage="Required" ToolTip="Required"></asp:RequiredFieldValidator>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    Remarks:
                </td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txtRemarks" Text="" MaxLength="300" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rvRemarks" runat="server" Display="Dynamic" ControlToValidate="txtRemarks"
                        ErrorMessage="Required" ToolTip="Required"></asp:RequiredFieldValidator>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">
                    Status:
                </td>
                <td>
                    <asp:DropDownList ID="ddlStatus" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvStatus" runat="server" Display="Dynamic" InitialValue="0"
                        ControlToValidate="ddlStatus" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                </td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
        
        </table>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
