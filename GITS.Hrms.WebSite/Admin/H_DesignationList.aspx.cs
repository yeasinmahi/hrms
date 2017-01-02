using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class H_DesignationList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_DESIGNATION LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.EntityType = typeof(H_Designation);
        }

        protected override string GetAddPageUrl()
        {
            return "H_DesignationAdd.aspx";
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                TransactionManager tm = new TransactionManager(false);
                BulletedList blGrade = (BulletedList)e.Row.FindControl("blGrade");

                blGrade.DataSource = tm.GetDataSet("SELECT Name FROM H_Grade INNER JOIN H_GradeDesignation ON H_GradeId = H_Grade.Id WHERE H_DesignationId = " + ((H_Designation)e.Row.DataItem).Id + " ORDER BY SortOrder").Tables[0];
                blGrade.DataBind();
            }
        }
    }
}
