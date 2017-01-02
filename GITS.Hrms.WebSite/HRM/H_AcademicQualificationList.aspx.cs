using System;
using System.Collections.Generic;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_AcademicQualificationList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_ACADEMICQUALIFICATION LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override object GetDataSource()
        {
            Int32 total = 0;
            IList<H_AcademicQualificationView> list = null;

            if (Request.QueryString["H_EmployeeId"] != null)
            {
                H_Employee h_Employee = H_Employee.GetById(Convert.ToInt32(Request.QueryString["H_EmployeeId"]));

                if (h_Employee != null)
                {
                    this.txtEmployeeName.Text = h_Employee.Name;
                    this.hlBack.NavigateUrl = "~/HRM/H_EmployeeAdd.aspx?Id=" + h_Employee.Id;

                    list = H_AcademicQualificationView.Find("H_EmployeeId = " + h_Employee.Id, "SortOrder", this.PageIndex * this.GridView.PageSize + 1, this.GridView.PageSize, out total);
                }
            }

            this.RecordCount = total;

            return list;
        }
    
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.BaseEntityType = typeof(H_AcademicQualification);
            this.EntityType = typeof(H_AcademicQualificationView);
        }

        protected override string GetAddPageUrl()
        {
            return "H_AcademicQualificationAdd.aspx?H_EmployeeId=" + Request.QueryString["H_EmployeeId"];
        }

    }
}
