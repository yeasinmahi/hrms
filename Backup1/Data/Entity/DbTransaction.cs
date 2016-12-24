using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class DbTransaction : EntityBase<DbTransaction>
	{
		private String _Description;
		private String _CreatedBy;
		private DateTime _CreatedDate;

		public DbTransaction()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[DbTransaction]"; }
		}

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override bool Audit
        {
            get { return false; }
        }

		protected override DbTransaction Map(SqlDataReader dataReader)
		{
			DbTransaction entity = new DbTransaction();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.Description = DBUtility.ToNullableString(dataReader["Description"]);
			entity.CreatedBy = DBUtility.ToString(dataReader["CreatedBy"]);
			entity.CreatedDate = DBUtility.ToDateTime(dataReader["CreatedDate"]);

			return entity;
		}

		public static IList<DbTransaction> FindByCreatedBy(String createdBy)
		{
			return Find("[CreatedBy] = '" + createdBy + "'", "");
		}

		public static IList<DbTransaction> FindByCreatedBy(TransactionManager transactionManager, String createdBy)
		{
			return Find(transactionManager, "[CreatedBy] = '" + createdBy + "'", "");
		}

		public String Description
		{
			get {return this._Description;}
			set {this._Description = value;}
		}

		public String CreatedBy
		{
			get {return this._CreatedBy;}
			set {this._CreatedBy = value;}
		}

		public DateTime CreatedDate
		{
			get {return this._CreatedDate;}
			set {this._CreatedDate = value;}
		}

	}
}
