<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeUploadedFiles.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.H_EmployeeUploadedFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="../App_Themes/Default/AjaxExtender.css" rel="stylesheet" type="text/css" />
    <div id="div1" runat="server">
    <table border="0" cellpadding="1" cellspacing="1">
        <tr><br /></tr>
        <tr>
            <td align="right">
                Employee:
            </td>
            <td>
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
            <td align="right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        
        <tr>
            <td align="right">
                Designation</td>
            <td>
                <asp:TextBox ID="txtDesignation" runat="server" Enabled="False" Font-Bold="true" ForeColor="ActiveCaptionText" ReadOnly="True" 
                    Width="250px"></asp:TextBox>
          </td>
        </tr>
        
        </table>
        <br />
        <br />
        <asp:Panel ID="pnlFile" runat="server" GroupingText="Uploaded File List">
        <asp:GridView ID="gvList" runat="server" SkinID="AddPageGrid" 
            HeaderStyle-Font-Bold="true" DataKeyNames="Id,FileName" 
            onrowcommand="gvList_RowCommand" EmptyDataText="No uploaded File found">
            <Columns>
                <mms:BoundField DataField="Title" HeaderText="File Name" SortExpression="Title"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                    FieldType="String" ItemStyle-Width="400px">
                </mms:BoundField>
                <asp:TemplateField HeaderText="View Item" >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkView" CausesValidation="false" CommandName="viewitem"
                            Text="View" CommandArgument='<%# Eval("Id") %>'
                            runat="server">View File</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
                                
            </Columns>
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
    </div>
</asp:Content>

