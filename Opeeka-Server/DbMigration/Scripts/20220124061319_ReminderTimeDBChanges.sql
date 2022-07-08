IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220124061319_ReminderTimeDBChanges')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireRegularReminderTimeRule]') AND [c].[name] = N'Time');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireRegularReminderTimeRule] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [QuestionnaireRegularReminderTimeRule] DROP COLUMN [Time];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220124061319_ReminderTimeDBChanges')
BEGIN
    ALTER TABLE [QuestionnaireRegularReminderTimeRule] ADD [AMorPM] nvarchar(6) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220124061319_ReminderTimeDBChanges')
BEGIN
    ALTER TABLE [QuestionnaireRegularReminderTimeRule] ADD [Hour] nvarchar(6) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220124061319_ReminderTimeDBChanges')
BEGIN
    ALTER TABLE [QuestionnaireRegularReminderTimeRule] ADD [Minute] nvarchar(6) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220124061319_ReminderTimeDBChanges')
BEGIN
    ALTER TABLE [NotifyReminder] ADD [InviteToCompleteMailStatus] nvarchar(10) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220124061319_ReminderTimeDBChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220124061319_ReminderTimeDBChanges', N'3.1.4');
END;

GO

