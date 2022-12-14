using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using GITS.Hrms.Library.Data.Entity;

namespace GITS.Hrms.Library.Utility
{
    public class UIUtility
    {
        static ListItem[] months = new ListItem[]{new ListItem("January"), new ListItem("February"), new ListItem("March"), new ListItem("April"), new ListItem("May"), new ListItem("June"),
	                                          new ListItem("July"), new ListItem("August"), new ListItem("September"), new ListItem("October"), new ListItem("November"), new ListItem("December")};

        public UIUtility()
        {

        }
        public static String NumberInBangla(Int32 num)
        {
            string retDate = num.ToString();

            retDate = retDate.Replace("0", "০")
                .Replace("1", "১")
                .Replace("2", "২")
                .Replace("3", "৩")
                .Replace("4", "৪")
                .Replace("5", "৫")
                .Replace("6", "৬")
                .Replace("7", "৭")
                .Replace("8", "৮")
                .Replace("9", "৯");

            return retDate;
        }
        public static String DateTimeInBangla(DateTime date, bool isLongDate)
        {
            string retDate = string.Empty;
            if (isLongDate)
            {
                retDate = date.ToString("MMMM dd, yyyy");
            }
            else
            {
                retDate = date.ToString("dd/MM/yyyy");
            }
            retDate=retDate.Replace("0", "০")
                .Replace("1", "১")
                .Replace("2", "২")
                .Replace("3", "৩")
                .Replace("4", "৪")
                .Replace("5", "৫")
                .Replace("6", "৬")
                .Replace("7", "৭")
                .Replace("8", "৮")
                .Replace("9", "৯");
            if (isLongDate)
            {
                retDate= retDate.Replace("January", "জানুয়ারী")
                .Replace("February", "ফেব্রুয়ারী")
                .Replace("March", "মার্চ")
                .Replace("April", "এপ্রিল")
                .Replace("May", "মে")
                .Replace("June", "জুন")
                .Replace("July", "জুলাই")
                .Replace("August", "আগস্ট")
                .Replace("September", "সেপ্টেম্বর")
                .Replace("October", "অক্টোবর")
                .Replace("November", "নভেম্বর")
                .Replace("December", "ডিসেম্বর");
            }
            return retDate;
        }
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            // column names 
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()== typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        public static Int32 GetEmployeeID(String text)
        {
            Int32 id = 0;

            if (text != String.Empty)
            {
                text = text.Trim();

                if (text.Contains(":"))
                {
                    Int32.TryParse(text.Substring(0, text.IndexOf(":")), out id);
                }
                else
                {
                    Int32.TryParse(text, out id);
                }
            }

            return id;
        }

        public static string Format(Nullable<DateTime> value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.Value.ToString(Configuration.DateFormat);
            }
        }

