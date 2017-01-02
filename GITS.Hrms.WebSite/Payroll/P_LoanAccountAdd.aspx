<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_LoanAccountAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.Payroll.P_LoanAccountAdd" %>
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
                Age:</td>
            <td>
                <asp:TextBox ID="txtAge" runat="server" Enabled="false" Font-Bold="true" Width="226px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                Service Length:</td>
            <td>
                <asp:TextBox ID="txtServiceLength" runat="server" Enabled="false" 
                    Font-Bold="true" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                Service Left:</td>
            <td>
                <asp:TextBox ID="txtServiceLeft" runat="server" Enabled="false" 
                    Font-Bold="true" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            
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
        <tr>
            <td align="right" style="height: 17px">
                Loan Type:</td>
            <td style="height: 17px">
                <asp:DropDownList ID="ddlLoan" runat="server" Width="230px" AutoPostBack="True" 
                    DataTextField="Name" DataValueField="Id" 
                    onselectedindexchanged="ddlLoan_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right" style="height: 17px">
                Interest Rate:</td>
            <td style="height: 17px">
                <asp:TextBox ID="txtInterestRate" runat="server" Enabled="false" ForeColor="ActiveCaptionText"></asp:TextBox>
			    </td>
            <td align="right" style="height: 17px">
                </td>
                <td align="right" style="height: 17px">
                </td>
            
        </tr>
        <tr>
            <td align="right">
                Loan Amount:</td>
            <td>
                <asp:TextBox ID="txtLoanAmount" runat="server" onkeypress="return isNumber(event)" onkeyup="myFunction();"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvtxtLoanAmount" runat="server"  ControlToValidate="txtLoanAmount" Display="Dynamic"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvtxtLoanAmount" runat="server" Type="Double" Operator="DataTypeCheck" ControlToValidate="txtLoanAmount" ErrorMessage="*"></asp:CompareValidator>
            </td>
            <td align="right">
                Interest Amount:</td>
            <td>
                <asp:TextBox ID="txtInterestAmount" runat="server" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td align="right">
                Duration:</td>
            <td>
                <asp:DropDownList ID="ddlDuration" runat="server" onchange="myFunction()">
                    <asp:ListItem Value="0">Select Duration</asp:ListItem>
                    <asp:ListItem Value="1">1 Year</asp:ListItem>
                    <asp:ListItem Value="2">2 Years</asp:ListItem>
                    <asp:ListItem Value="3">3 Years</asp:ListItem>
                    <asp:ListItem Value="4">4 Years</asp:ListItem>
                    <asp:ListItem Value="5">5 Years</asp:ListItem>
                    <asp:ListItem Value="6">Months</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlDuration" runat="server"  Display="Dynamic" ControlToValidate="ddlDuration" InitialValue="0"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtDurationMonth" runat="server" Width="50px"></asp:TextBox>
            </td>
            <td align="right">
                Total Amount:</td>
            <td>
                <asp:TextBox ID="txtTotalAmount" runat="server" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td align="right">
                &nbsp;Disburse Date:</td>
            <td>
                <asp:TextBox ID="txtDisburseDate" runat="server"></asp:TextBox>
                <ajaxc:CalendarExtender ID="txtDisburseDate_CalendarExtender" runat="server" 
                    Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgCal" 
                    TargetControlID="txtDisburseDate">
                </ajaxc:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvtxtDisburseDate" runat="server" Display="Dynamic" ControlToValidate="txtDisburseDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvtxtDisburseDate" runat="server" ControlToValidate="txtDisburseDate" Type="Date" Operator="DataTypeCheck"  ErrorMessage="*"></asp:CompareValidator>
                <asp:ImageButton ID="imgCal" runat="server" ImageUrl="~/Images/minical.gif" />
            </td>
            <td align="right">
                Inst. Amount:</td>
            <td>
                <asp:TextBox ID="txtInstAmount" runat="server" AutoCompleteType="Disabled" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td align="right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            
        </tr>
        <tr>
            <td align="right">
                Status:</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server">
                </asp:DropDownList>
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
                    function myFunction() {
                        var txtInst = document.getElementById('<%= ddlDuration.ClientID %>');
                        var txtLoanAmt=document.getElementById('<%= txtLoanAmount.ClientID %>');
                        var loanAmt = document.getElementById('<%= txtLoanAmount.ClientID %>').value;
                        var year = document.getElementById('<%= ddlDuration.ClientID %>').value;
                        var irate = document.getElementById('<%= txtInterestRate.ClientID %>').value;
                        var month = 12;
                        if (Number(year) > 0) {
                            
                            var interestAmt = (year * irate * loanAmt) / 100;
                            var totalAmt = Number(interestAmt) + Number(loanAmt);
                            document.getElementById('<%= txtInstAmount.ClientID %>').value = Math.round(totalAmt / (year * month));
                            document.getElementById('<%= txtInterestAmount.ClientID %>').value = interestAmt;
                            document.getElementById('<%= txtTotalAmount.ClientID %>').value = totalAmt;
                        }
                    }
                </script>
            </td>
            </tr>
    </table>
    </div>
</asp:Content>

