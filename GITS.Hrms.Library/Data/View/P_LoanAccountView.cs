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
            get { return this._DisbursDate; }
            set { this._DisbursDate = value; }
        }

        public Double LoanAmount
        {
            get { return this._LoanAmount; }
            set { this._LoanAmount = value; }
        }
        public Double InterestRate
        {
            get { return this._InterestRate; }
            set { this._InterestRate = value; }
        }
        public Double InterestAmount
        {
            get { return this._InterestAmount; }
            set { this._InterestAmount = value; }
        }
        public Double TotalAmount
        {
            get { return this._TotalAmount; }
            set { this._TotalAmount = value; }
        }
        public String Employee
        {
            get { return this._Employee; }
            set { this._Employee = value; }
        }
        public Int32 Code
        {
            get { return this._Code; }
            set { this._Code = value; }
        }
        public String LoanType
        {
            get { return this._LoanType; }
            set { this._LoanType = value; }
        }
    }
}
