using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;

namespace Asa.Hrms.Web
{
    [
        Designer(typeof(System.Web.UI.Design.WebControls.CompositeControlDesigner)),
        ToolboxData("<{0}:BoundField />")
    ]
    public class BoundField : System.Web.UI.WebControls.BoundField
    {
        private FieldTypes _FieldType;

        public enum FieldTypes
        {
            String,
            Int32,
            Int64,
            Double,
            DateTime,
            Boolean
        }

        [
        Browsable(true),
        Description("Set / Gets the FieldType"),
        Category("Misc"),
        DefaultValue(true),
        ]
        public FieldTypes FieldType
        {
            get { return _FieldType; }
            set { _FieldType = value; }
        }
    }
}
