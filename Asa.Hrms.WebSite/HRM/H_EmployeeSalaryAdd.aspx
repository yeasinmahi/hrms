<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeSalaryAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeSalaryAdd" MasterPageFile="~/Site.master" Title="" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
<div id="div1" runat="server">
<table border="0" cellpadding="1" cellspacing="1">
<tr><br /></tr>
<tr>
<td align="right">Employee:</td>
<td>
<asp:TextBox ID="txtEmployee" runat="server" MaxLength="250" autocomplete="off" Width="300px" ></asp:TextBox>
<ajaxc:AutoCompleteExtender ID="aceEmployee" runat="server" TargetControlID="txtEmployee"
  ServicePath="~/Services/SearchService.asmx" ServiceMethod="GetSuggestions"
  MinimumPrefixLength="1" CompletionInterval="0" EnableCaching="true" CompletionSetCount="10"
  DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
  CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="pnlExtenderList"
  OnClientShown="AdjustWidth">
</ajaxc:AutoCompleteExtender>
<asp:RequiredFieldValidator ID="rfvEmployee" runat="server" Display="Dynamic" ControlToValidate="txtEmployee" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td align="right">Basic Salary:</td>
<td>
<asp:TextBox ID="txtBasicSalary" runat="server" Text=""></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvBasicSalary" runat="server" Display="Dynamic" ControlToValidate="txtBasicSalary" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
<asp:RangeValidator ControlToValidate="txtBasicSalary" ID="rvBasicSalary" Type="Double" MaximumValue="999999999999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Only integer values are allowed"></asp:RangeValidator>
</td>
</tr>
<tr>
<td align="right">Last Increment Date:</td>
<td>
<asp:TextBox ID="txtLastIncrementDate" runat="server" Text="" MaxLength="10"></asp:TextBox>
<asp:ImageButton ID="ibLastIncrementDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtLastIncrementDate'));return false;"></asp:ImageButton>
<asp:RequiredFieldValidator ID="rfvLastIncrementDate" runat="server" Display="Dynamic" ControlToValidate="txtLastIncrementDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
</td>
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
