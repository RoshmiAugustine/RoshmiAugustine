IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210430121417_API_PERM_AddUpdateHelper_ExternalApi')
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
    --Declaration of variables ends.
    --'api/helper/add-external' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/helper/add-external')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/helper/add-external','SaveHelperDetailsForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/helper/add-external')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/helper/add-external')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@POST, 'SaveHelperDetailsForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @POST)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/helper/add-external' ends
    -------------------------------------------------------
    SET @appObjId = 0
    --'api/helper/update-external' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/helper/update-external')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/helper/update-external','UpdateHelperDetailsForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/helper/update-external')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/helper/update-external')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@PUT, 'UpdateHelperDetailsForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @PUT)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'helper/update-external' ends
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210430121417_API_PERM_AddUpdateHelper_ExternalApi', N'3.1.4');
END;

GO

