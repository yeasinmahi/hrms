using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class UploadStatus : EntityBase<UploadStatus>
	{
		private Int32 _UserId;
		private DateTime _DateTime;
		private String _FileName;
        private Boolean _IsProcessed;
        private String _Sender;
        private String _Subject;

		public UploadStatus()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
			get { return "[UploadStatus]"; }
		}

		protected override UploadStatus Map(SqlDataReader dataReader)
		{
			UploadStatus entity = new UploadStatus();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.UserId = DBUtility.ToInt32(dataReader["UserId"]);
			entity.DateTime = DBUtility.ToDateTime(dataReader["DateTime"]);
			entity.FileName = DBUtility.ToString(dataReader["FileName"]);
            entity.Sender = DBUtility.ToNullableString(dataReader["Sender"]);
            entity.Subject = DBUtility.ToNullableString(dataReader["Subject"]);
            entity.IsProcessed = DBUtility.ToBoolean(dataReader["IsProcessed"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public Int32 UserId
		{
			get {return this._UserId;}
			set {this._UserId = value;}
		}

		public DateTime DateTime
		{
			get {return this._DateTime;}
			set {this._DateTime = value;}
		}

		public String FileName
		{
			get {return this._FileName;}
			set {this._FileName = value;}
		}

        public String Sender
        {
            get {return this._Sender;}
            set {this._Sender = value;}
        }

        public String Subject
        {
            get {return this._Subject;}
            set {this._Subject = value;}
        }

        public Boolean IsProcessed
        {
            get { return this._IsProcessed; }
            set { this._IsProcessed = value; }
        }
	}
}