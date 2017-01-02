<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="H_PunishmentReport.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.H_PunishmentReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">
function printDiv(divID) {
//Get the HTML of div
var divElements = document.getElementById(divID).innerHTML;
//Get the HTML of whole page
var oldPage = document.body.innerHTML;
//Reset the page's HTML with div's HTML only
document.body.innerHTML =
"<html><head><title>Employee Punishment Report</title></head><body>" +
divElements + "</body>";
//Print Page
window.print();
//Restore orignal HTML
document.body.innerHTML = oldPage;
}
</script>
<div id="Indiv">
<asp:Panel ID="pnlSerach" runat="server" DefaultButton="lbSearch">
    <table >
        <tr>
            <td>
                Employee ID</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtEmpId" runat="server" MaxLength="5"></asp:TextBox>
                <asp:RangeValidator ID="rvEmpId" runat="server" ControlToValidate="txtEmpId" 
                    ErrorMessage="*" MaximumValue="99999" MinimumValue="1" Type="Integer"></asp:RangeValidator>
            </td>
            <td>
                <asp:LinkButton ID="lbSearch" runat="server" CausesValidation="False" 
                    onclick="lbSearch_Click">Search</asp:LinkButton>
            </td>
            <td style="width: 126px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Employee Name</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Enabled="false" ReadOnly="True" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td>
                Designation</td>
            <td style="width: 126px">
                <asp:TextBox ID="txtDesignation" Enabled="false" runat="server" ReadOnly="True" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td rowspan="4">
                <asp:Image ID="imgPhoto" runat="server" BorderWidth="1px" Height="90px" 
                    Width="90px" />
            </td>
        </tr>
        <tr>
            <td>
                Grade</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtGrade" runat="server" Enabled="false" ReadOnly="True" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td>
                Department</td>
            <td style="width: 126px">
                <asp:TextBox ID="txtDept" runat="server" Enabled="false" ReadOnly="True" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Branch</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtBranch" runat="server" Enabled="false" ReadOnly="True" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td>
                ASA District</td>
            <td style="width: 126px">
                <asp:TextBox ID="txtAsaDistrict" runat="server" Enabled="false" ReadOnly="True" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Own Thana</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtThana" runat="server" Enabled="false" ReadOnly="True" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td>
                Own District</td>
            <td style="width: 126px">
                <asp:TextBox ID="txtOwnDistrict" Enabled="false" runat="server" ReadOnly="True" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Date of Birth</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:TextBox ID="txtBirthDate" Enabled="false" runat="server" ReadOnly="True" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
            </td>
            <td>
                Joining Date</td>
            <td style="width: 126px">
                <asp:TextBox ID="txtJoiningDate" Enabled="false" runat="server" ReadOnly="True" Width="200px" ForeColor="ActiveCaptionText"></asp:TextBox>
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
            <td style="width: 126px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
 <asp:Panel ID="pnlTras" runat="server" GroupingText="Punishment Transfer">
     <asp:GridView ID="gvTransfer" Width="100%" runat="server" SkinID="Report">
     </asp:GridView>
 </asp:Panel>
 <asp:Panel ID="pnlLeave" runat="server" GroupingText="Force Leave/Suspension">
     <asp:GridView ID="gvLeav" Width="100%" runat="server" SkinID="Report">
     </asp:GridView>
 </asp:Panel>
 <asp:Panel ID="pnlPenalty" runat="server" GroupingText="Penalty Information">
     <asp:GridView ID="gvPenalty" Width="100%" runat="server" SkinID="Report">
     </asp:GridView>
 </asp:Panel>
 <asp:Panel ID="pnlWarning" runat="server" GroupingText="Warning Information">
     <asp:GridView ID="gvWarning" Width="100%" runat="server" SkinID="Report">
     </asp:GridView>
 </asp:Panel>
 <asp:Panel ID="Panel1" runat="server" GroupingText="Inceament Heldup Information">
     <asp:GridView ID="gvIncreamentHeldup" Width="100%" runat="server" SkinID="Report">
     </asp:GridView>
 </asp:Panel>
 <asp:Panel ID="Panel2" runat="server" GroupingText="Demotion Information">
     <asp:GridView ID="gvPromotion" Width="100%" runat="server" SkinID="Report">
     </asp:GridView>
 </asp:Panel>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
