IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210531070159_AssessmentResponseParent_Migration')
BEGIN
    ALTER TABLE [AssessmentResponse] ADD [ParentAssessmentResponseID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210531070159_AssessmentResponseParent_Migration')
BEGIN
    CREATE INDEX [IX_AssessmentResponse_ParentAssessmentResponseID] ON [AssessmentResponse] ([ParentAssessmentResponseID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210531070159_AssessmentResponseParent_Migration')
BEGIN
    ALTER TABLE [AssessmentResponse] ADD CONSTRAINT [FK_AssessmentResponse_AssessmentResponse_ParentAssessmentResponseID] FOREIGN KEY ([ParentAssessmentResponseID]) REFERENCES [AssessmentResponse] ([AssessmentResponseID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210531070159_AssessmentResponseParent_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210531070159_AssessmentResponseParent_Migration', N'3.1.4');
END;

GO

