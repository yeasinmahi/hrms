using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;
using System.Collections.Generic;

namespace Asa.Hrms.WebSite.Admin
{
    public partial class WF_DiseasesAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "WF_DISEASES ADD"; }
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
            return "WF_DiseasesList.aspx";
        }

        private Asa.Hrms.Data.Entity.WF_Diseases GetH_Grade()
        {
            Asa.Hrms.Data.Entity.WF_Diseases h_Grade = null;

            if (this.Type == TYPE_EDIT)
            {
                h_Grade = Asa.Hrms.Data.Entity.WF_Diseases.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Grade = new Asa.Hrms.Data.Entity.WF_Diseases();
            }

            h_Grade.Name = DBUtility.ToString(txtName.Text);
            h_Grade.Status = (WF_Diseases.Statuses)DBUtility.ToInt32(ddlStatus.SelectedValue);

            return h_Grade;
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
                Asa.Hrms.Data.Entity.WF_Diseases h_Grade = this.GetH_Grade();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [H_Grade]";
                }
                else
                {
                    desc = "Update [H_Grade]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Asa.Hrms.Data.Entity.WF_Diseases.Insert(this.TransactionManager, h_Grade);

                    hdnId.Value = h_Grade.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Asa.Hrms.Data.Entity.WF_Diseases.Update(this.TransactionManager, h_Grade);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlStatus, typeof(WF_Diseases.Statuses), false, false,false);
            Asa.Hrms.Data.Entity.WF_Diseases h_Grade = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_Grade = Asa.Hrms.Data.Entity.WF_Diseases.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Grade != null)
                {
                    this.Type = TYPE_EDIT;

                    txtName.Text = h_Grade.Name;
                    ddlStatus.SelectedValue =((Int32)h_Grade.Status).ToString();
                }
            }


        }

       
        
    }
}
