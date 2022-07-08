IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220509104039_API_PERM_PersonSupportListExternal')
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
    --'api/person/support/list-external' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/support/list-external')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/person/support/list-external','Get PersonSupport List For External',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/support/list-external')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/support/list-external')
       END 
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@POST, 'Get PersonSupport List For External',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @POST)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/person/support/list-external' ends
     
END;
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220509104039_API_PERM_PersonSupportListExternal', N'3.1.4');
END;

GO

