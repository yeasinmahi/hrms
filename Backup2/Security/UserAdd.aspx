<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    Inherits="UserAdd" Title="" Codebehind="UserAdd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="3" cellspacing="1">
        <tr>
            <td align="right">
                User Type</td>
            <td>
                <asp:DropDownList ID="ddlUserType" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Login:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtLogin" Text="" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLogin" runat="server" Display="Dynamic" ControlToValidate="txtLogin"
                    ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Name:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtName" Text="" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName"
                    ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr runat="server" id="trPassword">
            <td align="right">
                Password:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPassword" Text="" MaxLength="50" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" Display="Dynamic" ControlToValidate="txtPassword"
                    ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
		<td align="right">Is Active:</td>
		<td>
			<asp:CheckBox runat="server" ID="chkIsActive" Checked="true" Text=""></asp:CheckBox>
		</td>
	</tr>
        <tr>
            <td align="right" valign="top">
                Roles:
            </td>
            <td>
                <asp:CheckBoxList runat="server" ID="cblRole" DataTextField="Name" DataValueField="Name">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                Location:
            </td>
            <td>
                <table>
                <tr>
                        <td colspan="2">
                            <asp:RadioButton ID="rbAll" runat="server" Text="All" Checked="true"
                                AutoPostBack="true" OnCheckedChanged="rbAll_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbZone" runat="server" Text="Zone" 
                                AutoPostBack="true" OnCheckedChanged="rbZone_CheckedChanged" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="True" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbSubzone" runat="server" Text="District" 
                                AutoPostBack="true" OnCheckedChanged="rbSubzone_CheckedChanged" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSubzone" runat="server" AutoPostBack="True" DataTextField="Name" DataValueField="Id" OnSelectedIndexChanged="ddlSubzone_SelectedIndexChanged"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbRegion" runat="server" Text="Region" 
                                AutoPostBack="true" OnCheckedChanged="rbRegion_CheckedChanged" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="True" DataTextField="Name" 
                    DataValueField="Id" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rbBranch" runat="server" Text="Branch" 
                                AutoPostBack="true" OnCheckedChanged="rbBranch_CheckedChanged" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="Name" DataValueField="Id"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
	    <td colspan="2">
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Security/UserList.aspx" runat="server">Back</asp:HyperLink>
        </td>
	</tr>
    </table>
</asp:Content>
