IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013133727_UI_PERM_AddQuestionnaire_Change')
BEGIN
    if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Add Questionnaire' And ApplicationObjectID=111 And OperationTypeID=5 and SystemRoleID=5))
    BEGIN
        insert into info.SystemrolePermission select 5, PermissionID from info.Permission where Description = 'Add Questionnaire' And ApplicationObjectID=111 And OperationTypeID=5
    END;
    insert into info.RolePermission select ur.UserRoleID , srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201013133727_UI_PERM_AddQuestionnaire_Change', N'3.1.4');
END;

GO

