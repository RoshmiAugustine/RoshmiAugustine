IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210902090040_ItemNewColumnIsExpandableMigration')
BEGIN
    ALTER TABLE [Item] ADD [IsExpandable] bit NOT NULL DEFAULT CAST(1 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210902090040_ItemNewColumnIsExpandableMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210902090040_ItemNewColumnIsExpandableMigration', N'3.1.4');
END;

GO

