using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GITS.Hrms.Library.Utility;

namespace GITS.Hrms.Library.Data.Entity
{
	[Serializable]
	[Class(ClassAttribute.Attributes.Entity)]
	public class H_EmployeeSpecialHonor : EntityBase<H_EmployeeSpecialHonor>
	{
		private Int32 _H_EmployeeId;
		private String _SubjectOfHonor;
		private String _LetterNo;
		private DateTime _LetterDate;

		public H_EmployeeSpecialHonor()
		{

		}

		[Property(PropertyAttribute.Attributes.NonTable)]
		protected override String AbstractName
		{
			get { return "[H_EmployeeSpecialHonor]"; }
		}

		protected override H_EmployeeSpecialHonor Map(SqlDataReader dataReader)
		{
			H_EmployeeSpecialHonor entity = new H_EmployeeSpecialHonor();

			entity.Id = DBUtility.ToInt32(dataReader["Id"]);
			entity.H_EmployeeId = DBUtility.ToInt32(dataReader["H_EmployeeId"]);
			entity.SubjectOfHonor = DBUtility.ToString(dataReader["SubjectOfHonor"]);
			entity.LetterNo = DBUtility.ToString(dataReader["LetterNo"]);
			entity.LetterDate = DBUtility.ToDateTime(dataReader["LetterDate"]);

			entity.EntityState = EntityStates.Clean;

			return entity;
		}

		public static IList<H_EmployeeSpecialHonor> FindByH_EmployeeId(Int32 h_EmployeeId, String sortColumns)
		{
			return Find("[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public static IList<H_EmployeeSpecialHonor> FindByH_EmployeeId(TransactionManager transactionManager, Int32 h_EmployeeId, String sortColumns)
		{
			return Find(transactionManager, "[H_EmployeeId] = '" + h_EmployeeId + "'", sortColumns);
		}

		public Int32 H_EmployeeId
		{
			get {return _H_EmployeeId;}
			set {_H_EmployeeId = value;}
		}

		public String SubjectOfHonor
		{
			get {return _SubjectOfHonor;}
			set {_SubjectOfHonor = value;}
		}

		public String LetterNo
		{
			get {return _LetterNo;}
			set {_LetterNo = value;}
		}

		public DateTime LetterDate
		{
			get {return _LetterDate;}
			set {_LetterDate = value;}
		}

	}
}
