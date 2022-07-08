IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211027074046_Assessor_UI_PERM_2')

BEGIN TRY
BEGIN TRANSACTION
BEGIN
   DECLARE @appObjId int;
  DECLARE @OperTypeID int;
  DECLARE @PermissionID int;
  DECLARE @View int = 7;--UI permission OperationTypeID
  DECLARE @SuperAdmin int = 1;
  DECLARE @OrgAdminRW int  = 2;
  DECLARE @OrgAdminRO int  = 3;
  DECLARE @Supervisor int  = 4;
  DECLARE @HelperRW int  = 5;
  DECLARE @HelperRO int  = 6;
  DECLARE @Support int  = 7;
  DECLARE @Assessor int  = 10;
  DECLARE @AppObjectTypeID int  = 1;
-----------------------------------------------------Tab/People/Profile start-----------------------------------------
 BEGIN 
  --Declaration of variables ends
  IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'People/Profile')
     BEGIN
       INSERT INTO ApplicationObject VALUES(@AppObjectTypeID,'People/Profile','View People/Profile tab in the application',1, 0,GETDATE(),1)
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'People/Profile')
     END
  ELSE
     BEGIN
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'People/Profile')
     END
  IF (@appObjId > 0)
 BEGIN
  insert into info.Permission values(@appObjId,@View, 'View People/Profile tab in the application',1,GETDATE(),1,0)
  SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @View)
  insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
  insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
  insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
  insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
  insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
  insert into info.SystemRolePermission values(@HelperRO,@PermissionID);

  insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  
  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID
  not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)
 END 
 END
-----------------------------------------------------Tab/People/Profile end-------------------------------------------
-----------------------------------------------------Tab/People/Questionnaire start-----------------------------------
 BEGIN
  --Declaration of variables ends
  IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'People/Questionnaire')
     BEGIN
       INSERT INTO ApplicationObject VALUES(@AppObjectTypeID,'People/Questionnaire','View People/Questionnaire tab in the application',1, 0,GETDATE(),1)
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'People/Questionnaire')
     END
  ELSE
     BEGIN
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'People/Questionnaire')
     END
  IF (@appObjId > 0)
 BEGIN
  insert into info.Permission values(@appObjId,@View, 'View People/Questionnaire tab in the application',1,GETDATE(),1,0)
  SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @View)
  insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
  insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
  insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
  insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
  insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
  insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
  insert into info.SystemRolePermission values(@Assessor,@PermissionID);

  insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  
  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID
  not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)
 END 
 END
-----------------------------------------------------Tab/People/Questionnaire end-------------------------------------
-----------------------------------------------------Tab/People/Report start------------------------------------------
 BEGIN
  --Declaration of variables ends
  IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'People/Report')
     BEGIN
       INSERT INTO ApplicationObject VALUES(@AppObjectTypeID,'People/Report','View People/Report tab in the application',1, 0,GETDATE(),1)
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'People/Report')
     END
  ELSE
     BEGIN
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'People/Report')
     END
  IF (@appObjId > 0)
 BEGIN
  insert into info.Permission values(@appObjId,@View, 'View People/Report tab in the application',1,GETDATE(),1,0)
  SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @View)
  insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
  insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
  insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
  insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
  insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
  insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
  insert into info.SystemRolePermission values(@Assessor,@PermissionID);

  insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  
  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID
  not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)
 END 
 END
-----------------------------------------------------Tab/People/Report end--------------------------------------------
-----------------------------------------------------Tab/People/Notification start------------------------------------
 BEGIN
  --Declaration of variables ends
  IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'People/Notification')
     BEGIN
       INSERT INTO ApplicationObject VALUES(@AppObjectTypeID,'People/Notification','View People/Notification tab in the application',1, 0,GETDATE(),1)
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'People/Notification')
     END
  ELSE
     BEGIN
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'People/Notification')
     END
  IF (@appObjId > 0)
 BEGIN
  insert into info.Permission values(@appObjId,@View, 'View People/Notification tab in the application',1,GETDATE(),1,0)
  SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @View)
  insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
  insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
  insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
  insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
  insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
  insert into info.SystemRolePermission values(@HelperRO,@PermissionID);

  insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  
  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID
  not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)
 END 
 END
-----------------------------------------------------Tab/People/Notification end--------------------------------------
END;
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211027074046_Assessor_UI_PERM_2', N'3.1.4');
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 print ERROR_MESSAGE();
END CATCH
GO

