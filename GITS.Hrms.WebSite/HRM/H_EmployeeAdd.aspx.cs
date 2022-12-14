using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEE ADD"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "CLOSE":
                    UIUtility.Transfer(Page, GetListPageUrl());
                    break;
                case "REFRESH":
                    UIUtility.Transfer(Page, Request.Path);
                    break;
            
                default:
                    HandleSpecialCommand(sender, e);
                    break;

            }
        }

        protected override string GetListPageUrl()
        {
            return "H_EmployeeList.aspx";
        }

        private H_Employee GetH_Employee()
        {
            TransactionManager tm = new TransactionManager(false);
            //Int32 EmpCode =Convert.ToInt32(tm.GetDataSet("SELECT MAX(Code) FROM H_EMPLOYEE").Tables[0].Rows[0][0]);
            H_Employee h_Employee = null;

            if (Type == TYPE_EDIT)
            {
                h_Employee = H_Employee.GetById(Convert.ToInt32(hdnId.Value));

                h_Employee.PermanentAddress = H_Address.GetById(h_Employee.PermanentAddressId);
                h_Employee.PresentAddress = H_Address.GetById(h_Employee.PresentAddressId);
                h_Employee.Status = (H_Employee.Statuses)DBUtility.ToInt32(ddlEmployeeStatus.SelectedValue);
            }
            else
            {
                h_Employee = new H_Employee();
                h_Employee.Status = H_Employee.Statuses.Working;
                // h_Employee.Code = EmpCode + 1;
                h_Employee.PermanentAddress = new H_Address();
                h_Employee.PresentAddress = new H_Address();
            }
            //h_Employee.Code = DBUtility.ToInt32(txtId.Text);
            h_Employee.Name = DBUtility.ToString(txtName.Text.Trim()).ToUpper();
            h_Employee.NameInBangla = DBUtility.ToNullableString(txtNameInBangla.Text);
            h_Employee.FatherName = DBUtility.ToString(txtFatherName.Text.Trim()).ToUpper();
            h_Employee.MotherName = DBUtility.ToString(txtMotherName.Text).ToUpper();
            h_Employee.DateOfBirth = DBUtility.ToDateTime(txtDateOfBirth.Text);
            h_Employee.BloodGroup = (H_Employee.BloodGroups)DBUtility.ToInt32(ddlBloodGroup.SelectedValue);
            h_Employee.Sex = (H_Employee.Sexes)DBUtility.ToInt32(ddlSex.SelectedValue);
            h_Employee.MaritalStatus = (H_Employee.MaritalStatuses)DBUtility.ToInt32(ddlMaritalStatus.SelectedValue);
            h_Employee.Religion = (H_Employee.Religions)DBUtility.ToInt32(ddlReligion.SelectedValue);
            h_Employee.AppointmentLetterDate = DBUtility.ToDateTime(txtAppointmentLetterDate.Text);
            h_Employee.AppointmentLetterNo = DBUtility.ToString(txtAppointmentLetterNo.Text);
            h_Employee.JoiningDate = DBUtility.ToDateTime(txtJoiningDate.Text);
            h_Employee.EmploymentType = (H_Employee.EmploymentTypes)DBUtility.ToInt32(ddlEmploymentType.SelectedValue);
            h_Employee.NationalId = DBUtility.ToNullableInt64(txtNIDNo.Text);
            //if (Upload.PostedFile.ContentLength > 0)
            //{
            //    HttpPostedFile File = Upload.PostedFile;
            //    Byte[] Data = new Byte[File.ContentLength];
            //    File.InputStream.Read(Data, 0, File.ContentLength);
            //    h_Employee.Photo = Data;
            //}
        
            //if (this.imgPhoto.ImageUrl == "~/Images/Temp/default.jpg")
            //{
            //    Byte[] Data = File.ReadAllBytes(Server.MapPath("~/Images/Temp/default.jpg"));
            //    h_Employee.Photo = Data;
            //}

            h_Employee.PermanentAddress.Village = DBUtility.ToString(txtPermanentVillage.Text).ToUpper();
            h_Employee.PermanentAddress.PostOffice = String.IsNullOrEmpty(txtPermanentPostOffice.Text)==true? DBUtility.ToNullableString(txtPermanentPostOffice.Text) : DBUtility.ToNullableString(txtPermanentPostOffice.Text).ToUpper();
            h_Employee.PermanentAddress.PostCode = DBUtility.ToNullableInt32(txtPermanentPostCode.Text);
            h_Employee.PermanentAddress.ThanaId = DBUtility.ToInt32(ddlPermanentThana.SelectedValue);
            h_Employee.PermanentAddress.Phone = DBUtility.ToNullableString(txtPermanentPhone.Text);
            h_Employee.PermanentAddress.Email = DBUtility.ToNullableString(txtPermanentEmail.Text);

            h_Employee.PresentAddress.Village = DBUtility.ToString(txtPresentVillage.Text).ToUpper();
            h_Employee.PresentAddress.PostOffice =String.IsNullOrEmpty(txtPresentPostOffice.Text)==true? DBUtility.ToNullableString(txtPresentPostOffice.Text) : DBUtility.ToNullableString(txtPresentPostOffice.Text).ToUpper();
            h_Employee.PresentAddress.PostCode = DBUtility.ToNullableInt32(txtPresentPostCode.Text);
            h_Employee.PresentAddress.ThanaId = DBUtility.ToInt32(ddlPresentThana.SelectedValue);
            h_Employee.PresentAddress.Phone = DBUtility.ToNullableString(txtPresentPhone.Text);
            h_Employee.PresentAddress.Email = DBUtility.ToNullableString(txtPresentEmail.Text);

            return h_Employee;
        }

        private new Message Validate()
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Record saved successfully.";

            base.Validate();

            if (IsValid == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid data provided or required data missing";
                return msg;
            }

            if (DBUtility.ToDateTime(txtDateOfBirth.Text.Trim()) >= DBUtility.ToDateTime(txtAppointmentLetterDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Date of birth should be less than appointment letter date";
                return msg;
            }
            if ( DBUtility.ToDateTime(txtJoiningDate.Text.Trim()).DayOfWeek==DayOfWeek.Friday)
            {
                if (DBUtility.ToDateTime(txtJoiningDate.Text.Trim()) > (new DateTime(2014, 1, 1)))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Joining Date can't be Friday";
                    return msg;
                }
            }

            if (DBUtility.ToDateTime(txtDateOfBirth.Text.Trim()) >= DBUtility.ToDateTime(txtJoiningDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Date of birth should be less than joining date";
                return msg;
            }

            if (Upload.PostedFile.ContentLength > 100000)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Image must be within 100KB";
                return msg;
            }
            if (Type == TYPE_ADD)
            {
                H_Employee emp = H_Employee.Get("NationalId=" + txtNIDNo.Text);
                if (emp != null)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "এই ন্যাশনাল আইডি’র কর্মি  পূর্বেই সেভ করা হয়েছে. লিস্ট দেখুন";
                    return msg;
                }
                IList<H_Employee> empList = H_Employee.Find("Name='" + txtName.Text.Trim() + "' AND FatherName='" + txtFatherName.Text.Trim() + "'", "");
                if (empList != null && empList.Count > 0)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "এই নামের কর্মি  পূর্বেই সেভ করা হয়েছে. লিস্ট দেখুন Name:" + empList[0].Name + "(" + empList[0].Code.ToString() + ") Father's Name:" + empList[0].FatherName;
                    return msg;

                }
            }
            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                H_Employee h_Employee = GetH_Employee();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [H_Employee]";
                }
                else
                {
                    desc = "Update [H_Employee]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (h_Employee.PermanentAddress.Id == 0)
                {
                    H_Address.Insert(TransactionManager, h_Employee.PermanentAddress);
                    h_Employee.PermanentAddressId = h_Employee.PermanentAddress.Id;
                }
                else
                {
                    H_Address.Update(TransactionManager, h_Employee.PermanentAddress);
                    h_Employee.PermanentAddressId = h_Employee.PermanentAddress.Id;
                }

                if (h_Employee.PresentAddress.Id == 0)
                {
                    H_Address.Insert(TransactionManager, h_Employee.PresentAddress);
                    h_Employee.PresentAddressId = h_Employee.PresentAddress.Id;
                }
                else
                {
                    H_Address.Update(TransactionManager, h_Employee.PresentAddress);
                    h_Employee.PresentAddressId = h_Employee.PresentAddress.Id;
                }

                if (Type == TYPE_ADD)
                {
                    Int32 empCode = Convert.ToInt32(TransactionManager.GetDataSet("SELECT ISNULL(MAX(Code),0) FROM H_EMPLOYEE").Tables[0].Rows[0][0]);
                    
                    h_Employee.Code = empCode+1;
                    H_Employee.Insert(TransactionManager, h_Employee);

                    hdnId.Value = h_Employee.Id.ToString();
                    Type = TYPE_EDIT;

                    //txtId.Text = UIUtility.Format(h_Employee.Id);

                    H_EmployeeDepartment eDepartment = new H_EmployeeDepartment();
                    eDepartment.H_EmployeeId = h_Employee.Id;
                    eDepartment.H_DepartmentId = DBUtility.ToInt32(ddlDepartment.SelectedValue);
                    eDepartment.StartDate = h_Employee.JoiningDate.Value;
                    eDepartment.EndDate = new DateTime(2099, 12, 31);
                    H_EmployeeDepartment.Insert(TransactionManager, eDepartment);

                    H_EmployeeGrade eGrade = new H_EmployeeGrade();
                    eGrade.H_EmployeeId = h_Employee.Id;
                    eGrade.H_GradeId = DBUtility.ToInt32(ddlGrade.SelectedValue);
                    eGrade.StartDate = h_Employee.JoiningDate.Value;
                    eGrade.EndDate = new DateTime(2099, 12, 31);
                    H_EmployeeGrade.Insert(TransactionManager, eGrade);

                    H_EmployeeDesignation eDesignation = new H_EmployeeDesignation();
                    eDesignation.H_EmployeeId = h_Employee.Id;
                    eDesignation.H_DesignationId = DBUtility.ToInt32(ddlDesignation.SelectedValue);
                    eDesignation.StartDate = h_Employee.JoiningDate.Value;

                    eDesignation.EndDate = new DateTime(2099, 12, 31);
                    H_EmployeeDesignation.Insert(TransactionManager, eDesignation);

                    H_EmployeeBranch eBranch = new H_EmployeeBranch();
                    eBranch.H_EmployeeId = h_Employee.Id;
                    eBranch.BranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);                
                    eBranch.StartDate = h_Employee.JoiningDate.Value;
                    eBranch.EndDate = new DateTime(2099, 12, 31);
                    H_EmployeeBranch.Insert(TransactionManager, eBranch);
                
                    H_EmployeePhoto h_EmployeePhoto = new H_EmployeePhoto();
                    h_EmployeePhoto.H_EmployeeId = h_Employee.Id;
                    if (Upload.PostedFile.ContentLength > 0)
                    {
                        HttpPostedFile File = Upload.PostedFile;
                        Byte[] Data = new Byte[File.ContentLength];
                        File.InputStream.Read(Data, 0, File.ContentLength);
                        h_EmployeePhoto.Photo = Data;
                        H_EmployeePhoto.Insert(TransactionManager, h_EmployeePhoto);
                    }
                
                

                    txtId.Text = h_Employee.Code.ToString();
                }
                else
                {
                    H_Employee.Update(TransactionManager, h_Employee);
                    H_EmployeeDepartment eDepartment = H_EmployeeDepartment.Find("H_EmployeeId=" + h_Employee.Id, "EndDate")[0];
                    eDepartment.H_EmployeeId = h_Employee.Id;
                    eDepartment.H_DepartmentId = DBUtility.ToInt32(ddlDepartment.SelectedValue);
                    eDepartment.StartDate = h_Employee.JoiningDate.Value;
                    H_EmployeeDepartment.Update(TransactionManager, eDepartment);

                    H_EmployeeGrade eGrade = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate")[0];
                    eGrade.H_EmployeeId = h_Employee.Id;
                    eGrade.H_GradeId = DBUtility.ToInt32(ddlGrade.SelectedValue);
                    eGrade.StartDate = h_Employee.JoiningDate.Value;
                    H_EmployeeGrade.Update(TransactionManager, eGrade);

                    H_EmployeeDesignation eDesignation = H_EmployeeDesignation.Find("H_EmployeeId=" + h_Employee.Id, "EndDate")[0];
                    eDesignation.H_EmployeeId = h_Employee.Id;
                    eDesignation.H_DesignationId = DBUtility.ToInt32(ddlDesignation.SelectedValue);
                    eDesignation.StartDate = h_Employee.JoiningDate.Value;
                    H_EmployeeDesignation.Update(TransactionManager, eDesignation);

                    H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate")[0];
                    eBranch.H_EmployeeId = h_Employee.Id;
                    eBranch.BranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
                    eBranch.StartDate = h_Employee.JoiningDate.Value;
                    H_EmployeeBranch.Update(TransactionManager, eBranch);

                    H_EmployeePhoto h_EmployeePhoto = H_EmployeePhoto.GetByH_EmployeeId(h_Employee.Id);
                    if (Upload.PostedFile.ContentLength > 0)
                    {
                        HttpPostedFile File = Upload.PostedFile;
                        Byte[] Data = new Byte[File.ContentLength];
                        File.InputStream.Read(Data, 0, File.ContentLength);

                        if (h_EmployeePhoto != null)
                        {
                            h_EmployeePhoto.Photo = Data;
                            H_EmployeePhoto.Update(TransactionManager, h_EmployeePhoto);
                        }
                        else
                        {
                            h_EmployeePhoto = new H_EmployeePhoto();
                            h_EmployeePhoto.H_EmployeeId = h_Employee.Id;
                            h_EmployeePhoto.Photo = Data;
                            H_EmployeePhoto.Insert(TransactionManager, h_EmployeePhoto);
                        }
                    }
                
                
                }

                TransactionManager.Commit();

                hlAcademicQualification.Enabled = true;
                hlProfessionalQualification.Enabled = true;
                hlTraining.Enabled = true;
                hlExperience.Enabled = true;
                hlFileupload.Enabled = true;

                hlAcademicQualification.NavigateUrl = "~/HRM/H_AcademicQualificationList.aspx?H_EmployeeId=" + h_Employee.Id;
                hlProfessionalQualification.NavigateUrl = "~/HRM/H_ProfessionalQualificationList.aspx?H_EmployeeId=" + h_Employee.Id;
                hlTraining.NavigateUrl = "~/HRM/H_TrainingList.aspx?H_EmployeeId=" + h_Employee.Id;
                hlExperience.NavigateUrl = "~/HRM/H_ExperienceList.aspx?H_EmployeeId=" + h_Employee.Id;
                hlFileupload.NavigateUrl = "~/HRM/H_EmployeeInfoUpload.aspx?H_EmployeeId=" + h_Employee.Id;
                H_EmployeePhoto ePhoto = H_EmployeePhoto.GetByH_EmployeeId(h_Employee.Id);
                if (ePhoto.Photo != null)
                {
                    //System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(ePhoto.Photo));
                    //String guid = Guid.NewGuid().ToString();
                    //image.Save(Server.MapPath("~/Images") + "\\Temp\\" + guid + ".jpeg");
                    //this.imgPhoto.ImageUrl = "~/Images/Temp/" + guid + ".jpeg";

                    string base64String = Convert.ToBase64String(ePhoto.Photo, 0, ePhoto.Photo.Length);
                    imgPhoto.ImageUrl = "data:image/jpg;base64," + base64String;
                }
            }

            return msg;
        }

        protected override void LoadData()
        {
            H_Employee h_Employee = null;

            UIUtility.LoadEnums(ddlSex, typeof(H_Employee.Sexes), false, false, false);
            UIUtility.LoadEnums(ddlMaritalStatus, typeof(H_Employee.MaritalStatuses), false, false, false);
            UIUtility.LoadEnums(ddlReligion, typeof(H_Employee.Religions), false, false, false);
            UIUtility.LoadEnums(ddlBloodGroup, typeof(H_Employee.BloodGroups), false, false, true);
            UIUtility.LoadEnums(ddlEmploymentType, typeof(H_Employee.EmploymentTypes), false, false, true);
            UIUtility.LoadEnums(ddlEmployeeStatus, typeof(H_Employee.Statuses), false, false, true);

            ddlPermanentDistrict.DataSource = District.FindAll("Name");
            ddlPermanentDistrict.DataBind();
            ddlPermanentDistrict_SelectedIndexChanged(ddlPermanentDistrict, new EventArgs());

            ddlPresentDistrict.DataSource = District.FindAll("Name");
            ddlPresentDistrict.DataBind();
            ddlPresentDistrict_SelectedIndexChanged(ddlPresentDistrict, new EventArgs());

            ddlZone.DataSource = Zone.FindByLogin("", "Name", User.Identity.Name);
            //this.ddlZone.DataSource = Zone.Find("", "Name");
            ddlZone.DataBind();
            ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

            ddlDepartment.DataSource = H_Department.FindAll("SortOrder");
            ddlDepartment.DataBind();

            ddlGrade.DataSource = H_Grade.FindAll("SortOrder");
            ddlGrade.DataBind();
            ddlGrade_SelectedIndexChanged(ddlGrade, new EventArgs());

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_Employee = H_Employee.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Employee != null)
                {
                    Type = TYPE_EDIT;

                    txtId.Text = UIUtility.Format(h_Employee.Code);
                    txtName.Text = h_Employee.Name;
                    txtNameInBangla.Text = h_Employee.NameInBangla;
                    txtFatherName.Text = h_Employee.FatherName;
                    txtMotherName.Text = h_Employee.MotherName;
                    txtDateOfBirth.Text = UIUtility.Format(h_Employee.DateOfBirth);
                    ddlBloodGroup.SelectedValue = ((Int32)h_Employee.BloodGroup).ToString();
                    ddlSex.SelectedValue = ((Int32)h_Employee.Sex).ToString();
                    ddlMaritalStatus.SelectedValue = ((Int32)h_Employee.MaritalStatus).ToString();
                    ddlReligion.SelectedValue = ((Int32)h_Employee.Religion).ToString();
                    txtAppointmentLetterDate.Text = UIUtility.Format(h_Employee.AppointmentLetterDate);
                    txtAppointmentLetterNo.Text = h_Employee.AppointmentLetterNo;
                    txtJoiningDate.Text = UIUtility.Format(h_Employee.JoiningDate);
                    txtJoiningDate.Enabled = false;
                    ddlEmploymentType.SelectedValue = ((Int32)h_Employee.EmploymentType).ToString();
                    ddlEmploymentType.Enabled = false;
                    ddlEmployeeStatus.SelectedValue = ((Int32)h_Employee.Status).ToString();
                    ddlEmployeeStatus.Enabled = false;
                    txtNIDNo.Text = UIUtility.Format(h_Employee.NationalId);

                    H_EmployeePhoto photo = H_EmployeePhoto.GetByH_EmployeeId(h_Employee.Id);
                    if (photo != null && photo.Photo !=null)//(h_Employee.Photo != null && h_Employee.Photo.Length > 0)
                    {
                        //System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(h_Employee.Photo));
                        //String guid = Guid.NewGuid().ToString();
                        //image.Save(Server.MapPath("~/Images") + "\\Temp\\" + guid + ".jpeg");
                        //this.imgPhoto.ImageUrl = "~/Images/Temp/" + guid + ".jpeg";
                        string base64String = Convert.ToBase64String(photo.Photo, 0, photo.Photo.Length);
                        imgPhoto.ImageUrl = "data:image/jpg;base64," + base64String;
                    }
                    else
                    {
                        imgPhoto.ImageUrl = "~/Images/Temp/default.jpg";
                    }

                    h_Employee.PermanentAddress = H_Address.GetById(h_Employee.PermanentAddressId);
                    h_Employee.PresentAddress = H_Address.GetById(h_Employee.PresentAddressId);

                    txtPermanentVillage.Text = h_Employee.PermanentAddress.Village;
                    txtPermanentPostOffice.Text = h_Employee.PermanentAddress.PostOffice;
                    txtPermanentPostCode.Text = UIUtility.Format(h_Employee.PermanentAddress.PostCode);
                    txtPermanentPhone.Text = h_Employee.PermanentAddress.Phone;
                    txtPermanentEmail.Text = h_Employee.PermanentAddress.Email;

                    ddlPermanentDistrict.SelectedValue = UIUtility.Format(Thana.GetById(h_Employee.PermanentAddress.ThanaId).DistrictId);
                    ddlPermanentDistrict_SelectedIndexChanged(null, null);
                    //ddlPermanentThana.DataSource=Thana.FindByDistrictId(
                    ddlPermanentThana.SelectedValue = UIUtility.Format(h_Employee.PermanentAddress.ThanaId);


                    txtPermanentVillage.ReadOnly = true;
                    txtPermanentPostOffice.ReadOnly = true;
                    txtPermanentPostCode.ReadOnly = true;
                    txtPermanentPhone.ReadOnly = true;
                    txtPermanentEmail.ReadOnly = true;
                    ddlPermanentDistrict.Enabled = false;
                    ddlPermanentThana.Enabled = false;

                    txtPresentVillage.Text = h_Employee.PresentAddress.Village;
                    txtPresentPostOffice.Text = h_Employee.PresentAddress.PostOffice;
                    txtPresentPostCode.Text = UIUtility.Format(h_Employee.PresentAddress.PostCode);
                    txtPresentPhone.Text = h_Employee.PresentAddress.Phone;
                    txtPresentEmail.Text = h_Employee.PresentAddress.Email;

                    ddlPresentDistrict.SelectedValue = UIUtility.Format(Thana.GetById(h_Employee.PresentAddress.ThanaId).DistrictId);
                    ddlPresentDistrict_SelectedIndexChanged(null, null);
                    ddlPresentThana.SelectedValue = UIUtility.Format(h_Employee.PresentAddress.ThanaId);

                    H_EmployeeDepartment eDepartment = H_EmployeeDepartment.Find("H_EmployeeId=" + h_Employee.Id, "EndDate")[0];
                    ddlDepartment.SelectedValue = UIUtility.Format(eDepartment.H_DepartmentId);
                    ddlDepartment.Enabled = false;

                    H_EmployeeGrade eGrade = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate")[0];
                    ddlGrade.SelectedValue = UIUtility.Format(eGrade.H_GradeId);
                    ddlGrade.Enabled = false;
                    ddlGrade_SelectedIndexChanged(ddlGrade, new EventArgs());

                    H_EmployeeDesignation eDesignation = H_EmployeeDesignation.Find("H_EmployeeId=" + h_Employee.Id, "EndDate")[0];
                    ddlDesignation.SelectedValue = UIUtility.Format(eDesignation.H_DesignationId);
                    ddlDesignation.Enabled = false;

                    H_EmployeeBranch eBranch = H_EmployeeBranch.Find("H_EmployeeId=" + h_Employee.Id, "EndDate")[0];
                    Branch branch = Branch.GetById(eBranch.BranchId);
                    Region region = Region.GetById(branch.RegionId);

                    //this.ddlZone.DataSource = Zone.FindByLogin("", "Name", User.Identity.Name);
                    ddlZone.DataSource = Zone.Find("", "Name");
                    ddlZone.DataBind();
                    ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());

                    ddlZone.SelectedValue = UIUtility.Format(Subzone.GetById(region.SubzoneId).ZoneId);
                    ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
                    ddlZone.Enabled = false;

                    ddlSubzone.SelectedValue = UIUtility.Format(region.SubzoneId);
                    ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
                    ddlSubzone.Enabled = false;

                    ddlRegion.SelectedValue = UIUtility.Format(branch.RegionId);
                    ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
                    ddlRegion.Enabled = false;

                    ddlBranch.SelectedValue = UIUtility.Format(eBranch.BranchId);
                    ddlBranch.Enabled = false;

                    hlAcademicQualification.NavigateUrl = "~/HRM/H_AcademicQualificationList.aspx?H_EmployeeId=" + h_Employee.Id;
                    hlProfessionalQualification.NavigateUrl = "~/HRM/H_ProfessionalQualificationList.aspx?H_EmployeeId=" + h_Employee.Id;
                    hlTraining.NavigateUrl = "~/HRM/H_TrainingList.aspx?H_EmployeeId=" + h_Employee.Id;
                    hlExperience.NavigateUrl = "~/HRM/H_ExperienceList.aspx?H_EmployeeId=" + h_Employee.Id;
                    hlFileupload.NavigateUrl = "~/HRM/H_EmployeeInfoUpload.aspx?H_EmployeeId=" + h_Employee.Id;
                    IList<UserRole> role = UserRole.FindByUserLogin(User.Identity.Name, "");
                
                    foreach (UserRole ur in role)
                    {
                        if (ur.RoleName.ToLower() == "employee")
                        {
                            ddlDepartment.Enabled = true;
                            ddlGrade.Enabled = true;
                            ddlDesignation.Enabled = true;
                            ddlZone.Enabled = true;
                            ddlSubzone.Enabled = true;
                            ddlRegion.Enabled = true;
                            ddlBranch.Enabled = true;

                            txtPermanentVillage.ReadOnly = false;
                            txtPermanentPostOffice.ReadOnly = false;
                            txtPermanentPostCode.ReadOnly = false;
                            txtPermanentPhone.ReadOnly = false;
                            txtPermanentEmail.ReadOnly = false;
                            ddlPermanentDistrict.Enabled = true;
                            ddlPermanentThana.Enabled = true;
                            txtJoiningDate.Enabled = true;
                            ddlEmploymentType.Enabled = true;
                            ddlEmployeeStatus.Enabled = true;
                        }
                    }
                
                }
                else
                {
                    hlAcademicQualification.Enabled = false;
                    hlProfessionalQualification.Enabled = false;
                    hlTraining.Enabled = false;
                    hlExperience.Enabled = false;
                    hlFileupload.Enabled = false;

                    imgPhoto.ImageUrl = "~/Images/Temp/default.jpg";
                }
            }
            else
            {
                hlAcademicQualification.Enabled = false;
                hlProfessionalQualification.Enabled = false;
                hlTraining.Enabled = false;
                hlExperience.Enabled = false;
                hlFileupload.Enabled = false;

                imgPhoto.ImageUrl = "~/Images/Temp/default.jpg";
            }
        }

        protected void ddlPermanentDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPermanentDistrict.SelectedValue != null && ddlPermanentDistrict.SelectedValue != "")
            {
                ddlPermanentThana.DataSource = Thana.FindByDistrictId(Convert.ToInt32(ddlPermanentDistrict.SelectedValue), "Name");
                ddlPermanentThana.DataBind();
            }
        }

        protected void ddlPresentDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPresentDistrict.SelectedValue != null && ddlPresentDistrict.SelectedValue != "")
            {
                ddlPresentThana.DataSource = Thana.FindByDistrictId(Convert.ToInt32(ddlPresentDistrict.SelectedValue), "Name");
                ddlPresentThana.DataBind();
            }
        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGrade.SelectedValue != null && ddlGrade.SelectedValue != "")
            {
                TransactionManager tm = new TransactionManager(false);

                ddlDesignation.DataSource = tm.GetDataSet("SELECT H_Designation.Id, Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + ddlGrade.SelectedValue + " ORDER BY SortOrder").Tables[0];
                ddlDesignation.DataBind();
            }
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZone.SelectedValue != null && ddlZone.SelectedValue != "")
            {
                IList<Subzone> subzonelist = null;
                if (Type == TYPE_EDIT)
                {
                    subzonelist = Subzone.Find("ZoneId = " + ddlZone.SelectedValue , "Name");
                }
                else
                {
                    subzonelist = Subzone.FindByLogin("ZoneId = " + ddlZone.SelectedValue + " AND Status=1", "Name", User.Identity.Name);
                }
                ddlSubzone.DataSource = subzonelist;
                ddlSubzone.DataBind();

                ddlSubzone_SelectedIndexChanged(ddlSubzone, new EventArgs());
            } 
        }

        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubzone.SelectedValue != null && ddlSubzone.SelectedValue != "")
            {
                IList<Region> regionlist = null;
                if (Type == TYPE_EDIT)
                {
                    regionlist = Region.Find("SubzoneId = " + ddlSubzone.SelectedValue, "Name");
                }
                else
                {
                    regionlist = Region.FindByLogin("SubzoneId = " + ddlSubzone.SelectedValue + " AND Status=1", "Name", User.Identity.Name);
                }
                ddlRegion.DataSource = regionlist;// Region.Find("SubzoneId = " + this.ddlSubzone.SelectedValue + " AND Status=1", "Name");//, User.Identity.Name);
                ddlRegion.DataBind();
                ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRegion.SelectedValue != null && ddlRegion.SelectedValue != "")
            {
                IList<Branch> branchlist = null;
                if (Type == TYPE_EDIT)
                {
                    branchlist = Branch.Find("RegionId = " + ddlRegion.SelectedValue, "Name");
                }
                else
                {
                    branchlist = Branch.FindByLogin("RegionId = " + ddlRegion.SelectedValue + " AND Status=1", "Name", User.Identity.Name);
                }
                ddlBranch.DataSource = branchlist;// Branch.Find("RegionId = " + this.ddlRegion.SelectedValue + " AND Status=1", "Name");//, User.Identity.Name);
                ddlBranch.DataBind();
            }
        }

        protected void hlRemovePhoto_Click(object sender, EventArgs e)
        {
            imgPhoto.ImageUrl = "~/Images/Temp/default.jpg";
        }
    }
}
