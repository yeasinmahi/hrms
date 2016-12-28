<%@ Page Title="Employee Consultancy Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeConsultancyReport.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.H_EmployeeConsultancyReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
    <asp:Panel ID="pnlSerach" runat="server" DefaultButton="lbSearch">
    <table >
        <tr>
            <td>
                Employee ID</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtEmpId" runat="server"  autocomplete="off" MaxLength="250" Width="380px" onchange="javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbSearch', '')"></asp:TextBox>
                  <ajaxc:AutoCompleteExtender ID="aceEmployee" runat="server" TargetControlID="txtEmpId"
                    ServicePath="~/Services/SearchService.asmx" ServiceMethod="GetSuggestions"
                    MinimumPrefixLength="1" CompletionInterval="0" EnableCaching="true" CompletionSetCount="10"
                    DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="pnlExtenderList"
                    OnClientShown="AdjustWidth">
                </ajaxc:AutoCompleteExtender>
               <%-- <asp:RangeValidator ID="rvEmpId" ControlToValidate="txtEmpId" runat="server" Type="Integer" MinimumValue="1" MaximumValue="99999" ErrorMessage="*"></asp:RangeValidator>--%>
            </td>
            <td>
                <asp:LinkButton ID="lbSearch" runat="server" 
                    onclick="lbSearch_Click">Search</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                Designation</td>
            <td>
                <asp:DropDownList ID="ddlDesignationFilter" runat="server">
                    <asp:ListItem Value="1">Contains</asp:ListItem>
                    <asp:ListItem Value="2">Equal</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Country</td>
            <td>
                <asp:DropDownList ID="ddlCountryFilter" runat="server">
                    <asp:ListItem Value="1">Contains</asp:ListItem>
                    <asp:ListItem Value="2">Equal</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
       <tr>
            <td>
                Fund</td>
            <td>
                <asp:DropDownList ID="ddlFund" runat="server">
                    <asp:ListItem Value="1">Contains</asp:ListItem>
                    <asp:ListItem Value="2">Equal</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtFund" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlEmp" runat="server">
        <asp:GridView ID="gvList" runat="server" OnSorting="gvList_Sorting" SkinID="Report">
            
        </asp:GridView>
    </asp:Panel>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
