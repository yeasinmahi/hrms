using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Asa.Hrms.Data.View;

namespace Asa.Hrms.Security
{
    /// <summary>
    /// Summary description for MmsPermissionProvider
    /// </summary>
    public class MmsPermissionProvider
    {
        public const string PERMISSION_PAGE = "Permission.aspx";

        public MmsPermissionProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool HasPermission(string userLogin, string propertyName, string commandName)
        {
            IList<UserCommandView> permissions = UserCommandView.Find("UserLogin = '" + userLogin + "' AND PropertyName = '" + propertyName + "' AND CommandName = '" + commandName + "'", "");

            if (permissions == null || permissions.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool HasPermission(string userLogin, string propertyName)
        {
            IList<UserPropertyView> permissions = UserPropertyView.Find("UserLogin = '" + userLogin + "' AND PropertyName = '" + propertyName + "'", "");

            if (permissions == null || permissions.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
