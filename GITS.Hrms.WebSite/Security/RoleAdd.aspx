<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="GITS.Hrms.WebSite.Security.RoleAdd"
    Title="" CodeBehind="RoleAdd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="3" cellspacing="1">
        <tr>
            <td>Name:<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName"
                    ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <mms:GridViewSearchPanel ID="gvspList" runat="server" ListItems="<Properties, DisplayName>"
                    OnResetButtonClicked="gvspList_ResetButtonClicked" OnSearchButtonClicked="gvspList_SearchButtonClicked" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvList" runat="server" DataKeyNames="Name" OnRowDataBound="gvList_RowDataBound"
                    OnPageIndexChanging="gvList_PageIndexChanging" SkinID="Special" AllowPaging="false"
                    PageSize="15">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" ReadOnly="True"
                            Visible="False"></asp:BoundField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input id="chkAll" runat="server" type="checkbox" onclick="javascript:GridSelectAll(this);" />
                                Properties
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkProperty" runat="server" Text='<%# Eval("DisplayName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Commands">
                            <ItemTemplate>
                                <asp:CheckBoxList runat="server" ID="cblCommand" DataTextField="DisplayName" DataValueField="Id"
                                    RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="8">
                                </asp:CheckBoxList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
	    <td colspan="2">
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Security/RoleList.aspx" runat="server">Back</asp:HyperLink>
        </td>
	</tr>
    </table>
</asp:Content>
