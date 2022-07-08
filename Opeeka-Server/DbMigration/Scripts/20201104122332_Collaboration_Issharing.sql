IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201104122332_Collaboration_Issharing')
BEGIN
    ALTER TABLE [CollaborationSharing] ADD [IsSharing] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201104122332_Collaboration_Issharing')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201104122332_Collaboration_Issharing', N'3.1.4');
END;

GO

