﻿using System;
using System.Web.UI.WebControls;

using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.WebSite.Admin
{
    public partial class H_DuplicationAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "LETTERDUPLICATION ADD"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            Asa.Hrms.Data.Entity.H_Duplication h_Duplication = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_Duplication = Asa.Hrms.Data.Entity.H_Duplication.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Duplication != null)
                {
                    this.Type = TYPE_EDIT;
                    txtName.Text = h_Duplication.Name;
                    txtSortOrder.Text = UIUtility.Format(h_Duplication.SortOrder);
                }
            }
        }

        protected override string GetListPageUrl()
        {
            return "H_DuplicationList.aspx";
        }
        private Asa.Hrms.Data.Entity.H_Duplication GetH_Duplication()
        {
            Asa.Hrms.Data.Entity.H_Duplication h_Duplication = null;

            if (this.Type == TYPE_EDIT)
            {
                h_Duplication = Asa.Hrms.Data.Entity.H_Duplication.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Duplication = new Asa.Hrms.Data.Entity.H_Duplication();
            }
            h_Duplication.Name = DBUtility.ToString(txtName.Text);
            h_Duplication.SortOrder = DBUtility.ToInt32(txtSortOrder.Text);


            return h_Duplication;
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
                Asa.Hrms.Data.Entity.H_Duplication h_Duplication = this.GetH_Duplication();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [H_Duplication]";
                }
                else
                {
                    desc = "Update [H_Duplication]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Asa.Hrms.Data.Entity.H_Duplication.Insert(this.TransactionManager, h_Duplication);

                    hdnId.Value = h_Duplication.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Asa.Hrms.Data.Entity.H_Duplication.Update(this.TransactionManager, h_Duplication);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }
    }
}
