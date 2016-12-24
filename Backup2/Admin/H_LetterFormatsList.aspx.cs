using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Asa.Hrms.Web;
using Asa.Hrms.Data;

namespace Asa.Hrms.WebSite.Admin
{
    public partial class H_LetterFormatsList : GridPage
    {
        protected override string PropertyName
        {
            get { return "LETTERFORMAT LIST"; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            SortColumn = "SortOrder";
            this.GridView = this.gvList;
            this.EntityType = typeof(Asa.Hrms.Data.Entity.H_LetterFormats);
        }
        protected override string GetAddPageUrl()
        {
            return "H_LetterFormatsAdd.aspx";
        }
    }
}
