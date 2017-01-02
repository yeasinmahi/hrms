<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_ExperienceList.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_ExperienceList" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table style="width: 100%">
		<tr>
	        <td align="right" style="width:5%;">
                Employee Name:
            </td>
            <td style="width: 90%">
                <asp:TextBox runat="server" ID="txtEmployeeName" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="500px">
			</asp:TextBox>
			</td>
	    </tr>
		<tr>
			<td colspan="2">
				<asp:GridView ID="gvList" runat="server" OnRowCreated="gvList_RowCreated" OnSorting="gvList_Sorting" DataKeyNames="Id">
					<Columns>
						<asp:TemplateField>
						<ItemStyle HorizontalAlign="Center" Width="20px" />
						<ItemTemplate>
							<asp:CheckBox ID="chkSelect" runat="server" />
						</ItemTemplate>
						<HeaderTemplate>
							<input id="chkAll" runat="server" type="checkbox" onclick="javascript:GridSelectAll(this);" />
						</HeaderTemplate>
						</asp:TemplateField>
						<asp:HyperLinkField HeaderText="CompanyName" DataTextField="CompanyName" DataNavigateUrlFields="Id,H_EmployeeId" DataNavigateUrlFormatString="H_ExperienceAdd.aspx?Id={0}&H_EmployeeId={1}"></asp:HyperLinkField>
						<mms:BoundField DataField="CompanyBusiness" HeaderText="Company Business" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="CompanyLocation" HeaderText="Company Location" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="PositionHeld" HeaderText="Position Held" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="Department" HeaderText="Department" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="Responsibilities" HeaderText="Responsibilities" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
						<mms:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" FieldType="DateTime"></mms:BoundField>
					</Columns>
				</asp:GridView>
			</td>
		</tr>
		<tr>
		    <td colspan="2">
                <asp:HyperLink ID="hlBack" NavigateUrl="~/HRM/H_EmployeeAdd.aspx" runat="server">Back</asp:HyperLink>
            </td>
		</tr>
	</table>
</asp:Content>
