IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211005122252_ReminderScheduleColumnChanges')
BEGIN
    ALTER TABLE [PersonQuestionnaireSchedule] ADD [UpdateDate] datetime2 NOT NULL DEFAULT getdate();    
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211005122252_ReminderScheduleColumnChanges')
BEGIN
    ALTER TABLE [NotifyReminder] ADD [IsRemoved] bit NOT NULL DEFAULT CAST(0 AS bit);    
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211005122252_ReminderScheduleColumnChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211005122252_ReminderScheduleColumnChanges', N'3.1.4');
END;

GO

