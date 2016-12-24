<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="LetterOfIdCard.aspx.cs" Inherits="LetterOfIdCard" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="2" cellspacing="1">
	    <tr>
	        <td><strong>Employee Id:</strong></td>
	    </tr>
	    <tr>
	        <td align="right">Start Id: </td>
	        <td>
	            <asp:TextBox ID="txtStartId" runat="server"></asp:TextBox>
	            <asp:RequiredFieldValidator ID="rfvStartId" runat="server" Display="Dynamic" ControlToValidate="txtStartId" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtStartId" ID="rvStartId" Type="Integer" MaximumValue="2147483647" MinimumValue="0" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Only integer values are allowed"></asp:RangeValidator>
	        </td>
	    </tr>	    
	    <tr>
	        <td  align="right">End Id: </td>
	        <td>
	            <asp:TextBox ID="txtEndId" runat="server"></asp:TextBox>
	            <asp:RequiredFieldValidator ID="rfvEndId" runat="server" Display="Dynamic" ControlToValidate="txtEndId" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtEndId" ID="rvEndId" Type="Integer" MaximumValue="2147483647" MinimumValue="0" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Only integer values are allowed"></asp:RangeValidator>
                <asp:CompareValidator ID="cvEndId" Type="Integer" Operator="GreaterThanEqual" ControlToCompare="txtStartId" ControlToValidate="txtEndId" Display="Dynamic" runat="server" ErrorMessage="*" ToolTip="End id should be greater than or equal to start id"></asp:CompareValidator>
	        </td>
	    </tr>
	    <tr>
	        <td  align="right">Note:</td>
	        <td>
	            <asp:TextBox ID="txtNote" runat="server" Rows="3" TextMode="MultiLine" 
                    Width="600px"></asp:TextBox>
	        </td>
	    </tr>
	</table>
</asp:Content>