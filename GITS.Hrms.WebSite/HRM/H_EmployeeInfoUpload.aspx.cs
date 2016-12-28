using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeInfoUpload : AddPage
    {
        protected override string PropertyName
        {
            get{ return "H_FILEUPLOAD ADD";}
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            if (Request.QueryString["H_EmployeeId"] != null)
            {
                hdnId.Value = Request.QueryString["H_EmployeeId"];
                H_Employee employee = H_Employee.GetById(Convert.ToInt32(hdnId.Value));
                H_EmployeeDesignation desg = H_EmployeeDesignation.Get("H_EmployeeId=" + employee.Id +" AND EndDate='2099-12-31'");
                H_Designation empDesg = H_Designation.GetById(desg.H_DesignationId);
                txtEmployee.Text = employee.Name + "(" + employee.Code.ToString() + ")";
                txtDesignation.Text = empDesg.Name;
                IList<H_FileUpload> uploadList = H_FileUpload.GetByH_EmployeeId(Convert.ToInt32(hdnId.Value));
                gvList.DataSource = uploadList;
                gvList.DataBind();
                this.hlBack.NavigateUrl = "~/HRM/H_EmployeeAdd.aspx?Id=" + employee.Id.ToString();
            }
        }

        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }
        private new Message Validate()
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Record saved successfully.";

            base.Validate();

            if (base.IsValid == false)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid data provided or required data missing";
                return msg;
            }
            if (this.Type == TYPE_ADD)
            {
                if (!FileUpload1.HasFile)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "No File Selected";
                    return msg;
                }
            }
            //if (this.Type == TYPE_EDIT)
            //{
            //    IList<UserRole> role = UserRole.FindByUserLogin(User.Identity.Name, "");
            //    bool Permitted = false;
            //    foreach (UserRole ur in role)
            //    {
            //        if (ur.RoleName.ToLower() == "employee")
            //        {
            //            Permitted = true;
            //        }
            //    }
            //    if (!Permitted)
            //    {
            //        msg.Type = MessageType.Error;
            //        msg.Msg = "You Have no Update Permission";
            //        return msg;
            //    }
            //}

            return msg;
        }
        protected override Message Save()
        {
            Message msg = this.Validate();
            String uploadPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString();
            if (msg.Type == MessageType.Information)
            {
                H_FileUpload h_FileUpload = this.GetH_FileUpload();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [H_FileUpload]";
                }
                else
                {
                    desc = "Update [H_FileUpload]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    H_FileUpload.Insert(this.TransactionManager, h_FileUpload);
                    FileUpload1.SaveAs(uploadPath + h_FileUpload.FileName);
                    hdnUploadId.Value = h_FileUpload.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    H_FileUpload.Update(this.TransactionManager, h_FileUpload);
                    if (FileUpload1.HasFile)
                    {
                        string oldFile = uploadPath + h_FileUpload.FileName;
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                        FileUpload1.SaveAs(uploadPath + h_FileUpload.FileName);
                    }
                }
                
                this.TransactionManager.Commit();
                LoadFile(Convert.ToInt32(hdnId.Value));
            }

            return msg;
        }

        private H_FileUpload GetH_FileUpload()
        {
            H_FileUpload h_FileUpload = null;

            if (this.Type == TYPE_EDIT)
            {
                h_FileUpload = H_FileUpload.GetById(Convert.ToInt32(hdnUploadId.Value));
            }
            else
            {
                h_FileUpload = new H_FileUpload();
                h_FileUpload.H_EmployeeId = Convert.ToInt32(hdnId.Value);
                if (FileUpload1.HasFile)
                {
                    Guid uniquename = Guid.NewGuid();
                    String ext = System.IO.Path.GetExtension(FileUpload1.FileName);
                    h_FileUpload.FileName = uniquename.ToString() + ext;
                }
            }

            h_FileUpload.Title = DBUtility.ToString(txtTitle.Text);
            h_FileUpload.UploadDate = DateTime.Today.Date;

            


            return h_FileUpload;
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewitem")
            {
                Int32 Id = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect("~/HRM/DownloadFile.ashx?Id="+Id);

            }
            if (e.CommandName == "editrow")
            {
                Int32 Id = Convert.ToInt32(e.CommandArgument.ToString());
                H_FileUpload h_FileUpload = H_FileUpload.GetById(Id);
                txtTitle.Text = h_FileUpload.Title;
                hdnUploadId.Value = h_FileUpload.Id.ToString();
                this.Type = TYPE_EDIT;

            }
            if (e.CommandName == "deleterow")
            {
                Int32 Id = Convert.ToInt32(e.CommandArgument.ToString());
                H_FileUpload h_FileUpload = H_FileUpload.GetById(Id);
                TransactionManager tm = new TransactionManager(true);
                H_FileUpload.Delete(tm, Id);
                tm.Commit();
                string fileloc = Server.MapPath("~/Images/Temp/" + h_FileUpload.FileName);
                if (System.IO.File.Exists(fileloc))
                {
                    System.IO.File.Delete(fileloc);
                }
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadFile(Convert.ToInt32(hdnid.Value));
            }
        }

        private void LoadFile(int h_EmployeeId)
        {
            IList<H_FileUpload> uploadList = H_FileUpload.GetByH_EmployeeId(h_EmployeeId);
            gvList.DataSource = uploadList;
            gvList.DataBind();
                        
        }
    }
}
