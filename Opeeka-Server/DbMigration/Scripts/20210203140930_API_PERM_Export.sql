IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210203140930_API_PERM_Export')
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
--'/api/list-export-template' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/list-export-template')
   BEGIN
     INSERT INTO ApplicationObject VALUES(3,'/api/list-export-template','Get Export Template List',1, 0,GETDATE(),1)
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/list-export-template')
   END
ELSE
   BEGIN
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/list-export-template')
   END
insert into info.Permission values(@appObjId,@GET, 'Get Export Template List',1,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
--'/api/list-export-template' ends
-------------------------------------------------------

--'/api/export-template' starts
IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/export-template')
   BEGIN
     INSERT INTO ApplicationObject VALUES(3,'/api/export-template','Get Export Template Data',2, 0,GETDATE(),1)
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/export-template')
   END
ELSE
   BEGIN
     SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/export-template')
   END
insert into info.Permission values(@appObjId,@GET, 'Get Export Template Data',2,GETDATE(),1,0)
SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
--'/api/export-template' ends
-------------------------------------------------------

insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)


    CREATE TABLE [ExportTemplate] (
        [ExportTemplateID] int NOT NULL IDENTITY,
        [DisplayName] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [TemplateSourceText] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsPublic] bit NOT NULL,
        [IsPublished] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_ExportTemplate] PRIMARY KEY ([ExportTemplateID])
    );
END;

GO
 

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210203140930_API_PERM_Export')
BEGIN
    CREATE TABLE [ExportTemplateAgency] (
        [ExportTemplateAgencyID] int NOT NULL IDENTITY,
        [ExportTemplateID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_ExportTemplateAgency] PRIMARY KEY ([ExportTemplateAgencyID]),
        CONSTRAINT [FK_ExportTemplateAgency_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ExportTemplateAgency_ExportTemplate_ExportTemplateID] FOREIGN KEY ([ExportTemplateID]) REFERENCES [ExportTemplate] ([ExportTemplateID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210203140930_API_PERM_Export')
BEGIN
    CREATE INDEX [IX_ExportTemplateAgency_AgencyID] ON [ExportTemplateAgency] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210203140930_API_PERM_Export')
BEGIN
    CREATE INDEX [IX_ExportTemplateAgency_ExportTemplateID] ON [ExportTemplateAgency] ([ExportTemplateID]);
END;

GO
 

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210203140930_API_PERM_Export')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210203140930_API_PERM_Export', N'3.1.4');
END;

GO

