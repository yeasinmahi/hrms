<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_EmployeeTransferAdd.aspx.cs"
    Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeTransferAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                Zone:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtZone" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
			    &nbsp;From&nbsp;
			    <asp:TextBox runat="server" ID="txtZoneDate" Text="" Enabled="false" 
                    Font-Bold="true" Width="75px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                District:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtSubzone" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
			    &nbsp;From&nbsp;
			    <asp:TextBox runat="server" ID="txtSubzoneDate" Text="" Enabled="false" 
                    Font-Bold="true" Width="75px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                Region:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtRegion" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText"></asp:TextBox>
                &nbsp;From&nbsp;
			    <asp:TextBox runat="server" ID="txtRegionDate" Text="" Enabled="false" 
                    Font-Bold="true" Width="75px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td align="right">
                Branch</td>
            <td>
                <asp:TextBox runat="server" ID="txtBranch" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
                <input id="hdnBranch" runat="server" type="hidden" />
            &nbsp;From&nbsp;
			    <asp:TextBox runat="server" ID="txtBranchDate" Text="" Enabled="false" 
                    Font-Bold="true" Width="75px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                Mobile:</td>
            <td>
                <asp:TextBox runat="server" ID="txtMobile" Text="" Enabled="false" 
                    Font-Bold="true" ForeColor="ActiveCaptionText" Width="115px"></asp:TextBox>
			    </td>
            <td align="right">
                &nbsp;</td>
                <td align="right">
                &nbsp;</td>
            
        </tr>
        <tr>
            <td colspan="4"><br />
                <strong>Transfer Information</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                Letter No:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtLetterNo" Text="" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" Display="Dynamic" ControlToValidate="txtLetterNo" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                Letter Date:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtLetterDate" Text="" MaxLength="10"></asp:TextBox>
			    <asp:ImageButton ID="ibLetterDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtLetterDate'));return false;"></asp:ImageButton>
                <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			    <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
            </td>
            <td align="right">
                Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Joining Date:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtJoiningDate" Text="" MaxLength="10"></asp:TextBox>
			    <asp:ImageButton ID="ibJoiningDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtJoiningDate'));return false;"></asp:ImageButton>
                <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic" ControlToValidate="txtJoiningDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			    <asp:RangeValidator ControlToValidate="txtJoiningDate" ID="rvJoiningDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid joining date"></asp:RangeValidator>
			    <asp:CompareValidator ID="cvJoiningDate" Type="Date" Operator="GreaterThan" ControlToCompare="txtBranchDate" ControlToValidate="txtJoiningDate" Display="Dynamic" runat="server" ErrorMessage="*" ToolTip="New joining date should greater than previous branch's joining date"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                ASA District:
            </td>
            <td>
                <asp:DropDownList ID="ddlSubzone" runat="server" DataTextField="Name" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="ddlSubzone_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td align="right">
                Region:
            </td>
            <td>
                <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Name" DataValueField="Id" AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td align="right">
                Branch:
            </td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="Name" DataValueField="Id"></asp:DropDownList>
            </td>
            
        </tr>
        <tr>
            <td align="right">
                Remarks:
            </td>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtRemarks" Text="" MaxLength="300" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvRemarks" runat="server" Display="Dynamic" ControlToValidate="txtRemarks" ErrorMessage="Required" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
            <td align="right"></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td colspan="3">
                <asp:CheckBox ID="chkCancel" runat="server" Text="Transfer Cancel" 
                    oncheckedchanged="chkCancel_CheckedChanged" />
            </td>
            <td align="right">&nbsp;</td>
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
                </script>
            </td>
            </tr>
    </table>
    </div>
</asp:Content>