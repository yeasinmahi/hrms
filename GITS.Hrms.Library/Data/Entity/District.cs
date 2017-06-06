using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class District : EntityBase<District>
	{
        private Int32 _DivisionId;
		private String _Name;

		public District()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[District]"; }
		}

		protected override District Map(SqlDataReader dataReader)
		{
			District entity = new District();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);
            entity.DivisionId = DBUtility.ToInt32(dataReader["DivisionId"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}
		
		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}
        public Int32 DivisionId
        {
            get { return _DivisionId; }
            set { _DivisionId = value; }
        }

	}
}
