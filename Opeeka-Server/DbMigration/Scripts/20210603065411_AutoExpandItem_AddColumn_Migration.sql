IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210603065411_AutoExpandItem_AddColumn_Migration')
BEGIN
    ALTER TABLE [Item] ADD [AutoExpand] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210603065411_AutoExpandItem_AddColumn_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210603065411_AutoExpandItem_AddColumn_Migration', N'3.1.4');
END;

GO

