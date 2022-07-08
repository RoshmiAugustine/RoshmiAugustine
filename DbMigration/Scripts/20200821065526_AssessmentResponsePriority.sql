IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200821065526_AssessmentResponsePriority')
BEGIN
    ALTER TABLE [AssessmentResponse] ADD [Priority] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200821065526_AssessmentResponsePriority')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200821065526_AssessmentResponsePriority', N'3.1.4');
END;

GO

