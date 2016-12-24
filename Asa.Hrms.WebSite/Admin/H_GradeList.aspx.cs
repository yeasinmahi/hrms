using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class H_GradeList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_GRADE LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(H_Grade);
        }

        protected override string GetAddPageUrl()
        {
            return "H_GradeAdd.aspx";
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                TransactionManager tm = new TransactionManager(false);
                BulletedList blDesignation = (BulletedList)e.Row.FindControl("blDesignation");

                blDesignation.DataSource = tm.GetDataSet("SELECT Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + ((H_Grade)e.Row.DataItem).Id + " ORDER BY SortOrder").Tables[0];
                blDesignation.DataBind();
            }
        }

    }
}
