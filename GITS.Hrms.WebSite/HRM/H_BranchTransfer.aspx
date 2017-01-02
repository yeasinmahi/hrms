<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_BranchTransfer.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_BranchTransfer" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table >
        <tr>
            <td colspan="2">
               <b>Branch Information</b> </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                ASA Zone</td>
            <td>
                <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="True" 
                    DataTextField="Name" DataValueField="Id" 
                    onselectedindexchanged="ddlZone_SelectedIndexChanged" Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                ASA District</td>
            <td>
                <asp:DropDownList ID="ddlSubzone" runat="server" DataTextField="Name" 
                    DataValueField="Id" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlSubzone_SelectedIndexChanged" Width="200px"></asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Region</td>
            <td>
                <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Name" 
                    DataValueField="Id" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" Width="200px"></asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Branch</td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="Name" 
                    DataValueField="Id" Width="200px" AutoPostBack="True"
                    onselectedindexchanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Thana</td>
            <td>
                <asp:TextBox ID="txtThana" runat="server" ReadOnly="True" Width="200px"></asp:TextBox>
            </td>
            <td>
                Govt. District</td>
            <td>
                <asp:TextBox ID="txtDistrict" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
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
        <tr>
            <td colspan="2">
               <b> Transfer Information</b></td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Letter No</td>
            <td>
                <asp:TextBox ID="txtLetterNo" runat="server" MaxLength="50" 
                    ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLetterNo" runat="server" ControlToValidate="txtLetterNo" Display="Dynamic"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Letter Date</td>
            <td>
                <asp:TextBox ID="txtLetterDate" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/minical.gif" />
                    <ajaxc:CalendarExtender ID="ImageButton1_CalendarExtender" runat="server" TargetControlID="txtLetterDate" Format="dd/MM/yyyy" PopupButtonID="ImageButton1"></ajaxc:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server"  ControlToValidate="txtLetterDate"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:RangeValidator ID="rvLetterDate" runat="server" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" ControlToValidate="txtLetterDate" ErrorMessage="*"></asp:RangeValidator>
                
                
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                ASA Zone</td>
            <td>
                <asp:DropDownList ID="ddlTransZone" runat="server" AutoPostBack="True" 
                    DataTextField="Name" DataValueField="Id" 
                    onselectedindexchanged="ddlTransZone_SelectedIndexChanged" Width="200px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvZone" runat="server" Display="Dynamic" ControlToValidate="ddlTransZone" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                ASA District</td>
            <td>
                <asp:DropDownList ID="ddlTranSubzone" runat="server"  AutoPostBack="True"
                    onselectedindexchanged="ddlTranSubzone_SelectedIndexChanged" Width="200px" 
                    DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvSubzone" runat="server" Display="Dynamic" ControlToValidate="ddlTranSubzone" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Region</td>
            <td>
                <asp:DropDownList ID="ddlTransRegion" runat="server" Width="200px" 
                    AutoPostBack="True" DataTextField="Name" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvRegion" runat="server" Display="Dynamic" ControlToValidate="ddlTransRegion" InitialValue="0" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvTransRegion" ControlToValidate="ddlTransRegion" ControlToCompare="ddlRegion" Operator="NotEqual" Display="Dynamic" runat="server" ErrorMessage="*"></asp:CompareValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Transfer Date</td>
            <td>
                <asp:TextBox ID="txtTransferDate" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/minical.gif" />
                    <ajaxc:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtTransferDate" Format="dd/MM/yyyy" PopupButtonID="ImageButton2"></ajaxc:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvTransDate" runat="server" ControlToValidate="txtTransferDate" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvTransDate" runat="server" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" Display="Dynamic" ControlToValidate="txtTransferDate" ErrorMessage="*"></asp:RangeValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
