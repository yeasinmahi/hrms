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
                    txtEmployeeName.Text = h_Employee.Name;
                    hlBack.NavigateUrl = "~/HRM/H_EmployeeAdd.aspx?Id=" + h_Employee.Id;

                    list = H_AcademicQualificationView.Find("H_EmployeeId = " + h_Employee.Id, "SortOrder", PageIndex * GridView.PageSize + 1, GridView.PageSize, out total);
                }
            }

            RecordCount = total;

            return list;
        }
    
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            GridView = gvList;
            BaseEntityType = typeof(H_AcademicQualification);
            EntityType = typeof(H_AcademicQualificationView);
        }

        protected override string GetAddPageUrl()
        {
            return "H_AcademicQualificationAdd.aspx?H_EmployeeId=" + Request.QueryString["H_EmployeeId"];
        }

    }
}
