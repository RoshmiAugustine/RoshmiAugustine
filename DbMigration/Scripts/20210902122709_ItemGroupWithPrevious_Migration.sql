IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210902122709_ItemGroupWithPrevious_Migration')
BEGIN
    ALTER TABLE [Item] ADD [GroupWithPrevious] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210902122709_ItemGroupWithPrevious_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210902122709_ItemGroupWithPrevious_Migration', N'3.1.4');
END;

GO

