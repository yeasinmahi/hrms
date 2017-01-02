using System.ComponentModel;
using System.Web.UI;

namespace GITS.Hrms.Library.Web
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
