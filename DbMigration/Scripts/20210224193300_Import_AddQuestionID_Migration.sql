IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210224193300_Import_AddQuestionID_Migration')
BEGIN
    ALTER TABLE [FileImport] ADD [QuestionnaireID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210224193300_Import_AddQuestionID_Migration')
BEGIN
    CREATE INDEX [IX_FileImport_QuestionnaireID] ON [FileImport] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210224193300_Import_AddQuestionID_Migration')
BEGIN
    ALTER TABLE [FileImport] ADD CONSTRAINT [FK_FileImport_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210224193300_Import_AddQuestionID_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210224193300_Import_AddQuestionID_Migration', N'3.1.4');
END;

GO

