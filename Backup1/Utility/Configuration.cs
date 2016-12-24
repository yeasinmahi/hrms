using System;
using System.Configuration;
using System.Data.SqlClient;
using Asa.Hrms.Data;
using Asa.Hrms.Data.Entity;
using System.Web;

namespace Asa.Hrms.Utility
{
    /// <summary>
    /// Summary description for Configuration
    /// </summary>
    public class Configuration
    {
        protected static string _StandardProvider = "Standard";
        protected static string _SelectedCountry = null;
        protected static string _Provider = null;
        protected static string _Version = null;
        protected static string _DBUserId = null;
        protected static string _UploadPath = null;

        private static Nullable<Int32> _DecimalPlace = null;
        private static Nullable<Int32> _DaysInYear = null;

        private static String _ReportExtension = null;

        private static Nullable<Boolean> _EnableAudit = null;
        private static Nullable<Int32> _AuditDuration = null;

        public static Boolean EnableAudit
        {
            get
            {
                if (_EnableAudit == null)
                {
                    Config p_Config = Config.GetByKey(Config.KEY_ENABLE_AUDIT);

                    if (p_Config != null)
                    {
                        _EnableAudit = DBUtility.ToNullableBoolean(p_Config.Value);
                    }
                }

                return _EnableAudit.Value;
            }
        }

        public static Int32 AuditDuration
        {
            get
            {
                if (_AuditDuration == null)
                {
                    Config p_Config = Config.GetByKey(Config.KEY_AUDIT_DURATION);

                    if (p_Config != null)
                    {
                        _AuditDuration = DBUtility.ToNullableInt32(p_Config.Value);
                    }
                }

                return _AuditDuration.Value;
            }
        }

        public static Int32 StateId
        {
            get { return DBUtility.ToInt32(HttpContext.Current.Session["StateId"]); }
            set { HttpContext.Current.Session["StateId"] = value; }
        }

        public static Int32 DivisionId
        {
            get { return DBUtility.ToInt32(HttpContext.Current.Session["DivisionId"]); }
            set { HttpContext.Current.Session["DivisionId"] = value; }
        }

        public static Int32 RegionId
        {
            get { return DBUtility.ToInt32(HttpContext.Current.Session["RegionId"]); }
            set { HttpContext.Current.Session["RegionId"] = value; }
        }

        public static Int32 DistrictId
        {
            get { return DBUtility.ToInt32(HttpContext.Current.Session["DistrictId"]); }
            set { HttpContext.Current.Session["DistrictId"] = value; }
        }

        public static Int32 BranchCode
        {
            get { return DBUtility.ToInt32(HttpContext.Current.Session["BranchCode"]); }
            set { HttpContext.Current.Session["BranchCode"] = value; }
        }

        public static Int32 GradeId
        {
            get { return DBUtility.ToInt32(HttpContext.Current.Session["GradeId"]); }
            set { HttpContext.Current.Session["GradeId"] = value; }
        }

        public static Int32 DesignationId
        {
            get { return DBUtility.ToInt32(HttpContext.Current.Session["DesignationId"]); }
            set { HttpContext.Current.Session["DesignationId"] = value; }
        }

        public static Int32 Year
        {
            get 
            {
                if (HttpContext.Current.Session["Year"] == null)
                {
                    return DateTime.Now.Year;
                }
                else
                {
                    return DBUtility.ToInt32(HttpContext.Current.Session["Year"]);
                }
            }

            set { HttpContext.Current.Session["Year"] = value; }
        }

        public static Int32 Month
        {
            get
            {
                if (HttpContext.Current.Session["Month"] == null)
                {
                    return DateTime.Now.Month - 1;
                }
                else
                {
                    return DBUtility.ToInt32(HttpContext.Current.Session["Month"]);
                }
            }

            set { HttpContext.Current.Session["Month"] = value; }
        }

        public static DateTime StartDate
        {
            get
            {
                if (HttpContext.Current.Session["StartDate"] == null)
                {
                    return new DateTime(DateTime.Now.Year, 1, 1);
                }
                else
                {
                    return DBUtility.ToDateTime(HttpContext.Current.Session["StartDate"]);
                }
            }

            set { HttpContext.Current.Session["StartDate"] = value; }
        }

