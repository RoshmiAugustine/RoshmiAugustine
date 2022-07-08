IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901073058_QuestionnaireNewColumnHasFormViewMigration')
BEGIN
    ALTER TABLE [Questionnaire] ADD [hasFormView] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210901073058_QuestionnaireNewColumnHasFormViewMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210901073058_QuestionnaireNewColumnHasFormViewMigration', N'3.1.4');
END;

GO

