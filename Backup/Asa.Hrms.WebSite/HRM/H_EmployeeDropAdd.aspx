<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H_EmployeeDropAdd.aspx.cs" Inherits="Asa.Hrms.WebSite.HRM.H_EmployeeDropAdd" MasterPageFile="~/Site.master" Title="" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
<div id="div1" runat="server">
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
    ASA District</td>
<td>
    <asp:TextBox ID="txtDistrict" runat="server" ForeColor="ActiveCaptionText" 
        ReadOnly="True" Enabled="False"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left" colspan="2"><strong>Drop Information</strong>
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
<asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>

<tr>
<td align="right">Drop Date:</td>
<td>
<asp:TextBox ID="txtDropDate" runat="server"></asp:TextBox>
<asp:ImageButton ID="ibDropDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtDropDate'));return false;"></asp:ImageButton>
<asp:RequiredFieldValidator ID="rfvDropDate" runat="server" Display="Dynamic" ControlToValidate="txtDropDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
<asp:RangeValidator ControlToValidate="txtDropDate" ID="rvDropDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid drop date"></asp:RangeValidator>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Type of Drop:</td>
<td>
<asp:DropDownList ID="ddlType" runat="server" style="width: 145px;"></asp:DropDownList>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">&nbsp;</td>
<td>
    <asp:CheckBox ID="chkCancel" runat="server" Text="Drop Cancel" />
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
</table>
<asp:Panel ID="pnlList" runat="server" GroupingText="Drop Information">
    <asp:GridView ID="gvDrop" runat="server">
    <Columns>
						
		<mms:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" FieldType="String"></mms:BoundField>
		<mms:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" FieldType="Int32"></mms:BoundField>
		<mms:BoundField DataField="Letter_No" HeaderText="Letter_No" SortExpression="Letter_No" FieldType="String"></mms:BoundField>
		<mms:BoundField DataField="Letter_Date" HeaderText="Letter_Date" SortExpression="Letter_Date" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
		<mms:BoundField DataField="Drop_Date" HeaderText="Drop_Date" SortExpression="Drop_Date" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
		<mms:BoundField DataField="Drop_Type" HeaderText="Drop_Type" SortExpression="Drop_Type" FieldType="String"></mms:BoundField>
		</Columns>
    </asp:GridView>
</asp:Panel>
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
