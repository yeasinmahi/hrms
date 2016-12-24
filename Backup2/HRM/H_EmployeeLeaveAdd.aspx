<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeLeaveAdd.aspx.cs" Inherits="Asa.Hrms.WebSite.HRM.H_EmployeeLeaveAdd" MasterPageFile="~/Site.master" Title="" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
<div id="div1" runat="server">
<div>
<div style="float:left; width:auto;">
<table border="0" cellpadding="1" cellspacing="1">
<tr><br /></tr>
<tr>
<td align="right">Employee:</td>
<td colspan="3">
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
    District</td>
<td>
    <asp:TextBox ID="txtDistrict" runat="server" ForeColor="ActiveCaptionText" 
        ReadOnly="True" Enabled="False"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left" colspan="2"><strong>Leave Information</strong>
            </td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Leave Type:</td>
<td>
<asp:DropDownList ID="ddlType" runat="server" Width="125px" AutoPostBack="True" 
        onselectedindexchanged="ddlType_SelectedIndexChanged"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvType" runat="server" Display="Dynamic" ControlToValidate="ddlType" InitialValue="0" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
</td>
<td colspan="2">
    <asp:CheckBox ID="chkLeaveExtension" runat="server" Text="Leave Extension" />
    </td>
</tr>
<tr>
<td align="right">Letter No:</td>
<td>
<asp:TextBox ID="txtLetterNo" runat="server" Text="" MaxLength="200"></asp:TextBox>
<asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" Display="Dynamic" ControlToValidate="txtLetterNo" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
</td>
<td colspan="2">
    <asp:CheckBox ID="chkCancel" runat="server" Text="Cancel Leave" />
    </td>
</tr>
<tr>
<td align="right">Letter Date:</td>
<td>
<asp:TextBox runat="server" ID="txtLetterDate" Text="" MaxLength="10"></asp:TextBox>
<asp:ImageButton ID="ibLetterDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtLetterDate'));return false;"></asp:ImageButton>
<asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
<asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Start Date:</td>
<td>
<asp:TextBox runat="server" ID="txtStartDate" Text="" MaxLength="10"></asp:TextBox>
<asp:ImageButton ID="ibStartDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate'));return false;"></asp:ImageButton>
<asp:RequiredFieldValidator ID="rfvStartDate" runat="server" Display="Dynamic" ControlToValidate="txtStartDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
<asp:RangeValidator ControlToValidate="txtStartDate" ID="rvStartDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid start date"></asp:RangeValidator>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">End Date:</td>
<td>
<asp:TextBox ID="txtEndDate" runat="server" Text="" MaxLength="10"></asp:TextBox>
<asp:ImageButton ID="ibEndDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate'));return false;"></asp:ImageButton>
<asp:RequiredFieldValidator ID="rfvEndDate" runat="server" Display="Dynamic" ControlToValidate="txtEndDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
<asp:RangeValidator ControlToValidate="txtEndDate" ID="rvEndDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid end date"></asp:RangeValidator>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Cause of Leave:</td>
<td colspan="3">
<asp:TextBox ID="txtCause" runat="server" MaxLength="250" Width="300px"></asp:TextBox>
</td>
</tr>
<tr>
<td align="right">&nbsp;</td>
<td colspan="3">
    &nbsp;</td>
</tr>
<tr>
<td align="right">
    <asp:HyperLink ID="hlBack" runat="server" 
        NavigateUrl="~/HRM/H_EmployeeLeaveList.aspx">Back</asp:HyperLink>
    </td>
<td colspan="3">
    &nbsp;</td>
</tr>
</table>
</div>
<div style="width:auto;"><strong>Leave List</strong></div>
<div style="width:500px;" >
    <asp:GridView ID="gvList" SkinID="Special" runat="server">
    <Columns>
        <mms:BoundField DataField="LetterNo" HeaderText="Letter No." FieldType="String" SortExpression="LetterNo"></mms:BoundField>
        <mms:BoundField DataField="LetterDate" HeaderText="Letter Date" FieldType="DateTime" SortExpression="LetterDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
        <mms:BoundField DataField="LeaveBranch" HeaderText="Leave Branch" FieldType="String" SortExpression="LeaveBranch"></mms:BoundField>
        <mms:BoundField DataField="StartDate" HeaderText="Start Date" FieldType="DateTime" SortExpression="StartDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
        <mms:BoundField DataField="EndDate" HeaderText="End Date" FieldType="DateTime" SortExpression="EndDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
         <mms:BoundField DataField="Type" HeaderText="Leave Type" FieldType="String" SortExpression="Type"></mms:BoundField>
    </Columns>
    </asp:GridView>
</div>
</div>
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