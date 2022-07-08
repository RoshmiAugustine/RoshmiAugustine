IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005112801_UI_PERM_RemovePersonCollaboration')
BEGIN
	if not exists(select * from ApplicationObject WHERE Name = 'People/Profile/Collaboration')
	BEGIN
		INSERT INTO ApplicationObject VALUES(1,'People/Profile/Collaboration', 'Person Collaboration', 133,0, GETDATE(), 1)
	END
	if not exists(select * from info.Permission where Description = 'Remove Person Collaboration')
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Name= 'People/Profile/Collaboration'),(select OperationTypeID from info.OperationType where Name = 'DELETE'),'Remove Person Collaboration', 133, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Remove Person Collaboration'))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description = 'Remove Person Collaboration'
		insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description = 'Remove Person Collaboration'
		insert into info.SystemrolePermission select 4, PermissionID from info.Permission where Description = 'Remove Person Collaboration'
	end
	insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)
   
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201005112801_UI_PERM_RemovePersonCollaboration', N'3.1.4');
END;

GO

