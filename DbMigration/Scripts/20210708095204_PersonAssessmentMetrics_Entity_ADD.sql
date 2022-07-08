IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708095204_PersonAssessmentMetrics_Entity_ADD')
BEGIN
    CREATE TABLE [dbo].[PersonAssessmentMetrics] (
        [PersonAssessmentMetricsID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NOT NULL,
        [InstrumentID] int NOT NULL,
        [PersonQuestionnaireID] int NOT NULL,
        [ItemID] int NOT NULL,
        [NeedsEver] int NOT NULL,
        [NeedsIdentified] int NOT NULL,
        [NeedsAddressed] int NOT NULL,
        [NeedsAddressing] int NOT NULL,
        [NeedsImproved] int NOT NULL,
        [StrengthsEver] int NOT NULL,
        [StrengthsIdentified] int NOT NULL,
        [StrengthsBuilt] int NOT NULL,
        [StrengthsBuilding] int NOT NULL,
        [StrengthsImproved] int NOT NULL,
        [AssessmentID] int NOT NULL,
        [updateDate] int NOT NULL,
        CONSTRAINT [PK_PersonAssessmentMetrics] PRIMARY KEY ([PersonAssessmentMetricsID]),
        CONSTRAINT [FK_PersonAssessmentMetrics_Assessment_AssessmentID] FOREIGN KEY ([AssessmentID]) REFERENCES [Assessment] ([AssessmentID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708095204_PersonAssessmentMetrics_Entity_ADD')
BEGIN
    CREATE INDEX [IX_PersonAssessmentMetrics_AssessmentID] ON [dbo].[PersonAssessmentMetrics] ([AssessmentID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210708095204_PersonAssessmentMetrics_Entity_ADD')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210708095204_PersonAssessmentMetrics_Entity_ADD', N'3.1.4');
END;

GO

