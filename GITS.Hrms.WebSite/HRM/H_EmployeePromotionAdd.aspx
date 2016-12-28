<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_EmployeePromotionAdd.aspx.cs"
    Inherits="GITS.Hrms.WebSite.HRM.H_EmployeePromotionAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ddl_changed(ddl) {
            
            if(ddl.value=='8')
            {
                document.getElementById('<%=ddlNewH_GradeId.ClientID %>').disabled = true;
            }
            else
            {
                document.getElementById('<%=ddlNewH_GradeId.ClientID %>').disabled = false;
            }
        }
        
    </script>
    <div id="div1" runat="server">
    <table border="0" cellpadding="3" cellspacing="1">
        <tr><br /></tr>
        <tr>
            <td align="right">
                Employee:
            </td>
            <td colspan="5"><asp:Panel ID="Panel1" runat="server" DefaultButton="lbSearch">
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
            <td colspan="6"><br />
                <strong>Current Information</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                Department:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDepartment" Text="" Enabled="false" Font-Bold="true"
                    Width="200px" ForeColor="ActiveCaptionText">
			    </asp:TextBox>
            </td>
            <td align="right">
                Grade:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtGrade" Text="" Enabled="false" Font-Bold="true"
                    ForeColor="ActiveCaptionText" Width="60">
			    </asp:TextBox>&nbsp;From&nbsp;<asp:TextBox
                        runat="server" ID="txtGradeDate" Text="" Enabled="false" Font-Bold="true"
                        Width="70px" ForeColor="ActiveCaptionText">
			    </asp:TextBox>
			    <input id="hdnGrade" runat="server" type="hidden" />
            </td>
            <td align="right">
                Designation:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDesignation" Text="" Enabled="false" Font-Bold="true" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
			    &nbsp;From&nbsp;
			    <asp:TextBox runat="server" ID="txtDesignationDate" Text="" Enabled="false" Font-Bold="true" Width="70px" ForeColor="ActiveCaptionText"></asp:TextBox>
			    <input id="hdnDesignation" runat="server" type="hidden" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Zone:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtZone" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>
            </td>
            <td align="right">
                District:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtSubzone" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="200px"></asp:TextBox>
            </td>
            <td align="right">
                Region:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtRegion" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="171px"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td align="right">
                Branch</td>
            <td colspan="2">
                <asp:TextBox runat="server" ID="txtBranch" Text="" Enabled="false" 
                    Font-Bold="true" ForeColor="ActiveCaptionText" Width="300px"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td align="right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td colspan="6"><br />
                <strong>Promotion/Demotion Information</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlType" runat="server" Width="135px" onchange="ddl_changed(this)"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType" InitialValue="1" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                Letter No:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtLetterNo" Text="" MaxLength="200" Width="200px"></asp:TextBox>
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
        </tr>
        <tr>
            <td align="right">
                Grade:
            </td>
            <td>
                <asp:DropDownList ID="ddlNewH_GradeId" runat="server" DataTextField="Name" DataValueField="Id" AutoPostBack="True" Width="135px" onselectedindexchanged="ddlNewH_GradeId_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td align="right">
                Designation:
            </td>
            <td>
                <asp:DropDownList ID="ddlNewH_DesignationId" runat="server" DataTextField="Name" DataValueField="Id" Width="200px"></asp:DropDownList>
            </td>
            <td align="right" style="font-size: smaller;">
                Promotion/Demotion<br/> Date:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPromotionDate" Text="" MaxLength="10"></asp:TextBox>
                <asp:ImageButton ID="ibPromotionDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtPromotionDate'));return false;"></asp:ImageButton>
                <asp:RequiredFieldValidator ID="rfvPromotionDate" runat="server" Display="Dynamic" ControlToValidate="txtPromotionDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			    <asp:RangeValidator ControlToValidate="txtPromotionDate" ID="rvPromotionDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid promotion date"></asp:RangeValidator>
			    <asp:CompareValidator ID="cvPromotionDate" Type="Date" Operator="GreaterThan" ControlToCompare="txtGradeDate" ControlToValidate="txtPromotionDate" Display="Dynamic" runat="server" ErrorMessage="*" ToolTip="New promotion date should greater than previous promotion date"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Remarks:
            </td>
            <td colspan="5">
                <asp:TextBox runat="server" ID="txtRemarks" Text="" MaxLength="300" Width="495px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td colspan="5">
                <asp:CheckBox ID="chkCancel" runat="server" 
                    Text="Cancel Promotion/Demotion/Designation Change" />
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
