using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Xml;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    public enum EntityStates
    {
        New = 1,
        Clean = 2,
        Dirty = 3,
        Deleted = 4
    }

    /// <summary>
    /// Summary description for EntityBase
    /// </summary>
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public abstract class EntityBase<T> : AbstractBase<T>
    {
        private static EntityBase<T> _Instance;
        private EntityStates _EntityState = EntityStates.New;
        protected static string _DatabaseDateFormat;

        [Property(PropertyAttribute.Attributes.NonTable)]
        public virtual EntityStates EntityState
        {
            get { return _EntityState; }
            set { _EntityState = value; }
        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected virtual bool Audit
        {
            get { return true; }
        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        protected static new EntityBase<T> Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = (EntityBase<T>)Activator.CreateInstance(typeof(T));
                }

                return _Instance;
            }
        }

        protected virtual bool Insert(TransactionManager transactionManager)
        {
            SqlCommand cmd = transactionManager.CreateCommand();
            PropertyInfo[] props = GetType().GetProperties();
            string strInsertSql = "INSERT INTO " + AbstractName + " (";
            string fields = "";
            string parameters = "";

            foreach (PropertyInfo prop in props)
            {
                bool insert = true;
                Attribute[] attributes = Attribute.GetCustomAttributes(prop);

                foreach (Attribute att in attributes)
                {
                    if (att.GetType() == typeof(PropertyAttribute))
                    {
                        PropertyAttribute attribute = (PropertyAttribute)att;

                        switch (attribute.Attribute)
                        {
                            case PropertyAttribute.Attributes.AutoIncrement:
                            case PropertyAttribute.Attributes.NonTable:
                                insert = false;
                                break;
                        }
                    }
                }

                if (insert == false)
                {
                    continue;
                }

                object newValue = prop.GetValue(this, null);

                if (newValue == null || (newValue.GetType() == typeof(DateTime) && (DateTime)newValue == new DateTime()))
                {
                    continue;
                }

                fields += "[" + prop.Name + "],";
                parameters += "@" + prop.Name + ",";

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@" + prop.Name;
                param.Value = newValue;

                cmd.Parameters.Add(param);
            }

            strInsertSql += fields.Substring(0, fields.Length - 1) + ") VALUES (" + parameters.Substring(0, parameters.Length - 1) + ")";

            SqlParameter idParam = new SqlParameter();
            idParam.Direction = ParameterDirection.Output;
            idParam.SqlDbType = SqlDbType.Int;
            idParam.Size = sizeof(Int32);
            idParam.ParameterName = "@Id";
            cmd.Parameters.Add(idParam);
            strInsertSql += " SET @Id = SCOPE_IDENTITY()";

            cmd.CommandText = strInsertSql;
            transactionManager.RecordAffected += cmd.ExecuteNonQuery();

            Id = Convert.ToInt32(cmd.Parameters["@Id"].Value);

            #region audit
            if (Audit)
            {
                DbTransactionDetails dbtd = new DbTransactionDetails();
                dbtd.DbTransactionId = transactionManager.DbTransactionId;
                dbtd.Type = DbTransactionDetails.TYPE_INSERT;
                dbtd.TableName = AbstractName;
                dbtd.IdentityColumn = "Id";
                dbtd.IdentityValue = Id.ToString();
                //dbtd.Value = Serializer.Serialize(this);
                DbTransactionDetails.Insert(transactionManager, dbtd);
            }
            #endregion

            EntityState = EntityStates.Clean;

            return true;
        }

        public static bool Insert(EntityBase<T> entity)
        {
            TransactionManager tm = new TransactionManager(true, "Insert " + entity.AbstractName);

            try
            {
                entity.Insert(tm);
                tm.Commit();
            }
            catch (Exception ex)
            {
                tm.Rollback();

                throw ex;
            }

            return true;
        }

        public static bool Insert(TransactionManager transactionManager, EntityBase<T> entity)
        {
            entity.Insert(transactionManager);

            return true;
        }

        protected virtual bool Update(TransactionManager transactionManager)
        {
            string strUpdateSql = "";
            string strWhere = "";
            string strFieldValue = "";

            Type type = GetType();
            T oldObj = Get(transactionManager, "Id = " + Id);

            if (oldObj == null)
            {
                throw new Exception("Could not find the record.");
            }

            //IList<DbTransactionDetails> list = DbTransactionDetails.Find(transactionManager, "Type='" + DbTransactionDetails.TYPE_INSERT + "' AND TableName='" + this.AbstractName + "' AND IdentityValue='" + id + "'", "");

            //if (list != null && list.Count > 0)
            //{
            //    DbTransaction dbt = DbTransaction.GetById(transactionManager, list[0].DbTransactionId);

            //    if (dbt != null && dbt.CreatedBy != null)
            //    {
            //        throw new Exception("No update permission.");
            //    }
            //}

            SqlCommand cmd = transactionManager.CreateCommand();
            PropertyInfo[] props = type.GetProperties();

            strUpdateSql = "UPDATE " + AbstractName;
            strWhere = " WHERE Id = " + Id + "";
            strFieldValue = " SET ";

            XmlDocument doc = new XmlDocument();
            XmlElement node = doc.CreateElement(type.FullName);
            doc.AppendChild(node);

            foreach (PropertyInfo prop in props)
            {
                bool update = true;
                Attribute[] attributes = Attribute.GetCustomAttributes(prop);

                foreach (Attribute att in attributes)
                {
                    if (att.GetType() == typeof(PropertyAttribute))
                    {
                        PropertyAttribute attribute = (PropertyAttribute)att;

                        switch (attribute.Attribute)
                        {
                            case PropertyAttribute.Attributes.AutoIncrement:
                            case PropertyAttribute.Attributes.NonTable:
                                update = false;
                                break;
                        }
                    }
                }

                if (update == false)
                {
                    continue;
                }

                Object oldValue = prop.GetValue(oldObj, null);
                Object updValue = prop.GetValue(this, null);

                if ((oldValue == null && updValue != null) || (oldValue != null && oldValue.Equals(updValue) == false))
                {
                    strFieldValue += "[" + prop.Name + "]=" + "@" + prop.Name + ", ";

                    XmlAttribute attr = doc.CreateAttribute(prop.Name);
                    attr.Value = DBUtility.ToNullableString(oldValue);
                    node.Attributes.Append(attr);

                    if (updValue == null || (updValue.GetType() == typeof(DateTime) && (DateTime)updValue == new DateTime()))
                    {
                        SqlParameter myParam = new SqlParameter();
                        myParam.ParameterName = "@" + prop.Name;
                        myParam.Value = DBNull.Value;
                        cmd.Parameters.Add(myParam);
                    }
                    else
                    {
                        SqlParameter myParam = new SqlParameter();
                        myParam.ParameterName = "@" + prop.Name;
                        myParam.Value = updValue;
                        cmd.Parameters.Add(myParam);
                    }
                }
            }

            if (strFieldValue.Equals(" SET "))
            {
                return true;
            }

            strUpdateSql = strUpdateSql + strFieldValue.Substring(0, strFieldValue.Length - 2) + strWhere;

            cmd.CommandText = strUpdateSql;
            transactionManager.RecordAffected += cmd.ExecuteNonQuery();

            #region audit
            if (Audit)
            {
                DbTransactionDetails dbtd = new DbTransactionDetails();
                dbtd.DbTransactionId = transactionManager.DbTransactionId;
                dbtd.Type = DbTransactionDetails.TYPE_UPDATE;
                dbtd.TableName = AbstractName;
                dbtd.IdentityColumn = "Id";
                dbtd.IdentityValue = Id.ToString();
                dbtd.Value = doc.OuterXml;
                DbTransactionDetails.Insert(transactionManager, dbtd);
            }
            #endregion

            EntityState = EntityStates.Clean;

            return true;
        }

        public static bool Update(EntityBase<T> entity)
        {
            TransactionManager tm = new TransactionManager(true, "Update " + entity.AbstractName);

            try
            {
                entity.Update(tm);
                tm.Commit();
            }
            catch (Exception ex)
            {
                tm.Rollback();

                throw ex;
            }

            return true;
        }

        public static bool Update(TransactionManager transactionManager, EntityBase<T> entity)
        {
            entity.Update(transactionManager);

            return true;
        }

        public bool Delete(Int32 id, TransactionManager transactionManager)
        {
            Object entity = Get(transactionManager, "Id = " + id);

            if (entity == null)
            {
                throw new Exception("Could not find the record");
            }

            return ((EntityBase<T>)entity).Delete(transactionManager);
        }
        
        public virtual bool Delete(TransactionManager transactionManager)
        {
            string strDeleteSql = "DELETE FROM " + Instance.AbstractName + " WHERE Id = " + Id + ";";

            //IList<DbTransactionDetails> list = DbTransactionDetails.Find(transactionManager, "Type='" + DbTransactionDetails.TYPE_INSERT + "' AND TableName='" + Instance.AbstractName + "' AND IdentityValue='" + this.Id + "'", "");

            //if (list != null && list.Count > 0)
            //{
            //    DbTransaction dbt = DbTransaction.GetById(transactionManager, list[0].DbTransactionId);

            //    if (dbt != null && dbt.CreatedBy != null)
            //    {
            //        throw new Exception("No delete permission.");
            //    }
            //}

            SqlCommand cmd = transactionManager.CreateCommand();
            cmd.CommandText = strDeleteSql;
            transactionManager.RecordAffected += cmd.ExecuteNonQuery();

            #region audit
            if (Audit)
            {
                DbTransactionDetails dbtd = new DbTransactionDetails();
                dbtd.DbTransactionId = transactionManager.DbTransactionId;
                dbtd.Type = DbTransactionDetails.TYPE_DELETE;
                dbtd.TableName = Instance.AbstractName;
                dbtd.IdentityColumn = "Id";
                dbtd.IdentityValue = Id.ToString();
                dbtd.Value = Serializer.Serialize(this);
                DbTransactionDetails.Insert(transactionManager, dbtd);
            }
            #endregion

            EntityState = EntityStates.Deleted;

            return true;
        }

        //Added by Rofiq
        public virtual bool Delete(string whereCluse,TransactionManager transactionManager)
        {
            string strDeleteSql = "DELETE FROM " + Instance.AbstractName + " WHERE " + whereCluse + ";";

            //IList<DbTransactionDetails> list = DbTransactionDetails.Find(transactionManager, "Type='" + DbTransactionDetails.TYPE_INSERT + "' AND TableName='" + Instance.AbstractName + "' AND IdentityValue='" + this.Id + "'", "");

            //if (list != null && list.Count > 0)
            //{
            //    DbTransaction dbt = DbTransaction.GetById(transactionManager, list[0].DbTransactionId);

            //    if (dbt != null && dbt.CreatedBy != null)
            //    {
            //        throw new Exception("No delete permission.");
            //    }
            //}

            SqlCommand cmd = transactionManager.CreateCommand();
            cmd.CommandText = strDeleteSql;
            transactionManager.RecordAffected += cmd.ExecuteNonQuery();

            #region audit
            if (Audit)
            {
                DbTransactionDetails dbtd = new DbTransactionDetails();
                dbtd.DbTransactionId = transactionManager.DbTransactionId;
                dbtd.Type = DbTransactionDetails.TYPE_DELETE;
                dbtd.TableName = Instance.AbstractName;
                dbtd.IdentityColumn = "Id";
                dbtd.IdentityValue = Id.ToString();
                dbtd.Value = Serializer.Serialize(this);
                DbTransactionDetails.Insert(transactionManager, dbtd);
            }
            #endregion

            EntityState = EntityStates.Deleted;

            return true;
        }
        public static bool Delete(TransactionManager transactionManager, EntityBase<T> entity)
        {
            return entity.Delete(transactionManager);
        }

        public static bool Delete(TransactionManager transactionManager, Int32 id)
        {
            Object entity = Get(transactionManager, "Id = " + id);

            if (entity == null)
            {
                throw new Exception("Could not find the record");
            }

            return ((EntityBase<T>)entity).Delete(transactionManager);
        }
        //Added By Rofiq
        public static bool Delete(TransactionManager transactionManager, string whereCluse)
        {

            Object entity = Get(transactionManager, whereCluse);

            if (entity == null)
            {
                return true;
                //throw new Exception("Could not find the record");
            }

            return ((EntityBase<T>)entity).Delete(whereCluse,transactionManager);
        }
        public static bool Delete(int id)
        {
            TransactionManager tm = new TransactionManager(true, "Delete " + Instance.AbstractName);

            try
            {
                Delete(tm, id);
                tm.Commit();
            }
            catch (Exception ex)
            {
                tm.Rollback();

                throw ex;
            }

            return true;
        }
    }
}
