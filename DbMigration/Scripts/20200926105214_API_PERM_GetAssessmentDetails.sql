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
--'/api/assessment-details/{personIndex}/{questionnaireId}/{date}' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/assessment-details/{personIndex}/{questionnaireId}/{date}')
   BEGIN
     INSERT INTO ApplicationObject VALUES(3,'/api/assessment-details/{personIndex}/{questionnaireId}/{date}','Get Assesment Details',1, 0,GETDATE(),1)
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/assessment-details/{personIndex}/{questionnaireId}/{date}')
   END
ELSE
   BEGIN
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/assessment-details/{personIndex}/{questionnaireId}/{date}')
   END
insert into info.Permission values(@appObjId,@GET, 'Get Assesment Details',1,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
--'/api/assessment-details/{personIndex}/{questionnaireId}/{date}' ends
-------------------------------------------------------

insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)




IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926105214_API_PERM_GetAssessmentDetails')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200926105214_API_PERM_GetAssessmentDetails', N'3.1.4');
END;

GO

