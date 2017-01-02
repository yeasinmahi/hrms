using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class H_AppraisalQuestion : EntityBase<H_AppraisalQuestion>
    {
        private String  _QuestionText;
        private Int32 _H_AppraisalId;


        public H_AppraisalQuestion()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
            get { return "[H_AppraisalQuestion]"; }
		}

        protected override H_AppraisalQuestion Map(SqlDataReader dataReader)
		{
            H_AppraisalQuestion entity = new H_AppraisalQuestion();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.QuestionText = DBUtility.ToString(dataReader["QuestionText"]);
            entity.H_AppraisalId = DBUtility.ToInt32(dataReader["H_AppraisalId"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}
		
		public String QuestionText
		{
			get {return this._QuestionText;}
			set {this._QuestionText = value;}
		}
        public Int32 H_AppraisalId
        {
            get { return this._H_AppraisalId; }
            set { this._H_AppraisalId = value; }
        }
        

    }
}
