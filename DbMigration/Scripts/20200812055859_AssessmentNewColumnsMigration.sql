IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200812055859_AssessmentNewColumnsMigration')
BEGIN
    ALTER TABLE [Assessment] ADD [EventDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200812055859_AssessmentNewColumnsMigration')
BEGIN
    ALTER TABLE [Assessment] ADD [EventNotes] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200812055859_AssessmentNewColumnsMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200812055859_AssessmentNewColumnsMigration', N'3.1.4');
END;

GO

