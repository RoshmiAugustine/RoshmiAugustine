IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210616041010_API_Permission_QuestionnaireDefaultResponse')
BEGIN


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
--'/api/questionnaire-default-response-values/{personIndex}/{questionnaireId}' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/questionnaire-default-response-values/{personIndex}/{questionnaireId}')
   BEGIN
     INSERT INTO ApplicationObject VALUES(3,'/api/questionnaire-default-response-values/{personIndex}/{questionnaireId}','Get Questionnaire Defualt Response values ',1, 0,GETDATE(),1)
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/questionnaire-default-response-values/{personIndex}/{questionnaireId}')
   END
ELSE
   BEGIN
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/questionnaire-default-response-values/{personIndex}/{questionnaireId}')
   END
insert into info.Permission values(@appObjId,@GET, 'Get Questionnaire Defualt Response values ',1,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
--'/api/questionnaire-default-response-values/{personIndex}/{questionnaireId}' ends
-------------------------------------------------------

--'/api/questionnaire-default-response/{questionnaireId}' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/questionnaire-default-response/{questionnaireId}')
   BEGIN
     INSERT INTO ApplicationObject VALUES(3,'/api/questionnaire-default-response/{questionnaireId}','Get Questionnaire Defualt Response',2, 0,GETDATE(),1)
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/questionnaire-default-response/{questionnaireId}')
   END
ELSE
   BEGIN
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/questionnaire-default-response/{questionnaireId}')
   END
insert into info.Permission values(@appObjId,@GET, 'Get Questionnaire Defualt Response',2,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
--'/api/questionnaire-default-response/{questionnaireId}' ends
-------------------------------------------------------

insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)
END
BEGIN
	  DECLARE @SYS_ROLE_PERM_ID int;
	  DECLARE @AG_RW int;
	  DECLARE @COLL_RW int;
	  DECLARE @APPOBJECTID int=0;
	
	  SET @APPOBJECTID  = (select ApplicationObjectID  from ApplicationObject where NAme = '/api/questionnaire-default-response-values/{personIndex}/{questionnaireId}');
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
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 3)
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
Begin
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210616041010_API_Permission_QuestionnaireDefaultResponse', N'3.1.4');
	END
END;

GO

