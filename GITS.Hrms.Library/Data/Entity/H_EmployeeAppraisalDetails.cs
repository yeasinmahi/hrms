using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_EmployeeAppraisalDetails : EntityBase<H_EmployeeAppraisalDetails>
    {
        private Int32 _H_EmployeeAppraisalMasterId;
        private Int32 _H_AppraisalQuestionId;
        private Int32 _Marks;

        public H_EmployeeAppraisalDetails()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_EmployeeAppraisalDetails]"; }
		}

        protected override H_EmployeeAppraisalDetails Map(SqlDataReader dataReader)
		{
            H_EmployeeAppraisalDetails entity = new H_EmployeeAppraisalDetails();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_EmployeeAppraisalMasterId = DBUtility.ToInt32(dataReader["H_EmployeeAppraisalMasterId"]);
            entity.H_AppraisalQuestionId = DBUtility.ToInt32(dataReader["H_AppraisalQuestionId"]);
            entity.Marks = DBUtility.ToInt32(dataReader["Marks"]);
			entity.EntityState = EntityStates.Clean;

			return entity;
		}
        public static IList<H_EmployeeAppraisalDetails> FindByMasterIdId(Int32 h_EmployeeAppraisalMasterId, String sortColumns)
        {
            return Find("[H_EmployeeAppraisalMasterId] = " + h_EmployeeAppraisalMasterId, sortColumns);
        }

        public Int32 H_EmployeeAppraisalMasterId
		{
            get { return _H_EmployeeAppraisalMasterId; }
            set { _H_EmployeeAppraisalMasterId = value; }
		}
        public Int32 H_AppraisalQuestionId
        {
            get { return _H_AppraisalQuestionId; }
            set { _H_AppraisalQuestionId = value; }
        }
        public Int32 Marks
        {
            get { return _Marks; }
            set { _Marks = value; }
        }
       
    }
}
