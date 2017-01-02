<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="HRM_Report.aspx.cs" Inherits="GITS.Hrms.WebSite.Reports.HRM_Report" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="2" cellspacing="1">
	<tr>
	    <td align="right"><strong>Select Report</strong></td>
	</tr>
	<tr>
		<td align="right">Type:</td>
		<td>
			<asp:DropDownList ID="ddlReportType" runat="server" Width="250" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged"></asp:DropDownList>
		</td>
		<td>
			&nbsp;</td>
	</tr>
	<tr>
		<td align="right">Report:</td>
		<td>
			<asp:DropDownList ID="ddlReport" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="250" AutoPostBack="true" 
                OnSelectedIndexChanged="ddlReport_SelectedIndexChanged"></asp:DropDownList>
		</td>
		<td>
			&nbsp;</td>
	</tr>
	<tr><td><br /></td></tr>
	<tr>
	    <td align="right"><strong>Location</strong></td>
	</tr>
    <tr>
		<td align="right"><asp:Label runat="server" ID="lblZone" Text="Division:"></asp:Label></td>
		<td>
			<asp:DropDownList ID="ddlZone" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="200" AutoPostBack="true"
                OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
		</td>
		<td>
			&nbsp;</td>
		</tr>
		<tr>	
		<td align="right"><asp:Label ID="lblSubzone" runat="server" Text="District:"></asp:Label></td>
		<td>
			<asp:DropDownList ID="ddlSubzone" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="200" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSubzone_SelectedIndexChanged"></asp:DropDownList>
		</td>
		<td>
			&nbsp;</td>
		</tr>
		<tr>
		<td align="right"><asp:Label ID="lblRegion" runat="server" Text="Region:"></asp:Label></td>
		<td>
			<asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="200" AutoPostBack="true"
                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"></asp:DropDownList>
		</td>
		<td>
			&nbsp;</td>
		</tr>
		<tr>		
		<td align="right"><asp:Label ID="lblBranch" runat="server" Text="Branch:"></asp:Label></td>
		<td>
			<asp:DropDownList ID="ddlBranch" runat="server" DataTextField="Name" DataValueField="Code" Width="200" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:DropDownList>
		</td>
		<td>
			&nbsp;</td>
	</tr>
	<tr>
	    <td align="right"><strong>Position</strong></td>
	</tr>
	<tr>
	    <td align="right"><asp:Label ID="lblGrade" runat="server" Text="Grade:"></asp:Label></td>
	    <td>
	        <asp:DropDownList ID="ddlGrade" runat="server" AutoPostBack="true" Width="200"
	            OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged"></asp:DropDownList>
	    </td>
	    <td>
	        &nbsp;</td>
	    </tr>
		<tr>
	    <td align="right"><asp:Label ID="lblDesignation" runat="server" Text="Designation:"></asp:Label></td>
	    <td>
	        <asp:DropDownList ID="ddlDesignation" runat="server" DataTextField="Name" DataValueField="Id" AutoPostBack="true" Width="200"
	            OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged"></asp:DropDownList>
	    </td>
	    <td>
	        &nbsp;</td>
	</tr>
	<tr>
	    <td align="right"><strong>Other</strong></td>
	</tr>
	<tr>
	    <td align="right"><asp:Label ID="lblReligion" runat="server" Text="Religion:"></asp:Label></td>
	    <td>
	        <asp:DropDownList ID="ddlReligion" runat="server" AutoPostBack="true" Width="200"></asp:DropDownList>
	    </td>
	    <td>
	        &nbsp;</td>
	    </tr>
		<tr>
	    <td align="right"><asp:Label ID="lblSex" runat="server" Text="Sex:"></asp:Label></td>
	    <td>
	        <asp:DropDownList ID="ddlSex" runat="server" AutoPostBack="true" Width="200"></asp:DropDownList>
	    </td>
	    <td>
	        &nbsp;</td>
	</tr>
	<tr>
	    <td align="right"><strong>Date</strong></td>
	</tr>
	<tr runat="server" id="trStartDate">
		<td align="right">Start Date:</td>
		<td>
			<asp:TextBox runat="server" ID="txtStartDate" Text="" MaxLength="10">
			</asp:TextBox><asp:ImageButton ID="ibStartDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtStartDate'));return false;">
			</asp:ImageButton><asp:RequiredFieldValidator ID="rfvStartDate" runat="server" Display="Dynamic" ControlToValidate="txtStartDate" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator><asp:RangeValidator ControlToValidate="txtStartDate" ID="rvStartDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data"></asp:RangeValidator>
			<%--<span style="font-size: smaller">[*Begining of the month]</span>--%>
		</td>
		<td>
			&nbsp;</td>
		</tr>
		<tr runat="server" id="trEndDate">
		<td align="right" runat="server" id="tdEndDate">End Date:</td>
		<td>
			<asp:TextBox runat="server" ID="txtEndDate" Text="" MaxLength="10">
			</asp:TextBox><asp:ImageButton ID="ibEndDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtEndDate'));return false;">
			</asp:ImageButton><asp:RequiredFieldValidator ID="rfvEndDate" runat="server" Display="Dynamic" ControlToValidate="txtEndDate" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator><asp:RangeValidator ControlToValidate="txtEndDate" ID="rvEndDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data">
			</asp:RangeValidator><asp:CompareValidator ID="cvDate" runat="server" ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" Operator="GreaterThanEqual" Type="Date" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data"></asp:CompareValidator>
			<%--<span style="font-size: smaller">[*End of the month]</span>--%>
		</td>
		<td>
			<asp:Label ID="lblTotalRecord" runat="server" Font-Bold="True" Text="Label" 
                Visible="False"></asp:Label>
		</td>
	</tr>
	</table>
	<asp:Panel ID="pnlGrid" runat="server">
	
    <asp:GridView ID="gvList" runat="server" SkinID="Special">
    </asp:GridView>
	</asp:Panel>
</asp:Content>
