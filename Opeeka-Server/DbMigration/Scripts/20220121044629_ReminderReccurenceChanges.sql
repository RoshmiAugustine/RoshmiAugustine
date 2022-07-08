IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121044629_ReminderReccurenceChanges')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[info].[TimeZones]') AND [c].[name] = N'Name');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [info].[TimeZones] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [info].[TimeZones] ALTER COLUMN [Name] nvarchar(100) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121044629_ReminderReccurenceChanges')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[info].[RecurrencePattern]') AND [c].[name] = N'Name');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [info].[RecurrencePattern] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [info].[RecurrencePattern] ALTER COLUMN [Name] nvarchar(20) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121044629_ReminderReccurenceChanges')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[info].[RecurrenceEndType]') AND [c].[name] = N'Name');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [info].[RecurrenceEndType] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [info].[RecurrenceEndType] ALTER COLUMN [Name] nvarchar(20) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121044629_ReminderReccurenceChanges')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireRegularReminderRecurrence]') AND [c].[name] = N'RecurrenceOrdinalIDs');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireRegularReminderRecurrence] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [QuestionnaireRegularReminderRecurrence] ALTER COLUMN [RecurrenceOrdinalIDs] nvarchar(30) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121044629_ReminderReccurenceChanges')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireRegularReminderRecurrence]') AND [c].[name] = N'RecurrenceMonthIDs');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireRegularReminderRecurrence] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [QuestionnaireRegularReminderRecurrence] ALTER COLUMN [RecurrenceMonthIDs] nvarchar(30) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121044629_ReminderReccurenceChanges')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireRegularReminderRecurrence]') AND [c].[name] = N'RecurrenceDayNameIDs');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireRegularReminderRecurrence] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [QuestionnaireRegularReminderRecurrence] ALTER COLUMN [RecurrenceDayNameIDs] nvarchar(30) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121044629_ReminderReccurenceChanges')
BEGIN
    ALTER TABLE [PersonQuestionnaireSchedule] ADD [UniqueCounter] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121044629_ReminderReccurenceChanges')
BEGIN
    ALTER TABLE [PersonQuestionnaireSchedule] ADD [WindowCloseDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121044629_ReminderReccurenceChanges')
BEGIN
    ALTER TABLE [PersonQuestionnaireSchedule] ADD [WindowOpenDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121044629_ReminderReccurenceChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220121044629_ReminderReccurenceChanges', N'3.1.4');
END;

GO

