using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace GITS.Hrms.Library.Utility
{
    public static class MyExtensionMethods
    {
        public static void ReorderAlphabetized(this DropDownList ddl)
        {
            List<ListItem> listCopy = new List<ListItem>();
            foreach (ListItem item in ddl.Items)
                listCopy.Add(item);
            ddl.Items.Clear();
            foreach (ListItem item in listCopy.OrderBy(item => item.Text))
                ddl.Items.Add(item);
        }
    }
}
