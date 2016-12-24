using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class BoardUniversity : EntityBase<BoardUniversity>
	{
		private String _Name;

		public BoardUniversity()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[BoardUniversity]"; }
		}

		protected override BoardUniversity Map(SqlDataReader dataReader)
		{
			BoardUniversity entity = new BoardUniversity();

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
