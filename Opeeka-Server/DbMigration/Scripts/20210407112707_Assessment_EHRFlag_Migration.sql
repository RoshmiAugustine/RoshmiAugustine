IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210407112707_Assessment_EHRFlag_Migration')
BEGIN
    ALTER TABLE [Assessment] ADD [EHRUpdateStatus] nvarchar(20) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210407112707_Assessment_EHRFlag_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210407112707_Assessment_EHRFlag_Migration', N'3.1.4');
END;

GO

