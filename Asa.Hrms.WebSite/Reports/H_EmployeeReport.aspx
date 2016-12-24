<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_EmployeeReport.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.H_EmployeeReport" Title="Untitled Page" %>
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
                <%--<asp:RangeValidator ID="rvEmpId" ControlToValidate="txtEmpId" runat="server" Type="Integer" MinimumValue="1" MaximumValue="99999" ErrorMessage="*"></asp:RangeValidator>--%>
            </td>
            <td>
                <asp:LinkButton ID="lbSearch" runat="server" CausesValidation="False" 
                    onclick="lbSearch_Click">Search</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                Employee Name</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtEmpName" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Grade</td>
            <td>
                 <asp:DropDownList ID="ddlGradeFilter" runat="server" 
                     onselectedindexchanged="ddlGradeFilter_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="1">Equal</asp:ListItem>
                    <asp:ListItem Value="2">In</asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:DropDownList ID="ddlGrade" runat="server" AppendDataBoundItems="True" 
                    DataTextField="Name" DataValueField="Id" Width="100px">
                </asp:DropDownList>
                <asp:TextBox ID="txtGrade" runat="server" Visible="False"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Designation</td>
            <td>
                <asp:DropDownList ID="ddlDesigFilter" runat="server">
                    <asp:ListItem Value="1">Contains</asp:ListItem>
                    <asp:ListItem Value="2">Equal</asp:ListItem>
                     <asp:ListItem Value="3">In</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
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
                Own District</td>
            <td>
                <asp:DropDownList ID="ddlOwnDistrictFilter" runat="server">
                    <asp:ListItem Value="1">Contains</asp:ListItem>
                    <asp:ListItem Value="2">Equal</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtOwnDistrict" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Own Thana</td>
            <td>
                <asp:DropDownList ID="ddlOwnThanaFilter" runat="server">
                    <asp:ListItem Value="1">Contains</asp:ListItem>
                    <asp:ListItem Value="2">Equal</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txtOwnThana" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Employee
                Status</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Employment Type</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:DropDownList ID="ddlEmploymentType" runat="server">
                </asp:DropDownList>
            </td>
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
                <mms:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" FieldType="Int32">
                </mms:BoundField>
                <mms:BoundField DataField="Name" HeaderText="Employee Name" SortExpression="Name"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="DateOfBirth" HeaderText="Date of Birth" SortExpression="DateOfBirth"
                    DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime">
                </mms:BoundField>
                <mms:BoundField DataField="JoiningDate" HeaderText="Joining Date" SortExpression="JoiningDate"
                    DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime">
                </mms:BoundField>
                <mms:BoundField DataField="Grade" HeaderText="Grade" SortExpression="Grade" FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="GradeDate" HeaderText="Grade Date" SortExpression="GradeDate"
                    DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime">
                </mms:BoundField>
                <mms:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="BranchName" HeaderText="Branch Name" SortExpression="BranchName"
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
                <mms:BoundField DataField="OwnThana" HeaderText="Own Thana" SortExpression="OwnThana"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="OwnDistrict" HeaderText="Own District" SortExpression="OwnDistrict"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="BranchMobile" HeaderText="Br.Mobile" SortExpression="BranchMobile"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="Qualification" HeaderText="Qualification" SortExpression="Qualification"
                    FieldType="String">
                </mms:BoundField>
                <mms:BoundField DataField="Personal_Contact" HeaderText="Personal Contact" SortExpression="Personal_Contact"
                    FieldType="String">
                </mms:BoundField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
