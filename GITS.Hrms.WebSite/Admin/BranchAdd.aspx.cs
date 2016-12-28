using System;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Admin
{
    public partial class BranchAdd : AddPage
    {
        protected override string PropertyName
        {
            get { return "BRANCH ADD"; }
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
            return "BranchList.aspx";
        }

        private Branch GetBranch()
        {
            Branch branch = null;

            if (this.Type == TYPE_EDIT)
            {
                branch = Branch.GetById(Convert.ToInt32(hdnId.Value));
            }
            else
            {
                branch = new Branch();
            }

            branch.ThanaId = DBUtility.ToInt32(ddlThanaId.SelectedValue);
            branch.RegionId = DBUtility.ToInt32(ddlRegionId.SelectedValue);
            //branch.Code = DBUtility.ToNullableInt32(txtCode.Text);
            branch.Name = DBUtility.ToString(txtName.Text);
            branch.NameInBangla = DBUtility.ToNullableString(txtNameInBangla.Text);
            branch.BranchType = (Branch.BranchTypes)DBUtility.ToInt32(ddlBranchType.SelectedValue);
            branch.OpeningDate = DBUtility.ToDateTime(txtOpeningDate.Text);
            branch.MobileNumber = DBUtility.ToNullableString(txtMobileNumber.Text);
            branch.Status =(Branch.Statuses)DBUtility.ToInt32(ddlStatus.SelectedValue);
            branch.LocationType = (Branch.LocationTypes)DBUtility.ToInt32(ddlLocationType.SelectedValue);
            branch.Village = DBUtility.ToNullableString(txtVillage.Text);
            branch.PostOffice = DBUtility.ToNullableString(txtPostOffice.Text);
            branch.PostCode = DBUtility.ToNullableString(txtPostCode.Text);

            return branch;
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
                Branch branch = this.GetBranch();
                string desc = "";

                if (this.Type == TYPE_ADD)
                {
                    desc = "Insert [Branch]";
                }
                else
                {
                    desc = "Update [Branch]";
                }

                this.TransactionManager = new TransactionManager(true, desc);

                if (this.Type == TYPE_ADD)
                {
                    Branch.Insert(this.TransactionManager, branch);

                    hdnId.Value = branch.Id.ToString();
                    BranchOpenClose boc = new BranchOpenClose();
                    boc.BranchId = branch.Id;
                    boc.LetterNo = txtLetterNo.Text;
                    boc.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
                    boc.Effectivedate = DBUtility.ToDateTime(txtOpeningDate.Text);
                    boc.Types = DBUtility.ToInt32(ddlStatus.SelectedValue);
                    boc.IsRecent = true;
                    BranchOpenClose.Insert(this.TransactionManager, boc);
                    this.Type = TYPE_EDIT;
                }
                else
                {
                    Branch br = Branch.GetById(Convert.ToInt32(hdnId.Value));
                    if (br.Status == (Branch.Statuses)Convert.ToInt32(ddlStatus.SelectedValue))
                    {
                        BranchOpenClose boc = BranchOpenClose.Get("BranchId=" + hdnId.Value + " AND IsRecent=1");
                        if (boc != null)
                        {
                            boc.LetterNo = txtLetterNo.Text;
                            boc.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
                            boc.Effectivedate = DBUtility.ToDateTime(txtOpeningDate.Text);
                            BranchOpenClose.Update(this.TransactionManager, boc);
                        }
                    }
                    else
                    {
                        BranchOpenClose boc = BranchOpenClose.Get("BranchId=" + hdnId.Value + " AND IsRecent=1");
                        if (boc != null)
                        {
                            boc.IsRecent = false;
                            BranchOpenClose.Update(this.TransactionManager, boc);
                        }
                        boc = new BranchOpenClose();
                        boc.BranchId = Convert.ToInt32(hdnId.Value);
                        boc.LetterNo = txtLetterNo.Text;
                        boc.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
                        boc.Effectivedate = DBUtility.ToDateTime(txtOpeningDate.Text);
                        boc.Types = DBUtility.ToInt32(ddlStatus.SelectedValue);
                        boc.IsRecent = true;
                        BranchOpenClose.Insert(this.TransactionManager, boc);
                    }

                    Branch.Update(this.TransactionManager, branch);
                
                }

                this.TransactionManager.Commit();
            }

            return msg;
        }

        protected override void LoadData()
        {
            Branch branch = null;
            UIUtility.LoadEnums(ddlStatus, typeof(Branch.Statuses), false, false, true);
            UIUtility.LoadEnums(ddlBranchType, typeof(Branch.BranchTypes), false, false, true);
            UIUtility.LoadEnums(ddlLocationType, typeof(Branch.LocationTypes), false, false, true);

            this.ddlDistrictId.DataSource = District.Find("","Name");
            this.ddlDistrictId.DataBind();
            this.ddlDistrictId_OnSelectedIndexChanged(ddlDistrictId, new EventArgs());

            this.ddlZoneId.DataSource = Zone.Find("Status=1","Name");
            this.ddlZoneId.DataBind();
            this.ddlZoneId_OnSelectedIndexChanged(ddlZoneId, new EventArgs());

            if (Request.QueryString["Id"] != null)
            {
                hdnId.Value = Request.QueryString["Id"];
                branch = Branch.GetById(Convert.ToInt32(hdnId.Value));

                if (branch != null)
                {
                    this.Type = TYPE_EDIT;

                    Thana thana = Thana.GetById(Convert.ToInt32(branch.ThanaId));
                    Region region = Region.GetById(Convert.ToInt32(branch.RegionId));

                    ddlDistrictId.SelectedValue = UIUtility.Format(thana.DistrictId);
                    ddlDistrictId_OnSelectedIndexChanged(this.ddlDistrictId, new EventArgs());

                    ddlZoneId.SelectedValue = UIUtility.Format(Subzone.GetById(region.SubzoneId).ZoneId);
                    ddlZoneId_OnSelectedIndexChanged(this.ddlZoneId, new EventArgs());

                    ddlSubzoneId.SelectedValue = UIUtility.Format(region.SubzoneId);
                    ddlSubzoneId_OnSelectedIndexChanged(this.ddlSubzoneId, new EventArgs());

                    ddlThanaId.SelectedValue = UIUtility.Format(branch.ThanaId);
                    ddlRegionId.SelectedValue = UIUtility.Format(branch.RegionId);
                    //txtCode.Text = UIUtility.Format(branch.Code);
                    txtName.Text = branch.Name;
                    txtNameInBangla.Text = branch.NameInBangla;
                    ddlBranchType.SelectedValue = ((Int32)branch.BranchType).ToString();
                    txtOpeningDate.Text = UIUtility.Format(branch.OpeningDate);
                    txtMobileNumber.Text = branch.MobileNumber;
                    ddlStatus.SelectedValue = ((Int32)branch.Status).ToString();
                    ddlLocationType.SelectedValue = ((Int32)branch.LocationType).ToString();
                    txtVillage.Text = branch.Village;
                    txtPostOffice.Text = branch.PostOffice;
                    txtPostCode.Text = branch.PostCode;
                    BranchOpenClose boc = BranchOpenClose.Get("BranchId="+branch.Id+" AND IsRecent=1");
                    if (boc != null)
                    {
                        txtLetterNo.Text = boc.LetterNo;
                        txtLetterDate.Text = UIUtility.Format(boc.LetterDate);
                        txtOpeningDate.Text = UIUtility.Format(boc.Effectivedate);
                        if (boc.Types == 2)
                        {
                            lblOpenDate.Text = "Closing Date:";
                        }
                    }
                }
            }
        }

        protected void ddlDistrictId_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDistrictId.SelectedValue != null && ddlDistrictId.SelectedValue != "")
            {
                this.ddlThanaId.DataSource = Thana.FindByDistrictId(Convert.ToInt32(this.ddlDistrictId.SelectedValue), "");
                this.ddlThanaId.DataBind();
            }
        }

        protected void ddlZoneId_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlZoneId.SelectedValue != null && ddlZoneId.SelectedValue != "")
            {
                this.ddlSubzoneId.DataSource = Subzone.Find("ZoneId="+Convert.ToInt32(this.ddlZoneId.SelectedValue)+" AND Status=1", "");
                this.ddlSubzoneId.DataBind();
            }
        }

        protected void ddlSubzoneId_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSubzoneId.SelectedValue != null && ddlSubzoneId.SelectedValue != "")
            {
                this.ddlRegionId.DataSource = Region.Find("SubzoneId="+Convert.ToInt32(this.ddlSubzoneId.SelectedValue)+" AND Status=1", "");
                this.ddlRegionId.DataBind();
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOpeningDate.Text = "";
            txtLetterDate.Text = "";
            txtLetterNo.Text = "";
            if (ddlStatus.SelectedValue == "1")
                lblOpenDate.Text = "Opening Date:";
            else
                lblOpenDate.Text = "Closing Date:";

            txtLetterNo.Focus();
        }

    }
}
