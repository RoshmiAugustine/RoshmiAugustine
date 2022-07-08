IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201122302_NotificationLog_Optimize_Migration')
BEGIN
    ALTER TABLE [NotificationLog] ADD [AssessmentID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201122302_NotificationLog_Optimize_Migration')
BEGIN
    ALTER TABLE [NotificationLog] ADD [Details] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201122302_NotificationLog_Optimize_Migration')
BEGIN
    ALTER TABLE [NotificationLog] ADD [QuestionnaireID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210201122302_NotificationLog_Optimize_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210201122302_NotificationLog_Optimize_Migration', N'3.1.4');
END;

GO

