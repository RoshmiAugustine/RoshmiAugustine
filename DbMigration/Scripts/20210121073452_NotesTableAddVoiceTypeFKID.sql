IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210121073452_NotesTableAddVoiceTypeFKID')
BEGIN
    ALTER TABLE [Note] ADD [AddedByVoiceTypeID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210121073452_NotesTableAddVoiceTypeFKID')
BEGIN
    ALTER TABLE [Note] ADD [VoiceTypeFKID] bigint NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210121073452_NotesTableAddVoiceTypeFKID')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210121073452_NotesTableAddVoiceTypeFKID', N'3.1.4');
END;

GO

