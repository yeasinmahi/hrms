using System;

namespace GITS.Hrms.Library.Utility
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAttribute : Attribute
    {
        protected Attributes _Attribute;

        public enum Attributes
        {
            PrimaryKey = 1,
            AutoIncrement = 2,
            Index = 3,
            NonTable = 4,
            General = 5
        }

        public Attributes Attribute
        {
            get { return _Attribute; }
            set { _Attribute = value; }
        }

        internal PropertyAttribute(Attributes attribute)
        {
            _Attribute = attribute;
        }
    }
}
