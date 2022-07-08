IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210223141013_ImportType_AddColumn_Migration')
BEGIN
    ALTER TABLE [info].[ImportType] ADD [ListOrder] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210223141013_ImportType_AddColumn_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210223141013_ImportType_AddColumn_Migration', N'3.1.4');
END;

GO

