IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210217101813_API_PERM_ADD_FileImport')
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
--'/api/import' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/import')
   BEGIN
     INSERT INTO ApplicationObject VALUES(3,'/api/import','Insert Import Data',1, 0,GETDATE(),1)
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/import')
   END
ELSE
   BEGIN
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/import')
   END
insert into info.Permission values(@appObjId,@POST, 'Insert Import Data',1,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @POST)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
--'/api/import' ends
-------------------------------------------------------

--'/api/import-file-list' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/import-file-list')
   BEGIN
     INSERT INTO ApplicationObject VALUES(3,'/api/import-file-list','Get Files To Import List',2, 0,GETDATE(),1)
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/import-file-list')
   END
ELSE
   BEGIN
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/import-file-list')
   END
insert into info.Permission values(@appObjId,@GET, 'Get Files To Import List',2,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
--'/api/import-file-list' ends
-------------------------------------------------------

insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210217101813_API_PERM_ADD_FileImport')
BEGIN

    CREATE TABLE [FileImport] (
        [FileImportID] int NOT NULL IDENTITY,
        [ImportType] nvarchar(max) NULL,
        [FileJsonData] nvarchar(max) NULL,
        [UpdateUserID] int NOT NULL,
        [IsProcessed] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL DEFAULT (getdate()),
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_FileImport] PRIMARY KEY ([FileImportID]),
        CONSTRAINT [FK_FileImport_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_FileImport_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210217101813_API_PERM_ADD_FileImport')
BEGIN
    CREATE INDEX [IX_FileImport_AgencyID] ON [FileImport] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210217101813_API_PERM_ADD_FileImport')
BEGIN
    CREATE INDEX [IX_FileImport_UpdateUserID] ON [FileImport] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210217101813_API_PERM_ADD_FileImport')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210217101813_API_PERM_ADD_FileImport', N'3.1.4');
END;

GO

