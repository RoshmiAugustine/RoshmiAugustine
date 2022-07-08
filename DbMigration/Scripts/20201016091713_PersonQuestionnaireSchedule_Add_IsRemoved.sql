IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201016091713_PersonQuestionnaireSchedule_Add_IsRemoved')
BEGIN
    ALTER TABLE [PersonQuestionnaireSchedule] ADD [IsRemoved] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201016091713_PersonQuestionnaireSchedule_Add_IsRemoved')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201016091713_PersonQuestionnaireSchedule_Add_IsRemoved', N'3.1.4');
END;

GO

