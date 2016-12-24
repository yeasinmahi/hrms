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


--9 Scripts