        public static DateTime EndDate
        {
            get
            {
                if (HttpContext.Current.Session["EndDate"] == null)
                {
                    return new DateTime(DateTime.Now.Year, 12, 31);
                }
                else
                {
                    return DBUtility.ToDateTime(HttpContext.Current.Session["EndDate"]);
                }
            }

            set { HttpContext.Current.Session["EndDate"] = value; }
        }

        public static String ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Asa.Hrms.Data.ConnectionString"].ConnectionString;
            }
        }

        public static String DataSourceName
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Asa.Hrms.Data.DataSourceName"].ConnectionString;
            }
        }

        public static String DatabaseName
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Asa.Hrms.Data.DatabaseName"].ConnectionString;
            }
        }

        public static string Provider
        {
            get
            {
                if (_Provider == null)
                {
                    _Provider = ConfigurationManager.ConnectionStrings["Asa.Hrms.Data.Provider"].ConnectionString;
                }

                return _Provider;
            }
        }

        public static string Version
        {
            get
            {
                if (_Version == null)
                {
                    _Version = ConfigurationManager.ConnectionStrings["Asa.Hrms.Data.Version"].ConnectionString;
                }

                return _Version;
            }
        }

        public static String DatabaseUserId
        {
            get
            {
                _DBUserId = ConfigurationManager.ConnectionStrings["Asa.Hrms.Data.DatabaseUserId"].ConnectionString;
                return _DBUserId;
            }
        }

        public static String DatabasePassword
        {
            get
            {
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(DBUtility.DecryptConnectionString(ConnectionString));
                return connectionStringBuilder.Password;
            }
        }

        public static string UploadPath
        {
            get
            {
                if (_UploadPath == null)
                {
                    _UploadPath = ConfigurationManager.AppSettings["UploadPath"];
                }

                return _UploadPath;
            }
        }

        public static Int32 DecimalPlace
        {
            get
            {
                if (_DecimalPlace == null)
                {
                    Config p_Config = Config.GetByKey(Config.KEY_DECIMAL_PLACE);

                    if (p_Config != null)
                    {
                        _DecimalPlace = DBUtility.ToNullableInt32(p_Config.Value);
                    }
                }

                return _DecimalPlace.Value;
            }
        }

        public static Int32 DaysInYear
        {
            get
            {
                if (_DaysInYear == null)
                {
                    Config p_Config = Config.GetByKey(Config.KEY_DAYS_IN_YEAR);

                    if (p_Config != null)
                    {
                        _DaysInYear = DBUtility.ToNullableInt32(p_Config.Value);
                    }
                }

                return _DaysInYear.Value;
            }
        }

        public static String ReportExtension
        {
            get
            {
                if (_ReportExtension == null)
                {
                    Config p_Config = Config.GetByKey(Config.KEY_REPORT_EXTENSION);

                    if (p_Config != null)
                    {
                        _ReportExtension = p_Config.Value;
                    }
                }

                return _ReportExtension;
            }
        }

        public static string StandardProvider
        {
            get { return _StandardProvider; }
        }

        public static String ExcelIntegerFormat
        {
            get { return "_(* #,##0_);_(* \\(#,##0\\);_(* \"-\"??_);_(@_)"; }
        }

        public static String ExcelDoubleFormat
        {
            get
            {
                if (DecimalPlace == 0)
                {
                    return ExcelIntegerFormat;
                }
                else
                {
                    String places = "";

                    for (Int32 i = 1; i <= DecimalPlace; i++)
                    {
                        places += "0";
                    }

                    return "_(* #,##0." + places + "_);_(* \\(#,##0." + places + "\\);_(* \"-\"??_);_(@_)";
                }
            }
        }

        public static String ExcelDecimalFormat
        {
            get { return "_(* #,##0.00_);_(* \\(#,##0.00\\);_(* \"-\"??_);_(@_)"; }
        }

        public static String ExcelDateFormat
        {
            get
            {
                if (ReportExtension == null || ReportExtension == ".xls")
                {
                    return "ddd, dd/MM/yyyy";
                }
                else
                {
                    return "Short Date";
                }
            }
        }

        public static string DatabaseDateFormat
        {
            get
            {
                return "MM/dd/yyyy";
            }
        }

        public static string DateFormat
        {
            get
            {
                return "dd/MM/yyyy";
            }
        }

        public static string DoubleFormat
        {
            get
            {
                return "0";
            }
        }

        public static string Int32Format
        {
            get
            {
                return "0";
            }
        }

        public static string Int64Format
        {
            get
            {
                return "0";
            }
        }

        public Configuration()
        {
        }
    }
}