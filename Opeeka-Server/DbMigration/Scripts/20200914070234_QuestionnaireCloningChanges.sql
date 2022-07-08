IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200914070234_QuestionnaireCloningChanges')
BEGIN
    ALTER TABLE [QuestionnaireNotifyRiskRule] ADD [ClonedQuestionnaireNotifyRiskRuleID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200914070234_QuestionnaireCloningChanges')
BEGIN
    ALTER TABLE [QuestionnaireItem] ADD [ClonedQuestionnaireItemId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200914070234_QuestionnaireCloningChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200914070234_QuestionnaireCloningChanges', N'3.1.4');
END;

GO

