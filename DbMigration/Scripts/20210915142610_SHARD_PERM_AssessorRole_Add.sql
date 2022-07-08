IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210915142610_SHARD_PERM_AssessorRole_Add')
-------------Updating columns in HelperImport with flag for receiving Signup mail---------------------------
BEGIN
UPDATE info.importtype set templatejson = '[{"FirstName": "","LastName": "","Role": "","Email": "","ReviewerEmail":"","StartDate":"","ReceiveSignUPMail(True/False OR 1/0)":""}]' 
where importtypeid = (select importtypeid from info.importtype  where [Name] = 'Helper');
END
--------------For SharingRole Permissions-------------------------------------------
BEGIN
DECLARE @HelperRWRolePermissionID INT;
DECLARE @HelperRWPermissionID INT;
DECLARE @AssessorRolePermissionID INT;
DECLARE @AssessorRoleID INT;
DECLARE @HelperRWRoleID INT;
DECLARE @table_tempRolePermision TABLE
	(    
	    SystemRolePermissionID INT,
		PermissionID INT
	);
	
SELECT @AssessorRoleID = SystemRoleID FROM info.SystemRole WHERE Name = 'Assessor';
SELECT @HelperRWRoleID = SystemRoleID FROM info.SystemRole WHERE Name = 'Helper RW';

INSERT INTO @table_tempRolePermision
SELECT SystemRolePermissionID,PermissionID from (
  select distinct AO.Name,AOT.Name as ApplicationObjectTypeName, P.OperationTypeID,
         OT.Name AS OpertaionType,SRP.PermissionID,
		 SRP.SystemRolePermissionID
  from info.SharingRolePermission SHR
  JOIN info.SystemRolePermission SRP ON SHR.SystemRolePermissionID = SRP.SystemRolePermissionID
  JOIN info.Permission P ON SRP.PermissionID = P.PermissionID
  JOIN ApplicationObject AO ON AO.ApplicationObjectID = P.ApplicationObjectID
  JOIN info.OperationType OT ON P.OperationTypeID = OT.OperationTypeID
  JOIN ApplicationObjectType AOT ON AO.ApplicationObjectTypeId = AOT.ApplicationObjectTypeId
  WHERE SRP.SystemRoleID = @HelperRWRoleID  AND AOT.NAme = 'APIEndPoint' AND AO.NAme <> '/api/person'  
  UNION ALL
  select distinct AO.Name,AOT.Name as ApplicationObjectTypeName, P.OperationTypeID,
         OT.Name AS OpertaionType,SRP.PermissionID,
		 SRP.SystemRolePermissionID
  from info.SharingRolePermission SHR
  JOIN info.SystemRolePermission SRP ON SHR.SystemRolePermissionID = SRP.SystemRolePermissionID
  JOIN info.Permission P ON SRP.PermissionID = P.PermissionID
  JOIN ApplicationObject AO ON AO.ApplicationObjectID = P.ApplicationObjectID
  JOIN info.OperationType OT ON P.OperationTypeID = OT.OperationTypeID
  JOIN ApplicationObjectType AOT ON AO.ApplicationObjectTypeId = AOT.ApplicationObjectTypeId
 WHERE SRP.SystemRoleID = @HelperRWRoleID AND (AOT.NAme = 'UIComponentButton' OR AOT.NAme = 'UIComponentMenu') AND AO.NAme not like '%Profile%' --12+3
 )AS A  

--select * from @table_tempRolePermision
DECLARE cursor_permission CURSOR LOCAL FOR SELECT SystemRolePermissionID,PermissionID FROM @table_tempRolePermision order by SystemRolePermissionID
OPEN cursor_permission	
FETCH NEXT FROM cursor_permission INTO  @HelperRWRolePermissionID, @HelperRWPermissionID ;
WHILE @@FETCH_STATUS = 0
 BEGIN	     
   ----------Fetching assessorRole SystemRolePermissionID for each PermissionID of HelperRW in cursor loop------------------------
   SELECT @HelperRWRolePermissionID,@HelperRWPermissionID;
   SELECT @AssessorRolePermissionID = SystemRolePermissionID FROM info.SystemRolePermission 
        WHERE PermissionID = @HelperRWPermissionID and SystemRoleID = @AssessorRoleID;

   IF EXISTS(SELECT * FROM info.SharingRolePermission WHERE SystemRolePermissionID = @HelperRWRolePermissionID )
   BEGIN
   ------------------Insert sharedPerm with Assessor SystemRolePermID to sharedPermission Table--------------------
     INSERT INTO info.SharingRolePermission 
     SELECT @AssessorRolePermissionID, AgencySharingPolicyID,CollaborationSharingPolicyID,AllowInactiveAccess FROM info.SharingRolePermission 
     WHERE SystemRolePermissionID = @HelperRWRolePermissionID
   END
   ELSE 
     SELECT @HelperRWRolePermissionID,@HelperRWPermissionID;
   FETCH NEXT FROM cursor_permission INTO  @HelperRWRolePermissionID, @HelperRWPermissionID 
 END

END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210915142610_SHARD_PERM_AssessorRole_Add', N'3.1.4');
END;

GO

