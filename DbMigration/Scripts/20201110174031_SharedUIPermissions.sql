IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201110174031_SharedUIPermissions')
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

  --'Update Person UI'
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description= 'EditProfile' and operationtypeid = 6) AND SystemRoleID = 1)

  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) -- SA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) -- SA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) -- SA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SA active edit person
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) -- SA inactive edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) -- SA inactive edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) -- SA inactive edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) -- SA inactive edit person

  
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='EditProfile' and operationtypeid = 6) AND SystemRoleID = 2)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- OA active edit person
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) -- OA active edit person
    
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='EditProfile' and operationtypeid = 6) AND SystemRoleID = 4)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) -- SV active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) -- SV active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) -- SV active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SV active edit person

  
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='EditProfile' and operationtypeid = 6) AND SystemRoleID = 5)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) -- Hlpr active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) -- Hlpr active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) -- Hlpr active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- Hlpr active edit person
  -------------------------------------------------------------------------

  --select * from ApplicationObject where name = 'People/Profile/Person'
  --select * from info.Permission where ApplicationObjectID = 109
  --'Edit Person button'
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description= 'Edit Person' and operationtypeid = 6) AND SystemRoleID = 1)

  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) -- SA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) -- SA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) -- SA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SA active edit person
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) -- SA inactive edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) -- SA inactive edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) -- SA inactive edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) -- SA inactive edit person

  
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Person' and operationtypeid = 6) AND SystemRoleID = 2)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- OA active edit person
  
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 1) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 1) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 1) -- OA active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) -- OA active edit person
    
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Person' and operationtypeid = 6) AND SystemRoleID = 4)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) -- SV active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) -- SV active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) -- SV active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SV active edit person

  
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Person' and operationtypeid = 6) AND SystemRoleID = 5)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RO, 0) -- Hlpr active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RO, @COLL_RW, 0) -- Hlpr active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RO, 0) -- Hlpr active edit person
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- Hlpr active edit person
  ----------------------------------------------------------------------------------------------------------------------------

  --select * from ApplicationObject where name = 'People/Notification/Note'
  --select * from info.Permission where ApplicationObjectID = 114
   SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description= 'Add Note' and operationtypeid = 5) AND SystemRoleID = 1)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SA active Add Note

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Add Note' and operationtypeid = 5) AND SystemRoleID = 2)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- OA active Add Note
    
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Add Note' and operationtypeid = 5) AND SystemRoleID = 4)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SV active Add Note

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Add Note' and operationtypeid =5) AND SystemRoleID = 5)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- Hlpr active Add Note

  ------------------------------------------------------------------------------------------------------------------------------------
  
  --select * from ApplicationObject where name = 'People/Notification/Note'
  --select * from info.Permission where ApplicationObjectID = 114
   SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description= 'Edit Note' and operationtypeid = 6) AND SystemRoleID = 1)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SA active Add Note

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Note' and operationtypeid = 6) AND SystemRoleID = 2)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- OA active Add Note
    
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Note' and operationtypeid = 6) AND SystemRoleID = 4)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SV active Add Note

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Note' and operationtypeid =6) AND SystemRoleID = 5)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- Hlpr active Add Note

  ------------------------------------------------------------------------------------------------------------------------------
 --select * from ApplicationObject where name = 'People/Questionnaire'
  --select * from info.Permission where ApplicationObjectID = 113
  --select * from info.OperationType 
  --select * from ApplicationObject  where ApplicationObjectTypeID = 1
  --select * from info.SystemRolePermission where permissionID = 142

  -- Assessment delete
  if not exists( SELECT PermissionID FROM info.Permission where Description= 'Delete Assessment Button' and operationtypeid = 4)
  begin
  insert into info.Permission values ((select ApplicationObjectID from ApplicationObject where Name = 'People/Questionnaire/Assessment'),4, 'Delete Assessment Button', 6, getdate(),1,0) --People_Questionnaire_Assessment, DELETE
  insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description = 'Delete Assessment Button'
  insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description = 'Delete Assessment Button'
  insert into info.SystemrolePermission select 4, PermissionID from info.Permission where Description = 'Delete Assessment Button'
  insert into info.SystemrolePermission select 5, PermissionID from info.Permission where Description = 'Delete Assessment Button'
  end

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description= 'Delete Assessment Button' and operationtypeid = 4) AND SystemRoleID = 1)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SA active Add Note

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Delete Assessment Button' and operationtypeid = 4) AND SystemRoleID = 2)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- OA active Add Note
    
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Delete Assessment Button' and operationtypeid = 4) AND SystemRoleID = 4)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SV active Add Note

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Delete Assessment Button' and operationtypeid =4) AND SystemRoleID = 5)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- Hlpr active Add Note

  ---------------------------------------------------------------------------------------------------------------------

--- Assessment Save (permission is of 'Edit Questionnaire')
--select * from ApplicationObject where Name = 'People/Questionnaire'
--select * from info.Permission where ApplicationObjectID = 113

SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description= 'Edit Questionnaire' and operationtypeid = 6 and ApplicationObjectID = 113) AND SystemRoleID = 1)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SA active Assessment Save

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Questionnaire' and operationtypeid = 6 and ApplicationObjectID = 113) AND SystemRoleID = 2)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- OA active Assessment Save
    
  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Questionnaire' and operationtypeid = 6 and ApplicationObjectID = 113) AND SystemRoleID = 4)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- SV active Assessment Save

  SET @SYS_ROLE_PERM_ID = (select SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
								SELECT PermissionID FROM info.Permission where Description='Edit Questionnaire' and operationtypeid = 6 and ApplicationObjectID = 113) AND SystemRoleID = 5)
  INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 0) -- Hlpr active Assessment Save

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201110174031_SharedUIPermissions', N'3.1.4');
END;

GO

