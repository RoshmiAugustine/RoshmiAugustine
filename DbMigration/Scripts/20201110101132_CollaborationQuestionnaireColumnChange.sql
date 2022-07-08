IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201110101132_CollaborationQuestionnaireColumnChange')
BEGIN
    ALTER TABLE [CollaborationQuestionnaire] ADD [IsReminderOn] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201110101132_CollaborationQuestionnaireColumnChange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201110101132_CollaborationQuestionnaireColumnChange', N'3.1.4');
END;

GO

