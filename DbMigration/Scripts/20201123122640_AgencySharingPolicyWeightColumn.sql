IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123122640_AgencySharingPolicyWeightColumn')
BEGIN
    ALTER TABLE [AgencySharingPolicy] ADD [Weight] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123122640_AgencySharingPolicyWeightColumn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201123122640_AgencySharingPolicyWeightColumn', N'3.1.4');
END;

GO

