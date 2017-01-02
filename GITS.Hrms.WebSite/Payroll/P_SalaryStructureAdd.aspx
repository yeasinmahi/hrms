<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_SalaryStructureAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.P_SalaryStructureAdd" %>
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
                Salary Type:</td>
            <td>
                <asp:DropDownList ID="ddlSalaryType" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlSalaryType_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select Option</asp:ListItem>
                    <asp:ListItem Value="1">Earnings</asp:ListItem>
                    <asp:ListItem Value="2">Deductions</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvType" runat="server" 
                    ErrorMessage="*" ControlToValidate="ddlSalaryType" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
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
            <td  colspan="5"  >
                <asp:GridView ID="gvEarning" runat="server" AutoGenerateColumns="False" DataKeyNames="Id">
                    <Columns>
                        <asp:TemplateField >
                        <ItemStyle  Width="20px" />
						<ItemTemplate>
							<asp:CheckBox ID="chkSelect" Checked='<%#Convert.ToBoolean(Eval("Amount")) %>' 
                                runat="server" oncheckedchanged="chkSelect_CheckedChanged" />
						</ItemTemplate>
						<HeaderTemplate>
							<input id="chkAll" runat="server" type="checkbox" onclick="javascript:GridSelectAll(this);" />
						</HeaderTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Earning Head" DataField="Name"  ></asp:BoundField>
                        <asp:BoundField HeaderText="Fixed/Percent" DataField="FixedPercent" >
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Amount">
                        
                        <ItemTemplate >
							<asp:TextBox ID="txtAmount" Text='<%# Convert.ToDecimal(Eval("Amount"))==0?"" :Eval("Amount") %>' runat="server" Width="100px" />
							
                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="*" ControlToValidate="txtAmount" Display="Dynamic" Enabled='<%#Convert.ToBoolean(Eval("Amount")) %>'></asp:RequiredFieldValidator>
						</ItemTemplate>

                        </asp:TemplateField>
                    </Columns>
                
                </asp:GridView>
            </td>
            <td></td>
            
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

