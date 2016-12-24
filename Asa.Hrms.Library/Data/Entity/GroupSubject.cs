using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class GroupSubject : EntityBase<GroupSubject>
	{
		private String _Name;

		public GroupSubject()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[GroupSubject]"; }
		}

		protected override GroupSubject Map(SqlDataReader dataReader)
		{
			GroupSubject entity = new GroupSubject();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}

	}
}
