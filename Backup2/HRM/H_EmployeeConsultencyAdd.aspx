<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeConsultencyAdd.aspx.cs" Inherits="Asa.Hrms.WebSite.HRM.H_EmployeeConsultencyAdd" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
<div id="div1" runat="server">
<div>
<div >
<table border="0" cellpadding="1" cellspacing="1">
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
<td align="left">&nbsp;</td>
</tr>
<tr>
<td align="right">Designation</td>
<td>
    <asp:TextBox ID="txtDesignation" runat="server" ForeColor="ActiveCaptionText" 
        ReadOnly="True" Enabled="False" Width="200px"></asp:TextBox>
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
<td align="left" colspan="2"><strong>Leave Information</strong>
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
<td align="right">NGO Name</td>
<td>
<asp:TextBox runat="server" ID="txtNgoName" Text="" ></asp:TextBox>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Fund</td>
<td>
  <asp:DropDownList ID="ddlNgo" runat="server" DataTextField="Name" DataValueField="Id">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvNgo" runat="server" Display="Dynamic" ControlToValidate="ddlNgo" InitialValue="0" ErrorMessage="*" ></asp:RequiredFieldValidator>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Through</td>
<td>
<asp:TextBox runat="server" ID="txtThrough" Text="" MaxLength="100"></asp:TextBox>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Phone</td>
<td>
<asp:TextBox runat="server" ID="txtPhone" Text="" MaxLength="15"></asp:TextBox>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Fax</td>
<td>
<asp:TextBox runat="server" ID="txtFax" Text="" MaxLength="15"></asp:TextBox>
</td>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td align="right">Email</td>
<td>
<asp:TextBox runat="server" ID="txtEmail" Text="" MaxLength="50"></asp:TextBox>
</td>
<td>
    &nbsp;</td>
<td>
    <asp:HiddenField ID="hfConsultId" runat="server" />
    </td>
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
<td align="right">Country</td>
<td>
    <asp:DropDownList ID="ddlCountry" runat="server" DataTextField="Name" DataValueField="Id">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" Display="Dynamic" ControlToValidate="ddlCountry" InitialValue="0" ErrorMessage="*" ></asp:RequiredFieldValidator>
</td>
<td>
    &nbsp;</td>
<td>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    </td>
</tr>
</table>
</div>

</div>
<div style="width:auto;"><strong>Consultency List</strong></div>
<div style="width:500px;" >
    <asp:GridView ID="gvList" runat="server">
    <Columns>
        <mms:BoundField DataField="LetterNo" HeaderText="Letter No." FieldType="String" SortExpression="LetterNo"></mms:BoundField>
        <mms:BoundField DataField="LetterDate" HeaderText="Letter Date" FieldType="DateTime" SortExpression="LetterDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
        <mms:BoundField DataField="NgoName" HeaderText="NGO Name" FieldType="String" SortExpression="NgoName"></mms:BoundField>
        <mms:BoundField DataField="FundName" HeaderText="Fund Name" FieldType="String" SortExpression="FundName"></mms:BoundField>
        <mms:BoundField DataField="StartDate" HeaderText="Start Date" FieldType="DateTime" SortExpression="StartDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
        <mms:BoundField DataField="EndDate" HeaderText="End Date" FieldType="DateTime" SortExpression="EndDate" DataFormatString="{0:dd/MM/yyyy}"></mms:BoundField>
        <mms:BoundField DataField="Country" HeaderText="Country" FieldType="String" SortExpression="Country"></mms:BoundField>
    </Columns>
    </asp:GridView>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
