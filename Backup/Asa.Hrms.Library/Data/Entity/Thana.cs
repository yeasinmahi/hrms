using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class Thana : EntityBase<Thana>
	{
		private Int32 _DistrictId;
		private String _Name;

		public Thana()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[Thana]"; }
		}

		protected override Thana Map(SqlDataReader dataReader)
		{
			Thana entity = new Thana();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.DistrictId = DBUtility.ToInt32(dataReader["DistrictId"]);
			entity.Name = DBUtility.ToString(dataReader["Name"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<Thana> FindByDistrictId(Int32 districtId, String sortColumns)
		{
			return Find("[DistrictId] = '" + districtId + "'", sortColumns);
		}

		public static IList<Thana> FindByDistrictId(TransactionManager transactionManager, Int32 districtId, String sortColumns)
		{
			return Find(transactionManager, "[DistrictId] = '" + districtId + "'", sortColumns);
		}

		public Int32 DistrictId
		{
			get {return this._DistrictId;}
			set {this._DistrictId = value;}
		}

		public String Name
		{
			get {return this._Name;}
			set {this._Name = value;}
		}

	}
}
