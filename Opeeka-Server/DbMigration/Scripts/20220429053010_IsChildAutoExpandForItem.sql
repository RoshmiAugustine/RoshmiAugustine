IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220429053010_IsChildAutoExpandForItem')
BEGIN
    ALTER TABLE [Item] ADD [IsChildAutoExpand] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220429053010_IsChildAutoExpandForItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220429053010_IsChildAutoExpandForItem', N'3.1.4');
END;

GO

