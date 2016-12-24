<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_AcademicQualificationAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.HRM.H_AcademicQualificationAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="1" cellspacing="1">
	<tr>
		<td align="right">Employee Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtEmployeeName" Text="" Enabled="false" Font-Bold="true" ForeColor="ActiveCaptionText" Width="300px">
			</asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right">Level of Education:</td>
		<td>
			<asp:DropDownList ID="ddlLevel" runat="server">
			</asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Exam/Degree Title:</td>
		<td>
			<asp:DropDownList ID="ddlExam" runat="server" DataTextField="Name" 
                DataValueField="Id">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Group/Subject:</td>
		<td>
			<asp:DropDownList ID="ddlGroupSubject" runat="server" DataTextField="Name" 
                DataValueField="Id">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Institution:</td>
		<td>
			<asp:TextBox ID="txtInstitution" runat="server" Width="300px"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvInstitution" runat="server" Display="Dynamic" ControlToValidate="txtInstitution" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			(e.g. School Name/ college Name/Versity Name/...)
		</td>
	</tr>
	<tr>
		<td align="right">Board/University:</td>
		<td>
			<asp:DropDownList ID="ddlBoardUniversity" runat="server" DataTextField="Name" 
                DataValueField="Id">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Result:</td>
		<td>
			<asp:DropDownList ID="ddlResult" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlResult_SelectedIndexChanged">
                <asp:ListItem>First</asp:ListItem>
                <asp:ListItem>Second</asp:ListItem>
                <asp:ListItem>Third</asp:ListItem>
                <asp:ListItem>Pass</asp:ListItem>
                <asp:ListItem>Grade</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtGrade" runat="server" Width="50px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvGrade" runat="server" Display="Dynamic" ControlToValidate="txtGrade" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rvGrade" runat="server" ControlToValidate="txtGrade" MinimumValue="1"  MaximumValue="5" Type="Double"
                ErrorMessage="Between 1.00 & 5.00"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Passing Year:</td>
		<td>
			<asp:TextBox runat="server" ID="txtPassingYear" Text="" MaxLength="100"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvPassingYear" runat="server" Display="Dynamic" ControlToValidate="txtPassingYear" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			<asp:RangeValidator ControlToValidate="txtPassingYear" ID="rvPassingYear" Type="Integer" MaximumValue="2099" MinimumValue="1900" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid passing year"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
		<td align="right" >Serial No: </td>
		<td>
			<asp:TextBox runat="server" ID="txtSortOrder" Text="" MaxLength="100"></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvSortOrder" runat="server" Display="Dynamic" ControlToValidate="txtSortOrder" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
			<asp:CompareValidator ID="cvSortOrder" runat="server" Display="Dynamic" ControlToValidate="txtSortOrder" ErrorMessage="*" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
			&nbsp;(Example: SSC=1/ HSC=2/ Bachelor=3/ . . etc)</td>
	</tr>
	<tr>
		<td colspan="2">
                <asp:HyperLink ID="hlBack" NavigateUrl="~/HRM/H_AcademicQualificationList.aspx" runat="server">Back</asp:HyperLink>
            </td>
		</tr>
	</table>
</asp:Content>
