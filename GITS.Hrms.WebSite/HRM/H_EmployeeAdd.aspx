<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" CodeBehind="H_EmployeeAdd.aspx.cs"
    Inherits="GITS.Hrms.WebSite.HRM.H_EmployeeAdd" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    <script language="javascript" type="text/jscript">
      var prm = Sys.WebForms.PageRequestManager.getInstance();
     
      prm.add_initializeRequest(InitializeRequest);
      prm.add_endRequest(EndRequest);
      
      function InitializeRequest(sender, args) 
      {
        //document.getElementById('tblEmployee').disabled = true;
        document.body.parentNode.style.cursor = 'wait'; 
      }
     
      function EndRequest(sender, args) 
      {
        //document.getElementById('tblEmployee').disabled = false;
        document.body.parentNode.style.cursor = 'auto'; 
      }

      
    </script>
    <script type="text/javascript">
        function imagepreview(input) {
            if (input.files && input.files[0]) {
                var fildr = new FileReader();
                fildr.onload = function(e) {
                    //var uploadControlFileUploader = document.getElementById('<%= imgPhoto.ClientID%>');
                    $("#ctl00_ContentPlaceHolder1_imgPhoto").attr('src', e.target.result);
                }
                fildr.readAsDataURL(input.files[0]);

            }
        }
    </script>

    <table border="0" cellpadding="1" cellspacing="1" id="tblEmployee">
        <tr>
            <td colspan="4">
                <strong>General Information</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                Name:
            </td>
            <td  style="width: 260px;">
                <asp:TextBox style="text-transform:uppercase;"  runat="server" ID="txtName" Text="" MaxLength="100" Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" Display="Dynamic" ControlToValidate="txtName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>

            </td>
            <td align="right">
                ID No:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtId" Text="[Auto]" Font-Bold="true" 
                    ForeColor="ActiveCaptionText" Width="135px" ReadOnly="True"></asp:TextBox>
                
            </td>
            <td colspan="2" rowspan="6">
                <asp:Image ID="imgPhoto" runat="server" ImageUrl="" Height="121px" Width="122px" 
                    AlternateText="Photograph" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Name in Bangla:</td>
            <td  style="width: 260px;">
                <asp:TextBox runat="server" 
                    ID="txtNameInBangla" Text="" MaxLength="100" Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNameInBangla" runat="server" Display="Dynamic" ControlToValidate="txtNameInBangla" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                Father's Name:
            </td>
            <td>
                <asp:TextBox style="text-transform:uppercase;" runat="server" ID="txtFatherName" Text="" MaxLength="100" 
                    Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFatherName" runat="server" Display="Dynamic" ControlToValidate="txtFatherName" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                Mother's Name:
            </td>
            <td>
                <asp:TextBox style="text-transform:uppercase;" runat="server" ID="txtMotherName" Text="" MaxLength="100" 
                    Width="150px"></asp:TextBox>
			    <asp:RequiredFieldValidator ID="rfvMotherName" runat="server" Display="Dynamic" ControlToValidate="txtMotherName"  ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Date Of Birth:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDateOfBirth" Text="" MaxLength="10" Width="130px"></asp:TextBox>
                <asp:ImageButton ID="ibDateOfBirth" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtDateOfBirth'));return false;"></asp:ImageButton>
                <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" Display="Dynamic" ControlToValidate="txtDateOfBirth" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtDateOfBirth" ID="rvDateOfBirth" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid date of birth"></asp:RangeValidator>
            </td>
            <td align="right">
                Sex:
            </td>
            <td>
                <asp:DropDownList ID="ddlSex" runat="server" Width="155px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Marital Status:
            </td>
            <td>
                <asp:DropDownList ID="ddlMaritalStatus" runat="server" Width="250px"></asp:DropDownList>
            </td>
            <td align="right">
                Religion:
            </td>
            <td>
                <asp:DropDownList ID="ddlReligion" runat="server" Width="155px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Blood Group:
            </td>
            <td>
                <asp:DropDownList ID="ddlBloodGroup" runat="server" Width="250px"></asp:DropDownList>
            </td>
            <td align="right">NID No:</td>
            <td>
                <asp:TextBox runat="server" ID="txtNIDNo" Width="150px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNID" runat="server" Display="Dynamic" ControlToValidate="txtNIDNo" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rexvNID" runat="server" 
      ControlToValidate="txtNIDNo" ErrorMessage="*" 
      ValidationExpression="[1-9]\d*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <strong>Permanent Address</strong>
            </td>
            <td colspan="2">
                <input type="file" id="Upload" runat="server" onchange="imagepreview(this);" />
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="hlRemovePhoto" runat="server" Text="Remove Photo" CausesValidation="false" OnClick="hlRemovePhoto_Click"></asp:LinkButton>
                <asp:RegularExpressionValidator ID="revUpload" runat="server" ControlToValidate="upload" ErrorMessage="Only .gif, .jpg, .png, .tiff and .jpeg" 
                     ValidationExpression="(.*\.([Gg][Ii][Ff])|.*\.([Jj][Pp][Gg])|.*\.([Bb][Mm][Pp])|.*\.([pP][nN][gG])|.*\.([tT][iI][iI][fF])$)"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Village/Road:
            </td>
            <td>
                <asp:TextBox style="text-transform:uppercase;" runat="server" ID="txtPermanentVillage" Text="" MaxLength="100" 
                    Width="250px"></asp:TextBox>
			    <asp:RequiredFieldValidator  ID="rfvPermanentVillage" runat="server" Display="Dynamic" ControlToValidate="txtPermanentVillage" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                Post Office:
            </td>
            <td>
                <asp:TextBox style="text-transform:uppercase;" runat="server" ID="txtPermanentPostOffice" Text="" MaxLength="100" 
                    Width="150px"></asp:TextBox>
            </td>
            <td align="right">
                Post Code:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPermanentPostCode" Text="" MaxLength="4"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtPermanentPostCode" ID="rvPermanentPostCode" Type="Integer"
                    MaximumValue="9999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid post code"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Govt.
                District:
            </td>
            <td>
                <asp:DropDownList ID="ddlPermanentDistrict" runat="server" DataTextField="Name" DataValueField="Id"
                    AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlPermanentDistrict_SelectedIndexChanged" 
                    Width="250px"></asp:DropDownList>
            </td>
            <td align="right">
                Thana:
            </td>
            <td>
                <asp:DropDownList ID="ddlPermanentThana" runat="server" DataTextField="Name" 
                    DataValueField="Id" Width="155px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Phone:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPermanentPhone" Text="" MaxLength="25" 
                    Width="250px"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtPermanentPhone" ID="rvPermanentPhone" Type="Double"
                    MaximumValue="9999999999999999999999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid phone number"></asp:RangeValidator>
            </td>
            <td align="right">
                Email:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPermanentEmail" Text="" MaxLength="30" 
                    Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <strong>Present Address</strong>
            </td>
        </tr>
        <tr>
            <td align="right">
                Village/Road:
            </td>
            <td>
                <asp:TextBox style="text-transform:uppercase;" runat="server" ID="txtPresentVillage" MaxLength="100" 
                    Width="250px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPresentVillage" runat="server" Display="Dynamic" ControlToValidate="txtPresentVillage" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                Post Office:
            </td>
            <td>
                <asp:TextBox style="text-transform:uppercase;" runat="server" ID="txtPresentPostOffice" Text="" MaxLength="100" 
                    Width="150px"></asp:TextBox>
            </td>
            <td align="right">
                Post Code:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPresentPostCode" Text="" MaxLength="4"></asp:TextBox>
			    <asp:RangeValidator ControlToValidate="txtPresentPostCode" ID="rvPresentPostCode" Type="Integer"
                    MaximumValue="9999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid post code"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Govt.
                District:
            </td>
            <td>
                <asp:DropDownList ID="ddlPresentDistrict" runat="server" DataTextField="Name" DataValueField="Id"
                    AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlPresentDistrict_SelectedIndexChanged" Width="250px"></asp:DropDownList>
            </td>
            <td align="right">
                Thana:
            </td>
            <td>
                <asp:DropDownList ID="ddlPresentThana" runat="server" DataTextField="Name" 
                    DataValueField="Id" Width="155px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Phone:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPresentPhone" Text="" MaxLength="30" 
                    Width="250px"></asp:TextBox>
                <asp:RangeValidator ControlToValidate="txtPresentPhone" ID="rvPresentPhone" Type="Double"
                    MaximumValue="9999999999999999999999999" MinimumValue="1" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid phone number"></asp:RangeValidator>
            </td>
            <td align="right">
                Email:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPresentEmail" Text="" MaxLength="30" 
                    Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <strong>Joining Information</strong>
            </td>
        </tr>
        <tr>
            <td align="right" style="font-size: smaller">
                Appointment<br />
                Letter Date:
            </td>
            <td valign="middle">
                <asp:TextBox runat="server" ID="txtAppointmentLetterDate" Text="" MaxLength="10" Width="130px"></asp:TextBox>
                <asp:ImageButton ID="ibAppointmentLetterDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtAppointmentLetterDate'));return false;"></asp:ImageButton>
                <asp:RequiredFieldValidator ID="rfvAppointmentLetterDate" runat="server"
                    Display="Dynamic" ControlToValidate="txtAppointmentLetterDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtAppointmentLetterDate" ID="rvAppointmentLetterDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" 
                    runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid appointment letter date"></asp:RangeValidator>
            </td>
            <td align="right" style="font-size: smaller">
                Appointment<br />
                Letter No:
            </td>
            <td valign="middle">
                <asp:TextBox runat="server" ID="txtAppointmentLetterNo" Text="" MaxLength="100" 
                    Width="150px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAppointmentLetterNo" runat="server" Display="Dynamic" ControlToValidate="txtAppointmentLetterNo" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                Joining Date:
            </td>
            <td valign="middle">
                <asp:TextBox runat="server" ID="txtJoiningDate" Text="" MaxLength="10" Width="130px"></asp:TextBox>
                <asp:ImageButton ID="ibJoiningDate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(document.getElementById('ctl00_ContentPlaceHolder1_txtJoiningDate'));return false;">
                </asp:ImageButton><asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" Display="Dynamic" ControlToValidate="txtJoiningDate" ErrorMessage="*" ToolTip="Required"></asp:RequiredFieldValidator>
                <asp:RangeValidator ControlToValidate="txtJoiningDate" ID="rvJoiningDate" Type="Date" MaximumValue="31/12/9999" MinimumValue="1/1/1753" runat="server" Display="Dynamic" ErrorMessage="*" ToolTip="Invalid joining letter date"></asp:RangeValidator>
            </td>
            </tr>
            <tr>
            <td align="right">
                Department:
            </td>
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server" DataTextField="Name" 
                    DataValueField="Id" Width="250px"></asp:DropDownList>
            </td>
            <td align="right">
                Grade:
            </td>
            <td>
                <asp:DropDownList ID="ddlGrade" runat="server" DataTextField="Name" DataValueField="Id"
                    OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" AutoPostBack="True" 
                    Width="155px"></asp:DropDownList>
            </td>
            <td align="right">
                Designation:
            </td>
            <td>
                <asp:DropDownList ID="ddlDesignation" runat="server" DataTextField="Name" 
                    DataValueField="Id" Width="155px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Zone:
            </td>
            <td>
                <asp:DropDownList ID="ddlZone" runat="server" DataTextField="Name" DataValueField="Id"
                    AutoPostBack="True" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" 
                    Width="250px"></asp:DropDownList>
            </td>
            <td align="right">
                ASA District:
            </td>
            <td>
                <asp:DropDownList ID="ddlSubzone" runat="server" DataTextField="Name" DataValueField="Id"
                    AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlSubzone_SelectedIndexChanged" Width="155px"></asp:DropDownList>
            </td>
            <td align="right">
                Region:
            </td>
            <td>
                <asp:DropDownList ID="ddlRegion" runat="server" DataTextField="Name" 
                    DataValueField="Id" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged" Width="155px"></asp:DropDownList>
            </td>
            
        </tr>
        <tr>
            <td align="right" style="font-size: smaller;">
                Employee <br />Status:
            </td>
            <td>
                <asp:DropDownList ID="ddlEmployeeStatus" runat="server" Width="250px"></asp:DropDownList>
            </td>
            <td align="right" style="font-size: smaller;">
                Employment <br />Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlEmploymentType" runat="server" Width="155px"></asp:DropDownList>
            </td> 
            <td align="right">
                Branch:
            </td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" DataTextField="Name" 
                    DataValueField="Id" Width="155px"></asp:DropDownList>
            </td>           
        </tr>
        <tr>
            <td colspan="6">
                <asp:HyperLink ID="hlAcademicQualification" NavigateUrl="~/HRM/H_AcademicQualificationList.aspx"  runat="server">Academic Qualification</asp:HyperLink>&nbsp;&nbsp;
                <asp:HyperLink ID="hlProfessionalQualification" NavigateUrl="~/HRM/H_ProfessionalQualificationList.aspx"  runat="server">Professional Qualification</asp:HyperLink>&nbsp;&nbsp;
                <asp:HyperLink ID="hlTraining" NavigateUrl="~/HRM/H_TrainingList.aspx"  runat="server">Training</asp:HyperLink>&nbsp;&nbsp;
                <asp:HyperLink ID="hlExperience" NavigateUrl="~/HRM/H_ExperienceList.aspx"  runat="server">Experience</asp:HyperLink> &nbsp;&nbsp;
                <asp:HyperLink ID="hlFileupload" NavigateUrl="~/HRM/H_EmployeeInfoUpload.aspx"  runat="server">Necessary File</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:HyperLink ID="hlBack" NavigateUrl="~/HRM/H_EmployeeList.aspx" runat="server">Back</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>

