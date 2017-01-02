<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_BranchReport.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.H_BranchReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table>
    <tr>
        <td>Branch Name</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtBranchName" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Region</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtRegion" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>ASA District</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtAsaDistrict" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Zone</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtZone" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Govt. District</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtOwnDistrict" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Govt. Thana</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtOwnThana" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Opening Date</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtOpeningDate" runat="server" AutoPostBack="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>Branch Type</td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="ddlBranchType" runat="server" >
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Branch Location</td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="ddlLocation" runat="server">
            </asp:DropDownList>
        </td>
    </tr> 
    <tr>
        <td>Status</td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="ddlStatus" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td>
           
        </td>
    </tr>
</table>
    <asp:Panel ID="pnlBranch" GroupingText="Branch List" runat="server">
    </asp:Panel>
</asp:Content>

