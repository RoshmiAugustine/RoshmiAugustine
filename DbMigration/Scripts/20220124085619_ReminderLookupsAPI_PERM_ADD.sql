IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220124085619_ReminderLookupsAPI_PERM_ADD')
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
DECLARE @APIUser int  = 9;
--Declaration of variables ends
--'api/regular-reminder-lookups' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/regular-reminder-lookups')
   BEGIN
     INSERT INTO ApplicationObject VALUES(3,'api/regular-reminder-lookups','GetLookupsForRegularReminderSchedules',1, 0,GETDATE(),1)
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/regular-reminder-lookups')
   END
ELSE
   BEGIN
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/regular-reminder-lookups')
   END
insert into info.Permission values(@appObjId,@GET, 'GetLookupsForRegularReminderSchedules',1,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
--'api/regular-reminder-lookups' ends
-------------------------------------------------------
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220124085619_ReminderLookupsAPI_PERM_ADD', N'3.1.4');
END;

GO

