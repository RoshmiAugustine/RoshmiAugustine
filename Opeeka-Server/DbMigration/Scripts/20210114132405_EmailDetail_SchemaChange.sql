IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210114132405_EmailDetail_SchemaChange')
BEGIN
    ALTER TABLE [EmailDetail] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210114132405_EmailDetail_SchemaChange')
BEGIN
    ALTER TABLE [EmailDetail] ADD [Type] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210114132405_EmailDetail_SchemaChange')
BEGIN
    ALTER TABLE [EmailDetail] ADD [URL] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210114132405_EmailDetail_SchemaChange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210114132405_EmailDetail_SchemaChange', N'3.1.4');
END;

GO

