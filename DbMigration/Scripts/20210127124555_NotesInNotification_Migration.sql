IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210127124555_NotesInNotification_Migration')
BEGIN
    ALTER TABLE [NotificationLog] ADD [AssessmentNoteID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210127124555_NotesInNotification_Migration')
BEGIN
    ALTER TABLE [Assessment] ADD [NoteUpdateDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210127124555_NotesInNotification_Migration')
BEGIN
    ALTER TABLE [Assessment] ADD [NoteUpdateUserID] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210127124555_NotesInNotification_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210127124555_NotesInNotification_Migration', N'3.1.4');
END;

GO

