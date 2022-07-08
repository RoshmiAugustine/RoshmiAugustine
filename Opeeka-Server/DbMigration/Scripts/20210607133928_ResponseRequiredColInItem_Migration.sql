IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210607133928_ResponseRequiredColInItem_Migration')
BEGIN
    ALTER TABLE [Item] ADD [ResponseRequired] bit NOT NULL DEFAULT CAST(1 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210607133928_ResponseRequiredColInItem_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210607133928_ResponseRequiredColInItem_Migration', N'3.1.4');
END;

GO

