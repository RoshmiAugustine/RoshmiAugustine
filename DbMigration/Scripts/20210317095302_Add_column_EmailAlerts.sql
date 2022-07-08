IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317095302_Add_column_EmailAlerts')
BEGIN
    ALTER TABLE [Questionnaire] ADD [IsAlertsHelpersManagers] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317095302_Add_column_EmailAlerts')
BEGIN
    ALTER TABLE [Questionnaire] ADD [IsEmailRemindersHelpers] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317095302_Add_column_EmailAlerts')
BEGIN
    ALTER TABLE [Helper] ADD [IsEmailReminderAlerts] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317095302_Add_column_EmailAlerts')
BEGIN
    update [Helper] set [IsEmailReminderAlerts]=1;
END;
GO
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317095302_Add_column_EmailAlerts')
BEGIN
    update [Questionnaire] set [IsEmailRemindersHelpers]=1,[IsAlertsHelpersManagers]=1;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317095302_Add_column_EmailAlerts')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210317095302_Add_column_EmailAlerts', N'3.1.4');
END;

GO

