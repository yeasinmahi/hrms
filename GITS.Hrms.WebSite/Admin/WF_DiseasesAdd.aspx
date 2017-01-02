<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="WF_DiseasesAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.Admin.WF_DiseasesAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <asp:DropDownList ID="ddlStatus" runat="server">
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
                            &nbsp;</td>
                    </tr>
                    <tr>
	                    <td colspan="2">
                            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/WF_DiseasesList.aspx" runat="server">Back</asp:HyperLink>
                        </td>
	                </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

