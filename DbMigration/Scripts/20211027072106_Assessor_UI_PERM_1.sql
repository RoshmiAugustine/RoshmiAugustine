IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211027072106_Assessor_UI_PERM_1')
BEGIN TRY
BEGIN TRANSACTION
BEGIN 
--Declaration of variables starts
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
-----------------------------------------------------SidebarMenu start------------------------------------------------
 BEGIN 
  --Declaration of variables ends
  IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'SidebarMenu')
     BEGIN
       INSERT INTO ApplicationObject VALUES(2,'SidebarMenu','SidebarMenu within the application',1, 0,GETDATE(),1)
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'SidebarMenu')
     END
  ELSE
     BEGIN
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'SidebarMenu')
     END
  IF (@appObjId > 0)
 BEGIN
  insert into info.Permission values(@appObjId,@View, 'SidebarMenu within the application',1,GETDATE(),1,0)
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
-----------------------------------------------------SidebarMenu end--------------------------------------------------  
 
-----------------------------------------------------Header/Notificationbell start------------------------------------
 BEGIN
  --Declaration of variables ends
  IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'Header/Notificationbell')
     BEGIN
       INSERT INTO ApplicationObject VALUES(2,'Header/Notificationbell','Notificationbell on header',1, 0,GETDATE(),1)
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'Header/Notificationbell')
     END
  ELSE
     BEGIN
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'Header/Notificationbell')
     END
  IF (@appObjId > 0)
 BEGIN
  insert into info.Permission values(@appObjId,@View, 'Notificationbell on header',1,GETDATE(),1,0)
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
-----------------------------------------------------Header/Notificationbell end---------------------------------------
-----------------------------------------------------Breadcrumbs start--------------------------------------------------
 BEGIN
  --Declaration of variables ends
  IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'Breadcrumbs')
     BEGIN
       INSERT INTO ApplicationObject VALUES(2,'Breadcrumbs','Breadcrumbs in Application',1, 0,GETDATE(),1)
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'Breadcrumbs')
     END
  ELSE
     BEGIN
       SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'Breadcrumbs')
     END
  IF (@appObjId > 0)
 BEGIN
  insert into info.Permission values(@appObjId,@View, 'Breadcrumbs in Application',1,GETDATE(),1,0)
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
-----------------------------------------------------Breadcrumbs end----------------------------------------------------
END;
 INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211027072106_Assessor_UI_PERM_1', N'3.1.4');
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 print ERROR_MESSAGE();
END CATCH


GO

