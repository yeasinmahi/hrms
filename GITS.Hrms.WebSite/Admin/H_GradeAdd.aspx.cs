using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class H_GradeAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_GRADE ADD"; }
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
            return "H_GradeList.aspx";
        }

        private H_Grade GetH_Grade()
        {
            H_Grade h_Grade = null;

            if (Type == TYPE_EDIT)
            {
                h_Grade = H_Grade.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Grade = new H_Grade();
            }

            h_Grade.Name = DBUtility.ToString(txtName.Text);
            h_Grade.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);

            return h_Grade;
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

            return msg;
        }

        protected override Message Save()
        {
            Message msg = Validate();

            if (msg.Type == MessageType.Information)
            {
                H_Grade h_Grade = GetH_Grade();
                string desc = "";

                if (Type == TYPE_ADD)
                {
                    desc = "Insert [H_Grade]";
                }
                else
                {
                    desc = "Update [H_Grade]";
                }

                TransactionManager = new TransactionManager(true, desc);

                if (Type == TYPE_ADD)
                {
                    H_Grade.Insert(TransactionManager, h_Grade);

                    hdnId.Value = h_Grade.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    H_Grade.Update(TransactionManager, h_Grade);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            H_Grade h_Grade = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_Grade = H_Grade.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Grade != null)
                {
                    Type = TYPE_EDIT;

                    txtName.Text = h_Grade.Name;
                    txtSortOrder.Text = UIUtility.Format(h_Grade.SortOrder);

                    TransactionManager tm = new TransactionManager(false);

                    rpDesignation.DataSource = tm.GetDataSet("SELECT H_GradeDesignation.Id, Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + h_Grade.Id + " ORDER BY SortOrder").Tables[0];
                    rpDesignation.DataBind();
                }
            }

            IList<H_Designation> designations = H_Designation.FindAll("SortOrder");
            designations.Insert(0, new H_Designation());

            ddlDesignation.DataSource = designations;
            ddlDesignation.DataBind();
        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            if (ddlDesignation.SelectedValue != "0")
            {
                try
                {
                    H_GradeDesignation gd = new H_GradeDesignation();
                    gd.H_GradeId = Convert.ToInt32(hdnId.Value);
                    gd.H_DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);

                    H_GradeDesignation.Insert(gd);

                    TransactionManager tm = new TransactionManager(false);
                    rpDesignation.DataSource = tm.GetDataSet("SELECT H_GradeDesignation.Id, Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + gd.H_GradeId + " ORDER BY SortOrder").Tables[0];
                    rpDesignation.DataBind();
                }
                catch (Exception ex)
                {
                    ShowUiMessage(ex);
                }
            }
        }

        protected void lbDeleteDesignation_Click(object sender, EventArgs e)
        {
            LinkButton lbDeleteDesignation = (LinkButton)sender;
            Int32 id = Convert.ToInt32(lbDeleteDesignation.ClientID.Replace("ctl00_ContentPlaceHolder1_rpDesignation_ctl", "").Replace("_lbDeleteDesignation", ""));

            try
            {
                H_GradeDesignation.Delete(Convert.ToInt32(((Label)rpDesignation.Items[id].FindControl("lblDesignationId")).Text));

                TransactionManager tm = new TransactionManager(false);
                rpDesignation.DataSource = tm.GetDataSet("SELECT H_GradeDesignation.Id, Name FROM H_Designation INNER JOIN H_GradeDesignation ON H_DesignationId = H_Designation.Id WHERE H_GradeId = " + hdnId.Value + " ORDER BY SortOrder").Tables[0];
                rpDesignation.DataBind();
            }
            catch (Exception ex)
            {
                ShowUiMessage(ex);
            }
        }

    }
}
