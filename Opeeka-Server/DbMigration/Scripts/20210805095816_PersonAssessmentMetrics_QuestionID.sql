IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805095816_PersonAssessmentMetrics_QuestionID')
BEGIN
    ALTER TABLE [dbo].[PersonAssessmentMetrics] ADD [QuestionnaireID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805095816_PersonAssessmentMetrics_QuestionID')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210805095816_PersonAssessmentMetrics_QuestionID', N'3.1.4');
END;

GO

