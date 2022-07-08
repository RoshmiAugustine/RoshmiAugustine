IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220106075136_Insight_API_PERM')
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
    DECLARE @Assessor int  = 10;
    --Declaration of variables ends
    --'api/insight-dashboard-details' starts
    IF NOT EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = 'api/insight-dashboard-details')
       BEGIN
         INSERT INTO ApplicationObject VALUES(3,'api/insight-dashboard-details','GetAgencyInsightDashboardDetails',1, 0,GETDATE(),1)
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/insight-dashboard-details')
       END
    ELSE
       BEGIN
         SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = 'api/insight-dashboard-details')
       END
    insert into info.Permission values(@appObjId,@GET, 'GetAgencyInsightDashboardDetails',1,GETDATE(),1,0)
    SET @PermissionID = (SELECT PermissionID from info.Permission where ApplicationObjectID = @appObjId AND OperationTypeID = @GET)
    insert into info.SystemRolePermission values(@SuperAdmin,@PermissionID);
    insert into info.SystemRolePermission values(@OrgAdminRW,@PermissionID);
    insert into info.SystemRolePermission values(@OrgAdminRO,@PermissionID);
    insert into info.SystemRolePermission values(@Supervisor,@PermissionID);
    insert into info.SystemRolePermission values(@HelperRW,@PermissionID);
    insert into info.SystemRolePermission values(@HelperRO,@PermissionID);
    insert into info.SystemRolePermission values(@Assessor,@PermissionID);
    --'api/assessment/list-external' ends
    -------------------------------------------------------

    Update ApplicationObject set name = 'api/get-insight-url/{agencyInsightDashboardId}', Description = 'Get sisense url for a insightDashboardId' 
      where ApplicationObjectId = (Select ApplicationObjectId from ApplicationObject where name = '/api/get-report-url' )
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220106075136_Insight_API_PERM', N'3.1.4');
END;

GO

