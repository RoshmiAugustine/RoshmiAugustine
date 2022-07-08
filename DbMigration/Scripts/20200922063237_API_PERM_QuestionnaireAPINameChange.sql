IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200922063237_API_PERM_QuestionnaireAPINameChange')
BEGIN
    UPDATE ApplicationObject SET Name = '/api/personQuestionnaire' where Name = '/api/personQuestionaire'
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200922063237_API_PERM_QuestionnaireAPINameChange', N'3.1.4');
END;

GO

