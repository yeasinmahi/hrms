<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_LoanAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.P_LoanAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="50" Width="250px">
			</asp:TextBox><asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Interest Rate:</td>
		<td>
			<asp:TextBox ID="txtInterestRate" runat="server"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvtxtInterestRate" runat="server" Display="Dynamic" ControlToValidate="txtInterestRate" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator><asp:RangeValidator ControlToValidate="txtInterestRate" ID="RangeValidator1" Type="Double" MaximumValue="10" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data"></asp:RangeValidator>
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
		<td align="right">Status</td>
		<td>
			<asp:DropDownList ID="ddlStatus" runat="server" Width="100px">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
            <td align="right">
                &nbsp;</td>
            <td>
                <table>
                    <tr>
                        <td style="padding-left:15px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
	                    <td colspan="2">
                            &nbsp;</td>
	                </tr>
                </table>
            </td>
        </tr>
	</table>
</asp:Content>

