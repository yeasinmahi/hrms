using System;
using System.Data;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.Reports
{
    public partial class H_BranchReport : AddPage
    {
        protected override string PropertyName
        {
            get
            {
                return "BRANCHREPORT";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void LoadData()
        {
            UIUtility.LoadEnums(ddlStatus, typeof(Branch.Statuses), false, true, false);
            UIUtility.LoadEnums(ddlBranchType, typeof(Branch.BranchTypes), false, true,false);
            UIUtility.LoadEnums(ddlLocation, typeof(Branch.LocationTypes), false, true, false);
        }

        protected override string GetListPageUrl()
        {
            throw new NotImplementedException();
        }

        protected override Message Save()
        {
            throw new NotImplementedException();
        }

        protected override void PrintData()
        {
            string query = "select ROW_NUMBER() OVER(Order by b.Name) SL,b.Name,b.OpeningDate,b.MobileNumber,r.Name as Region,s.Name as ASA_District,z.Name as Zone, "
                            + "ISNULL(b.Village,'')+' '+ISNULL(b.PostOffice,'') AS [Address], t.Name as Thana, d.Name District "
                            + "from Branch b "
                            + "INNER JOIN Region r ON b.RegionId=r.Id  and r.Name LIKE '" + txtRegion.Text + "%' "
                            + "INNER JOIN Subzone s ON r.SubZoneId=s.Id and s.Name LIKE '" + txtAsaDistrict.Text + "%' "
                            + "INNER JOIN Zone z ON s.ZoneId=z.Id and z.Name LIKE '" + txtZone.Text + "%' "
                            + "INNER JOIN Thana t ON b.ThanaId=t.Id and t.Name LIKE '" + txtOwnThana.Text + "%' "
                            + "INNER JOIN District d ON t.DistrictId=d.Id and d.Name LIKE '" + txtOwnDistrict.Text + "%' "

                            + "Where b.Name LIKE '" + txtBranchName.Text + "%' ";
            if(!String.IsNullOrEmpty(txtOpeningDate.Text))
            {
                query = query + "AND b.OpeningDate=" + DBUtility.ToDateTime(txtOpeningDate.Text);
            }
            if (ddlStatus.SelectedValue != "0")
            {
                query = query + "AND b.Status=" + ddlStatus.SelectedValue;
            }
            if (ddlBranchType.SelectedValue != "0")
            {
                query = query + "AND b.BranchType=" + ddlBranchType.SelectedValue;
            }
            if (ddlLocation.SelectedValue != "0")
            {
                query = query + "AND b.LocationType=" + ddlLocation.SelectedValue;
            }
            TransactionManager tm = new TransactionManager(false);
            DataTable dt = tm.GetDataSet(query).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Table table = new Table();
                TableRow tr = new TableRow();
                TableCell tc=new TableCell();
                TableHeaderRow hr = new TableHeaderRow();
                hr.BackColor = System.Drawing.Color.LightGray;
                TableHeaderCell hc = null;
                foreach (DataColumn dc in dt.Columns)
                {
                    hc = new TableHeaderCell();
                    hc.BorderWidth = 1;
                    hc.Font.Bold = true;
                    hc.Text = dc.ColumnName;
                    hr.Controls.Add(hc);
                }
                table.Controls.Add(hr);
                foreach (DataRow dr in dt.Rows)
                {
                    tr = new TableRow();
                    for (int c = 0; c < dt.Columns.Count ;c++ )
                    {
                        tc = new TableCell();
                        tc.BorderWidth = 1;
                        tc.Text = dr[c].GetType().ToString()=="System.DateTime" ? UIUtility.Format(Convert.ToDateTime(dr[c])) : dr[c].ToString();
                        tr.Controls.Add(tc);
                    }
                    table.Controls.Add(tr);
                }
                table.BorderWidth = 1;
                table.CellSpacing = 0;
                table.CellPadding = 7;
                pnlBranch.Controls.Add(table);
            }
            else
            {
                Label lblMessage = new Label();
                lblMessage.Text = "No Data Found";
                pnlBranch.Controls.Add(lblMessage);
            }
                           
                            
        }

        
    }
}
