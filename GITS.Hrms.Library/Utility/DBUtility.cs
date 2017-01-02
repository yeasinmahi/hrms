using System;
using System.Data;

namespace GITS.Hrms.Library.Utility
{
    public class DBUtility
    {
        public DBUtility()
        {
        }

        public static String DecryptConnectionString(string cipherConnectionString)
        {
            string[] parts = cipherConnectionString.Split(new char[] { ';' });
            int p = -1;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].TrimStart().ToLower().StartsWith("password="))
                {
                    p = i;
                    break;
                }
            }

            if (p >= 0)
            {
                parts[p] = "Password=" + Cryptography.Decrypt(parts[p].TrimStart().Remove(0, 9));

                string connectionString = "";

                for (int i = 0; i < parts.Length; i++)
                {
                    connectionString += parts[i] + ";";
                }

                return connectionString;
            }
            else
            {
                return cipherConnectionString;
            }
        }

        public static Nullable<Boolean> ToNullableBoolean(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return null;
            }
            else if (value.ToString().ToLower() == "false" || value.ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Boolean ToBoolean(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "" || value.ToString().ToLower() == "false" || value.ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Nullable<DateTime> ToNullableDateTime(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(value.ToString());
            }
        }

        public static Nullable<DateTime> ToNullableDateTime(String value)
        {
            if (value == null || value.ToString() == "")
            {
                return null;
            }
            else
            {
                DateTime d;

                if (DateTime.TryParse(value, out d))
                {
                    return d;
                }
                else
                {
                    return null;
                }
            }
        }

        public static DateTime ToDateTime(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return new DateTime();
            }
            else
            {
                return Convert.ToDateTime(value.ToString());
            }
        }

        public static DateTime ToDateTime(String value)
        {
            if (value == null || value.ToString() == "")
            {
                return new DateTime();
            }
            else
            {
                DateTime d;

                if (DateTime.TryParse(value, out d))
                {
                    return d;
                }
                else
                {
                    return new DateTime();
                }
            }
        }

        public static Nullable<Int32> ToNullableInt32(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        public static Int32 ToInt32(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        public static Nullable<Int64> ToNullableInt64(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return null;
            }
            else
            {
                return Convert.ToInt64(value);
            }
        }

        public static Int64 ToInt64(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(value);
            }
        }

        public static Nullable<Single> ToNullableSingle(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return null;
            }
            else
            {
                return Convert.ToSingle(value);
            }
        }

        public static Single ToSingle(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToSingle(value);
            }
        }

        public static Nullable<Double> ToNullableDouble(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return null;
            }
            else
            {
                return Convert.ToDouble(value);
            }
        }

        public static Double ToDouble(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(value);
            }
        }

        public static Double ToDouble(String value)
        {
            if (value == null || value.ToString() == "")
            {
                return 0;
            }
            else
            {
                Double d;

                if (Double.TryParse(value, out d))
                {
                    return d;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static String ToNullableString(Object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == "")
            {
                return null;
            }
            else
            {
                return value.ToString();
            }
        }

        public static String ToString(Object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return "";
            }
            else
            {
                return value.ToString();
            }
        }

        public static DataTable FullJoin(DataTable First, DataTable Second, DataColumn[] FJC, DataColumn[] SJC)
        {
            //Create Empty Table
            DataTable table = new DataTable("Join");
            if (First.TableName == Second.TableName)
            {
                First.TableName = "First";
                Second.TableName = "Second";
            }

            // Use a DataSet to leverage DataRelation
            using (DataSet ds = new DataSet())
            {
                //Add Copy of Tables
                ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });

                //Identify Joining Columns from First
                DataColumn[] parentcolumns = new DataColumn[FJC.Length];

                for (int i = 0; i < parentcolumns.Length; i++)
                {
                    parentcolumns[i] = ds.Tables[0].Columns[FJC[i].ColumnName];
                }

                //Identify Joining Columns from Second
                DataColumn[] childcolumns = new DataColumn[SJC.Length];

                for (int i = 0; i < childcolumns.Length; i++)
                {
                    childcolumns[i] = ds.Tables[1].Columns[SJC[i].ColumnName];
                }

                //Create DataRelation
                DataRelation r = new DataRelation(string.Empty, parentcolumns, childcolumns, false);
                ds.Relations.Add(r);

                //Create Columns for JOIN table
                for (int i = 0; i < First.Columns.Count; i++)
                {
                    table.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);
                }

                for (int i = 0; i < Second.Columns.Count; i++)
                {
                    //Beware Duplicates
                    if (table.Columns.Contains(Second.Columns[i].ColumnName) == false)
                    {
                        table.Columns.Add(Second.Columns[i].ColumnName, Second.Columns[i].DataType);
                    }
                    else
                    {
                        table.Columns.Add(Second.Columns[i].ColumnName + "_Second", Second.Columns[i].DataType);
                    }
                }

                //Loop through First table
                table.BeginLoadData();

                foreach (DataRow firstrow in ds.Tables[0].Rows)
                {
                    //Get "joined" rows
                    DataRow[] childrows = firstrow.GetChildRows(r);
                    object[] parentarray = firstrow.ItemArray;

                    if (childrows != null && childrows.Length > 0)
                    {
                        foreach (DataRow secondrow in childrows)
                        {
                            object[] secondarray = secondrow.ItemArray;
                            object[] joinarray = new object[parentarray.Length + secondarray.Length];

                            Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);
                            Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);

                            table.LoadDataRow(joinarray, true);
                        }
                    }
                    else
                    {
                        object[] secondarray = ds.Tables[1].NewRow().ItemArray;
                        object[] joinarray = new object[parentarray.Length + secondarray.Length];

                        Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);
                        Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);

                        table.LoadDataRow(joinarray, true);
                    }
                }

                foreach (DataRow secondrow in ds.Tables[1].Rows)
                {
                    DataRow[] parentrows = secondrow.GetParentRows(r);

                    if (parentrows == null || parentrows.Length == 0)
                    {
                        object[] parentarray = ds.Tables[0].NewRow().ItemArray;
                        object[] secondarray = secondrow.ItemArray;
                        object[] joinarray = new object[parentarray.Length + secondarray.Length];

                        Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);
                        Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);

                        table.LoadDataRow(joinarray, true);
                    }
                }

                table.EndLoadData();
            }

            return table;
        }

        public static DataTable Join(DataTable First, DataTable Second, DataColumn[] FJC, DataColumn[] SJC)
        {
            //Create Empty Table
            DataTable table = new DataTable("Join");
            if (First.TableName == Second.TableName)
            {
                First.TableName = "First";
                Second.TableName = "Second";
            }

            // Use a DataSet to leverage DataRelation
            using (DataSet ds = new DataSet())
            {
                //Add Copy of Tables
                ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });

                //Identify Joining Columns from First
                DataColumn[] parentcolumns = new DataColumn[FJC.Length];

                for (int i = 0; i < parentcolumns.Length; i++)
                {
                    parentcolumns[i] = ds.Tables[0].Columns[FJC[i].ColumnName];
                }

                //Identify Joining Columns from Second
                DataColumn[] childcolumns = new DataColumn[SJC.Length];

                for (int i = 0; i < childcolumns.Length; i++)
                {
                    childcolumns[i] = ds.Tables[1].Columns[SJC[i].ColumnName];
                }

                //Create DataRelation
                DataRelation r = new DataRelation(string.Empty, parentcolumns, childcolumns, false);
                ds.Relations.Add(r);

                //Create Columns for JOIN table
                for (int i = 0; i < First.Columns.Count; i++)
                {
                    table.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);
                }

                for (int i = 0; i < Second.Columns.Count; i++)
                {
                    //Beware Duplicates
                    if (table.Columns.Contains(Second.Columns[i].ColumnName) == false)
                    {
                        table.Columns.Add(Second.Columns[i].ColumnName, Second.Columns[i].DataType);
                    }
                    else
                    {
                        table.Columns.Add(Second.Columns[i].ColumnName + "_Second", Second.Columns[i].DataType);
                    }
                }

                //Loop through First table
                table.BeginLoadData();

                foreach (DataRow firstrow in ds.Tables[0].Rows)
                {
                    //Get "joined" rows
                    DataRow[] childrows = firstrow.GetChildRows(r);

                    if (childrows != null && childrows.Length > 0)
                    {
                        object[] parentarray = firstrow.ItemArray;

                        foreach (DataRow secondrow in childrows)
                        {
                            object[] secondarray = secondrow.ItemArray;
                            object[] joinarray = new object[parentarray.Length + secondarray.Length];

                            Array.Copy(parentarray, 0, joinarray, 0, parentarray.Length);
                            Array.Copy(secondarray, 0, joinarray, parentarray.Length, secondarray.Length);

                            table.LoadDataRow(joinarray, true);
                        }
                    }
                }

                table.EndLoadData();
            }

            return table;
        }

        public static DataTable Join(DataTable First, DataTable Second, DataColumn FJC, DataColumn SJC)
        {
            return Join(First, Second, new DataColumn[] { FJC }, new DataColumn[] { SJC });
        }

        public static DataTable Join(DataTable First, DataTable Second, string FJC, string SJC)
        {
            return Join(First, Second, new DataColumn[] { First.Columns[FJC] }, new DataColumn[] { First.Columns[SJC] });
        }

        private static bool RowEqual(object[] Values, object[] OtherValues)
        {
            if (Values == null)
            {
                return false;
            }

            for (int i = 0; i < Values.Length; i++)
            {
                if (!Values[i].Equals(OtherValues[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static DataTable Distinct(DataTable Table, DataColumn[] Columns)
        {
            //Empty table
            DataTable table = new DataTable("Distinct");

            //Sort variable
            string sort = string.Empty;

            //Add Columns & Build Sort expression
            for (int i = 0; i < Columns.Length; i++)
            {
                table.Columns.Add(Columns[i].ColumnName, Columns[i].DataType);
                sort += Columns[i].ColumnName + ",";
            }

            //Select all rows and sort
            DataRow[] sortedrows = Table.Select(string.Empty, sort.Substring(0, sort.Length - 1));
            object[] currentrow = null;
            object[] previousrow = null;

            table.BeginLoadData();
            foreach (DataRow row in sortedrows)
            {
                //Current row
                currentrow = new object[Columns.Length];

                for (int i = 0; i < Columns.Length; i++)
                {
                    currentrow[i] = row[Columns[i].ColumnName];
                }

                //Match Current row to previous row
                if (!RowEqual(previousrow, currentrow))
                {
                    table.LoadDataRow(currentrow, true);
                }

                //Previous row
                previousrow = new object[Columns.Length];

                for (int i = 0; i < Columns.Length; i++)
                {
                    previousrow[i] = row[Columns[i].ColumnName];
                }
            }

            table.EndLoadData();

            return table;
        }

        public static DataTable Project(DataTable Table, DataColumn[] Columns, bool Include)
        {
            DataTable table = Table.Copy();
            table.TableName = "Project";

            int columns_to_remove = Include ? (Table.Columns.Count - Columns.Length) : Columns.Length;
            string[] columns = new String[columns_to_remove];
            int z = 0;

            for (int i = 0; i < table.Columns.Count; i++)
            {
                string column_name = table.Columns[i].ColumnName;
                bool is_in_list = false;

                for (int x = 0; x < Columns.Length; x++)
                {
                    if (column_name == Columns[x].ColumnName)
                    {
                        is_in_list = true;
                        break;
                    }
                }

                if (is_in_list ^ Include)
                {
                    columns[z++] = column_name;
                }
            }

            foreach (string s in columns)
            {
                table.Columns.Remove(s);
            }

            return Distinct(table, Columns);
        }

        public static DataTable Project(DataTable Table, DataColumn[] Columns)
        {
            return Project(Table, Columns, true);
        }

        public static DataTable Project(DataTable Table, params string[] Columns)
        {
            DataColumn[] columns = new DataColumn[Columns.Length];

            for (int i = 0; i < Columns.Length; i++)
            {
                columns[i] = Table.Columns[Columns[i]];
            }

            return Project(Table, columns, true);
        }

        public static DataTable Project(DataTable Table, bool Include, params string[] Columns)
        {
            DataColumn[] columns = new DataColumn[Columns.Length];

            for (int i = 0; i < Columns.Length; i++)
            {
                columns[i] = Table.Columns[Columns[i]];
            }

            return Project(Table, columns, Include);
        }

        public static DataTable GroupBy(DataTable Table, DataColumn[] Grouping, string[] AggregateExpressions, string[] ExpressionNames, Type[] Types)
        {
            if (Table.Rows.Count == 0)
            {
                return Table;
            }

            DataTable table = Project(Table, Grouping, true);
            table.TableName = "GroupBy";

            for (int i = 0; i < ExpressionNames.Length; i++)
            {
                table.Columns.Add(ExpressionNames[i], Types[i]);
            }

            foreach (DataRow row in table.Rows)
            {
                string filter = string.Empty;

                for (int i = 0; i < Grouping.Length; i++)
                {
                    string columnname = Grouping[i].ColumnName;
                    object o = row[columnname];

                    if (o == DBNull.Value)
                    {
                        filter += "ISNULL([" + columnname + "], '')='' AND ";
                    }
                    else if (o is string)
                    {
                        filter += "[" + columnname + "]='" + o.ToString() + "' AND ";
                    }
                    else if (o is DateTime)
                    {
                        filter += "[" + columnname + "]=#" + ((DateTime)o).ToLongDateString() + " " + ((DateTime)o).ToLongTimeString() + "# AND ";
                    }
                    else
                    {
                        filter += "[" + columnname + "]=" + o.ToString() + " AND ";
                    }
                }

                filter = filter.Substring(0, filter.Length - 5);

                for (int i = 0; i < AggregateExpressions.Length; i++)
                {
                    object computed = Table.Compute(AggregateExpressions[i], filter);
                    row[ExpressionNames[i]] = computed;
                }
            }

            return table;
        }
    }
}
