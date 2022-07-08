IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201222135608_Helper_HelperExternalID')
BEGIN
    ALTER TABLE [Helper] ADD [HelperExternalID] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201222135608_Helper_HelperExternalID')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201222135608_Helper_HelperExternalID', N'3.1.4');
END;

GO

