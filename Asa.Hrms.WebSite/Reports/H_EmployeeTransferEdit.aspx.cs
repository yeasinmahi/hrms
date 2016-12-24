using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Reports
{
    public partial class H_EmployeeTransferEdit : AddPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlBranch.Visible = false;
                pnlTransfer.Visible = false;
                pnlLeave.Visible = false;
                pnlWarning.Visible = false;
                pnlPenalty.Visible = false;
                pnlDesignation.Visible = false;
                pnlPromotion.Visible = false;
                pnlConsultancy.Visible = false;
                pnlGrade.Visible = false;
                pnlDropout.Visible = false;
                pnlRejoin.Visible = false;
                pnlPromoEdit.Visible = false;
                pnlTransferEdit.Visible = false;
                Session.Remove("PromotionEdit");
                User user = Library.Data.Entity.User.GetByLogin(User.Identity.Name);
                if (user.UserType == Library.Data.Entity.User.UserTypes.Field_User)
                {
                    btnConsultancy.Enabled = false;
                    btnDropOut.Enabled = false;
                    btnPromotion.Enabled = false;
                    btnTransfer.Enabled = false;

                }
            }

        }

        private void LoadGrid(Int32 h_EmployeeId)
        {
            //H_Employee emp = H_Employee.GetByCode("14794");
            IList<H_EmployeeTransfer> list = H_EmployeeTransfer.Find("H_EmployeeId=" + h_EmployeeId, " LetterDate");
            IList<Branch> branch = Branch.FindAll();
            IList<Region> region = Region.FindAll();
            IList<Subzone> dist = Subzone.FindAll();
            var gList = (from t in list
                         join sb in branch on t.SourceBranchId equals sb.Id
                         join sr in region on sb.RegionId equals sr.Id
                         join sd in dist on sr.SubzoneId equals sd.Id
                         join db in branch on t.DestinationBranchId equals db.Id
                         join dr in region on db.RegionId equals dr.Id
                         join dd in dist on dr.SubzoneId equals dd.Id
                         select new
                         {
                             t.Id,
                             t.LetterNo,
                             t.LetterDate,
                             t.SourceBranchId,
                             t.DestinationBranchId,
                             t.JoiningDate,
                             SourceDistrictId = sd.Id,
                             sDistrictName = sd.Name,
                             sBranchName = sb.Name,
                             dDistrictName = dd.Name,
                             dBranchName = db.Name,
                             DestinationDistrictId = dd.Id
                         }).OrderBy(o => o.LetterDate);
            DataTable dt = UIUtility.LINQToDataTable(gList);
            gvTransfer.DataSource = dt;
            gvTransfer.DataBind();



        }
        private void LoadEmployeeBranch(Int32 h_EmployeeId)
        {
            //H_Employee emp = H_Employee.GetByCode("14794");
            IList<H_EmployeeBranch> employeeBranchList = H_EmployeeBranch.Find("H_EmployeeId=" + h_EmployeeId, " StartDate");
            IList<Branch> branch = Branch.FindAll();
            IList<Region> region = Region.FindAll();
            IList<Subzone> dist = Subzone.FindAll();
            var gList = (from t in employeeBranchList
                         join b in branch on t.BranchId equals b.Id
                         join r in region on b.RegionId equals r.Id
                         join d in dist on r.SubzoneId equals d.Id
                         select new
                         {
                             t.Id,
                             EmployeeId = t.H_EmployeeId,
                             t.StartDate,
                             t.EndDate,
                             BranchId = t.BranchId,
                             BranchName = b.Name,
                             DistrictId = d.Id,
                             DistrictName = d.Name
                         }).OrderBy(o => o.EndDate);
            DataTable dt = UIUtility.LINQToDataTable(gList);
            gvBranch.DataSource = dt;
            gvBranch.DataBind();

        }

        protected void gvTransfer_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string FromDistrictId = gvTransfer.DataKeys[e.NewEditIndex]["SourceDistrictId"].ToString();
            string FromBranchId = gvTransfer.DataKeys[e.NewEditIndex]["SourceBranchId"].ToString();
            string ToDistrictId = gvTransfer.DataKeys[e.NewEditIndex]["DestinationDistrictId"].ToString();
            string ToBranchId = gvTransfer.DataKeys[e.NewEditIndex]["DestinationBranchId"].ToString();
            gvTransfer.EditIndex = e.NewEditIndex;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadGrid(Convert.ToInt32(hdnid.Value));
            DropDownList ddlFDist = (DropDownList)gvTransfer.Rows[e.NewEditIndex].FindControl("ddlFromDistrict");
            DropDownList ddlTDist = (DropDownList)gvTransfer.Rows[e.NewEditIndex].FindControl("ddlToDistrict");
            DropDownList ddlFromBranch = (DropDownList)gvTransfer.Rows[e.NewEditIndex].FindControl("ddlFromBranch");
            DropDownList ddlToBranch = (DropDownList)gvTransfer.Rows[e.NewEditIndex].FindControl("ddlToBranch");
            if (ddlFDist != null)
            {
                IList<Subzone> disList = Subzone.Find(" Status=1 ", "Name");
                ddlFDist.DataSource = disList;
                ddlFDist.DataTextField = "Name";
                ddlFDist.DataValueField = "Id";
                ddlFDist.DataBind();
                ddlFDist.SelectedValue = FromDistrictId;

                ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(FromDistrictId));
                ddlFromBranch.DataTextField = "Name";
                ddlFromBranch.DataValueField = "Id";
                ddlFromBranch.DataBind();
                ddlFromBranch.SelectedValue = FromBranchId;

                ddlTDist.DataSource = disList;
                ddlTDist.DataTextField = "Name";
                ddlTDist.DataValueField = "Id";
                ddlTDist.DataBind();
                ddlTDist.SelectedValue = ToDistrictId;

                ddlToBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ToDistrictId));
                ddlToBranch.DataTextField = "Name";
                ddlToBranch.DataValueField = "Id";
                ddlToBranch.DataBind();
                ddlToBranch.SelectedValue = ToBranchId;
            }
            gvTransfer.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvTransfer_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTransfer.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadGrid(Convert.ToInt32(hdnid.Value));
        }

        protected void gvTransfer_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string h_TransferId = gvTransfer.DataKeys[e.RowIndex]["Id"].ToString();
            TransactionManager tm = new TransactionManager(true);
            H_EmployeeTransfer h_trans = H_EmployeeTransfer.GetById(Convert.ToInt32(h_TransferId));
            h_trans.LetterNo = ((TextBox)gvTransfer.Rows[e.RowIndex].FindControl("txtLetterNo")).Text;
            h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvTransfer.Rows[e.RowIndex].FindControl("txtLetterDate")).Text);
            h_trans.JoiningDate = DBUtility.ToDateTime(((TextBox)gvTransfer.Rows[e.RowIndex].FindControl("txtJoiningDate")).Text);
            h_trans.SourceBranchId = Convert.ToInt32(((DropDownList)gvTransfer.Rows[e.RowIndex].FindControl("ddlFromBranch")).SelectedValue);
            h_trans.DestinationBranchId = Convert.ToInt32(((DropDownList)gvTransfer.Rows[e.RowIndex].FindControl("ddlToBranch")).SelectedValue);
            h_trans.UserLogin = User.Identity.Name;
            H_EmployeeTransfer.Update(tm, h_trans);
            tm.Commit();
            gvTransfer.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadGrid(Convert.ToInt32(hdnid.Value));
        }
        private IList<Branch> GetBranchBySubzoneId(int subzoneId)
        {
            return Branch.Find("RegionId IN (Select Id from Region Where SubzoneId=" + subzoneId + ")", "Name");
        }

        protected void ddlFromDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FromDistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvTransfer.EditIndex;
            DropDownList ddlFromBranch = (DropDownList)gvTransfer.Rows[rowindex].FindControl("ddlFromBranch");
            if (ddlFromBranch != null)
            {
                ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(FromDistrictId));
                ddlFromBranch.DataTextField = "Name";
                ddlFromBranch.DataValueField = "Id";
                ddlFromBranch.DataBind();
            }
        }

        protected void ddlToDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ToDistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvTransfer.EditIndex;
            DropDownList ddlToBranch = (DropDownList)gvTransfer.Rows[rowindex].FindControl("ddlToBranch");
            if (ddlToBranch != null)
            {
                ddlToBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ToDistrictId));
                ddlToBranch.DataTextField = "Name";
                ddlToBranch.DataValueField = "Id";
                ddlToBranch.DataBind();
            }
        }


        protected void lbSearch_Click(object sender, EventArgs e)
        {
            TransactionManager tm = new TransactionManager(false);
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
               // LoadGrid(h_Employee.Id);
               // LoadEmployeeBranch(h_Employee.Id);
            }
        }

        protected void gvTransfer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 h_TransferId = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeeTransfer.Delete(tm, h_TransferId);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadGrid(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvBranch.EditIndex;
            DropDownList ddlBranch = (DropDownList)gvBranch.Rows[rowindex].FindControl("ddlBranch");
            if (ddlBranch != null)
            {
                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(DistrictId));
                ddlBranch.DataTextField = "Name";
                ddlBranch.DataValueField = "Id";
                ddlBranch.DataBind();
            }
        }

        protected void gvBranch_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string EmployeeId = gvBranch.DataKeys[e.NewEditIndex]["EmployeeId"].ToString();
            string DistrictId = gvBranch.DataKeys[e.NewEditIndex]["DistrictId"].ToString();
            string BranchId = gvBranch.DataKeys[e.NewEditIndex]["BranchId"].ToString();

            gvBranch.EditIndex = e.NewEditIndex;
            LoadEmployeeBranch(Convert.ToInt32(EmployeeId));
            DropDownList ddlDist = (DropDownList)gvBranch.Rows[e.NewEditIndex].FindControl("ddlDistrict");
            DropDownList ddlBranch = (DropDownList)gvBranch.Rows[e.NewEditIndex].FindControl("ddlBranch");
            if (ddlDist != null)
            {
                IList<Subzone> disList = Subzone.Find(" Status=1 ", "Name");
                ddlDist.DataSource = disList;
                ddlDist.DataTextField = "Name";
                ddlDist.DataValueField = "Id";
                ddlDist.DataBind();
                ddlDist.SelectedValue = DistrictId;

                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(DistrictId));
                ddlBranch.DataTextField = "Name";
                ddlBranch.DataValueField = "Id";
                ddlBranch.DataBind();
                ddlBranch.SelectedValue = BranchId;
            }
            gvBranch.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvBranch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvBranch.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadEmployeeBranch(Convert.ToInt32(hdnid.Value));
        }

        protected void gvBranch_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string h_EmployeeBranchId = gvBranch.DataKeys[e.RowIndex]["Id"].ToString();
            TransactionManager tm = new TransactionManager(true);
            H_EmployeeBranch h_trans = H_EmployeeBranch.GetById(Convert.ToInt32(h_EmployeeBranchId));

            h_trans.StartDate = DBUtility.ToDateTime(((TextBox)gvBranch.Rows[e.RowIndex].FindControl("txtStartDate")).Text);
            h_trans.EndDate = DBUtility.ToDateTime(((TextBox)gvBranch.Rows[e.RowIndex].FindControl("txtEndDate")).Text);
            h_trans.BranchId = Convert.ToInt32(((DropDownList)gvBranch.Rows[e.RowIndex].FindControl("ddlBranch")).SelectedValue);

            H_EmployeeBranch.Update(tm, h_trans);
            tm.Commit();
            gvBranch.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadEmployeeBranch(Convert.ToInt32(hdnid.Value));
        }

        protected void gvBranch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 h_EmployeeBranchId = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeeBranch.Delete(tm, h_EmployeeBranchId);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadEmployeeBranch(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void btnLeave_Click(object sender, EventArgs e)
        {
            pnlIncHeldup.Visible = false;
            pnlGrade.Visible = false;
            pnlWarning.Visible = false;
            pnlPenalty.Visible = false;
            pnlBranch.Visible = false;
            pnlTransfer.Visible = false;
            
            pnlDesignation.Visible = false;
            pnlPromotion.Visible = false;
            pnlConsultancy.Visible = false;
            pnlDropout.Visible = false;
            pnlRejoin.Visible = false;
            pnlPromoEdit.Visible = false;
            pnlTransferEdit.Visible = false;
            pnlLeave.Visible = true;
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (!HasEditPermission(h_Employee.Id))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Information;
                    msg.Msg = "You have no Update Permission for this Employee";
                    ShowUIMessage(msg);
                    return;
                }
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                LoadEmployeeLeave(h_Employee.Id);
                
            }
            //onchange="javascript:__doPostBack('ctl00$ContentPlaceHolder1$lbSearch', '')"
        }
        private void LoadEmployeeLeave(Int32 h_EmployeeId)
        {
            IList<H_EmployeeLeave> leave = H_EmployeeLeave.Find("H_EmployeeId=" + h_EmployeeId, "StartDate");

            if (leave != null && leave.Count > 0)
            {
                gvLeave.DataSource = leave;
                gvLeave.DataBind();
                DropDownList ddlTypeAdd = (DropDownList)gvLeave.FooterRow.FindControl("ddlTypeAdd");
                if (ddlTypeAdd != null)
                {
                    UIUtility.LoadEnums(ddlTypeAdd, typeof(H_EmployeeLeave.Types), false, false, false);

                }
            }
            else
            {
                H_EmployeeLeave eLeave = new H_EmployeeLeave();
                leave.Add(eLeave);
                gvLeave.DataSource = leave;
                gvLeave.DataBind();
                DropDownList ddlTypeAdd = (DropDownList)gvLeave.FooterRow.FindControl("ddlTypeAdd");
                if (ddlTypeAdd != null)
                {
                    UIUtility.LoadEnums(ddlTypeAdd, typeof(H_EmployeeLeave.Types), false, false, false);

                }
                gvLeave.Rows[0].Visible = false;
            }
        }

        protected void gvLeave_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string EmployeeId = gvLeave.DataKeys[e.NewEditIndex]["H_EmployeeId"].ToString();
            string LeaveType = gvLeave.DataKeys[e.NewEditIndex]["Type"].ToString();
            gvLeave.EditIndex = e.NewEditIndex;
            LoadEmployeeLeave(Convert.ToInt32(EmployeeId));
            DropDownList ddlType = (DropDownList)gvLeave.Rows[e.NewEditIndex].FindControl("ddlType");
            if (ddlType != null)
            {
                UIUtility.LoadEnums(ddlType, typeof(H_EmployeeLeave.Types), false, false, false);
                ddlType.SelectedValue = ddlType.Items.FindByText(LeaveType).Value;// ((Int32)Enum.Parse(typeof(H_EmployeeLeave.Types), LeaveType)).ToString();

            }
            gvLeave.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvLeave_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            string EmployeeId = gvLeave.DataKeys[e.RowIndex]["H_EmployeeId"].ToString();
            gvLeave.EditIndex = -1;
            LoadEmployeeLeave(Convert.ToInt32(EmployeeId));
        }

        protected void gvLeave_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string h_EmployeeId = gvLeave.DataKeys[e.RowIndex]["H_EmployeeId"].ToString();
            string h_EmployeeLeaveId = gvLeave.DataKeys[e.RowIndex]["Id"].ToString();
            TransactionManager tm = new TransactionManager(true);
            H_EmployeeLeave h_trans = H_EmployeeLeave.GetById(Convert.ToInt32(h_EmployeeLeaveId));
            h_trans.Type = (H_EmployeeLeave.Types)DBUtility.ToInt32(((DropDownList)gvLeave.Rows[e.RowIndex].FindControl("ddlType")).SelectedValue);
            h_trans.LetterNo = ((TextBox)gvLeave.Rows[e.RowIndex].FindControl("txtLetterNo")).Text;
            h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvLeave.Rows[e.RowIndex].FindControl("txtLetterDate")).Text);
            h_trans.StartDate = DBUtility.ToDateTime(((TextBox)gvLeave.Rows[e.RowIndex].FindControl("txtStartDate")).Text);
            h_trans.EndDate = DBUtility.ToNullableDateTime(((TextBox)gvLeave.Rows[e.RowIndex].FindControl("txtEndDate")).Text);

            H_EmployeeLeave.Update(tm, h_trans);
            tm.Commit();
            gvLeave.EditIndex = -1;
            LoadEmployeeLeave(Convert.ToInt32(h_EmployeeId));
        }

        protected void gvLeave_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 h_EmployeeLeaveId = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeeLeave.Delete(tm, h_EmployeeLeaveId);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadEmployeeLeave(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                DateTime temp;
                if (string.IsNullOrEmpty(((TextBox)gvLeave.FooterRow.FindControl("txtLetterNoAdd")).Text) )
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter Letter No";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvLeave.FooterRow.FindControl("txtLetterDateAdd")).Text) || !DateTime.TryParse(((TextBox)gvLeave.FooterRow.FindControl("txtLetterDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Letter Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvLeave.FooterRow.FindControl("txtStartDateAdd")).Text) || !DateTime.TryParse(((TextBox)gvLeave.FooterRow.FindControl("txtStartDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Start Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvLeave.FooterRow.FindControl("txtEndDateAdd")).Text) || !DateTime.TryParse(((TextBox)gvLeave.FooterRow.FindControl("txtEndDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid End Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                TransactionManager tm = new TransactionManager(true);

                H_EmployeeLeave h_trans = new H_EmployeeLeave();
                h_trans.H_EmployeeId = Convert.ToInt32(hdnid.Value);
                h_trans.Type = (H_EmployeeLeave.Types)DBUtility.ToInt32(((DropDownList)gvLeave.FooterRow.FindControl("ddlTypeAdd")).SelectedValue);
                h_trans.LetterNo = ((TextBox)gvLeave.FooterRow.FindControl("txtLetterNoAdd")).Text;
                h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvLeave.FooterRow.FindControl("txtLetterDateAdd")).Text);
                h_trans.StartDate = DBUtility.ToDateTime(((TextBox)gvLeave.FooterRow.FindControl("txtStartDateAdd")).Text);
                h_trans.EndDate = DBUtility.ToNullableDateTime(((TextBox)gvLeave.FooterRow.FindControl("txtEndDateAdd")).Text);

                H_EmployeeLeave.Insert(tm, h_trans);
                tm.Commit();

                LoadEmployeeLeave(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            pnlIncHeldup.Visible = false;
            pnlGrade.Visible = false;
            pnlWarning.Visible = false;
            pnlPenalty.Visible = false;
            pnlBranch.Visible = true;
            pnlTransfer.Visible = true;
            pnlLeave.Visible = false;
            pnlDesignation.Visible = false;
            pnlPromotion.Visible = false;
            pnlConsultancy.Visible = false;
            pnlDropout.Visible = false;
            pnlRejoin.Visible = false;
            pnlPromoEdit.Visible = false;
            pnlTransferEdit.Visible = false;
            //TransactionManager tm = new TransactionManager(false);
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                LoadGrid(h_Employee.Id);
                LoadEmployeeBranch(h_Employee.Id);
            }
        }

        protected void btnPenalty_Click(object sender, EventArgs e)
        {
            pnlIncHeldup.Visible = false;
            pnlGrade.Visible = false;
            pnlWarning.Visible = false;
            pnlBranch.Visible = false;
            pnlTransfer.Visible = false;
            pnlLeave.Visible = false;
            pnlDesignation.Visible = false;
            pnlPromotion.Visible = false;
            pnlConsultancy.Visible = false;
            pnlDropout.Visible = false;
            pnlRejoin.Visible = false;
            pnlPromoEdit.Visible = false;
            pnlTransferEdit.Visible = false;
            pnlPenalty.Visible = true;
            //TransactionManager tm = new TransactionManager(false);
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (!HasEditPermission(h_Employee.Id))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Information;
                    msg.Msg = "You have no Update Permission for this Employee";
                    ShowUIMessage(msg);
                    return;
                }
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                LoadPenalty(h_Employee.Id);

            }
        }

        private void LoadPenalty(int h_EmployeeId)
        {
            IList<H_EmployeePenalty> penalty = H_EmployeePenalty.Find("H_EmployeeId=" + h_EmployeeId, " LetterDate");
            IList<Branch> branch = Branch.FindAll();
            IList<Region> region = Region.FindAll();
            IList<Subzone> dist = Subzone.FindAll();
            var gList = (from p in penalty
                         join b in branch on p.BranchId equals b.Id
                         join r in region on b.RegionId equals r.Id
                         join d in dist on r.SubzoneId equals d.Id
                         select new
                         {
                             p.Id,
                             H_EmployeeId = p.H_EmployeeId,
                             p.LetterNo,
                             p.LetterDate,
                             p.FineType,
                             p.FineAmount,
                             p.RemissionLetterNo,
                             p.RemissionLetterDate,
                             p.RemissionAmount,
                             BranchId = p.BranchId,
                             BranchName = b.Name,
                             DistrictId = d.Id,
                             DistrictName = d.Name
                         }).OrderBy(p => p.LetterDate);
            DataTable dt = UIUtility.LINQToDataTable(gList);
            gvPenalty.DataSource = dt;
            gvPenalty.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                DropDownList ddlFromDistrict = (DropDownList)gvPenalty.FooterRow.FindControl("ddlPenaltyDistrictAdd");
                DropDownList ddlFromBranch = (DropDownList)gvPenalty.FooterRow.FindControl("ddlPenaltyBranchAdd");

                DropDownList ddlFineTypeAdd = (DropDownList)gvPenalty.FooterRow.FindControl("ddlFineTypeAdd");
                if (ddlFromDistrict != null)
                {
                    IList<Subzone> disList = Subzone.FindAll("Name");
                    ddlFromDistrict.DataSource = disList;
                    ddlFromDistrict.DataTextField = "Name";
                    ddlFromDistrict.DataValueField = "Id";
                    ddlFromDistrict.DataBind();

                    ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlFromDistrict.SelectedValue));
                    ddlFromBranch.DataTextField = "Name";
                    ddlFromBranch.DataValueField = "Id";
                    ddlFromBranch.DataBind();
                }
                if (ddlFineTypeAdd != null)
                {
                    ddlFineTypeAdd.Items.Insert(0, new ListItem("Fine", "F"));
                    ddlFineTypeAdd.Items.Insert(1, new ListItem("Penalty", "P"));
                }
            }
            
        }

        protected void gvPenalty_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string h_EmployeeId = gvPenalty.DataKeys[e.NewEditIndex]["H_EmployeeId"].ToString();
            string DistrictId = gvPenalty.DataKeys[e.NewEditIndex]["DistrictId"].ToString();
            string BranchId = gvPenalty.DataKeys[e.NewEditIndex]["BranchId"].ToString();
            string FineType = gvPenalty.DataKeys[e.NewEditIndex]["FineType"].ToString();

            gvPenalty.EditIndex = e.NewEditIndex;
            LoadPenalty(Convert.ToInt32(h_EmployeeId));
            DropDownList ddlDist = (DropDownList)gvPenalty.Rows[e.NewEditIndex].FindControl("ddlDistrictPenalty");
            DropDownList ddlBranch = (DropDownList)gvPenalty.Rows[e.NewEditIndex].FindControl("ddlBranchPenalty");
            DropDownList ddlFineType = (DropDownList)gvPenalty.Rows[e.NewEditIndex].FindControl("ddlFineType");
            if (ddlDist != null)
            {
                IList<Subzone> disList = Subzone.FindAll("Name");
                ddlDist.DataSource = disList;
                ddlDist.DataTextField = "Name";
                ddlDist.DataValueField = "Id";
                ddlDist.DataBind();
                ddlDist.SelectedValue = DistrictId;

                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(DistrictId));
                ddlBranch.DataTextField = "Name";
                ddlBranch.DataValueField = "Id";
                ddlBranch.DataBind();
                ddlBranch.SelectedValue = BranchId;
            }
            if (ddlFineType != null)
            {
                ddlFineType.Items.Insert(0, new ListItem("Fine", "F"));
                ddlFineType.Items.Insert(1, new ListItem("Penalty", "P"));
                ddlFineType.SelectedValue = FineType;
            }
            gvPenalty.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvPenalty_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            string EmployeeId = gvPenalty.DataKeys[e.RowIndex]["H_EmployeeId"].ToString();
            gvPenalty.EditIndex = -1;
            LoadPenalty(Convert.ToInt32(EmployeeId));
        }

        protected void gvPenalty_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string h_EmployeePenaltyId = gvPenalty.DataKeys[e.RowIndex]["Id"].ToString();
            TransactionManager tm = new TransactionManager(true);
            H_EmployeePenalty h_trans = H_EmployeePenalty.GetById(Convert.ToInt32(h_EmployeePenaltyId));
            h_trans.FineType = ((DropDownList)gvPenalty.Rows[e.RowIndex].FindControl("ddlFineType")).SelectedValue;
            h_trans.FineAmount = DBUtility.ToDouble(((TextBox)gvPenalty.Rows[e.RowIndex].FindControl("txtAmount")).Text);
            h_trans.LetterNo = ((TextBox)gvPenalty.Rows[e.RowIndex].FindControl("txtLetterNo")).Text;
            h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvPenalty.Rows[e.RowIndex].FindControl("txtLetterDate")).Text);
            h_trans.BranchId = Convert.ToInt32(((DropDownList)gvPenalty.Rows[e.RowIndex].FindControl("ddlBranchPenalty")).SelectedValue);
            h_trans.RemissionAmount = DBUtility.ToDouble(((TextBox)gvPenalty.Rows[e.RowIndex].FindControl("txtRAmount")).Text);
            h_trans.RemissionLetterNo = ((TextBox)gvPenalty.Rows[e.RowIndex].FindControl("txtRLetterNo")).Text;
            h_trans.RemissionLetterDate = DBUtility.ToDateTime(((TextBox)gvPenalty.Rows[e.RowIndex].FindControl("txtRLetterDate")).Text);

            H_EmployeePenalty.Update(tm, h_trans);
            tm.Commit();
            gvPenalty.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadPenalty(Convert.ToInt32(hdnid.Value));
        }

        protected void gvPenalty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 h_EmployeePenaltyId = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeePenalty.Delete(tm, h_EmployeePenaltyId);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadPenalty(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                DateTime temp;
                Int32 amount;
                if (string.IsNullOrEmpty(((TextBox)gvPenalty.FooterRow.FindControl("txtLetterNoAdd")).Text))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter Letter No";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvPenalty.FooterRow.FindControl("txtLetterDateAdd")).Text) || !DateTime.TryParse(((TextBox)gvPenalty.FooterRow.FindControl("txtLetterDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Letter Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvPenalty.FooterRow.FindControl("txtAmountAdd")).Text) || !Int32.TryParse(((TextBox)gvPenalty.FooterRow.FindControl("txtAmountAdd")).Text, out amount))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Amount";
                    this.ShowUIMessage(msg);
                    return;
                }
               
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                TransactionManager tm = new TransactionManager(true);

                H_EmployeePenalty h_trans = new H_EmployeePenalty();
                h_trans.H_EmployeeId = Convert.ToInt32(hdnid.Value);
                h_trans.BranchId = DBUtility.ToInt32(((DropDownList)gvPenalty.FooterRow.FindControl("ddlPenaltyBranchAdd")).SelectedValue);
                h_trans.FineType = ((DropDownList)gvPenalty.FooterRow.FindControl("ddlFineTypeAdd")).SelectedValue;
                h_trans.LetterNo = ((TextBox)gvPenalty.FooterRow.FindControl("txtLetterNoAdd")).Text;
                h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvPenalty.FooterRow.FindControl("txtLetterDateAdd")).Text);
                h_trans.FineAmount = DBUtility.ToDouble(((TextBox)gvPenalty.FooterRow.FindControl("txtAmountAdd")).Text);
                h_trans.RemissionLetterNo = DBUtility.ToNullableString(((TextBox)gvPenalty.FooterRow.FindControl("txtRLetterNoAdd")).Text);
                h_trans.RemissionLetterDate = DBUtility.ToNullableDateTime(((TextBox)gvPenalty.FooterRow.FindControl("txtRLetterDateAdd")).Text);
                h_trans.RemissionAmount = DBUtility.ToNullableDouble(((TextBox)gvPenalty.FooterRow.FindControl("txtRAmountAdd")).Text);

                H_EmployeePenalty.Insert(tm, h_trans);
                tm.Commit();

                LoadPenalty(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void ddlDistrictPenalty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvPenalty.EditIndex;
            DropDownList ddlBranch = (DropDownList)gvPenalty.Rows[rowindex].FindControl("ddlBranchPenalty");
            if (ddlBranch != null)
            {
                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(DistrictId));
                ddlBranch.DataTextField = "Name";
                ddlBranch.DataValueField = "Id";
                ddlBranch.DataBind();
            }
        }
        protected void ddlPenaltyDistrictAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FromDistrictId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlFromBranch = (DropDownList)gvPenalty.FooterRow.FindControl("ddlPenaltyBranchAdd");
            if (ddlFromBranch != null)
            {
                ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(FromDistrictId));
                ddlFromBranch.DataTextField = "Name";
                ddlFromBranch.DataValueField = "Id";
                ddlFromBranch.DataBind();
            }
        }
        private void LoadWarning(int h_EmployeeId)
        {
            IList<H_EmployeeWarning> penalty = H_EmployeeWarning.Find("H_EmployeeId=" + h_EmployeeId, " LetterDate");
            IList<Branch> branch = Branch.FindAll();
            IList<Region> region = Region.FindAll();
            IList<Subzone> dist = Subzone.FindAll();
            var gList = (from p in penalty
                         join b in branch on p.BranchId equals b.Id
                         join r in region on b.RegionId equals r.Id
                         join d in dist on r.SubzoneId equals d.Id
                         select new
                         {
                             p.Id,
                             H_EmployeeId = p.H_EmployeeId,
                             p.LetterNo,
                             p.LetterDate,
                             BranchId = p.BranchId,
                             BranchName = b.Name,
                             DistrictId = d.Id,
                             DistrictName = d.Name,
                             p.Duration,
                             p.TotalWarningTime
                         }).OrderBy(w => w.LetterDate);
            DataTable dt = UIUtility.LINQToDataTable(gList);
            gvWarning.DataSource = dt;
            gvWarning.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                DropDownList ddlDistrict = (DropDownList)gvWarning.FooterRow.FindControl("ddlDistrictWarningAdd");
                DropDownList ddlBranch = (DropDownList)gvWarning.FooterRow.FindControl("ddlBranchWarningAdd");
                if (ddlDistrict != null)
                {
                    IList<Subzone> disList = Subzone.FindAll("Name");
                    ddlDistrict.DataSource = disList;
                    ddlDistrict.DataTextField = "Name";
                    ddlDistrict.DataValueField = "Id";
                    ddlDistrict.DataBind();

                    ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlDistrict.SelectedValue));
                    ddlBranch.DataTextField = "Name";
                    ddlBranch.DataValueField = "Id";
                    ddlBranch.DataBind();
                }
            }
            foreach (GridViewRow row in gvWarning.Rows)
            {
                LinkButton lnkFull = row.FindControl("linkDelete") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);
            }
            
        }
        protected void ddlDistrictWarningAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvPenalty.EditIndex;
            DropDownList ddlBranch = (DropDownList)gvWarning.FooterRow.FindControl("ddlBranchWarningAdd");
            if (ddlBranch != null)
            {
                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(DistrictId));
                ddlBranch.DataTextField = "Name";
                ddlBranch.DataValueField = "Id";
                ddlBranch.DataBind();
            }
        }
        protected void btnWarning_Click(object sender, EventArgs e)
        {
            pnlIncHeldup.Visible = false;
            pnlGrade.Visible = false;
            pnlBranch.Visible = false;
            pnlTransfer.Visible = false;
            pnlLeave.Visible = false;
            pnlPenalty.Visible = false;
            pnlPromoEdit.Visible = false;
            pnlDesignation.Visible = false;
            pnlPromotion.Visible = false;
            pnlConsultancy.Visible = false;
            pnlDropout.Visible = false;
            pnlRejoin.Visible = false;
            pnlTransferEdit.Visible = false;
            pnlWarning.Visible = true;
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (!HasEditPermission(h_Employee.Id))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Information;
                    msg.Msg = "You have no Update Permission for this Employee";
                    ShowUIMessage(msg);
                    return;
                }
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                LoadWarning(h_Employee.Id);

            }
        }

        protected void gvWarning_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string h_EmployeeId = gvWarning.DataKeys[e.NewEditIndex]["H_EmployeeId"].ToString();
            string DistrictId = gvWarning.DataKeys[e.NewEditIndex]["DistrictId"].ToString();
            string BranchId = gvWarning.DataKeys[e.NewEditIndex]["BranchId"].ToString();

            gvWarning.EditIndex = e.NewEditIndex;
            LoadWarning(Convert.ToInt32(h_EmployeeId));
            DropDownList ddlDist = (DropDownList)gvWarning.Rows[e.NewEditIndex].FindControl("ddlDistrictWarning");
            DropDownList ddlBranch = (DropDownList)gvWarning.Rows[e.NewEditIndex].FindControl("ddlBranchWarning");
            if (ddlDist != null)
            {
                IList<Subzone> disList = Subzone.FindAll("Name");
                ddlDist.DataSource = disList;
                ddlDist.DataTextField = "Name";
                ddlDist.DataValueField = "Id";
                ddlDist.DataBind();
                ddlDist.SelectedValue = DistrictId;

                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(DistrictId));
                ddlBranch.DataTextField = "Name";
                ddlBranch.DataValueField = "Id";
                ddlBranch.DataBind();
                ddlBranch.SelectedValue = BranchId;
            }

            gvWarning.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvWarning_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            string EmployeeId = gvWarning.DataKeys[e.RowIndex]["H_EmployeeId"].ToString();
            gvWarning.EditIndex = -1;
            LoadWarning(Convert.ToInt32(EmployeeId));
        }

        protected void gvWarning_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string h_EmployeePenaltyId = gvWarning.DataKeys[e.RowIndex]["Id"].ToString();
            TransactionManager tm = new TransactionManager(true);
            H_EmployeeWarning h_trans = H_EmployeeWarning.GetById(Convert.ToInt32(h_EmployeePenaltyId));
            h_trans.LetterNo = ((TextBox)gvWarning.Rows[e.RowIndex].FindControl("txtLetterNo")).Text;
            h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvWarning.Rows[e.RowIndex].FindControl("txtLetterDate")).Text);
            h_trans.BranchId = Convert.ToInt32(((DropDownList)gvWarning.Rows[e.RowIndex].FindControl("ddlBranchWarning")).SelectedValue);
            h_trans.Duration = DBUtility.ToString(((TextBox)gvWarning.Rows[e.RowIndex].FindControl("txtDuration")).Text);
            h_trans.TotalWarningTime = DBUtility.ToInt32(((TextBox)gvWarning.Rows[e.RowIndex].FindControl("txtTotalWarningTime")).Text);

            H_EmployeeWarning.Update(tm, h_trans);
            tm.Commit();
            gvWarning.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadWarning(Convert.ToInt32(hdnid.Value));
        }

        protected void gvWarning_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 WarningId = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeeWarning.Delete(tm, WarningId);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadWarning(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                DateTime temp;
                Int32 amount;
                if (string.IsNullOrEmpty(((TextBox)gvWarning.FooterRow.FindControl("txtLetterNoAdd")).Text))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter Letter No";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvWarning.FooterRow.FindControl("txtLetterDateAdd")).Text) || !DateTime.TryParse(((TextBox)gvWarning.FooterRow.FindControl("txtLetterDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Letter Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvWarning.FooterRow.FindControl("txtDurationAdd")).Text) || !Int32.TryParse(((TextBox)gvWarning.FooterRow.FindControl("txtDurationAdd")).Text, out amount))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Year of Letter Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvWarning.FooterRow.FindControl("txtTotalWarningTimeAdd")).Text) || !Int32.TryParse(((TextBox)gvWarning.FooterRow.FindControl("txtTotalWarningTimeAdd")).Text, out amount))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter Warning Time";
                    this.ShowUIMessage(msg);
                    return;
                }

                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                TransactionManager tm = new TransactionManager(true);

                H_EmployeeWarning h_trans = new H_EmployeeWarning();
                h_trans.H_EmployeeId = Convert.ToInt32(hdnid.Value);
                h_trans.BranchId = DBUtility.ToInt32(((DropDownList)gvWarning.FooterRow.FindControl("ddlBranchWarningAdd")).SelectedValue);
                h_trans.LetterNo = ((TextBox)gvWarning.FooterRow.FindControl("txtLetterNoAdd")).Text;
                h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvWarning.FooterRow.FindControl("txtLetterDateAdd")).Text);
                h_trans.BranchId = Convert.ToInt32(((DropDownList)gvWarning.FooterRow.FindControl("ddlBranchWarningAdd")).SelectedValue);
                h_trans.Duration = DBUtility.ToString(((TextBox)gvWarning.FooterRow.FindControl("txtDurationAdd")).Text);
                h_trans.TotalWarningTime = DBUtility.ToInt32(((TextBox)gvWarning.FooterRow.FindControl("txtTotalWarningTimeAdd")).Text);

                H_EmployeeWarning.Insert(tm, h_trans);
                tm.Commit();

                LoadWarning(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void ddlDistrictWarning_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvWarning.EditIndex;
            DropDownList ddlBranch = (DropDownList)gvWarning.Rows[rowindex].FindControl("ddlBranchWarning");
            if (ddlBranch != null)
            {
                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(DistrictId));
                ddlBranch.DataTextField = "Name";
                ddlBranch.DataValueField = "Id";
                ddlBranch.DataBind();
            }
        }

        protected void btnPromotion_Click(object sender, EventArgs e)
        {
            pnlIncHeldup.Visible = false;
            pnlGrade.Visible = true;
            pnlBranch.Visible = false;
            pnlTransfer.Visible = false;
            pnlLeave.Visible = false;
            pnlPenalty.Visible = false;
            pnlWarning.Visible = false;
            pnlDesignation.Visible = true;
            pnlPromotion.Visible = true;
            pnlConsultancy.Visible = false;
            pnlDropout.Visible = false;
            pnlRejoin.Visible = false;
            pnlPromoEdit.Visible = false;
            pnlTransferEdit.Visible = false;
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                LoadGrade(h_Employee.Id);
                LoadDesignation(h_Employee.Id);
                LoadPromotion(h_Employee.Id);

            }

        }

        private void LoadGrade(int h_EmployeeId)
        {
            IList<H_EmployeeGrade> employeeGrade = H_EmployeeGrade.Find("H_EmployeeId=" + h_EmployeeId, " StartDate");
            IList<H_Grade> grade = H_Grade.FindAll("Name");
            var gradeList = (from eg in employeeGrade
                             join g in grade on eg.H_GradeId equals g.Id
                             select new
                             {
                                 eg.Id,
                                 eg.H_EmployeeId,
                                 eg.H_GradeId,
                                 GradeName = g.Name,
                                 eg.StartDate,
                                 eg.EndDate
                             }).OrderBy(g => g.EndDate);
            gvGrade.DataSource = UIUtility.LINQToDataTable(gradeList);
            gvGrade.DataBind();
            DropDownList ddlGradeAdd = (DropDownList)gvGrade.FooterRow.FindControl("ddlGradeAdd");

            if (ddlGradeAdd != null)
            {
                IList<H_Grade> disList = grade;// H_Grade.FindAll("Name");
                ddlGradeAdd.DataSource = disList;
                ddlGradeAdd.DataTextField = "Name";
                ddlGradeAdd.DataValueField = "Id";
                ddlGradeAdd.DataBind();
            }
        }

        private void LoadPromotion(int h_EmployeeId)
        {
            IList<H_EmployeePromotion> list = H_EmployeePromotion.Find("H_EmployeeId=" + h_EmployeeId, " LetterDate");
            IList<H_Grade> grade = H_Grade.FindAll();
            IList<H_Designation> desg = H_Designation.FindAll();
            var gList = (from t in list
                         join og in grade on t.OldH_GradeId equals og.Id
                         join ng in grade on t.NewH_GradeId equals ng.Id
                         join od in desg on t.OldH_DesignationId equals od.Id
                         join nd in desg on t.NewH_DesignationId equals nd.Id
                         select new
                         {
                             t.Id,
                             t.LetterNo,
                             t.LetterDate,
                             t.PromotionDate,
                             OldGradeId = og.Id,
                             OldGradeName = og.Name,
                             NewGradeId = ng.Id,
                             NewGradeName = ng.Name,
                             OldDesignationId = od.Id,
                             OldDesignationName = od.Name,
                             NewDesignationId = nd.Id,
                             NewDesignationName = nd.Name,
                             t.Type,
                             TypeName = ((H_EmployeePromotion.Types)Enum.ToObject(typeof(H_EmployeePromotion.Types), (Int32)t.Type)).ToString()
                         }).OrderBy(p => p.LetterDate);
            DataTable dt = UIUtility.LINQToDataTable(gList);
            if (dt == null || dt.Rows.Count == 0)
            {
                dt.Columns.Add("Id", typeof(int)); dt.Columns.Add("LetterNo", typeof(string)); dt.Columns.Add("LetterDate", typeof(DateTime));
                dt.Columns.Add("PromotionDate", typeof(DateTime)); dt.Columns.Add("OldGradeId", typeof(int)); dt.Columns.Add("OldGradeName", typeof(string));
                dt.Columns.Add("NewGradeId", typeof(int)); dt.Columns.Add("NewGradeName", typeof(string)); dt.Columns.Add("OldDesignationId", typeof(int));
                dt.Columns.Add("OldDesignationName", typeof(string)); dt.Columns.Add("NewDesignationId", typeof(int)); dt.Columns.Add("NewDesignationName", typeof(string));
                dt.Columns.Add("Type", typeof(int)); dt.Columns.Add("TypeName", typeof(string));
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            gvPromotion.DataSource = dt;
            gvPromotion.DataBind();
            DropDownList ddlOG = (DropDownList)gvPromotion.FooterRow.FindControl("ddlOldGradeAdd");
            DropDownList ddlOD = (DropDownList)gvPromotion.FooterRow.FindControl("ddlOldDesignationAdd");
            DropDownList ddlNG = (DropDownList)gvPromotion.FooterRow.FindControl("ddlNewGradeAdd");
            DropDownList ddlND = (DropDownList)gvPromotion.FooterRow.FindControl("ddlNewDesignationAdd");
            DropDownList ddlTypeAdd = (DropDownList)gvPromotion.FooterRow.FindControl("ddlTypeAdd");
            if (ddlOG != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlOG.DataSource = disList;
                ddlOG.DataTextField = "Name";
                ddlOG.DataValueField = "Id";
                ddlOG.DataBind();

                ddlOD.DataSource = GetDesignationByGradeId(Convert.ToInt32(ddlOG.SelectedValue));
                ddlOD.DataTextField = "Name";
                ddlOD.DataValueField = "Id";
                ddlOD.DataBind();
            }
            if (ddlNG != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlNG.DataSource = disList;
                ddlNG.DataTextField = "Name";
                ddlNG.DataValueField = "Id";
                ddlNG.DataBind();

                ddlND.DataSource = GetDesignationByGradeId(Convert.ToInt32(ddlNG.SelectedValue));
                ddlND.DataTextField = "Name";
                ddlND.DataValueField = "Id";
                ddlND.DataBind();
            }
            UIUtility.LoadEnums(ddlTypeAdd, typeof(H_EmployeePromotion.Types), false, false, false);
            // gvPromotion.FooterRow.BackColor = System.Drawing.Color.DarkKhaki;
        }

        private void LoadDesignation(int h_EmployeeId)
        {
            IList<H_EmployeeDesignation> employeeDesignation = H_EmployeeDesignation.Find("H_EmployeeId=" + h_EmployeeId, " StartDate");
            IList<H_Grade> grade = H_Grade.FindAll();
            IList<H_Designation> designation = H_Designation.FindAll();
            IList<H_GradeDesignation> gradeDesignation = H_GradeDesignation.FindAll();
            var gList = (from ed in employeeDesignation
                         join d in designation on ed.H_DesignationId equals d.Id
                         join gd in gradeDesignation on d.Id equals gd.H_DesignationId
                         join g in grade on gd.H_GradeId equals g.Id
                         select new
                         {
                             ed.Id,
                             H_EmployeeId = ed.H_EmployeeId,
                             ed.StartDate,
                             ed.EndDate,
                             DesignationId = d.Id,
                             DesignationName = d.Name,
                             GradeId = g.Id,
                             GradeName = g.Name
                         }).OrderBy(d => d.EndDate);
            DataTable dt = UIUtility.LINQToDataTable(gList);
            gvDesignation.DataSource = dt;
            gvDesignation.DataBind();
            DropDownList ddlGradeAdd = (DropDownList)gvDesignation.FooterRow.FindControl("ddlGradeAdd");
            DropDownList ddlDesgAdd = (DropDownList)gvDesignation.FooterRow.FindControl("ddlDesignationAdd");
            if (ddlGradeAdd != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlGradeAdd.DataSource = disList;
                ddlGradeAdd.DataTextField = "Name";
                ddlGradeAdd.DataValueField = "Id";
                ddlGradeAdd.DataBind();

                ddlDesgAdd.DataSource = GetDesignationByGradeId(Convert.ToInt32(ddlGradeAdd.SelectedValue));
                ddlDesgAdd.DataTextField = "Name";
                ddlDesgAdd.DataValueField = "Id";
                ddlDesgAdd.DataBind();

            }
            //gvDesignation.FooterRow.BackColor = System.Drawing.Color.DarkKhaki;
        }

        protected void gvDesignation_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string h_EmployeeId = gvDesignation.DataKeys[e.NewEditIndex]["H_EmployeeId"].ToString();
            string GradeId = gvDesignation.DataKeys[e.NewEditIndex]["GradeId"].ToString();
            string DesignationId = gvDesignation.DataKeys[e.NewEditIndex]["DesignationId"].ToString();

            gvDesignation.EditIndex = e.NewEditIndex;
            LoadDesignation(Convert.ToInt32(h_EmployeeId));
            DropDownList ddlGrade = (DropDownList)gvDesignation.Rows[e.NewEditIndex].FindControl("ddlGrade");
            DropDownList ddlDesg = (DropDownList)gvDesignation.Rows[e.NewEditIndex].FindControl("ddlDesignation");
            if (ddlGrade != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlGrade.DataSource = disList;
                ddlGrade.DataTextField = "Name";
                ddlGrade.DataValueField = "Id";
                ddlGrade.DataBind();
                ddlGrade.SelectedValue = GradeId;

                ddlDesg.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlDesg.DataTextField = "Name";
                ddlDesg.DataValueField = "Id";
                ddlDesg.DataBind();
                ddlDesg.SelectedValue = DesignationId;
            }

            gvDesignation.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        private DataTable GetDesignationByGradeId(int p)
        {
            IList<H_GradeDesignation> gd = H_GradeDesignation.FindAll().Where(n => n.H_GradeId == p).ToList();
            IList<H_Designation> desg = H_Designation.FindAll();
            var dList = (from d in desg
                         join g in gd on d.Id equals g.H_DesignationId
                         select new
                         {
                             d.Id,
                             d.Name
                         }).OrderBy(n => n.Name);
            return UIUtility.LINQToDataTable(dList);
        }

        protected void gvDesignation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            string EmployeeId = gvDesignation.DataKeys[e.RowIndex]["H_EmployeeId"].ToString();
            gvDesignation.EditIndex = -1;
            LoadDesignation(Convert.ToInt32(EmployeeId));
        }

        protected void gvDesignation_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string Id = gvDesignation.DataKeys[e.RowIndex]["Id"].ToString();
            TransactionManager tm = new TransactionManager(true);
            H_EmployeeDesignation h_trans = H_EmployeeDesignation.GetById(Convert.ToInt32(Id));
            h_trans.StartDate = DBUtility.ToDateTime(((TextBox)gvDesignation.Rows[e.RowIndex].FindControl("txtStartDate")).Text);
            h_trans.EndDate = DBUtility.ToDateTime(((TextBox)gvDesignation.Rows[e.RowIndex].FindControl("txtEndDate")).Text);
            h_trans.H_DesignationId = Convert.ToInt32(((DropDownList)gvDesignation.Rows[e.RowIndex].FindControl("ddlDesignation")).SelectedValue);

            H_EmployeeDesignation.Update(tm, h_trans);
            tm.Commit();
            gvDesignation.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadDesignation(Convert.ToInt32(hdnid.Value));
        }

        protected void gvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 Id = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeeDesignation.Delete(tm, Id);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadDesignation(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                TransactionManager tm = new TransactionManager(true);

                H_EmployeeDesignation h_trans = new H_EmployeeDesignation();
                h_trans.H_EmployeeId = Convert.ToInt32(hdnid.Value);
                h_trans.StartDate = DBUtility.ToDateTime(((TextBox)gvDesignation.FooterRow.FindControl("txtStartDateAdd")).Text);
                h_trans.EndDate = DBUtility.ToDateTime(((TextBox)gvDesignation.FooterRow.FindControl("txtEndDateAdd")).Text);
                h_trans.H_DesignationId = Convert.ToInt32(((DropDownList)gvDesignation.FooterRow.FindControl("ddlDesignationAdd")).SelectedValue);

                H_EmployeeDesignation.Insert(tm, h_trans);
                tm.Commit();

                LoadDesignation(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GradeId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvDesignation.EditIndex;
            DropDownList ddlDesg = (DropDownList)gvDesignation.Rows[rowindex].FindControl("ddlDesignation");
            if (ddlDesg != null)
            {
                ddlDesg.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlDesg.DataTextField = "Name";
                ddlDesg.DataValueField = "Id";
                ddlDesg.DataBind();
            }
        }

        protected void gvPromotion_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string OldGradeId = gvPromotion.DataKeys[e.NewEditIndex]["OldGradeId"].ToString();
            string OldDesignationId = gvPromotion.DataKeys[e.NewEditIndex]["OldDesignationId"].ToString();
            string NewGradeId = gvPromotion.DataKeys[e.NewEditIndex]["NewGradeId"].ToString();
            string NewDesignationId = gvPromotion.DataKeys[e.NewEditIndex]["NewDesignationId"].ToString();
            string NewType = gvPromotion.DataKeys[e.NewEditIndex]["Type"].ToString();
            gvPromotion.EditIndex = e.NewEditIndex;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadPromotion(Convert.ToInt32(hdnid.Value));
            DropDownList ddlOG = (DropDownList)gvPromotion.Rows[e.NewEditIndex].FindControl("ddlOldGrade");
            DropDownList ddlOD = (DropDownList)gvPromotion.Rows[e.NewEditIndex].FindControl("ddlOldDesignation");
            DropDownList ddlNG = (DropDownList)gvPromotion.Rows[e.NewEditIndex].FindControl("ddlNewGrade");
            DropDownList ddlND = (DropDownList)gvPromotion.Rows[e.NewEditIndex].FindControl("ddlNewDesignation");
            DropDownList ddlType = (DropDownList)gvPromotion.Rows[e.NewEditIndex].FindControl("ddlType");
            if (ddlOG != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlOG.DataSource = disList;
                ddlOG.DataTextField = "Name";
                ddlOG.DataValueField = "Id";
                ddlOG.DataBind();
                ddlOG.SelectedValue = OldGradeId;

                ddlOD.DataSource = GetDesignationByGradeId(Convert.ToInt32(OldGradeId));
                ddlOD.DataTextField = "Name";
                ddlOD.DataValueField = "Id";
                ddlOD.DataBind();
                ddlOD.SelectedValue = OldDesignationId;
            }
            if (ddlNG != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlNG.DataSource = disList;
                ddlNG.DataTextField = "Name";
                ddlNG.DataValueField = "Id";
                ddlNG.DataBind();
                ddlNG.SelectedValue = NewGradeId;

                ddlND.DataSource = GetDesignationByGradeId(Convert.ToInt32(NewGradeId));
                ddlND.DataTextField = "Name";
                ddlND.DataValueField = "Id";
                ddlND.DataBind();
                ddlND.SelectedValue = NewDesignationId;
            }
            UIUtility.LoadEnums(ddlType, typeof(H_EmployeePromotion.Types), false, false, true);
            ddlType.SelectedValue = NewType;
            gvPromotion.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvPromotion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPromotion.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadPromotion(Convert.ToInt32(hdnid.Value));
        }

        protected void gvPromotion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string Id = gvPromotion.DataKeys[e.RowIndex]["Id"].ToString();
            TransactionManager tm = new TransactionManager(true);
            H_EmployeePromotion h_trans = H_EmployeePromotion.GetById(Convert.ToInt32(Id));
            h_trans.LetterNo = ((TextBox)gvPromotion.Rows[e.RowIndex].FindControl("txtLetterNo")).Text;
            h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvPromotion.Rows[e.RowIndex].FindControl("txtLetterDate")).Text);
            h_trans.PromotionDate = DBUtility.ToDateTime(((TextBox)gvPromotion.Rows[e.RowIndex].FindControl("txtPromotionDate")).Text);
            h_trans.OldH_GradeId = Convert.ToInt32(((DropDownList)gvPromotion.Rows[e.RowIndex].FindControl("ddlOldGrade")).SelectedValue);
            h_trans.OldH_DesignationId = Convert.ToInt32(((DropDownList)gvPromotion.Rows[e.RowIndex].FindControl("ddlOldDesignation")).SelectedValue);
            h_trans.NewH_GradeId = Convert.ToInt32(((DropDownList)gvPromotion.Rows[e.RowIndex].FindControl("ddlNewGrade")).SelectedValue);
            h_trans.NewH_DesignationId = Convert.ToInt32(((DropDownList)gvPromotion.Rows[e.RowIndex].FindControl("ddlNewDesignation")).SelectedValue);
            h_trans.Type = (H_EmployeePromotion.Types)Convert.ToInt32(((DropDownList)gvPromotion.Rows[e.RowIndex].FindControl("ddlType")).SelectedValue);

            H_EmployeePromotion.Update(tm, h_trans);
            tm.Commit();
            gvPromotion.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadPromotion(Convert.ToInt32(hdnid.Value));
        }

        protected void gvPromotion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 Id = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeePromotion.Delete(tm, Id);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadPromotion(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                TransactionManager tm = new TransactionManager(true);

                H_EmployeePromotion h_trans = new H_EmployeePromotion();
                h_trans.H_EmployeeId = Convert.ToInt32(hdnid.Value);
                h_trans.LetterNo = ((TextBox)gvPromotion.FooterRow.FindControl("txtLetterNoAdd")).Text;
                h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvPromotion.FooterRow.FindControl("txtLetterDateAdd")).Text);
                h_trans.PromotionDate = DBUtility.ToDateTime(((TextBox)gvPromotion.FooterRow.FindControl("txtPromotionDateAdd")).Text);
                h_trans.OldH_GradeId = Convert.ToInt32(((DropDownList)gvPromotion.FooterRow.FindControl("ddlOldGradeAdd")).SelectedValue);
                h_trans.OldH_DesignationId = Convert.ToInt32(((DropDownList)gvPromotion.FooterRow.FindControl("ddlOldDesignationAdd")).SelectedValue);
                h_trans.NewH_GradeId = Convert.ToInt32(((DropDownList)gvPromotion.FooterRow.FindControl("ddlNewGradeAdd")).SelectedValue);
                h_trans.NewH_DesignationId = Convert.ToInt32(((DropDownList)gvPromotion.FooterRow.FindControl("ddlNewDesignationAdd")).SelectedValue);
                h_trans.Type = (H_EmployeePromotion.Types)Convert.ToInt32(((DropDownList)gvPromotion.FooterRow.FindControl("ddlTypeAdd")).SelectedValue);

                H_EmployeePromotion.Insert(tm, h_trans);
                tm.Commit();

                LoadPromotion(Convert.ToInt32(hdnid.Value));
            }
        }
        protected void ddlOldGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GradeId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvPromotion.EditIndex;
            DropDownList ddlDesg = (DropDownList)gvPromotion.Rows[rowindex].FindControl("ddlOldDesignation");
            if (ddlDesg != null)
            {
                ddlDesg.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlDesg.DataTextField = "Name";
                ddlDesg.DataValueField = "Id";
                ddlDesg.DataBind();
            }
        }
        protected void ddlNewGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GradeId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvPromotion.EditIndex;
            DropDownList ddlDesg = (DropDownList)gvPromotion.Rows[rowindex].FindControl("ddlNewDesignation");
            if (ddlDesg != null)
            {
                ddlDesg.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlDesg.DataTextField = "Name";
                ddlDesg.DataValueField = "Id";
                ddlDesg.DataBind();
            }
        }

        protected void btnConsultancy_Click(object sender, EventArgs e)
        {
            pnlGrade.Visible = false;
            pnlIncHeldup.Visible = false;
            pnlBranch.Visible = false;
            pnlTransfer.Visible = false;
            pnlLeave.Visible = false;
            pnlPenalty.Visible = false;
            pnlWarning.Visible = false;
            pnlDesignation.Visible = false;
            pnlPromotion.Visible = false;
            pnlConsultancy.Visible = true;
            pnlDropout.Visible = false;
            pnlRejoin.Visible = false;
            pnlPromoEdit.Visible = false;
            pnlTransferEdit.Visible = false;

            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                LoadConsultancy(h_Employee.Id);
            }
        }
        void LoadConsultancy(int h_EmployeeId)
        {
            var aTransactionManager = new TransactionManager(false);
            string query = "SELECT hec.Id,"
                    + "hec.LetterNo,"
                    + "hec.LetterDate,"
                    + "hec.NgoName AS NgoName,"
                    + "o.Name AS Organization,"
                    + "hec.StartDate,"
                    + "hec.EndDate,"
                    + "c.Name AS Country"
                    + " FROM "
                    + "H_EmployeeConsultency AS hec INNER JOIN Country AS c on hec.CountryId=c.Id"
                    + " INNER JOIN Organization AS o ON hec.OrganizationId=o.Id where hec.H_EmployeeID=" + h_EmployeeId + " ORDER BY hec.LetterDate Desc";


            DataSet ds = aTransactionManager.GetDataSet(query);
            DataTable dtleave = ds.Tables[0];
            gvConsultancy.DataSource = dtleave;
            gvConsultancy.DataBind();

        }
        protected void gvConsultancy_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvConsultancy.EditIndex = e.NewEditIndex;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadConsultancy(Convert.ToInt32(hdnid.Value));
            DropDownList ddlOrganization = (DropDownList)gvConsultancy.Rows[e.NewEditIndex].FindControl("ddlOrganization");
            DropDownList ddlCountry = (DropDownList)gvConsultancy.Rows[e.NewEditIndex].FindControl("ddlCountry");

            if (ddlOrganization != null && ddlCountry != null)
            {
                int Id = Convert.ToInt32(gvConsultancy.DataKeys[e.NewEditIndex]["Id"].ToString());
                H_EmployeeConsultency consuntancy = H_EmployeeConsultency.GetById(Id);
                if (consuntancy == null) throw new ArgumentNullException("consuntancy");
                ddlCountry.DataSource = Country.Find("", "Name");
                ddlCountry.DataBind();
                ddlCountry.SelectedValue = consuntancy.CountryId.ToString();


                ddlOrganization.DataSource = Organization.Find("", "Name");
                ddlOrganization.DataBind();
                ddlOrganization.SelectedValue = consuntancy.NgoName;
            }
            gvConsultancy.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvConsultancy_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvConsultancy.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadConsultancy(Convert.ToInt32(hdnid.Value));
        }

        protected void gvConsultancy_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int Id = Convert.ToInt32(gvConsultancy.DataKeys[e.RowIndex]["Id"].ToString());
            TextBox txtLetterNo = gvConsultancy.Rows[e.RowIndex].FindControl("txtLetterNo") as TextBox;
            TextBox txtLetterDate = gvConsultancy.Rows[e.RowIndex].FindControl("txtLetterDate") as TextBox;
            TextBox txtNgoName = gvConsultancy.Rows[e.RowIndex].FindControl("txtNgoName") as TextBox;
            DropDownList ddlOrganization = gvConsultancy.Rows[e.RowIndex].FindControl("ddlOrganization") as DropDownList;
            DropDownList ddlCountry = gvConsultancy.Rows[e.RowIndex].FindControl("ddlCountry") as DropDownList;
            TextBox txtStartDate = gvConsultancy.Rows[e.RowIndex].FindControl("txtStartDate") as TextBox;
            H_EmployeeConsultency consuntancy = H_EmployeeConsultency.GetById(Id);
            if (consuntancy != null)
            {

                TransactionManager tm = new TransactionManager(true);
                consuntancy.LetterNo = txtLetterNo.Text;
                consuntancy.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
                consuntancy.NgoName = txtNgoName.Text;
                consuntancy.OrganizationId = Convert.ToInt32(ddlOrganization.SelectedValue);
                consuntancy.StartDate = DBUtility.ToDateTime(txtStartDate.Text);
                consuntancy.CountryId = Convert.ToInt32(ddlCountry.SelectedValue);
                H_EmployeeConsultency.Update(tm, consuntancy);
                tm.Commit();
                gvConsultancy.EditIndex = -1;
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadConsultancy(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void gvConsultancy_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 Id = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeeConsultency.Delete(tm, Id);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadConsultancy(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void ddlGradeAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GradeId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlDesgAdd = (DropDownList)gvDesignation.FooterRow.FindControl("ddlDesignationAdd");
            if (ddlDesgAdd != null)
            {
                ddlDesgAdd.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlDesgAdd.DataTextField = "Name";
                ddlDesgAdd.DataValueField = "Id";
                ddlDesgAdd.DataBind();
            }
        }

        protected void ddlOldGradeAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GradeId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlDesgAdd = (DropDownList)gvPromotion.FooterRow.FindControl("ddlOldDesignationAdd");
            if (ddlDesgAdd != null)
            {
                ddlDesgAdd.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlDesgAdd.DataTextField = "Name";
                ddlDesgAdd.DataValueField = "Id";
                ddlDesgAdd.DataBind();
            }
        }

        protected void ddlNewGradeAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GradeId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlDesgAdd = (DropDownList)gvPromotion.FooterRow.FindControl("ddlNewDesignationAdd");
            if (ddlDesgAdd != null)
            {
                ddlDesgAdd.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlDesgAdd.DataTextField = "Name";
                ddlDesgAdd.DataValueField = "Id";
                ddlDesgAdd.DataBind();
            }
        }

        protected void gvGrade_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string h_EmployeeId = gvGrade.DataKeys[e.NewEditIndex]["H_EmployeeId"].ToString();
            string GradeId = gvGrade.DataKeys[e.NewEditIndex]["H_GradeId"].ToString();

            gvGrade.EditIndex = e.NewEditIndex;
            LoadGrade(Convert.ToInt32(h_EmployeeId));
            DropDownList ddlGrade = (DropDownList)gvGrade.Rows[e.NewEditIndex].FindControl("ddlGrade");
            if (ddlGrade != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlGrade.DataSource = disList;
                ddlGrade.DataTextField = "Name";
                ddlGrade.DataValueField = "Id";
                ddlGrade.DataBind();
                ddlGrade.SelectedValue = GradeId;
            }

            gvGrade.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvGrade_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            string EmployeeId = gvGrade.DataKeys[e.RowIndex]["H_EmployeeId"].ToString();
            gvGrade.EditIndex = -1;
            LoadGrade(Convert.ToInt32(EmployeeId));
        }

        protected void gvGrade_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string Id = gvGrade.DataKeys[e.RowIndex]["Id"].ToString();
            TransactionManager tm = new TransactionManager(true);
            H_EmployeeGrade h_trans = H_EmployeeGrade.GetById(Convert.ToInt32(Id));
            h_trans.StartDate = DBUtility.ToDateTime(((TextBox)gvGrade.Rows[e.RowIndex].FindControl("txtStartDate")).Text);
            h_trans.EndDate = DBUtility.ToDateTime(((TextBox)gvGrade.Rows[e.RowIndex].FindControl("txtEndDate")).Text);
            h_trans.H_GradeId = Convert.ToInt32(((DropDownList)gvGrade.Rows[e.RowIndex].FindControl("ddlGrade")).SelectedValue);
            H_EmployeeGrade.Update(tm, h_trans);
            tm.Commit();
            gvGrade.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadGrade(Convert.ToInt32(hdnid.Value));
        }

        protected void gvGrade_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 Id = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeeGrade.Delete(tm, Id);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadGrade(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                TransactionManager tm = new TransactionManager(true);

                H_EmployeeGrade h_trans = new H_EmployeeGrade();
                h_trans.H_EmployeeId = Convert.ToInt32(hdnid.Value);
                h_trans.StartDate = DBUtility.ToDateTime(((TextBox)gvGrade.FooterRow.FindControl("txtStartDateAdd")).Text);
                h_trans.EndDate = DBUtility.ToDateTime(((TextBox)gvGrade.FooterRow.FindControl("txtEndDateAdd")).Text);
                h_trans.H_GradeId = Convert.ToInt32(((DropDownList)gvGrade.FooterRow.FindControl("ddlGradeAdd")).SelectedValue);

                H_EmployeeGrade.Insert(tm, h_trans);
                tm.Commit();

                LoadGrade(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void btnDropOut_Click(object sender, EventArgs e)
        {
            pnlIncHeldup.Visible = false;
            pnlGrade.Visible = false;
            pnlBranch.Visible = false;
            pnlTransfer.Visible = false;
            pnlLeave.Visible = false;
            pnlPenalty.Visible = false;
            pnlWarning.Visible = false;
            pnlDesignation.Visible = false;
            pnlPromotion.Visible = false;
            pnlConsultancy.Visible = false;
            pnlDropout.Visible = true;
            pnlRejoin.Visible = false;
            pnlPromoEdit.Visible = false;
            pnlTransferEdit.Visible = false;
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                LoadDropOut(h_Employee.Id);
            }
        }

        private void LoadDropOut(int h_EmployeeId)
        {
            IList<H_EmployeeDrop> list = H_EmployeeDrop.Find("H_EmployeeId=" + h_EmployeeId, " LetterDate");
            var gList = (from t in list
                         select new
                         {
                             t.Id,
                             t.LetterNo,
                             t.LetterDate,
                             t.DropDate,
                             t.Type,
                             TypeName = ((H_EmployeeDrop.Types)Enum.ToObject(typeof(H_EmployeeDrop.Types), (Int32)t.Type)).ToString()
                         });
            DataTable dt = UIUtility.LINQToDataTable(gList);
            if (dt == null || dt.Rows.Count == 0)
            {
                dt.Columns.Add("Id", typeof(int)); dt.Columns.Add("LetterNo", typeof(string)); dt.Columns.Add("LetterDate", typeof(DateTime));
                dt.Columns.Add("DropDate", typeof(DateTime)); dt.Columns.Add("Type", typeof(int)); dt.Columns.Add("TypeName", typeof(string));
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            gvDropout.DataSource = dt;
            gvDropout.DataBind();
            DropDownList ddlTypeAdd = (DropDownList)gvDropout.FooterRow.FindControl("ddlTypeAdd");

            UIUtility.LoadEnums(ddlTypeAdd, typeof(H_EmployeeDrop.Types), false, false, false);
        }

        protected void gvDropout_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string NewType = gvDropout.DataKeys[e.NewEditIndex]["Type"].ToString();
            gvDropout.EditIndex = e.NewEditIndex;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadDropOut(Convert.ToInt32(hdnid.Value));
            DropDownList ddlType = (DropDownList)gvDropout.Rows[e.NewEditIndex].FindControl("ddlType");

            UIUtility.LoadEnums(ddlType, typeof(H_EmployeeDrop.Types), false, false, true);
            ddlType.SelectedValue = NewType;
            gvDropout.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvDropout_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDropout.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadDropOut(Convert.ToInt32(hdnid.Value));
        }

        protected void gvDropout_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string Id = gvDropout.DataKeys[e.RowIndex]["Id"].ToString();
            TransactionManager tm = new TransactionManager(true);
            H_EmployeeDrop h_trans = H_EmployeeDrop.GetById(Convert.ToInt32(Id));
            h_trans.LetterNo = ((TextBox)gvDropout.Rows[e.RowIndex].FindControl("txtLetterNo")).Text;
            h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvDropout.Rows[e.RowIndex].FindControl("txtLetterDate")).Text);
            h_trans.DropDate = DBUtility.ToDateTime(((TextBox)gvDropout.Rows[e.RowIndex].FindControl("txtDropDate")).Text);

            h_trans.Type = (H_EmployeeDrop.Types)Convert.ToInt32(((DropDownList)gvDropout.Rows[e.RowIndex].FindControl("ddlType")).SelectedValue);

            H_EmployeeDrop.Update(tm, h_trans);
            tm.Commit();
            gvDropout.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadDropOut(Convert.ToInt32(hdnid.Value));
        }

        protected void gvDropout_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 Id = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeeDrop.Delete(tm, Id);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadDropOut(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                TransactionManager tm = new TransactionManager(true);

                H_EmployeeDrop h_trans = new H_EmployeeDrop();
                h_trans.H_EmployeeId = Convert.ToInt32(hdnid.Value);
                h_trans.LetterNo = ((TextBox)gvDropout.FooterRow.FindControl("txtLetterNoAdd")).Text;
                h_trans.LetterDate = DBUtility.ToDateTime(((TextBox)gvDropout.FooterRow.FindControl("txtLetterDateAdd")).Text);
                h_trans.DropDate = DBUtility.ToDateTime(((TextBox)gvDropout.FooterRow.FindControl("txtDropDateAdd")).Text);

                h_trans.Type = (H_EmployeeDrop.Types)Convert.ToInt32(((DropDownList)gvDropout.FooterRow.FindControl("ddlTypeAdd")).SelectedValue);

                H_EmployeeDrop.Insert(tm, h_trans);
                tm.Commit();

                LoadDropOut(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void btnRejoin_Click(object sender, EventArgs e)
        {
            pnlIncHeldup.Visible = false;
            pnlGrade.Visible = false;
            pnlWarning.Visible = false;
            pnlPenalty.Visible = false;
            pnlBranch.Visible = false;
            pnlTransfer.Visible = false;
            pnlLeave.Visible = false;
            pnlDesignation.Visible = false;
            pnlPromotion.Visible = false;
            pnlConsultancy.Visible = false;
            pnlDropout.Visible = false;
            pnlPromoEdit.Visible = false;
            pnlTransferEdit.Visible = false;
            pnlRejoin.Visible = true;
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (!HasEditPermission(h_Employee.Id))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Information;
                    msg.Msg = "You have no Update Permission for this Employee";
                    ShowUIMessage(msg);
                    return;
                }
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                LoadEmployeeRejoin(h_Employee.Id);
                
            }
        }

        private void LoadEmployeeRejoin(Int32 h_EmployeeId)
        {
            IList<H_EmployeeRejoin> list = H_EmployeeRejoin.Find("H_EmployeeId=" + h_EmployeeId, " LetterDate");
            IList<Branch> branch = Branch.FindAll();
            IList<Region> region = Region.FindAll();
            IList<Subzone> dist = Subzone.FindAll();
            var gList = (from t in list
                         join sb in branch on t.SourceBranchId equals sb.Id
                         join sr in region on sb.RegionId equals sr.Id
                         join sd in dist on sr.SubzoneId equals sd.Id
                         join db in branch on t.DestinationBranchId equals db.Id
                         join dr in region on db.RegionId equals dr.Id
                         join dd in dist on dr.SubzoneId equals dd.Id
                         select new
                         {
                             t.Id,
                             t.LetterNo,
                             t.LetterDate,
                             t.SourceBranchId,
                             t.DestinationBranchId,
                             t.FromDate,
                             t.RejoinDate,
                             SourceDistrictId = sd.Id,
                             sDistrictName = sd.Name,
                             sBranchName = sb.Name,
                             dDistrictName = dd.Name,
                             dBranchName = db.Name,
                             DestinationDistrictId = dd.Id,
                             LeaveTypeId = t.LeaveType,
                             LeaveType = ((H_EmployeeLeave.Types)t.LeaveType).ToString(),
                             RejoinTypeId = t.RejoinType,
                             RejoinType = ((H_Employee.EmploymentTypes)t.RejoinType).ToString()
                         }).OrderBy(o => o.LetterDate);
            DataTable dt = UIUtility.LINQToDataTable(gList);
            gvRejoin.DataSource = dt;
            gvRejoin.DataBind();
            if (dt != null && dt.Rows.Count > 0)
            {
                DropDownList ddlFDist = (DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinFromDistrictAdd");
                DropDownList ddlTDist = (DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinToDistrictAdd");
                DropDownList ddlFromBranch = (DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinFromBranchAdd");
                DropDownList ddlToBranch = (DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinToBranchAdd");
                DropDownList ddlLeaveType = (DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinLeaveTypeAdd");
                DropDownList ddlRejoinType = (DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinTypeAdd");
                if (ddlFDist != null)
                {
                    UIUtility.LoadEnums(ddlLeaveType, typeof(H_EmployeeLeave.Types), false, false, true);
                    UIUtility.LoadEnums(ddlRejoinType, typeof(H_Employee.EmploymentTypes), false, false, true);

                    IList<Subzone> disList = Subzone.FindAll("Name");
                    ddlFDist.DataSource = disList;
                    ddlFDist.DataTextField = "Name";
                    ddlFDist.DataValueField = "Id";
                    ddlFDist.DataBind();

                    ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlFDist.SelectedValue));
                    ddlFromBranch.DataTextField = "Name";
                    ddlFromBranch.DataValueField = "Id";
                    ddlFromBranch.DataBind();

                    ddlTDist.DataSource = disList;
                    ddlTDist.DataTextField = "Name";
                    ddlTDist.DataValueField = "Id";
                    ddlTDist.DataBind();

                    ddlToBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlTDist.SelectedValue));
                    ddlToBranch.DataTextField = "Name";
                    ddlToBranch.DataValueField = "Id";
                    ddlToBranch.DataBind();
                }
            }
        }

        protected void gvRejoin_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string FromDistrictId = gvRejoin.DataKeys[e.NewEditIndex]["SourceDistrictId"].ToString();
            string FromBranchId = gvRejoin.DataKeys[e.NewEditIndex]["SourceBranchId"].ToString();
            string ToDistrictId = gvRejoin.DataKeys[e.NewEditIndex]["DestinationDistrictId"].ToString();
            string ToBranchId = gvRejoin.DataKeys[e.NewEditIndex]["DestinationBranchId"].ToString();
            string LeaveTypeId = gvRejoin.DataKeys[e.NewEditIndex]["LeaveTypeId"].ToString();
            string RejoinTypeId = gvRejoin.DataKeys[e.NewEditIndex]["RejoinTypeId"].ToString();
            gvRejoin.EditIndex = e.NewEditIndex;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadEmployeeRejoin(Convert.ToInt32(hdnid.Value));
            DropDownList ddlFDist = (DropDownList)gvRejoin.Rows[e.NewEditIndex].FindControl("ddlRejoinFromDistrict");
            DropDownList ddlTDist = (DropDownList)gvRejoin.Rows[e.NewEditIndex].FindControl("ddlRejoinToDistrict");
            DropDownList ddlFromBranch = (DropDownList)gvRejoin.Rows[e.NewEditIndex].FindControl("ddlRejoinFromBranch");
            DropDownList ddlToBranch = (DropDownList)gvRejoin.Rows[e.NewEditIndex].FindControl("ddlRejoinToBranch");
            DropDownList ddlLeaveType = (DropDownList)gvRejoin.Rows[e.NewEditIndex].FindControl("ddlRejoinLeaveType");
            DropDownList ddlRejoinType = (DropDownList)gvRejoin.Rows[e.NewEditIndex].FindControl("ddlRejoinType");
            if (ddlFDist != null)
            {
                UIUtility.LoadEnums(ddlLeaveType, typeof(H_EmployeeLeave.Types), false, false, true);
                ddlLeaveType.SelectedValue = LeaveTypeId;
                UIUtility.LoadEnums(ddlRejoinType, typeof(H_Employee.EmploymentTypes), false, false, true);
                ddlRejoinType.SelectedValue = RejoinTypeId;

                IList<Subzone> disList = Subzone.FindAll("Name");
                ddlFDist.DataSource = disList;
                ddlFDist.DataTextField = "Name";
                ddlFDist.DataValueField = "Id";
                ddlFDist.DataBind();
                ddlFDist.SelectedValue = FromDistrictId;

                ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(FromDistrictId));
                ddlFromBranch.DataTextField = "Name";
                ddlFromBranch.DataValueField = "Id";
                ddlFromBranch.DataBind();
                ddlFromBranch.SelectedValue = FromBranchId;

                ddlTDist.DataSource = disList;
                ddlTDist.DataTextField = "Name";
                ddlTDist.DataValueField = "Id";
                ddlTDist.DataBind();
                ddlTDist.SelectedValue = ToDistrictId;

                ddlToBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ToDistrictId));
                ddlToBranch.DataTextField = "Name";
                ddlToBranch.DataValueField = "Id";
                ddlToBranch.DataBind();
                ddlToBranch.SelectedValue = ToBranchId;
            }
            gvRejoin.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvRejoin_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRejoin.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadEmployeeRejoin(Convert.ToInt32(hdnid.Value));
        }

        protected void ddlRejoinFromDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FromDistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvRejoin.EditIndex;
            DropDownList ddlFromBranch = (DropDownList)gvRejoin.Rows[rowindex].FindControl("ddlRejoinFromBranch");
            if (ddlFromBranch != null)
            {
                ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(FromDistrictId));
                ddlFromBranch.DataTextField = "Name";
                ddlFromBranch.DataValueField = "Id";
                ddlFromBranch.DataBind();
            }
        }
        protected void ddlRejoinFromDistrictAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FromDistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvRejoin.EditIndex;
            DropDownList ddlFromBranch = (DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinFromBranchAdd");
            if (ddlFromBranch != null)
            {
                ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(FromDistrictId));
                ddlFromBranch.DataTextField = "Name";
                ddlFromBranch.DataValueField = "Id";
                ddlFromBranch.DataBind();
            }
        }

        protected void ddlRejoinToDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FromDistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvRejoin.EditIndex;
            DropDownList ddlToBranch = (DropDownList)gvRejoin.Rows[rowindex].FindControl("ddlRejoinToBranch");
            if (ddlToBranch != null)
            {
                ddlToBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(FromDistrictId));
                ddlToBranch.DataTextField = "Name";
                ddlToBranch.DataValueField = "Id";
                ddlToBranch.DataBind();
            }
        }
        protected void ddlRejoinToDistrictAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FromDistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvRejoin.EditIndex;
            DropDownList ddlToBranch = (DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinToBranchAdd");
            if (ddlToBranch != null)
            {
                ddlToBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(FromDistrictId));
                ddlToBranch.DataTextField = "Name";
                ddlToBranch.DataValueField = "Id";
                ddlToBranch.DataBind();
            }
        }

        protected void gvRejoin_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string h_RejoinId = gvRejoin.DataKeys[e.RowIndex]["Id"].ToString();
            TransactionManager tm = new TransactionManager(true);
            H_EmployeeRejoin h_Rejoin = H_EmployeeRejoin.GetById(Convert.ToInt32(h_RejoinId));
            h_Rejoin.LetterNo = ((TextBox)gvRejoin.Rows[e.RowIndex].FindControl("txtLetterNo")).Text;
            h_Rejoin.LetterDate = DBUtility.ToDateTime(((TextBox)gvRejoin.Rows[e.RowIndex].FindControl("txtLetterDate")).Text);
            h_Rejoin.FromDate = DBUtility.ToDateTime(((TextBox)gvRejoin.Rows[e.RowIndex].FindControl("txtFromDate")).Text);
            h_Rejoin.RejoinDate = DBUtility.ToDateTime(((TextBox)gvRejoin.Rows[e.RowIndex].FindControl("txtRejoinDate")).Text);
            h_Rejoin.SourceBranchId = Convert.ToInt32(((DropDownList)gvRejoin.Rows[e.RowIndex].FindControl("ddlRejoinFromBranch")).SelectedValue);
            h_Rejoin.DestinationBranchId = Convert.ToInt32(((DropDownList)gvRejoin.Rows[e.RowIndex].FindControl("ddlRejoinToBranch")).SelectedValue);
            h_Rejoin.LeaveType = (H_EmployeeLeave.Types)DBUtility.ToInt32(((DropDownList)gvRejoin.Rows[e.RowIndex].FindControl("ddlRejoinLeaveType")).SelectedValue);
            h_Rejoin.RejoinType = (H_Employee.EmploymentTypes)DBUtility.ToInt32(((DropDownList)gvRejoin.Rows[e.RowIndex].FindControl("ddlRejoinType")).SelectedValue);

            H_EmployeeRejoin.Update(tm, h_Rejoin);
            tm.Commit();
            gvRejoin.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadEmployeeRejoin(Convert.ToInt32(hdnid.Value));
        }

        protected void gvRejoin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 h_RejoinId = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeeRejoin.Delete(tm, h_RejoinId);
                tm.Commit();
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadEmployeeRejoin(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                DateTime temp;
                Int32 amount;
                if (string.IsNullOrEmpty(((TextBox)gvRejoin.FooterRow.FindControl("txtLetterNoAdd")).Text))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter Letter No";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvRejoin.FooterRow.FindControl("txtLetterDateAdd")).Text) || !DateTime.TryParse(((TextBox)gvRejoin.FooterRow.FindControl("txtLetterDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Letter Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvRejoin.FooterRow.FindControl("txtFromDateAdd")).Text) || !DateTime.TryParse(((TextBox)gvRejoin.FooterRow.FindControl("txtFromDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid From Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvRejoin.FooterRow.FindControl("txtRejoinDateAdd")).Text) || !DateTime.TryParse(((TextBox)gvRejoin.FooterRow.FindControl("txtRejoinDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Rejoin Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                

                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                TransactionManager tm = new TransactionManager(true);

                H_EmployeeRejoin h_Rejoin = new H_EmployeeRejoin();
                h_Rejoin.H_EmployeeId = Convert.ToInt32(hdnid.Value);
                h_Rejoin.LetterNo = ((TextBox)gvRejoin.FooterRow.FindControl("txtLetterNoAdd")).Text;
                h_Rejoin.LetterDate = DBUtility.ToDateTime(((TextBox)gvRejoin.FooterRow.FindControl("txtLetterDateAdd")).Text);
                h_Rejoin.FromDate = DBUtility.ToDateTime(((TextBox)gvRejoin.FooterRow.FindControl("txtFromDateAdd")).Text);
                h_Rejoin.RejoinDate = DBUtility.ToDateTime(((TextBox)gvRejoin.FooterRow.FindControl("txtRejoinDateAdd")).Text);
                h_Rejoin.SourceBranchId = Convert.ToInt32(((DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinFromBranchAdd")).SelectedValue);
                h_Rejoin.DestinationBranchId = Convert.ToInt32(((DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinToBranchAdd")).SelectedValue);
                h_Rejoin.LeaveType = (H_EmployeeLeave.Types)DBUtility.ToInt32(((DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinLeaveTypeAdd")).SelectedValue);
                h_Rejoin.RejoinType = (H_Employee.EmploymentTypes)DBUtility.ToInt32(((DropDownList)gvRejoin.FooterRow.FindControl("ddlRejoinTypeAdd")).SelectedValue);

                H_EmployeeRejoin.Insert(tm, h_Rejoin);
                tm.Commit();

                LoadEmployeeRejoin(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void btnProtionEdit_Click(object sender, EventArgs e)
        {
            pnlIncHeldup.Visible = false;
            pnlGrade.Visible = false;
            pnlBranch.Visible = false;
            pnlTransfer.Visible = false;
            pnlLeave.Visible = false;
            pnlPenalty.Visible = false;
            pnlWarning.Visible = false;
            pnlDesignation.Visible = false;
            pnlPromotion.Visible = false;
            pnlConsultancy.Visible = false;
            pnlDropout.Visible = false;
            pnlRejoin.Visible = false;
            pnlTransferEdit.Visible = false;
            pnlPromoEdit.Visible = true;
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (!HasEditPermission(h_Employee.Id))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Information;
                    msg.Msg = "You have no Update Permission for this Employee";
                    ShowUIMessage(msg);
                    return;
                }
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                Session.Remove("PromotionEdit");
                LoadPromotionEdit(h_Employee.Id);

            }
        }

        private void LoadPromotionEdit(int h_EmployeeId)
        {
            DataTable dt = new DataTable();
            if (Session["PromotionEdit"] != null && ((DataTable)Session["PromotionEdit"]).Rows.Count > 0)
            {
                dt = (DataTable)Session["PromotionEdit"];
                gvPromotionEdit.DataSource = dt;
                gvPromotionEdit.DataBind();
                gvPromotionEdit.Rows[0].Visible = true;
            }
            else
            {
                dt = GetPromotionData(h_EmployeeId);
                if (dt.Rows.Count > 0)
                {
                    Session["PromotionEdit"] = dt;
                    gvPromotionEdit.DataSource = dt;
                    gvPromotionEdit.DataBind();
                    gvPromotionEdit.Rows[0].Visible = true;
                }
                else
                {
                    Session["PromotionEdit"] = GetEmptyDataTable();
                }
            }

            if (dt == null || dt.Rows.Count == 0)
            {
                dt = GetEmptyDataTable();
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                gvPromotionEdit.DataSource = dt;
                gvPromotionEdit.DataBind();
                gvPromotionEdit.Rows[0].Visible = false;
            }

            DropDownList ddlOG = (DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlOldGradeAddE");
            DropDownList ddlOD = (DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlOldDesignationAdd");
            DropDownList ddlNG = (DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlNewGradeAddE");
            DropDownList ddlND = (DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlNewDesignationAdd");
            DropDownList ddlTypeAdd = (DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlTypeAdd");
            if (ddlOG != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlOG.DataSource = disList;
                ddlOG.DataTextField = "Name";
                ddlOG.DataValueField = "Id";
                ddlOG.DataBind();

                ddlOD.DataSource = GetDesignationByGradeId(Convert.ToInt32(ddlOG.SelectedValue));
                ddlOD.DataTextField = "Name";
                ddlOD.DataValueField = "Id";
                ddlOD.DataBind();
            }
            if (ddlNG != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlNG.DataSource = disList;
                ddlNG.DataTextField = "Name";
                ddlNG.DataValueField = "Id";
                ddlNG.DataBind();

                ddlND.DataSource = GetDesignationByGradeId(Convert.ToInt32(ddlNG.SelectedValue));
                ddlND.DataTextField = "Name";
                ddlND.DataValueField = "Id";
                ddlND.DataBind();
            }
            UIUtility.LoadEnums(ddlTypeAdd, typeof(H_EmployeePromotion.Types), false, false, false);
        }

        protected void ddlOldGradeE_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GradeId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvPromotionEdit.EditIndex;
            DropDownList ddlDesg = (DropDownList)gvPromotionEdit.Rows[rowindex].FindControl("ddlOldDesignation");
            if (ddlDesg != null)
            {
                ddlDesg.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlDesg.DataTextField = "Name";
                ddlDesg.DataValueField = "Id";
                ddlDesg.DataBind();
            }
        }

        protected void ddlNewGradeE_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GradeId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvPromotionEdit.EditIndex;
            DropDownList ddlDesg = (DropDownList)gvPromotionEdit.Rows[rowindex].FindControl("ddlNewDesignation");
            if (ddlDesg != null)
            {
                ddlDesg.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlDesg.DataTextField = "Name";
                ddlDesg.DataValueField = "Id";
                ddlDesg.DataBind();
            }
        }

        protected void ddlOldGradeAddE_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GradeId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlDesgAdd = (DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlOldDesignationAdd");
            if (ddlDesgAdd != null)
            {
                ddlDesgAdd.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlDesgAdd.DataTextField = "Name";
                ddlDesgAdd.DataValueField = "Id";
                ddlDesgAdd.DataBind();
            }
        }

        protected void ddlNewGradeAddE_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GradeId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlNewDesgAdd = (DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlNewDesignationAdd");
            if (ddlNewDesgAdd != null)
            {
                ddlNewDesgAdd.DataSource = GetDesignationByGradeId(Convert.ToInt32(GradeId));
                ddlNewDesgAdd.DataTextField = "Name";
                ddlNewDesgAdd.DataValueField = "Id";
                ddlNewDesgAdd.DataBind();
            }
        }

        protected void gvPromotionEdit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPromotionEdit.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadPromotionEdit(Convert.ToInt32(hdnid.Value));
        }

        protected void gvPromotionEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                //TransactionManager tm = new TransactionManager(true);
                //H_EmployeePromotion.Delete(tm, Id);
                //tm.Commit();
                DataTable dt = (DataTable)Session["PromotionEdit"];
                dt.Rows[rowIndex].Delete();
                dt.AcceptChanges();
                dt.DefaultView.Sort = "PromotionDate";
                dt = dt.DefaultView.ToTable();
                Session["PromotionEdit"] = dt;
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadPromotionEdit(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                if (string.IsNullOrEmpty(((TextBox)gvPromotionEdit.FooterRow.FindControl("txtLetterNoAdd")).Text))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter Letter No";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvPromotionEdit.FooterRow.FindControl("txtLetterDateAdd")).Text))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Letter Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                DataTable dt = (DataTable)Session["PromotionEdit"];
                DataRow dr = dt.NewRow();

                dr["LetterNo"] = ((TextBox)gvPromotionEdit.FooterRow.FindControl("txtLetterNoAdd")).Text;
                dr["LetterDate"] = DBUtility.ToDateTime(((TextBox)gvPromotionEdit.FooterRow.FindControl("txtLetterDateAdd")).Text);
                dr["PromotionDate"] = DBUtility.ToDateTime(((TextBox)gvPromotionEdit.FooterRow.FindControl("txtPromotionDateAdd")).Text);
                dr["OldGradeId"] = Convert.ToInt32(((DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlOldGradeAddE")).SelectedValue);
                dr["OldGradeName"] = ((DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlOldGradeAddE")).SelectedItem;
                dr["OldDesignationId"] = Convert.ToInt32(((DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlOldDesignationAdd")).SelectedValue);
                dr["OldDesignationName"] = ((DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlOldDesignationAdd")).SelectedItem;
                dr["NewGradeId"] = Convert.ToInt32(((DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlNewGradeAddE")).SelectedValue);
                dr["NewGradeName"] = ((DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlNewGradeAddE")).SelectedItem;
                dr["NewDesignationId"] = Convert.ToInt32(((DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlNewDesignationAdd")).SelectedValue);
                dr["NewDesignationName"] = ((DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlNewDesignationAdd")).SelectedItem;
                dr["Type"] = (H_EmployeePromotion.Types)Convert.ToInt32(((DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlTypeAdd")).SelectedValue);
                dr["TypeName"] = ((DropDownList)gvPromotionEdit.FooterRow.FindControl("ddlTypeAdd")).SelectedItem;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                dt.DefaultView.Sort = "PromotionDate";
                dt = dt.DefaultView.ToTable();
                Session["PromotionEdit"] = dt;
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                LoadPromotionEdit(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void gvPromotionEdit_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string OldGradeId = gvPromotionEdit.DataKeys[e.NewEditIndex]["OldGradeId"].ToString();
            string OldDesignationId = gvPromotionEdit.DataKeys[e.NewEditIndex]["OldDesignationId"].ToString();
            string NewGradeId = gvPromotionEdit.DataKeys[e.NewEditIndex]["NewGradeId"].ToString();
            string NewDesignationId = gvPromotionEdit.DataKeys[e.NewEditIndex]["NewDesignationId"].ToString();
            string NewType = gvPromotionEdit.DataKeys[e.NewEditIndex]["Type"].ToString();
            gvPromotionEdit.EditIndex = e.NewEditIndex;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadPromotionEdit(Convert.ToInt32(hdnid.Value));
            DropDownList ddlOG = (DropDownList)gvPromotionEdit.Rows[e.NewEditIndex].FindControl("ddlOldGradeE");
            DropDownList ddlOD = (DropDownList)gvPromotionEdit.Rows[e.NewEditIndex].FindControl("ddlOldDesignation");
            DropDownList ddlNG = (DropDownList)gvPromotionEdit.Rows[e.NewEditIndex].FindControl("ddlNewGradeE");
            DropDownList ddlND = (DropDownList)gvPromotionEdit.Rows[e.NewEditIndex].FindControl("ddlNewDesignation");
            DropDownList ddlType = (DropDownList)gvPromotionEdit.Rows[e.NewEditIndex].FindControl("ddlType");
            if (ddlOG != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlOG.DataSource = disList;
                ddlOG.DataTextField = "Name";
                ddlOG.DataValueField = "Id";
                ddlOG.DataBind();
                ddlOG.SelectedValue = OldGradeId;

                ddlOD.DataSource = GetDesignationByGradeId(Convert.ToInt32(OldGradeId));
                ddlOD.DataTextField = "Name";
                ddlOD.DataValueField = "Id";
                ddlOD.DataBind();
                ddlOD.SelectedValue = OldDesignationId;
            }
            if (ddlNG != null)
            {
                IList<H_Grade> disList = H_Grade.FindAll("Name");
                ddlNG.DataSource = disList;
                ddlNG.DataTextField = "Name";
                ddlNG.DataValueField = "Id";
                ddlNG.DataBind();
                ddlNG.SelectedValue = NewGradeId;

                ddlND.DataSource = GetDesignationByGradeId(Convert.ToInt32(NewGradeId));
                ddlND.DataTextField = "Name";
                ddlND.DataValueField = "Id";
                ddlND.DataBind();
                ddlND.SelectedValue = NewDesignationId;
            }
            UIUtility.LoadEnums(ddlType, typeof(H_EmployeePromotion.Types), false, false, true);
            ddlType.SelectedValue = NewType;
            gvPromotionEdit.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvPromotionEdit_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int roIndex = e.RowIndex;
            DataTable dt = (DataTable)Session["PromotionEdit"];
            DataRow dr = dt.Rows[roIndex];
            dr["LetterNo"] = ((TextBox)gvPromotionEdit.Rows[e.RowIndex].FindControl("txtLetterNo")).Text;
            dr["LetterDate"] = DBUtility.ToDateTime(((TextBox)gvPromotionEdit.Rows[e.RowIndex].FindControl("txtLetterDate")).Text);
            dr["PromotionDate"] = DBUtility.ToDateTime(((TextBox)gvPromotionEdit.Rows[e.RowIndex].FindControl("txtPromotionDate")).Text);
            dr["OldGradeId"] = Convert.ToInt32(((DropDownList)gvPromotionEdit.Rows[e.RowIndex].FindControl("ddlOldGradeE")).SelectedValue);
            dr["OldGradeName"] = ((DropDownList)gvPromotionEdit.Rows[e.RowIndex].FindControl("ddlOldGradeE")).SelectedItem;
            dr["OldDesignationId"] = Convert.ToInt32(((DropDownList)gvPromotionEdit.Rows[e.RowIndex].FindControl("ddlOldDesignation")).SelectedValue);
            dr["OldDesignationName"] = ((DropDownList)gvPromotionEdit.Rows[e.RowIndex].FindControl("ddlOldDesignation")).SelectedItem;
            dr["NewGradeId"] = Convert.ToInt32(((DropDownList)gvPromotionEdit.Rows[e.RowIndex].FindControl("ddlNewGradeE")).SelectedValue);
            dr["NewGradeName"] = ((DropDownList)gvPromotionEdit.Rows[e.RowIndex].FindControl("ddlNewGradeE")).SelectedItem;
            dr["NewDesignationId"] = Convert.ToInt32(((DropDownList)gvPromotionEdit.Rows[e.RowIndex].FindControl("ddlNewDesignation")).SelectedValue);
            dr["NewDesignationName"] = ((DropDownList)gvPromotionEdit.Rows[e.RowIndex].FindControl("ddlNewDesignation")).SelectedItem;
            dr["Type"] = (H_EmployeePromotion.Types)Convert.ToInt32(((DropDownList)gvPromotionEdit.Rows[e.RowIndex].FindControl("ddlType")).SelectedValue);
            dr["TypeName"] = ((DropDownList)gvPromotionEdit.Rows[e.RowIndex].FindControl("ddlType")).SelectedItem;
            dt.AcceptChanges();
            dt.DefaultView.Sort = "PromotionDate";
            dt = dt.DefaultView.ToTable();
            Session["PromotionEdit"] = dt;
            gvPromotionEdit.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            LoadPromotionEdit(Convert.ToInt32(hdnid.Value));
        }

        protected override void LoadData()
        {

        }

        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        protected override Message Save()
        {
            throw new NotImplementedException();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Data Updated successfully";
            if (Session["PromotionEdit"] != null && ((DataTable)Session["PromotionEdit"]).Rows.Count > 0)
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                DataTable dt = (DataTable)Session["PromotionEdit"];
                int lastRowIndex = dt.Rows.Count - 1;

                IList<H_EmployeeDesignation> desigList = H_EmployeeDesignation.Find("H_EmployeeId=" + Convert.ToInt32(hdnid.Value), "");
                IList<H_EmployeeGrade> gradeList = H_EmployeeGrade.Find("H_EmployeeId=" + Convert.ToInt32(hdnid.Value), "");
                IList<H_EmployeePromotion> promoList = H_EmployeePromotion.Find("H_EmployeeId=" + Convert.ToInt32(hdnid.Value), " LetterDate");
                H_Employee employee = H_Employee.GetById(Convert.ToInt32(hdnid.Value));
                this.TransactionManager = new TransactionManager(true, "Update [Promotion]");
                try
                {
                    foreach (H_EmployeeGrade eg in gradeList)
                    {
                        H_EmployeeGrade.Delete(this.TransactionManager, eg);
                    }
                    foreach (H_EmployeeDesignation ed in desigList)
                    {
                        H_EmployeeDesignation.Delete(this.TransactionManager, ed);
                    }
                    foreach (H_EmployeePromotion promotion in promoList)
                    {
                        H_EmployeePromotion.Delete(this.TransactionManager, promotion);
                    }

                    DateTime GradeStartDate = employee.JoiningDate.Value;
                    DateTime DesignationStartdate = employee.JoiningDate.Value;

                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        //promotion table Insert
                        H_EmployeePromotion promotion = new H_EmployeePromotion();
                        promotion.H_EmployeeId = employee.Id;
                        promotion.LetterNo = dt.Rows[row][1].ToString();
                        promotion.LetterDate = DBUtility.ToDateTime(dt.Rows[row][2]);
                        promotion.PromotionDate = DBUtility.ToDateTime(dt.Rows[row][3]);
                        promotion.OldH_GradeId = Convert.ToInt32(dt.Rows[row][4]);
                        promotion.NewH_GradeId = Convert.ToInt32(dt.Rows[row][6]);
                        promotion.OldH_DesignationId = Convert.ToInt32(dt.Rows[row][8]);
                        promotion.NewH_DesignationId = Convert.ToInt32(dt.Rows[row][10]);
                        promotion.Type = (H_EmployeePromotion.Types)Convert.ToInt32(dt.Rows[row][12]);
                        H_EmployeePromotion.Insert(TransactionManager, promotion);

                        //Employee Grade Table Insert
                        if (Convert.ToInt32(dt.Rows[row][4]) != Convert.ToInt32(dt.Rows[row][6])) // if not Designation Change
                        {

                            H_EmployeeGrade grade = new H_EmployeeGrade();
                            grade.H_EmployeeId = employee.Id;
                            grade.H_GradeId = Convert.ToInt32(dt.Rows[row][4]);
                            grade.StartDate = GradeStartDate;// row == 0 ? employee.JoiningDate.Value : GradeDate;// DBUtility.ToDateTime(dt.Rows[row - 1][3]);
                            grade.EndDate = DBUtility.ToDateTime(dt.Rows[row][3]).AddDays(-1);
                            H_EmployeeGrade.Insert(TransactionManager, grade);

                            GradeStartDate = DBUtility.ToDateTime(dt.Rows[row][3]);
                            if (row == lastRowIndex)
                            {
                                H_EmployeeGrade lastGrade = new H_EmployeeGrade();
                                lastGrade.H_EmployeeId = employee.Id;
                                lastGrade.H_GradeId = Convert.ToInt32(dt.Rows[row][6]);
                                lastGrade.StartDate = DBUtility.ToDateTime(dt.Rows[row][3]);
                                lastGrade.EndDate = new DateTime(2099, 12, 31);
                                H_EmployeeGrade.Insert(TransactionManager, lastGrade);
                            }

                        }
                        else
                        {
                            if (row == lastRowIndex)
                            {
                                H_EmployeeGrade lastGrade = new H_EmployeeGrade();
                                lastGrade.H_EmployeeId = employee.Id;
                                lastGrade.H_GradeId = Convert.ToInt32(dt.Rows[row][6]);
                                lastGrade.StartDate = GradeStartDate;
                                lastGrade.EndDate = new DateTime(2099, 12, 31);
                                H_EmployeeGrade.Insert(TransactionManager, lastGrade);
                            }
                        }
                        //Employee Designation Table Insert
                        if (Convert.ToInt32(dt.Rows[row][8]) != Convert.ToInt32(dt.Rows[row][10])) // if Designation Change
                        {
                            H_EmployeeDesignation designation = new H_EmployeeDesignation();
                            designation.H_EmployeeId = employee.Id;
                            designation.H_DesignationId = Convert.ToInt32(dt.Rows[row][8]);
                            designation.StartDate = DesignationStartdate;// row == 0 ? employee.JoiningDate.Value : Desigdate;// DBUtility.ToDateTime(dt.Rows[row - 1][3]);
                            designation.EndDate = DBUtility.ToDateTime(dt.Rows[row][3]).AddDays(-1);
                            H_EmployeeDesignation.Insert(TransactionManager, designation);

                            DesignationStartdate = DBUtility.ToDateTime(dt.Rows[row][3]); //1.1.2001=1.1.2002=1.12003
                            if (row == lastRowIndex) // if Designation Change and Present Designation
                            {
                                H_EmployeeDesignation lastDesignation = new H_EmployeeDesignation();
                                lastDesignation.H_EmployeeId = employee.Id;
                                lastDesignation.H_DesignationId = Convert.ToInt32(dt.Rows[row][10]);
                                lastDesignation.StartDate = DBUtility.ToDateTime(dt.Rows[row][3]);
                                lastDesignation.EndDate = new DateTime(2099, 12, 31);
                                H_EmployeeDesignation.Insert(TransactionManager, lastDesignation);
                            }
                        }
                        else //If Designation Not Changed
                        {
                            if (row == lastRowIndex) // if Designation not Change and Present Designation
                            {
                                H_EmployeeDesignation lastDesignation = new H_EmployeeDesignation();
                                lastDesignation.H_EmployeeId = employee.Id;
                                lastDesignation.H_DesignationId = Convert.ToInt32(dt.Rows[row][10]);
                                lastDesignation.StartDate = DesignationStartdate;// DBUtility.ToDateTime(dt.Rows[row][3]);
                                lastDesignation.EndDate = new DateTime(2099, 12, 31);
                                H_EmployeeDesignation.Insert(TransactionManager, lastDesignation);
                            }
                        }

                    }
                    TransactionManager.Commit();
                }
                catch (Exception ex)
                {
                    msg.Type = MessageType.Error;
                    msg.Msg = "Update Failed. " + ex.Message;
                }

            }
            else
            {
                msg.Msg = "No Data to be Saved";
                msg.Type = MessageType.Error;

            }
            ShowUIMessage(msg);
        }
        private DataTable GetEmptyDataTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Id", typeof(int)); tbl.Columns.Add("LetterNo", typeof(string)); tbl.Columns.Add("LetterDate", typeof(DateTime));
            tbl.Columns.Add("PromotionDate", typeof(DateTime)); tbl.Columns.Add("OldGradeId", typeof(int)); tbl.Columns.Add("OldGradeName", typeof(string));
            tbl.Columns.Add("NewGradeId", typeof(int)); tbl.Columns.Add("NewGradeName", typeof(string)); tbl.Columns.Add("OldDesignationId", typeof(int));
            tbl.Columns.Add("OldDesignationName", typeof(string)); tbl.Columns.Add("NewDesignationId", typeof(int)); tbl.Columns.Add("NewDesignationName", typeof(string));
            tbl.Columns.Add("Type", typeof(int)); tbl.Columns.Add("TypeName", typeof(string));

            return tbl;
        }
        private DataTable GetPromotionData(Int32 h_EmployeeId)
        {
            IList<H_EmployeePromotion> list = H_EmployeePromotion.Find("H_EmployeeId=" + h_EmployeeId, " LetterDate");
            IList<H_Grade> grade = H_Grade.FindAll();
            IList<H_Designation> desg = H_Designation.FindAll();
            var gList = (from t in list
                         join og in grade on t.OldH_GradeId equals og.Id
                         join ng in grade on t.NewH_GradeId equals ng.Id
                         join od in desg on t.OldH_DesignationId equals od.Id
                         join nd in desg on t.NewH_DesignationId equals nd.Id
                         select new
                         {
                             t.Id,
                             t.LetterNo,
                             t.LetterDate,
                             t.PromotionDate,
                             OldGradeId = og.Id,
                             OldGradeName = og.Name,
                             NewGradeId = ng.Id,
                             NewGradeName = ng.Name,
                             OldDesignationId = od.Id,
                             OldDesignationName = od.Name,
                             NewDesignationId = nd.Id,
                             NewDesignationName = nd.Name,
                             t.Type,
                             TypeName = ((H_EmployeePromotion.Types)Enum.ToObject(typeof(H_EmployeePromotion.Types), (Int32)t.Type)).ToString()
                         }).OrderBy(p => p.PromotionDate);
            return UIUtility.LINQToDataTable(gList);
        }

        protected void btnTransferEdit_Click(object sender, EventArgs e)
        {
            pnlIncHeldup.Visible = false;
            pnlGrade.Visible = false;
            pnlWarning.Visible = false;
            pnlPenalty.Visible = false;
            pnlBranch.Visible = false;
            pnlTransfer.Visible = false;
            pnlLeave.Visible = false;
            pnlDesignation.Visible = false;
            pnlPromotion.Visible = false;
            pnlConsultancy.Visible = false;
            pnlDropout.Visible = false;
            pnlRejoin.Visible = false;
            pnlPromoEdit.Visible = false;
            pnlTransferEdit.Visible = true;
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (!HasEditPermission(h_Employee.Id))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Information;
                    msg.Msg = "You have no Update Permission for this Employee";
                    ShowUIMessage(msg);
                    return;
                }
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                if (Session["EmployeeTransferInfo"] != null)
                {
                    Session.Remove("EmployeeTransferInfo");
                }
                Load_gvTransferEdit(h_Employee.Id);
            }
        }

        protected void gvTransferEdit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                DataTable dt = (DataTable)Session["EmployeeTransferInfo"];
                dt.Rows[rowIndex].Delete();
                dt.AcceptChanges();
                dt.DefaultView.Sort = "JoiningDate";
                dt = dt.DefaultView.ToTable();
                Session["EmployeeTransferInfo"] = dt;
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                Load_gvTransferEdit(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                if (string.IsNullOrEmpty(((TextBox)gvTransferEdit.FooterRow.FindControl("txtLetterNoAdd")).Text))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter Letter No";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvTransferEdit.FooterRow.FindControl("txtLetterDateAdd")).Text))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Letter Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvTransferEdit.FooterRow.FindControl("txtJoiningDateAdd")).Text))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Transfer Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                DataTable dt = (DataTable)Session["EmployeeTransferInfo"];
                DataRow dr = dt.NewRow();

                dr["LetterNo"] = ((TextBox)gvTransferEdit.FooterRow.FindControl("txtLetterNoAdd")).Text;
                dr["LetterDate"] = DBUtility.ToDateTime(((TextBox)gvTransferEdit.FooterRow.FindControl("txtLetterDateAdd")).Text);
                dr["JoiningDate"] = DBUtility.ToDateTime(((TextBox)gvTransferEdit.FooterRow.FindControl("txtJoiningDateAdd")).Text);
                dr["SourceBranchId"] = Convert.ToInt32(((DropDownList)gvTransferEdit.FooterRow.FindControl("ddlFromBranchAdd")).SelectedValue);
                dr["DestinationBranchId"] = Convert.ToInt32(((DropDownList)gvTransferEdit.FooterRow.FindControl("ddlToBranchAdd")).SelectedValue);
                dr["SourceBranchName"] = ((DropDownList)gvTransferEdit.FooterRow.FindControl("ddlFromBranchAdd")).SelectedItem;
                dr["DestinationBranchName"] = ((DropDownList)gvTransferEdit.FooterRow.FindControl("ddlToBranchAdd")).SelectedItem;
                dr["SourceDistrictId"] = Convert.ToInt32(((DropDownList)gvTransferEdit.FooterRow.FindControl("ddlFromDistrictAdd")).SelectedValue);
                dr["DestinationDistrictId"] = Convert.ToInt32(((DropDownList)gvTransferEdit.FooterRow.FindControl("ddlToDistrictAdd")).SelectedValue);
                dr["SourceDistrictName"] = ((DropDownList)gvTransferEdit.FooterRow.FindControl("ddlFromDistrictAdd")).SelectedItem;
                dr["DestinationDistrictName"] = ((DropDownList)gvTransferEdit.FooterRow.FindControl("ddlToDistrictAdd")).SelectedItem;
                dr["Type"] = (H_EmployeeTransfer.Types)Convert.ToInt32(((DropDownList)gvTransferEdit.FooterRow.FindControl("ddlTypeAdd")).SelectedValue);
                dr["TypeName"] = ((DropDownList)gvTransferEdit.FooterRow.FindControl("ddlTypeAdd")).SelectedItem;
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                dt.DefaultView.Sort = "JoiningDate";
                dt = dt.DefaultView.ToTable();
                Session["EmployeeTransferInfo"] = dt;
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                Load_gvTransferEdit(Convert.ToInt32(hdnid.Value));
            }
        }

        protected void gvTransferEdit_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string SourceBranchId = gvTransferEdit.DataKeys[e.NewEditIndex]["SourceBranchId"].ToString();
            string DestinationBranchId = gvTransferEdit.DataKeys[e.NewEditIndex]["DestinationBranchId"].ToString();
            string SourceDistrictId = gvTransferEdit.DataKeys[e.NewEditIndex]["SourceDistrictId"].ToString();
            string DestinationDistrictId = gvTransferEdit.DataKeys[e.NewEditIndex]["DestinationDistrictId"].ToString();
            string TypeId = gvTransferEdit.DataKeys[e.NewEditIndex]["Type"].ToString();
            gvTransferEdit.EditIndex = e.NewEditIndex;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            Load_gvTransferEdit(Convert.ToInt32(hdnid.Value));
            DropDownList ddlFromDistrict = (DropDownList)gvTransferEdit.Rows[e.NewEditIndex].FindControl("ddlFromDistrictEdit");
            DropDownList ddlFromBranch = (DropDownList)gvTransferEdit.Rows[e.NewEditIndex].FindControl("ddlFromBranch");
            DropDownList ddlNewDistrict = (DropDownList)gvTransferEdit.Rows[e.NewEditIndex].FindControl("ddlToDistrictEdit");
            DropDownList ddlNewBranch = (DropDownList)gvTransferEdit.Rows[e.NewEditIndex].FindControl("ddlToBranch");
            DropDownList ddlType = (DropDownList)gvTransferEdit.Rows[e.NewEditIndex].FindControl("ddlType");
            if (ddlFromDistrict != null)
            {
                IList<Subzone> disList = Subzone.FindAll("Name");
                ddlFromDistrict.DataSource = disList;
                ddlFromDistrict.DataTextField = "Name";
                ddlFromDistrict.DataValueField = "Id";
                ddlFromDistrict.DataBind();
                ddlFromDistrict.SelectedValue = SourceDistrictId;

                ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlFromDistrict.SelectedValue));
                ddlFromBranch.DataTextField = "Name";
                ddlFromBranch.DataValueField = "Id";
                ddlFromBranch.DataBind();
                ddlFromBranch.SelectedValue = SourceBranchId;
            }
            if (ddlNewDistrict != null)
            {
                IList<Subzone> disList = Subzone.FindAll("Name");
                ddlNewDistrict.DataSource = disList;
                ddlNewDistrict.DataTextField = "Name";
                ddlNewDistrict.DataValueField = "Id";
                ddlNewDistrict.DataBind();
                ddlNewDistrict.SelectedValue = DestinationDistrictId;

                ddlNewBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlNewDistrict.SelectedValue));
                ddlNewBranch.DataTextField = "Name";
                ddlNewBranch.DataValueField = "Id";
                ddlNewBranch.DataBind();
                ddlNewBranch.SelectedValue = DestinationBranchId;
            }
            UIUtility.LoadEnums(ddlType, typeof(H_EmployeeTransfer.Types), false, false, false);
            ddlType.SelectedValue = TypeId;
            gvTransferEdit.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvTransferEdit_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTransferEdit.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            Load_gvTransferEdit(Convert.ToInt32(hdnid.Value));
        }

        protected void gvTransferEdit_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataTable dt = (DataTable)Session["EmployeeTransferInfo"];
            DataRow dr = dt.Rows[rowIndex];
            dr["LetterNo"] = ((TextBox)gvTransferEdit.Rows[rowIndex].FindControl("txtLetterNo")).Text;
            dr["LetterDate"] = DBUtility.ToDateTime(((TextBox)gvTransferEdit.Rows[rowIndex].FindControl("txtLetterDate")).Text);
            dr["JoiningDate"] = DBUtility.ToDateTime(((TextBox)gvTransferEdit.Rows[rowIndex].FindControl("txtJoiningDate")).Text);
            dr["SourceBranchId"] = Convert.ToInt32(((DropDownList)gvTransferEdit.Rows[rowIndex].FindControl("ddlFromBranch")).SelectedValue);
            dr["DestinationBranchId"] = Convert.ToInt32(((DropDownList)gvTransferEdit.Rows[rowIndex].FindControl("ddlToBranch")).SelectedValue);
            dr["SourceBranchName"] = ((DropDownList)gvTransferEdit.Rows[rowIndex].FindControl("ddlFromBranch")).SelectedItem;
            dr["DestinationBranchName"] = ((DropDownList)gvTransferEdit.Rows[rowIndex].FindControl("ddlToBranch")).SelectedItem;
            dr["SourceDistrictId"] = Convert.ToInt32(((DropDownList)gvTransferEdit.Rows[rowIndex].FindControl("ddlFromDistrictEdit")).SelectedValue);
            dr["DestinationDistrictId"] = Convert.ToInt32(((DropDownList)gvTransferEdit.Rows[rowIndex].FindControl("ddlToDistrictEdit")).SelectedValue);
            dr["SourceDistrictName"] = ((DropDownList)gvTransferEdit.Rows[rowIndex].FindControl("ddlFromDistrictEdit")).SelectedItem;
            dr["DestinationDistrictName"] = ((DropDownList)gvTransferEdit.Rows[rowIndex].FindControl("ddlToDistrictEdit")).SelectedItem;
            dr["Type"] = (H_EmployeeTransfer.Types)Convert.ToInt32(((DropDownList)gvTransferEdit.Rows[rowIndex].FindControl("ddlType")).SelectedValue);
            dr["TypeName"] = ((DropDownList)gvTransferEdit.Rows[rowIndex].FindControl("ddlType")).SelectedItem;
            dt.AcceptChanges();
            dt.DefaultView.Sort = "JoiningDate";
            dt = dt.DefaultView.ToTable();
            Session["EmployeeTransferInfo"] = dt;
            gvTransferEdit.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            Load_gvTransferEdit(Convert.ToInt32(hdnid.Value));
        }
        protected void ddlFromDistrictEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FromDistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvTransferEdit.EditIndex;
            DropDownList ddlFromBranch = (DropDownList)gvTransferEdit.Rows[rowindex].FindControl("ddlFromBranch");
            if (ddlFromBranch != null)
            {
                ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(FromDistrictId));
                ddlFromBranch.DataTextField = "Name";
                ddlFromBranch.DataValueField = "Id";
                ddlFromBranch.DataBind();
            }
        }

        protected void ddlToDistrictEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ToDistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvTransferEdit.EditIndex;
            DropDownList ddlToBranch = (DropDownList)gvTransferEdit.Rows[rowindex].FindControl("ddlToBranch");
            if (ddlToBranch != null)
            {
                ddlToBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ToDistrictId));
                ddlToBranch.DataTextField = "Name";
                ddlToBranch.DataValueField = "Id";
                ddlToBranch.DataBind();
            }
        }
        protected void ddlFromDistrictAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ToDistrictId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlFromBranchAdd = (DropDownList)gvTransferEdit.FooterRow.FindControl("ddlFromBranchAdd");
            if (ddlFromBranchAdd != null)
            {
                ddlFromBranchAdd.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ToDistrictId));
                ddlFromBranchAdd.DataTextField = "Name";
                ddlFromBranchAdd.DataValueField = "Id";
                ddlFromBranchAdd.DataBind();
            }
        }
        protected void ddlToDistrictAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ToDistrictId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlToBranchAdd = (DropDownList)gvTransferEdit.FooterRow.FindControl("ddlToBranchAdd");
            if (ddlToBranchAdd != null)
            {
                ddlToBranchAdd.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ToDistrictId));
                ddlToBranchAdd.DataTextField = "Name";
                ddlToBranchAdd.DataValueField = "Id";
                ddlToBranchAdd.DataBind();
            }
        }

        private void Load_gvTransferEdit(Int32 h_EmployeeId)
        {
            DataTable dt = new DataTable();
            if (Session["EmployeeTransferInfo"] != null)
            {
                if (((DataTable)Session["EmployeeTransferInfo"]).Rows.Count > 0)
                {
                    dt = (DataTable)Session["EmployeeTransferInfo"];
                    gvTransferEdit.DataSource = dt;
                    gvTransferEdit.DataBind();
                }
                else
                {
                    DataTable tbl = GetEmptyTransferDataTable();
                    DataRow dr = tbl.NewRow();
                    tbl.Rows.Add(dr);
                    gvTransferEdit.DataSource = tbl;
                    gvTransferEdit.DataBind();
                    gvTransferEdit.Rows[0].Visible = false;
                }

            }
            else
            {
                dt = GetTransferInfoFromDataBase(h_EmployeeId);
                if (dt.Rows.Count == 0)
                {
                    Session["EmployeeTransferInfo"] = GetEmptyTransferDataTable();
                    dt = GetEmptyTransferDataTable();

                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                    gvTransferEdit.DataSource = dt;
                    gvTransferEdit.DataBind();
                    gvTransferEdit.Rows[0].Visible = false;
                }
                else
                {
                    Session["EmployeeTransferInfo"] = dt;
                    gvTransferEdit.DataSource = dt;
                    gvTransferEdit.DataBind();
                }
            }
            DropDownList ddlFromDistrict = (DropDownList)gvTransferEdit.FooterRow.FindControl("ddlFromDistrictAdd");
            DropDownList ddlFromBranch = (DropDownList)gvTransferEdit.FooterRow.FindControl("ddlFromBranchAdd");
            DropDownList ddlNewDistrict = (DropDownList)gvTransferEdit.FooterRow.FindControl("ddlToDistrictAdd");
            DropDownList ddlNewBranch = (DropDownList)gvTransferEdit.FooterRow.FindControl("ddlToBranchAdd");
            DropDownList ddlTypeAdd = (DropDownList)gvTransferEdit.FooterRow.FindControl("ddlTypeAdd");
            if (ddlFromDistrict != null)
            {
                IList<Subzone> disList = Subzone.FindAll("Name");
                ddlFromDistrict.DataSource = disList;
                ddlFromDistrict.DataTextField = "Name";
                ddlFromDistrict.DataValueField = "Id";
                ddlFromDistrict.DataBind();

                ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlFromDistrict.SelectedValue));
                ddlFromBranch.DataTextField = "Name";
                ddlFromBranch.DataValueField = "Id";
                ddlFromBranch.DataBind();
            }
            if (ddlNewDistrict != null)
            {
                IList<Subzone> disList = Subzone.FindAll("Name");
                ddlNewDistrict.DataSource = disList;
                ddlNewDistrict.DataTextField = "Name";
                ddlNewDistrict.DataValueField = "Id";
                ddlNewDistrict.DataBind();

                ddlNewBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlNewDistrict.SelectedValue));
                ddlNewBranch.DataTextField = "Name";
                ddlNewBranch.DataValueField = "Id";
                ddlNewBranch.DataBind();
            }
            UIUtility.LoadEnums(ddlTypeAdd, typeof(H_EmployeeTransfer.Types), false, false, false);

        }

        private DataTable GetEmptyTransferDataTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("LetterNo", typeof(string));
            tbl.Columns.Add("LetterDate", typeof(DateTime));
            tbl.Columns.Add("JoiningDate", typeof(DateTime));
            tbl.Columns.Add("SourceBranchId", typeof(int));
            tbl.Columns.Add("DestinationBranchId", typeof(int));
            tbl.Columns.Add("SourceDistrictId", typeof(int));
            tbl.Columns.Add("DestinationDistrictId", typeof(int));
            tbl.Columns.Add("SourceBranchName", typeof(string));
            tbl.Columns.Add("DestinationBranchName", typeof(string));
            tbl.Columns.Add("SourceDistrictName", typeof(string));
            tbl.Columns.Add("DestinationDistrictName", typeof(string));
            tbl.Columns.Add("Type", typeof(int));
            tbl.Columns.Add("TypeName", typeof(string));

            return tbl;
        }
        private DataTable GetTransferInfoFromDataBase(Int32 h_EmployeeId)
        {
            IList<H_EmployeeTransfer> list = H_EmployeeTransfer.Find("H_EmployeeId=" + h_EmployeeId, " LetterDate");
            IList<Branch> branch = Branch.FindAll();
            IList<Region> region = Region.FindAll();
            IList<Subzone> dist = Subzone.FindAll();
            var gList = (from t in list
                         join sb in branch on t.SourceBranchId equals sb.Id
                         join sr in region on sb.RegionId equals sr.Id
                         join sd in dist on sr.SubzoneId equals sd.Id
                         join db in branch on t.DestinationBranchId equals db.Id
                         join dr in region on db.RegionId equals dr.Id
                         join dd in dist on dr.SubzoneId equals dd.Id
                         select new
                         {
                             t.LetterNo,
                             t.LetterDate,
                             t.JoiningDate,
                             t.SourceBranchId,
                             t.DestinationBranchId,
                             SourceDistrictId = sd.Id,
                             DestinationDistrictId = dd.Id,
                             SourceBranchName = sb.Name,
                             DestinationBranchName = db.Name,
                             SourceDistrictName = sd.Name,
                             DestinationDistrictName = dd.Name,
                             t.Type,
                             TypeName = ((H_EmployeeTransfer.Types)Enum.ToObject(typeof(H_EmployeeTransfer.Types), (Int32)t.Type)).ToString()
                         }).OrderBy(o => o.JoiningDate);
            return UIUtility.LINQToDataTable(gList);
        }

        protected void btnTransferUpdate_Click(object sender, EventArgs e)
        {
            Message msg = new Message();
            msg.Type = MessageType.Information;
            msg.Msg = "Data Updated successfully";
            if (Session["EmployeeTransferInfo"] != null && ((DataTable)Session["EmployeeTransferInfo"]).Rows.Count > 0)
            {
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                DataTable dt = (DataTable)Session["EmployeeTransferInfo"];
                int lastRowIndex = dt.Rows.Count - 1;

                IList<H_EmployeeBranch> branchList = H_EmployeeBranch.Find("H_EmployeeId=" + Convert.ToInt32(hdnid.Value), "");
                IList<H_EmployeeTransfer> transferList = H_EmployeeTransfer.Find("H_EmployeeId=" + Convert.ToInt32(hdnid.Value), " JoiningDate");
                H_Employee employee = H_Employee.GetById(Convert.ToInt32(hdnid.Value));

                this.TransactionManager = new TransactionManager(true, "Update [H_EmployeeTransfer]");
                try
                {
                    foreach (H_EmployeeBranch eb in branchList)
                    {
                        H_EmployeeBranch.Delete(this.TransactionManager, eb);
                    }
                    foreach (H_EmployeeTransfer transfer in transferList)
                    {
                        H_EmployeeTransfer.Delete(this.TransactionManager, transfer);
                    }

                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        H_EmployeeTransfer employeeTransfer = new H_EmployeeTransfer();
                        employeeTransfer.H_EmployeeId = employee.Id;
                        employeeTransfer.LetterNo = dt.Rows[row][0].ToString();
                        employeeTransfer.LetterDate = DBUtility.ToDateTime(dt.Rows[row][1]);
                        employeeTransfer.JoiningDate = DBUtility.ToDateTime(dt.Rows[row][2]);
                        employeeTransfer.SourceBranchId = Convert.ToInt32(dt.Rows[row][3]);
                        employeeTransfer.DestinationBranchId = Convert.ToInt32(dt.Rows[row][4]);
                        employeeTransfer.Type = (H_EmployeeTransfer.Types)Convert.ToInt32(dt.Rows[row][11]);
                        employeeTransfer.UserLogin = User.Identity.Name;
                        H_EmployeeTransfer.Insert(TransactionManager, employeeTransfer);

                        H_EmployeeBranch employeeBranch = new H_EmployeeBranch();
                        employeeBranch.H_EmployeeId = employee.Id;
                        employeeBranch.BranchId = Convert.ToInt32(dt.Rows[row][3]);
                        employeeBranch.StartDate = (row == 0 ? employee.JoiningDate.Value : DBUtility.ToDateTime(dt.Rows[row - 1][2]));
                        employeeBranch.EndDate = DBUtility.ToDateTime(dt.Rows[row][2]).AddDays(-1);
                        H_EmployeeBranch.Insert(TransactionManager, employeeBranch);

                        if (row == lastRowIndex)
                        {
                            H_EmployeeBranch presentBrach = new H_EmployeeBranch();
                            presentBrach.H_EmployeeId = employee.Id;
                            presentBrach.BranchId = Convert.ToInt32(dt.Rows[row][4]);
                            presentBrach.StartDate = DBUtility.ToDateTime(dt.Rows[row][2]);
                            presentBrach.EndDate = new DateTime(2099, 12, 31);
                            H_EmployeeBranch.Insert(TransactionManager, presentBrach);
                        }

                    }
                    TransactionManager.Commit();
                }
                catch (Exception ex)
                {
                    this.TransactionManager.Rollback();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Update Failed. " + ex.Message;
                }

            }
            else
            {
                msg.Msg = "No Data to be Saved";
                msg.Type = MessageType.Error;

            }
            ShowUIMessage(msg);
        }

        private bool HasEditPermission(int h_EmployeeId)
        {
            bool isPermitted = false;
            H_EmployeeDesignation ed = H_EmployeeDesignation.Find("H_EmployeeId=" + h_EmployeeId, "EndDate DESC").FirstOrDefault();
            H_Designation desg = H_Designation.GetById(ed.H_DesignationId);
            UserLocation ul = UserLocation.FindByLogin(User.Identity.Name, "").FirstOrDefault();
            if (ul.BranchId == null && ul.RegionId == null && ul.SubzoneId == null && ul.ZoneId == null)
                return true;
            else if (ul.BranchId != null &&
                (desg.GroupType == H_Designation.GroupTypes.ABM
                || desg.GroupType == H_Designation.GroupTypes.LO
                || desg.GroupType == H_Designation.GroupTypes.Sr_LO
                || desg.GroupType == H_Designation.GroupTypes.IRO
                || desg.GroupType == H_Designation.GroupTypes.Peon
                || desg.GroupType == H_Designation.GroupTypes.Other))
                return true;
            else if (ul.RegionId != null &&
                (desg.GroupType == H_Designation.GroupTypes.BM
                || desg.GroupType == H_Designation.GroupTypes.SBM
                || desg.GroupType == H_Designation.GroupTypes.ASE
                || desg.GroupType == H_Designation.GroupTypes.CO))
                return true;
            else if (ul.SubzoneId != null && desg.GroupType == H_Designation.GroupTypes.RM)
                return true;
            else if (ul.ZoneId != null &&
                (desg.GroupType == H_Designation.GroupTypes.DM
                || desg.GroupType == H_Designation.GroupTypes.Auditor
                || desg.GroupType == H_Designation.GroupTypes.Jr_Auditor
                || desg.GroupType == H_Designation.GroupTypes.Sr_Auditor
                || desg.GroupType == H_Designation.GroupTypes.Off_Asst
                || desg.GroupType == H_Designation.GroupTypes.Auditor_M
                || desg.GroupType == H_Designation.GroupTypes.Sr_Auditor_M))
                return true;


            return isPermitted;            
        }

        protected void btnIncrementHeldup_Click(object sender, EventArgs e)
        {
            pnlGrade.Visible = false;
            pnlBranch.Visible = false;
            pnlTransfer.Visible = false;
            pnlLeave.Visible = false;
            pnlPenalty.Visible = false;
            pnlWarning.Visible = false;
            pnlDesignation.Visible = false;
            pnlPromotion.Visible = false;
            pnlConsultancy.Visible = false;
            pnlDropout.Visible = false;
            pnlRejoin.Visible = false;
            pnlPromoEdit.Visible = false;
            pnlTransferEdit.Visible = false;
            pnlIncHeldup.Visible = true;
            H_Employee h_Employee = H_Employee.GetByCode(UIUtility.GetEmployeeID(this.txtEmployee.Text) + UIUtility.GetAccessLevel(User.Identity.Name));
            if (h_Employee != null)
            {
                if (!HasEditPermission(h_Employee.Id))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Information;
                    msg.Msg = "You have no Update Permission for this Employee";
                    ShowUIMessage(msg);
                    return;
                }
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                hdnid.Value = h_Employee.Id.ToString();
                txtEmployee.Text = h_Employee.Code.ToString() + ": " + h_Employee.Name;
                txtStatus.Text = ((H_Employee.Statuses)h_Employee.Status).ToString();
                H_EmployeeDesignation eDesignation = H_EmployeeDesignation.FindByH_EmployeeId(h_Employee.Id, "EndDate DESC")[0];
                txtDesignation.Text = H_Designation.GetById(eDesignation.H_DesignationId).Name;
                if (Session["INC_HELDUP"] != null)
                {
                    Session.Remove("INC_HELDUP");
                }
                Load_gvIncrementHeldup(h_Employee.Id);
            }
        }

        private void Load_gvIncrementHeldup(int p)
        {
            IList<H_EmployeeIncrementHeldup> heldupList = H_EmployeeIncrementHeldup.Find("H_EmployeeId=" + p, "");
            if (heldupList != null && heldupList.Count > 0)
            {
                IList<Branch> branchList=Branch.FindAll();
                IList<Region> reginList=Region.FindAll();
                IList<Subzone> subzonelist=Subzone.FindAll();
                var data = from inc in heldupList
                           join b in branchList on inc.BranchId equals b.Id
                           join r in reginList on b.RegionId equals r.Id
                           join s in subzonelist on r.SubzoneId equals s.Id

                           select new
                           {  
                               inc.Id,
                               inc.H_EmployeeId,
                               inc.LetterNo,
                               inc.LetterDate,
                               inc.IncrementStop,
                               inc.FromDate,
                               inc.ToDate,
                               inc.BranchId,
                               BranchName=b.Name,
                               DistrictId=s.Id,
                               DistrictName=s.Name,
                               inc.ExemptionLetterNo,
                               inc.ExemptionLetterDate,
                               inc.IncrementExempted

                           };
                DataTable dt = UIUtility.LINQToDataTable(data);
                gvIncrementHelup.DataSource = dt;
                gvIncrementHelup.DataBind();
                DropDownList ddlDistrict = (DropDownList)gvIncrementHelup.FooterRow.FindControl("ddlDistrictIncAdd");
                DropDownList ddlBranch = (DropDownList)gvIncrementHelup.FooterRow.FindControl("ddlBranchIncAdd");

                if (ddlDistrict != null)
                {
                    IList<Subzone> disList = Subzone.FindAll("Name");
                    ddlDistrict.DataSource = disList;
                    ddlDistrict.DataTextField = "Name";
                    ddlDistrict.DataValueField = "Id";
                    ddlDistrict.DataBind();

                    ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlDistrict.SelectedValue));
                    ddlBranch.DataTextField = "Name";
                    ddlBranch.DataValueField = "Id";
                    ddlBranch.DataBind();
                }
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int)); dt.Columns.Add("H_EmployeeId", typeof(int));
                dt.Columns.Add("LetterNo", typeof(String )); dt.Columns.Add("LetterDate", typeof(DateTime));
                dt.Columns.Add("IncrementStop", typeof(int)); dt.Columns.Add("FromDate", typeof(DateTime));
                dt.Columns.Add("ToDate", typeof(DateTime)); dt.Columns.Add("BranchId", typeof(int));
                dt.Columns.Add("BranchName", typeof(String )); dt.Columns.Add("DistrictId", typeof(int));
                dt.Columns.Add("DistrictName", typeof(String )); dt.Columns.Add("ExemptionLetterNo", typeof(String ));
                dt.Columns.Add("ExemptionLetterDate", typeof(DateTime)); dt.Columns.Add("IncrementExempted", typeof(int));
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                gvIncrementHelup.DataSource = dt;
                gvIncrementHelup.DataBind();
                DropDownList ddlDistrict = (DropDownList)gvIncrementHelup.FooterRow.FindControl("ddlDistrictIncAdd");
                DropDownList ddlBranch = (DropDownList)gvIncrementHelup.FooterRow.FindControl("ddlBranchIncAdd");

                if (ddlDistrict != null)
                {
                    IList<Subzone> disList = Subzone.FindAll("Name");
                    ddlDistrict.DataSource = disList;
                    ddlDistrict.DataTextField = "Name";
                    ddlDistrict.DataValueField = "Id";
                    ddlDistrict.DataBind();

                    ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlDistrict.SelectedValue));
                    ddlBranch.DataTextField = "Name";
                    ddlBranch.DataValueField = "Id";
                    ddlBranch.DataBind();
                }
                gvIncrementHelup.Rows[0].Visible = false;
            }
        }

        protected void gvIncrementHelup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string BranchId =gvIncrementHelup.DataKeys[e.NewEditIndex]["BranchId"].ToString();
            string DistrictId = gvIncrementHelup.DataKeys[e.NewEditIndex]["DistrictId"].ToString();
            gvIncrementHelup.EditIndex = e.NewEditIndex;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            Load_gvIncrementHeldup(Convert.ToInt32(hdnid.Value));
            DropDownList ddlFromDistrict = (DropDownList)gvIncrementHelup.Rows[e.NewEditIndex].FindControl("ddlDistrictInc");
            DropDownList ddlFromBranch = (DropDownList)gvIncrementHelup.Rows[e.NewEditIndex].FindControl("ddlBranchInc");

            if (ddlFromDistrict != null)
            {
                IList<Subzone> disList = Subzone.FindAll("Name");
                ddlFromDistrict.DataSource = disList;
                ddlFromDistrict.DataTextField = "Name";
                ddlFromDistrict.DataValueField = "Id";
                ddlFromDistrict.DataBind();
                ddlFromDistrict.SelectedValue = DistrictId;

                ddlFromBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(ddlFromDistrict.SelectedValue));
                ddlFromBranch.DataTextField = "Name";
                ddlFromBranch.DataValueField = "Id";
                ddlFromBranch.DataBind();
                ddlFromBranch.SelectedValue = BranchId;
            }


            gvIncrementHelup.Rows[e.NewEditIndex].BackColor = System.Drawing.Color.DarkGray;
        }

        protected void gvIncrementHelup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvIncrementHelup.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            Load_gvIncrementHeldup(Convert.ToInt32(hdnid.Value));
        }

        protected void gvIncrementHelup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvIncrementHelup.DataKeys[e.RowIndex]["Id"].ToString();
            string h_EmployeeId = gvIncrementHelup.DataKeys[e.RowIndex]["H_EmployeeId"].ToString();
            
            TransactionManager tm = new TransactionManager(true);
            H_EmployeeIncrementHeldup h_Rejoin = H_EmployeeIncrementHeldup.GetById(Convert.ToInt32(id));
            h_Rejoin.LetterNo = ((TextBox)gvIncrementHelup.Rows[e.RowIndex].FindControl("txtLetterNo")).Text;
            h_Rejoin.LetterDate = DBUtility.ToDateTime(((TextBox)gvIncrementHelup.Rows[e.RowIndex].FindControl("txtLetterDate")).Text);
            h_Rejoin.FromDate = DBUtility.ToDateTime(((TextBox)gvIncrementHelup.Rows[e.RowIndex].FindControl("txtFromDate")).Text);
            h_Rejoin.ToDate = DBUtility.ToNullableDateTime(((TextBox)gvIncrementHelup.Rows[e.RowIndex].FindControl("txtToDate")).Text);
            h_Rejoin.IncrementStop = DBUtility.ToInt32(((TextBox)gvIncrementHelup.Rows[e.RowIndex].FindControl("txtIncrementStop")).Text);
            h_Rejoin.BranchId = Convert.ToInt32(((DropDownList)gvIncrementHelup.Rows[e.RowIndex].FindControl("ddlBranchInc")).SelectedValue);
            h_Rejoin.ExemptionLetterNo = DBUtility.ToNullableString(((TextBox)gvIncrementHelup.Rows[e.RowIndex].FindControl("txtExemptionLetterNo")).Text);
            h_Rejoin.ExemptionLetterDate = DBUtility.ToNullableDateTime(((TextBox)gvIncrementHelup.Rows[e.RowIndex].FindControl("txtExemptionLetterDate")).Text);
            h_Rejoin.IncrementExempted = DBUtility.ToNullableInt32(((TextBox)gvIncrementHelup.Rows[e.RowIndex].FindControl("txtIncrementExempted")).Text);
            H_EmployeeIncrementHeldup.Update(tm, h_Rejoin);
            tm.Commit();
            gvIncrementHelup.EditIndex = -1;
            HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
            Load_gvIncrementHeldup(Convert.ToInt32(hdnid.Value));
        }

        protected void gvIncrementHelup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "deleterow")
            {
                Int32 id = Convert.ToInt32(e.CommandArgument.ToString());
                TransactionManager tm = new TransactionManager(true);
                H_EmployeeIncrementHeldup.Delete(tm, id);
                tm.Commit();
                
                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                Load_gvIncrementHeldup(Convert.ToInt32(hdnid.Value));
            }
            if (e.CommandName == "addrow")
            {
                DateTime temp;
                Int32 amount;
                if (string.IsNullOrEmpty(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtLetterNoAdd")).Text))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter Letter No";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtLetterDateAdd")).Text) || !DateTime.TryParse(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtLetterDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid Letter Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (string.IsNullOrEmpty(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtFromDateAdd")).Text) || !DateTime.TryParse(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtFromDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid From Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (!string.IsNullOrEmpty(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtToDateAdd")).Text) && !DateTime.TryParse(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtToDateAdd")).Text, out temp))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter valid To Date";
                    this.ShowUIMessage(msg);
                    return;
                }
                if (!string.IsNullOrEmpty(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtIncrementStopAdd")).Text) && !Int32.TryParse(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtIncrementStopAdd")).Text, out amount))
                {
                    Message msg = new Message();
                    msg.Type = MessageType.Error;
                    msg.Msg = "Enter Number of Increment Stop";
                    this.ShowUIMessage(msg);
                    return;
                }


                HiddenField hdnid = (HiddenField)this.Master.FindControl("hdnId");
                TransactionManager tm = new TransactionManager(true);

                H_EmployeeIncrementHeldup h_Rejoin =new  H_EmployeeIncrementHeldup();
                h_Rejoin.H_EmployeeId = Convert.ToInt32(hdnid.Value);
                h_Rejoin.LetterNo = ((TextBox)gvIncrementHelup.FooterRow.FindControl("txtLetterNoAdd")).Text;
                h_Rejoin.LetterDate = DBUtility.ToDateTime(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtLetterDateAdd")).Text);
                h_Rejoin.FromDate = DBUtility.ToDateTime(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtFromDateAdd")).Text);
                h_Rejoin.ToDate = DBUtility.ToNullableDateTime(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtToDateAdd")).Text);
                h_Rejoin.IncrementStop = DBUtility.ToInt32(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtIncrementStopAdd")).Text);
                h_Rejoin.BranchId = Convert.ToInt32(((DropDownList)gvIncrementHelup.FooterRow.FindControl("ddlBranchIncAdd")).SelectedValue);
                h_Rejoin.ExemptionLetterNo = DBUtility.ToNullableString(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtExemptionLetterNoAdd")).Text);
                h_Rejoin.ExemptionLetterDate = DBUtility.ToNullableDateTime(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtExemptionLetterDateAdd")).Text);
                h_Rejoin.IncrementExempted = DBUtility.ToNullableInt32(((TextBox)gvIncrementHelup.FooterRow.FindControl("txtIncrementExemptedAdd")).Text);

                H_EmployeeIncrementHeldup.Insert(tm, h_Rejoin);
                tm.Commit();

                Load_gvIncrementHeldup(Convert.ToInt32(hdnid.Value));
            }
        }
        protected void ddlDistrictInc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DistrictId = ((DropDownList)sender).SelectedValue;
            int rowindex = gvIncrementHelup.EditIndex;
            DropDownList ddlBranch = (DropDownList)gvIncrementHelup.Rows[rowindex].FindControl("ddlBranchInc");
            if (ddlBranch != null)
            {
                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(DistrictId));
                ddlBranch.DataTextField = "Name";
                ddlBranch.DataValueField = "Id";
                ddlBranch.DataBind();
            }
        }
        protected void ddlDistrictIncAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string DistrictId = ((DropDownList)sender).SelectedValue;
            DropDownList ddlBranch = (DropDownList)gvIncrementHelup.FooterRow.FindControl("ddlBranchIncAdd");
            if (ddlBranch != null)
            {
                ddlBranch.DataSource = GetBranchBySubzoneId(Convert.ToInt32(DistrictId));
                ddlBranch.DataTextField = "Name";
                ddlBranch.DataValueField = "Id";
                ddlBranch.DataBind();
            }
        }


    }
}
