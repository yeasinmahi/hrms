<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeAppraisalAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeAppraisalAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table border="0" cellpadding="1" cellspacing="1">

<tr>
<td align="right">
    <asp:HiddenField ID="hfAppraisalId" runat="server" />
    </td>
<td colspan="3">
    <asp:Label ID="lblEvaluation" runat="server" Font-Bold="True" 
        Font-Size="X-Large"></asp:Label>
</td>
</tr>
<tr><br /></tr>
<tr>
<td align="right">Employee:</td>
<td colspan="3">
<asp:Panel ID="Panel1" runat="server"  DefaultButton="lbSearch">
<asp:TextBox ID="txtEmployee" runat="server" MaxLength="250" autocomplete="off" Width="300px" onchange="javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbSearch', '')" ></asp:TextBox>
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
<td align="left" colspan="2">
                <strong>Current Information</strong>
            </td>
<td align="left">&nbsp;</td>
<td align="left">
    &nbsp;</td>
</tr>
<tr>
<td align="right">Designation</td>
<td>
    <asp:TextBox ID="txtDesignation" runat="server" ForeColor="ActiveCaptionText" 
        ReadOnly="True" Enabled="False"></asp:TextBox>
</td>
<td align="right">
    Status</td>
<td align="left">
    <asp:TextBox ID="txtStatus" runat="server" ForeColor="ActiveCaptionText" 
        ReadOnly="True" Enabled="False"></asp:TextBox>
</td>
</tr>
<tr>
<td align="right">Branch</td>
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
<td align="left" colspan="2">&nbsp;</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right"></td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>



<tr>
<td align="left" colspan="4">
    <asp:GridView ID="gvAppraisal" runat="server" DataKeyNames="Id">
       <Columns>
            <asp:TemplateField HeaderText="SL">
                <ItemTemplate>
                     <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            
            <mms:BoundField DataField="QuestionText" HeaderText="Subject" FieldType="String" SortExpression="QuestionText"></mms:BoundField>
            
            <asp:TemplateField HeaderText="Options" >
            <ItemStyle HorizontalAlign="Center" Width="20px" />
            <ItemTemplate>
                <asp:RadioButtonList ID="rdoOption" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="5">Execilent</asp:ListItem>
                        <asp:ListItem Value="4">very Good</asp:ListItem>
                        <asp:ListItem Value="3">Good</asp:ListItem>
                        <asp:ListItem Value="2">Average</asp:ListItem>
                        <asp:ListItem Value="1">Bad</asp:ListItem>
                </asp:RadioButtonList>
            </ItemTemplate>
            
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
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
<tr>
<td align="right">
    Appraiser:</td>
<td>
    <asp:Label ID="lblAppraiser" runat="server"></asp:Label>
    </td>
<td>
    Designation:</td>
<td>
    <asp:Label ID="lblDesg" runat="server"></asp:Label>
    </td>
</tr>
<tr>
<td align="left">
    <asp:HiddenField ID="hfApp" runat="server" />
    </td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
</table>
<script type="text/javascript">



</script>
</asp:Content>


