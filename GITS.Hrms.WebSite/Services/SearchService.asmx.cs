using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.WebSite.Services
{
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    [ScriptService]
    public class SearchService : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public String[] GetSuggestions(String prefixText, Int32 count)
        {
            Int32 total = 0;

            IList<H_Employee> employees = H_Employee.Find("(Name LIKE '%" + prefixText + "%' OR CONVERT(VARCHAR(10), Code) LIKE '%" + prefixText + "%')" + UIUtility.GetAccessLevel(User.Identity.Name), "Code", 1, count, out total);

             if (employees.Count > 0)
             {
                 return employees.Select(e => e.EmployeeName).ToArray<String>();
             }
             else
             {
                 return null;
             }
        }
    }
}