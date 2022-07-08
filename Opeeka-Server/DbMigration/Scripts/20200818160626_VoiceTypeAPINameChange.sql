IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200818160626_VoiceTypeAPINameChange')
BEGIN
    UPDATE dbo.ApplicationObject SET Name = '/api/voice-type/{personIndex}' WHERE Name = '/api/voice-type';
END

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200818160626_VoiceTypeAPINameChange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200818160626_VoiceTypeAPINameChange', N'3.1.4');
END;

GO

