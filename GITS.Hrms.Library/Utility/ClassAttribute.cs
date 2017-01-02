using System;

namespace GITS.Hrms.Library.Utility
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassAttribute : Attribute
    {
        protected Attributes _Attribute;

        public enum Attributes
        {
            Entity = 1,
            View = 2,
            Procedure = 3
        }

        public Attributes Attribute
        {
            get { return _Attribute; }
            set { _Attribute = value; }
        }

        internal ClassAttribute(Attributes attribute)
        {
            this._Attribute = attribute;
        }
    }
}