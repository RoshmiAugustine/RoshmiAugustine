﻿IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210326160415_SHRD_PERM_API_PersonProfileHistory')
BEGIN
	  DECLARE @SYS_ROLE_PERM_ID int;
	  DECLARE @AG_RW int;
	  DECLARE @COLL_RW int;
	  DECLARE @APPOBJECTID int=0;
	
	  SET @APPOBJECTID  = (select ApplicationObjectID  from ApplicationObject where NAme = '/api/audit-person-profile/{peopleIndex}/{historyType}');
	  SET @AG_RW  = (select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Both Read and Write');
	  SET @COLL_RW =(select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Both Read and Write');
	  IF(@APPOBJECTID <> 0)
	  BEGIN
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 1)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 2)   
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	       
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 4)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	     
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 5)									
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 6)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);   
	  END
END
BEGIN
	  DECLARE @SYS_ROLE_PERM_ID int;
	  DECLARE @AG_RW int;
	  DECLARE @COLL_RW int;
	  DECLARE @APPOBJECTID int=0;
	
	  SET @APPOBJECTID  = (select ApplicationObjectID  from ApplicationObject where NAme = '/api/notification-count/{lastLoginTime}');
	  SET @AG_RW  = (select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Both Read and Write');
	  SET @COLL_RW =(select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Both Read and Write');
	  IF(@APPOBJECTID <> 0)
	  BEGIN
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 1)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 2)   
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	       
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 4)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	     
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 5)									
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 6)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);   
	  END
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210326160415_SHRD_PERM_API_PersonProfileHistory', N'3.1.4');
END;

GO

