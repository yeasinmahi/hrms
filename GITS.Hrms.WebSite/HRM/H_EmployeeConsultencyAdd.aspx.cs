using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeConsultencyAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_CONSULTENCY ADD"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected override string GetListPageUrl()
        {
            return "H_EmployeeList.aspx";
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
                H_Employee employee = H_Employee.GetById(Convert.ToInt32(hdnId.Value));
                if (employee.Status.Equals(H_Employee.Statuses.Consultancy))
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "The Employee Already in Consultency";
                    return msg;
                }

            }
            return msg;
        }
        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                H_EmployeeConsultency h_Consultency = this.GetH_EmployeeConsultency();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [H_EmployeeConsultency]";
                }
                else
                {
                    desc = "Update [H_EmployeeConsultency]";
                }

                this.TransactionManager = new TransactionManager(true, desc);
                if (this.Type == TYPE_ADD)
                {
                    H_EmployeeConsultency.Insert(this.TransactionManager,h_Consultency);
                    H_Employee employee = H_Employee.GetById(h_Consultency.H_EmployeeId);
                    employee.Status = H_Employee.Statuses.Consultancy;
                    H_Employee.Update(this.TransactionManager,employee);
                }
                else
                {
                    H_EmployeeConsultency.Update(this.TransactionManager,h_Consultency);
                }
                this.TransactionManager.Commit();
                this.TransactionManager = new TransactionManager(false);
                string query = "SELECT "
                        + "hec.LetterNo,"
                        + " hec.LetterDate,"
                        + " hec.NgoName,"
                        + " o.Name AS FundName,"
                        + " hec.StartDate,"
                        + " hec.EndDate,"
                        + " c.Name AS Country"
                        + " FROM "
                        + " H_EmployeeConsultency AS hec INNER JOIN Country AS c on hec.CountryId=c.Id"
                        + " INNER JOIN Organization AS o ON hec.OrganizationId=o.Id where hec.H_EmployeeID=" + h_Consultency.H_EmployeeId + " ORDER BY hec.LetterDate Desc";


                DataSet ds = TransactionManager.GetDataSet(query);
                DataTable dtleave = ds.Tables[0];
                gvList.DataSource = dtleave;
                gvList.DataBind();

                this.Type = TYPE_EDIT;
                hfConsultId.Value = h_Consultency.Id.ToString();
            }
            
            return msg;
        }
        private H_EmployeeConsultency GetH_EmployeeConsultency()
        {
            //TransactionManager tm = new TransactionManager(false);
            
            H_EmployeeConsultency h_Consultency = null;

            if (this.Type == TYPE_EDIT)
            {
               IList<H_EmployeeConsultency> list = H_EmployeeConsultency.Find("H_EmployeeId="+Convert.ToInt32(hdnId.Value)+" AND Status=1","");
               h_Consultency = list[0];

            }
            else
            {
                h_Consultency = new H_EmployeeConsultency();
                
            }
            h_Consultency.H_EmployeeId = Convert.ToInt32(hdnId.Value);
            h_Consultency.LetterNo = txtLetterNo.Text;
            h_Consultency.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_Consultency.NgoName = txtNgoName.Text;
            h_Consultency.Through = DBUtility.ToString(txtThrough.Text.Trim());
            h_Consultency.Phone = DBUtility.ToNullableString(txtPhone.Text);
            h_Consultency.Fax = DBUtility.ToNullableString(txtFax.Text);
            h_Consultency.Email = DBUtility.ToNullableString(txtEmail.Text);
            h_Consultency.StartDate = DBUtility.ToDateTime(txtStartDate.Text);
            h_Consultency.Status = H_EmployeeConsultency.Statuses.ACTIVE;
            h_Consultency.CountryId = DBUtility.ToInt32(ddlCountry.SelectedValue);
            h_Consultency.OrganizationId = DBUtility.ToInt32(ddlNgo.SelectedValue);
            return h_Consultency;
        }

        protected override void LoadData()
        {
            this.ddlCountry.DataSource = Country.Find("", "Name");
            this.ddlCountry.DataBind();
            this.ddlCountry.Items.Insert(0, new ListItem("Select Country","0"));
            
            this.ddlNgo.DataSource = Organization.Find("", "Name");
            this.ddlNgo.DataBind();
            this.ddlNgo.Items.Insert(0, new ListItem("Select Fund", "0"));

        }
        protected void lbSearch_Click(object sender, EventArgs e)
        {
            TransactionManager tm = new TransactionManager(false);
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (h_Employee.Status != H_Employee.Statuses.Working)
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    this.ShowUIMessage(msg);
                    return;
                }
                hdnId.Value = h_Employee.Id.ToString();
                this.Type = TYPE_ADD;
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;

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
                txtDistrict.Text = Subzone.GetById(region.SubzoneId).Name; 
                txtBranch.Text = branch.Name;

               // IList<H_EmployeeConsultency> h_EmployeeConsultList = H_EmployeeConsultency.Find("H_EmployeeID = " + h_Employee.Id, "LetterDate Desc");

                this.TransactionManager = new TransactionManager(false);
                string query = "SELECT "
                        + "hec.LetterNo,"
                        + " hec.LetterDate,"
                        + " hec.NgoName,"
                        + " o.Name AS FundName,"
                        + " hec.StartDate,"
                        + " hec.EndDate,"
                        + " c.Name AS Country"
                        + " FROM "
                        + " H_EmployeeConsultency AS hec INNER JOIN Country AS c on hec.CountryId=c.Id"
                        + " INNER JOIN Organization AS o ON hec.OrganizationId=o.Id where hec.H_EmployeeID=" + h_Employee.Id + " ORDER BY hec.LetterDate Desc";


                DataSet ds = TransactionManager.GetDataSet(query);
                DataTable dtleave = ds.Tables[0];
                gvList.DataSource = dtleave;
                gvList.DataBind();


            }
            else
            {
                hdnId.Value = "0";
                txtDistrict.Text = "";
                txtDesignation.Text = "";
                txtBranch.Text = "";
                txtStatus.Text = "";
                txtLetterDate.Text = "";
                txtLetterNo.Text = "";
                
                txtThrough.Text = "";
                txtPhone.Text = "";
                txtFax.Text = "";
                txtEmail.Text = "";
                txtStartDate.Text = "";


            }
        }
    }
}
