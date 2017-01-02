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
    public partial class H_EmployeeLeaveAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEELEAVE ADD"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Menu mnuPageToolbar = (Menu)Master.FindControl("mnuPageToolbar");

            if (mnuPageToolbar.Items.Count > 1 && mnuPageToolbar.Items[1].Value == "SAVE")
            {
                mnuPageToolbar.Items.RemoveAt(1);
            }
        }

        protected override void HandleSpecialCommand(object sender, MenuEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override string GetListPageUrl()
        {
            return "H_EmployeeLeaveList.aspx";
        }

        private H_EmployeeLeaveHistory GetH_EmployeeLeave()
        {
            H_EmployeeLeaveHistory h_EmployeeLeave = null;
            if (this.Type == TYPE_EDIT)
            {
                h_EmployeeLeave = H_EmployeeLeaveHistory.GetById(Convert.ToInt32(hdnId.Value)); //if update hdnId is H_EmployeeLeaveHistoryId
            }
            else
            {
                h_EmployeeLeave = new H_EmployeeLeaveHistory();
                h_EmployeeLeave.H_EmployeeId = DBUtility.ToInt32(hdnId.Value); // if Insert hdnId is H_EmployeeId
            }
            
            h_EmployeeLeave.Type = (H_EmployeeLeaveHistory.Types)DBUtility.ToInt32(ddlType.SelectedValue);
            h_EmployeeLeave.JoinType = H_EmployeeLeaveHistory.JoinTypes.Rejoin;// (H_EmployeeLeave.JoinTypes)DBUtility.ToInt32(ddlJoinType.SelectedValue);
            h_EmployeeLeave.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_EmployeeLeave.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_EmployeeLeave.StartDate = DBUtility.ToDateTime(txtStartDate.Text);
            h_EmployeeLeave.EndDate = DBUtility.ToNullableDateTime(txtEndDate.Text);
            h_EmployeeLeave.Cause = DBUtility.ToString(txtCause.Text);
            h_EmployeeLeave.Status = 1;

            return h_EmployeeLeave;
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
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            //H_Employee h_Employee = H_Employee.GetById(UIUtility.GetEmployeeID(this.txtEmployee.Text + UIUtility.GetAccessLevel(User.Identity.Name)));

            if (h_Employee == null)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "No employee found";
                return msg;
            }

            if (h_Employee.JoiningDate >= DBUtility.ToDateTime(this.txtLetterDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Letter date should be greater than employee's joining date (" + h_Employee.JoiningDate + ")";
                return msg;
            }

            if (h_Employee.JoiningDate >= DBUtility.ToDateTime(this.txtStartDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Start date should be greater than employee's joining date (" + h_Employee.JoiningDate + ")";
                return msg;
            }

            if (DBUtility.ToDateTime(this.txtStartDate.Text.Trim()) > DBUtility.ToDateTime(this.txtEndDate.Text.Trim()))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "End date should be greater than or equal to start date";
                return msg;
            }

            if (h_Employee.Status != H_Employee.Statuses.Working && h_Employee.Status != H_Employee.Statuses.Consultancy)
            {
                if (h_Employee.Status == H_Employee.Statuses.In_Leave )
                {
                    if (chkLeaveExtension.Checked == false)
                    {
                        msg.Type = MessageType.Error;
                        msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                        return msg;
                    }
                }
                else
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Employee presently " + ((H_Employee.Statuses)(h_Employee.Status)).ToString().Replace("_", " ").ToLower();
                    return msg;
                }
            }

            IList<H_EmployeeLeave> h_EmployeeLeaveList = H_EmployeeLeave.Find("H_EmployeeID = " + h_Employee.Id + " AND LetterDate= '" + DBUtility.ToDateTime(this.txtLetterDate.Text).ToString(Configuration.DatabaseDateFormat)+"'","");// + "' AND '" + DBUtility.ToDateTime(this.txtEndDate.Text).ToString(Configuration.DatabaseDateFormat) + "'", "Id DESC");

            if (h_EmployeeLeaveList.Count > 0)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Invalid operation. Employee already on leave from " + h_EmployeeLeaveList[0].StartDate.ToShortDateString() + " to " + h_EmployeeLeaveList[0].EndDate.ToString();
                return msg;
            }
            IList<H_EmployeeLeaveHistory> h_EmployeeLeavePendingList = H_EmployeeLeaveHistory.Find("H_EmployeeID = " + h_Employee.Id + " AND Status=1", "Id DESC");

            if (h_EmployeeLeavePendingList.Count > 0)
            {
                if (chkCancel.Checked == false)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Invalid operation. Leave already Posted for " + h_Employee.Name + "(" + h_Employee.Code.ToString() + "). Please Check List";
                    return msg;
                }
            }
            if (ddlType.SelectedValue == ((Int32)H_EmployeeLeave.Types.Maternity_Leave).ToString() && h_Employee.Sex == H_Employee.Sexes.Male)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Maternity Leave valid for female only";
                return msg;
            }
            return msg;
        }

        protected override Message Save()
        {
            Message msg = this.Validate();

            if (msg.Type == MessageType.Information)
            {
                H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
                if (h_Employee != null)
                {

                    H_EmployeeLeaveHistory h_EmployeeLeave = this.GetH_EmployeeLeave();                  

                    string desc = "Insert [H_EmployeeLeaveHistory]";
                    this.TransactionManager = new TransactionManager(true, desc);

                    H_EmployeeLeave pastLeave = H_EmployeeLeave.Get("H_EmployeeId="+h_Employee.Id+" AND Status=1");
                    if (pastLeave != null && chkCancel.Checked==false)
                    {
                        pastLeave.Status = 2;
                        pastLeave.EndDate = h_EmployeeLeave.StartDate.AddDays(-1);
                        H_EmployeeLeave.Update(this.TransactionManager, pastLeave);

                    }
                    if (this.Type == TYPE_EDIT)
                    {
                        if (chkCancel.Checked == true)
                        {
                            h_EmployeeLeave.Status = 2;
                        }
                        H_EmployeeLeaveHistory.Update(this.TransactionManager, h_EmployeeLeave);
                    }
                    else
                    {
                        H_EmployeeLeaveHistory.Insert(this.TransactionManager, h_EmployeeLeave);
                    }

                    //if (h_EmployeeLeave.JoinType == H_EmployeeLeaveHistory.JoinTypes.Rejoin)
                    //{
                    //    h_Employee.Status = H_Employee.Statuses.In_Leave;
                    //   // h_Employee.EmploymentType = H_Employee.EmploymentTypes.None;

                    //    desc = "Update [H_Employee]";
                    //    Asa.Hrms.Data.Entity.H_Employee.Update(this.TransactionManager, h_Employee);
                    //}                    

                    hdnId.Value = h_EmployeeLeave.Id.ToString();
                    this.Type = TYPE_EDIT;

                    this.TransactionManager.Commit();
                    this.TransactionManager = new TransactionManager(false);
                    //IList<H_EmployeeLeave> h_EmployeeLeaveList = H_EmployeeLeave.Find("H_EmployeeID = " + h_Employee.Id, "StartDate Desc");// + " AND EndDate BETWEEN '" + DBUtility.ToDateTime(this.txtStartDate.Text).ToString(Configuration.DatabaseDateFormat) + "' AND '" + DBUtility.ToDateTime(this.txtEndDate.Text).ToString(Configuration.DatabaseDateFormat) + "'", "Id DESC");
                    string query = "SELECT hel.LetterNo,hel.LetterDate,"
                    + "ISNULL((SELECT TOP(1) b.NAME FROM branch b INNER JOIN H_EmployeeBranch AS heb ON b.Id=heb.BranchId AND heb.H_EmployeeId=he.Id AND (DATEADD(dd, 0, DATEDIFF(dd, 0, hel.StartDate)) Between StartDate AND EndDate)),'None-0000000') AS 'LeaveBranch',"
                    + "hel.StartDate,"
                    + "hel.EndDate,"
                    + "case when hel.[Type]=11 THEN 'Leave With Pay' WHEN  hel.[Type]=12 THEN 'Leave Without Pay' WHEN hel.[Type]=13 THEN 'Medical Leave' WHEN hel.[Type]=14 THEN 'Maternity_Leave' WHEN hel.[Type]=15 THEN 'Suspension' WHEN  hel.[Type]=16 THEN 'Force Leave' WHEN  hel.[Type]=17 THEN 'Lien'  end AS 'Type'"
                    + " FROM "
                    + "H_EmployeeLeave AS hel "
                    + "INNER JOIN H_Employee AS he ON hel.H_EmployeeId=he.Id where he.Id=" + h_Employee.Id + " order by StartDate Desc";
                    DataSet ds = TransactionManager.GetDataSet(query);
                    DataTable dtleave = ds.Tables[0];
                    gvList.DataSource = dtleave;
                    gvList.DataBind();
                }
                else
                {
                    msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "No employee found";

                    hdnId.Value = "0";
                }
            }

            return msg;
        }

        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlType, typeof(H_EmployeeLeave.Types), false, false, true);
           // UIUtility.LoadEnums(ddlJoinType, typeof(H_EmployeeLeave.JoinTypes), false, false, true);
            ddlType.Items.Insert(0, new ListItem("Select Option", "0"));
            ddlType.SelectedIndex = 0;
            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                H_EmployeeLeaveHistory h_History = H_EmployeeLeaveHistory.GetById(Convert.ToInt32(hdnId.Value));

                if (h_History != null)
                {
                    this.Type = TYPE_EDIT;
                    H_Employee h_Employee = H_Employee.GetById(h_History.H_EmployeeId);
                    txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                    txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
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
                    txtDistrict.Text = Subzone.GetById(region.SubzoneId).Name;
                    txtBranch.Text = branch.Name;

                    ddlType.SelectedValue =((Int32)h_History.Type).ToString();
                    txtLetterDate.Text = UIUtility.Format(h_History.LetterDate);
                    txtLetterNo.Text = h_History.LetterNo;
                    txtStartDate.Text = UIUtility.Format(h_History.StartDate);
                    txtEndDate.Text = UIUtility.Format(h_History.EndDate);
                    txtCause.Text = h_History.Cause;
                    this.TransactionManager = new TransactionManager(false);
                    //IList<H_EmployeeLeave> h_EmployeeLeaveList = H_EmployeeLeave.Find("H_EmployeeID = " + h_Employee.Id, "StartDate Desc");// + " AND EndDate BETWEEN '" + DBUtility.ToDateTime(this.txtStartDate.Text).ToString(Configuration.DatabaseDateFormat) + "' AND '" + DBUtility.ToDateTime(this.txtEndDate.Text).ToString(Configuration.DatabaseDateFormat) + "'", "Id DESC");
                    string query = "SELECT hel.LetterNo,hel.LetterDate,"
                    +"ISNULL((SELECT TOP(1) b.NAME FROM branch b INNER JOIN H_EmployeeBranch AS heb ON b.Id=heb.BranchId AND heb.H_EmployeeId=he.Id AND (DATEADD(dd, 0, DATEDIFF(dd, 0, hel.StartDate)) Between StartDate AND EndDate)),'None-0000000') AS 'LeaveBranch',"
                    + "hel.StartDate,"
                    + "hel.EndDate,"
                    + "case when hel.[Type]=11 THEN 'Leave With Pay' WHEN  hel.[Type]=12 THEN 'Leave Without Pay' WHEN hel.[Type]=13 THEN 'Medical Leave' WHEN hel.[Type]=14 THEN 'Maternity_Leave' WHEN hel.[Type]=15 THEN 'Suspension' WHEN  hel.[Type]=16 THEN 'Force Leave' WHEN  hel.[Type]=17 THEN 'Lien'  end AS 'Type'"
                    + " FROM "
                    + "H_EmployeeLeave AS hel "
                    + "INNER JOIN H_Employee AS he ON hel.H_EmployeeId=he.Id where he.Id=" + h_Employee.Id + " order by StartDate Desc";
                    DataSet ds = TransactionManager.GetDataSet(query);
                    DataTable dtleave = ds.Tables[0];
                    gvList.DataSource = dtleave;
                    gvList.DataBind();
                }
            }
        }

        protected void lbSearch_Click(object sender, EventArgs e)
        {
            TransactionManager tm = new TransactionManager(false);
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
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
                //txtZone.Text = Zone.GetById(Subzone.GetById(region.SubzoneId).ZoneId).Name;
                //txtZoneDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[z]["StartDate"]));
                txtDistrict.Text = Subzone.GetById(region.SubzoneId).Name;
                //txtSubzoneDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[s]["StartDate"]));
                //txtRegion.Text = region.Name;
                //txtRegionDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[r]["StartDate"]));
                txtBranch.Text = branch.Name;
                // txtBranchDate.Text = UIUtility.Format(DBUtility.ToDateTime(dt.Rows[b]["StartDate"]));
                // hdnBranch.Value = branch.Id.ToString();
                hdnId.Value = h_Employee.Id.ToString();
              
                this.TransactionManager = new TransactionManager(false);
                //IList<H_EmployeeLeave> h_EmployeeLeaveList = H_EmployeeLeave.Find("H_EmployeeID = " + h_Employee.Id, "StartDate Desc");// + " AND EndDate BETWEEN '" + DBUtility.ToDateTime(this.txtStartDate.Text).ToString(Configuration.DatabaseDateFormat) + "' AND '" + DBUtility.ToDateTime(this.txtEndDate.Text).ToString(Configuration.DatabaseDateFormat) + "'", "Id DESC");
                string query = "SELECT hel.LetterNo,hel.LetterDate,"
                + "ISNULL((SELECT TOP(1) b.NAME FROM branch b INNER JOIN H_EmployeeBranch AS heb ON b.Id=heb.BranchId AND heb.H_EmployeeId=he.Id AND (DATEADD(dd, 0, DATEDIFF(dd, 0, hel.StartDate)) Between StartDate AND EndDate)),'None-0000000') AS 'LeaveBranch',"
                + "hel.StartDate,"
                + "hel.EndDate,"
                + "case when hel.[Type]=11 THEN 'Leave With Pay' WHEN  hel.[Type]=12 THEN 'Leave Without Pay' WHEN hel.[Type]=13 THEN 'Medical Leave' WHEN hel.[Type]=14 THEN 'Maternity_Leave' WHEN hel.[Type]=15 THEN 'Suspension' WHEN  hel.[Type]=16 THEN 'Force Leave' WHEN  hel.[Type]=17 THEN 'Lien'  end AS 'Type'"
                + " FROM "
                + "H_EmployeeLeave AS hel "
                + "INNER JOIN H_Employee AS he ON hel.H_EmployeeId=he.Id where he.Id=" + h_Employee.Id + " order by StartDate Desc";
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

            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == ((Int32)H_EmployeeLeave.Types.Suspension).ToString() || ddlType.SelectedValue == ((Int32)H_EmployeeLeave.Types.Lien).ToString())
            {
                rvEndDate.Enabled = false;
            }
            else
            {
                rvEndDate.Enabled = true;
            }
        }
    }
}
