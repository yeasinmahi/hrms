<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeActingDesignationAdd.aspx.cs"
    Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeActingDesignationAdd" MasterPageFile="~/Site.master"
    Title="" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
    <div id="div1" runat="server">
        <table border="0" cellpadding="3" cellspacing="1">
            <tr>
                <br />
            </tr>
            <tr>
                <td align="right">
                    Employee:
                </td>
                <td colspan="5">
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
                </td>
            </tr>
            <tr>
                <td colspan="6">
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
                        Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
                </td>
                <td align="right">
                    Grade:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtGrade" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>&nbsp;
                </td>
                <td align="right">
                    Designation:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtDesignation" Text="" Enabled="false" Font-Bold="true"
                        Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    Zone:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtZone" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>
                </td>
                <td align="right">
                    Subzone:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSubzone" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>
                </td>
                <td align="right">
                    Region:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtRegion" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>
                </td>
                <td align="right">
                    Branch:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBranch" Text="" Enabled="false" Font-Bold="true"
                        ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <br />
                    <strong>Acting Designation Information</strong>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Grade:
                </td>
                <td>
                    <asp:DropDownList ID="ddlGradeId" runat="server" DataTextField="Name" DataValueField="Id"
                        AutoPostBack="True" Width="200px" OnSelectedIndexChanged="ddlGradeId_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    Designation:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDesignationId" runat="server" DataTextField="Name" DataValueField="Id"
                        Width="200px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    From Date:
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" Text="" MaxLength="10" Width="135"></asp:TextBox>
                    <asp:ImageButton ID="ibFromDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtFromDate'));return false;">
                    </asp:ImageButton>
                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" Display="Dynamic" ControlToValidate="txtFromDate"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtFromDate" ID="rvFromDate" Type="Date" MaximumValue="31/12/9999"
                        MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid from date"></asp:RangeValidator>
                </td>
                <td align="right">
                    To Date:
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" Text="" MaxLength="10" Width="135"></asp:TextBox>
                    <asp:ImageButton ID="ibToDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtToDate'));return false;">
                    </asp:ImageButton>
                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" Display="Dynamic" ControlToValidate="txtToDate"
                        ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ControlToValidate="txtToDate" ID="rvToDate" Type="Date" MaximumValue="31/12/9999"
                        MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid to date"></asp:RangeValidator>
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
