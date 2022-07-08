IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201110044446_API_PERM_SetSuperAdminDefaultAgency')
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
--'/api/helpers/SetSuperAdminDefaultAgency' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/helpers/SetSuperAdminDefaultAgency')
   BEGIN
     INSERT INTO ApplicationObject VALUES(3,'/api/helpers/SetSuperAdminDefaultAgency','Set Super Admin Default Agency',1, 0,GETDATE(),1)
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/helpers/SetSuperAdminDefaultAgency')
   END
ELSE
   BEGIN
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/helpers/SetSuperAdminDefaultAgency')
   END
insert into info.Permission values(@appObjId,@PUT, 'Set Super Admin Default Agency',1,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @PUT)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
--'/api/helpers/SetSuperAdminDefaultAgency' ends
-------------------------------------------------------

insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)

END;
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201110044446_API_PERM_SetSuperAdminDefaultAgency', N'3.1.4');
END;

GO

