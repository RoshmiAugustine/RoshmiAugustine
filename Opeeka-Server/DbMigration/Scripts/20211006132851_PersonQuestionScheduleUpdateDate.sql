IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211006132851_PersonQuestionScheduleUpdateDate')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonQuestionnaireSchedule]') AND [c].[name] = N'UpdateDate');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [PersonQuestionnaireSchedule] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [PersonQuestionnaireSchedule] ALTER COLUMN [UpdateDate] datetime2 NOT NULL;
    ALTER TABLE [PersonQuestionnaireSchedule] ADD DEFAULT (getdate()) FOR [UpdateDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211006132851_PersonQuestionScheduleUpdateDate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211006132851_PersonQuestionScheduleUpdateDate', N'3.1.4');
END;

GO

