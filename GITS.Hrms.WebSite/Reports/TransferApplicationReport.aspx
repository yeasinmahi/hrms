<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="TransferApplicationReport.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.TransferApplicationReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="pnlSearch" runat="server" DefaultButton="lbSearch">
<table>
    <tr>
        <td>Employee ID</td>
        <td>
            <asp:TextBox ID="txtEmpId" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:LinkButton ID="lbSearch" runat="server" CausesValidation="False" 
                    onclick="lbSearch_Click">Search</asp:LinkButton></td>
    </tr>
    <tr>
        <td>Designation</td>
        <td>
            <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>Letter No</td>
        <td>
            <asp:TextBox ID="txtLetterNo" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>ASA District</td>
        <td>
            <asp:TextBox ID="txtAsaDistrict" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>Own District</td>
        <td>
            <asp:TextBox ID="txtOwnDistrict" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>Demanded Place</td>
        <td>
            <asp:TextBox ID="txtDemandedPlace" runat="server"></asp:TextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>Status</td>
        <td>
            <asp:DropDownList ID="ddlStatus" runat="server" Width="120px">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Panel>
<asp:Panel ID="pnlEmp" runat="server">
        <asp:GridView ID="gvList" runat="server" OnSorting="gvList_Sorting" SkinID="Report">
            <Columns>
            <asp:TemplateField HeaderText="SL">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>.
                </ItemTemplate>
                <ItemStyle Width="2%" />
            </asp:TemplateField>
                
                <mms:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="Name"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="Code" HeaderText="Emp.ID" SortExpression="Code" FieldType="Int32">
                </mms:BoundField>
                <mms:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="ApplicationNo" HeaderText="Application No" SortExpression="ApplicationNo"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="ReceivingDate" HeaderText="Receiving Date" SortExpression="ReceivingDate"
                    DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime">
                </mms:BoundField>
                <mms:BoundField DataField="Branch" HeaderText="Branch Name" SortExpression="Branch"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="BranchDate" HeaderText="Branch Date" SortExpression="BranchDate"
                    DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime">
                </mms:BoundField>
                <mms:BoundField DataField="ASADistrict" HeaderText="ASA District" SortExpression="ASADistrict"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="DistrictDate" HeaderText="District Date" SortExpression="DistrictDate"
                    DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime">
                </mms:BoundField>
                <mms:BoundField DataField="BranchThana" HeaderText="Branch Thana" SortExpression="BranchThana"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="OwnThana" HeaderText="Own Thana" SortExpression="OwnThana"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="OwnDistrict" HeaderText="Own District" SortExpression="OwnDistrict"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="DemandedPlace" HeaderText="Demanded Place" SortExpression="DemandedPlace"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks"
                    FieldType="String">
                </mms:BoundField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
