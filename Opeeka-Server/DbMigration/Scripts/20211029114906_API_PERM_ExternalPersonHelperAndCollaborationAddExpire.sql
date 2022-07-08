IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211029114906_API_PERM_ExternalPersonHelperAndCollaborationAddExpire')
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
    --'api/person/add-helper' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/add-helper')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/person/add-helper','Add PersonHelper For External',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/add-helper')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/add-helper')
       END 
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@POST, 'Add PersonHelper For External',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @POST)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/person/add-helper' ends
    SET @appObjId = 0
    
    --'api/person/add-collaboration' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/add-collaboration')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/person/add-collaboration','Add PersonCollaboration For External',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/add-collaboration')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/add-collaboration')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@POST, 'Add PersonCollaboration For External',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @POST)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/person/add-collaboration' ends
        SET @appObjId = 0
    
    --'api/person/expire-helper' starts
   IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/expire-helper')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/person/expire-helper','Expire PersonHelper For External',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/expire-helper')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/expire-helper')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@PUT, 'Expire PersonHelper For External',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @PUT)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/person/expire-helper' ends

     
    --'api/person/expire-collaboration' starts
   IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/expire-collaboration')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/person/expire-collaboration','Expire PersonCollaboration For External',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/expire-collaboration')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/person/expire-collaboration')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@PUT, 'Expire PersonCollaboration For External',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @PUT)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/person/expire-collaboration' ends
    -------------------------------------------------------    
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211029114906_API_PERM_ExternalPersonHelperAndCollaborationAddExpire', N'3.1.4');
END;
END;
GO



