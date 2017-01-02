using System;
using System.Collections.Generic;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_TrainingList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_TRAINING LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override object GetDataSource()
        {
            Int32 total = 0;
            IList<H_Training> list = null;

            if (Request.QueryString["H_EmployeeId"] != null)
            {
                H_Employee h_Employee = H_Employee.GetById(Convert.ToInt32(Request.QueryString["H_EmployeeId"]));

                if (h_Employee != null)
                {
                    this.txtEmployeeName.Text = h_Employee.Name;
                    this.hlBack.NavigateUrl = "~/HRM/H_EmployeeAdd.aspx?Id=" + h_Employee.Id;

                    list = H_Training.Find("H_EmployeeId = " + h_Employee.Id, "SortOrder DESC", this.PageIndex * this.GridView.PageSize + 1, this.GridView.PageSize, out total);
                }
            }

            this.RecordCount = total;

            return list;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(H_Training);
        }

        protected override string GetAddPageUrl()
        {
            return "H_TrainingAdd.aspx?H_EmployeeId=" + Request.QueryString["H_EmployeeId"];
        }

    }
}
