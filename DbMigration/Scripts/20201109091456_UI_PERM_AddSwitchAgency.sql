IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201109091456_UI_PERM_AddSwitchAgency')
BEGIN
	if not exists(select * from ApplicationObject WHERE Name = 'Switch Agency')
	BEGIN
		INSERT INTO ApplicationObject VALUES(2,'Switch Agency', 'Switch Agency', 6,0, GETDATE(), 1)
	END
	if not exists(select * from info.Permission where Description = 'Switch Agency')
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Name= 'Switch Agency'),(select OperationTypeID from info.OperationType where Name = 'View'),'Switch Agency', 6, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Switch Agency'))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description = 'Switch Agency'
	end
	insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)
   
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201109091456_UI_PERM_AddSwitchAgency', N'3.1.4');
END;

GO

