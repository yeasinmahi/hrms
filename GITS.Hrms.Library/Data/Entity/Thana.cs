using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
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
			get {return _DistrictId;}
			set {_DistrictId = value;}
		}

		public String Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

	}
}
