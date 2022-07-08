IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528164231_AllowMultipleItemGroups_Migration')
BEGIN
    ALTER TABLE [Item] ADD [AllowMultipleGroups] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528164231_AllowMultipleItemGroups_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210528164231_AllowMultipleItemGroups_Migration', N'3.1.4');
END;

GO

