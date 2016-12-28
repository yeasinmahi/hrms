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
			get {return this._Village;}
			set {this._Village = value;}
		}

		public String PostOffice
		{
			get {return this._PostOffice;}
			set {this._PostOffice = value;}
		}

		public Nullable<Int32> PostCode
		{
			get {return this._PostCode;}
			set {this._PostCode = value;}
		}

		public Int32 ThanaId
		{
			get {return this._ThanaId;}
			set {this._ThanaId = value;}
		}

		public String Phone
		{
			get {return this._Phone;}
			set {this._Phone = value;}
		}

		public String Email
		{
			get {return this._Email;}
			set {this._Email = value;}
		}

	}
}