        public static string Format(Nullable<Int32> value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.Value.ToString(Configuration.Int32Format);
            }
        }

        public static string Format(Nullable<Int64> value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.Value.ToString(Configuration.Int64Format);
            }
        }

        public static string Format(Nullable<Double> value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.Value.ToString(Configuration.DoubleFormat);
            }
        }

        public static void Fill(DropDownList dropDownList, String[] items)
        {
            dropDownList.Items.Clear();

            foreach (String item in items)
            {
                dropDownList.Items.Add(item);
            }
        }

        public static void FillCombo(DropDownList dropDownList, string dataTextField, string dataValueField, IEnumerable iCollection, int defaultValue, bool bHasBlank, bool bMandatory)
        {
            FillCombo(dropDownList, dataTextField, dataValueField, iCollection, bHasBlank, bMandatory);

            if (dropDownList.Items.FindByValue(defaultValue.ToString()) != null)
                dropDownList.SelectedValue = defaultValue.ToString();
        }

        public static void FillCombo(DropDownList dropDownList, string dataTextField, string dataValueField, IEnumerable iCollection, bool bHasBlank, bool bMandatory)
        {
            dropDownList.DataTextField = dataTextField;
            dropDownList.DataValueField = dataValueField;
            dropDownList.DataSource = iCollection;
            dropDownList.DataBind();

            if (bHasBlank)
            {
                ListItem oItem = new ListItem();
                if (!bMandatory)
                {
                    oItem.Value = "0";
                    oItem.Text = "";
                }
                dropDownList.Items.Insert(0, oItem);
            }
        }

        public static void FillCombo(DropDownList dropDownList, string[] sArray, bool bHasBlank, bool bMandatory)
        {
            dropDownList.Items.Clear();
            ListItem oItem = new ListItem();

            if (bHasBlank)
            {
                if (!bMandatory)
                {
                    oItem.Value = "0";
                    oItem.Text = "";
                }
                dropDownList.Items.Add(oItem);
            }

            for (int i = 0; i <= sArray.GetUpperBound(0); i++)
            {
                oItem = new ListItem();
                oItem.Text = sArray[i];
                oItem.Value = Convert.ToString(i + 1);
                dropDownList.Items.Add(oItem);
            }
        }

        public static String GetDurationDisplayName(Int32 duration, String unit)
        {
            return duration + " " + unit;
        }

        public static Int32 GetDurationValue(String displayName)
        {
            if (displayName == null || displayName.Trim() == "")
            {
                return 0;
            }

            return DBUtility.ToInt32(displayName.Split(new Char[] { ' ' })[0]);
        }

        public static Nullable<Int32> GetNullableDurationValue(String displayName)
        {
            if (displayName == null || displayName.Trim() == "")
            {
                return null;
            }

            return DBUtility.ToInt32(displayName.Split(new Char[] { ' ' })[0]);
        }

        public static String GetDurationUnit(String displayName)
        {
            if (displayName == null || displayName.Trim() == "")
            {
                return null;
            }

            return displayName.Split(new Char[] { ' ' })[1];
        }

        public static Nullable<DateTime> ToNullableDateTime(String value)
        {
            if (value == null || value.ToString() == "" || value.ToString() == "&nbsp;")
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

        public static Nullable<Double> ToNullableDouble(String value)
        {
            if (value == null || value.ToString() == "" || value.ToString() == "&nbsp;")
            {
                return null;
            }
            else
            {
                if (value.StartsWith("("))
                {
                    return -Convert.ToDouble(value.Replace("(", "").Replace(")", ""));
                }
                else
                {
                    return Convert.ToDouble(value);
                }
            }
        }

        public static Int32 ToInt32(DateTime value)
        {
            return new TimeSpan(value.Date.Ticks).Days;
        }

        public static Nullable<DateTime> ToDateTime(Nullable<Int32> value)
        {
            if (value != null)
            {
                return new DateTime(new TimeSpan((Int32)value, 0, 0, 0).Ticks);
            }

            return null;
        }

        public static DateTime ToDateTime(Int32 value)
        {
            return new DateTime(new TimeSpan(value, 0, 0, 0).Ticks);
        }

        public static ListItem[] GetYearList(int CurrentYear, int Count)
        {
            ListItem[] list = new ListItem[Count * 2 + 1];
            int c = 0;

            for (int i = CurrentYear - Count; i <= CurrentYear + Count; i++)
            {
                list[c++] = new ListItem(i.ToString());
            }

            return list;
        }

        public static void FillYear(DropDownList combo, int CurrentYear, int Count)
        {
            combo.Items.AddRange(GetYearList(CurrentYear, Count));
        }

        public static void FillMonth(DropDownList combo)
        {
            combo.Items.AddRange(months);
        }

        public static void Transfer(System.Web.UI.Page page, String path)
        {
            //string script = "<script language='javascript'>Transfer('" + page.Request.ApplicationPath + path + "');</script>";            
            //string script = "<script language='javascript'>Transfer('" + page.Request.Url.AbsoluteUri.Replace(page.Request.Url.AbsolutePath, "") + path + "');</script>";
            string script = "<script language='javascript'>document.location.href = '" + path + "';</script>";

            //page.ClientScript.RegisterStartupScript(page.GetType(), "Transfer", script);
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "Transfer", script, false);
        }

        public static void LoadEnums(DropDownList combo, Type type, bool hasBlankItem, bool hasAllItem, bool removeUnderScore)
        {
            string[] enumData = Enum.GetNames(type);
            Array valueData = Enum.GetValues(type);

            DictionaryEntry[] dicEntry = null;
            int index = 0;

            if (hasBlankItem)
            {
                dicEntry = new DictionaryEntry[enumData.Length + 1];
                dicEntry.SetValue(new DictionaryEntry(0, ""), index);
                index++;
            }
            else if (hasAllItem)
            {
                dicEntry = new DictionaryEntry[enumData.Length + 1];
                dicEntry.SetValue(new DictionaryEntry(0, "All"), index);
                index++;
            }
            else
            {
                dicEntry = new DictionaryEntry[enumData.Length];
            }

            for (int i = 0; i < enumData.Length; i++)
            {
                if (removeUnderScore)
                {
                    string value = valueData.GetValue(i).ToString();
                    value = value.Replace("_", " ");
                    dicEntry[index] = new DictionaryEntry((int)valueData.GetValue(i), value);
                }
                else
                {
                    dicEntry[index] = new DictionaryEntry((int)valueData.GetValue(i), valueData.GetValue(i));
                }

                index++;
            }

            combo.DataSource = dicEntry;
            combo.DataTextField = "Value";
            combo.DataValueField = "Key";
            combo.DataBind();

            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        public static void LoadEnums(DropDownList combo, Type type, bool hasBlankItem, bool hasAllItem, bool removeUnderScore, string[] removeValues)
        {
            if (removeValues == null || removeValues.Length <= 0)
            {
                LoadEnums(combo, type, hasBlankItem, hasAllItem, removeUnderScore);
                return;
            }

            string[] enumData = Enum.GetNames(type);
            Array valueData = Enum.GetValues(type);

            DictionaryEntry[] dicEntry = null;
            int index = 0;

            if (hasBlankItem)
            {
                dicEntry = new DictionaryEntry[enumData.Length + 1 - removeValues.Length];
                dicEntry.SetValue(new DictionaryEntry(0, ""), index);
                index++;
            }
            else if (hasAllItem)
            {
                dicEntry = new DictionaryEntry[enumData.Length + 1 - removeValues.Length];
                dicEntry.SetValue(new DictionaryEntry(0, "All"), index);
                index++;
            }
            else
            {
                dicEntry = new DictionaryEntry[enumData.Length - removeValues.Length];
            }

            for (int i = 0; i < enumData.Length; i++)
            {
                if (Array.IndexOf(removeValues, DBUtility.ToString((int)valueData.GetValue(i))) >= 0)
                    continue;

                if (removeUnderScore)
                {
                    string value = valueData.GetValue(i).ToString();
                    value = value.Replace("_", " ");
                    dicEntry[index] = new DictionaryEntry((int)valueData.GetValue(i), value);
                }
                else
                {
                    dicEntry[index] = new DictionaryEntry((int)valueData.GetValue(i), valueData.GetValue(i));
                }

                index++;
            }

            combo.DataSource = dicEntry;
            combo.DataTextField = "Value";
            combo.DataValueField = "Key";
            combo.DataBind();

            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        public static string ToDatabaseFormat(Nullable<DateTime> value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.Value.ToString(Configuration.DatabaseDateFormat);
            }
        }

        public static String GetLocationLabel(String labelName)
        {
            Property property = Property.GetByName(labelName.Trim().ToUpper() + " LIST");

            if (property == null)
            {
                return labelName;
            }
            else
            {
                return property.DisplayName;
            }
        }

        public static String GetHeader(int? zoneId, int? subzoneId, int? regionId, int? branchCode)
        {
            String header = "";

            Zone zone = Zone.GetById(DBUtility.ToInt32(zoneId));

            if (zone != null)
            {
                header += GetLocationLabel("Zone") + ": " + zone.Name + ", ";
                Subzone subzone = Subzone.GetById(DBUtility.ToInt32(subzoneId));

                if (subzone != null)
                {
                    header += GetLocationLabel("District") + ": " + subzone.Name + ", ";
                    Region region = Region.GetById(DBUtility.ToInt32(regionId));

                    if (region != null)
                    {
                        header += GetLocationLabel("Region") + ": " + region.Name + ", ";
                        Branch branch = Branch.GetByCode(DBUtility.ToInt32(branchCode));

                        if (branch != null)
                        {
                            header += GetLocationLabel("Branch") + ": " + branch.Name + ", ";
                        }
                    }
                }
            }

            if (header.Length > 2)
            {
                header = header.Substring(0, header.Length - 2);
            }

            return header;
        }

        public static string GetPositionHeader(int? gradeId, int? designationId)
        {
            String header = "";

            H_Grade grade = H_Grade.GetById(DBUtility.ToInt32(gradeId));

            if (grade != null)
            {
                header += GetLocationLabel("Grade" + ": " + grade.Name + ", ");
                H_Designation designation = H_Designation.GetById(DBUtility.ToInt32(designationId));

                if (designation != null)
                {
                    header += GetLocationLabel("Designation") + ": " + designation.Name + ", ";
                }
            }

            if (header.Length > 2)
            {
                header = header.Substring(0, header.Length - 2);
            }

            return header;
        }

        public static String GetAccessLevel(string login)
        {
            string filter = string.Empty;

            UserLocation uLocation = UserLocation.FindByLogin(login, "")[0];

            if (uLocation.ZoneId != null)
            {
                filter = " AND Id In (Select H_EmployeeId from H_EmployeeBranch Where BranchId In (Select Branch.Id from Branch Where Branch.RegionId In" +
                         " (Select Region.Id from Region Where Region.SubzoneId in ( Select Id from Subzone where ZoneId=" + uLocation.ZoneId + "))))";
            }
            if (uLocation.SubzoneId != null)
            {
                filter = " AND Id In (Select H_EmployeeId from H_EmployeeBranch Where BranchId In (Select Branch.Id from Branch Where Branch.RegionId In" +
                         " (Select Region.Id from Region Where Region.SubzoneId = " + uLocation.SubzoneId + ")))";
            }
            else if (uLocation.RegionId != null)
            {
                filter = " AND Id In (Select H_EmployeeId from H_EmployeeBranch Where BranchId In (Select Id from Branch" +
                         " where Branch.RegionId = " + uLocation.RegionId + "))";
            }
            else if (uLocation.BranchId != null)
            {
                filter = " AND ID IN (SELECT H_EmployeeId FROM H_EmployeeBranch WHERE BranchId = " + uLocation.BranchId + ")";
            }
            else
            {
                filter = string.Empty;
            }

            return filter;
        }
        public static string GetDifference(DateTime startDate, DateTime endDate)
        {
            Int32 months = endDate.Month - startDate.Month;
            Int32 years = endDate.Year - startDate.Year;
            Int32 days = endDate.Day - startDate.Day;
            if (days < 0)
            {
                months = months - 1;
                days = days + 30;
            }
            if (months < 0)
            {
                years = years - 1;
                months = months + 12;
            }
            return years.ToString() + " Year(s) " + months.ToString() + " Month(s) ";
            //TimeSpan period = endDate.AddDays(1) - startDate;
            //DateTime date = new DateTime(period.Ticks);
            //int totalYears = date.Year - 1;
            //int totalMonths = ((date.Year - 1) * 12) + date.Month - 1;
            //int totalWeeks = (int)period.TotalDays / 7;

            //return totalYears.ToString() + " years " + totalMonths.ToString() + " months";
        }

        public static string ConvertEnamValueToString(Type eEnum, int value)
        {
            return Enum.GetName(eEnum, value);
        }
    }
}