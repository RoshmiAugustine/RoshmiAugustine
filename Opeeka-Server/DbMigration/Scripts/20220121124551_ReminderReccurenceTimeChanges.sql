IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121124551_ReminderReccurenceTimeChanges')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireRegularReminderTimeRule]') AND [c].[name] = N'Time');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireRegularReminderTimeRule] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [QuestionnaireRegularReminderTimeRule] ALTER COLUMN [Time] nvarchar(6) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121124551_ReminderReccurenceTimeChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220121124551_ReminderReccurenceTimeChanges', N'3.1.4');
END;

GO

