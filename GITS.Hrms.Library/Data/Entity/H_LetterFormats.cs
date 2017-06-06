using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_LetterFormats : EntityBase<H_LetterFormats>
    {
        private LetterTypes _LetterType;
        private String _Name;
        private String _InsideAddress;
        private String _Subject;
        private String _LetterBody;
        private String _Conclusion;
        private String _Signatory;
        private String _Designation;
        private Int32 _SortOrder;

        public enum LetterTypes
        {
            Transfer_Letter = 1,
            Warning_Letter = 2,
            Increment_Heldup_Letter = 3,
            Promotion_Letter = 4,
            Penalty_Letter = 5,
            Leave_Letter = 6
        }

        public H_LetterFormats()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[H_LetterFormats]"; }
		}

        protected override H_LetterFormats Map(SqlDataReader dataReader)
        {
            H_LetterFormats entity = new H_LetterFormats();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.LetterType = (LetterTypes)DBUtility.ToInt32(dataReader["LetterType"]);
            entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.InsideAddress = DBUtility.ToString(dataReader["InsideAddress"]);
            entity.Subject = DBUtility.ToString(dataReader["Subject"]);
            entity.LetterBody = DBUtility.ToString(dataReader["LetterBody"]);
            entity.Conclusion = DBUtility.ToString(dataReader["Conclusion"]);
            entity.Signatory = DBUtility.ToString(dataReader["Signatory"]);
            entity.Designation = DBUtility.ToString(dataReader["Designation"]);
            entity.SortOrder = DBUtility.ToInt32(dataReader["SortOrder"]);
            
            entity.EntityState = EntityStates.Clean;

            return entity;
        }

        public static IList<H_LetterFormats> FindByLetterType(Int32 letterTypeId, String sortColumns)
        {
            return Find("[LetterType] = " + letterTypeId, sortColumns);
        }

        public LetterTypes LetterType
        {
            get { return _LetterType; }
            set { _LetterType = value; }
        }

        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public String Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        public String LetterBody
        {
            get { return _LetterBody; }
            set { _LetterBody = value; }
        }
        public String Conclusion
        {
            get { return _Conclusion; }
            set { _Conclusion = value; }
        }
        public String Signatory
        {
            get { return _Signatory; }
            set { _Signatory = value; }
        }
        public String Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }
        public String InsideAddress
        {
            get { return _InsideAddress; }
            set { _InsideAddress = value; }
        }
        public Int32  SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
    }
}
