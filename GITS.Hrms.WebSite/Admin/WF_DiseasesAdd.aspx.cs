using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
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

        private WF_Diseases GetH_Grade()
        {
            WF_Diseases h_Grade = null;

            if (Type == TYPE_EDIT)
            {
                h_Grade = WF_Diseases.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_Grade = new WF_Diseases();
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
                WF_Diseases h_Grade = GetH_Grade();
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
                    WF_Diseases.Insert(TransactionManager, h_Grade);

                    hdnId.Value = h_Grade.Id.ToString();
                    Type = TYPE_EDIT;
                }
                else
                {
                    WF_Diseases.Update(TransactionManager, h_Grade);
                }

                TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlStatus, typeof(WF_Diseases.Statuses), false, false,false);
            WF_Diseases h_Grade = null;

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                h_Grade = WF_Diseases.GetById(Convert.ToInt32(hdnId.Value));

                if (h_Grade != null)
                {
                    Type = TYPE_EDIT;

                    txtName.Text = h_Grade.Name;
                    ddlStatus.SelectedValue =((Int32)h_Grade.Status).ToString();
                }
            }


        }

       
        
    }
}
