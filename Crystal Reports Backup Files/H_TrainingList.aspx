<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_TrainingList.aspx.cs" Inherits="H_TrainingList" Title="" %>
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
						<asp:HyperLinkField HeaderText="Training Title" DataTextField="Title" DataNavigateUrlFields="Id,H_EmployeeId" DataNavigateUrlFormatString="H_TrainingAdd.aspx?Id={0}&H_EmployeeId={1}"></asp:HyperLinkField>
						<mms:BoundField DataField="Topics" HeaderText="Topics Covered" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="InstituteName" HeaderText="Institute Name" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="Country" HeaderText="Country" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="Location" HeaderText="Location" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="TrainingYear" HeaderText="Training Year" FieldType="String"></mms:BoundField>
						<mms:BoundField DataField="Duration" HeaderText="Duration" FieldType="String"></mms:BoundField>
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
