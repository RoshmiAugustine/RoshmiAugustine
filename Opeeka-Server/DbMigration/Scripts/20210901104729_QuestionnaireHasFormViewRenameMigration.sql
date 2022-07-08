IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901104729_QuestionnaireHasFormViewRenameMigration')
BEGIN
    EXEC sp_rename N'[Questionnaire].[hasFormView]', N'HasFormView', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901104729_QuestionnaireHasFormViewRenameMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210901104729_QuestionnaireHasFormViewRenameMigration', N'3.1.4');
END;

GO

