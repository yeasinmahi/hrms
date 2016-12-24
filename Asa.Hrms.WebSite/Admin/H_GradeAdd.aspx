<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_GradeAdd.aspx.cs"
    Inherits="GITS.Hrms.WebSite.Admin.H_GradeAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="3" cellspacing="1">
        <tr>
            <td align="right">
                Name:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtName" Text="" MaxLength="50" Width="250px">
			</asp:TextBox><asp:RequiredFieldValidator
                    ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*"
                    ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Sort Order:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtSortOrder" Text="" MaxLength="10" Width="250px">
			</asp:TextBox><asp:RequiredFieldValidator
                    ID="rfvSortOrder" runat="server" Display="Dynamic" ControlToValidate="txtSortOrder"
                    ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator><asp:RangeValidator
                        ControlToValidate="txtSortOrder" ID="rvSortOrder" Type="Integer" MaximumValue="2147483647"
                        MinimumValue="0" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Designation:
            </td>
            <td>
                <table>
                    <tr>
                        <td style="padding-left:15px;">
                            <asp:Repeater ID="rpDesignation" runat="server">
                                <ItemTemplate>
                                    <asp:Label runat="server" Visible="false" ID="lblDesignationId" Text='<%# Eval("Id") %>'></asp:Label>
                                    <li><asp:Label runat="server" ID="lblDesignation" Text='<%# Eval("Name") %>'></asp:Label>
                                    <asp:LinkButton ID="lbDeleteDesignation" runat="server" OnClick="lbDeleteDesignation_Click"
                                        OnClientClick="javascript:ConfirmPostBack('Are you sure you want to remove this designation?', this);return false;"
                                        Text="Remove"></asp:LinkButton></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlDesignation" runat="server" DataTextField="Name" DataValueField="Id">
                            </asp:DropDownList>
                            <asp:LinkButton ID="lbAdd" runat="server" OnClick="lbAdd_Click">Add</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
	                    <td colspan="2">
                            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/H_GradeList.aspx" runat="server">Back</asp:HyperLink>
                        </td>
	                </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
