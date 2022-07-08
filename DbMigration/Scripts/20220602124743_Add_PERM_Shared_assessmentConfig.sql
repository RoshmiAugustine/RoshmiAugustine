IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220602124743_Add_PERM_Shared_assessmentConfig')

BEGIN
	  DECLARE @SYS_ROLE_PERM_ID int;
	  DECLARE @AG_RW int;
	  DECLARE @COLL_RW int;
	  DECLARE @APPOBJECTID int=0;
	
	  SET @APPOBJECTID  = (select ApplicationObjectID  from ApplicationObject where NAme like '/api/assessment-configurations');
	  IF(@APPOBJECTID <> 0)
	  BEGIN
	  SET @AG_RW  = (select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Read Only');
	  SET @COLL_RW =(select CollaborationSharingPolicyID from CollaborationSharingPolicy where Description = 'Read Only');
	  BEGIN
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 1)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	     INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 2) 
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)  
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);

	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 3)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	       
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 4)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	     
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 5)	
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)								
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 6)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);   

	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 10)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1); 

	  END

	  SET @AG_RW  = (select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Read Only');
	  SET @COLL_RW =(select CollaborationSharingPolicyID from CollaborationSharingPolicy where Description = 'Both Read and Write');
	  BEGIN
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 1)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	     INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 2) 
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)  
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);

	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 3)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	       
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 4)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	     
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 5)	
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)								
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 6)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);   

	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 10)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1); 

	  END

	  SET @AG_RW  = (select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Both Read and Write');
	  SET @COLL_RW =(select CollaborationSharingPolicyID from CollaborationSharingPolicy where Description = 'Read Only');
	  BEGIN
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 1)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	     INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 2) 
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)  
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);

	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 3)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	       
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 4)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	     
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 5)	
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)								
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 6)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);   

	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 10)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1); 

	  END
	  
	  SET @AG_RW  = (select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Both Read and Write');
	  SET @COLL_RW =(select CollaborationSharingPolicyID from CollaborationSharingPolicy where Description = 'Both Read and Write');
	  BEGIN
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 1)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	     INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 2) 
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)  
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);

	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 3)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	       
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 4)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	     
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 5)	
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)								
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);
	   
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 6)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1);   

	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 10)
	   IF NOT EXISTS(SELECT * from info.SharingRolePermission where SystemRolePermissionID = @SYS_ROLE_PERM_ID 
	                           AND AgencySharingPolicyID = @AG_RW AND CollaborationSharingPolicyID = @COLL_RW)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1); 

	  END

	  END
END


BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220602124743_Add_PERM_Shared_assessmentConfig', N'3.1.4');
END;

GO

