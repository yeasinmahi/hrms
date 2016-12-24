using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;
using System.Linq;

namespace Asa.Hrms.WebSite.HRM
{
    public partial class H_FnalPaymentAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_FINALPAYMENT ADD"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override string GetListPageUrl()
        {
            return "H_FinalPaymentList.aspx";
        }
        protected override void LoadData()
        {

        }
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                hdnEmpId.Value = "";
                hdnId.Value = "";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";
                txtSubzone.Text = "";
                txtBranch.Text = "";
                txtEmployee.Text =h_Employee.FullName;
                gvFinalPayment.DataSource = null;
                gvFinalPayment.DataBind();
                if (h_Employee.Status != H_Employee.Statuses.Dropped)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    this.ShowUIMessage(msg);
                    return;
                }
                H_FinalPayment h_FinalPayment = H_FinalPayment.Get("H_EmployeeId=" + h_Employee.Id);
                
                if (h_FinalPayment != null)
                {
                    H_EmployeeDesignation eDesg = H_EmployeeDesignation.Find("H_EmployeeId=" + h_Employee.Id," EndDate DESC")[0];
                    H_Designation desg = H_Designation.GetById(eDesg.H_DesignationId);
                    List<H_FinalPayment> final = new List<H_FinalPayment>();
                    final.Add(h_FinalPayment);
                    var result = from f in final
                                 select new
                                 {
                                     Name = h_Employee.Name,
                                     Code = h_Employee.Code,
                                     Designation=desg.Name,
                                     Letter_No = f.LetterNo,
                                     Letter_Date = f.LetterDate,
                                     Closing_Date = f.ClosingDate,
                                     Net_Pay = f.NetPay

                                 };

                    gvFinalPayment.DataSource = UIUtility.LINQToDataTable(result);
                    gvFinalPayment.DataBind();
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid Operation. Final Payment Already Posted for this Employee";
                    this.ShowUIMessage(msg);
                    return;
                }

                hdnEmpId.Value = h_Employee.Id.ToString();

                H_EmployeeDepartment eDepartment = H_EmployeeDepartment.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDepartment.Text = H_Department.GetById(eDepartment.H_DepartmentId).Name;

                H_EmployeeGrade eGrade = H_EmployeeGrade.Find("H_EmployeeId=" + h_Employee.Id, "EndDate DESC")[0];
                txtGrade.Text = H_Grade.GetById(eGrade.H_GradeId).Name;

                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;

                TransactionManager tm = new TransactionManager(false);

                DataTable dt = tm.GetDataSet("SELECT ZoneId, SubzoneId, RegionId, BranchId, StartDate FROM H_EmployeeBranch INNER JOIN Branch ON BranchId = Branch.Id INNER JOIN Region ON RegionId = Region.Id INNER JOIN Subzone ON SubzoneId = Subzone.Id WHERE H_EmployeeId = " + h_Employee.Id + " ORDER BY EndDate DESC").Tables[0];
                Int32 z = 0;
                Int32 s = 0;
                Int32 r = 0;
                Int32 b = 0;

                for (Int32 i = 1; i < dt.Rows.Count; i++)
                {
                    if (z == i - 1 && dt.Rows[i - 1]["ZoneId"].ToString() == dt.Rows[i]["ZoneId"].ToString())
                    {
                        z = i;
                    }

                    if (s == i - 1 && dt.Rows[i - 1]["SubzoneId"].ToString() == dt.Rows[i]["SubzoneId"].ToString())
                    {
                        s = i;
                    }

                    if (r == i - 1 && dt.Rows[i - 1]["RegionId"].ToString() == dt.Rows[i]["RegionId"].ToString())
                    {
                        r = i;
                    }

                    if (b == i - 1 && dt.Rows[i - 1]["BranchId"].ToString() == dt.Rows[i]["BranchId"].ToString())
                    {
                        b = i;
                    }
                }

                Branch branch = Branch.GetById(DBUtility.ToInt32(dt.Rows[b]["BranchId"]));
                Region region = Region.GetById(branch.RegionId);

                txtSubzone.Text = Subzone.GetById(region.SubzoneId).Name;

                txtBranch.Text = branch.Name;


            }
            else
            {
                hdnEmpId.Value = "";
                hdnId.Value = "";
                txtDepartment.Text = "";
                txtGrade.Text = "";
                txtDesignation.Text = "";
                txtSubzone.Text = "";
                txtBranch.Text = "";

                if (this.txtEmployee.Text.Trim() != "")
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "No employee found";
                    this.ShowUIMessage(msg);
                }
            }
        }
        private Asa.Hrms.Data.Entity.H_FinalPayment GetH_FinalPayment()
        {
            H_FinalPayment h_FinalPayment = null;
            if (this.Type == TYPE_EDIT)
            {
                h_FinalPayment = H_FinalPayment.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                h_FinalPayment = new H_FinalPayment();
                h_FinalPayment.H_EmployeeId = DBUtility.ToInt32(hdnEmpId.Value);
            }
            h_FinalPayment.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_FinalPayment.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_FinalPayment.ClosingDate = DBUtility.ToDateTime(txtClosingDate.Text);
            h_FinalPayment.NetPay = DBUtility.ToDouble(txtNetPay.Text);

            return h_FinalPayment;
        }
        protected override Message Save()
        {
            Message msg = this.Validate();
            if (msg.Type == MessageType.Information)
            {
                Asa.Hrms.Data.Entity.H_FinalPayment h_FinalPay = this.GetH_FinalPayment();
                string desc = null;
                if (this.Type == TYPE_EDIT)
                {
                    desc = "Insert [H_FinalPayment]";
                }
                else
                {
                    desc = "Insert [H_FinalPayment]";
                }

                this.TransactionManager = new TransactionManager(true, desc);
                if (this.Type == TYPE_EDIT)
                {
                    Asa.Hrms.Data.Entity.H_FinalPayment.Update(this.TransactionManager, h_FinalPay);
                }
                else
                {
                    Asa.Hrms.Data.Entity.H_FinalPayment.Insert(this.TransactionManager, h_FinalPay);
                }


                hdnId.Value = h_FinalPay.Id.ToString();
                this.Type = TYPE_EDIT;

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
            H_FinalPayment h_FinalPayment = H_FinalPayment.Get("H_EmployeeId=" + hdnEmpId.Value);
            if (h_FinalPayment != null)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid Operation. Final Payment Already Posted for this Employee";
                return msg;
            }
            return msg;
        }

    }
}
