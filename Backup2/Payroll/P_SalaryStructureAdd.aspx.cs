using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Asa.Hrms.Web;
using Asa.Hrms.Data.Entity;
using Asa.Hrms.Utility;
using System.Data;
using Asa.Hrms.Data.Procedure;

namespace Asa.Hrms.WebSite
{
    public partial class P_SalaryStructureAdd : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            
        }

        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        private List<P_EmployeeEarning> GetP_EmployeeEarning()
        {
            List<P_EmployeeEarning> earningList = new List<P_EmployeeEarning>();
            foreach (GridViewRow row in gvEarning.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (((CheckBox)row.FindControl("chkSelect")).Checked)
                    {
                        P_EmployeeEarning entity = new P_EmployeeEarning();
                        entity.H_EmployeeId = Convert.ToInt32(hdnId.Value);
                        entity.P_EarningId = Convert.ToInt32(gvEarning.DataKeys[row.RowIndex]["Id"]);
                        entity.Amount = DBUtility.ToDouble(((TextBox)row.FindControl("txtAmount")).Text);
                        earningList.Add(entity);
                    }
                }
            }


            return earningList;
        }
        private List<P_EmployeeDeduction> GetP_EmployeeDeduction()
        {
            List<P_EmployeeDeduction> earningList = new List<P_EmployeeDeduction>();
            foreach (GridViewRow row in gvEarning.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (((CheckBox)row.FindControl("chkSelect")).Checked)
                    {
                        P_EmployeeDeduction entity = new P_EmployeeDeduction();
                        entity.H_EmployeeId = Convert.ToInt32(hdnId.Value);
                        entity.P_DeductionId = Convert.ToInt32(gvEarning.DataKeys[row.RowIndex]["Id"]);
                        entity.Amount = DBUtility.ToDouble(((TextBox)row.FindControl("txtAmount")).Text);
                        earningList.Add(entity);
                    }
                }
            }


            return earningList;
        }
        protected override Asa.Hrms.Utility.Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                string desc = null;
                if (ddlSalaryType.SelectedValue == "1")
                {
                    List<P_EmployeeEarning> earningList = GetP_EmployeeEarning();
                    desc = "Insert [P_EmployeeEarning]";


                    this.TransactionManager = new Asa.Hrms.Data.TransactionManager(true, desc);
                    P_EmployeeEarning.Delete(TransactionManager, "H_EmployeeId=" + hdnId.Value);
                    foreach (P_EmployeeEarning entity in earningList)
                    {
                        P_EmployeeEarning.Insert(TransactionManager, entity);
                    }
                }
                else
                {
                    List<P_EmployeeDeduction> earningList = GetP_EmployeeDeduction();
                    desc = "Insert [P_EmployeeDeduction]";


                    this.TransactionManager = new Asa.Hrms.Data.TransactionManager(true, desc);
                    P_EmployeeDeduction.Delete(TransactionManager, "H_EmployeeId=" + hdnId.Value);
                    foreach (P_EmployeeDeduction entity in earningList)
                    {
                        P_EmployeeDeduction.Insert(TransactionManager, entity);
                    }
                }

                

                this.TransactionManager.Commit();
            }

            return msg;
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
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                hdnId.Value = "0";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";

                if (h_Employee.Status != H_Employee.Statuses.Working)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    this.ShowUIMessage(msg);
                    return;
                }

                this.Type = TYPE_ADD;
                hdnId.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                H_EmployeeDepartment eDepartment = H_EmployeeDepartment.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDepartment.Text = H_Department.GetById(eDepartment.H_DepartmentId).Name;

                H_EmployeeGrade eGrade = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                txtGrade.Text = H_Grade.GetById(eGrade.H_GradeId).Name;

                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                //IList<P_Earning> earning = P_Earning.FindAll();
                //IList<Asa.Hrms.Data.Entity.P_EmployeeEarning> eEarning = Asa.Hrms.Data.Entity.P_EmployeeEarning.FindByEmployeeId(h_Employee.Id, "");
                //var query = from ea in earning
                //            join ee in eEarning on ea.Id equals ee.P_EarningId into gj
                //            from subpet in gj.DefaultIfEmpty(new Asa.Hrms.Data.Entity.P_EmployeeEarning())
                //            select new
                //            {
                //                ea.Id,
                //                ea.Name,
                //                FixedPercent = (ea.IsFixed ? "Fixed" : "Percent"),
                //                Amount =subpet.Amount// DBUtility.ToNullableDouble(subpet.Amount)// (subpet.Amount == null ? 0.0 : subpet.Amount)

                //            };
                //DataTable dt = UIUtility.LINQToDataTable(query);

                //gvEarning.DataSource = dt;
                //gvEarning.DataBind();
                
            }
            else
            {
                hdnId.Value = "0";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";


                if (this.txtEmployee.Text.Trim() != "")
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "No employee found";
                    this.ShowUIMessage(msg);
                }
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
            string CurrentCbxId = ((CheckBox)sender).ClientID;
            foreach (GridViewRow Row in gvEarning.Rows)
            {
                RequiredFieldValidator rfvAttributeType = (RequiredFieldValidator)Row.FindControl("rfvAmount");
                if (((CheckBox)Row.FindControl("chkSelect")).ClientID.Equals(CurrentCbxId))
                {
                    rfvAttributeType.Enabled = cbx.Checked;
                }
            }
        }

        protected void ddlSalaryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlSalaryType.SelectedValue !="0")
            if (hdnId.Value != "" && hdnId.Value != "0")
            {
                DataTable dt = null;
                
                if (ddlSalaryType.SelectedValue == "1")
                {
                    IList<Asa.Hrms.Data.Entity.P_EmployeeEarning> eEarning = Asa.Hrms.Data.Entity.P_EmployeeEarning.FindByEmployeeId(Convert.ToInt32(hdnId.Value), "");
                    IList<P_Earning> salary = P_Earning.FindAll();
                    var query = from ea in salary
                                join ee in eEarning on ea.Id equals ee.P_EarningId into gj
                                from subpet in gj.DefaultIfEmpty(new Asa.Hrms.Data.Entity.P_EmployeeEarning())
                                select new
                                {
                                    ea.Id,
                                    ea.Name,
                                    FixedPercent = (ea.IsFixed ? "Fixed" : "Percent"),
                                    Amount = subpet.Amount// DBUtility.ToNullableDouble(subpet.Amount)// (subpet.Amount == null ? 0.0 : subpet.Amount)

                                };
                    dt = UIUtility.LINQToDataTable(query);
                }
                else
                {
                    IList<Asa.Hrms.Data.Entity.P_EmployeeDeduction> eEarning = Asa.Hrms.Data.Entity.P_EmployeeDeduction.FindByEmployeeId(Convert.ToInt32(hdnId.Value), "");
                    IList<P_Deduction> salary = P_Deduction.FindAll();
                     var query = from ea in salary
                                join ee in eEarning on ea.Id equals ee.P_DeductionId into gj
                                from subpet in gj.DefaultIfEmpty(new Asa.Hrms.Data.Entity.P_EmployeeDeduction())
                                select new
                                {
                                    ea.Id,
                                    ea.Name,
                                    FixedPercent = (ea.IsFixed ? "Fixed" : "Percent"),
                                    Amount = subpet.Amount// DBUtility.ToNullableDouble(subpet.Amount)// (subpet.Amount == null ? 0.0 : subpet.Amount)

                                };
                     dt = UIUtility.LINQToDataTable(query);
                }
                
                gvEarning.DataSource = dt;
                gvEarning.DataBind();
            }
        }
    }
}
