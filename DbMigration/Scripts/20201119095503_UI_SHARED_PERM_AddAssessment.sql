﻿IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201119095503_UI_SHARED_PERM_AddAssessment')
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
								SELECT PermissionID FROM info.Permission where Description= 'Add Assessment' and OperationTypeID = 5) AND SystemRoleID = 1)
		INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
	SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Add Assessment' and OperationTypeID = 5) AND SystemRoleID = 2)
		INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
    SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Add Assessment' and OperationTypeID = 5) AND SystemRoleID = 4)
		INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 

	SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Add Assessment' and OperationTypeID = 5) AND SystemRoleID = 5)
		INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
 

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201119095503_UI_SHARED_PERM_AddAssessment', N'3.1.4');
END;

GO

