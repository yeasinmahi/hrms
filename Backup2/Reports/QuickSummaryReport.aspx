<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="QuickSummaryReport.aspx.cs" Inherits="QuickSummaryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <table border="0" cellpadding="2" cellspacing="1">   
    <tr>
        <td align="right"><asp:Label runat="server" ID="lblReport" Text="Report:"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddlReport" runat="server" DataTextField="Name" DataValueField="Id" Width="250" AutoPostBack="true"
            OnSelectedIndexChanged="ddlReport_SelectedIndexChanged"></asp:DropDownList>
    </td>
    </tr>
    <tr>
		<td align="right"><asp:Label runat="server" ID="lblZone" Text="Zone:"></asp:Label></td>
		<td>
			<asp:DropDownList ID="ddlZone" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="250" AutoPostBack="true"
                OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
		</td>
	</tr>
    <tr>
		<td align="right"><asp:Label ID="lblSubzone" runat="server" Text="Subzone:"></asp:Label></td>
		<td>
			<asp:DropDownList ID="ddlSubzone" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="250" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSubzone_SelectedIndexChanged"></asp:DropDownList>
		</td>
	</tr>
    <tr>
		<td align="right"><asp:Label ID="lblRegion" runat="server" Text="Region:"></asp:Label></td>
		<td>
			<asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="250" AutoPostBack="true"
                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
		</td>		
	</tr>
	<tr>
		<td align="right"><asp:Label ID="lblBranch" runat="server" Text="Branch:"></asp:Label></td>
		<td>
			<asp:DropDownList ID="ddlBranch" runat="server" DataTextField="Name" DataValueField="Code" Width="250" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
		</td>
	</tr>
	<tr>
	    <td align="right"><asp:Label ID="lblGrade" runat="server" Text="Grade:"></asp:Label></td>
	    <td>
	        <asp:DropDownList ID="ddlGrade" runat="server" AutoPostBack="true" Width="250"
	            OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged"></asp:DropDownList>
	    </td>
	</tr>
	<tr>
	    <td align="right"><asp:Label ID="lblDesignation" runat="server" Text="Designation:"></asp:Label></td>
	    <td>
	        <asp:DropDownList ID="ddlDesignation" runat="server" DataTextField="Name" DataValueField="Id" AutoPostBack="true" Width="250"
	            OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged"></asp:DropDownList>
	    </td>
	</tr>
	<tr>
		<td align="right">As On:</td>
		<td>
			<asp:TextBox runat="server" ID="txtAsOnDate" Text="" MaxLength="10"></asp:TextBox>
			<asp:ImageButton ID="ibAsOnDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtAsOnDate'));return false;"></asp:ImageButton>
		</td>
	</tr>
  </table>
</asp:Content>
