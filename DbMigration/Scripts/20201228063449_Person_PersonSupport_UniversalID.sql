IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201228063449_Person_PersonSupport_UniversalID')
BEGIN
    ALTER TABLE [PersonSupport] ADD [UniversalID] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201228063449_Person_PersonSupport_UniversalID')
BEGIN
    ALTER TABLE [Person] ADD [UniversalID] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201228063449_Person_PersonSupport_UniversalID')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201228063449_Person_PersonSupport_UniversalID', N'3.1.4');
END;

GO

