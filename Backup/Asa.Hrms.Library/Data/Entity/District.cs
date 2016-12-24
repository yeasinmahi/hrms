using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
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
			get {return this._Name;}
			set {this._Name = value;}
		}
        public Int32 DivisionId
        {
            get { return this._DivisionId; }
            set { this._DivisionId = value; }
        }

	}
}
