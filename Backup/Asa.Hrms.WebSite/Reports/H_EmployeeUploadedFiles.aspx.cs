using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Asa.Hrms.Web;
using Asa.Hrms.Data.Entity;
using Asa.Hrms.Utility;

namespace Asa.Hrms.WebSite.Reports
{
    public partial class H_EmployeeUploadedFiles : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            
        }

        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        protected override Asa.Hrms.Utility.Message Save()
        {
            throw new NotImplementedException();
        }
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));

            if (h_Employee != null)
            {
                hdnId.Value = h_Employee.Id.ToString();
                H_EmployeeDesignation desg = H_EmployeeDesignation.Get("H_EmployeeId=" + h_Employee.Id + " AND EndDate='2099-12-31'");
                H_Designation empDesg = H_Designation.GetById(desg.H_DesignationId);
                txtEmployee.Text = h_Employee.Code.ToString()+": "+ h_Employee.Name ;
                txtDesignation.Text = empDesg.Name;
                IList<H_FileUpload> uploadList = H_FileUpload.GetByH_EmployeeId(h_Employee.Id);
                gvList.DataSource = uploadList;
                gvList.DataBind();            
            }
        }
        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "viewitem")
            {
                Int32 Id = Convert.ToInt32(e.CommandArgument.ToString());
                Response.Redirect("~/HRM/DownloadFile.ashx?Id=" + Id);

            }
            
            
        }
    }
}
