IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201118175136_EditPastHelperUISharedPermission')
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

  --select * from ApplicationObject where Name = 'People/Profile/PastHelper'
  --select * from info.Permission where ApplicationObjectID = 188
  --'Update Person UI'
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description= 'Edit Past Helper' and operationtypeid = 6) AND SystemRoleID = 1)

  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

  
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Past Helper' and operationtypeid = 6) AND SystemRoleID = 2)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 
    
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201118175136_EditPastHelperUISharedPermission', N'3.1.4');
END;

GO

