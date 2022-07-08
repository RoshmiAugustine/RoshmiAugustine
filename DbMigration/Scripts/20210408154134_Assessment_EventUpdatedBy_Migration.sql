IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408154134_Assessment_EventUpdatedBy_Migration')
BEGIN
    ALTER TABLE [Assessment] ADD [EventNoteUpdatedBy] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210408154134_Assessment_EventUpdatedBy_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210408154134_Assessment_EventUpdatedBy_Migration', N'3.1.4');
END;

GO

