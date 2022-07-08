IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220218070732_RemoveInsightPermissionForSomeRoles')
BEGIN	
	DECLARE @ApplicationObjectID int = 0;
	---------------------------------------------//Insights - Menu view permission in UI //------------------------------------------------
	select @ApplicationObjectID = ApplicationObjectID from applicationobject where name like '%insight%' and ApplicationObjectTypeID in (
	select ApplicationObjectTypeID from ApplicationObjectType where Name = 'UIComponentMenu' )
	IF (@ApplicationObjectID > 0)
	BEGIN
		delete  SRP from info.SystemRolePermission SRP
		JOIN info.systemRole SR ON SRP.SystemRoleID = SR.SystemRoleID 
		where PermissionID in (
		select PermissionID from info.Permission where ApplicationObjectID = @ApplicationObjectID )
		and (SR.Name = 'Helper RW' OR SR.Name = 'Helper RO' OR SR.Name = 'Assessor' )
	END
	-----------------------------------// api/insight-dashboard-details - API permission //-----------------------------------------------------------------
	SET @ApplicationObjectID  = 0
	select @ApplicationObjectID = ApplicationObjectID from applicationobject where name like 'api/insight-dashboard-details' and ApplicationObjectTypeID in (
	select ApplicationObjectTypeID from ApplicationObjectType where Name = 'APIEndPoint' )
	IF (@ApplicationObjectID > 0)
	BEGIN
		delete  SRP from info.SystemRolePermission SRP
		JOIN info.systemRole SR ON SRP.SystemRoleID = SR.SystemRoleID 
		where PermissionID in (
		select PermissionID from info.Permission where ApplicationObjectID = @ApplicationObjectID )
		and (SR.Name = 'Helper RW' OR SR.Name = 'Helper RO' OR SR.Name = 'Assessor' )
	END
	-----------------------------------// api/get-insight-url/{agencyInsightDashboardId} - API permission //----------------------------------------------------
	SET @ApplicationObjectID  = 0
	select @ApplicationObjectID = ApplicationObjectID from applicationobject where name like 'api/get-insight-url%' and ApplicationObjectTypeID in (
	select ApplicationObjectTypeID from ApplicationObjectType where Name = 'APIEndPoint' )
	IF (@ApplicationObjectID > 0)
	BEGIN
		delete  SRP from info.SystemRolePermission SRP
		JOIN info.systemRole SR ON SRP.SystemRoleID = SR.SystemRoleID 
		where PermissionID in (
		select PermissionID from info.Permission where ApplicationObjectID = @ApplicationObjectID )
		and (SR.Name = 'Helper RW' OR SR.Name = 'Helper RO' OR SR.Name = 'Assessor' )
	END
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220218070732_RemoveInsightPermissionForSomeRoles', N'3.1.4');
END;

GO

