<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_DesignationAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.Admin.H_DesignationAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="50" Width="250px">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Name in Bangla</td>
		<td>
			<asp:TextBox runat="server" ID="txtBanglaName" Text="" MaxLength="50" 
                Width="250px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvBanglaName" runat="server" Display="Dynamic" ControlToValidate="txtBanglaName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Short Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtShortName" Text="" MaxLength="50" Width="250px">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvShortName" runat="server" Display="Dynamic" ControlToValidate="txtShortName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Sort Order:</td>
		<td>
			<asp:TextBox runat="server" ID="txtSortOrder" Text="" MaxLength="10" Width="250px">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvSortOrder" runat="server" Display="Dynamic" ControlToValidate="txtSortOrder" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator><asp:RangeValidator ControlToValidate="txtSortOrder" ID="rvSortOrder" Type="Integer" MaximumValue="2147483647" MinimumValue="0" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Group Type</td>
		<td>
			<asp:DropDownList ID="ddlGroupType" runat="server" 
                 Width="100px">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Status</td>
		<td>
			<asp:DropDownList ID="ddlStatus" runat="server" Width="100px">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
            <td align="right">
                Grade:
            </td>
            <td>
                <table>
                    <tr>
                        <td style="padding-left:15px;">
                            <asp:Repeater ID="rpGrade" runat="server">
                                <ItemTemplate>
                                    <asp:Label runat="server" Visible="false" ID="lblGradeId" Text='<%# Eval("Id") %>'></asp:Label>
                                    <li><asp:Label runat="server" ID="lblGrade" Text='<%# Eval("Name") %>'></asp:Label>
                                    <asp:LinkButton ID="lbDeleteGrade" runat="server" OnClick="lbDeleteGrade_Click"
                                        OnClientClick="javascript:ConfirmPostBack('Are you sure you want to remove this grade?', this);return false;"
                                        Text="Remove"></asp:LinkButton></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlGrade" runat="server" DataTextField="Name" DataValueField="Id">
                            </asp:DropDownList>
                            <asp:LinkButton ID="lbAdd" runat="server" OnClick="lbAdd_Click">Add</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
	                    <td colspan="2">
                            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/H_DesignationList.aspx" runat="server">Back</asp:HyperLink>
                        </td>
	                </tr>
                </table>
            </td>
        </tr>
	</table>
</asp:Content>
