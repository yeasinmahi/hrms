using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class ExamName : EntityBase<ExamName>
	{
		private String _Name;

		public ExamName()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[ExamName]"; }
		}

		protected override ExamName Map(SqlDataReader dataReader)
		{
			ExamName entity = new ExamName();

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
