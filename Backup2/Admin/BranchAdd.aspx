<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="BranchAdd.aspx.cs" Inherits="BranchAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="3" cellspacing="1">
	
	<tr>
		<td align="right">ASA Zone:</td>
		<td>
			<asp:DropDownList ID="ddlZoneId" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="250px"
			    AutoPostBack="true" OnSelectedIndexChanged="ddlZoneId_OnSelectedIndexChanged"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">ASA District:</td>
		<td>
			<asp:DropDownList ID="ddlSubzoneId" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="250px"
		        AutoPostBack="true" OnSelectedIndexChanged="ddlSubzoneId_OnSelectedIndexChanged"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Region:</td>
		<td>
			<asp:DropDownList ID="ddlRegionId" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="250px"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Branch Name:</td>
		<td>
			<asp:TextBox runat="server" ID="txtName" Text="" MaxLength="100" Width="245px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Name in Bangla:</td>
		<td>
			<asp:TextBox ID="txtNameInBangla" runat="server" AutoPostBack="True" 
                Width="245px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvBanglaName" runat="server" 
                ControlToValidate="txtNameInBangla" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right">Branch Type:</td>
		<td>
		    <asp:DropDownList runat="server" ID="ddlBranchType" Width="250px"></asp:DropDownList>
		</td>
	</tr>
	
	<tr>
		<td align="right">Mobile Number:</td>
		<td>
			<asp:TextBox runat="server" ID="txtMobileNumber" Text="" MaxLength="50" 
                Width="245px"></asp:TextBox>		</td>
	</tr>
	
	<tr>
		<td align="right">Location Type:</td>
		<td>
			<asp:DropDownList runat="server" ID="ddlLocationType" Width="250px"></asp:DropDownList>
		</td>
	</tr>
	
	<tr>
		<td align="right">Govt. District:</td>
		<td>
			<asp:DropDownList ID="ddlDistrictId" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="250px"
			    AutoPostBack="true" OnSelectedIndexChanged="ddlDistrictId_OnSelectedIndexChanged"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Govt. Thana:</td>
		<td>
			<asp:DropDownList ID="ddlThanaId" runat="server" DataTextField="Name" 
                DataValueField="Id" Width="250px"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Village/Road:</td>
		<td>
			<asp:TextBox ID="txtVillage" runat="server" MaxLength="50" Width="245px"></asp:TextBox>
        </td>
	</tr>
	<tr>
		<td align="right">Post Office:</td>
		<td>
			<asp:TextBox ID="txtPostOffice" runat="server" MaxLength="50" Width="245px"></asp:TextBox>
        </td>
	</tr>
	<tr>
		<td align="right">Post Code:</td>
		<td>
			<asp:TextBox ID="txtPostCode" runat="server" MaxLength="4"></asp:TextBox>
        </td>
	</tr>
	<tr>
		<td align="right">Status:</td>
		<td>
			<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlStatus_SelectedIndexChanged" Width="125px">
            </asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right">Letter No:</td>
		<td>
			<asp:TextBox ID="txtLetterNo" runat="server" MaxLength="50"></asp:TextBox>
        </td>
	</tr>
	<tr>
		<td align="right">Letter Date:</td>
		<td>
			<asp:TextBox ID="txtLetterDate" runat="server"></asp:TextBox>
			<asp:ImageButton ID="ibLetterDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtLetterDate'));return false;">
			</asp:ImageButton>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="txtLetterDate" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator><asp:RangeValidator ControlToValidate="txtLetterDate" ID="RangeValidator1" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data"></asp:RangeValidator>
        </td>
	</tr>
	<tr>
		<td align="right">
            <asp:Label ID="lblOpenDate" runat="server" Text="Openning Date:"></asp:Label>
        </td>
		<td>
			<asp:TextBox runat="server" ID="txtOpeningDate" Text="" MaxLength="10">
			</asp:TextBox><asp:ImageButton ID="ibOpeningDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtOpeningDate'));return false;">
			</asp:ImageButton><asp:RequiredFieldValidator ID="rfvOpeningDate" runat="server" Display="Dynamic" ControlToValidate="txtOpeningDate" ErrorMessage="*" ToolTip="Required">
			</asp:RequiredFieldValidator><asp:RangeValidator ControlToValidate="txtOpeningDate" ID="rvOpeningDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid data"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
	    <td colspan="2">
            <asp:HyperLink ID="hlBack" NavigateUrl="~/Admin/BranchList.aspx" runat="server">Back</asp:HyperLink>
        </td>
	</tr>
	</table>
</asp:Content>
