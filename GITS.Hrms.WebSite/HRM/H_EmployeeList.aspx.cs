using System;
using System.Collections.Generic;
using GITS.Hrms.Library.Data;
using GITS.Hrms.Library.Data.Entity;
using GITS.Hrms.Library.Data.View;
using GITS.Hrms.Library.Utility;
using GITS.Hrms.Library.Web;

namespace GITS.Hrms.WebSite.HRM
{
    public partial class H_EmployeeList : GridPage
    {
        protected override string PropertyName
        {
            get { return "H_EMPLOYEE LIST"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.GridView = this.gvList;
            this.BaseEntityType = typeof(H_Employee);
            this.EntityType = typeof(H_EmployeeView);
        }

        protected override string GetAddPageUrl()
        {
            return "H_EmployeeAddNotification.aspx";
            //return "H_EmployeeAdd.aspx";
        }

        protected override object GetDataSource()
        {

            Int32 total = 0;
            IList<H_EmployeeView> list = H_EmployeeView.FindByLogin(this.FilterExpression, this.SortExpression, User.Identity.Name, this.PageIndex * this.GridView.PageSize + 1, this.GridView.PageSize, out total);


            //string whereClause = "BranchId IN(SELECT DISTINCT Branch.Id FROM Zone INNER JOIN UserLocation ON [Login] = '" + User.Identity.Name + "' AND (UserLocation.ZoneId IS NULL OR UserLocation.ZoneId = Zone.Id) INNER JOIN Subzone ON Zone.Id = Subzone.ZoneId AND (UserLocation.SubzoneId IS NULL OR UserLocation.SubzoneId = Subzone.Id) INNER JOIN Region ON Subzone.Id = Region.SubzoneId AND (UserLocation.RegionId IS NULL OR UserLocation.RegionId = Region.Id) INNER JOIN Branch ON Region.Id = Branch.RegionId  AND (UserLocation.BranchId IS NULL OR UserLocation.BranchId = Branch.Id))";
            //IList<H_EmployeeView> list = H_EmployeeView.Find(whereClause, "", this.PageIndex * this.GridView.PageSize + 1, this.GridView.PageSize, out total);
            RecordCount = total;
            return list;

        }

        protected override Message Delete(TransactionManager transactionManager, int id)
        {
            Message msg = new Message();
            H_Employee emp = H_Employee.GetById(transactionManager, id);

            H_EmployeePhoto ePhoto = H_EmployeePhoto.GetByH_EmployeeId(id);
            if (ePhoto != null)
            {
                H_EmployeePhoto.Delete(transactionManager, ePhoto.Id);
            }

            IList<H_AcademicQualification> academicQualifications = H_AcademicQualification.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_AcademicQualification aq in academicQualifications)
            {
                H_AcademicQualification.Delete(transactionManager, aq.Id);
            }

            IList<H_EmployeeActingDesignation> eActingDesignations = H_EmployeeActingDesignation.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeActingDesignation ead in eActingDesignations)
            {
                H_EmployeeActingDesignation.Delete(transactionManager, ead.Id);
            }

            IList<H_EmployeeBranch> eBranches = H_EmployeeBranch.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeBranch ed in eBranches)
            {
                H_EmployeeBranch.Delete(transactionManager, ed.Id);
            }

            IList<H_EmployeeDepartment> eDepartments = H_EmployeeDepartment.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeDepartment ed in eDepartments)
            {
                H_EmployeeDepartment.Delete(transactionManager, ed.Id);
            }

            IList<H_EmployeeDesignation> eDesignations = H_EmployeeDesignation.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeDesignation ed in eDesignations)
            {
                H_EmployeeDesignation.Delete(transactionManager, ed.Id);
            }

            IList<H_EmployeeDrop> eDrops = H_EmployeeDrop.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeDrop ed in eDrops)
            {
                H_EmployeeDesignation.Delete(transactionManager, ed.Id);
            }

            IList<H_EmployeeGrade> eGrades = H_EmployeeGrade.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeGrade ed in eGrades)
            {
                H_EmployeeGrade.Delete(transactionManager, ed.Id);
            }

            IList<H_EmployeeIncrementHeldup> eIncrementHeldups = H_EmployeeIncrementHeldup.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeIncrementHeldup eih in eIncrementHeldups)
            {
                H_EmployeeIncrementHeldup.Delete(transactionManager, eih.Id);
            }

            IList<H_EmployeeLeave> eLeaves = H_EmployeeLeave.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeLeave el in eLeaves)
            {
                H_EmployeeLeave.Delete(transactionManager, el.Id);
            }

            IList<H_EmployeePenalty> ePenalties = H_EmployeePenalty.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeePenalty ep in ePenalties)
            {
                H_EmployeePenalty.Delete(transactionManager, ep.Id);
            }

            IList<H_EmployeePromotion> ePromotions = H_EmployeePromotion.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeePromotion ep in ePromotions)
            {
                H_EmployeePromotion.Delete(transactionManager, ep.Id);
            }

            IList<H_EmployeeReemployment> eReemployments = H_EmployeeReemployment.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeReemployment er in eReemployments)
            {
                H_EmployeeReemployment.Delete(transactionManager, er.Id);
            }

            IList<H_EmployeeRejoin> eRejoins = H_EmployeeRejoin.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeRejoin er in eRejoins)
            {
                H_EmployeeRejoin.Delete(transactionManager, er.Id);
            }

            IList<H_EmployeeSalary> eSalaries = H_EmployeeSalary.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeSalary es in eSalaries)
            {
                H_EmployeeSalary.Delete(transactionManager, es.Id);
            }

            IList<H_EmployeeSpecialHonor> eSpecialHonors = H_EmployeeSpecialHonor.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeSpecialHonor esh in eSpecialHonors)
            {
                H_EmployeeSpecialHonor.Delete(transactionManager, esh.Id);
            }

            IList<H_EmployeeTransfer> eTransfers = H_EmployeeTransfer.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeTransfer et in eTransfers)
            {
                H_EmployeeTransfer.Delete(transactionManager, et.Id);
            }

            IList<H_EmployeeWaitingForPosting> eWaitingForPostings = H_EmployeeWaitingForPosting.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeWaitingForPosting ewfp in eWaitingForPostings)
            {
                H_EmployeeWaitingForPosting.Delete(transactionManager, ewfp.Id);
            }

            IList<H_EmployeeWarning> eWarnings = H_EmployeeWarning.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_EmployeeWarning ew in eWarnings)
            {
                H_EmployeeWarning.Delete(transactionManager, ew.Id);
            }

            IList<H_Experience> eExperiences = H_Experience.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_Experience ee in eExperiences)
            {
                H_Experience.Delete(transactionManager, ee.Id);
            }

            IList<H_ProfessionalQualification> eProfessionalQualifications = H_ProfessionalQualification.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_ProfessionalQualification epq in eProfessionalQualifications)
            {
                H_ProfessionalQualification.Delete(transactionManager, epq.Id);
            }

            IList<H_Training> eTrainings = H_Training.FindByH_EmployeeId(transactionManager, id, "");

            foreach (H_Training et in eTrainings)
            {
                H_Training.Delete(transactionManager, et.Id);
            }

            msg = base.Delete(transactionManager, id);

            H_Address.Delete(transactionManager, emp.PermanentAddressId);
            H_Address.Delete(transactionManager, emp.PresentAddressId);

            return msg;
        }

    }
}
