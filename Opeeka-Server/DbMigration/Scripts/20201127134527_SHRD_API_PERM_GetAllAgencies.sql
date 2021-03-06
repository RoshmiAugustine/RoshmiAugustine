IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201127134527_SHRD_API_PERM_GetAllAgencies')
BEGIN

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
								select PermissionID from info.Permission where description  = 'Get all agencies' and ApplicationObjectID = (select ApplicationObjectID from ApplicationObject where Name = '/api/agency/GetAllAgencies')) AND SystemRoleID = 1)

  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201127134527_SHRD_API_PERM_GetAllAgencies', N'3.1.4');
END;

GO

