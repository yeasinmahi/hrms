<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="H_EmployeeInfoUpload.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeInfoUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td colspan="2" style="font-family: Arial; font-weight: bold;">
                Upload Employee Information Form
            </td>
            <td style="font-family: Arial; font-weight: bold;">
                &nbsp;
            </td>
            <td style="font-family: Arial; font-weight: bold;">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Employee</td>
            <td>
                <asp:TextBox ID="txtEmployee" runat="server" Enabled="false" ReadOnly="True" Font-Bold="true" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td>
                &nbsp;Designation
            </td>
            <td>
                <asp:TextBox ID="txtDesignation" runat="server" Enabled="false" Width="200px" ReadOnly="True" Font-Bold="true" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                File Title
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtTitle" runat="server" MaxLength="100" Width="490px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" 
                    ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Select File</td>
            <td colspan="3">
                <asp:FileUpload ID="FileUpload1" runat="server" />
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:HiddenField ID="hdnUploadId" runat="server" />
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:Panel ID="pnlFile" runat="server" GroupingText="Uploaded File List">
        <asp:GridView ID="gvList" runat="server" SkinID="AddPageGrid" 
            HeaderStyle-Font-Bold="true" DataKeyNames="Id,FileName" 
            onrowcommand="gvList_RowCommand" EmptyDataText="No uploaded File found">
            <Columns>
                <mms:BoundField DataField="Title" HeaderText="File Title" SortExpression="Title"
                    ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                    FieldType="String" ItemStyle-Width="400px">
                </mms:BoundField>
                <asp:TemplateField HeaderText="View Item" >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkView" CausesValidation="false" CommandName="viewitem"
                            Text="View" CommandArgument='<%# Eval("Id") %>'
                            runat="server">View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit Row" >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" CausesValidation="false" CommandName="editrow"
                            Text="Edit" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Edit</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete Row" >
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <br />
    <asp:HyperLink ID="hlBack" NavigateUrl="~/HRM/H_EmployeeAdd.aspx" runat="server">Back</asp:HyperLink>
</asp:Content>
