IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210073548_AssessmentResponseAddColumnGuid')
BEGIN
    ALTER TABLE [Note] ADD [NoteGUID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210073548_AssessmentResponseAddColumnGuid')
BEGIN
    ALTER TABLE [AssessmentResponse] ADD [AssessmentResponseGuid] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210073548_AssessmentResponseAddColumnGuid')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210210073548_AssessmentResponseAddColumnGuid', N'3.1.4');
END;

GO

