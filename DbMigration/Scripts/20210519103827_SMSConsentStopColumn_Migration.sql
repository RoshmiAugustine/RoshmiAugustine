IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210519103827_SMSConsentStopColumn_Migration')
BEGIN
    ALTER TABLE [PersonSupport] ADD [SMSConsentStoppedON] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210519103827_SMSConsentStopColumn_Migration')
BEGIN
    ALTER TABLE [Person] ADD [SMSConsentStoppedON] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210519103827_SMSConsentStopColumn_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210519103827_SMSConsentStopColumn_Migration', N'3.1.4');
END;

GO

