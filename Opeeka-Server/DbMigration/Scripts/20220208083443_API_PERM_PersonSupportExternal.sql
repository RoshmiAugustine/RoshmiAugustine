IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220208083443_API_PERM_PersonSupportExternal')
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
    --'api/person/support-external' Method Starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/support-external')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/person/support-external','Add Or Edit PersonSupport For External',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/support-external')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/support-external')
       END 
       
    --'api/person/support-external' POST Method Starts
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@POST, 'Add PersonSupport For External',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @POST)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END    
    --'api/person/support-external' POST Method Ends
    
    --'api/person/support-external' PUT Method Starts   
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@PUT, 'Edit PersonSupport For External',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @PUT)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/person/support-external' PUT Method Ends    
    --'api/person/support-external' Method Ends

    SET @appObjId = 0
    
    --'api/person/expire-support-external' starts
   IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/expire-support-external')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/person/expire-support-external','Expire PersonSupport For External',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/expire-support-external')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/expire-support-external')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@PUT, 'Expire PersonSupport For External',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @PUT)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/person/expire-support-external' ends

END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220208083443_API_PERM_PersonSupportExternal', N'3.1.4');
END;

GO

