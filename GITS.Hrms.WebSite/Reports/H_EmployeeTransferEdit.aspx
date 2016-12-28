<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="H_EmployeeTransferEdit.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.H_EmployeeTransferEdit" %>

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

    <div id="div1" runat="server">
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td align="right">
                    Employee:
                </td>
                <td colspan="3">
                    <asp:Panel ID="Panel1" runat="server">
                        <asp:TextBox ID="txtEmployee" runat="server" MaxLength="250" autocomplete="off" Width="300px" onchange="javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbSearch', '')" ></asp:TextBox>
                        <ajaxc:AutoCompleteExtender ID="aceEmployee" runat="server" TargetControlID="txtEmployee"
                            ServicePath="~/Services/SearchService.asmx" ServiceMethod="GetSuggestions" MinimumPrefixLength="1"
                            CompletionInterval="0" EnableCaching="true" CompletionSetCount="10" DelimiterCharacters=","
                            CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="pnlExtenderList"
                            OnClientShown="AdjustWidth">
                        </ajaxc:AutoCompleteExtender>
                        <asp:RequiredFieldValidator ID="rfvEmployeeName" runat="server" Display="Dynamic"
                            ControlToValidate="txtEmployee" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <%--<asp:LinkButton ID="lbSearch" runat="server" CausesValidation="False" OnClick="lbSearch_Click">Search</asp:LinkButton>--%>
                        <asp:LinkButton ID="lbSearch" runat="server" CausesValidation="False" onclick="lbSearch_Click" style="display: none">Search</asp:LinkButton>
                    </asp:Panel>
                </td>
                <td>
                    <asp:LinkButton ID="btnTransfer" runat="server" OnClick="btnTransfer_Click">Transfer Info</asp:LinkButton>
                </td>
                <td>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnLeave" runat="server" OnClick="btnLeave_Click">Leave Info</asp:LinkButton>
                </td>
                <td>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnPenalty" runat="server" OnClick="btnPenalty_Click">Penalty Info</asp:LinkButton>
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="btnWarning" runat="server" OnClick="btnWarning_Click">Warning Info</asp:LinkButton>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="btnPromotion" runat="server" OnClick="btnPromotion_Click">Promotion</asp:LinkButton>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                    <asp:LinkButton ID="btnConsultancy" runat="server" OnClick="btnConsultancy_Click">Consultancy</asp:LinkButton>
                    &nbsp;
                </td>
                <td>
                    <asp:LinkButton ID="btnDropOut" runat="server" 
                        OnClick="btnDropOut_Click">Drop-Out</asp:LinkButton>
                </td>
                <td>
                     &nbsp;<asp:LinkButton ID="btnRejoin" runat="server" 
                        OnClick="btnRejoin_Click">Rejoin</asp:LinkButton>
                        
                        &nbsp; &nbsp;
                    <asp:LinkButton ID="btnProtionEdit" runat="server" OnClick="btnProtionEdit_Click">Promotion Edit</asp:LinkButton>
                    </td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="height: 17px">
                    <strong>Current Information</strong>
                </td>
                <td align="left" style="height: 17px">
                    &nbsp;
                </td>
                <td align="left" style="height: 17px">
                    &nbsp;
                </td>
                <td align="left" style="height: 17px">
                    <asp:LinkButton ID="btnTransferEdit" runat="server" 
                        onclick="btnTransferEdit_Click">Transfer Edit</asp:LinkButton>
                </td>
                <td align="left" style="height: 17px">
                &nbsp;
                    <asp:LinkButton ID="btnIncrementHeldup" runat="server" 
                        onclick="btnIncrementHeldup_Click">Inc.Heldup</asp:LinkButton>
                </td>
                <td align="left" style="height: 17px">
                    &nbsp;
                </td>
                <td align="left" style="height: 17px">
                    &nbsp;
                </td>
                <td align="left" style="height: 17px">
                    &nbsp;
                </td>
                <td align="left" style="height: 17px">
                    &nbsp;
                </td>
                <td align="left" style="height: 17px">
                    &nbsp;</td>
                <td align="left" style="height: 17px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                    Designation
                </td>
                <td>
                    <asp:TextBox ID="txtDesignation" runat="server" ForeColor="ActiveCaptionText" ReadOnly="True"
                        Enabled="False"></asp:TextBox>
                </td>
                <td align="right">
                    Status
                </td>
                <td align="left">
                    <asp:TextBox ID="txtStatus" runat="server" ForeColor="ActiveCaptionText" ReadOnly="True"
                        Enabled="False"></asp:TextBox>
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    &nbsp;
                </td>
                 <td align="left">
                    &nbsp;
                </td>
                 <td align="left">
                     &nbsp;</td>
                 <td align="left">
                     &nbsp;</td>
            </tr>
            <tr>
                <td align="right">
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="right">
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                 <td>
                    &nbsp;
                </td>
                 <td>
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
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
    </div>
    <asp:Panel ID="pnlBranch" GroupingText="Employee Branch History" runat="server">
        <asp:GridView ID="gvBranch" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,EmployeeId,BranchId,DistrictId"
            OnRowCancelingEdit="gvBranch_RowCancelingEdit" OnRowCommand="gvBranch_RowCommand"
            OnRowEditing="gvBranch_RowEditing" OnRowUpdating="gvBranch_RowUpdating" SkinID="Special">
            <Columns>
                <asp:TemplateField HeaderText="District">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch">
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlBranch" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Width="70px" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStartDate" runat="server" Width="70px" Text='<%# Eval("StartDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtStartDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtStartDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date">
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Width="70px" Text='<%# Bind("EndDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEndDate" runat="server" Width="70px" Text='<%# Eval("EndDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic"
                            ControlToValidate="txtEndDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtEndDate" ID="rvJoiningDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlTransfer" GroupingText="Transfer History" runat="server">
        <asp:GridView ID="gvTransfer" runat="server" AutoGenerateColumns="False" OnRowEditing="gvTransfer_RowEditing"
            DataKeyNames="Id,SourceBranchId,DestinationBranchId,SourceDistrictId,DestinationDistrictId"
            OnRowUpdating="gvTransfer_RowUpdating" OnRowCancelingEdit="gvTransfer_RowCancelingEdit"
            OnRowCommand="gvTransfer_RowCommand"  SkinID="Special">
            <Columns>
            
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From District">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlFromDistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFromDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("sDistrictName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From Branch">
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("sBranchName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlFromBranch" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To District">
                    <ItemTemplate>
                        <asp:Label ID="lbltoDistrict" runat="server" Text='<%# Eval("dDistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlToDistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlToDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Branch">
                    <ItemTemplate>
                        <asp:Label ID="lbltoBranch" runat="server" Text='<%# Eval("dBranchName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlToBranch" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transfer Date">
                    <ItemTemplate>
                        <asp:Label ID="lblJoiningDate" runat="server" Width="70px" Text='<%# Bind("JoiningDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtJoiningDate" runat="server" Width="70px" Text='<%# Eval("JoiningDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic"
                            ControlToValidate="txtJoiningDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtJoiningDate" ID="rvJoiningDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlLeave" GroupingText="Leave History" runat="server">
        <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,H_EmployeeId,Type"
            OnRowCancelingEdit="gvLeave_RowCancelingEdit" OnRowEditing="gvLeave_RowEditing"
            OnRowUpdating="gvLeave_RowUpdating" OnRowCommand="gvLeave_RowCommand" SkinID="Special" ShowFooter="true">
            <Columns>
                <asp:TemplateField HeaderText="Type">
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlTypeAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                         <asp:TextBox ID="txtLetterNoAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                         <asp:TextBox ID="txtLetterDateAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Width="70px" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStartDate" runat="server" Width="70px" Text='<%# Eval("StartDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" Display="Dynamic" ControlToValidate="txtStartDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtStartDate" ID="rvStartDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                         <asp:TextBox ID="txtStartDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date">
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Width="70px" Text='<%# Bind("EndDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEndDate" runat="server" Width="70px" Text='<%# Eval("EndDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" Display="Dynamic" ControlToValidate="txtEndDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtEndDate" ID="rvEndDate" Type="Date" MaximumValue="31/12/9999"
                            MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                         <asp:TextBox ID="txtEndDateAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                         <asp:LinkButton ID="linkAdd" CausesValidation="false" CommandName="addrow"
                            Text="Add" runat="server">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlPenalty" GroupingText="Employee Penalty History" runat="server">
        <asp:GridView ID="gvPenalty" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,H_EmployeeId,BranchId,DistrictId,FineType"
            OnRowCancelingEdit="gvPenalty_RowCancelingEdit" OnRowCommand="gvPenalty_RowCommand"
            OnRowEditing="gvPenalty_RowEditing" OnRowUpdating="gvPenalty_RowUpdating" SkinID="Special" ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="District">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlDistrictPenalty" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrictPenalty_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlPenaltyDistrictAdd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPenaltyDistrictAdd_SelectedIndexChanged">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch">
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlBranchPenalty" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlPenaltyBranchAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fine Type">
                    <ItemTemplate>
                        <asp:Label ID="lblFineType" runat="server" Text='<%# Eval("FineType") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlFineType" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlFineTypeAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterNoAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount(Tk.)">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("FineAmount") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server" Width="70px" Text='<%# Eval("FineAmount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtAmountAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R.Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblRLetterNo" runat="server" Text='<%# Eval("RemissionLetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRLetterNo" runat="server" Width="70px" Text='<%# Eval("RemissionLetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtRLetterNoAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R.Letter Dt">
                    <ItemTemplate>
                        <asp:Label ID="lblRLetterDate" runat="server" Width="70px" Text='<%# Bind("RemissionLetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRLetterDate" runat="server" Width="70px" Text='<%# Eval("RemissionLetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtRLetterDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R.Amount">
                    <ItemTemplate>
                        <asp:Label ID="lblRAmount" runat="server" Text='<%# Eval("RemissionAmount") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRAmount" runat="server" Width="70px" Text='<%# Eval("RemissionAmount") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtRAmountAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                       <asp:LinkButton ID="linkAdd" CausesValidation="false" CommandName="addrow"
                            Text="Add" runat="server">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
        <h3 style="color:Red;">*R.Letter No, R.Letter Dt এবং R.Amount বলতে জরিমান/ক্ষতিপুরণ 
            মওকুফ-এর পত্র নং, পত্রের তারিখ এবং জরিমানা মওকুফের পরিমান কে বুঝায়*</h3>
    </asp:Panel>
    <asp:Panel ID="pnlWarning" GroupingText="Employee Warning History" runat="server">
        <asp:GridView ID="gvWarning" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,H_EmployeeId,BranchId,DistrictId"
            OnRowCancelingEdit="gvWarning_RowCancelingEdit" OnRowCommand="gvWarning_RowCommand"
            OnRowEditing="gvWarning_RowEditing" OnRowUpdating="gvWarning_RowUpdating" SkinID="Special" ShowFooter="true">
            <Columns>
                <asp:TemplateField HeaderText="District">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlDistrictWarning" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrictWarning_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlDistrictWarningAdd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrictWarningAdd_SelectedIndexChanged">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch">
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlBranchWarning" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlBranchWarningAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterNoAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterDateAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Duration">
                    <ItemTemplate>
                        <asp:Label ID="lblDuration" runat="server" Width="70px" Text='<%# Eval("Duration") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDuration" runat="server" Width="70px" Text='<%# Eval("Duration") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDurationAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Warning Time">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalWarningTime" runat="server" Width="70px" Text='<%# Eval("TotalWarningTime") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTotalWarningTime" runat="server" Width="70px" Text='<%# Eval("TotalWarningTime") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtTotalWarningTimeAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" ItemStyle-Width="70px" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="linkAdd" CausesValidation="false" CommandName="addrow"
                            Text="Add" runat="server">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
                
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
    </asp:Panel>
    
    <asp:Panel ID="pnlGrade" GroupingText="Employee Grade" runat="server">
        <asp:GridView ID="gvGrade" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="Id,H_EmployeeId,H_GradeId" 
            onrowcancelingedit="gvGrade_RowCancelingEdit" onrowcommand="gvGrade_RowCommand" 
            onrowediting="gvGrade_RowEditing" onrowupdating="gvGrade_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="Grade">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlGrade" runat="server" >
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlGradeAdd" runat="server"  >
                        </asp:DropDownList>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GradeName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Start Date">
                    <FooterTemplate>
                        <asp:TextBox ID="txtStartDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Width="70px" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStartDate" runat="server" Width="70px" Text='<%# Eval("StartDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtStartDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtStartDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date">
                    <FooterTemplate>
                        <asp:TextBox ID="txtEndDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Width="70px" Text='<%# Bind("EndDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEndDate" runat="server" Width="70px" Text='<%# Eval("EndDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic"
                            ControlToValidate="txtEndDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtEndDate" ID="rvJoiningDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <FooterTemplate>
                        <asp:LinkButton ID="linkAdd" runat="server" CausesValidation="false" CommandName="addrow" Text="Add">Add</asp:LinkButton>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlDesignation" GroupingText="Employee Designation" runat="server">
        <asp:GridView ID="gvDesignation" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,H_EmployeeId,GradeId,DesignationId"
            OnRowCancelingEdit="gvDesignation_RowCancelingEdit" OnRowCommand="gvDesignation_RowCommand"
            OnRowEditing="gvDesignation_RowEditing" OnRowUpdating="gvDesignation_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="Grade">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlGrade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlGradeAdd" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlGradeAdd_SelectedIndexChanged">
                        </asp:DropDownList>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GradeName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Designation">
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlDesignationAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("DesignationName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlDesignation" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date">
                    <FooterTemplate>
                        <asp:TextBox ID="txtStartDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Width="70px" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStartDate" runat="server" Width="70px" Text='<%# Eval("StartDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtStartDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtStartDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date">
                    <FooterTemplate>
                        <asp:TextBox ID="txtEndDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Width="70px" Text='<%# Bind("EndDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEndDate" runat="server" Width="70px" Text='<%# Eval("EndDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic"
                            ControlToValidate="txtEndDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtEndDate" ID="rvJoiningDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <FooterTemplate>
                        <asp:LinkButton ID="linkAdd" runat="server" CausesValidation="false" CommandName="addrow" Text="Add">Add</asp:LinkButton>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlPromotion" GroupingText="Promotion History" runat="server">
        <asp:GridView ID="gvPromotion" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,OldGradeId,NewGradeId,OldDesignationId,NewDesignationId,Type"
            OnRowCancelingEdit="gvPromotion_RowCancelingEdit" OnRowCommand="gvPromotion_RowCommand"
            OnRowEditing="gvPromotion_RowEditing" OnRowUpdating="gvPromotion_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterNoAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From Grade">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlOldGrade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOldGrade_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("OldGradeName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlOldGradeAdd" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlOldGradeAdd_SelectedIndexChanged">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From Designation">
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("OldDesignationName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlOldDesignation" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlOldDesignationAdd" runat="server" >
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Grade">
                    <ItemTemplate>
                        <asp:Label ID="lbltoDistrict" runat="server" Text='<%# Eval("NewGradeName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlNewGrade" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNewGrade_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlNewGradeAdd" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlNewGradeAdd_SelectedIndexChanged">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Designation">
                    <ItemTemplate>
                        <asp:Label ID="lbltoBranch" runat="server" Text='<%# Eval("NewDesignationName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlNewDesignation" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlNewDesignationAdd" runat="server" >
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Promotion Date">
                    <ItemTemplate>
                        <asp:Label ID="lblJoiningDate" runat="server" Width="70px" Text='<%# Bind("PromotionDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPromotionDate" runat="server" Width="70px" Text='<%# Eval("PromotionDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic"
                            ControlToValidate="txtPromotionDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtPromotionDate" ID="rvJoiningDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPromotionDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlTypeAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                     <FooterTemplate>
                        <asp:LinkButton ID="linkAdd" runat="server" CausesValidation="false" CommandName="addrow" Text="Add">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlConsultancy" runat="server" GroupingText="Consuntancy History">
        <asp:GridView ID="gvConsultancy" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"  
        OnRowCancelingEdit="gvConsultancy_RowCancelingEdit" OnRowCommand="gvConsultancy_RowCommand"
            OnRowEditing="gvConsultancy_RowEditing" OnRowUpdating="gvConsultancy_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Organization">
                    <EditItemTemplate>
                       <asp:TextBox ID="txtNgoName" runat="server" Width="70px" Text='<%# Eval("NgoName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNgoName" runat="server" Text='<%# Eval("NgoName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Organization">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlOrganization" runat="server" DataTextField="Name" DataValueField="Id" >
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblOrganization" runat="server" Text='<%# Eval("Organization") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Start Date">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Width="70px" Text='<%# Bind("StartDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStartDate" runat="server" Width="70px" Text='<%# Eval("StartDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic"
                            ControlToValidate="txtStartDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtStartDate" ID="rvJoiningDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid Start date"></asp:RangeValidator>
                    </EditItemTemplate>
                </asp:TemplateField>
                      <asp:TemplateField HeaderText="Country">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlCountry" runat="server" DataTextField="Name" DataValueField="Id"  >
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlDropout" GroupingText="Drop-Out Information" runat="server">
        <asp:GridView ID="gvDropout" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="Id,Type" onrowcancelingedit="gvDropout_RowCancelingEdit" 
            onrowcommand="gvDropout_RowCommand" onrowediting="gvDropout_RowEditing" onrowupdating="gvDropout_RowUpdating"
            >
            <Columns>
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterNoAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Drop Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDropDate" runat="server" Width="70px" Text='<%# Bind("DropDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDropDate" runat="server" Width="70px" Text='<%# Eval("DropDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDropDate" runat="server" Display="Dynamic"
                            ControlToValidate="txtDropDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtDropDate" ID="rvDropDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDropDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlTypeAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                     <FooterTemplate>
                        <asp:LinkButton ID="linkAdd" runat="server" CausesValidation="false" CommandName="addrow" Text="Add">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
    </asp:Panel>
    
    <asp:Panel ID="pnlRejoin" GroupingText="Rejoin History" runat="server">
        <asp:GridView ID="gvRejoin" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="Id,SourceBranchId,DestinationBranchId,SourceDistrictId,DestinationDistrictId,LeaveTypeId,RejoinTypeId"
              SkinID="Special" onrowcancelingedit="gvRejoin_RowCancelingEdit" 
            onrowediting="gvRejoin_RowEditing" onrowcommand="gvRejoin_RowCommand" 
            onrowupdating="gvRejoin_RowUpdating" ShowFooter="true">
            <Columns>
            <asp:TemplateField HeaderText="Leave Type">
                    <ItemTemplate>
                        <asp:Label ID="lblLeaveType" runat="server" Text='<%# Eval("LeaveType") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRejoinLeaveType" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlRejoinLeaveTypeAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rejoin Type">
                    <ItemTemplate>
                        <asp:Label ID="lblRejoinType" runat="server" Text='<%# Eval("RejoinType") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRejoinType" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlRejoinTypeAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterNoAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterDateAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="L/C. Start Date">
                    <ItemTemplate>
                        <asp:Label ID="lblFromrDate" runat="server" Width="70px" Text='<%# Bind("FromDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFromDate" runat="server" Width="70px" Text='<%# Eval("FromDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtFromDateAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rejoin Date">
                    <ItemTemplate>
                        <asp:Label ID="lblRejoinDate" runat="server" Width="70px" Text='<%# Bind("RejoinDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRejoinDate" runat="server" Width="70px" Text='<%# Eval("RejoinDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtRejoinDateAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From District">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRejoinFromDistrict" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlRejoinFromDistrict_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("sDistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlRejoinFromDistrictAdd" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlRejoinFromDistrictAdd_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From Branch">
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("sBranchName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRejoinFromBranch" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlRejoinFromBranchAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To District">
                    <ItemTemplate>
                        <asp:Label ID="lbltoDistrict" runat="server" Text='<%# Eval("dDistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRejoinToDistrict" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlRejoinToDistrict_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlRejoinToDistrictAdd" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlRejoinToDistrictAdd_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Branch">
                    <ItemTemplate>
                        <asp:Label ID="lbltoBranch" runat="server" Text='<%# Eval("dBranchName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlRejoinToBranch" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlRejoinToBranchAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                       <asp:LinkButton ID="linkAdd" CausesValidation="false" CommandName="addrow"
                            Text="Add" runat="server">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
        <h4 style="color:Red;">*L/C Start date = Leave Start Date/ Consultency Start Date</h4>
    </asp:Panel>
    
    <asp:Panel ID="pnlPromoEdit" GroupingText="Promotion History" runat="server">
        <asp:GridView ID="gvPromotionEdit" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,OldGradeId,NewGradeId,OldDesignationId,NewDesignationId,Type"
            OnRowCancelingEdit="gvPromotionEdit_RowCancelingEdit" OnRowCommand="gvPromotionEdit_RowCommand"
            OnRowEditing="gvPromotionEdit_RowEditing" OnRowUpdating="gvPromotionEdit_RowUpdating">
            <Columns>
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterNoAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From Grade">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlOldGradeE" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOldGradeE_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("OldGradeName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlOldGradeAddE" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlOldGradeAddE_SelectedIndexChanged">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From Designation">
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("OldDesignationName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlOldDesignation" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlOldDesignationAdd" runat="server" >
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Grade">
                    <ItemTemplate>
                        <asp:Label ID="lbltoDistrict" runat="server" Text='<%# Eval("NewGradeName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlNewGradeE" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNewGradeE_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlNewGradeAddE" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlNewGradeAddE_SelectedIndexChanged">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Designation">
                    <ItemTemplate>
                        <asp:Label ID="lbltoBranch" runat="server" Text='<%# Eval("NewDesignationName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlNewDesignation" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlNewDesignationAdd" runat="server" >
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Promotion Date">
                    <ItemTemplate>
                        <asp:Label ID="lblJoiningDate" runat="server" Width="70px" Text='<%# Bind("PromotionDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPromotionDate" runat="server" Width="70px" Text='<%# Eval("PromotionDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic"
                            ControlToValidate="txtPromotionDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtPromotionDate" ID="rvJoiningDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPromotionDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlTypeAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Container.DataItemIndex%>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                     <FooterTemplate>
                        <asp:LinkButton ID="linkAdd" runat="server" CausesValidation="false" CommandName="addrow" Text="Add">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
        <br />
        <asp:LinkButton ID="btnUpdate" runat="server" onclick="btnUpdate_Click">Update Data</asp:LinkButton>
    </asp:Panel>
    
    <asp:Panel ID="pnlTransferEdit" GroupingText="Transfer History" runat="server">
        <asp:GridView ID="gvTransferEdit" runat="server" AutoGenerateColumns="False" OnRowEditing="gvTransferEdit_RowEditing"
            DataKeyNames="SourceBranchId,DestinationBranchId,SourceDistrictId,DestinationDistrictId,Type"
            OnRowUpdating="gvTransferEdit_RowUpdating" OnRowCancelingEdit="gvTransferEdit_RowCancelingEdit"
            OnRowCommand="gvTransferEdit_RowCommand"  SkinID="Special" 
            ShowFooter="True">
            <Columns>
            
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterNoAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From District">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlFromDistrictEdit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFromDistrictEdit_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("SourceDistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlFromDistrictAdd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFromDistrictAdd_SelectedIndexChanged">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From Branch">
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("SourceBranchName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlFromBranch" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlFromBranchAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="To District">
                    <ItemTemplate>
                        <asp:Label ID="lbltoDistrict" runat="server" Text='<%# Eval("DestinationDistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlToDistrictEdit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlToDistrictEdit_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlToDistrictAdd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlToDistrictAdd_SelectedIndexChanged">
                        </asp:DropDownList>
                    
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Branch">
                    <ItemTemplate>
                        <asp:Label ID="lbltoBranch" runat="server" Text='<%# Eval("DestinationBranchName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlToBranch" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlToBranchAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Transfer Date">
                    <ItemTemplate>
                        <asp:Label ID="lblJoiningDate" runat="server" Width="70px" Text='<%# Bind("JoiningDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtJoiningDate" runat="server" Width="70px" Text='<%# Eval("JoiningDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic"
                            ControlToValidate="txtJoiningDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtJoiningDate" ID="rvJoiningDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:TextBox ID="txtJoiningDateAdd" runat="server" Width="70px" ></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlType" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlTypeAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Container.DataItemIndex %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                       <asp:LinkButton ID="linkAdd" CausesValidation="false" CommandName="addrow"
                            Text="Add" runat="server">Add</asp:LinkButton>
                    </FooterTemplate>
               
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
        <br />
        <asp:LinkButton ID="btnTransferUpdate" runat="server" 
            onclick="btnTransferUpdate_Click">Update Transfer</asp:LinkButton>
        
        
    </asp:Panel>
    <asp:Panel ID="pnlIncHeldup" runat="server" GroupingText="Increment Heldup List">
    <asp:GridView ID="gvIncrementHelup" runat="server" AutoGenerateColumns="False" DataKeyNames="Id,H_EmployeeId,BranchId,DistrictId"
            OnRowCancelingEdit="gvIncrementHelup_RowCancelingEdit" OnRowCommand="gvIncrementHelup_RowCommand"
            OnRowEditing="gvIncrementHelup_RowEditing" OnRowUpdating="gvIncrementHelup_RowUpdating" SkinID="Special" ShowFooter="True">
            <Columns>
                
                
                <asp:TemplateField HeaderText="Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterNo" runat="server" Text='<%# Eval("LetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterNo" runat="server" Width="70px" Text='<%# Eval("LetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterNoAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Letter Date">
                    <ItemTemplate>
                        <asp:Label ID="lblLetterDate" runat="server" Width="70px" Text='<%# Bind("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLetterDate" runat="server" Width="70px" Text='<%# Eval("LetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLetterDate" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate"
                            ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ControlToValidate="txtLetterDate" ID="rvLetterDate" Type="Date"
                            MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic"
                            ErrorMessage="*" ToolTip="Invalid letter date"></asp:RangeValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtLetterDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Increment Stop">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("IncrementStop") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtIncrementStop" runat="server" Width="70px" Text='<%# Eval("IncrementStop") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtIncrementStopAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From Date">
                    <ItemTemplate>
                        <asp:Label ID="lblFromDate" runat="server" Width="70px" Text='<%# Bind("FromDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFromDate" runat="server" Width="70px" Text='<%# Eval("FromDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtFromDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To Date">
                    <ItemTemplate>
                        <asp:Label ID="lblToDate" runat="server" Width="70px" Text='<%# Bind("ToDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtToDate" runat="server" Width="70px" Text='<%# Eval("ToDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                        
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtToDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="District">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlDistrictInc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrictInc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlDistrictIncAdd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrictIncAdd_SelectedIndexChanged">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch">
                    <ItemTemplate>
                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlBranchInc" runat="server">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <FooterTemplate>
                    <asp:DropDownList ID="ddlBranchIncAdd" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R.Letter No">
                    <ItemTemplate>
                        <asp:Label ID="lblExemptionLetterNo" runat="server" Text='<%# Eval("ExemptionLetterNo") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtExemptionLetterNo" runat="server" Width="70px" Text='<%# Eval("ExemptionLetterNo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtExemptionLetterNoAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R.Letter Dt">
                    <ItemTemplate>
                        <asp:Label ID="lblExemptionLetterDate" runat="server" Width="70px" Text='<%# Bind("ExemptionLetterDate", "{0:dd/MM/yyyy }") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtExemptionLetterDate" runat="server" Width="70px" Text='<%# Eval("ExemptionLetterDate", "{0:dd/MM/yyyy }") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtExemptionLetterDateAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="R.Increment">
                    <ItemTemplate>
                        <asp:Label ID="lblIncrementExempted" runat="server" Text='<%# Eval("IncrementExempted") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtIncrementExempted" runat="server" Width="70px" Text='<%# Eval("IncrementExempted") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtIncrementExemptedAdd" runat="server" Width="70px"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Edit Action" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" CausesValidation="false" CommandName="deleterow"
                            Text="Delete" OnClientClick="return ConfirmDelete()" CommandArgument='<%# Eval("Id") %>'
                            runat="server">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                       <asp:LinkButton ID="linkAdd" CausesValidation="false" CommandName="addrow"
                            Text="Add" runat="server">Add</asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <SelectedRowStyle BackColor="#FFCC66" ForeColor="#663399" />
        </asp:GridView>
    </asp:Panel>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
