IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210429164826_API_PERM_ADD_GetPermissions')
BEGIN
    --Declaration of variables starts
    DECLARE @appObjId int;
    DECLARE @OperTypeID int;
    DECLARE @PermissionID int;
    DECLARE @GET int = 1;
    DECLARE @POST int = 2;
    DECLARE @APIUser int  = 9;
    --Declaration of variables ends
     --'api/language-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/language-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/language-external/{pageNumber}/{pageSize}','GetLanguageListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/language-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/language-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetLanguageListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/language-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/country-states-external' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/country-states-external')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/country-states-external','GetAllCountryStateForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/country-states-external')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/country-states-external')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetAllCountryStateForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/country-states-external' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/collaboration-level-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/collaboration-level-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/collaboration-level-external/{pageNumber}/{pageSize}','GetCollaborationLevelListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/collaboration-level-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/collaboration-level-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetCollaborationLevelListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/collaboration-level-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/collaboration-tag-type-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/collaboration-tag-type-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/collaboration-tag-type-external/{pageNumber}/{pageSize}','GetCollaborationTagTypeListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/collaboration-tag-type-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/collaboration-tag-type-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetCollaborationTagTypeListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/collaboration-tag-type-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/therapy-type-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/therapy-type-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/therapy-type-external/{pageNumber}/{pageSize}','GetTherapyTypeListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/therapy-type-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/therapy-type-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetTherapyTypeListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/therapy-type-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/helper-title-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/helper-title-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/helper-title-external/{pageNumber}/{pageSize}','GetHelperTitleListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/helper-title-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/helper-title-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetHelperTitleListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/helper-title-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/notification-level-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/notification-level-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/notification-level-external/{pageNumber}/{pageSize}','GetNotificationLevelListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/notification-level-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/notification-level-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetNotificationLevelListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/notification-level-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/gender-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/gender-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/gender-external/{pageNumber}/{pageSize}','GetGenderListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/gender-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/gender-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetGenderListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/gender-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/identification-type-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/identification-type-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/identification-type-external/{pageNumber}/{pageSize}','GetIdentificationTypeListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/identification-type-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/identification-type-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetIdentificationTypeListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/identification-type-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/race-ethnicity-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/race-ethnicity-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/race-ethnicity-external/{pageNumber}/{pageSize}','GetRaceEthnicityListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/race-ethnicity-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/race-ethnicity-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetRaceEthnicityListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/race-ethnicity-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/support-type-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/support-type-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/support-type-external/{pageNumber}/{pageSize}','GetSupportTypeListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/support-type-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/support-type-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@POST, 'GetSupportTypeListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @POST)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/support-type-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/sexuality-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/sexuality-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/sexuality-external/{pageNumber}/{pageSize}','GetSexualityListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/sexuality-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/sexuality-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetSexualityListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/sexuality-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/identified-gender-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/identified-gender-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/identified-gender-external/{pageNumber}/{pageSize}','GetIdentifiedGenderListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/identified-gender-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/identified-gender-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetIdentifiedGenderListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/identified-gender-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/countries-external' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/countries-external')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/countries-external','GetAllCountriesForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/countries-external')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/countries-external')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetAllCountriesForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/countries-external' ends
    -------------------------------------------------------
    SET @appObjId = 0
     --'api/system-role-external/{pageNumber}/{pageSize}' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/system-role-external/{pageNumber}/{pageSize}')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/system-role-external/{pageNumber}/{pageSize}','GetSystemRoleListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/system-role-external/{pageNumber}/{pageSize}')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/system-role-external/{pageNumber}/{pageSize}')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@GET, 'GetSystemRoleListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END
    --'api/system-role-external/{pageNumber}/{pageSize}' ends
    -------------------------------------------------------
      --'api/questionnaires-external' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/questionnaires-external')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/questionnaires-external','GetQuestionnaireListForExternal',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/questionnaires-external')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/questionnaires-external')
       END
    IF(@appObjId <> 0)
    BEGIN
        insert into info.Permission values(@appObjId,@POST, 'GetQuestionnaireListForExternal',1,GETDATE(),1,0)
        SET @PermissionID = (SELECT top 1 PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @POST)
        insert into info.SystemRolePermission values(@APIUser,@PermissionID);
    END

END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210429164826_API_PERM_ADD_GetPermissions', N'3.1.4');
END;

GO

