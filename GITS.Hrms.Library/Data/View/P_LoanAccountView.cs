using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.View
{
    [Serializable]
    [Class(ClassAttribute.Attributes.View)]
    public class P_LoanAccountView : ViewBase<P_LoanAccountView>
    {
        private DateTime _DisbursDate;
        private Double _LoanAmount;
        private Double _InterestRate;
        private Double _InterestAmount;
        private Double _TotalAmount;
        private String _Employee;
        private Int32 _Code;
        private String _LoanType;


        public P_LoanAccountView()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[P_LoanAccountView]"; }
		}

        protected override P_LoanAccountView Map(SqlDataReader dataReader)
		{
            P_LoanAccountView view = new P_LoanAccountView();

			view.Id = DBUtility.ToInt32(dataReader["Id"]);
            view.DisbursDate = DBUtility.ToDateTime(dataReader["DisbursDate"]);
            view.LoanAmount = DBUtility.ToDouble(dataReader["LoanAmount"]);
            view.InterestRate = DBUtility.ToDouble(dataReader["InterestRate"]);
            view.InterestAmount = DBUtility.ToDouble(dataReader["InterestAmount"]);
            view.TotalAmount = DBUtility.ToDouble(dataReader["TotalAmount"]);
            view.Employee = DBUtility.ToString(dataReader["Employee"]);
            view.Code = DBUtility.ToInt32(dataReader["Code"]);
            view.LoanType = DBUtility.ToString(dataReader["LoanType"]);

			return view;
		}

        public DateTime DisbursDate
        {
            get { return _DisbursDate; }
            set { _DisbursDate = value; }
        }

        public Double LoanAmount
        {
            get { return _LoanAmount; }
            set { _LoanAmount = value; }
        }
        public Double InterestRate
        {
            get { return _InterestRate; }
            set { _InterestRate = value; }
        }
        public Double InterestAmount
        {
            get { return _InterestAmount; }
            set { _InterestAmount = value; }
        }
        public Double TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        public String Employee
        {
            get { return _Employee; }
            set { _Employee = value; }
        }
        public Int32 Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public String LoanType
        {
            get { return _LoanType; }
            set { _LoanType = value; }
        }
    }
}
