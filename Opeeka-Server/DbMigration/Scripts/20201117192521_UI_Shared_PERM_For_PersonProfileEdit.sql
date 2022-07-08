IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117192521_UI_Shared_PERM_For_PersonProfileEdit')
BEGIN
if not exists(select * from ApplicationObject WHERE Description = 'Edit Person Helper Name')
	BEGIN
		INSERT INTO ApplicationObject VALUES(1,'Profile/CurrentHelper/HelperName', 'Edit Person Helper Name', 6,0, GETDATE(), 1)
	END
	if not exists(select * from info.Permission where Description = 'Edit Person Helper Name')
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Description= 'Edit Person Helper Name'),(select OperationTypeID from info.OperationType where Name = 'Edit'),'Edit Person Helper Name', 6, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Edit Person Helper Name'))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description =  'Edit Person Helper Name'
		insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description =  'Edit Person Helper Name'

	end
	insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)

	--------------------------
	
	if not exists(select * from ApplicationObject WHERE Description = 'Add New Person Helper Icon')
	BEGIN
		INSERT INTO ApplicationObject VALUES(1,'Profile/CurrentHelper/AddNew', 'Add New Person Helper Icon', 6,0, GETDATE(), 1)
	END
	if not exists(select * from info.Permission where Description = 'Add New Person Helper Icon')
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Description= 'Add New Person Helper Icon'),(select OperationTypeID from info.OperationType where Name = 'Add'),'Add New Person Helper Icon', 6, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Add New Person Helper Icon'))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description =  'Add New Person Helper Icon'
		insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description =  'Add New Person Helper Icon'
	end
	insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)

	------------------------------
	if not exists(select * from ApplicationObject WHERE Description = 'Edit Person Helper End Date')
	BEGIN
		INSERT INTO ApplicationObject VALUES(1,'Profile/CurrentHelper/EditEndDate', 'Edit Person Helper End Date', 6,0, GETDATE(), 1)
	END
	if not exists(select * from info.Permission where Description = 'Edit Person Helper End Date')
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Description= 'Edit Person Helper End Date'),(select OperationTypeID from info.OperationType where Name = 'Edit'),'Edit Person Helper End Date', 6, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Edit Person Helper End Date'))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description =  'Edit Person Helper End Date'
		insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description =  'Edit Person Helper End Date'
		insert into info.SystemrolePermission select 4, PermissionID from info.Permission where Description =  'Edit Person Helper End Date'
		insert into info.SystemrolePermission select 5, PermissionID from info.Permission where Description =  'Edit Person Helper End Date'
	end
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
								SELECT PermissionID FROM info.Permission where Description= 'Edit Person Helper Name') AND SystemRoleID = 1)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Person Helper Name') AND SystemRoleID = 2)
      INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0)
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 
 
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Add New Person Helper Icon') AND SystemRoleID = 1)
      INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0)
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Add New Person Helper Icon') AND SystemRoleID = 2)
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0)
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

	 SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Person Helper End Date') AND SystemRoleID = 1)
      INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0)
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Person Helper End Date') AND SystemRoleID = 2)
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0)
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

	   SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Person Helper End Date') AND SystemRoleID = 4)
      INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0)
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Person Helper End Date') AND SystemRoleID = 5)
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0)
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) 
  
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) 
	  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201117192521_UI_Shared_PERM_For_PersonProfileEdit', N'3.1.4');
END;

GO

