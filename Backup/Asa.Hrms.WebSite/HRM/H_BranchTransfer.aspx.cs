using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using Asa.Hrms.Web;
using Asa.Hrms.Data;
using Asa.Hrms.Utility;
using Asa.Hrms.Data.Entity;

namespace Asa.Hrms.WebSite.HRM
{
    public partial class H_BranchTransfer : AddPage
    {
        protected override string PropertyName
        {
            get { return "H_BRANCHTRANSFER ADD"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override string GetListPageUrl()
        {
            return "H_BranchTransferList.aspx";
        }
        protected override void LoadData()
        {
            ddlZone.DataSource = Zone.Find("Status=1", "Name");
            ddlZone.DataBind();
            this.ddlZone_SelectedIndexChanged(this.ddlZone, new EventArgs());

            
            //Trasfer Info
            IList<Zone> zoneList = Zone.Find("Status=1", "Name"); //FindByLogin("Status=1", "Name", User.Identity.Name);
            if (zoneList == null)
            {
                zoneList = new List<Zone>();
            }
            Zone zone1 = new Zone();
            zone1.Id = 0;
            zone1.Name = "Select Zone";
            zone1.Status = Zone.Statuses.ACTIVE;
            zoneList.Insert(0, zone1);
            ddlTransZone.DataSource = zoneList;
            ddlTransZone.DataBind();
            this.ddlTransZone_SelectedIndexChanged(null, null);
            
        }
        protected void ddlSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlSubzone.SelectedValue != null && this.ddlSubzone.SelectedValue != "")
            { 
                this.ddlRegion.DataSource =  Region.Find("SubzoneId = " + this.ddlSubzone.SelectedValue + " And Status=1", "Name");//, User.Identity.Name);
                this.ddlRegion.DataBind();
                this.ddlRegion_SelectedIndexChanged(ddlRegion, new EventArgs());
            }
        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlRegion.SelectedValue != null && this.ddlRegion.SelectedValue != "")
            {
                this.ddlBranch.DataSource = Branch.Find("RegionId = " + this.ddlRegion.SelectedValue + " AND Status=1", "Name");//, User.Identity.Name);
                this.ddlBranch.DataBind();
                ddlBranch_SelectedIndexChanged(ddlBranch,new EventArgs());
            }
            else
            {
                this.ddlBranch.Items.Clear();
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlBranch.SelectedValue != null && this.ddlBranch.SelectedValue != "")
            {
                Branch branch = Branch.GetById(Convert.ToInt32(ddlBranch.SelectedValue));
                Thana thana = Thana.GetById(branch.ThanaId);
                District district = District.GetById(thana.DistrictId);
                txtThana.Text = thana.Name;
                txtDistrict.Text = district.Name;
            }
            else
            {
                txtThana.Text = "";
                txtDistrict.Text = "";
            }
        }
        protected override Message Save()
        {
            Message msg = this.Validate();
            if (msg.Type == MessageType.Information)
            {
                Asa.Hrms.Data.Entity.H_BranchTransfer h_BranchTransfer = this.GetH_BranchTransfer();
                string desc = "Insert [H_BranchTransfer]";

                this.TransactionManager = new TransactionManager(true, desc);

                Asa.Hrms.Data.Entity.H_BranchTransfer.Insert(this.TransactionManager, h_BranchTransfer);

                hdnId.Value = h_BranchTransfer.Id.ToString();
                this.Type = TYPE_EDIT;

                Branch oBranch = Branch.GetById(Convert.ToInt32(ddlBranch.SelectedValue));

                oBranch.RegionId =h_BranchTransfer.DestinationRegionId;
                Branch.Update(this.TransactionManager, oBranch);

                this.TransactionManager.Commit();
            }
            return msg;
        }
        private Asa.Hrms.Data.Entity.H_BranchTransfer GetH_BranchTransfer()
        {
            Asa.Hrms.Data.Entity.H_BranchTransfer h_BranchTransfer = new Asa.Hrms.Data.Entity.H_BranchTransfer();

            h_BranchTransfer.BranchId = DBUtility.ToInt32(ddlBranch.SelectedValue);
            h_BranchTransfer.LetterNo = DBUtility.ToString(txtLetterNo.Text);
            h_BranchTransfer.LetterDate = DBUtility.ToDateTime(txtLetterDate.Text);
            h_BranchTransfer.SourceRegionId = DBUtility.ToInt32(ddlRegion.SelectedValue);
            h_BranchTransfer.DestinationRegionId = DBUtility.ToInt32(ddlTransRegion.SelectedValue);
            h_BranchTransfer.TransferDate = DBUtility.ToDateTime(txtTransferDate.Text);

            return h_BranchTransfer;
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
            if (ddlRegion.SelectedValue==ddlTransRegion.SelectedValue)
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Source and Destination Region can not be same";
                return msg;
            }
            if (DBUtility.ToDateTime(txtLetterDate.Text) > DBUtility.ToDateTime(txtTransferDate.Text))
            {
                msg.Type = MessageType.Error;
                msg.Msg = "Letter Date must not greater than Transfer Date";
                return msg;
            }
            return msg;
        }

        protected void ddlTranSubzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlTranSubzone.SelectedValue != null && this.ddlTranSubzone.SelectedValue != "")
            {
                IList<Region> regionList = Region.Find("SubzoneId = " + this.ddlTranSubzone.SelectedValue + " And Status=1", "Name");//, User.Identity.Name);
                if (regionList == null)
                {
                    regionList = new List<Region>();
                }

                Region all = new Region();
                all.SubzoneId = 0;
                all.Id = 0;
                all.Name = "Select Region";
                regionList.Insert(0, all);
                this.ddlTransRegion.DataSource = regionList;
                this.ddlTransRegion.DataBind();

            }
        }

        protected void ddlTransZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<Subzone> subzoneList = Subzone.Find("ZoneId = " + this.ddlTransZone.SelectedValue + " And Status=1", "Name");//, User.Identity.Name);
            if (subzoneList == null)
            {
                subzoneList = new List<Subzone>();
            }

            Subzone all = new Subzone();
            all.ZoneId = 0;
            all.Id = 0;
            all.Name = "Select District";
            subzoneList.Insert(0, all);
            this.ddlTranSubzone.DataSource = subzoneList;// Subzone.FindByLogin("Status=1", "Name", User.Identity.Name);
            this.ddlTranSubzone.DataBind();
            this.ddlTranSubzone_SelectedIndexChanged(ddlTranSubzone, new EventArgs());
        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlZone.SelectedValue != null && this.ddlZone.SelectedValue != "")
            {
                this.ddlSubzone.DataSource = Subzone.Find("ZoneId="+ddlZone.SelectedValue+" AND Status=1", "Name");//, User.Identity.Name);
                this.ddlSubzone.DataBind();
                this.ddlSubzone_SelectedIndexChanged(this.ddlSubzone, new EventArgs());
            }
        }

        
    }
}
