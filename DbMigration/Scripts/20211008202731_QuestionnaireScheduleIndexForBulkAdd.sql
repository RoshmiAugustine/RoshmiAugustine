IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211008202731_QuestionnaireScheduleIndexForBulkAdd')
BEGIN
    ALTER TABLE [PersonQuestionnaireSchedule] ADD [PersonQuestionnaireScheduleIndex] uniqueidentifier NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211008202731_QuestionnaireScheduleIndexForBulkAdd')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211008202731_QuestionnaireScheduleIndexForBulkAdd', N'3.1.4');
END;

GO

