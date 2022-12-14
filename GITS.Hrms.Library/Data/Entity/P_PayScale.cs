using System;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
    [Serializable]
    [Class(ClassAttribute.Attributes.Entity)]
    public class P_PayScale : EntityBase<P_PayScale>
    {
        private Int32 _H_GradeId;
        private Double _StartBasic;
        private Double _Increment;
        private Double _TargetBasic;


        public enum Statuses
        {
            ACTIVE = 1,
            INACTIVE = 2
        }
        public P_PayScale()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override string AbstractName
		{
            get { return "[P_PayScale]"; }
		}
        protected override P_PayScale Map(SqlDataReader dataReader)
        {
            P_PayScale entity = new P_PayScale();

            entity.Id = DBUtility.ToInt32(dataReader["Id"]);
            entity.H_GradeId = DBUtility.ToInt32(dataReader["H_GradeId"]);
            entity.StartBasic = DBUtility.ToDouble(dataReader["StartBasic"]);
            entity.Increment = DBUtility.ToDouble(dataReader["Increment"]);
            entity.TargetBasic = DBUtility.ToDouble(dataReader["TargetBasic"]);
            entity.EntityState = EntityStates.Clean;

            return entity;
        }
        public static P_PayScale GetByGradeId(Int32 gradeId)
        {
            return Get("H_GradeId=" + gradeId);
        }
        public Int32 H_GradeId
        {
            get { return _H_GradeId; }
            set { _H_GradeId = value; }
        }
        public Double StartBasic
        {
            get { return _StartBasic; }
            set { _StartBasic = value; }
        }


        public Double Increment
        {
            get { return _Increment; }
            set { _Increment = value; }
        }
        public Double TargetBasic
        {
            get { return _TargetBasic; }
            set { _TargetBasic = value; }
        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        public String Scale
        {
            get { return _StartBasic.ToString()+"-"+Increment.ToString()+"-"+ _TargetBasic.ToString(); }

        }

        [Property(PropertyAttribute.Attributes.NonTable)]
        public String Grade
        {
            get { return H_Grade.GetById(_H_GradeId).Name; }

        }
    }
}
