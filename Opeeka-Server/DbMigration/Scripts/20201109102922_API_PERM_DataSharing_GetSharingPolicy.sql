IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201109102922_API_PERM_DataSharing_GetSharingPolicy')
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
--'/api/agencysharingpolicy' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/agencysharingpolicy')
  BEGIN
    INSERT INTO ApplicationObject VALUES(3,'/api/agencysharingpolicy','To get Agency Sharing Policy',1, 0,GETDATE(),1)
    SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/agencysharingpolicy')
  END
ELSE
  BEGIN
    SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/agencysharingpolicy')
  END
insert into info.Permission values(@appObjId,@GET, 'To get Agency Sharing Policy',1,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
--'/api/agencysharingpolicy' ends
-------------------------------------------------------

insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)

END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201109102922_API_PERM_DataSharing_GetSharingPolicy', N'3.1.4');
END;

GO

