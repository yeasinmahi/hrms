<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="H_LetterReport.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.H_LetterReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function ConfirmDelete() {
            var flag = window.confirm("Are you sure you want to Delete this row?");
            if (flag) {
                return true;
            }
            else {
                return false;
            }
        }

    </script>

    <table>
        <tr id="trReportType" runat="server">
            <td>
                Report Type
            </td>
            <td colspan="4">
                <asp:DropDownList ID="ddlReportType" runat="server" Width="200px" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server">
            <td style="width: 60pt;">
                Report Format
            </td>
            <td colspan="4">
                <asp:DropDownList ID="ddlReportName" runat="server" DataTextField="Name" DataValueField="Id"
                    Width="405px" AutoPostBack="True" OnSelectedIndexChanged="ddlReportName_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server">
            <td>
                Letter No
            </td>
            <td width="350px">
                <asp:TextBox ID="txtLetterNo" runat="server"></asp:TextBox>
            </td>
            <td>
                Letter Date
                <asp:TextBox ID="txtLetterDate" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td rowspan="10">
                অনুলিপিঃ
                <br />
                <asp:CheckBoxList ID="chkDuplication" runat="server" RepeatLayout="Flow">
                    <asp:ListItem Value="1">১। সংশ্লিষ্ট ইভিপি/কেন্দ্রীয় বাস্তবায়নকারী (অপারেশন)।</asp:ListItem>
                    <asp:ListItem Value="2">২। সংশ্লিষ্ট জেডএম, আশা।</asp:ListItem>
                    <asp:ListItem Value="3">৩। সংশ্লিষ্ট ডিএম, আশা।</asp:ListItem>
                    <asp:ListItem Value="4">৪। সংশ্লিষ্ট ব্রাঞ্চ, আশা।</asp:ListItem>
                    <asp:ListItem Value="5">৫। পিএমআইএস।</asp:ListItem>
                    <asp:ListItem Value="6">৬। ব্যক্তিগত নথি।</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr runat="server">
            <td>
                Address To
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtAddressTo" runat="server" Width="600px"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server">
            <td>
                Subject
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtSubject" runat="server" Width="600px"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server">
            <td>
                Letter Body
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Width="600px" Rows="4"></asp:TextBox>
            </td>
        </tr>
        <tr runat="server">
            <td colspan="4">
                &nbsp;
                
                <br />
                
            </td>
        </tr>
        <tr runat="server">
            <td>
                Conclution
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtConlution" runat="server" Width="600px"></asp:TextBox>
            </td>
        </tr>
        <tr id="trNote" runat="server">
            <td>
                Note
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr id="trDMNote" runat="server">
            <td>
                Special Note
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDMNote" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Report Type
            </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlFormat" runat="server" Width="100px">
                    <asp:ListItem Value="1">PDF</asp:ListItem>
                    <asp:ListItem Value="2">MS Word</asp:ListItem>
                    <asp:ListItem Value="3">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="height: 400px;">
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
    </table>
    <table>
            <tr>
                <td>
                    <asp:Panel runat="server" ID="pnlExtenderList" ScrollBars="Vertical" Style="overflow: hidden;
                        height: 0px; width: 0px; z-index: 99999">
                    </asp:Panel>

                    <script type="text/javascript">
                        function AdjustWidth(source, eventArgs) {
                            document.getElementById('ctl00_ContentPlaceHolder1_pnlExtenderList').style.height = '200px';
                            document.getElementById('ctl00_ContentPlaceHolder1_pnlExtenderList').style.width = '700px';
                        }
                    </script>

                </td>
            </tr>
        </table>
    <asp:Panel ID="pnl" runat="server" GroupingText="Transfer List">
    <asp:GridView ID="gvTransfer" runat="server" Width="600px" OnRowCommand="gvTransfer_RowCommand"
                    AutoGenerateColumns="False" SkinID="AddPageGrid"  DataKeyNames="Id" >
                    <Columns>
                        <asp:TemplateField HeaderText="Emp ID">
                            <FooterTemplate>
                                <asp:TextBox ID="txtCode" Width="55px" runat="server" onchange="javascript:__doPostBack('ctl00$ContentPlaceHolder1$gvTransfer$ctl03$lbEmpSearch', '')"></asp:TextBox>
                                <asp:LinkButton ID="lbEmpSearch" runat="server" CausesValidation="false" Text=""
                                    OnClick="lbEmpSearch_Click"><img alt="Search" src="../Images/search.png" height="10px" width="10px" style="margin-bottom:0px; margin-top:5px; padding-bottom:0px; padding-top:0px;" /></asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <FooterTemplate>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designation">
                            <FooterTemplate>
                                <asp:TextBox ID="txtDesignation" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Present District">
                            <FooterTemplate>
                                <asp:TextBox ID="txtPresentDistrict" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPresentDistrict" runat="server" Text='<%# Eval("PresentDistrict") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Present Branch">
                            <FooterTemplate>
                                <asp:TextBox ID="txtPresentBranch" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPresentBranch" runat="server" Text='<%# Eval("PresentBranch") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transferred District">
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlDistrict" runat="server" DataTextField="Name" DataValueField="Id"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DestinationDistrict") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transferred Branch">
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlBranch" DataTextField="Name" DataValueField="Id" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("DestinationBranch") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            
                            <FooterTemplate>
                                <asp:LinkButton ID="linkAdd" runat="server" CausesValidation="false" CommandName="addrow"
                                    Text="Add">Add</asp:LinkButton>
                                     
                            </FooterTemplate>
                            <ItemTemplate>
                               <asp:LinkButton ID="LinkDelete" CausesValidation="false" EnableViewState="true" runat="server" CommandName="deleterow"
                                    Text="Delete" CommandArgument='<%# Eval("Id") %>' >Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="cvbcvn">
                        <ItemTemplate>
                        <asp:LinkButton ID="LinkDelete" CausesValidation="false" EnableViewState="true" runat="server" CommandName="deleterow"
                                    Text="Delete" CommandArgument='<%# Eval("Id") %>' >Delete</asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
    </asp:Panel>
</asp:Content>
