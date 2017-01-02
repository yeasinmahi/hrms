<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="P_PayScaleAdd.aspx.cs" Inherits="GITS.Hrms.WebSite.Payroll.P_PayScaleAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table border="0" cellpadding="3" cellspacing="1">
	<tr>
		<td align="right">Name:</td>
		<td>
			<asp:DropDownList ID="ddlGrade" runat="server" DataTextField="Name" 
                DataValueField="Id">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Start Basic:</td>
		<td>
			<asp:TextBox ID="txtStartBasic" runat="server" ></asp:TextBox>
			<asp:RequiredFieldValidator ID="rfvtxtInterestRate" runat="server" Display="Dynamic" ControlToValidate="txtStartBasic" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtStartBasic" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" ErrorMessage="*"></asp:CompareValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Increment:</td>
		<td>
			<asp:TextBox runat="server" ID="txtIncrement" 
                CausesValidation="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtIncrement" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtIncrement" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" ErrorMessage="*"></asp:CompareValidator></td></tr><tr>
		<td align="right">Targer Basic:</td><td>
			<asp:TextBox ID="txtTargetBasic" runat="server" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtTargetBasic" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtTargetBasic" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" ErrorMessage="*"></asp:CompareValidator></td></tr><tr>
            <td align="right">
                &nbsp;</td><td>
                <table>
                    <tr>
                        <td style="padding-left:15px;">
                            &nbsp;</td></tr><tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
	                    <td colspan="2">
                            &nbsp;</td></tr></table></td></tr></table></asp:Content>