IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200922082300_UI_PERM_PersonRemovePermission')
BEGIN
  if not exists(select * from info.Permission where Description = 'Remove Person')
  begin
  insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Name= 'People/Profile/Person'),(select OperationTypeID from info.OperationType where Name = 'DELETE'),'Remove Person', 3, getdate(), 1,0)
  end
  if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Remove Person'))
  begin
  insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description = 'Remove Person'
  insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description = 'Remove Person'
  end
  
  insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200922082300_UI_PERM_PersonRemovePermission', N'3.1.4');
END;

GO

