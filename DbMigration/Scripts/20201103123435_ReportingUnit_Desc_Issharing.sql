IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201103123435_ReportingUnit_Desc_Issharing')
BEGIN
    ALTER TABLE [ReportingUnit] ADD [Description] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201103123435_ReportingUnit_Desc_Issharing')
BEGIN
    ALTER TABLE [ReportingUnit] ADD [IsSharing] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201103123435_ReportingUnit_Desc_Issharing')
BEGIN
    ALTER TABLE [AgencySharing] ADD [IsSharing] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201103123435_ReportingUnit_Desc_Issharing')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201103123435_ReportingUnit_Desc_Issharing', N'3.1.4');
END;

GO

