IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106052525_SharingRolePermission')
BEGIN
    CREATE TABLE [info].[SharingRolePermission] (
        [SharingRolePermissionID] int NOT NULL IDENTITY,
        [SystemRolePermissionID] int NOT NULL,
        [AgencySharingPolicyID] int NOT NULL,
        [CollaborationSharingPolicyID] int NOT NULL,
        [AllowInactiveAccess] bit NOT NULL,
        CONSTRAINT [PK_SharingRolePermission] PRIMARY KEY ([SharingRolePermissionID]),
        CONSTRAINT [FK_SharingRolePermission_AgencySharingPolicy_AgencySharingPolicyID] FOREIGN KEY ([AgencySharingPolicyID]) REFERENCES [AgencySharingPolicy] ([AgencySharingPolicyID]) ON DELETE CASCADE,
        CONSTRAINT [FK_SharingRolePermission_CollaborationSharingPolicy_CollaborationSharingPolicyID] FOREIGN KEY ([CollaborationSharingPolicyID]) REFERENCES [CollaborationSharingPolicy] ([CollaborationSharingPolicyID]) ON DELETE CASCADE,
        CONSTRAINT [FK_SharingRolePermission_SystemRolePermission_SystemRolePermissionID] FOREIGN KEY ([SystemRolePermissionID]) REFERENCES [info].[SystemRolePermission] ([SystemRolePermissionID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106052525_SharingRolePermission')
BEGIN
    CREATE INDEX [IX_SharingRolePermission_AgencySharingPolicyID] ON [info].[SharingRolePermission] ([AgencySharingPolicyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106052525_SharingRolePermission')
BEGIN
    CREATE INDEX [IX_SharingRolePermission_CollaborationSharingPolicyID] ON [info].[SharingRolePermission] ([CollaborationSharingPolicyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106052525_SharingRolePermission')
BEGIN
    CREATE INDEX [IX_SharingRolePermission_SystemRolePermissionID] ON [info].[SharingRolePermission] ([SystemRolePermissionID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106052525_SharingRolePermission')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201106052525_SharingRolePermission', N'3.1.4');
END;

GO

