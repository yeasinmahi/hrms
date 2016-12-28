--1 Scripts
alter table SubZone add NameInBangla varchar(50) null
alter table SubZone add OpeningDate datetime null
alter table SubZone add ClosingDate datetime null

--2 Scripts
alter table Region add OpeningDate datetime null
alter table Region add ClosingDate datetime null


--3 Scripts
alter table Branch add NameInBangla varchar(50) null


--4 Scripts
alter table H_Grade add RomanName varchar(50) null

--5 Scripts
ALTER VIEW [dbo].[SubzoneView]
      AS
      SELECT Subzone.Id, Subzone.Name, ZoneId, Zone.Name AS ZoneName, Subzone.NameInBangla, Subzone.Status
      FROM Subzone
      INNER JOIN Zone ON Zone.Id = ZoneId

--6 Scripts
ALTER VIEW [dbo].[BranchView]
      AS
      SELECT Branch.Id,
      Code,
      Branch.Name,
      Branch.BranchType,
      Branch.OpeningDate,
	  Branch.NameInBangla,
      MobileNumber,
      Branch.Status,
      LocationType,
      RegionId,
      Region.Name AS RegionName,
      SubzoneId,
      Subzone.Name AS SubzoneName,
      ZoneId,
      Zone.Name AS ZoneName,
      ThanaId,
      Thana.Name AS ThanaName,
      Thana.DistrictId,
      District.Name AS DistrictName
      FROM Branch
      INNER JOIN Region ON RegionId = Region.Id
      INNER JOIN Subzone ON SubzoneId = Subzone.Id
      INNER JOIN Zone ON ZoneId = Zone.Id
      INNER JOIN Thana ON ThanaId = Thana.Id
      INNER JOIN District ON Thana.DistrictId = District.Id
GO

--7 Scripts
alter table H_Designation add ShortName varchar(50) null
alter table H_Designation add Status int 
alter table H_Designation add BanglaName varchar(50) null 

--8 Scripts
alter table H_Employee add NationalId varchar(50) not null
alter table H_Employee add NameInBangla nchar(50) not null

--9 Scripts

ALTER VIEW [dbo].[H_EmployeeView]
      AS
      SELECT
      H_Employee.Id,
      H_Employee.Name,
	  H_Employee.NameInBangla,
	  H_Employee.Code,
      FatherName,
      MotherName,
      DateOfBirth,
      BloodGroup,
      Sex,
      MaritalStatus,
      Religion,
      PermanentAddressId,
      PresentAddressId,
      AppointmentLetterDate,
      AppointmentLetterNo,
      JoiningDate,
      H_Employee.EmploymentType,
      H_Employee.Status,
      BranchId,
      Branch.Name AS BranchName,
      RegionId,
      Region.Name AS RegionName,
      SubzoneId,
      Subzone.Name AS SubzoneName,
      ZoneId,
      Zone.Name AS ZoneName,
      H_DepartmentId,
      H_Department.Name AS DepartmentName,
      H_DesignationId,
      H_Designation.Name AS DesignationName,
      H_GradeId,
      H_Grade.Name AS GradeName
      FROM H_Employee
      INNER JOIN H_EmployeeBranch ON H_Employee.Id = H_EmployeeBranch.H_EmployeeId AND (H_EmployeeBranch.EndDate = '2099-12-31' OR H_EmployeeBranch.EndDate = (SELECT MAX(EndDate) FROM H_EmployeeBranch WHERE H_EmployeeBranch.H_EmployeeId = H_Employee.Id))
      INNER JOIN Branch ON Branch.Id = BranchId
      INNER JOIN Region ON Region.Id = RegionId
      INNER JOIN Subzone ON Subzone.Id = SubzoneId
      INNER JOIN Zone ON Zone.Id = ZoneId
      INNER JOIN H_EmployeeDepartment ON H_Employee.Id = H_EmployeeDepartment.H_EmployeeId AND (H_EmployeeDepartment.EndDate = '2099-12-31' OR H_EmployeeDepartment.EndDate = (SELECT MAX(EndDate) FROM H_EmployeeDepartment WHERE H_EmployeeDepartment.H_EmployeeId = H_Employee.Id))
      INNER JOIN H_Department ON H_Department.Id = H_DepartmentId
      INNER JOIN H_EmployeeDesignation ON H_Employee.Id = H_EmployeeDesignation.H_EmployeeId AND (H_EmployeeDesignation.EndDate = '2099-12-31' OR H_EmployeeDesignation.EndDate = (SELECT MAX(EndDate) FROM H_EmployeeDesignation WHERE H_EmployeeDesignation.H_EmployeeId = H_Employee.Id))
      INNER JOIN H_Designation ON H_Designation.Id = H_DesignationId
      INNER JOIN H_EmployeeGrade ON H_Employee.Id = H_EmployeeGrade.H_EmployeeId AND (H_EmployeeGrade.EndDate = '2099-12-31' OR H_EmployeeGrade.EndDate = (SELECT MAX(EndDate) FROM H_EmployeeGrade WHERE H_EmployeeGrade.H_EmployeeId = H_Employee.Id))
      INNER JOIN H_Grade ON H_Grade.Id = H_GradeId
GO

--10 Scripts
create table H_EmployeePhoto (
Id int IDENTITY(1, 1) NOT NULL,
H_EmployeeId int not null,
Photo image not null
)

--10 Scripts
create table H_EmployeePromotionHistory (
Id int IDENTITY(1, 1) NOT NULL,
H_EmployeeId int not null,
Type int not null,
LetterNo varchar(50) null,
LetterDate date null,
OldH_GradeId int null,
NewH_GradeId int null,
OldH_DesignationId int null,
NewH_DesignationId int null,
PromotionDate date null,
Remarks varchar(50) not null,
Status varchar(50) null,
UserLogin varchar(50) not null
)

--11 Scripts
alter table H_AcademicQualification add Institution varchar(50) not null

--12 Scripts
ALTER VIEW [dbo].[H_AcademicQualificationView]
			AS
			SELECT dbo.H_AcademicQualification.Id, dbo.H_AcademicQualification.H_EmployeeId, dbo.H_AcademicQualification.[Level],dbo.H_AcademicQualification.Institution,
			ISNULL(convert(varchar,dbo.H_AcademicQualification.GPA), dbo.H_AcademicQualification.Result) AS Result
			,dbo.H_AcademicQualification.PassingYear,
			dbo.H_AcademicQualification.SortOrder, dbo.ExamName.Name AS ExamName, dbo.GroupSubject.Name AS SubjectName,
			dbo.BoardUniversity.Name AS BoardName
			FROM dbo.H_AcademicQualification
			INNER JOIN dbo.ExamName ON dbo.H_AcademicQualification.ExamNameId = dbo.ExamName.Id
			INNER JOIN dbo.GroupSubject ON dbo.H_AcademicQualification.GroupSubjectId = dbo.GroupSubject.Id
			INNER JOIN dbo.BoardUniversity ON dbo.H_AcademicQualification.BoardUniversityId = dbo.BoardUniversity.Id
GO

--13 Scripts
create table H_FileUpload (
Id int IDENTITY(1, 1) NOT NULL,
H_EmployeeId int not null,
Title varchar(50) not null,
FileName varchar(50) not null,
UploadDate date null
)