using System;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Payroll
{
    public partial class P_PayScaleAdd : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            ddlGrade.DataSource = H_Grade.FindAll("Name");
            ddlGrade.DataBind();
            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                P_PayScale p_Loan = P_PayScale.GetById(Convert.ToInt32(hdnId.Value));

                if (p_Loan != null)
                {
                    this.Type = TYPE_EDIT;
                    ddlGrade.SelectedValue = p_Loan.H_GradeId.ToString();
                    txtStartBasic.Text =UIUtility.Format( p_Loan.StartBasic);
                    txtIncrement.Text = UIUtility.Format(p_Loan.Increment);
                    txtTargetBasic.Text = UIUtility.Format(p_Loan.TargetBasic);

                }
            }
            
        }

        protected override string GetListPageUrl()
        {
            return "P_PayScaleList.aspx";
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
            if (this.Type == TYPE_ADD)
            {
                P_PayScale p_PayScale = P_PayScale.GetByGradeId(Convert.ToInt32(ddlGrade.SelectedValue));
                if (p_PayScale != null)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Grade " + ddlGrade.SelectedItem.ToString() + " already Exist";
                    return msg;
                }
            }

            return msg;
        }
        private P_PayScale Get_PayScale()
        {
            P_PayScale entity = null;
            if (this.Type == TYPE_EDIT)
            {
                entity = P_PayScale.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                entity = new P_PayScale();
            }
            entity.H_GradeId = DBUtility.ToInt32(ddlGrade.SelectedValue);
            entity.StartBasic = DBUtility.ToDouble(txtStartBasic.Text);
            entity.Increment = DBUtility.ToDouble(txtIncrement.Text);
            entity.TargetBasic = DBUtility.ToDouble(txtTargetBasic.Text);

            return entity;
        }
        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                P_PayScale p_Loan = this.Get_PayScale();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [P_PayScale]";
                }
                else
                {
                    desc = "Update [P_PayScale]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    P_PayScale.Insert(this.TransactionManager, p_Loan);

                    hdnId.Value = p_Loan.Id.ToString();
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    P_PayScale.Update(this.TransactionManager, p_Loan);
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }
    }
}
