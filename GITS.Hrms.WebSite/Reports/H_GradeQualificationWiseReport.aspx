<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_GradeQualificationWiseReport.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.H_GradeQualificationWiseReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table >
        <tr>
            <td>
                Grade</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:DropDownList ID="ddlGrade" runat="server" AppendDataBoundItems="True" 
                    DataTextField="Name" DataValueField="Id" Width="100px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Designation</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Masters</td>
            <td>
                <asp:DropDownList ID="ddlMastersFilter" runat="server">
                    <asp:ListItem Value="1">P.Grade Date</asp:ListItem>
                    <asp:ListItem Value="2">Joining Date</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtMastersDate" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtMastersDate" ID="rvMastersDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data">
			    </asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Bachelor</td>
            <td>
                <asp:DropDownList ID="ddlBachelorFilter" runat="server">
                    <asp:ListItem Value="1">P.Grade Date</asp:ListItem>
                    <asp:ListItem Value="2">Joining Date</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtBachelorDate" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtBachelorDate" ID="rvBachelorDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data">
			    </asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                HSC</td>
            <td>
                <asp:DropDownList ID="ddlHSCFilter" runat="server">
                    <asp:ListItem Value="1">P.Grade Date</asp:ListItem>
                    <asp:ListItem Value="2">Joining Date</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtHSCDate" runat="server" AutoCompleteType="Cellular"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtHSCDate" ID="rvHSCDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data">
			    </asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Below HSC</td>
            <td>
                <asp:DropDownList ID="ddlBelowHSCFilter" runat="server" >
                    <asp:ListItem Value="1">P.Grade Date</asp:ListItem>
                    <asp:ListItem Value="2">Joining Date</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtBelowHSCDate" runat="server"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtBelowHSCDate" ID="rvBelowHSCDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data">
			    </asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
