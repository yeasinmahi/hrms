using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Asa.Hrms.Utility;

namespace Asa.Hrms.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
    public class DatabaseConfiguration : EntityBase<DatabaseConfiguration>
    {
        private Int32 _InstallationId;
        private Int64 _Sequence;
        private String _Type;
        private String _Version;
        private String _Description;
        private String _Script;
        private String _Runat;

        public const string TYPE_FRESH = "FRESH";
        public const string TYPE_UPGRADE = "UPGRADE";
        public const string TYPE_BOTH = "BOTH";
        public const string TYPE_ALWAYS = "ALWAYS";

        public DatabaseConfiguration()
        {

        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override bool Audit
        {
            get { return false; }
        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected override string AbstractName
        {
            get { return "[DatabaseConfiguration]"; }
        }

        protected override DatabaseConfiguration Map(SqlDataReader dataReader)
        {
            DatabaseConfiguration entity = new DatabaseConfiguration();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.InstallationId = DBUtility.ToInt32(dataReader["InstallationId"]);
            entity.Sequence = DBUtility.ToInt64(dataReader["Sequence"]);
            entity.Type = DBUtility.ToString(dataReader["Type"]);
            entity.Version = DBUtility.ToString(dataReader["Version"]);
            entity.Description = DBUtility.ToNullableString(dataReader["Description"]);
            entity.Script = DBUtility.ToNullableString(dataReader["Script"]);

            return entity;
        }

        public static DatabaseConfiguration GetBySequence(Int32 sequence)
        {
            return Get("[Sequence] = '" + sequence + "'");
        }

        public static DatabaseConfiguration GetBySequence(TransactionManager transactionManager, Int32 sequence)
        {
            return Get(transactionManager, "[Sequence] = '" + sequence + "'");
        }

        public static IList<DatabaseConfiguration> FindByInstallationId(Int32 installationId)
        {
            return Find("[InstallationId] = '" + installationId + "'", "");
        }

        public static IList<DatabaseConfiguration> FindByInstallationId(TransactionManager transactionManager, Int32 installationId)
        {
            return Find(transactionManager, "[InstallationId] = '" + installationId + "'", "");
        }

        public Int32 InstallationId
        {
            get { return this._InstallationId; }
            set { this._InstallationId = value; }
        }

        public Int64 Sequence
        {
            get { return this._Sequence; }
            set { this._Sequence = value; }
        }

        public String Type
        {
            get { return this._Type; }
            set { this._Type = value; }
        }

        public String Version
        {
            get { return this._Version; }
            set { this._Version = value; }
        }

        public String Description
        {
            get { return this._Description; }
            set { this._Description = value; }
        }

        public String Script
        {
            get { return this._Script; }
            set { this._Script = value; }
        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        public String Runat
        {
            get { return _Runat; }
            set { _Runat = value; }
        }

    }
}
