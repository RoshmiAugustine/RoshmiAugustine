IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210915061715_PERM_AssessorRole_Add')
--------------For adding new Assessor Role and SystemRolePermissions-------------------------------------------
BEGIN
DECLARE @AssessorRoleID AS INT;
DECLARE @AssessorName AS VARCHAR(10);
DECLARE @HelperRWRoleID AS INT;
----Assessor Role insertion---
SET @AssessorName = 'Assessor';
IF NOT EXISTS (SELECT * FROM info.SystemRole WHERE Name = @AssessorName)
BEGIN
 INSERT INTO info.SystemRole VALUES (@AssessorName, Null, 0, 10,0 ,GETDATE(), 1,null);
END
SELECT @AssessorRoleID = SystemRoleID FROM info.SystemRole WHERE Name = @AssessorName;

SELECT @HelperRWRoleID = SystemRoleID FROM info.SystemRole WHERE Name = 'Helper RW';
------------------------Permissions for Assessor Role-------------------------------
IF (@AssessorRoleID <> 0 AND @HelperRWRoleID <> 0)
BEGIN
 INSERT INTO info.SystemRolePermission 
 SELECT @AssessorRoleID,PermissionID FROm (
  -------------------------------Permision for API---------------------------------
  select AO.Name, AOT.Name as ApplicationObjectTypeName, P.OperationTypeID,
         OT.Name AS OpertaionType,SRP.SystemRoleID,SRP.PermissionID
  from info.SystemRolePermission SRP
  JOIN info.Permission P ON SRP.PermissionID = P.PermissionID
  JOIN ApplicationObject AO ON AO.ApplicationObjectID = P.ApplicationObjectID
  JOIN info.OperationType OT ON P.OperationTypeID = OT.OperationTypeID
  JOIN ApplicationObjectType AOT ON AO.ApplicationObjectTypeId = AOT.ApplicationObjectTypeId
  WHERE SRP.SystemRoleID = @HelperRWRoleID  AND AOT.NAme = 'APIEndPoint' AND AO.NAme <> '/api/person' 
	UNION ALL
  -------------------------------Permision for UI---------------------------------
  select AO.Name, AOT.Name as ApplicationObjectTypeName, P.OperationTypeID,
         OT.Name AS OpertaionType, SRP.SystemRoleID,SRP.PermissionID
  from info.SystemRolePermission SRP
  JOIN info.Permission P ON SRP.PermissionID = P.PermissionID
  JOIN ApplicationObject AO ON AO.ApplicationObjectID = P.ApplicationObjectID
  JOIN info.OperationType OT ON P.OperationTypeID = OT.OperationTypeID
  JOIN ApplicationObjectType AOT ON AO.ApplicationObjectTypeId = AOT.ApplicationObjectTypeId
  WHERE SRP.SystemRoleID = @HelperRWRoleID AND (AOT.NAme = 'UIComponentButton' OR AOT.NAme = 'UIComponentMenu')
  AND AO.NAme not like '%Profile%' 
 )AS A
END
END

BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210915061715_PERM_AssessorRole_Add', N'3.1.4');
END;

GO

