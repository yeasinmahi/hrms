<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_TransferReport.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.H_TransferReport" Title="Transfer Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlSerach" runat="server" DefaultButton="lbSearch">
    <table >
        <tr>
            <td>
                Employee ID</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtEmpId" runat="server" ></asp:TextBox>
              <%--  <asp:RangeValidator ID="rvEmpId" ControlToValidate="txtEmpId" runat="server" Type="Integer" MinimumValue="1" MaximumValue="99999" ErrorMessage="*"></asp:RangeValidator>--%>
            </td>
            <td>
                <asp:LinkButton ID="lbSearch" runat="server" 
                    onclick="lbSearch_Click">Search</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                Branch</td>
            <td>
                <asp:DropDownList ID="ddlBranchFilter" runat="server">
                    <asp:ListItem Value="1">Contains</asp:ListItem>
                    <asp:ListItem Value="2">Equal</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtBranch" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                ASA District</td>
            <td>
                <asp:DropDownList ID="ddlAsaDistrictFilter" runat="server">
                    <asp:ListItem Value="1">Contains</asp:ListItem>
                    <asp:ListItem Value="2">Equal</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtAsaDistrict" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Employee Status</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlEmp" runat="server">
        <asp:GridView ID="gvList" runat="server" OnSorting="gvList_Sorting" SkinID="Report">
            
        </asp:GridView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
