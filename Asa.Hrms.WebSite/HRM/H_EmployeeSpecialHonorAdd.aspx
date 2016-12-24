<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeSpecialHonorAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeSpecialHonorAdd" MasterPageFile="~/Site.master" Title="" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
<div id="div1" runat="server">
<table border="0" cellpadding="1" cellspacing="1">
<tr><br /></tr>
<tr>
<td align="right">Employee:</td>
<td>
<asp:Panel ID="Panel1" runat="server"  DefaultButton="lbSearch">
<asp:TextBox ID="txtEmployee" runat="server" MaxLength="250" autocomplete="off" Width="300px" ></asp:TextBox>
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
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="left" colspan="2">
                <strong>Current Information</strong>
            </td>
<td align="left">&nbsp;</td>
<td align="left">&nbsp;</td>
</tr>
<tr>
<td align="right">Designation</td>
<td>
    <asp:TextBox ID="txtDesignation" runat="server" ForeColor="ActiveCaptionText" 
        ReadOnly="True" Enabled="False"></asp:TextBox>
</td>
<td align="right">
    Status</td>
<td align="right">
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
    ASA
    District</td>
<td>
    <asp:TextBox ID="txtDistrict" runat="server" ForeColor="ActiveCaptionText" 
        ReadOnly="True" Enabled="False"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left" colspan="2"><strong>Special Honor:</strong>
            </td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Subject of Honor:</td>
<td>
<asp:TextBox ID="txtSubjectofHonor" runat="server" Text="" MaxLength="300" Width="300"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvSubjectOfHonor" runat="server" Display="Dynamic" ControlToValidate="txtSubjectOfHonor" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Letter No:</td>
<td>
<asp:TextBox ID="txtLetterNo" runat="server" Text="" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" Display="Dynamic" ControlToValidate="txtLetterNo" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Letter Date:</td>
<td>
<asp:TextBox runat="server" ID="txtLetterDate" Text="" MaxLength="10"></asp:TextBox>
<asp:ImageButton ID="ibLetterDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtLetterDate'));return false;"></asp:ImageButton>
<asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
</table>
<table>
<tr>
<td>
<asp:Panel runat="server" ID="pnlExtenderList" ScrollBars="Vertical" Style="overflow: hidden; height: 0px; width: 0px; z-index: 99999"></asp:Panel>
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
