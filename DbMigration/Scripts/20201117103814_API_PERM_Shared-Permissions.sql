IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117103814_API_PERM_Shared-Permissions')
BEGIN
 --Declaration of variables starts
DECLARE @appObjId int;
DECLARE @OperTypeID int;
DECLARE @PermissionID int;
DECLARE @GET int = 1;
DECLARE @POST int = 2;
DECLARE @PUT int = 3;
DECLARE @DELETE int = 4;
DECLARE @SuperAdmin int = 1;
DECLARE @OrgAdminRW int  = 2;
DECLARE @OrgAdminRO int  = 3;
DECLARE @Supervisor int  = 4;
DECLARE @HelperRW int  = 5;
DECLARE @HelperRO int  = 6;
DECLARE @Support int  = 7;
--Declaration of variables ends
--'/api/present-notification-list/{personIndex}' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/shared-permissions/{agencySharingIndex}/{collaborationSharingIndex}/{isActiveForSharing}')
   BEGIN
     INSERT INTO ApplicationObject VALUES(3,'/api/shared-permissions/{agencySharingIndex}/{collaborationSharingIndex}/{isActiveForSharing}','Get shared UI permissions',1, 0,GETDATE(),1)
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/shared-permissions/{agencySharingIndex}/{collaborationSharingIndex}/{isActiveForSharing}')
   END
ELSE
   BEGIN
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/shared-permissions/{agencySharingIndex}/{collaborationSharingIndex}/{isActiveForSharing}')
   END
insert into info.Permission values(@appObjId,@GET, 'Get shared UI permissions',1,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
--'/api/present-notification-list/{personIndex}' ends
-------------------------------------------------------

insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)


  DECLARE @SYS_ROLE_PERM_ID int;
  DECLARE @AG_RO int;
  DECLARE @AG_RW int;
  DECLARE @COLL_RO int;
  DECLARE @COLL_RW int;

  SET @AG_RO = (select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Read Only')
  SET @AG_RW  = (select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Both Read and Write')
  SET @COLL_RO = (select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Read Only')
  SET @COLL_RW =(select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Both Read and Write')

   SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Get shared UI permissions' and operationTypeID = 1) AND SystemRoleID = 1)

  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

   SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Get shared UI permissions' and operationTypeID = 1) AND SystemRoleID = 2)

  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Get shared UI permissions' and operationTypeID = 1) AND SystemRoleID = 3)

  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0)
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

    
   SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Get shared UI permissions' and operationTypeID = 1) AND SystemRoleID = 4)

  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

  
   SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Get shared UI permissions' and operationTypeID = 1) AND SystemRoleID = 5)

  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Get shared UI permissions' and operationTypeID = 1) AND SystemRoleID = 6)

  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201117103814_API_PERM_Shared-Permissions', N'3.1.4');
END;

GO

