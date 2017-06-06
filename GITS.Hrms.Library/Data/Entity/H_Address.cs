using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_Address : EntityBase<H_Address>
	{
		private String _Village;
		private String _PostOffice;
		private Nullable<Int32> _PostCode;
		private Int32 _ThanaId;
		private String _Phone;
		private String _Email;

		public H_Address()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_Address]"; }
		}

		protected override H_Address Map(SqlDataReader dataReader)
		{
			H_Address entity = new H_Address();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Village = DBUtility.ToString(dataReader["Village"]);
			entity.PostOffice = DBUtility.ToNullableString(dataReader["PostOffice"]);
			entity.PostCode = DBUtility.ToNullableInt32(dataReader["PostCode"]);
			entity.ThanaId = DBUtility.ToInt32(dataReader["ThanaId"]);
			entity.Phone = DBUtility.ToNullableString(dataReader["Phone"]);
			entity.Email = DBUtility.ToNullableString(dataReader["Email"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_Address> FindByThanaId(Int32 thanaId, String sortColumns)
		{
			return Find("[ThanaId] = '" + thanaId + "'", sortColumns);
		}

		public static IList<H_Address> FindByThanaId(TransactionManager transactionManager, Int32 thanaId, String sortColumns)
		{
			return Find(transactionManager, "[ThanaId] = '" + thanaId + "'", sortColumns);
		}

		public String Village
		{
			get {return _Village;}
			set {_Village = value;}
		}

		public String PostOffice
		{
			get {return _PostOffice;}
			set {_PostOffice = value;}
		}

		public Nullable<Int32> PostCode
		{
			get {return _PostCode;}
			set {_PostCode = value;}
		}

		public Int32 ThanaId
		{
			get {return _ThanaId;}
			set {_ThanaId = value;}
		}

		public String Phone
		{
			get {return _Phone;}
			set {_Phone = value;}
		}

		public String Email
		{
			get {return _Email;}
			set {_Email = value;}
		}

	}
}
