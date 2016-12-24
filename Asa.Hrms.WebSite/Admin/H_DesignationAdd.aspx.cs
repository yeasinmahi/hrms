using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class H_DesignationAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_DESIGNATION ADD"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override string GetListPageUrl()
        {
            return "H_DesignationList.aspx";
        }

        private H_Designation GetH_Designation()
        {
            H_Designation h_Designation = null;

            if (this.Type == TYPE_EDIT)
            {
                h_Designation = H_Designation.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Designation = new H_Designation();
            }

            h_Designation.Name = DBUtility.ToString(txtName.Text);
            h_Designation.BanglaName = DBUtility.ToString(txtBanglaName.Text);
            h_Designation.ShortName = DBUtility.ToString(txtShortName.Text);
            h_Designation.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);
            h_Designation.GroupType = (H_Designation.GroupTypes)DBUtility.ToInt32(ddlGroupType.SelectedValue);
            h_Designation.Status = (H_Designation.Statuses)DBUtility.ToInt32(ddlStatus.SelectedValue);
            return h_Designation;
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

            return msg;
        }

        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                H_Designation h_Designation = this.GetH_Designation();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [H_Designation]";
                }
                else
                {
                    desc = "Update [H_Designation]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    H_Designation.Insert(this.TransactionManager, h_Designation);

                    hdnId.Value = h_Designation.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    H_Designation.Update(this.TransactionManager, h_Designation);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            H_Designation h_Designation = null;
            UIUtility.LoadEnums(ddlGroupType, typeof(H_Designation.GroupTypes), false, false,true);
            UIUtility.LoadEnums(ddlStatus, typeof(H_Designation.Statuses), false, false, true);

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_Designation = H_Designation.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Designation != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = h_Designation.Name;
                    txtBanglaName.Text = h_Designation.BanglaName;
                    txtShortName.Text = h_Designation.ShortName;
                    txtSortOrder.Text = UIUtility.Format(h_Designation.SortOrder);
                    if (h_Designation.GroupType != null)
                    {
                        ddlGroupType.SelectedValue = ((Int32)h_Designation.GroupType).ToString();
                    }
                    ddlStatus.SelectedValue = ((Int32)h_Designation.Status).ToString();
                    TransactionManager tm = new TransactionManager(false);
                    rpGrade.DataSource = tm.GetDataSet("SELECT H_GradeDesignation.Id, Name FROM H_Grade INNER JOIN H_GradeDesignation ON H_GradeId = H_Grade.Id WHERE H_DesignationId = " + h_Designation.Id + " ORDER BY SortOrder").Tables[0];
                    rpGrade.DataBind();
                }
            }

            IList<H_Grade> grades = H_Grade.FindAll("SortOrder");
            grades.Insert(0, new H_Grade());

            ddlGrade.DataSource = grades;
            ddlGrade.DataBind();
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            if (ddlGrade.SelectedValue != "0")
            {
                try
                {
                    H_GradeDesignation gd = new H_GradeDesignation();
                    gd.H_DesignationId = Convert.ToInt32(hdnId.Value);
                    gd.H_GradeId = Convert.ToInt32(ddlGrade.SelectedValue);

                    H_GradeDesignation.Insert(gd);

                    TransactionManager tm = new TransactionManager(false);
                    rpGrade.DataSource = tm.GetDataSet("SELECT H_GradeDesignation.Id, Name FROM H_Grade INNER JOIN H_GradeDesignation ON H_GradeId = H_Grade.Id WHERE H_DesignationId = " + gd.H_DesignationId + " ORDER BY SortOrder").Tables[0];
                    rpGrade.DataBind();
                }
                catch (Exception ex)
                {
                    ShowUIMessage(ex);
                }
            }
        }

        protected void lbDeleteGrade_Click(object sender, EventArgs e)
        {
            LinkButton lbDeleteGrade = (LinkButton)sender;
            Int32 id = Convert.ToInt32(lbDeleteGrade.ClientID.Replace("ctl00_ContentPlaceHolder1_rpGrade_ctl", "").Replace("_lbDeleteGrade", ""));

            try
            {
                H_GradeDesignation.Delete(Convert.ToInt32(((Label)rpGrade.Items[id].FindControl("lblGradeId")).Text));

                TransactionManager tm = new TransactionManager(false);
                rpGrade.DataSource = tm.GetDataSet("SELECT H_GradeDesignation.Id, Name FROM H_Grade INNER JOIN H_GradeDesignation ON H_GradeId = H_Grade.Id WHERE H_DesignationId = " + hdnId.Value + " ORDER BY SortOrder").Tables[0];
                rpGrade.DataBind();
            }
            catch (Exception ex)
            {
                ShowUIMessage(ex);
            }
        }

    }
}
